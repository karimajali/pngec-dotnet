using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Newtonsoft.Json;
using PNGEC.API.Domain.User;
using PNGEC.API.Helper;
using PNGEC.Desktop.Model;
using PNGEC.Desktop.Properties;

namespace PNGEC.Desktop.Forms
{
    public partial class User : Form
    {
        #region variable declaration
        /// <summary>
        /// The dt
        /// </summary>
        private DataTable dt = null;

        /// <summary>
        /// The model
        /// </summary>
        private UserModel model;

        /// <summary>
        /// The ofd
        /// </summary>
        private OpenFileDialog ofd = null;
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        public User()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        /// <param name="user">The user.</param>
        public User(UserModel model)
        {
            InitializeComponent();
            this.model = model;
        }
        #endregion

        #region Events
        /// <summary>
        /// Handles the Load event of the User control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void User_Load(object sender, EventArgs e)
        {
            this.BindStatus();
            this.BindRole();
            this.BindRegionName();
            this.BindMKADistrictName();

            this.pnlRegionProvince.Visible = false;
            this.pnlDistrict.Visible = false;
            this.pnlLlgWard.Visible = false;
            this.pnlLocation.Visible = false;
            this.pnlPermission.Visible = false;
            this.pnlMKAAssign.Visible = false;

            if (this.model != null && this.model.UserId > 0)
            {
                BindControlData(model);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnUploadProfileImage control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnUploadProfileImage_Click(object sender, EventArgs e)
        {
            try
            {
                ofd = new OpenFileDialog();
                ofd.Multiselect = false;
                ofd.Title = "Upload Profile Image";
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.tif;";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtProfileImage.Text = ofd.SafeFileName;
                    pbProfileImage.Image = new Bitmap(ofd.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Resources.ResourceManager.GetString("ErrorMessage"), "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorLog.Write("User Form", "btnUploadProfileImage_Click", ex);
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cmbRole control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cmbRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                chkForm11.Enabled = true;
                chkMKAForm11.Enabled = true;

                this.lblProvinceName.Visible = true;
                this.cmbProvinceName.Visible = true;
                this.pnlRegionProvince.Visible = false;
                this.pnlDistrict.Visible = false;
                this.pnlLlgWard.Visible = false;
                this.pnlLocation.Visible = false;
                this.pnlPermission.Visible = false;
                this.pnlMKAAssign.Visible = false;
                this.cmbRegionName.Enabled = true;
                this.cmbProvinceName.Enabled = true;

                if (this.cmbRegionName.Items != null && this.cmbRegionName.Items.Count > 0)
                {
                    this.cmbRegionName.SelectedIndex = 0;
                }

                //if (cmbRole.Text == "Admin" || cmbRole.Text == "Enrolment" || cmbRole.Text == "Management")
                if (cmbRole.Text == "Enrolment" || cmbRole.Text == "Management")
                {
                    this.pnlPermission.Visible = true;
                    //pnlPermission.Location = new Point(0, 0);

                    if (cmbRole.Text == "Enrolment")
                    {
                        chkForm11.Enabled = false;
                        chkMKAForm11.Enabled = false;
                    }

                    if (cmbRole.Text == "Management")
                    {
                        pnlDistrict.Visible = false;
                        pnlLocation.Visible = false;
                    }
                }
                else if (cmbRole.Text == "DPO")
                {
                    this.pnlRegionProvince.Visible = true;
                    this.pnlDistrict.Visible = true;
                }
                else if (cmbRole.Text == "MKA")
                {
                    this.pnlMKAAssign.Visible = true;
                    this.pnlRegionProvince.Visible = true;
                    this.pnlDistrict.Visible = true;
                    this.pnlLlgWard.Visible = true;

                    this.cmbRegionName.SelectedIndex = 4;
                    this.cmbRegionName.Enabled = false;
                    this.cmbProvinceName.SelectedIndex = 4;
                    this.cmbProvinceName.Enabled = false;

                }
                else if (cmbRole.Text == "RO")
                {
                    this.pnlRegionProvince.Visible = true;
                    this.pnlDistrict.Visible = true;
                    this.pnlLlgWard.Visible = true;
                    this.pnlLocation.Visible = true;
                }
                else if (cmbRole.Text == "Guest")
                {
                    //
                }
                else if (cmbRole.Text == "EM")
                {
                    this.pnlRegionProvince.Visible = true;
                }
                else if (cmbRole.Text == "RD")
                {
                    this.pnlRegionProvince.Visible = true;
                    this.lblProvinceName.Visible = false;
                    this.cmbProvinceName.Visible = false;
                }

                chkFile.Checked = false;
                chkGeoStructure.Checked = false;
                chkDatabaseConnect.Checked = false;
                chkDatabaseConnect.Checked = false;
                chkRoleStat.Checked = false;

                if (cmbRole.Text == "Enrolment")
                {
                    chkForm11.Checked = true;
                    chkMKAForm11.Checked = true;
                }
                else
                {
                    chkForm11.Checked = false;
                    chkMKAForm11.Checked = false;
                }

                if (cmbRole.Text == "ManageClan")
                {
                    this.pnlRegionProvince.Visible = true;
                    chkManageClan.Checked = true;
                }

                chkUsers.Checked = false;
                chkDevices.Checked = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Resources.ResourceManager.GetString("ErrorMessage"), "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorLog.Write("User Form", "cmbRole_SelectedIndexChanged", ex);
            }
        }
        private void cmbMKADistrictName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(Resources.ResourceManager.GetString("ErrorMessage"), "MKA PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorLog.Write("User Form", "cmbMKADistrictName_SelectedIndexChanged", ex);
            }
        }


        private void cmbMKALLGName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
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

                // 
                //if (cmbRole.SelectedIndex == 1)
                //{
                //    this.pnlLlgWard.Visible = false;

                //    if (cmbRegionName.SelectedIndex == 4 && cmbProvinceName.SelectedIndex == 4)
                //    {
                //        this.pnlLlgWard.Visible = true;
                //    }
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(Resources.ResourceManager.GetString("ErrorMessage"), "MKA PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorLog.Write("User Form", "cmbMKALLGName_SelectedIndexChanged", ex);
            }
        }


        /// <summary>
        /// Handles the SelectedIndexChanged event of the cmbRegionName control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cmbRegionName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(Resources.ResourceManager.GetString("ErrorMessage"), "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorLog.Write("User Form", "cmbRegionName_SelectedIndexChanged", ex);
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cmbProvinceName control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cmbProvinceName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
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

                // 
                if (cmbRole.SelectedIndex == 1)
                {
                    this.pnlLlgWard.Visible = false;

                    if (cmbRegionName.SelectedIndex == 4 && cmbProvinceName.SelectedIndex == 4)
                    {
                        this.pnlLlgWard.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Resources.ResourceManager.GetString("ErrorMessage"), "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorLog.Write("User Form", "cmbProvinceName_SelectedIndexChanged", ex);
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cmbDistrictName control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cmbDistrictName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(Resources.ResourceManager.GetString("ErrorMessage"), "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorLog.Write("User Form", "cmbDistrictName_SelectedIndexChanged", ex);
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cmbLLGName control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cmbLLGName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(Resources.ResourceManager.GetString("ErrorMessage"), "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorLog.Write("User Form", "cmbLLGName_SelectedIndexChanged", ex);
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cmbWardName control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cmbWardName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dt = new DataTable();
                dt.Clear();
                dt.Columns.Add("LocationId");
                dt.Columns.Add("LocationName");

                if (cmbWardName.SelectedIndex > 0)
                {
                    dt.Clear();
                    int selectedValue = Convert.ToInt32(cmbWardName.SelectedValue);
                    DataView dataView = Globals.dtLocation.DefaultView;
                    if (selectedValue > 0)
                    {
                        dataView.RowFilter = "wardId = " + selectedValue + "";
                    }
                    dt = dataView.ToTable();
                }

                DataRow row = dt.NewRow();
                row[0] = 0;
                row[1] = "Select Location";
                dt.Rows.InsertAt(row, 0);

                cmbLocationName.DataSource = dt;
                cmbLocationName.ValueMember = "LocationId";
                cmbLocationName.DisplayMember = "LocationName";
            }
            catch (Exception ex)
            {
                MessageBox.Show(Resources.ResourceManager.GetString("ErrorMessage"), "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorLog.Write("User Form", "cmbLLGName_SelectedIndexChanged", ex);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnSubmit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (IsValidForm())
            {
                string processtext = string.Empty;
                if (!string.IsNullOrWhiteSpace(txtUserId.Text) && Convert.ToInt32(txtUserId.Text) > 0)
                    processtext = "update";
                else
                    processtext = "add";

                DialogResult dialogResult = MessageBox.Show("Are you sure you want to " + processtext + " the record?", "PNGEC", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    this.AddUpdateRecord();
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.ClearForm();
            this.DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// Handles the KeyPress event of the txtContactNumber control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        private void txtContactNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || (e.KeyChar == (char)Keys.Back)))
                e.Handled = true;
        }
        #endregion

        #region Methods

        #region BindStatus
        /// <summary>
        /// Binds the status.
        /// </summary>
        private void BindStatus()
        {
            try
            {
                cmbStatus.DataSource = Enum.GetValues(typeof(CommonEnum.Status));
                //cmbStatus.DataSource = new List<EnumerationMember>(EnumExtensionMethods.GetEnumerationMemberList(typeof(CommonEnum.Status)));
                //cmbStatus.ValueMember = "Value";
                //cmbStatus.DisplayMember = "Description";
                //cmbStatus.Refresh();
                cmbStatus.SelectedItem = CommonEnum.Status.Active;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Resources.ResourceManager.GetString("ErrorMessage"), "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorLog.Write("User Form", "BindStatus", ex);
            }
        }
        #endregion

        #region BindRole
        /// <summary>
        /// Binds the role.
        /// </summary>
        private void BindRole()
        {
            try
            {
                cmbRole.DataSource = Enum.GetValues(typeof(CommonEnum.Role));
            }
            catch (Exception ex)
            {
                MessageBox.Show(Resources.ResourceManager.GetString("ErrorMessage"), "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorLog.Write("User Form", "BindRole", ex);
            }

        }
        #endregion

        #region BindRegionName
        /// <summary>
        /// Binds the name of the region.
        /// </summary>
        private void BindRegionName()
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(Resources.ResourceManager.GetString("ErrorMessage"), "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorLog.Write("User Form", "BindRole", ex);
            }
        }
        #endregion

        #region BindMKADistrictName
        /// <summary>
        /// Binds the name of the region.
        /// </summary>
        private void BindMKADistrictName()
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(Resources.ResourceManager.GetString("ErrorMessage"), "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorLog.Write("User Form", "BindRole", ex);
            }
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
            if (string.IsNullOrWhiteSpace(txtFirstName.Text.Trim()))
            {
                MessageBox.Show("First Name can't be empty.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtLastName.Text.Trim()))
            {
                MessageBox.Show("Last Name can't be empty.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtEmailAddress.Text.Trim()))
            {
                MessageBox.Show("Email Address can't be empty.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
            {
                bool isEmail = Regex.IsMatch(txtEmailAddress.Text.Trim(), @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
                if (isEmail == false)
                {
                    MessageBox.Show("Please enter a valid email address.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text.Trim()))
            {
                MessageBox.Show("Password can't be empty.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtContactNumber.Text.Trim()))
            {
                MessageBox.Show("Contact Number can't be empty.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
            {
                bool isContact = Regex.IsMatch(txtContactNumber.Text.Trim(), @"^[0-9]{10}$", RegexOptions.IgnoreCase);
                if (isContact == false)
                {
                    MessageBox.Show("Please enter a valid contact number.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            if (cmbRole.Text == "DPO")
            {
                if (cmbRegionName.SelectedIndex == 4 && cmbProvinceName.SelectedIndex == 4)
                {
                    if (Convert.ToInt32(cmbRegionName.SelectedValue) <= 0)
                    {
                        MessageBox.Show("Please select region name", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    if (Convert.ToInt32(cmbProvinceName.SelectedValue) <= 0)
                    {
                        MessageBox.Show("Please select province name", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    if (Convert.ToInt32(cmbDistrictName.SelectedValue) <= 0)
                    {
                        MessageBox.Show("Please select district name", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    if (Convert.ToInt32(cmbLLGName.SelectedValue) <= 0)
                    {
                        MessageBox.Show("Please select llg name", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    if (Convert.ToInt32(cmbWardName.SelectedValue) <= 0)
                    {
                        MessageBox.Show("Please select ward name", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
                else
                {
                    if (Convert.ToInt32(cmbRegionName.SelectedValue) <= 0)
                    {
                        MessageBox.Show("Please select region name", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    if (Convert.ToInt32(cmbProvinceName.SelectedValue) <= 0)
                    {
                        MessageBox.Show("Please select province name", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    if (Convert.ToInt32(cmbDistrictName.SelectedValue) <= 0)
                    {
                        MessageBox.Show("Please select district name", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }

                #region
                //if (Convert.ToInt32(cmbDistrictName.SelectedValue) <= 0)
                //{
                //    MessageBox.Show("Please select district name", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return false;
                //}

                //if (Convert.ToInt32(cmbLLGName.SelectedValue) <= 0)
                //{
                //    MessageBox.Show("Please select LLG name", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return false;
                //}

                //if (Convert.ToInt32(cmbWardName.SelectedValue) <= 0)
                //{
                //    MessageBox.Show("Please select ward name", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return false;
                //}

                //if (Convert.ToInt32(cmbLocationName.SelectedValue) <= 0)
                //{
                //    MessageBox.Show("Please select location name", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return false;
                //}
                #endregion
            }
            else if (cmbRole.Text == "MKA")
            {
                //if (cmbRegionName.SelectedIndex == 4 && cmbProvinceName.SelectedIndex == 4)
                //{
                if (Convert.ToInt32(cmbRegionName.SelectedValue) <= 0)
                {
                    MessageBox.Show("Please select region name", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (Convert.ToInt32(cmbProvinceName.SelectedValue) <= 0)
                {
                    MessageBox.Show("Please select province name", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (Convert.ToInt32(cmbDistrictName.SelectedValue) <= 0)
                {
                    MessageBox.Show("Please select district name", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (Convert.ToInt32(cmbLLGName.SelectedValue) <= 0)
                {
                    MessageBox.Show("Please select llg name", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (Convert.ToInt32(cmbWardName.SelectedValue) <= 0)
                {
                    MessageBox.Show("Please select ward name", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                if (Convert.ToInt32(cmbMKADistrictName.SelectedValue) <= 0)
                {
                    MessageBox.Show("Please select MKA District name", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                if (Convert.ToInt32(cmbMKALLGName.SelectedValue) <= 0)
                {
                    MessageBox.Show("Please select MKA LLG name", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                if (Convert.ToInt32(cmbMKAWardName.SelectedValue) <= 0)
                {
                    MessageBox.Show("Please select MKA Ward name", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                //}
                //else
                //{
                //    if (Convert.ToInt32(cmbRegionName.SelectedValue) <= 0)
                //    {
                //        MessageBox.Show("Please select region name", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //        return false;
                //    }

                //    if (Convert.ToInt32(cmbProvinceName.SelectedValue) <= 0)
                //    {
                //        MessageBox.Show("Please select province name", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //        return false;
                //    }

                //    if (Convert.ToInt32(cmbDistrictName.SelectedValue) <= 0)
                //    {
                //        MessageBox.Show("Please select district name", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //        return false;
                //    }
                //}
            }
            else if (cmbRole.Text == "EM")
            {
                if (Convert.ToInt32(cmbRegionName.SelectedValue) <= 0)
                {
                    MessageBox.Show("Please select region name", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (Convert.ToInt32(cmbProvinceName.SelectedValue) <= 0)
                {
                    MessageBox.Show("Please select province name", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            else if (cmbRole.Text == "RD")
            {
                if (Convert.ToInt32(cmbRegionName.SelectedValue) <= 0)
                {
                    MessageBox.Show("Please select region name", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            else if (cmbRole.Text == "Admin" || cmbRole.Text == "Enrolment" || cmbRole.Text == "Management")
            {
                // ---
            }
            else if (cmbRole.Text == "RO")
            {
                if (Convert.ToInt32(cmbRegionName.SelectedValue) <= 0)
                {
                    MessageBox.Show("Please select region name", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (Convert.ToInt32(cmbProvinceName.SelectedValue) <= 0)
                {
                    MessageBox.Show("Please select province name", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            return true;
        }
        #endregion

        #region ClearForm
        /// <summary>
        /// Clears the form.
        /// </summary>
        private void ClearForm()
        {
            //this.BindStatus();
            //this.BindRole();
            //this.BindRegionName();

            txtUserId.Text = "0";
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtProfileImage.Text = string.Empty;
            txtOldProfileImageName.Text = string.Empty;

            if (pbProfileImage.Image != null)
            {
                pbProfileImage.Image.Dispose();
                pbProfileImage.Image = null;
            }

            txtEmailAddress.Text = string.Empty;
            txtContactNumber.Text = string.Empty;
            txtPassword.Text = string.Empty;
            cmbStatus.SelectedItem = CommonEnum.Status.Active;
            cmbRole.SelectedItem = CommonEnum.Role.SelectRole;

            this.pnlLlgWard.Visible = false;
            this.pnlRegionProvince.Visible = false;
            this.pnlPermission.Visible = false;

            //cmbRegionName.SelectedIndex = 0;
            //cmbProvinceName.SelectedIndex = 0;
            //cmbDistrictName.SelectedIndex = 0;
            //cmbLLGName.SelectedIndex = 0;
            //cmbWardName.SelectedIndex = 0;
            //cmbLocationName.SelectedIndex = 0;

            chkFile.Checked = false;
            chkGeoStructure.Checked = false;
            chkDatabaseConnect.Checked = false;
            chkDatabaseConnect.Checked = false;
            chkRoleStat.Checked = false;
            chkForm11.Checked = false;
            chkUsers.Checked = false;
            chkDevices.Checked = false;

            chkMKAFile.Checked = false;
            chkMKAGeoStructure.Checked = false;
            chkMKAForm11.Checked = false;
        }
        #endregion

        #region GetPermission
        /// <summary>
        /// Gets the permission.
        /// </summary>
        /// <returns></returns>
        private string GetPermission()
        {
            var permission = new List<int>();

            if (chkFile.Checked)
            {
                permission.Add(Convert.ToInt32(chkFile.Tag));
            }
            if (chkGeoStructure.Checked)
            {
                permission.Add(Convert.ToInt32(chkGeoStructure.Tag));
            }
            if (chkDatabaseConnect.Checked)
            {
                permission.Add(Convert.ToInt32(chkDatabaseConnect.Tag));
            }
            if (chkWardRollBook.Checked)
            {
                permission.Add(Convert.ToInt32(chkWardRollBook.Tag));
            }
            if (chkRoleStat.Checked)
            {
                permission.Add(Convert.ToInt32(chkRoleStat.Tag));
            }
            if (chkForm11.Checked)
            {
                permission.Add(Convert.ToInt32(chkForm11.Tag));
            }
            if (chkUsers.Checked)
            {
                permission.Add(Convert.ToInt32(chkUsers.Tag));
            }
            if (chkDevices.Checked)
            {
                permission.Add(Convert.ToInt32(chkDevices.Tag));
            }
            if (chkMKAFile.Checked)
            {
                permission.Add(Convert.ToInt32(chkMKAFile.Tag));
            }
            if (chkMKAGeoStructure.Checked)
            {
                permission.Add(Convert.ToInt32(chkMKAGeoStructure.Tag));
            }
            if (chkMKAForm11.Checked)
            {
                permission.Add(Convert.ToInt32(chkMKAForm11.Tag));
            }
            if (chkMKAWardRollBook.Checked)
            {
                permission.Add(Convert.ToInt32(chkMKAWardRollBook.Tag));
            }
            if (chkManageClan.Checked)
            {
                permission.Add(Convert.ToInt32(chkManageClan.Tag));
            }

            if (cmbRole.SelectedValue.ToString() == "ManageClan") 
            {
                return "1,9,13";
            }

            return string.Join(",", permission.Select(n => n.ToString()).ToArray());
        }
        #endregion

        #region BindControlData
        /// <summary>
        /// Binds the control data.
        /// </summary>
        /// <param name="user">The user.</param>
        private void BindControlData(UserModel user)
        {
            try
            {
                txtUserId.Text = user.UserId.ToString();
                txtFirstName.Text = user.FirstName;
                txtLastName.Text = user.LastName;

                #region profile image
                //System.Net.WebRequest request = System.Net.WebRequest.Create(user.ProfileImageUrl);
                //System.Net.WebResponse response = request.GetResponse();
                //System.IO.Stream responseStream = response.GetResponseStream();

                pbProfileImage.Image = GetImageFromUrl(user.ProfileImageUrl);
                txtProfileImage.Text = user.ProfileImageName;
                txtProfileImage.Enabled = false;
                txtOldProfileImageName.Text = user.ProfileImageName;
                #endregion

                txtEmailAddress.Text = user.EmailAddress;
                txtContactNumber.Text = user.ContactNumber;
                txtPassword.Text = user.Password;
                txtPassword.Enabled = false;

                cmbStatus.SelectedItem = Convert.ToBoolean(user.Status) ? CommonEnum.Status.Active : CommonEnum.Status.Inactive;

                #region Role
                if (Convert.ToInt32(user.Role) == 1)
                {
                    cmbRole.SelectedItem = CommonEnum.Role.DPO;
                }
                else if (Convert.ToInt32(user.Role) == 2)
                {
                    cmbRole.SelectedItem = CommonEnum.Role.EM;
                }
                else if (Convert.ToInt32(user.Role) == 3)
                {
                    cmbRole.SelectedItem = CommonEnum.Role.RD;
                }
                //else if (Convert.ToInt32(user.Role) == 4)
                //{
                //    cmbRole.SelectedItem = CommonEnum.Role.Enrolment;
                //}
                else if (Convert.ToInt32(user.Role) == 5)
                {
                    cmbRole.SelectedItem = CommonEnum.Role.Guest;
                }
                else if (Convert.ToInt32(user.Role) == 6)
                {
                    cmbRole.SelectedItem = CommonEnum.Role.Admin;
                }
                else if (Convert.ToInt32(user.Role) == 7)
                {
                    cmbRole.SelectedItem = CommonEnum.Role.RO;
                }
                else if (Convert.ToInt32(user.Role) == 8)
                {
                    cmbRole.SelectedItem = CommonEnum.Role.Management;
                }
                else if (Convert.ToInt32(user.Role) == 9)
                {
                    cmbRole.SelectedItem = CommonEnum.Role.MKA;
                }
                else if (Convert.ToInt32(user.Role) == 10)
                {
                    cmbRole.SelectedItem = CommonEnum.Role.ManageClan;
                }
                else
                {
                    cmbRole.SelectedItem = CommonEnum.Role.SelectRole;
                }
                #endregion

                cmbRegionName.SelectedValue = Convert.ToInt32(user.Region);
                cmbProvinceName.SelectedValue = Convert.ToInt32(user.Province);
                cmbDistrictName.SelectedValue = Convert.ToInt32(user.District);
                cmbLLGName.SelectedValue = Convert.ToInt32(user.LLG);
                cmbWardName.SelectedValue = Convert.ToInt32(user.Ward);
                cmbLocationName.SelectedValue = Convert.ToInt32(user.Location);

                //MKA User details
                cmbMKADistrictName.SelectedValue = Convert.ToInt32(user.MKADistrict);
                cmbMKALLGName.SelectedValue = Convert.ToInt32(user.MKALLG);
                cmbMKAWardName.SelectedValue = Convert.ToInt32(user.MKAWard);

                if (!string.IsNullOrWhiteSpace(user.Permission) && user.Permission.Length > 0)
                {
                    List<string> lstPermission = user.Permission.Split(',').ToList();
                    foreach (string tag in lstPermission)
                    {
                        if (tag == chkFile.Tag.ToString())
                        {
                            chkFile.Checked = true;
                        }
                        else if (tag == chkGeoStructure.Tag.ToString())
                        {
                            chkGeoStructure.Checked = true;
                        }
                        else if (tag == chkDatabaseConnect.Tag.ToString())
                        {
                            chkDatabaseConnect.Checked = true;
                        }
                        else if (tag == chkWardRollBook.Tag.ToString())
                        {
                            chkWardRollBook.Checked = true;
                        }
                        else if (tag == chkRoleStat.Tag.ToString())
                        {
                            chkRoleStat.Checked = true;
                        }
                        else if (tag == chkForm11.Tag.ToString())
                        {
                            chkForm11.Checked = true;
                        }
                        else if (tag == chkUsers.Tag.ToString())
                        {
                            chkUsers.Checked = true;
                        }
                        else if (tag == chkDevices.Tag.ToString())
                        {
                            chkDevices.Checked = true;
                        }
                        else if (tag == chkMKAFile.Tag.ToString())
                        {
                            chkMKAFile.Checked = true;
                        }
                        else if (tag == chkMKAGeoStructure.Tag.ToString())
                        {
                            chkMKAGeoStructure.Checked = true;
                        }
                        else if (tag == chkMKAWardRollBook.Tag.ToString())
                        {
                            chkMKAWardRollBook.Checked = true;
                        }
                        else if (tag == chkMKAForm11.Tag.ToString())
                        {
                            chkMKAForm11.Checked = true;
                        }
                        else if (tag == chkManageClan.Tag.ToString())
                        {
                            chkManageClan.Checked = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Resources.ResourceManager.GetString("ErrorMessage"), "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorLog.Write("User Form", "BindControlData", ex);
            }
        }
        #endregion

        #region AddUpdateRecord
        /// <summary>
        /// Adds the update record.
        /// </summary>
        private async void AddUpdateRecord()
        {
            try
            {
                model = new UserModel();

                if (!string.IsNullOrWhiteSpace(txtUserId.Text))
                {
                    model.UserId = Convert.ToInt32(txtUserId.Text);
                }
                else
                {
                    model.UserId = 0;
                    txtUserId.Text = "0";
                }

                model.FirstName = txtFirstName.Text;
                model.LastName = txtLastName.Text;
                #region
                //if (!string.IsNullOrWhiteSpace(ofd.FileName))
                //{
                //    byte[] bytes = System.IO.File.ReadAllBytes(ofd.FileName);
                //    if (bytes != null & bytes.Length > 0)
                //    {
                //        var stream = new MemoryStream(bytes);

                //        if (stream != null & stream.Length > 0)
                //        {
                //            model.ProfileImage = new FormFile(stream, 0, bytes.Length, "ProfileImage", ofd.SafeFileName);
                //        }
                //    }
                //}
                #endregion
                model.EmailAddress = txtEmailAddress.Text;
                model.ContactNumber = txtContactNumber.Text;
                model.Status = Convert.ToBoolean(cmbStatus.SelectedValue);
                model.Role = Convert.ToInt32(cmbRole.SelectedValue);
                model.Region = Convert.ToInt32(cmbRegionName.SelectedValue);
                model.Province = Convert.ToInt32(cmbProvinceName.SelectedValue);
                model.District = Convert.ToInt32(cmbDistrictName.SelectedValue);
                model.LLG = Convert.ToInt32(cmbLLGName.SelectedValue);
                model.Ward = Convert.ToInt32(cmbWardName.SelectedValue);
                model.Location = Convert.ToInt32(cmbLocationName.SelectedValue);
                model.Permission = GetPermission();
                //MKA
                //For EM and RD user if the region assigned is Southern and Province is NCD, assign MKA Data
                if(model.Role == 2 && model.Region == 4 && model.Province == 13)
                {
                    cmbMKADistrictName.SelectedValue = 1;
                }
                else if(model.Role == 3 && model.Region == 4)
                {
                    cmbMKADistrictName.SelectedValue = 1;
                }
                else
                    model.MKADistrict = Convert.ToInt32(cmbMKADistrictName.SelectedValue);
                model.MKALLG = Convert.ToInt32(cmbMKALLGName.SelectedValue);
                model.MKAWard = Convert.ToInt32(cmbMKAWardName.SelectedValue);
                //model.MKALocation = Convert.ToInt32(cmbMKALocationName.SelectedValue);

                if (model != null)
                {
                    Response responseData = null;

                    byte[] bytes = null;

                    if (ofd != null && !string.IsNullOrWhiteSpace(ofd.FileName))
                    {
                        bytes = System.IO.File.ReadAllBytes(ofd.FileName);
                    }

                    string permission = GetPermission();

                    MultipartFormDataContent formContent;

                    if (bytes != null && bytes.Length > 0)
                    {
                        formContent = new MultipartFormDataContent
                    {
                        {new StringContent(txtUserId.Text),"UserId"},
                        {new StringContent(txtFirstName.Text),"FirstName"},
                        {new StringContent(txtLastName.Text), "LastName"},
                        {new StreamContent(new MemoryStream(bytes)),"ProfileImage", ofd.SafeFileName},
                        {new StringContent(txtOldProfileImageName.Text),"ProfileImageName"},
                        {new StringContent(txtEmailAddress.Text),"EmailAddress"},
                        {new StringContent(txtContactNumber.Text),"ContactNumber"},
                        {new StringContent(txtPassword.Text),"Password"},
                        {new StringContent(Convert.ToBoolean(cmbStatus.SelectedValue).ToString()),"Status"},
                        {new StringContent(Convert.ToInt32(cmbRole.SelectedValue).ToString()),"Role"},
                        {new StringContent(Convert.ToInt32(cmbRegionName.SelectedValue).ToString()),"Region"},
                        {new StringContent(Convert.ToInt32(cmbProvinceName.SelectedValue).ToString()),"Province"},
                        {new StringContent(Convert.ToInt32(cmbDistrictName.SelectedValue).ToString()), "District"},
                        {new StringContent(Convert.ToInt32(cmbLLGName.SelectedValue).ToString()),"LLG"},
                        {new StringContent(Convert.ToInt32(cmbWardName.SelectedValue).ToString()),"Ward"},
                        {new StringContent(Convert.ToInt32(cmbLocationName.SelectedValue).ToString()),"Location"},
                        {new StringContent(Convert.ToInt32(cmbMKADistrictName.SelectedValue).ToString()), "MKADistrict"},
                        {new StringContent(Convert.ToInt32(cmbMKALLGName.SelectedValue).ToString()),"MKALLG"},
                        {new StringContent(Convert.ToInt32(cmbMKAWardName.SelectedValue).ToString()),"MKAWard"},
                        //{new StringContent(Convert.ToInt32(cmbMKALocationName.SelectedValue).ToString()),"Location"},
                        {new StringContent(permission), "Permission"},
                        {new StringContent(GlobalsModel.LoggedUser.User.UserId.ToString()), "ActionBy"},
                    };
                    }
                    else
                    {
                        formContent = new MultipartFormDataContent
                    {
                        {new StringContent(txtUserId.Text),"UserId"},
                        {new StringContent(txtFirstName.Text),"FirstName"},
                        {new StringContent(txtLastName.Text), "LastName"},
                        {new StringContent(txtEmailAddress.Text),"EmailAddress"},
                        {new StringContent(txtContactNumber.Text),"ContactNumber"},
                        {new StringContent(txtPassword.Text),"Password"},
                        {new StringContent(Convert.ToBoolean(cmbStatus.SelectedValue).ToString()),"Status"},
                        {new StringContent(Convert.ToInt32(cmbRole.SelectedValue).ToString()),"Role"},
                        {new StringContent(Convert.ToInt32(cmbRegionName.SelectedValue).ToString()),"Region"},
                        {new StringContent(Convert.ToInt32(cmbProvinceName.SelectedValue).ToString()),"Province"},
                        {new StringContent(Convert.ToInt32(cmbDistrictName.SelectedValue).ToString()), "District"},
                        {new StringContent(Convert.ToInt32(cmbLLGName.SelectedValue).ToString()),"LLG"},
                        {new StringContent(Convert.ToInt32(cmbWardName.SelectedValue).ToString()),"Ward"},
                        {new StringContent(Convert.ToInt32(cmbLocationName.SelectedValue).ToString()),"Location"},
                        {new StringContent(Convert.ToInt32(cmbMKADistrictName.SelectedValue).ToString()), "MKADistrict"},
                        {new StringContent(Convert.ToInt32(cmbMKALLGName.SelectedValue).ToString()),"MKALLG"},
                        {new StringContent(Convert.ToInt32(cmbMKAWardName.SelectedValue).ToString()),"MKAWard"},
                        {new StringContent(permission), "Permission"},
                        {new StringContent(GlobalsModel.LoggedUser.User.UserId.ToString()), "ActionBy"},
                    };
                    }

                    GlobalsModel.mainMaster.ShowLoader();

                    using (var client = new HttpClient())
                    {
                        //client.BaseAddress = new Uri(ConfigurationManager.AppSettings["apiURL"]);
                        client.BaseAddress = new Uri(Globals.serverURL);
                        using (HttpResponseMessage response = client.PostAsync("User/AddUpdateUser", formContent).Result)
                        {
                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                string responseBody = await response.Content.ReadAsStringAsync();
                                responseData = JsonConvert.DeserializeObject<Response>(responseBody);
                            }
                        }
                    }

                    GlobalsModel.mainMaster.HideLoader();
                    if (responseData.Data != null && responseData.Status)
                    {
                        MessageBox.Show(responseData.Message, "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        ////this.DialogResult = DialogResult.None;
                        MessageBox.Show(responseData.Message, "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                GlobalsModel.mainMaster.HideLoader();
                MessageBox.Show(Resources.ResourceManager.GetString("ErrorMessage"), "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorLog.Write("User Form", "AddUpdateRecord", ex);
            }
        }
        #endregion

        #region GetImageFromUrl
        /// <summary>
        /// Gets the image from URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        public static Image GetImageFromUrl(string url)
        {
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
                using (HttpWebResponse httpWebReponse = (HttpWebResponse)httpWebRequest.GetResponse())
                {
                    using (Stream stream = httpWebReponse.GetResponseStream())
                    {
                        return Image.FromStream(stream);
                    }
                }
            }
            catch
            {
                return Resources.noimage;
            }
        }

        #endregion

        #endregion
    }
}
