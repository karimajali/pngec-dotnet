using PNGEC.API.Domain.Writ;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Domain.User;
using PNGEC.API.Helper;
using PNGEC.Desktop.Model;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Web.UI;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using PNGEC.API.Domain.Clan;
using PNGEC.Desktop.Properties;
using System.Drawing;

namespace PNGEC.Desktop.Forms
{
    public partial class Writ : Form
    {
        #region Variable declaration
        /// <summary>
        /// The model
        /// </summary>
        private WritModel model;

        /// <summary>
        /// The Scope Type array --  "Provincial" = 1, "District" == 2, "LLG" == 3, "Ward" == 4
        /// </summary>
        private string[] ScopeArray = { "SCLECT SCOPE", "PROVINCIAL", "DISTRICT", "LLG", "WARD" };


        /// <summary>
        /// The ELECTION array --  "BY-ELECTION" == 1, "NATIONAL" == 2, "PROVINCIAL" == 3
        /// </summary>
        private string[] ElectionTypeArray = { "ALL", "BY-ELECTION", "NATIONAL", "PROVINCIAL" };

        /// <summary>
        /// tab writ can move next page
        /// </summary>
        private Boolean tabMove = true;

        /// <summary>
        /// for dyanamic controls 
        /// </summary>
        int ControlCount = 0;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Writ"/> class.
        /// </summary>
        public Writ()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new parameterized instance of the <see cref="Writ"/> class.
        /// </summary>
        public Writ(WritModel model)
        {
            InitializeComponent();
            this.model = model;
        }

        #endregion

        #region Events


        /// <summary>        
        /// The Scope array --  "Provincial" = 1, "District" == 2, "LLG" == 3, "Ward" == 4
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void CmbScop_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbRegion.Visible = false;
            lblRegion.Visible = false;
            cmbProvince.Visible = false;
            lblProvince.Visible = false;
            cmbDistrict.Visible = false;
            lblDistrict.Visible = false;
            cmbLLG.Visible = false;
            lblLLG.Visible = false;
            cmbWard.Visible = false;
            lblWard.Visible = false;

            if (cmbScop.SelectedIndex == 1) // Provincial
            {
                lblRegion.Visible = true;
                lblProvince.Visible = true;
                cmbRegion.Visible = true;
                cmbProvince.Visible = true;
            }
            else if (cmbScop.SelectedIndex == 2) //District
            {
                lblRegion.Visible = true;
                lblProvince.Visible = true;
                lblDistrict.Visible = true;
                cmbRegion.Visible = true;
                cmbProvince.Visible = true;
                cmbDistrict.Visible = true;
            }
            else if (cmbScop.SelectedIndex == 3) // LLG
            {
                lblRegion.Visible = true;
                lblProvince.Visible = true;
                lblDistrict.Visible = true;
                lblLLG.Visible = true;
                cmbRegion.Visible = true;
                cmbProvince.Visible = true;
                cmbDistrict.Visible = true;
                cmbLLG.Visible = true;
            }
            else if (cmbScop.SelectedIndex == 4) //Ward
            {
                lblRegion.Visible = true;
                lblProvince.Visible = true;
                lblDistrict.Visible = true;
                lblLLG.Visible = true;
                lblWard.Visible = true;
                cmbRegion.Visible = true;
                cmbProvince.Visible = true;
                cmbDistrict.Visible = true;
                cmbLLG.Visible = true;
                cmbWard.Visible = true;
            }
        }


        /// <summary>
        /// Election Type
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmbElectionType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the Load event of the Writ control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Writ_Load(object sender, EventArgs e)
        {
            this.BindElectionType();
            this.BindRegionName();
            this.BindScop();
            if (this.model != null && this.model.Id > 0)
            {
                this.BindData();

                if (model.IsLocked == true)
                {
                    tabStep1.Enabled = false;
                    tabStep2.Enabled = false;
                    if (cmbElectionType.SelectedIndex == 1)
                    {
                        tabStep3.Enabled = false;
                    }
                    tabStep4.Enabled = false;
                    btnSave.Visible = false;
                }
            }

            btnPrevious.Visible = false;
        }

