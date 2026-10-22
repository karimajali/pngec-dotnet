using PNGEC.API.Domain.GeoStructure;
using PNGEC.API.Domain.MKAGeoStructure;
using PNGEC.API.Helper.Data;
using System.Data;
using System.Security.Policy;
using System.Web.UI.WebControls;

namespace PNGEC.API.Helper
{
    public class Globals
    {
        public const string DesktopVersionYear = "2025";
        public const string ReportServerVersionYear = "2025";
        public const string saltKey = "hbpngec2022dcpasswordencrypt";
        public const string AdminSecurityKey = "PNGEC_Admin_Internal_Authorized_Key_2026!";

        /// <summary>
        /// Gets or sets the geo list.
        /// </summary>
        /// <value>
        /// The geo list.
        /// </value>
        public static GeoListModel GeoList { get; set; }
        /// <summary>
        /// Gets or sets the mka geo list.
        /// </summary>
        /// <value>
        /// The geo list.
        /// </value>
        public static MKAGeoListModel MKAGeoList { get; set; }

        /// <summary>
        /// Gets the data service manager.
        /// </summary>
        /// <value>
        /// The data service manager.
        /// </value>
        public static IDataService DataServiceManager { get; } = new DataService();

        /// <summary>
        /// The action by
        /// </summary>
        private static int _actionBy = 1;
        /// <summary>
        /// Gets or sets the action by.
        /// </summary>
        /// <value>
        /// The action by.
        /// </value>
        public static int ActionBy
        {
            get
            {
                // Reads are usually simple
                return _actionBy;
            }
            set
            {
                // You can add logic here for race conditions,
                // or other measurements
                _actionBy = value;
            }
        }

        /// <summary>
        /// The dt region
        /// </summary>
        private static DataTable _dtRegion = null;
        /// <summary>
        /// Gets or sets the dt region.
        /// </summary>
        /// <value>
        /// The dt region.
        /// </value>
        public static DataTable dtRegion
        {
            get
            {
                // Reads are usually simple
                return _dtRegion;
            }
            set
            {
                // You can add logic here for race conditions,
                // or other measurements
                _dtRegion = value;
            }
        }

        /// <summary>
        /// The dt province
        /// </summary>
        private static DataTable _dtProvince = null;
        /// <summary>
        /// Gets or sets the dt province.
        /// </summary>
        /// <value>
        /// The dt province.
        /// </value>
        public static DataTable dtProvince
        {
            get
            {
                // Reads are usually simple
                return _dtProvince;
            }
            set
            {
                // You can add logic here for race conditions,
                // or other measurements
                _dtProvince = value;
            }
        }

        /// <summary>
        /// The dt district
        /// </summary>
        private static DataTable _dtDistrict = null;
        /// <summary>
        /// Gets or sets the dt district.
        /// </summary>
        /// <value>
        /// The dt district.
        /// </value>
        public static DataTable dtDistrict
        {
            get
            {
                // Reads are usually simple
                return _dtDistrict;
            }
            set
            {
                // You can add logic here for race conditions,
                // or other measurements
                _dtDistrict = value;
            }
        }

        /// <summary>
        /// The dt LLG
        /// </summary>
        private static DataTable _dtLLG = null;
        /// <summary>
        /// Gets or sets the dt LLG.
        /// </summary>
        /// <value>
        /// The dt LLG.
        /// </value>
        public static DataTable dtLLG
        {
            get
            {
                // Reads are usually simple
                return _dtLLG;
            }
            set
            {
                // You can add logic here for race conditions,
                // or other measurements
                _dtLLG = value;
            }
        }

        /// <summary>
        /// The dt ward
        /// </summary>
        private static DataTable _dtWard = null;
        /// <summary>
        /// Gets or sets the dt ward.
        /// </summary>
        /// <value>
        /// The dt ward.
        /// </value>
        public static DataTable dtWard
        {
            get
            {
                // Reads are usually simple
                return _dtWard;
            }
            set
            {
                // You can add logic here for race conditions,
                // or other measurements
                _dtWard = value;
            }
        }

        /// <summary>
        /// The dt location
        /// </summary>
        private static DataTable _dtLocation = null;
        /// <summary>
        /// Gets or sets the dt location.
        /// </summary>
        /// <value>
        /// The dt location.
        /// </value>
        ///  
        public static DataTable dtLocation
        {
            get
            {
                // Reads are usually simple
                return _dtLocation;
            }
            set
            {
                // You can add logic here for race conditions,
                // or other measurements
                _dtLocation = value;
            }
        }
        private static DataTable _dtClan = null;
        /// <summary>
        /// Gets or sets the dt location.
        /// </summary>
        /// <value>
        /// The dt location.
        /// </value>
        ///  
        public static DataTable dtClan
        {
            get
            {
                // Reads are usually simple
                return _dtClan;
            }
            set
            {
                // You can add logic here for race conditions,
                // or other measurements
                _dtClan = value;
            }
        }

        /// <summary>
        /// The dt mka Districts
        /// </summary>
        private static DataTable _dtMKADistricts = null;
        /// <summary>
        /// Gets or sets the dt Districts.
        /// </summary>
        /// <value>
        /// The dt Districts.
        /// </value>
        ///
        public static DataTable dtMKADistricts
        {
            get
            {
                // Reads are usually simple
                return _dtMKADistricts;
            }
            set
            {
                // You can add logic here for race conditions,
                // or other measurements
                _dtMKADistricts = value;
            }
        }

        /// <summary>
        /// The dt mka LLGs
        /// </summary>
        private static DataTable _dtMKALLGs = null;
        /// <summary>
        /// Gets or sets the dt LLGs.
        /// </summary>
        /// <value>
        /// The dt LLGs.
        /// </value>
        ///
        public static DataTable dtMKALLGs
        {
            get
            {
                // Reads are usually simple
                return _dtMKALLGs;
            }
            set
            {
                // You can add logic here for race conditions,
                // or other measurements
                _dtMKALLGs = value;
            }
        }

        /// <summary>
        /// The dt mka Wards
        /// </summary>
        private static DataTable _dtMKAWards = null;
        /// <summary>
        /// Gets or sets the dt Wards.
        /// </summary>
        /// <value>
        /// The dt Wards.
        /// </value>
        ///
        public static DataTable dtMKAWards
        {
            get
            {
                // Reads are usually simple
                return _dtMKAWards;
            }
            set
            {
                // You can add logic here for race conditions,
                // or other measurements
                _dtMKAWards = value;
            }
        }

        /// <summary>
        /// The dt mka Locations
        /// </summary>
        private static DataTable _dtMKALocations = null;
        /// <summary>
        /// Gets or sets the dt Locations.
        /// </summary>
        /// <value>
        /// The dt Locations.
        /// </value>
        ///
        public static DataTable dtMKALocations
        {
            get
            {
                // Reads are usually simple
                return _dtMKALocations;
            }
            set
            {
                // You can add logic here for race conditions,
                // or other measurements
                _dtMKALocations = value;
            }
        }

        /// <summary>
        /// The string server url
        /// </summary>
        private static string _serverURL = null;
        /// <summary>
        /// Gets or sets the string server.
        /// </summary>
        /// <value>
        /// The string server.
        /// </value>
        public static string serverURL
        {
            get
            {
                // Reads are usually simple
                return _serverURL;
            }
            set
            {
                // You can add logic here for race conditions,
                // or other measurements
                _serverURL = value;
            }
        }      
    }
}
