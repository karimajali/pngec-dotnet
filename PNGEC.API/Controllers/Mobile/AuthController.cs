using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PNGEC.API.Domain.Mobile;
using PNGEC.API.Domain.Mobile.User;
using PNGEC.API.Domain.User;
using PNGEC.API.Helper;
using PNGEC.API.Service.User;

namespace PNGEC.API.Controllers.Mobile
{
    /// <summary>
    /// Phase 1 & 2 Mobile Authentication & JWT Token Controller
    /// Issues 30-day JWT Bearer tokens for mobile devices, supports token renewal, and developer testing tokens.
    /// </summary>
    [Route("api/mobile/v2/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public AuthController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        /// <summary>
        /// Authenticates mobile DPO/operator credentials and issues a signed JWT Bearer Token (30-day validity).
        /// </summary>
        [HttpPost("Token")]
        [AllowAnonymous]
        public async Task<IActionResult> GenerateToken([FromBody] LoginUserRequestModel request)
        {
            var response = new Response();
            if (request == null || string.IsNullOrWhiteSpace(request.UserName))
            {
                response.Status = false;
                response.Message = "Username and password are required.";
                return BadRequest(response);
            }

            var loginResult = await _userService.MobileUserLogin(request);
            if (loginResult == null || !loginResult.Status || loginResult.Data == null)
            {
                response.Status = false;
                response.Message = loginResult?.Message ?? "Authentication failed. Invalid username or password.";
                return Unauthorized(response);
            }

            var userObj = loginResult.Data as dynamic;
            string userId = (userObj?.UserId ?? 0).ToString();
            string userName = request.UserName;
            string role = (userObj?.Role ?? "DPO").ToString();

            var tokenString = CreateJwtToken(userId, userName, role, 30);

            response.Status = true;
            response.Message = "Authentication successful. JWT Bearer token generated.";
            response.Data = new
            {
                Token = tokenString,
                TokenType = "Bearer",
                ExpiresInDays = 30,
                ExpiresAt = DateTime.UtcNow.AddDays(30),
                UserId = userId,
                UserName = userName,
                Role = role,
                UserData = loginResult.Data
            };

            return Ok(response);
        }

        /// <summary>
        /// Generates a test token for automated testing and field verification.
        /// </summary>
        [HttpPost("DevToken")]
        [AllowAnonymous]
        public IActionResult GenerateDevToken([FromBody] DevTokenRequest request)
        {
            string userName = string.IsNullOrWhiteSpace(request?.UserName) ? "dev_tester" : request.UserName;
            string role = string.IsNullOrWhiteSpace(request?.Role) ? "Admin" : request.Role;
            string userId = string.IsNullOrWhiteSpace(request?.UserId) ? "1" : request.UserId;
            int days = (request?.ExpiryDays > 0) ? request.ExpiryDays : 30;

            string token = CreateJwtToken(userId, userName, role, days);

            return Ok(new Response
            {
                Status = true,
                Message = "Dev JWT Token generated successfully.",
                Data = new
                {
                    Token = token,
                    TokenType = "Bearer",
                    ExpiresAt = DateTime.UtcNow.AddDays(days),
                    UserId = userId,
                    UserName = userName,
                    Role = role
                }
            });
        }

        /// <summary>
        /// Validates current JWT Bearer token and returns decoded claim identity.
        /// </summary>
        [HttpGet("VerifyToken")]
        [Authorize]
        public IActionResult VerifyToken()
        {
            var identity = User.Identity;
            return Ok(new Response
            {
                Status = true,
                Message = "Token is valid and active.",
                Data = new
                {
                    IsAuthenticated = identity?.IsAuthenticated ?? false,
                    UserName = User.FindFirst(ClaimTypes.Name)?.Value ?? identity?.Name,
                    UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                    Role = User.FindFirst(ClaimTypes.Role)?.Value
                }
            });
        }

        private string CreateJwtToken(string userId, string userName, string role, int expiryDays)
        {
            string secretKey = _configuration["JwtSettings:SecretKey"] ?? "PNGEC_Electoral_Roll_Secured_Key_2026_Dict_Portal_Key_1289!";
            string issuer = _configuration["JwtSettings:Issuer"] ?? "PNGEC_API";
            string audience = _configuration["JwtSettings:Audience"] ?? "PNGEC_Clients";

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId ?? "1"),
                new Claim(ClaimTypes.Name, userName ?? "User"),
                new Claim(ClaimTypes.Role, role ?? "DPO"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(expiryDays),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    public class DevTokenRequest
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public int ExpiryDays { get; set; } = 30;
    }
}