        /// <summary>
        /// Move Previous Steps
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrevious_Click(object sender, EventArgs e)
        {
            btnNext.Text = "Next";

            if (tabWrit.SelectedIndex == 3) /// 
            {
                tabMove = false;
                tabWrit.SelectedIndex = 2;
                tabMove = true;                
            }
            else if (tabWrit.SelectedIndex == 2) /// 
            {
                tabMove = false;
                if (cmbElectionType.SelectedIndex == 1) /// if BY-ELECTION 
                {
                    tabWrit.SelectedIndex = 1;
                }
                else
                {
                    tabWrit.SelectedIndex = 0;
                    btnPrevious.Visible = false;
                }

                tabMove = true;
            }
            else if (tabWrit.SelectedIndex == 1) /// 
            {
                tabMove = false;
                tabWrit.SelectedIndex = 0;
                tabMove = true;
                btnPrevious.Visible = false;
            }
        }

        /// <summary>
        /// Move to Next step 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNext_Click(object sender, EventArgs e)
        {
            if (btnNext.Text == "Close")
            { 
                this.Close();   
            }

            if (tabWrit.SelectedIndex == 0)
            {
                if (IsValidStep1())
                {
                    tabMove = false;
                    if (cmbElectionType.SelectedIndex == 1) /// if BY-ELECTION move to next step
                    {
                        tabWrit.SelectedIndex = 1;
                        btnPrevious.Visible = true;
                    }
                    else  /// else move to third stpe
                    {

                        tabWrit.SelectedIndex = 2;
                        btnPrevious.Visible = true;
                        txtNameOfRO.Visible = false;
                        //load 22 province name with RO UI
                        if (pnlNameOfRO.Controls.Count <= 0)
                        {
                            // Globals.dtProvince.DefaultView.Sort = Globals.dtProvince.Columns[2].ColumnName + "," + Globals.dtProvince.Columns[1].ColumnName;

                            DataTable dataView = Globals.dtProvince.AsEnumerable().OrderBy(x => x.Field<string>("RegionId")).ThenBy(x => x.Field<string>("ProvinceName")).CopyToDataTable();

                            foreach (DataRow row in dataView.Rows)
                            {
                                ControlCount = ControlCount + 1;

                                Panel panel = new Panel();
                                panel.Dock = DockStyle.Top;
                                panel.Name = "Panel" + ControlCount.ToString();
                                panel.Height = 30;

                                Label lblProvince = new Label();

                                lblProvince.Dock = System.Windows.Forms.DockStyle.Left;
                                lblProvince.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                                lblProvince.Name = row[0].ToString();
                                lblProvince.Text = row[1].ToString();
                                lblProvince.Width = 200;

                                TextBox txtProvinceRO = new TextBox();

                                txtProvinceRO.Dock = System.Windows.Forms.DockStyle.Top;
                                txtProvinceRO.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                                txtProvinceRO.Name = row[0].ToString(); // provinceID
                                txtProvinceRO.Tag = "0"; //tag is WritROID


                                panel.Controls.Add(lblProvince);
                                panel.Controls.Add(txtProvinceRO);
                                txtProvinceRO.BringToFront();
                                pnlNameOfRO.Controls.Add(panel);
                                panel.BringToFront();
                            }

                        }
                    }
                    tabWrit.Invalidate();
                    tabMove = true;
                }
            }
            else if (tabWrit.SelectedIndex == 1) /// 
            {
                if (IsValidStep2())
                {
                    tabMove = false;
                    tabWrit.SelectedIndex = 2;
                    tabMove = true;
                }
            }
            else if (tabWrit.SelectedIndex == 2) ///
            {
                if (IsValidStep3())
                {
                    tabMove = false;
                    tabWrit.SelectedIndex = 3;
                    tabMove = true;
                    btnNext.Text = "Close";
                }
            }
        }

