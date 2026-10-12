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
using PNGEC.API.Domain.MKAGeoStructure;
using PNGEC.Desktop.Model;
using PNGEC.Desktop.Properties;
using Newtonsoft.Json;

namespace PNGEC.Desktop.Forms
{
    public partial class MKALocation : Form
    {
        
        #region Variable Declaration
        /// <summary>
        /// The dt
        /// </summary>
        private DataTable dt = null;

        /// <summary>
        /// The model
        /// </summary>
        private MKALocationModel mkaLocationModel;

        /// <summary>
        /// The ReturnModel
        /// </summary>
        public MKALocationModel ReturnMKALocationModel { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="LLG"/> class.
        /// </summary>
        public MKALocation()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance with parameter of the <see cref="LLG"/> class.
        /// </summary>
        public MKALocation(MKALocationModel mkaLocationModel)
        {
            InitializeComponent();

            this.mkaLocationModel = mkaLocationModel;
        }
        #endregion

        #region Events

        /// <summary>
        /// Handles the Load event of the MKA Location control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>        
        private void MKALocation_Load(object sender, EventArgs e)
        {
            this.BindStatus();
            this.BindMKADistrictName();
            if (this.mkaLocationModel != null && this.mkaLocationModel.LLGId > 0)
            {
                txtMKALocationName.Text = this.mkaLocationModel.LocationName;
                cmbMKADistrictName.SelectedValue = this.mkaLocationModel.DistrictId;
                cmbMKALLGName.SelectedValue = this.mkaLocationModel.LLGId;
                cmbMKAWardName.SelectedValue = this.mkaLocationModel.WardId;
                cmbMKADistrictName.Enabled = false;
                cmbMKALLGName.Enabled = false;
                cmbMKAWardName.Enabled = false;
                cmbStatus.SelectedItem = Convert.ToBoolean(this.mkaLocationModel.Status) ? CommonEnum.Status.Active : CommonEnum.Status.Inactive;
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
                    var response = GenericHttpClient.PostRequestCommon<MKALocationModel>("MKALocation/AddUpdateMKALocation", request);

                    if (response.Result != null)
                    {
                        if (response.Result.Status)
                        {
                            this.ReturnMKALocationModel = response.Result.Data;
                            this.DialogResult = DialogResult.OK;
                            MessageBox.Show(response.Result.Message, "MKA PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            this.DialogResult = DialogResult.None;
                            MessageBox.Show(response.Result.Message, "MKA PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    ErrorLog.Write("MKA Location", "btnSave_Click", ex);
                }

                GlobalsModel.mainMaster.HideLoader();
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cmbRegion control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>        
        private void cmbMKADistrictName_SelectedIndexChanged(object sender, EventArgs e)
        {
            dt = new DataTable();
            dt.Clear();
            dt.Columns.Add("LLGId");
            dt.Columns.Add("LLGName");

            if (cmbMKADistrictName.SelectedIndex > 0)
            {
                dt.Clear();
                int selectedValue = Convert.ToInt32(cmbMKADistrictName.SelectedValue);
                DataView dataView = Globals.dtMKALLGs.DefaultView;
                if (selectedValue > 0)
                {
                    dataView.RowFilter = "districtId = " + selectedValue + "";
                }
                dt = dataView.ToTable();
            }

            DataRow row = dt.NewRow();
            row[0] = 0;
            row[1] = "Select MKA LLG";
            dt.Rows.InsertAt(row, 0);

            cmbMKALLGName.DataSource = dt;
            cmbMKALLGName.ValueMember = "LLGId";
            cmbMKALLGName.DisplayMember = "LLGName";
        }
        /// <summary>
        /// Handles the SelectedIndexChanged event of the cmbProvince control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>        
        private void cmbMKALLGName_SelectedIndexChanged(object sender, EventArgs e)
        {
            dt = new DataTable();
            dt.Clear();
            dt.Columns.Add("WardId");
            dt.Columns.Add("WardName");

            if (cmbMKALLGName.SelectedIndex > 0)
            {
                dt.Clear();
                int selectedValue = Convert.ToInt32(cmbMKALLGName.SelectedValue);
                DataView dataView = Globals.dtMKAWards.DefaultView;
                if (selectedValue > 0)
                {
                    dataView.RowFilter = "llgId = " + selectedValue + "";
                }
                dt = dataView.ToTable();
            }

            DataRow row = dt.NewRow();
            row[0] = 0;
            row[1] = "Select MKA Ward";
            dt.Rows.InsertAt(row, 0);

            cmbMKAWardName.DataSource = dt;
            cmbMKAWardName.ValueMember = "WardId";
            cmbMKAWardName.DisplayMember = "WardName";
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
            MKALocationModel llg = new MKALocationModel
            {
                LocationId = this.mkaLocationModel != null ? this.mkaLocationModel.LocationId : 0,
                LocationName = txtMKALocationName.Text,
                WardId = Convert.ToInt32(cmbMKAWardName.SelectedValue),
                LLGId = Convert.ToInt32(cmbMKALLGName.SelectedValue),
                DistrictId = Convert.ToInt32(cmbMKADistrictName.SelectedValue),
                Status = cmbStatus.SelectedItem.ToString() == CommonEnum.Status.Active.ToString(),
                ActionBy = GlobalsModel.LoggedUser.User.UserId
            };
            return llg;
        }
        #endregion

        #region ClearForm
        /// <summary>
        /// Clears the form.
        /// </summary>
        private void clearData()
        {
            txtMKALocationName.Text = "";
            cmbMKADistrictName.SelectedIndex = 0;
            cmbMKALLGName.SelectedIndex = 0;
            cmbMKAWardName.SelectedIndex = 0;
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

        #region BindMKADistrictName
        /// <summary>
        /// Binds the name of the region.
        /// </summary>
        private void BindMKADistrictName()
        {
            dt = new DataTable();
            dt = Globals.dtMKADistricts.Copy();

            DataRow row = dt.NewRow();
            row[0] = 0;
            row[1] = "Select MKA District";
            dt.Rows.InsertAt(row, 0);

            cmbMKADistrictName.DataSource = dt;
            cmbMKADistrictName.ValueMember = "DistrictId";
            cmbMKADistrictName.DisplayMember = "DistrictName";
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
            if (string.IsNullOrWhiteSpace(txtMKALocationName.Text.Trim()))
            {
                MessageBox.Show("MKA Location Name can't be empty.", "MKA PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (Convert.ToInt32(cmbMKADistrictName.SelectedIndex) <= 0)
            {
                MessageBox.Show("Please select MKA District Name", "MKA PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (Convert.ToInt32(cmbMKALLGName.SelectedIndex) <= 0)
            {
                MessageBox.Show("Please select MKA LLG Name", "MKA PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (Convert.ToInt32(cmbMKAWardName.SelectedIndex) <= 0)
            {
                MessageBox.Show("Please select MKA Ward Name", "MKA PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            string url = string.Format("MKALocation/ListMKALocation/{0}", this.mkaLocationModel.LocationId);           
            var response = await GenericHttpClient.GetResponse(url);
            List<MKALocationModel> auditList = JsonConvert.DeserializeObject<List<MKALocationModel>>(response.Data.ToString());
            if (auditList.Count > 0)
            {
                BindAuditGrid(auditList);
            }
        }
        #endregion

        #region BindAuditGrid
        /// <summary>
        /// Binds the audit gird.
        /// </summary>
        /// <param name="model">The model.</param>
        private void BindAuditGrid(List<MKALocationModel> lst)
        {
            dgAudit.DataSource = lst;

            dgAudit.Columns["LocationId"].Visible = false;
            dgAudit.Columns["WardId"].Visible = false;
            dgAudit.Columns["LLGId"].Visible = false;
            dgAudit.Columns["DistrictId"].Visible = false;
            dgAudit.Columns["Status"].Visible = false;
            dgAudit.Columns["CreatedDate"].Visible = false;
            dgAudit.Columns["ResponseCode"].Visible = false;
            dgAudit.Columns["LastChangeDate"].Visible = false;
            dgAudit.Columns["ActionBy"].Visible = false;
            dgAudit.Columns["StatusName"].HeaderText = "Status";
            dgAudit.Columns["LLGName"].HeaderText = "MKA LLG Name";
            dgAudit.Columns["DistrictName"].HeaderText = "MKA District Name";
            dgAudit.Columns["ModifiedDate"].HeaderText = "Last Change Date";
            dgAudit.Columns["UpdateByUserName"].HeaderText = "Update By User Name";
        }

        #endregion

        #endregion

        
    }
}
