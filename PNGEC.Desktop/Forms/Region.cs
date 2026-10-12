using System;
using System.Collections.Generic;
using System.Windows.Forms;
using PNGEC.API.Domain.Region;
using PNGEC.API.Helper;
using PNGEC.Desktop.Model;
using PNGEC.Desktop.Properties;

namespace PNGEC.Desktop.Forms
{
    public partial class Region : Form
    {
        #region Variable Declaration
        /// <summary>
        /// The model
        /// </summary>
        private RegionModel regionModel;

        /// <summary>
        /// The ReturnModel
        /// </summary>
        public RegionModel ReturnRegionModel { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Region"/> class.
        /// </summary>
        public Region()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance with parameter of the <see cref="Region"/> class.
        /// </summary>
        public Region(RegionModel regionModel)
        {
            InitializeComponent();
            this.regionModel = regionModel;
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
                    var response = await GenericHttpClient.PostRequestCommon<RegionModel>("Region/AddUpdateRegion", request);

                    if (response != null)
                    {
                        if (response.Status)
                        {
                            this.ReturnRegionModel = response.Data;
                            this.DialogResult = DialogResult.OK;
                            MessageBox.Show(response.Message, "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            this.DialogResult = DialogResult.None;
                            MessageBox.Show(response.Message, "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        this.DialogResult = DialogResult.None;
                        MessageBox.Show("Something went wrong", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Resources.ResourceManager.GetString("ErrorMessage"), "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ErrorLog.Write("Region", "btnSave_Click", ex);
                }

                GlobalsModel.mainMaster.HideLoader();
            }
        }

        /// <summary>
        /// Handles the Load event of the Region control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Region_Load(object sender, EventArgs e)
        {
            this.BindStatus();
            if (this.regionModel != null && Convert.ToInt32(this.regionModel.RegionId) > 0)
            {
                txtRegionName.Text = this.regionModel.RegionName;
                cmbStatus.SelectedItem = Convert.ToBoolean(this.regionModel.Status) ? CommonEnum.Status.Active : CommonEnum.Status.Inactive;

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
            RegionModel region = new RegionModel
            {
                RegionId = this.regionModel != null ? this.regionModel.RegionId : 0,
                RegionName = txtRegionName.Text,
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
            txtRegionName.Text = "";
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
            if (string.IsNullOrWhiteSpace(txtRegionName.Text.Trim()))
            {
                MessageBox.Show("Region Name can't be empty.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        private void BindAuditGrid(List<RegionModel> lstRegions)
        {
            dgAudit.DataSource = lstRegions;

            dgAudit.Columns["RegionId"].Visible = false;
            dgAudit.Columns["Status"].Visible = false;
            dgAudit.Columns["CreatedDate"].Visible = false;
            dgAudit.Columns["ResponseCode"].Visible = false;
            dgAudit.Columns["LastChangeDate"].Visible = false;
            dgAudit.Columns["ActionBy"].Visible = false;
            dgAudit.Columns["StatusName"].HeaderText = "Status";
            dgAudit.Columns["RegionName"].HeaderText = "Region Name";
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
            string url = string.Format("Region/ListRegion/{0}", this.regionModel.RegionId);
            List<RegionModel> response = await GenericHttpClient.GetRequest<RegionModel>(url);
            if (response.Count > 0)
            {
                BindAuditGrid(response);
            }
        }
        #endregion

        #endregion
    }
}

