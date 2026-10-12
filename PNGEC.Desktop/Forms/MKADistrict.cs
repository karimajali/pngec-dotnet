using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PNGEC.API.Helper;
using PNGEC.Desktop.Model;
using PNGEC.Desktop.Properties;
using PNGEC.API.Domain.MKAGeoStructure;
using Newtonsoft.Json;

namespace PNGEC.Desktop.Forms
{
    public partial class MKADistrict : Form
    {       
        #region Variable Declaration
        /// <summary>
        /// The model
        /// </summary>
        private MKADistrictModel mkaDistrictModel;

        /// <summary>
        /// The ReturnModel
        /// </summary>
        public MKADistrictModel ReturnMKADistrictModel { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="MKADistrict"/> class.
        /// </summary>
        public MKADistrict()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance with parameter of the <see cref="MKADistrict"/> class.
        /// </summary>
        public MKADistrict(MKADistrictModel mkaDistrictModel)
        {
            InitializeComponent();
            this.mkaDistrictModel = mkaDistrictModel;
        }

        #endregion

        #region Events
        /// <summary>
        /// Handles the Click event of the btnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (IsValidForm())
            {
                GlobalsModel.mainMaster.ShowLoader();

                try
                {
                    var request = CreateAddRequestParam();
                    var response = await GenericHttpClient.PostRequestCommon<MKADistrictModel>("MKADistrict/AddUpdateMKADistrict", request);

                    if (response != null)
                    {
                        if (response.Status)
                        {
                            this.ReturnMKADistrictModel = response.Data;
                            this.DialogResult = DialogResult.OK;
                            MessageBox.Show(response.Message, "MKA PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            this.DialogResult = DialogResult.None;
                            MessageBox.Show(response.Message, "MKA PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        this.DialogResult = DialogResult.None;
                        MessageBox.Show("Something went wrong", "MKA PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Resources.ResourceManager.GetString("ErrorMessage"), "MKA PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ErrorLog.Write("MKA District", "btnSave_Click", ex);
                }

                GlobalsModel.mainMaster.HideLoader();
            }
        }

        /// <summary>
        /// Handles the Load event of the MKADistrict control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>      
        private void MKADistrict_Load(object sender, EventArgs e)
        {
            this.BindStatus();
            if (this.mkaDistrictModel != null && Convert.ToInt32(this.mkaDistrictModel.DistrictId) > 0)
            {
                txtMKADistrictName.Text = this.mkaDistrictModel.DistrictName;
                cmbStatus.SelectedItem = Convert.ToBoolean(this.mkaDistrictModel.Status) ? CommonEnum.Status.Active : CommonEnum.Status.Inactive;

                LoadAuditTab();
            }
            else
            {
                tab.TabPages.Remove(tabAudit);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        #endregion

        #region Methods

        #region CreateAddRequestParam
        /// <summary>
        /// Creates the request model
        /// </summary>
        private object CreateAddRequestParam()
        {
            MKADistrictModel region = new MKADistrictModel
            {
                DistrictId = this.mkaDistrictModel != null ? this.mkaDistrictModel.DistrictId : 0,
                DistrictName = txtMKADistrictName.Text,               
                Status = cmbStatus.SelectedItem.ToString() == CommonEnum.Status.Active.ToString(),
                ActionBy = GlobalsModel.LoggedUser.User.UserId
            };
            return region;
        }
        #endregion

        #region ClearForm
        /// <summary>
        /// Clears the form.
        /// </summary>
        private void clearData()
        {
            txtMKADistrictName.Text = "";
            cmbStatus.SelectedIndex = 0;
        }
        #endregion

        #region IsValidForm
        /// <summary>
        /// Determines whether [is valid form].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is valid form]; otherwise, <c>false</c>.
        /// </returns>
        private Boolean IsValidForm()
        {
            if (string.IsNullOrWhiteSpace(txtMKADistrictName.Text.Trim()))
            {
                MessageBox.Show("MKA District Name can't be empty.", "MKA PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
        #endregion

        #region BindStatus
        /// <summary>
        /// Binds the status.
        /// </summary>
        private void BindStatus()
        {
            cmbStatus.DataSource = Enum.GetValues(typeof(CommonEnum.Status));
            cmbStatus.SelectedItem = CommonEnum.Status.Active;
        }
        #endregion

        #region BindAuditGrid
        /// <summary>
        /// Binds the audit gird.
        /// </summary>
        /// <param name="model">The model.</param>
        private void BindAuditGrid(List<MKADistrictModel> lstRegions)
        {
            dgAudit.DataSource = lstRegions;

            dgAudit.Columns["DistrictId"].Visible = false;
            dgAudit.Columns["Status"].Visible = false;
            dgAudit.Columns["CreatedDate"].Visible = false;
            dgAudit.Columns["ResponseCode"].Visible = false;
            dgAudit.Columns["LastChangeDate"].Visible = false;
            dgAudit.Columns["ActionBy"].Visible = false;
            dgAudit.Columns["StatusName"].HeaderText = "Status";
            dgAudit.Columns["DistrictName"].HeaderText = "District Name";
            dgAudit.Columns["ModifiedDate"].HeaderText = "Last Change Date";
            dgAudit.Columns["UpdateByUserName"].HeaderText = "Update By User Name";
        }
        #endregion

        #region LoadAuditTab
        /// <summary>
        /// Load Audit Tab.
        /// </summary>
        private async void LoadAuditTab()
        {
            string url = string.Format("MKADistrict/ListMKADistrict/{0}", this.mkaDistrictModel.DistrictId);           
            //List<MKADistrictModel> response = await GenericHttpClient.GetRequest<MKADistrictModel>(url);
            var response = await GenericHttpClient.GetResponse(url);
            List<MKADistrictModel> auditList = JsonConvert.DeserializeObject<List<MKADistrictModel>>(response.Data.ToString()); 
            
            //response2
            if (auditList.Count > 0)
            {
                BindAuditGrid(auditList);
            }
        }
        #endregion

        #endregion

        
    }
}
