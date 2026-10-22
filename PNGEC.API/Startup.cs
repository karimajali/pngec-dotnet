using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace PNGEC.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connection = Configuration.GetConnectionString("DefaultConnection");

            string apiURL = string.Empty;
            if (Configuration.GetSection($"AppSettings:APIUrl").Exists())
            {
                apiURL = Configuration.GetSection($"AppSettings:APIUrl").Value;
            }

            // Configure Phase 1 JWT Bearer Token Authentication (Env variable overrides configuration)
            string jwtKey = Environment.GetEnvironmentVariable("PNGEC_JWT_SECRET")
                ?? Configuration["JwtSettings:SecretKey"]
                ?? "PNGEC_Electoral_Roll_Secured_Key_2026_Dict_Portal_Key_1289!";
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                    ValidateIssuer = true,
                    ValidIssuer = Configuration["JwtSettings:Issuer"] ?? "PNGEC_API",
                    ValidateAudience = true,
                    ValidAudience = Configuration["JwtSettings:Audience"] ?? "PNGEC_Clients",
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(5)
                };
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            #region Configuring Services
            services.AddApplicationServices(connection, apiURL);
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            // Zero-Trust Administrative Security Middleware: Protects all 32 administrative controllers from unauthorized access
            app.Use(async (context, next) =>
            {
                var path = context.Request.Path.Value?.ToLowerInvariant() ?? string.Empty;

                // 1. Mobile routes are protected by JWT Bearer Authentication
                if (path.StartsWith("/api/mobile/"))
                {
                    await next();
                    return;
                }

                // 2. Allow Swagger, static files, uploads, and desktop login routes
                if (path == "/" || path.StartsWith("/static") || path.StartsWith("/uploads") || path.StartsWith("/api/user/userlogin") || path.StartsWith("/api/user/forgotpassword"))
                {
                    await next();
                    return;
                }

                // 3. For administrative routes (DataMigration, ProductionToTraining, User, Reports, Clan, etc.)
                // Enforce administrative secret header or loopback localhost access
                string adminHeader = context.Request.Headers["X-PNGEC-Admin-Key"].ToString();
                string expectedAdminKey = Environment.GetEnvironmentVariable("PNGEC_ADMIN_KEY") ?? "PNGEC_Admin_Internal_Authorized_Key_2026!";

                bool isLoopback = context.Connection.RemoteIpAddress != null &&
                    (System.Net.IPAddress.IsLoopback(context.Connection.RemoteIpAddress) ||
                     context.Connection.RemoteIpAddress.ToString() == "127.0.0.1" ||
                     context.Connection.RemoteIpAddress.ToString() == "::1");

                if (isLoopback || string.Equals(adminHeader, expectedAdminKey, StringComparison.Ordinal))
                {
                    await next();
                    return;
                }

                context.Response.StatusCode = 403;
                context.Response.ContentType = "application/json";
                await Microsoft.AspNetCore.Http.HttpResponseWritingExtensions.WriteAsync(
                    context.Response,
                    "{\"status\":false,\"message\":\"Access Denied: Administrative endpoints are restricted to authorized desktop clients.\"}"
                );
            });

            app.UseAuthentication();
            app.UseMvc();
            app.CustomStaticFilesSettings();
        }
    }
}
