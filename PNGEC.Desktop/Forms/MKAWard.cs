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
    public partial class MKAWard : Form
    {
       
        #region Variable Declaration
        /// <summary>
        /// The dt
        /// </summary>
        private DataTable dt = null;

        /// <summary>
        /// The model
        /// </summary>
        private MKAWardModel mkaWardModel;

        /// <summary>
        /// The ReturnModel
        /// </summary>
        public MKAWardModel ReturnMKAWardModel { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="District"/> class.
        /// </summary>
        public MKAWard()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance with parameter of the <see cref="District"/> class.
        /// </summary>
        public MKAWard(MKAWardModel mkaWardModel)
        {
            InitializeComponent();
            this.mkaWardModel = mkaWardModel;
        }
        #endregion

        #region Events

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
                    var response = GenericHttpClient.PostRequestCommon<MKAWardModel>("MKAWard/AddUpdateMKAWard", request);

                    if (response.Result != null)
                    {
                        if (response.Result.Status)
                        {
                            this.ReturnMKAWardModel = response.Result.Data;
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
                    ErrorLog.Write("MKA Ward", "btnSave_Click", ex);
                }

                GlobalsModel.mainMaster.HideLoader();
            }
        }

        /// <summary>
        /// Handles the Load event of the District control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>       

        private void MKAWard_Load(object sender, EventArgs e)
        {
            this.BindStatus();
            this.BindMKADistrictName();
            if (this.mkaWardModel != null && this.mkaWardModel.DistrictId > 0)
            {
                txtMKAWardName.Text = this.mkaWardModel.WardName;
                txtMKAWardNumber.Text = Convert.ToString(this.mkaWardModel.WardNumber);
                cmbMKADistrictName.SelectedValue = this.mkaWardModel.DistrictId;
                cmbMKALLGName.SelectedValue = this.mkaWardModel.LLGId;
                cmbMKADistrictName.Enabled = false;
                cmbMKALLGName.Enabled = false;
                cmbStatus.SelectedItem = Convert.ToBoolean(this.mkaWardModel.Status) ? CommonEnum.Status.Active : CommonEnum.Status.Inactive;
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

        /// <summary>
        /// Handles the Key press event of txtMKAWardNumber control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtMKAWardNumber_KeyPress(object sender, KeyPressEventArgs e)
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
            MKAWardModel region = new MKAWardModel
            {
                WardId = this.mkaWardModel != null ? this.mkaWardModel.WardId : 0,               
                WardName = txtMKAWardName.Text,
                WardNumber = Convert.ToInt32(txtMKAWardNumber.Text),
                DistrictId = Convert.ToInt32(cmbMKADistrictName.SelectedValue),
                LLGId = Convert.ToInt32(cmbMKALLGName.SelectedValue),
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
            txtMKAWardNumber.Text = "";
            txtMKAWardName.Text = "";
            cmbMKADistrictName.SelectedIndex = 0;
            cmbMKALLGName.SelectedIndex = 0;
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
            if (string.IsNullOrWhiteSpace(txtMKAWardNumber.Text.Trim()))
            {
                MessageBox.Show("MKA Ward Number can't be empty.", "MKA PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtMKAWardName.Text.Trim()))
            {
                MessageBox.Show("MKA Ward Name can't be empty.", "MKA PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            return true;
        }

        #endregion

        #region LoadAuditTab
        /// <summary>
        /// Load Audit Tab.
        /// </summary>
        private async void LoadAuditTab()
        {
            string url = string.Format("MKAWard/ListMKAWard/{0}", this.mkaWardModel.WardId);
            var response = await GenericHttpClient.GetResponse(url);
            List<MKAWardModel> auditList = JsonConvert.DeserializeObject<List<MKAWardModel>>(response.Data.ToString());
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
        private void BindAuditGrid(List<MKAWardModel> lst)
        {
            dgAudit.DataSource = lst;

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