        /// <summary>
        /// Prevent moving sept using click 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabWrit_Selecting(object sender, TabControlCancelEventArgs e)
        {
            e.Cancel = tabMove;
        }

        /// <summary>
        /// Save Writ Record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (IsValidStep1() && IsValidStep3())
            {
                GlobalsModel.mainMaster.ShowLoader();

                try
                {
                    var request = CreateAddRequestParam();
                    var response = GenericHttpClient.PostRequestCommon<WritModel>("Writ/AddUpdateWrit", request);

                    // Save RO Name for the Writ record

                    if (response.Result != null)
                    {
                        if (response.Result.Status)
                        {
                            if (cmbElectionType.SelectedIndex == 1) /// if BY-ELECTION check RO NAME
                            {
                                WritRO writRO = new WritRO();
                                writRO.ElectionWritId = response.Result.Data.Id;
                                writRO.ProvinceId = Convert.ToInt32(cmbProvince.SelectedValue);
                                writRO.ROName = txtNameOfRO.Text;
                                writRO.WritROId = txtNameOfRO.Tag == null ? 0: Convert.ToInt32(txtNameOfRO.Tag.ToString());
                                var writROresponse = GenericHttpClient.PostRequestCommon<WritRO>("WritRO/AddUpdateWritRO", writRO as object);

                            }
                            else // else 22 province RO name save
                            {
                                foreach (System.Windows.Forms.Control txtControl in pnlNameOfRO.Controls)
                                {
                                    if (txtControl is Panel)
                                    {
                                        if (!string.IsNullOrWhiteSpace(txtControl.Controls[0].Text.Trim()))
                                        {
                                            WritRO writRO = new WritRO();
                                            writRO.ElectionWritId = response.Result.Data.Id;
                                            writRO.ProvinceId = int.Parse(txtControl.Controls[0].Name.ToString());
                                            writRO.ROName = txtControl.Controls[0].Text;
                                            writRO.WritROId = Convert.ToInt32(txtControl.Controls[0].Tag.ToString());
                                            var writROresponse = GenericHttpClient.PostRequestCommon<WritModel>("WritRO/AddUpdateWritRO", writRO);
                                        }
                                    }
                                }
                            }

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
                    ErrorLog.Write("Clan", "btnSave_Click", ex);
                }

                GlobalsModel.mainMaster.HideLoader();
            }
        }


        /// <summary>
        /// Handles the SelectedIndexChanged event of the cmbRegion control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cmbRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add("ProvinceId");
            dt.Columns.Add("ProvinceName");

            if (cmbRegion.SelectedIndex > 0)
            {
                dt.Clear();
                int selectedValue = Convert.ToInt32(cmbRegion.SelectedValue);
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

            cmbProvince.DataSource = dt;
            cmbProvince.ValueMember = "ProvinceId";
            cmbProvince.DisplayMember = "ProvinceName";
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cmbProvince control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cmbProvinceName_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add("DistrictId");
            dt.Columns.Add("DistrictName");

            if (cmbProvince.SelectedIndex > 0)
            {
                dt.Clear();
                int selectedValue = Convert.ToInt32(cmbProvince.SelectedValue);
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

            cmbDistrict.DataSource = dt;
            cmbDistrict.ValueMember = "DistrictId";
            cmbDistrict.DisplayMember = "DistrictName";
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cmbDistrict control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cmbDistrictName_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add("LLGId");
            dt.Columns.Add("LLGName");

            if (cmbDistrict.SelectedIndex > 0)
            {
                dt.Clear();
                int selectedValue = Convert.ToInt32(cmbDistrict.SelectedValue);
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

            cmbLLG.DataSource = dt;
            cmbLLG.ValueMember = "LLGId";
            cmbLLG.DisplayMember = "LLGName";
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cmbLLG control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cmbLLG_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add("WardId");
            dt.Columns.Add("WardName");
            dt.Columns.Add("WardNoWardName");

            if (cmbLLG.SelectedIndex > 0)
            {
                dt.Clear();
                int selectedValue = Convert.ToInt32(cmbLLG.SelectedValue);
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
            row["WardName"] = "Select Ward";
            dt.Rows.InsertAt(row, 0);

            cmbWard.DataSource = dt;
            cmbWard.ValueMember = "WardId";
            cmbWard.DisplayMember = "WardName";
        }


        #endregion

        #region Methods

        #region CreateAddRequestParam
        /// <summary>
        /// Creates the request model
        /// </summary>
        private object CreateAddRequestParam()
        {

            string ScopValue = cmbScop.SelectedIndex.ToString();

            WritModel Writ = new WritModel
            {
                Id = this.model != null ? this.model.Id : 0,
                RegionId = Convert.ToInt32(cmbRegion.SelectedValue),
                ProvinceId = Convert.ToInt32(cmbProvince.SelectedValue),
                DistrictId = Convert.ToInt32(cmbDistrict.SelectedValue),
                LLGId = Convert.ToInt32(cmbLLG.SelectedValue),
                WardId = Convert.ToInt32(cmbWard.SelectedValue),
                WritName = Convert.ToString(txtElectionName.Text),
                Type = Convert.ToString(cmbElectionType.SelectedIndex),
                Scop = ScopValue,
                NominationStartDate = dtpNomiStartDate.Value,
                NominationEndDate = dtpNomiEndDate.Value,
                FirstDayOfPolling = dtpFirstDayofPolling.Value,
                LastDayOfPolling = dtpLastDayOfPolling.Value,
                ReturnOfWritDate = dtpReturnofWritDate.Value,
                IssueDate = dtpIssueDate.Value,
                IssuerName = Convert.ToString(txtIssuerName.Text),
                IssuerTitle = Convert.ToString(txtIssuerTitle.Text),
                ModifiedBy = GlobalsModel.LoggedUser.User.UserId,
                CreatedBy = GlobalsModel.LoggedUser.User.UserId,
                IsLocked = chkLockWrit.Checked
            };
            return Writ;
        }
        #endregion

        #region ClearForm
        /// <summary>
        /// Clears the form.
        /// </summary>
        private void clearData()
        {

        }
        #endregion

        #region form Valication
        /// <summary>
        /// Valid first step
        /// </summary>
        /// <returns></returns>
        private Boolean IsValidStep1()
        {
            if (cmbElectionType.SelectedIndex == 0)
            {
                MessageBox.Show("Select Election Type.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbElectionType.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtElectionName.Text.Trim()))
            {
                MessageBox.Show("Election Name can't be empty.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtElectionName.Focus();
                return false;
            }

            if (dtpNomiStartDate.Value.ToShortDateString() == "01-01-1753")
            {
                MessageBox.Show("Please select Nomination Start Date.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpNomiStartDate.Focus();
                return false;
            }

            if (dtpNomiEndDate.Value.ToShortDateString() == "01-01-1753")
            {
                MessageBox.Show("Please select Nomination End Date.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpNomiEndDate.Focus();
                return false;
            }

            if (dtpFirstDayofPolling.Value.ToShortDateString() == "01-01-1753")
            {
                MessageBox.Show("First Day of Polling can't be empty.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpFirstDayofPolling.Focus();
                return false;
            }

            if (dtpLastDayOfPolling.Value.ToShortDateString() == "01-01-1753")
            {
                MessageBox.Show("Last Day of Polling can't be empty.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpLastDayOfPolling.Focus();
                return false;
            }

            if (dtpReturnofWritDate.Value.ToShortDateString() == "01-01-1753")
            {
                MessageBox.Show("Please select Return of Writ Date.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpReturnofWritDate.Focus();
                return false;
            }

            if (dtpIssueDate.Value.ToShortDateString() == "01-01-1753")
            {
                MessageBox.Show("Please select Issue Date.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpIssueDate.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtIssuerName.Text.Trim()))
            {
                MessageBox.Show("Issuer Name can't be empty.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }


            if (string.IsNullOrWhiteSpace(txtIssuerTitle.Text.Trim()))
            {
                MessageBox.Show("Issuer Title can't be empty.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }



            return true;
        }

        /// <summary>
        /// Valid step 2
        /// </summary>
        /// <returns></returns>
        private Boolean IsValidStep2()
        {

            if (cmbScop.SelectedIndex == 1) // Provincial
            {
                if (Convert.ToInt32(cmbRegion.SelectedIndex) <= 0)
                {
                    MessageBox.Show("Please select Region", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (Convert.ToInt32(cmbProvince.SelectedIndex) <= 0)
                {
                    MessageBox.Show("Please select Province", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            if (cmbScop.SelectedIndex == 2) //District
            {
                if (Convert.ToInt32(cmbDistrict.SelectedIndex) <= 0)
                {
                    MessageBox.Show("Please select District", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            if (cmbScop.SelectedIndex == 3) // LLG
            {

                if (Convert.ToInt32(cmbLLG.SelectedIndex) <= 0)
                {
                    MessageBox.Show("Please select LLG", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

            }
            if (cmbScop.SelectedIndex == 4) //Ward
            {
                if (Convert.ToInt32(cmbWard.SelectedIndex) <= 0)
                {
                    MessageBox.Show("Please select Ward", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }         

            return true;
        }

        /// <summary>
        /// valid step 3
        /// </summary>
        /// <returns></returns>
        private Boolean IsValidStep3()
        {
            if (cmbElectionType.SelectedIndex == 1) /// if BY-ELECTION check RO NAME
            {
                if (string.IsNullOrWhiteSpace(txtNameOfRO.Text.Trim()))
                {
                    MessageBox.Show("RO Name can't be empty.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            else  /// else check all 22 RO NAMES
            {
                foreach (System.Windows.Forms.Control txtControl in pnlNameOfRO.Controls)
                {
                    if (txtControl is Panel)
                    {
                        if (string.IsNullOrWhiteSpace(txtControl.Controls[0].Text.Trim()))
                        {
                            MessageBox.Show(txtControl.Controls[1].Text + " RO Name can't be empty.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtControl.Controls[0].Focus();
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// valid step 4
        /// </summary>
        /// <returns></returns>
        private Boolean IsValidStep4()
        {
            return true;
        }

        #endregion


        #region BindElectionType
        /// <summary>
        /// Binds the BindElectionType.
        /// </summary>
        private void BindElectionType()
        {
            this.cmbElectionType.DataSource = ElectionTypeArray;
            this.cmbElectionType.SelectedIndexChanged -= CmbElectionType_SelectedIndexChanged;
            this.cmbElectionType.SelectedIndexChanged += CmbElectionType_SelectedIndexChanged;
        }


        #endregion

        #region Bind Scop 
        /// <summary>
        /// Binds the Bind Scop Combobox.
        /// </summary>
        private void BindScop()
        {
            this.cmbScop.DataSource = ScopeArray;
            this.cmbScop.SelectedIndexChanged -= CmbScop_SelectedIndexChanged;
            this.cmbScop.SelectedIndexChanged += CmbScop_SelectedIndexChanged;
        }



        #endregion


        #region BindRegionName
        /// <summary>
        /// Binds the name of the region.
        /// </summary>
        private void BindRegionName()
        {
            DataTable dt = new DataTable();
            dt = Globals.dtRegion.Copy();

            DataRow row = dt.NewRow();
            row[0] = 0;
            row[1] = "Select Region";
            dt.Rows.InsertAt(row, 0);

            cmbRegion.DataSource = dt;
            cmbRegion.ValueMember = "RegionId";
            cmbRegion.DisplayMember = "RegionName";
        }


        private async void BindData()
        {

            PageList model = new PageList();
            model.ElectionWritId = this.model.Id;
            List<WritRO> lstWritRO = new List<WritRO>();
            var response = await GenericHttpClient.PostRequestCommon<List<WritRO>>("WritRO/GetWritROList", model);
            if (response.Status)
            {
                lstWritRO = response.Data;

                if (lstWritRO.Count == 1) // only one RO == "BY-ELECTION"?
                {
                    this.txtNameOfRO.Text = lstWritRO.FirstOrDefault().ROName;
                    this.txtNameOfRO.Tag = lstWritRO.FirstOrDefault().WritROId;
                }
                else // generate 22 Province controls for the RO NAME
                {
                    //Globals.dtProvince.DefaultView.Sort = Globals.dtProvince.Columns[3].ColumnName + "," + Globals.dtProvince.Columns[2].ColumnName;

                    //DataTable dataView = Globals.dtProvince.DefaultView.ToTable();

                    lstWritRO.Reverse();

                    foreach (WritRO row in lstWritRO)
                    {
                        ControlCount = ControlCount + 1;

                        Panel panel = new Panel();
                        panel.Dock = DockStyle.Top;
                        panel.Name = "Panel" + ControlCount.ToString();
                        panel.Height = 30;

                        Label lblProvince = new Label();

                        lblProvince.Dock = System.Windows.Forms.DockStyle.Left;
                        lblProvince.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        lblProvince.Name = row.WritROId.ToString();
                        var provinceNme = Globals.dtProvince.AsEnumerable().Where(r => Convert.ToInt32(r["ProvinceId"]) == row.ProvinceId).Select(col => col.Field<string>("ProvinceName"));
                        lblProvince.Text = provinceNme.FirstOrDefault();
                        lblProvince.Width = 200;

                        TextBox txtProvinceRO = new TextBox();

                        txtProvinceRO.Dock = System.Windows.Forms.DockStyle.Top;
                        txtProvinceRO.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        txtProvinceRO.Tag = row.WritROId.ToString();
                        txtProvinceRO.Name = row.ProvinceId.ToString();
                        txtProvinceRO.Text = row.ROName;


                        panel.Controls.Add(lblProvince);
                        panel.Controls.Add(txtProvinceRO);
                        if (this.model.IsLocked)
                        {
                            panel.Enabled = false;
                        }

                        txtProvinceRO.BringToFront();
                        pnlNameOfRO.Controls.Add(panel);
                        panel.BringToFront();
                    }
                }
            }

            // "BY-ELECTION" == 1, "NATIONAL" == 2, "PROVINCIAL" == 3
            this.cmbElectionType.SelectedIndex = this.model.Type.ToUpper() == "BY-ELECTION" ? 1 : this.model.Type.ToUpper() == "NATIONAL" ? 2 : 3;

            //"PROVINCIAL", "DISTRICT", "LLG", "WARD"
            if (!string.IsNullOrEmpty(this.model.Scop))
            {
                this.cmbScop.SelectedIndex = this.model.Scop.ToUpper() == "PROVINCIAL" ? 1 : this.model.Scop.ToUpper() == "DISTRICT" ? 2 : this.model.Scop.ToUpper() == "LLG" ? 3 : 4;
            }
            this.txtElectionName.Text = this.model.WritName;
            this.dtpNomiStartDate.Value = this.model.NominationStartDate;
            this.dtpNomiEndDate.Value = this.model.NominationEndDate;
            this.dtpFirstDayofPolling.Value = this.model.FirstDayOfPolling;

            this.dtpLastDayOfPolling.Value = this.model.LastDayOfPolling;
            this.dtpReturnofWritDate.Value = this.model.ReturnOfWritDate;
            this.dtpIssueDate.Value = this.model.IssueDate;
            this.txtIssuerName.Text = this.model.IssuerName;
            this.txtIssuerTitle.Text = this.model.IssuerTitle;


            this.cmbRegion.Text = this.model.RegionName;
            this.cmbProvince.Text = this.model.ProvinceName;
            this.cmbDistrict.Text = this.model.DistrictName;
            this.cmbLLG.Text = this.model.LLGName;
            this.cmbWard.Text = this.model.WardName;

            // this.txtNameOfRO.Text =  this.model.NameofRo
            // need RO Name List to populate

            this.chkLockWrit.Checked = this.model.IsLocked;


        }

        #endregion


        #endregion


    }
}

