using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using PNGEC.API.Domain.Ward;
using PNGEC.API.Helper;
using PNGEC.Desktop.Model;
using PNGEC.Desktop.Properties;

namespace PNGEC.Desktop.Forms
{
    public partial class Ward : Form
    {
        #region Variable Declaration
        /// <summary>
        /// The dt
        /// </summary>
        private DataTable dt = null;

        /// <summary>
        /// The model
        /// </summary>
        private WardModel wardModel;

        /// <summary>
        /// The ReturnWardModel
        /// </summary>
        public WardModel ReturnWardModel { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Ward"/> class.
        /// </summary>
        public Ward()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance with parameter of the <see cref="Ward"/> class.
        /// </summary>
        public Ward(WardModel wardModel)
        {
            InitializeComponent();

            this.wardModel = wardModel;
        }
        #endregion

        #region Events

        /// <summary>
        /// Handles the Load event of the Ward control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Ward_Load(object sender, EventArgs e)
        {
            this.BindStatus();
            this.BindRegionName();
            if (this.wardModel != null && this.wardModel.WardId > 0)
            {
                txtWardNumber.Text = Convert.ToString(this.wardModel.WardNumber);
                txtWardName.Text = this.wardModel.WardName;
                cmbRegionName.SelectedValue = this.wardModel.RegionId;
                cmbProvinceName.SelectedValue = this.wardModel.ProvinceId;
                cmbDistrictName.SelectedValue = this.wardModel.DistrictId;
                cmbLLGName.SelectedValue = this.wardModel.LLGId;
                cmbRegionName.Enabled = false;
                cmbProvinceName.Enabled = false;
                cmbDistrictName.Enabled = false;
                cmbLLGName.Enabled = false;
                cmbStatus.SelectedItem = Convert.ToBoolean(this.wardModel.Status) ? CommonEnum.Status.Active : CommonEnum.Status.Inactive;
                LoadAuditTab();
            }
            else
            {
                tab.TabPages.Remove(tabAudit);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (IsValidForm())
            {
                GlobalsModel.mainMaster.ShowLoader();

                try
                {
                    var request = CreateAddRequestParam();
                    var response = GenericHttpClient.PostRequestCommon<WardModel>("Ward/AddUpdateWard", request);

                    if (response.Result != null)
                    {
                        if (response.Result.Status)
                        {
                            this.ReturnWardModel = response.Result.Data;
                            this.ReturnWardModel.WardNoWardName = $"{this.ReturnWardModel.WardNumber}-{this.ReturnWardModel.WardName}";
                            this.DialogResult = DialogResult.OK;
                            MessageBox.Show(response.Result.Message, "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            this.DialogResult = DialogResult.None;
                            MessageBox.Show(response.Result.Message, "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    ErrorLog.Write("Ward", "btnSave_Click", ex);
                }

                GlobalsModel.mainMaster.HideLoader();
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cmbRegion control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cmbRegionName_SelectedIndexChanged(object sender, EventArgs e)
        {
            dt = new DataTable();
            dt.Clear();
            dt.Columns.Add("ProvinceId");
            dt.Columns.Add("ProvinceName");

            if (cmbRegionName.SelectedIndex > 0)
            {
                dt.Clear();
                int selectedValue = Convert.ToInt32(cmbRegionName.SelectedValue);
                DataView dataView = Globals.dtProvince.DefaultView;
                if (selectedValue > 0)
                {
                    dataView.RowFilter = "regionId = " + selectedValue + "";
                }
                dt = dataView.ToTable();
            }

            DataRow row = dt.NewRow();
            row[0] = 0;
            row[1] = "Select Province";
            dt.Rows.InsertAt(row, 0);

            cmbProvinceName.DataSource = dt;
            cmbProvinceName.ValueMember = "ProvinceId";
            cmbProvinceName.DisplayMember = "ProvinceName";
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cmbProvince control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cmbProvinceName_SelectedIndexChanged(object sender, EventArgs e)
        {
            dt = new DataTable();
            dt.Clear();
            dt.Columns.Add("DistrictId");
            dt.Columns.Add("DistrictName");

            if (cmbProvinceName.SelectedIndex > 0)
            {
                dt.Clear();
                int selectedValue = Convert.ToInt32(cmbProvinceName.SelectedValue);
                DataView dataView = Globals.dtDistrict.DefaultView;
                if (selectedValue > 0)
                {
                    dataView.RowFilter = "provinceId = " + selectedValue + "";
                }
                dt = dataView.ToTable();
            }

            DataRow row = dt.NewRow();
            row[0] = 0;
            row[1] = "Select District";
            dt.Rows.InsertAt(row, 0);

            cmbDistrictName.DataSource = dt;
            cmbDistrictName.ValueMember = "DistrictId";
            cmbDistrictName.DisplayMember = "DistrictName";
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cmbDistrict control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cmbDistrictName_SelectedIndexChanged(object sender, EventArgs e)
        {
            dt = new DataTable();
            dt.Clear();
            dt.Columns.Add("LLGId");
            dt.Columns.Add("LLGName");

            if (cmbDistrictName.SelectedIndex > 0)
            {
                dt.Clear();
                int selectedValue = Convert.ToInt32(cmbDistrictName.SelectedValue);
                DataView dataView = Globals.dtLLG.DefaultView;
                if (selectedValue > 0)
                {
                    dataView.RowFilter = "districtId = " + selectedValue + "";
                }
                dt = dataView.ToTable();
            }

            DataRow row = dt.NewRow();
            row[0] = 0;
            row[1] = "Select LLG";
            dt.Rows.InsertAt(row, 0);

            cmbLLGName.DataSource = dt;
            cmbLLGName.ValueMember = "LLGId";
            cmbLLGName.DisplayMember = "LLGName";
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

        /// <summary>
        /// Handles the KeyPress event of the txtWardNumber control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void txtWardNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        #endregion

        #region Methods

        #region CreateAddRequestParam
        /// <summary>
        /// Creates the request model
        /// </summary>
        private object CreateAddRequestParam()
        {
            WardModel region = new WardModel
            {
                WardId = this.wardModel != null ? this.wardModel.WardId : 0,
                WardNumber = Convert.ToInt32(txtWardNumber.Text),
                WardName = txtWardName.Text,
                LLGId = Convert.ToInt32(cmbLLGName.SelectedValue),
                DistrictId = Convert.ToInt32(cmbDistrictName.SelectedValue),
                ProvinceId = Convert.ToInt32(cmbProvinceName.SelectedValue),
                RegionId = Convert.ToInt32(cmbRegionName.SelectedValue),
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
            txtWardNumber.Text = "";
            txtWardName.Text = "";
            cmbRegionName.SelectedIndex = 0;
            cmbProvinceName.SelectedIndex = 0;
            cmbDistrictName.SelectedIndex = 0;
            cmbLLGName.SelectedIndex = 0;
            cmbStatus.SelectedIndex = 0;
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

        #region BindRegionName
        /// <summary>
        /// Binds the name of the region.
        /// </summary>
        private void BindRegionName()
        {
            dt = new DataTable();
            dt = Globals.dtRegion.Copy();

            DataRow row = dt.NewRow();
            row[0] = 0;
            row[1] = "Select Region";
            dt.Rows.InsertAt(row, 0);

            cmbRegionName.DataSource = dt;
            cmbRegionName.ValueMember = "RegionId";
            cmbRegionName.DisplayMember = "RegionName";
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
            if (string.IsNullOrWhiteSpace(txtWardNumber.Text.Trim()))
            {
                MessageBox.Show("Ward Number can't be empty.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtWardName.Text.Trim()))
            {
                MessageBox.Show("Ward Name can't be empty.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (Convert.ToInt32(cmbRegionName.SelectedIndex) <= 0)
            {
                MessageBox.Show("Please select Region Name", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (Convert.ToInt32(cmbProvinceName.SelectedIndex) <= 0)
            {
                MessageBox.Show("Please select Province Name", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (Convert.ToInt32(cmbDistrictName.SelectedIndex) <= 0)
            {
                MessageBox.Show("Please select District Name", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (Convert.ToInt32(cmbLLGName.SelectedIndex) <= 0)
            {
                MessageBox.Show("Please select LLG Name", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        #endregion

        #region LoadAuditTab
        /// <summary>
        /// Load Audit Tab.
        /// </summary>
        private async void LoadAuditTab()
        {
            string url = string.Format("Ward/ListWard/{0}", this.wardModel.WardId);
            List<WardModel> response = await GenericHttpClient.GetRequest<WardModel>(url);
            if (response.Count > 0)
            {
                BindAuditGrid(response);
            }
        }
        #endregion

        #region BindAuditGrid
        /// <summary>
        /// Binds the audit gird.
        /// </summary>
        /// <param name="model">The model.</param>
        private void BindAuditGrid(List<WardModel> lst)
        {
            dgAudit.DataSource = lst;

            dgAudit.Columns["WardId"].Visible = false;
            dgAudit.Columns["LLGId"].Visible = false;
            dgAudit.Columns["DistrictId"].Visible = false;
            dgAudit.Columns["ProvinceId"].Visible = false;
            dgAudit.Columns["RegionId"].Visible = false;
            dgAudit.Columns["Status"].Visible = false;
            dgAudit.Columns["CreatedDate"].Visible = false;
            dgAudit.Columns["ResponseCode"].Visible = false;
            dgAudit.Columns["LastChangeDate"].Visible = false;
            dgAudit.Columns["ActionBy"].Visible = false;
            dgAudit.Columns["StatusName"].HeaderText = "Status";
            dgAudit.Columns["ProvinceName"].HeaderText = "Province Name";
            dgAudit.Columns["RegionName"].HeaderText = "Region Name";
            dgAudit.Columns["ModifiedDate"].HeaderText = "Last Change Date";
            dgAudit.Columns["UpdateByUserName"].HeaderText = "Update By User Name";
        }
        #endregion
        #endregion

    }
}

