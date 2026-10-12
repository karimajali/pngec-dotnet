using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using PNGEC.API.Service.AugmentedRollStat;
using PNGEC.API.Service.DataMigration;
using PNGEC.API.Service.Device;
using PNGEC.API.Service.District;
using PNGEC.API.Service.Document;
using PNGEC.API.Service.EnrolmentForm;
using PNGEC.API.Service.File;
using PNGEC.API.Service.General;
using PNGEC.API.Service.GeoStructure;
using PNGEC.API.Service.KupianoWardRoll;
using PNGEC.API.Service.LLG;
using PNGEC.API.Service.Location;
using PNGEC.API.Service.MKAClan;
using PNGEC.API.Service.MKADistrict;
using PNGEC.API.Service.MKALLG;
using PNGEC.API.Service.MKALocation;
using PNGEC.API.Service.MKAWard;
using PNGEC.API.Service.Occupation;
using PNGEC.API.Service.Province;
using PNGEC.API.Service.Region;
using PNGEC.API.Service.Report;
using PNGEC.API.Service.Role;
using PNGEC.API.Service.ScheduleLocSummary;
using PNGEC.API.Service.Setting;
using PNGEC.API.Service.User;
using PNGEC.API.Service.Voter;
using PNGEC.API.Service.Ward;
using PNGEC.API.Service.Clan;
using System.IO;
using PNGEC.API.Service.Writ;
using PNGEC.API.Service.ActivityLog;
using PNGEC.API.Service.ProductionToTrainingService;

namespace PNGEC.API
{
    public static class Extensions
    {
        /// <summary>
        /// Adds the application services.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="connection">The connection.</param>
        /// <param name="apiURL">The API URL.</param>
        public static void AddApplicationServices(this IServiceCollection services, string connection, string apiURL)
        {
            services.AddScoped<IUserService>(c => new UserService(connection, apiURL));
            services.AddScoped<IDeviceService>(c => new DeviceService(connection));
            services.AddScoped<IGeoStructureService>(c => new GeoStructureService(connection));
            services.AddScoped<IRoleService>(c => new RoleService(connection));
            services.AddScoped<IRegionService>(c => new RegionService(connection));
            services.AddScoped<IProvinceService>(c => new ProvinceService(connection));
            services.AddScoped<IDistrictService>(c => new DistrictService(connection));
            services.AddScoped<ILLGService>(c => new LLGService(connection));
            services.AddScoped<IWardService>(c => new WardService(connection));
            services.AddScoped<ILocationService>(c => new LocationService(connection));
            services.AddScoped<IVoterService>(c => new VoterService(connection, apiURL));
            services.AddScoped<IFileService>(c => new FileService(connection, apiURL));
            services.AddScoped<IGeneralService>(c => new GeneralService(connection));
            services.AddScoped<IMKAGeneralService>(c => new MKAGeneralService(connection));
            services.AddScoped<IEnrolmentFormService>(c => new EnrolmentFormService(connection, apiURL));
            services.AddScoped<IDataMigrationService>(c => new DataMigrationService(connection, apiURL));
            services.AddScoped<IScheduleLocSummaryService>(c => new ScheduleLocSummaryService(connection));
            services.AddScoped<IAugmentedRollStatService>(c => new AugmentedRollStatService(connection));
            services.AddScoped<IKupianoWardRollService>(c => new KupianoWardRollService(connection, apiURL));
            services.AddScoped<IDocumentService>(c => new DocumentService(connection, apiURL));
            services.AddScoped<IReportsService>(c => new ReportsService(connection));
            services.AddScoped<IOccupationService>(c => new OccupationService(connection));
            services.AddScoped<ISettingService>(c => new SettingService(connection));
            services.AddScoped<IMKAGeoStructureService>(c => new MKAGeoStructureService(connection));
            services.AddScoped<IMKADistrictService>(c => new MKADistrictService(connection));
            services.AddScoped<IMKALLGService>(c => new MKALLGService(connection));
            services.AddScoped<IMKAWardService>(c => new MKAWardService(connection));
            services.AddScoped<IMKALocationService>(c => new MKALocationService(connection));
            services.AddScoped<IMKAClanService>(c => new MKAClanService(connection));
            services.AddScoped<IClanService>(c => new ClanService(connection));
            services.AddScoped<IWritService>(c => new WritService(connection));
            services.AddScoped<IWritROService>(c => new WritROService(connection));
            services.AddScoped<IActivityLogService>(c => new ActivityLogService(connection));
            services.AddScoped<IProductionToTrainingService>(c => new ProductionToTrainingService(connection));
        }

        /// <summary>
        /// Customs the static files settings.
        /// </summary>
        /// <param name="application">The application.</param>
        public static void CustomStaticFilesSettings(this IApplicationBuilder application)
        {
            application.UseStaticFiles();

            if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\uploads\\"))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\uploads\\");
            }

            if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\uploads\\user\\"))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\uploads\\user\\");
            }

            if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\uploads\\elector_profile\\"))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\uploads\\elector_profile\\");
            }

            if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\uploads\\elector_form\\"))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\uploads\\elector_form\\");
            }

            if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\uploads\\enrolment\\"))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\uploads\\enrolment\\");
            }

            if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\uploads\\device_operator\\"))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\uploads\\device_operator\\");
            }

            if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\uploads\\province\\"))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\uploads\\province\\");
            }

            if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\uploads\\mobile_download\\"))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\uploads\\mobile_download\\");
            }

            if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\uploads\\geo\\"))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\uploads\\geo\\");
            }

            application.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "uploads")),
                RequestPath = "/uploads"
            });
        }
    }
}
