using Microsoft.Extensions.Configuration;
using System.IO;

namespace PNGEC.API.Helper
{
    public class SettingsConfig
    {
        /// <summary>
        /// The application settings
        /// </summary>
        private static SettingsConfig _appSettings;

        /// <summary>
        /// Gets or sets the application setting value.
        /// </summary>
        /// <value>
        /// The application setting value.
        /// </value>
        public string appSettingValue { get; set; }

        /// <summary>
        /// Applications the setting.
        /// </summary>
        /// <param name="Key">The key.</param>
        /// <returns></returns>
        public static string AppSetting(string Key, string section = "")
        {
            _appSettings = GetCurrentSettings(Key, section);
            return _appSettings.appSettingValue;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsConfig"/> class.
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <param name="Key">The key.</param>
        public SettingsConfig(IConfiguration config, string Key)
        {
            this.appSettingValue = config.GetValue<string>(Key);
        }

        // Get a valued stored in the appsettings.
        // Pass in a key like TestArea:TestKey to get TestValue
        public static SettingsConfig GetCurrentSettings(string Key, string section)
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                            .AddEnvironmentVariables();

            IConfigurationRoot configuration = builder.Build();

            if (section == "base".ToLower())
            {
                return new SettingsConfig(configuration.GetSection("AppSettings"), Key);
            }
            else
            {
                return new SettingsConfig(configuration.GetSection("ApplicationSettings"), Key);
            }
        }
    }
}
