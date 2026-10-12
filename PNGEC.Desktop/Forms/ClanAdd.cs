using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using PNGEC.API.Domain.Clan;
using PNGEC.API.Helper;
using PNGEC.Desktop.Model;
using PNGEC.Desktop.Properties;

namespace PNGEC.Desktop.Forms
{
    public partial class ClanAdd : Form
    {
        #region Variable Declaration
        /// <summary>
        /// The dt
        /// </summary>
        private DataTable dt = null;

        /// <summary>
        /// The model
        /// </summary>
        private ClanModel clanModel;

        /// <summary>
        /// The ReturnModel
        /// </summary>
        public List<ClanModel> ReturnClanModel { get; set; }

        int ControlCount = 0;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Clan"/> class.
        /// </summary>
        public ClanAdd()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance with parameter of the <see cref="Clan"/> class.
        /// </summary>
        public ClanAdd(ClanModel clanModel)
        {
            InitializeComponent();

            this.clanModel = clanModel;
        }
        #endregion

        #region Events

        /// <summary>
        /// Handles the Load event of the Clan control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ClanAdd_Load(object sender, EventArgs e)
        {
            this.BindStatus();
            this.BindRegionName();

            if (this.clanModel != null && this.clanModel.ClanId > 0)
            {
                //txtClanName.Text = this.clanModel.ClanName;
                //txtDescription.Text = this.clanModel.Description;
                cmbRegionName.SelectedValue = this.clanModel.RegionId;
                cmbProvinceName.SelectedValue = this.clanModel.ProvinceId;
                cmbDistrictName.SelectedValue = this.clanModel.DistrictId;
                cmbLLGName.SelectedValue = this.clanModel.LLGId;
                cmbWardName.SelectedValue = this.clanModel.WardId;
                cmbRegionName.Enabled = false;
                cmbProvinceName.Enabled = false;
                cmbDistrictName.Enabled = false;
                cmbLLGName.Enabled = false;
                cmbWardName.Enabled = false;
                cmbStatus.SelectedItem = Convert.ToBoolean(this.clanModel.Status) ? CommonEnum.Status.Active : CommonEnum.Status.Inactive;
                LoadAuditTab();
            }
            else
            {
                tab.TabPages.Remove(tabAudit);
                #region setdata
                if (GlobalsModel.SelectedNode != null)
                {
                    var node = GlobalsModel.SelectedNode;
                    int wardID = 0;
                    int llgID = 0;
                    int districtID = 0;
                    int provinceID = 0;
                    int regionID = 0;
                    while (node.Parent != null)
                    {

                        if (((PNGEC.Desktop.Model.GeoTreeStructure)node.Tag).MainTagName == "Ward")
                        {
                           wardID = ((PNGEC.Desktop.Model.GeoTreeStructure)node.Tag).Id;
                        }
                        else if (((PNGEC.Desktop.Model.GeoTreeStructure)node.Tag).MainTagName == "LLG")
                        {
                            llgID = ((PNGEC.Desktop.Model.GeoTreeStructure)node.Tag).Id;
                        }
                        else if (((PNGEC.Desktop.Model.GeoTreeStructure)node.Tag).MainTagName == "District")
                        {
                            districtID = ((PNGEC.Desktop.Model.GeoTreeStructure)node.Tag).Id;
                        }
                        else if (((PNGEC.Desktop.Model.GeoTreeStructure)node.Tag).MainTagName == "Province")
                        {
                            provinceID = ((PNGEC.Desktop.Model.GeoTreeStructure)node.Tag).Id;
                        }
                        else if (((PNGEC.Desktop.Model.GeoTreeStructure)node.Tag).MainTagName == "Region")
                        {
                            regionID = ((PNGEC.Desktop.Model.GeoTreeStructure)node.Tag).Id;
                        }

                        node = node.Parent;
                    }

                    cmbRegionName.SelectedValue = regionID;
                    cmbProvinceName.SelectedValue = provinceID;
                    cmbDistrictName.SelectedValue = districtID;
                    cmbLLGName.SelectedValue = llgID;
                    cmbWardName.SelectedValue = wardID;

                }
                #endregion
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

                foreach (Control panle in pnlControls.Controls)
                {
                    if(string.IsNullOrEmpty(panle.Controls[1].Text.Trim()))
                        continue;
                    try
                    {
                        var request = CreateAddRequestParam(panle.Controls[1].Text, panle.Controls[0].Text);
                        var response = GenericHttpClient.PostRequestCommon<ClanModel>("Clan/AddUpdateClan", request);
                        if (response.Result != null)
                        {
                            if (response.Result.Status)
                            {
                                this.ReturnClanModel =  this.ReturnClanModel == null? new List<ClanModel>() : this.ReturnClanModel; 
                                this.ReturnClanModel.Add(response.Result.Data);
                                this.DialogResult = DialogResult.OK;
                                //MessageBox.Show(response.Result.Message, "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                this.DialogResult = DialogResult.None;
                                //MessageBox.Show(response.Result.Message, "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            this.DialogResult = DialogResult.None;
                           // MessageBox.Show("Something went wrong", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(Resources.ResourceManager.GetString("ErrorMessage"), "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ErrorLog.Write("Clan", "btnSave_Click", ex);
                    }

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
        /// Handles the SelectedIndexChanged event of the cmbLLG control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cmbLLGName_SelectedIndexChanged(object sender, EventArgs e)
        {
            dt = new DataTable();
            dt.Clear();
            dt.Columns.Add("WardId");
            dt.Columns.Add("WardName");
            dt.Columns.Add("WardNoWardName");

            if (cmbLLGName.SelectedIndex > 0)
            {
                dt.Clear();
                int selectedValue = Convert.ToInt32(cmbLLGName.SelectedValue);
                DataView dataView = Globals.dtWard.DefaultView;
                if (selectedValue > 0)
                {
                    dataView.RowFilter = "lLGId = " + selectedValue + "";
                }
                dt = dataView.ToTable();
            }

            DataRow row = dt.NewRow();
            row[0] = 0;
            row[1] = "Select Ward";
            row["WardNoWardName"] = "Select Ward";
            dt.Rows.InsertAt(row, 0);

            cmbWardName.DataSource = dt;
            cmbWardName.ValueMember = "WardId";
            cmbWardName.DisplayMember = "WardNoWardName";
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
        private object CreateAddRequestParam(string ClanName,string ClanDesc)
        {
            ClanModel region = new ClanModel
            {
                ClanId = this.clanModel != null ? this.clanModel.ClanId : 0,
                ClanName = ClanName,
                Description = ClanDesc,
                WardId = Convert.ToInt32(cmbWardName.SelectedValue),
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
            cmbRegionName.SelectedIndex = 0;
            cmbProvinceName.SelectedIndex = 0;
            cmbDistrictName.SelectedIndex = 0;
            cmbLLGName.SelectedIndex = 0;
            cmbWardName.SelectedIndex = 0;
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

            if (Convert.ToInt32(cmbWardName.SelectedIndex) <= 0)
            {
                MessageBox.Show("Please select Ward Name", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (pnlControls.Controls.Count <= 0)
            {
                MessageBox.Show("Clan Name can't be empty.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            string url = string.Format("Clan/ListClan/{0}", this.clanModel.ClanId);
            List<ClanModel> response = await GenericHttpClient.GetRequest<ClanModel>(url);
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
        private void BindAuditGrid(List<ClanModel> lst)
        {
            dgAudit.DataSource = lst;

            dgAudit.Columns["ClanId"].Visible = false;
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

        private void btnAddCtrl_Click(object sender, EventArgs e)
        {
            ControlCount = ControlCount + 1;

            Panel panel = new Panel();
            panel.Dock = DockStyle.Top;
            panel.Name = "Panel" + ControlCount.ToString();
            panel.Height = 30;

            TextBox txtClan =  new TextBox();

            txtClan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            txtClan.Dock = System.Windows.Forms.DockStyle.Left;
            txtClan.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));           
            txtClan.Name = "txtClanName" + ControlCount.ToString();
            txtClan.Width = 200;

            TextBox txtClanDesc = new TextBox();

            txtClanDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            txtClanDesc.Dock = System.Windows.Forms.DockStyle.Top;
            txtClanDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))); 
            txtClanDesc.Name = "txtDescription" + ControlCount.ToString();
           

            panel.Controls.Add(txtClan);
            panel.Controls.Add(txtClanDesc);
            txtClanDesc.BringToFront();
            pnlControls.Controls.Add(panel);
            panel.BringToFront();
            txtClan.Focus();

        }
    }
}

