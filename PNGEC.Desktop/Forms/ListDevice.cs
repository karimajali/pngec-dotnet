using PNGEC.API.Domain.Device;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Helper;
using PNGEC.Desktop.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PNGEC.Desktop.Forms
{
    public partial class ListDevice : Form
    {
        #region Variable declaration
        /// <summary>
        /// The header CheckBox
        /// </summary>
        private CheckBox headerCheckBox = new CheckBox();

        /// <summary>
        /// The request model
        /// </summary>
        private CommonRequest requestModel = null;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ListDevice"/> class.
        /// </summary>
        public ListDevice()
        {
            InitializeComponent();
        }
        #endregion

        #region Events

        /// <summary>
        /// Handles the Load event of the ListDevice control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ListDevice_Load(object sender, EventArgs e)
        {
            RefreshData();

            headerCheckBox.Click -= new EventHandler(HeaderCheckBox_Clicked);
            headerCheckBox.Click += new EventHandler(HeaderCheckBox_Clicked);

            dgDevices.CellContentClick -= new DataGridViewCellEventHandler(dgDevices_CellContentClick);
            dgDevices.CellContentClick += new DataGridViewCellEventHandler(dgDevices_CellContentClick);
        }

        /// <summary>
        /// Handles the Click event of the btnAddNew control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnAddNew_Click(object sender, EventArgs e)
        {
            Device device = new Device();
            device.AutoScroll = true;
            if (device.ShowDialog() == DialogResult.OK)
            {
                RefreshData();
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
            dgDevices.EndEdit();

            //Loop and check and uncheck all row CheckBoxes based on Header Cell CheckBox.
            foreach (DataGridViewRow row in dgDevices.Rows)
            {
                DataGridViewCheckBoxCell checkBox = (row.Cells["checkBoxColumn"] as DataGridViewCheckBoxCell);
                checkBox.Value = headerCheckBox.Checked;
            }
        }

        /// <summary>
        /// Handles the Click event of the btnRefresh control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshData();
            txtSearch.Text = string.Empty;
            headerCheckBox.Checked = false;
        }

        /// <summary>
        /// Handles the CellContentClick event of the dgDevices control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void dgDevices_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Check to ensure that the row CheckBox is clicked.
            if (e.RowIndex >= 0 && dgDevices.Columns[e.ColumnIndex].Name == "checkBoxColumn" && e.ColumnIndex == 0)
            {
                DataGridViewCheckBoxCell crntCheckBoxCell = (DataGridViewCheckBoxCell)this.dgDevices.Rows[e.RowIndex].Cells["checkBoxColumn"];
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
                foreach (DataGridViewRow row in dgDevices.Rows)
                {
                    if (Convert.ToBoolean(row.Cells["checkBoxColumn"].EditedFormattedValue) == false)
                    {
                        isChecked = false;
                        break;
                    }
                }
                headerCheckBox.Checked = isChecked;
            }
            else if (e.RowIndex >= 0 && dgDevices.Columns[e.ColumnIndex].Name == "Edit")
            {
                // Clear all checkbox
                this.ClearCheckbox();

                DeviceModel model = new DeviceModel();
                model.DeviceId = Convert.ToInt32(dgDevices.Rows[e.RowIndex].Cells["DeviceId"].Value);
                model.DeviceNumber = Convert.ToString(dgDevices.Rows[e.RowIndex].Cells["DeviceNumber"].Value);
                model.DeviceName = Convert.ToString(dgDevices.Rows[e.RowIndex].Cells["DeviceName"].Value);
                model.DeviceOS = Convert.ToString(dgDevices.Rows[e.RowIndex].Cells["DeviceOS"].Value);
                model.DeviceType = Convert.ToString(dgDevices.Rows[e.RowIndex].Cells["DeviceType"].Value);
                model.AssignedTo = Convert.ToInt32(dgDevices.Rows[e.RowIndex].Cells["AssignedTo"].Value);
                model.Status = Convert.ToBoolean(dgDevices.Rows[e.RowIndex].Cells["Status"].Value);

                if (model != null)
                {
                    Device device = new Device(model);
                    if (device.ShowDialog() == DialogResult.OK)
                    {
                        RefreshData();
                    }
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the btnSearch control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void btnSearch_Click(object sender, EventArgs e)
        {
            PageList model = new PageList();
            model.PageNo = 1;
            model.PageSize = 50;
            model.KeyWord = txtSearch.Text;
            model.SortField = "DeviceId";
            model.SortOrder = "DESC";

            Response<DeviceModel> response = await GenericHttpClient.PostRequest<DeviceModel>("Device/GetDeviceList", model);
            if (response.Status)
            {
                BindDeviceGrid(response.Data.ListItems.ToList());
            }
        }

        /// <summary>
        /// Handles the Click event of the btnActive control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnActive_Click(object sender, EventArgs e)
        {
            this.ChangeDeviceStatus(true);
        }

        /// <summary>
        /// Handles the Click event of the btnInactive control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnInactive_Click(object sender, EventArgs e)
        {
            this.ChangeDeviceStatus(false);
        }

        /// <summary>
        /// Handles the CellPainting event of the dgUsers control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellPaintingEventArgs"/> instance containing the event data.</param>
        private void dgDevices_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex == 0)
            {
                ResetHeaderCheckBoxLocation(e.ColumnIndex, e.RowIndex);
            }
        }

        /// <summary>
        /// Handles the CellFormatting event of the dgDevices control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void dgDevices_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgDevices.Columns[e.ColumnIndex].Name == "StatusName")
            {
                foreach (DataGridViewRow row in dgDevices.Rows)
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
        
        #endregion

        #region Methods

        #region BindGrid
        /// <summary>
        /// Binds the device gird.
        /// </summary>
        /// <param name="model">The model.</param>
        private void BindDeviceGrid(List<DeviceModel> lstDevices)
        {           
            ////dgDevices.ColumnHeadersDefaultCellStyle.BackColor = Color.Gray;
            ////using (Font font = new Font(dgDevices.DefaultCellStyle.Font.FontFamily, 7, FontStyle.Bold))
            ////{
            ////    dgDevices.ColumnHeadersDefaultCellStyle.Font = font;
            ////}
            ////dgDevices.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ////dgDevices.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            ////dgDevices.EnableHeadersVisualStyles = false;
            
            dgDevices.DataSource = lstDevices;

            dgDevices.Columns["DeviceId"].Visible = false;
            dgDevices.Columns["DeviceOS"].Visible = false;
            dgDevices.Columns["Status"].Visible = false;
            dgDevices.Columns["AssignedTo"].Visible = false;
            dgDevices.Columns["ResponseCode"].Visible = false;
            dgDevices.Columns["UserName"].Visible = false;
            dgDevices.Columns["DeviceNameChanged"].Visible = false;
            dgDevices.Columns["AssignedToChanged"].Visible = false;
            dgDevices.Columns["StatusName"].HeaderText = "Status";
            dgDevices.Columns["DeviceType"].HeaderText = "Type";
            dgDevices.Columns["EmailAddress"].HeaderText = "Assigned To";
            dgDevices.Columns["DeviceNumber"].HeaderText = "Device Number";
            dgDevices.Columns["DeviceName"].HeaderText = "Device Name";

            #region CheckBox
            //////Place the Header CheckBox in the Location of the Header Cell.
            ////headerCheckBox.Location = new Point(145, 3);
            ////headerCheckBox.BackColor = Color.Gray;
            ////headerCheckBox.Size = new Size(18, 18);

            //////Assign Click event to the Header CheckBox.
            ////headerCheckBox.Click += new EventHandler(HeaderCheckBox_Clicked);
            ////dgDevices.Controls.Add(headerCheckBox);

            //Add a CheckBox Column to the DataGridView at the first position.
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            checkBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            ////checkBoxColumn.CellTemplate = new DataGridViewCheckBoxCell();
            checkBoxColumn.FlatStyle = FlatStyle.Standard;
            checkBoxColumn.ReadOnly = false;
            checkBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            checkBoxColumn.HeaderText = string.Empty;
            checkBoxColumn.Width = 60;
            checkBoxColumn.Name = "checkBoxColumn";
            if (dgDevices.Columns["checkBoxColumn"] == null)
            {
                dgDevices.Columns.Insert(0, checkBoxColumn);
            }

            Rectangle oRectangle = this.dgDevices.GetCellDisplayRectangle(0, -1, true);
            Point oPoint = new Point();
            headerCheckBox.Size = new Size(15, 15);
            oPoint.X = oRectangle.Location.X + (oRectangle.Width - headerCheckBox.Width) / 2 + 1;
            oPoint.Y = oRectangle.Location.Y + (oRectangle.Height - headerCheckBox.Height) / 2 + 1;            
            headerCheckBox.BackColor = Color.FromArgb(180, 180, 180);
            //Change the location of the CheckBox to make it stay on the header
            headerCheckBox.Location = oPoint;
            dgDevices.Controls.Add(headerCheckBox);
            //Assign Click event to the DataGridView Cell.
            #endregion

            #region Edit Button
            DataGridViewButtonColumn editButton = new DataGridViewButtonColumn();
            editButton.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            editButton.FlatStyle = FlatStyle.Standard;
            editButton.HeaderText = "Action";
            editButton.Name = "Edit";
            editButton.Text = "Edit";
            editButton.UseColumnTextForButtonValue = true;
            editButton.Width = 80;
            if (dgDevices.Columns["Edit"] == null)
            {
                dgDevices.Columns.Insert(dgDevices.ColumnCount, editButton);
            }
            #endregion
        }
        #endregion

        #region RefreshData
        /// <summary>
        /// Refresh Data
        /// </summary>
        private async void RefreshData()
        {
            txtSearch.Text = "";
            PageList model = new PageList();
            model.PageNo = 1;
            model.PageSize = 50;
            model.KeyWord = "";
            model.SortField = "DeviceId";
            model.SortOrder = "DESC";
            model.LoggedUserId = GlobalsModel.LoggedUser.User.UserId;

            Response<DeviceModel> response = await GenericHttpClient.PostRequest<DeviceModel>("Device/GetDeviceList", model);
            if (response.Status)
            {
                BindDeviceGrid(response.Data.ListItems.ToList());
            }
        }
        #endregion

        #region ChangeDeviceStatus
        /// <summary>
        /// Change Device Status
        /// </summary>
        private async void ChangeDeviceStatus(bool status)
        {
            string statustext = status.Equals(true) ? "active" : "inactive";
            requestModel = new CommonRequest();
            requestModel.status = status;

            List<int> deviceId = new List<int>();
            foreach (DataGridViewRow row in dgDevices.Rows)
            {
                bool isSelected = Convert.ToBoolean(row.Cells["checkBoxColumn"].Value);
                if (isSelected)
                {
                    deviceId.Add(Convert.ToInt32(row.Cells["DeviceId"].Value));
                }
            }

            if (deviceId.Count > 0)
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to " + statustext + " the record?", "PNGEC", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    this.Cursor = Cursors.WaitCursor;
                    requestModel.ids = string.Join(",", deviceId.Select(n => n.ToString()).ToArray());
                    Response<DeviceModel> response = await GenericHttpClient.PostRequest<DeviceModel>("Device/UpdateDeviceStatus", requestModel);
                    if (response.Status)
                    {
                        if (response.Data.ListItems != null && response.Data.ListItems.ToList().Count > 0)
                        {
                            this.BindDeviceGrid(response.Data.ListItems.ToList());
                        }
                        this.Cursor = Cursors.Arrow;
                        MessageBox.Show("Status changed successfully", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        this.Cursor = Cursors.Arrow;
                        MessageBox.Show("Something went wrong, try again later", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        
        #region ClearCheckbox
        /// <summary>
        /// Clears the checkbox.
        /// </summary>
        private void ClearCheckbox()
        {
            headerCheckBox.Checked = false;

            //Loop and check and uncheck all row CheckBoxes based on Header Cell CheckBox.
            foreach (DataGridViewRow row in dgDevices.Rows)
            {
                DataGridViewCheckBoxCell checkBox = (row.Cells["checkBoxColumn"] as DataGridViewCheckBoxCell);
                checkBox.Value = headerCheckBox.Checked;
            }
        }
        #endregion

        #region ResetHeaderCheckBoxLocation
        /// <summary>
        /// Resets the header CheckBox location.
        /// </summary>
        /// <param name="ColumnIndex">Index of the column.</param>
        /// <param name="RowIndex">Index of the row.</param>
        private void ResetHeaderCheckBoxLocation(int ColumnIndex, int RowIndex)
        {
            //Get the column header cell bounds
            Rectangle oRectangle = this.dgDevices.GetCellDisplayRectangle(ColumnIndex, RowIndex, true);

            Point oPoint = new Point();

            oPoint.X = oRectangle.Location.X + (oRectangle.Width - headerCheckBox.Width) / 2 + 1;
            oPoint.Y = oRectangle.Location.Y + (oRectangle.Height - headerCheckBox.Height) / 2 + 1;

            //Change the location of the CheckBox to make it stay on the header
            headerCheckBox.Location = oPoint;
        }
        #endregion

        #endregion
    }
}
