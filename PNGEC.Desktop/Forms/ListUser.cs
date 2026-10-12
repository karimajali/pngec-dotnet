using PNGEC.API.Domain.Listing;
using PNGEC.API.Domain.User;
using PNGEC.API.Helper;
using PNGEC.Desktop.Model;
using PNGEC.Desktop.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PNGEC.Desktop.Forms
{
    public partial class ListUser : Form
    {
        #region variable declaration
        /// <summary>
        /// The header CheckBox
        /// </summary>
        private CheckBox headerCheckBox = new CheckBox();

        /// <summary>
        /// The request model
        /// </summary>
        private CommonRequest requestModel = null;

        /// <summary>
        /// The geo tree structure
        /// </summary>
        private GeoTreeStructure TreeStructure = null;

        /// <summary>
        /// The geo tree structure
        /// </summary>
        private MKAGeoTreeStructure MKAGeoTreeStructure = null;

        private bool MKASelected = false;
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ListUser"/> class.
        /// </summary>
        public ListUser()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        /// <summary>
        /// Handles the Load event of the ListUser control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ListUser_Load(object sender, EventArgs e)
        {
            this.headerCheckBox.Click -= new EventHandler(HeaderCheckBox_Clicked);
            this.headerCheckBox.Click += new EventHandler(HeaderCheckBox_Clicked);

            this.dgUsers.CellContentClick -= new DataGridViewCellEventHandler(dgUsers_CellClick);
            this.dgUsers.CellContentClick += new DataGridViewCellEventHandler(dgUsers_CellClick);

            GlobalsModel.mainMaster.TreeViewSelectedChanged -= TreeViewSelectedChanged_AfterSelect;
            GlobalsModel.mainMaster.TreeViewSelectedChanged += TreeViewSelectedChanged_AfterSelect;

            TreeStructure = GlobalsModel.TreeViewStructure;
            MKAGeoTreeStructure = GlobalsModel.MKATreeViewStructure;
            this.BindRole();
            this.RefreshData();
        }

        /// <summary>
        /// Handles the Click event of the btnAddUser control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnAddUser_Click(object sender, EventArgs e)
        {
            User user = new User();
            user.AutoScroll = true;
            if (user.ShowDialog() == DialogResult.OK)
            {
                RefreshData();
            }
        }

        /// <summary>
        /// Handles the Click event of the btnRefresh control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.RefreshData();
            this.txtSearch.Text = string.Empty;
            this.headerCheckBox.Checked = false;
        }

        /// <summary>
        /// Handles the Click event of the btnActive control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnActive_Click(object sender, EventArgs e)
        {
            this.ChangeUserStatus(true);
        }

        /// <summary>
        /// Handles the Click event of the btnInactive control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnInactive_Click(object sender, EventArgs e)
        {
            this.ChangeUserStatus(false);
        }

        /// <summary>
        /// Handles the Click event of the btnSearch control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void btnSearch_Click(object sender, EventArgs e)
        {
            //PageList model = new PageList();
            //model.PageNo = 1;
            //model.PageSize = 100;
            //model.KeyWord = txtSearch.Text.Trim();
            //model.SortField = "UserId";
            //model.SortOrder = "DESC";

            //Response<UserModel> response = await GenericHttpClient.PostRequest<UserModel>("User/GetUserList", model);
            //if (response.Status)
            //{
            //    if (response.Data.ListItems.ToList() != null)
            //    {
            //        BindUserGird(response.Data.ListItems.ToList());
            //    }
            //}

            await GetUserDataAsync("Search");
        }

        /// <summary>
        /// Handles the Click event of the pcbSearchButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void pcbSearchButton_Click(object sender, EventArgs e)
        {
            PageList model = new PageList();
            model.PageNo = 1;
            model.PageSize = 100;
            model.KeyWord = txtSearch.Text.Trim();
            model.SortField = "UserId";
            model.SortOrder = "DESC";

            try
            {
                Response<UserModel> response = await GenericHttpClient.PostRequest<UserModel>("User/GetUserList", model);
                if (response.Status)
                {
                    if (response.Data.ListItems.ToList() != null)
                    {
                        BindUserGird(response.Data.ListItems.ToList());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Resources.ResourceManager.GetString("ErrorMessage"), "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorLog.Write("List User", "pcbSearchButton_Click", ex);
            }
        }

        /// <summary>
        /// Handles the Clicked event of the HeaderCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void HeaderCheckBox_Clicked(object sender, EventArgs e)
        {
            //Necessary to end the edit mode of the Cell.
            dgUsers.EndEdit();

            //Loop and check and uncheck all row CheckBoxes based on Header Cell CheckBox.
            foreach (DataGridViewRow row in dgUsers.Rows)
            {
                DataGridViewCheckBoxCell checkBox = (row.Cells["checkBoxColumn"] as DataGridViewCheckBoxCell);
                checkBox.Value = headerCheckBox.Checked;
            }
        }

        /// <summary>
        /// Handles the CellClick event of the dgUsers control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void dgUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Check to ensure that the row CheckBox is clicked.
            if (e.RowIndex >= 0 && dgUsers.Columns[e.ColumnIndex].Name == "checkBoxColumn" && e.ColumnIndex == 0)
            {
                DataGridViewCheckBoxCell crntCheckBoxCell = (DataGridViewCheckBoxCell)this.dgUsers.Rows[e.RowIndex].Cells["checkBoxColumn"];
                if (crntCheckBoxCell.Value == null)
                {
                    crntCheckBoxCell.Value = true;
                }
                else
                {
                    crntCheckBoxCell.Value = !(bool)crntCheckBoxCell.Value;
                }

                //Loop to verify whether all row CheckBoxes are checked or not.
                bool isChecked = true;
                foreach (DataGridViewRow row in dgUsers.Rows)
                {
                    if (Convert.ToBoolean(row.Cells["checkBoxColumn"].EditedFormattedValue) == false)
                    {
                        isChecked = false;
                        break;
                    }
                }
                headerCheckBox.Checked = isChecked;
            }
            else if (e.RowIndex >= 0 && dgUsers.Columns[e.ColumnIndex].Name == "Edit")
            {
                // Clear all checkbox
                this.ClearCheckbox();

                UserModel model = GetGridCellData(e.RowIndex);

                if (model != null)
                {
                    User user = new User(model);
                    if (user.ShowDialog() == DialogResult.OK)
                    {
                        RefreshData();
                    }
                }
            }
            else if (e.RowIndex >= 0 && dgUsers.Columns[e.ColumnIndex].Name == "ResetPassword")
            {
                // Clear all checkbox
                this.ClearCheckbox();

                UserModel model = GetGridCellData(e.RowIndex);

                if (model != null)
                {
                    ResetPassword resetPassword = new ResetPassword(model);
                    resetPassword.ShowDialog();
                }
            }
        }

        /// <summary>
        /// Handles the CellFormatting event of the dgUsers control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void dgUsers_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgUsers.Columns[e.ColumnIndex].Name == "ProfilePicture")
            {
                if (e.Value == null)
                {
                    string imagePath = this.dgUsers.Rows[e.RowIndex].Cells["ProfileImageUrl"].FormattedValue.ToString();

                    DataGridViewCell cell = this.dgUsers.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    cell.Value = GetImageFromUrl(imagePath);
                }

            }
            else if (dgUsers.Columns[e.ColumnIndex].Name == "StatusName")
            {
                foreach (DataGridViewRow row in dgUsers.Rows)
                {
                    if (row.Cells["StatusName"].Value.ToString() == "Active")
                    {
                        row.Cells["StatusName"].Style.ForeColor = Color.Green;
                    }
                    else if (row.Cells["StatusName"].Value.ToString() == "InActive")
                    {
                        row.Cells["StatusName"].Style.ForeColor = Color.Red;
                    }
                    else
                    {
                        row.Cells["StatusName"].Style.ForeColor = Color.Black;
                    }
                }
            }
        }

        /// <summary>
        /// Handles the CellPainting event of the dgUsers control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellPaintingEventArgs"/> instance containing the event data.</param>
        private void dgUsers_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex == 0)
            {
                ResetHeaderCheckBoxLocation(e.ColumnIndex, e.RowIndex);
            }
        }

        /// <summary>
        /// Handles the AfterSelect event of the TreeViewSelectedChanged control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        public async void TreeViewSelectedChanged_AfterSelect(object sender, EventArgs e)
        {           
            MainMaster m = (MainMaster)sender;
            string selectedNode = ((TreeView)m.ActiveControl).TopNode.Text;
            if (selectedNode == "MKA PNGEC")
            {
                this.MKAGeoTreeStructure = GlobalsModel.MKATreeViewStructure;
                this.MKASelected = true;
            }
            else
                this.TreeStructure = GlobalsModel.TreeViewStructure; 

            await GetUserDataAsync();
        }

        /// <summary>
        /// Handles the FormClosing event of the ListUser control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
        private void ListUser_FormClosing(object sender, FormClosingEventArgs e)
        {
            GlobalsModel.mainMaster.TreeViewSelectedChanged -= TreeViewSelectedChanged_AfterSelect;
        }
        #endregion

        #region Methods

        #region BindRole
        /// <summary>
        /// Binds the role.
        /// </summary>
        private void BindRole()
        {
            try
            {
                this.cmbRole.DataSource = EnumExtensionMethods.GetEnumerationMemberList(typeof(CommonEnum.Role));
                this.cmbRole.DisplayMember = "Description";
                this.cmbRole.ValueMember = "Value";

                this.cmbRole.SelectedItem = CommonEnum.Role.SelectRole;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Resources.ResourceManager.GetString("ErrorMessage"), "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorLog.Write("ListUser", "BindRole", ex);
            }
        }
        #endregion

        #region Refresh Data
        /// <summary>
        /// Refresh Data
        /// </summary>
        private async void RefreshData()
        {
            //PageList model = new PageList();
            //model.PageNo = 1;
            //model.PageSize = 100;
            //model.KeyWord = "";
            //model.SortField = "UserId";
            //model.SortOrder = "DESC";

            //Response<UserModel> response = await GenericHttpClient.PostRequest<UserModel>("User/GetUserList", model);
            //if (response.Status)
            //{
            //    if (response.Data.ListItems.ToList() != null)
            //    {
            //        BindUserGird(response.Data.ListItems.ToList());
            //    }
            //}

            await GetUserDataAsync("Refresh");
        }
        private async Task GetUserDataAsync(string eventName = "")
        {
            GlobalsModel.mainMaster.ShowLoader();

            try
            {
                PageList pageList = new PageList();
                //pageList.PageNo = 1;
                //pageList.PageSize = 1000;

                if (eventName == "Refresh")
                {
                    pageList.KeyWord = "";
                    this.cmbRole.SelectedValue = CommonEnum.Role.SelectRole;
                }
                else
                {
                    pageList.KeyWord = txtSearch.Text.Trim();
                }

                // TreeView Filed
                pageList.GeoName = MKASelected == true? this.MKAGeoTreeStructure.MainTagName:TreeStructure.MainTagName;
                pageList.GeoId = MKASelected == true ? this.MKAGeoTreeStructure.Id : TreeStructure.Id;

                pageList.SortField = "UserId";
                pageList.SortOrder = "DESC";

                pageList.RoleId = (CommonEnum.Role)this.cmbRole.SelectedValue;

                Response<UserModel> response = await GenericHttpClient.PostRequest<UserModel>("User/GetUserList", pageList);
                if (response.Status)
                {
                    if (response.Data.ListItems.ToList() != null)
                    {
                        BindUserGird(response.Data.ListItems.ToList());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Resources.ErrorMessage, "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorLog.Write("ListUser", "GetUserDataAsync", ex);
            }

            GlobalsModel.mainMaster.HideLoader();
        }
        #endregion

        #region Bind User Gird
        /// <summary>
        /// Binds the user gird.
        /// </summary>
        /// <param name="model">The model.</param>
        private void BindUserGird(List<UserModel> lstUsers)
        {
            try
            {
                if (GlobalsModel.LoggedUser != null && GlobalsModel.LoggedUser.User.UserId > 0)
                {
                    lstUsers = lstUsers.Where(x => x.UserId != GlobalsModel.LoggedUser.User.UserId).ToList();
                }
                if (this.dgUsers.Columns.Count > 0)
                {
                    this.dgUsers.Columns.Clear();
                }

                this.dgUsers.DataSource = null;
                this.dgUsers.DataSource = lstUsers;
                this.dgUsers.Columns["UserId"].Visible = false;
                this.dgUsers.Columns["FirstName"].Visible = false;
                this.dgUsers.Columns["LastName"].Visible = false;
                this.dgUsers.Columns["LastName"].Visible = false;
                this.dgUsers.Columns["ProfileImage"].Visible = false;
                this.dgUsers.Columns["ProfileImageUrl"].Visible = false;
                this.dgUsers.Columns["Status"].Visible = false;
                this.dgUsers.Columns["Role"].Visible = false;
                this.dgUsers.Columns["Region"].Visible = false;
                this.dgUsers.Columns["Province"].Visible = false;
                this.dgUsers.Columns["District"].Visible = false;
                this.dgUsers.Columns["LLG"].Visible = false;
                this.dgUsers.Columns["Ward"].Visible = false;
                this.dgUsers.Columns["Location"].Visible = false;
                this.dgUsers.Columns["Password"].Visible = false;
                this.dgUsers.Columns["Permission"].Visible = false;
                this.dgUsers.Columns["IsDBWrite"].Visible = false;
                this.dgUsers.Columns["ResponseCode"].Visible = false;
                this.dgUsers.Columns["ProfileImageName"].Visible = false;
                this.dgUsers.Columns["ActionBy"].Visible = false;
                this.dgUsers.Columns["GeoFileUrl"].Visible = false;
                this.dgUsers.Columns["GeoVersion"].Visible = false;

                this.dgUsers.Columns["MKALLGName"].Visible = false;
                this.dgUsers.Columns["MKAWard"].Visible = false;
                this.dgUsers.Columns["MKAWardName"].Visible = false;
                this.dgUsers.Columns["MKALocation"].Visible = false;
                this.dgUsers.Columns["MKALocationName"].Visible = false;
                this.dgUsers.Columns["MKAGeoFileUrl"].Visible = false;

                this.dgUsers.Columns["FullName"].HeaderText = "Full name";
                this.dgUsers.Columns["EmailAddress"].HeaderText = "Email Address";
                this.dgUsers.Columns["ContactNumber"].HeaderText = "Contact Number";
                this.dgUsers.Columns["StatusName"].HeaderText = "Status";
                this.dgUsers.Columns["RoleName"].HeaderText = "Role";
                this.dgUsers.Columns["RegionName"].HeaderText = "Region";
                this.dgUsers.Columns["ProvinceName"].HeaderText = "Province";
                this.dgUsers.Columns["DistrictName"].HeaderText = "District";
                this.dgUsers.Columns["LLGName"].HeaderText = "LLG";
                this.dgUsers.Columns["WardName"].HeaderText = "Ward";
                this.dgUsers.Columns["LocationName"].HeaderText = "Location";

                #region CheckBox

                //Add a CheckBox Column to the DataGridView at the first position.
                DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
                checkBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                ////checkBoxColumn.CellTemplate = new DataGridViewCheckBoxCell();
                checkBoxColumn.FlatStyle = FlatStyle.Standard;
                checkBoxColumn.ReadOnly = false;
                checkBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                checkBoxColumn.HeaderText = string.Empty;
                checkBoxColumn.Width = 40;
                checkBoxColumn.Name = "checkBoxColumn";
                if (dgUsers.Columns["checkBoxColumn"] == null)
                {
                    dgUsers.Columns.Insert(0, checkBoxColumn);
                };

                Rectangle oRectangle = this.dgUsers.GetCellDisplayRectangle(0, -1, true);
                Point oPoint = new Point();
                headerCheckBox.Size = new Size(15, 15);
                oPoint.X = oRectangle.Location.X + (oRectangle.Width - headerCheckBox.Width) / 2 + 1;
                oPoint.Y = oRectangle.Location.Y + (oRectangle.Height - headerCheckBox.Height) / 2 + 1;
                headerCheckBox.BackColor = Color.FromArgb(180, 180, 180);
                //Change the location of the CheckBox to make it stay on the header
                headerCheckBox.Location = oPoint;
                dgUsers.Controls.Add(headerCheckBox);

                //Assign Click event to the DataGridView Cell.
                //dgUsers.CellContentClick += new DataGridViewCellEventHandler(dgUsers_CellClick);
                #endregion

                //#region Profile Image
                //DataGridViewImageColumn profileImage = new DataGridViewImageColumn();
                ////profileImage.CellTemplate = new DataGridViewImageCell();
                //profileImage.ImageLayout = DataGridViewImageCellLayout.Stretch;
                //profileImage.Name = "ProfilePicture";
                //profileImage.HeaderText = "Profile Image";
                //dgUsers.RowTemplate.Height = 100;
                //dgUsers.Columns[1].Width = 100;
                //if (dgUsers.Columns["ProfilePicture"] == null)
                //{
                //    dgUsers.Columns.Insert(1, profileImage);
                //};
                //#endregion

                #region Edit Button
                DataGridViewButtonColumn editButton = new DataGridViewButtonColumn();
                editButton.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                editButton.FlatStyle = FlatStyle.Standard;
                editButton.HeaderText = "Action";
                editButton.Name = "Edit";
                editButton.Text = "Edit";
                editButton.UseColumnTextForButtonValue = true;
                editButton.Width = 60;
                if (dgUsers.Columns["Edit"] == null)
                {
                    dgUsers.Columns.Insert(30, editButton);
                }
                #endregion

                #region Reset Password
                DataGridViewButtonColumn resetPasswordButton = new DataGridViewButtonColumn();
                resetPasswordButton.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                resetPasswordButton.FlatStyle = FlatStyle.Standard;
                resetPasswordButton.HeaderText = "Reset Password";
                resetPasswordButton.Name = "ResetPassword";
                resetPasswordButton.Text = "Reset Password";
                resetPasswordButton.UseColumnTextForButtonValue = true;
                resetPasswordButton.Width = 100;
                if (dgUsers.Columns["ResetPassword"] == null)
                {
                    dgUsers.Columns.Insert(31, resetPasswordButton);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Resources.ResourceManager.GetString("ErrorMessage"), "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorLog.Write("List User", "BindUserGird", ex);
            }
            #endregion
        }
        #endregion

        #region Change User Status
        /// <summary>
        /// Changes the user status.
        /// </summary>
        /// <param name="status">if set to <c>true</c> [status].</param>
        private async void ChangeUserStatus(bool status)
        {
            string statustext = status.Equals(true) ? "active" : "inactive";
            requestModel = new CommonRequest();
            requestModel.status = status;

            List<int> userId = new List<int>();
            foreach (DataGridViewRow row in dgUsers.Rows)
            {
                bool isSelected = Convert.ToBoolean(row.Cells["checkBoxColumn"].Value);
                if (isSelected)
                {
                    userId.Add(Convert.ToInt32(row.Cells["UserId"].Value));
                }
            }

            if (userId.Count > 0)
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to " + statustext + " the record?", "PNGEC", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {
                        this.Cursor = Cursors.WaitCursor;
                        requestModel.ids = string.Join(",", userId.Select(n => n.ToString()).ToArray());
                        Response<UserModel> response = await GenericHttpClient.PostRequest<UserModel>("User/UpdateUserStatus", requestModel);
                        if (response.Status)
                        {
                            if (response.Data.ListItems != null && response.Data.ListItems.ToList().Count > 0)
                            {
                                if (response.Data.ListItems.ToList() != null)
                                {
                                    BindUserGird(response.Data.ListItems.ToList());
                                }
                            }
                            this.Cursor = Cursors.Arrow;
                            //MessageBox.Show("Status changed successfully", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            this.Cursor = Cursors.Arrow;
                            MessageBox.Show("Something went wrong, try again later", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        this.Cursor = Cursors.Arrow;
                        MessageBox.Show(Resources.ResourceManager.GetString("ErrorMessage"), "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ErrorLog.Write("List User", "ChangeUserStatus", ex);
                    }
                }
                // Clear all checkbox
                this.ClearCheckbox();
            }
            else
            {
                MessageBox.Show("At least one checkbox must be selected.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region Clear Checkbox
        /// <summary>
        /// Clears the checkbox.
        /// </summary>
        private void ClearCheckbox()
        {
            headerCheckBox.Checked = false;

            //Loop and check and uncheck all row CheckBoxes based on Header Cell CheckBox.
            foreach (DataGridViewRow row in dgUsers.Rows)
            {
                DataGridViewCheckBoxCell checkBox = (row.Cells["checkBoxColumn"] as DataGridViewCheckBoxCell);
                checkBox.Value = headerCheckBox.Checked;
            }
        }
        #endregion

        #region Get Image From Url
        /// <summary>
        /// Gets the image from URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        private static Image GetImageFromUrl(string url)
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
                return Properties.Resources.noimage;
            }
        }
        #endregion

        #region Reset Header CheckBox Location
        /// <summary>
        /// Resets the header CheckBox location.
        /// </summary>
        /// <param name="ColumnIndex">Index of the column.</param>
        /// <param name="RowIndex">Index of the row.</param>
        private void ResetHeaderCheckBoxLocation(int ColumnIndex, int RowIndex)
        {
            //Get the column header cell bounds
            Rectangle oRectangle = this.dgUsers.GetCellDisplayRectangle(ColumnIndex, RowIndex, true);

            Point oPoint = new Point();

            oPoint.X = oRectangle.Location.X + (oRectangle.Width - headerCheckBox.Width) / 2 + 1;
            oPoint.Y = oRectangle.Location.Y + (oRectangle.Height - headerCheckBox.Height) / 2 + 1;

            //Change the location of the CheckBox to make it stay on the header
            headerCheckBox.Location = oPoint;
        }
        #endregion

        #region Get Grid Cell Data
        /// <summary>
        /// Gets the grid cell data.
        /// </summary>
        private UserModel GetGridCellData(int rowIndex)
        {
            UserModel model = new UserModel();

            model.UserId = Convert.ToInt32(dgUsers.Rows[rowIndex].Cells["UserId"].Value);
            model.FirstName = Convert.ToString(dgUsers.Rows[rowIndex].Cells["FirstName"].Value);
            model.LastName = Convert.ToString(dgUsers.Rows[rowIndex].Cells["LastName"].Value);
            model.ProfileImageUrl = Convert.ToString(dgUsers.Rows[rowIndex].Cells["ProfileImageUrl"].Value);
            model.ProfileImageName = Convert.ToString(dgUsers.Rows[rowIndex].Cells["ProfileImageName"].Value);
            model.EmailAddress = Convert.ToString(dgUsers.Rows[rowIndex].Cells["EmailAddress"].Value);
            model.ContactNumber = Convert.ToString(dgUsers.Rows[rowIndex].Cells["ContactNumber"].Value);
            model.Password = Convert.ToString(dgUsers.Rows[rowIndex].Cells["Password"].Value);
            model.Status = Convert.ToBoolean(dgUsers.Rows[rowIndex].Cells["Status"].Value);
            model.Role = Convert.ToInt32(dgUsers.Rows[rowIndex].Cells["Role"].Value);
            model.Region = Convert.ToInt32(dgUsers.Rows[rowIndex].Cells["Region"].Value);
            model.Province = Convert.ToInt32(dgUsers.Rows[rowIndex].Cells["Province"].Value);
            model.District = Convert.ToInt32(dgUsers.Rows[rowIndex].Cells["District"].Value);
            model.LLG = Convert.ToInt32(dgUsers.Rows[rowIndex].Cells["LLG"].Value);
            model.Ward = Convert.ToInt32(dgUsers.Rows[rowIndex].Cells["Ward"].Value);
            model.Location = Convert.ToInt32(dgUsers.Rows[rowIndex].Cells["Location"].Value);
            model.MKADistrict = Convert.ToInt32(dgUsers.Rows[rowIndex].Cells["MKADistrict"].Value);
            model.MKALLG = Convert.ToInt32(dgUsers.Rows[rowIndex].Cells["MKALLG"].Value);
            model.MKAWard = Convert.ToInt32(dgUsers.Rows[rowIndex].Cells["MKAWard"].Value);
            model.MKALocation = Convert.ToInt32(dgUsers.Rows[rowIndex].Cells["MKALocation"].Value);
            model.Permission = Convert.ToString(dgUsers.Rows[rowIndex].Cells["Permission"].Value);

            return model;
        }
        #endregion


        #endregion

    }
}
