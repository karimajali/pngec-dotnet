using PNGEC.API.Domain.Device;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Domain.User;
using PNGEC.API.Helper;
using PNGEC.Desktop.Model;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace PNGEC.Desktop.Forms
{
    public partial class Device : Form
    {
        #region Variable declaration
        /// <summary>
        /// The model
        /// </summary>
        private DeviceModel model;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Device"/> class.
        /// </summary>
        public Device()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new parameterized instance of the <see cref="Device"/> class.
        /// </summary>
        public Device(DeviceModel model)
        {
            InitializeComponent();

            this.model = model;
        }
        #endregion

        #region Events

        /// <summary>
        /// Handles the Load event of the Device control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Device_Load(object sender, EventArgs e)
        {
            this.BindStatus();
            BindAssignedToAsync();
            if (this.model != null && this.model.DeviceId > 0)
            {
                btnAdd.Text = "Save";
                txtDeviceNumber.Text = this.model.DeviceNumber;
                txtDeviceName.Text = this.model.DeviceName;
                txtDeviceOS.Text = this.model.DeviceOS;
                txtType.Text = this.model.DeviceType;
                cmbAssignedTo.SelectedValue = Convert.ToInt32(this.model.AssignedTo);
                cmbStatus.SelectedItem = Convert.ToBoolean(this.model.Status) ? CommonEnum.Status.Active : CommonEnum.Status.Inactive;
            }
        }

        /// <summary>
        /// Handles the Click event of the btnAdd control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnAdd_ClickAsync(object sender, EventArgs e)
        {
            if (IsValidForm())
            {
                string processtext = string.Empty;
                var deviceId = this.model != null ? this.model.DeviceId : 0;
                if (deviceId > 0)
                    processtext = "update";
                else
                    processtext = "add";

                DialogResult dialogResult = MessageBox.Show("Are you sure you want to " + processtext + " the record?", "PNGEC", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    var request = CreateAddRequestParam();
                    var response = GenericHttpClient.PostRequest<DeviceModel>("Device/AddUpdateDevice", request);
                    if (response.Result != null && response.Result.Status)
                    {
                        this.DialogResult = DialogResult.OK;
                        MessageBox.Show(response.Result.Message, "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        this.DialogResult = DialogResult.None;
                        MessageBox.Show(response.Result.Message, "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
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
            DeviceModel device = new DeviceModel
            {
                DeviceId = this.model != null ? this.model.DeviceId : 0,
                DeviceNumber = txtDeviceNumber.Text,
                DeviceName = txtDeviceName.Text,
                DeviceOS = txtDeviceOS.Text,
                Status = cmbStatus.SelectedItem.ToString() == CommonEnum.Status.Active.ToString(),
                AssignedTo = Convert.ToInt32(cmbAssignedTo.SelectedValue),
                DeviceType = txtType.Text,
                ActionBy = GlobalsModel.LoggedUser.User.UserId
            };
            return device;
        }
        #endregion

        #region ClearForm
        /// <summary>
        /// Clears the form.
        /// </summary>
        private void clearData()
        {
            txtDeviceNumber.Text = "";
            txtDeviceName.Text = "";
            txtDeviceOS.Text = "";
            txtType.Text = "";
            cmbAssignedTo.SelectedIndex = -1;
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
            if (string.IsNullOrWhiteSpace(txtDeviceNumber.Text.Trim()))
            {
                MessageBox.Show("Device Number can't be empty.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDeviceName.Text.Trim()))
            {
                MessageBox.Show("Device Name can't be empty.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (Convert.ToInt32(cmbAssignedTo.SelectedIndex) <= 0)
            {
                MessageBox.Show("Please select Assigned To", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        #endregion

        #region BindAssignedTo
        /// <summary>
        /// Binds the name of the region.
        /// </summary>
        private async void BindAssignedToAsync()
        {
            PageList model = new PageList();
            model.PageNo = 1;
            model.PageSize = 100;
            model.KeyWord = "";
            model.SortField = "UserId";
            model.SortOrder = "DESC";
            model.LoggedUserId = GlobalsModel.LoggedUser.User.UserId;

            Response<UserModel> response = await GenericHttpClient.PostRequest<UserModel>("User/GetUserList", model);
            if (response.Data != null)
            {
                var responseRole = response.Data.ListItems.Where(x => x.Role == CommonEnum.Role.DPO.GetHashCode() || x.Role == CommonEnum.Role.MKA.GetHashCode()).OrderBy(x => x.EmailAddress).ToList();
                responseRole.Insert(0, new UserModel() { UserId = 0, EmailAddress = "Select Assigned To" });
                cmbAssignedTo.DataSource = responseRole;
                cmbAssignedTo.ValueMember = "UserId";
                cmbAssignedTo.DisplayMember = "EmailAddress";
            }
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

        #endregion
    }
}

