using PNGEC.API.Domain.GeoStructure;
using PNGEC.API.Helper;
using PNGEC.Desktop.Model;
using System;
using System.Configuration;
using System.Net.Http;
using System.Windows.Forms;

namespace PNGEC.Desktop.Forms
{
    public partial class GeoStructure : Form
    {
        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="GeoStructure"/> class.
        /// </summary>
        public GeoStructure()
        {
            InitializeComponent();
        }
        #endregion

        #region Events

        /// <summary>
        /// Handles the Load event of the GeoStructure control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void GeoStructure_Load(object sender, EventArgs e)
        {
            cmbStatus.DataSource = Enum.GetValues(typeof(CommonEnum.Status));
        }

        /// <summary>
        /// Handles the Click event of the btnAdd control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var client = new HttpClient())
            {
                var request = CreateAddRequestParam();

                //client.BaseAddress = new Uri(ConfigurationManager.AppSettings["apiURL"]);
                client.BaseAddress = new Uri(Globals.serverURL);
                var response = client.PostAsJsonAsync("api/GeoStructure/AddUpdateGeoStructure", request).Result;
                if (response.IsSuccessStatusCode)
                {
                    lblResponse.Text = "Added successfully";
                    clearData();
                }
                else
                    lblResponse.Text = "Error..!!";
            }
        }

        #endregion

        #region Methods

        #region Create Add Request Param
        /// <summary>
        /// Creates the add request parameter.
        /// </summary>
        /// <returns></returns>
        private object CreateAddRequestParam()
        {
            GeoStructureModel geoStructure = new GeoStructureModel
            {
                GeoStructureName = txtGeoStructureName.Text,
                Sequence = txtSequence.Text,
                Status = cmbStatus.SelectedItem.ToString() == CommonEnum.Status.Active.ToString() ? true : false,
                ActionBy = GlobalsModel.LoggedUser.User.UserId
            };
            return geoStructure;
        }
        #endregion

        #region clear Data
        /// <summary>
        /// Clears the data.
        /// </summary>
        private void clearData()
        {
            txtGeoStructureName.Text = "";
            txtSequence.Text = "";
            cmbStatus.SelectedIndex = 0;
        }
        #endregion

        #endregion
    }
}
