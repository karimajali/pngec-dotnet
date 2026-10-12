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
    public partial class MKALLG : Form
    {

        #region Variable Declaration
        /// <summary>
        /// The dt
        /// </summary>
        private DataTable dt = null;

        /// <summary>
        /// The model
        /// </summary>
        private MKALLGModel mkaLLGModel;

        /// <summary>
        /// The ReturnModel
        /// </summary>
        public MKALLGModel ReturnMKALLGModel { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="MKALLG"/> class.
        /// </summary>
        public MKALLG()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance with parameter of the <see cref="MKALLG"/> class.
        /// </summary>
        public MKALLG(MKALLGModel mkaLLGModel)
        {
            InitializeComponent();

            this.mkaLLGModel = mkaLLGModel;
        }
        #endregion

        #region Events

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
                    var response = GenericHttpClient.PostRequestCommon<MKALLGModel>("MKALLG/AddUpdateMKALLG", request);

                    if (response.Result != null)
                    {
                        if (response.Result.Status)
                        {
                            this.ReturnMKALLGModel = response.Result.Data;
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
                    ErrorLog.Write("MKA LLG", "btnSave_Click", ex);
                }

                GlobalsModel.mainMaster.HideLoader();
            }
        }

        /// <summary>
        /// Handles the Load event of the MKALLG control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>       
        private void MKALLG_Load(object sender, EventArgs e)
        {
            BindStatus();
            BindMKADistrictName();
            if (this.mkaLLGModel != null && this.mkaLLGModel.LLGId > 0)
            {
                txtMKALLGName.Text = this.mkaLLGModel.LLGName;
                cmbMKADistrictName.SelectedValue = this.mkaLLGModel.DistrictId;
                cmbMKADistrictName.Enabled = false;
                cmbStatus.SelectedItem = Convert.ToBoolean(this.mkaLLGModel.Status) ? CommonEnum.Status.Active : CommonEnum.Status.Inactive;
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
        /// <param name = "sender" > The source of the event.</param>
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
            MKALLGModel region = new MKALLGModel
            {
                LLGId = this.mkaLLGModel != null ? this.mkaLLGModel.LLGId : 0,
                LLGName = txtMKALLGName.Text,
                DistrictId = Convert.ToInt32(cmbMKADistrictName.SelectedValue),
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
            txtMKALLGName.Text = "";
            cmbMKADistrictName.SelectedIndex = 0;
            cmbStatus.SelectedIndex = 1;
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
        /// Binds the name of the MKA Districts.
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
            if (string.IsNullOrWhiteSpace(txtMKALLGName.Text.Trim()))
            {
                MessageBox.Show("MKA LLG Name can't be empty", "MKA PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (Convert.ToInt32(cmbMKADistrictName.SelectedIndex) <= 0)
            {
                MessageBox.Show("Please select MKA District Name", "MKA PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            string url = string.Format("MKALLG/ListMKALLG/{0}", this.mkaLLGModel.LLGId);
            var response = await GenericHttpClient.GetResponse(url);
            List<MKALLGModel> auditList = JsonConvert.DeserializeObject<List<MKALLGModel>>(response.Data.ToString());
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
        private void BindAuditGrid(List<MKALLGModel> lstMKALLG)
        {
            dgAudit.DataSource = lstMKALLG;

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
