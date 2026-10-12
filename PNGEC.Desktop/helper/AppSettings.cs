using System;
using System.Configuration;

namespace PNGEC.Desktop.Helper
{
    public class AppSettings
    {
        public static string MapKey { get { return Get<string>("mapKey"); } }

        public static string DefaultProvisionalCutOffDOB { 
            get { return Get<string>("DefaultProvisionalCutOffDOB"); }            
        }

        private static T Get<T>(string key)
        {
            //// try to convert
            try
            {
                return (T)Convert.ChangeType(ConfigurationManager.AppSettings[key], typeof(T));
            }
            catch { }

            //// return default
            return default(T);
        }
    }
}
