using PNGEC.Desktop.Forms;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PNGEC.Desktop.Model
{
    public class GlobalsModel
    {
        /// <summary>
        /// The logged user
        /// </summary>
        private static LoggedUserModel loggedUser = new LoggedUserModel();
        /// <summary>
        /// Gets or sets the logged user.
        /// </summary>
        /// <value>
        /// The logged user.
        /// </value>
        public static LoggedUserModel LoggedUser
        {
            get
            {
                // Reads are usually simple
                return loggedUser;
            }
            set
            {
                // You can add logic here for race conditions,
                // or other measurements
                loggedUser = value;
            }
        }

        /// <summary>
        /// The tree view structure
        /// </summary>
        private static GeoTreeStructure treeViewStructure = new GeoTreeStructure();
        /// <summary>
        /// Gets or sets the TreeView structure.
        /// </summary>
        /// <value>
        /// The TreeView structure.
        /// </value>
        public static GeoTreeStructure TreeViewStructure
        {
            get
            {
                // Reads are usually simple
                return treeViewStructure;
            }
            set
            {
                // You can add logic here for race conditions,
                // or other measurements
                treeViewStructure = value;
            }
        }

        /// <summary>
        /// The MKA tree view structure
        /// </summary>
        private static MKAGeoTreeStructure mkaTreeViewStructure = new MKAGeoTreeStructure();
        /// <summary>
        /// Gets or sets the MKA TreeView structure.
        /// </summary>
        /// <value>
        /// The MKA TreeView structure.
        /// </value>
        public static MKAGeoTreeStructure MKATreeViewStructure
        {
            get
            {
                // Reads are usually simple
                return mkaTreeViewStructure;
            }
            set
            {
                // You can add logic here for race conditions,
                // or other measurements
                mkaTreeViewStructure = value;
            }
        }

        /// <summary>
        /// The main master
        /// </summary>
        public static MainMaster mainMaster;

        /// <summary>
        /// The download provincial
        /// </summary>
        public static DownloadProvincialFile downloadProvincial;

        /// <summary>
        /// Gets or sets the name of the report.
        /// </summary>
        /// <value>
        /// The name of the report.
        /// </value>
        public static string reportName { get; set; }

        /// <summary>
        /// Gets or sets the download province list.
        /// </summary>
        /// <value>
        /// The download province list.
        /// </value>
        public static List<DownloadProvince> DownloadProvinceList { get; set; }

        /// <summary>
        /// The transfer voter
        /// </summary>
        public static TransferVoter transferVoter;

        /// <summary>
        /// The location details
        /// </summary>
        private static LocationDetailsModel locationDetails = new LocationDetailsModel();
        /// <summary>
        /// Gets or sets the location details.
        /// </summary>
        /// <value>
        /// The location details.
        /// </value>
        public static LocationDetailsModel LocationDetails
        {
            get
            {
                // Reads are usually simple
                return locationDetails;
            }
            set
            {
                // You can add logic here for race conditions,
                // or other measurements
                locationDetails = value;
            }
        }

        private static TreeNode selectedNode;

        public static TreeNode SelectedNode
        {
            get { return selectedNode; }
            set { selectedNode = value; }
        }

    }
}
