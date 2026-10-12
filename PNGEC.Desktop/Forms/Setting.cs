using System;
using System.Globalization;
using System.Windows.Forms;
using Newtonsoft.Json;
using PNGEC.API.Domain.Setting;
using PNGEC.API.Helper;
using PNGEC.Desktop.Model;

namespace PNGEC.Desktop.Forms
{
    public partial class Setting : Form
    {
        #region Variable declaration
        private SettingModel settingModel;
        #endregion

        #region Constructor
        /// <summary>Initializes a new instance of the <see cref="Setting" /> class.</summary>
        public Setting()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        /// <summary>Handles the Load event of the Setting control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void Setting_Load(object sender, EventArgs e)
        {
            this.dtpProvisionalCutOffDOB.Value = new DateTime(2005, 11, 09);
            this.dtpProvisionalCutOffDOB.MinDate = new DateTime(2005, 11, 09);
            this.dtpProvisionalCutOffDOB.MaxDate = DateTime.Today;
            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.DefaultProvisionalCutOffDOB))
            {
                try
                {
                    this.dtpProvisionalCutOffDOB.Value = DateTime.ParseExact(Properties.Settings.Default.DefaultProvisionalCutOffDOB, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                }
                catch { }
            }

            this.GetSetting();
        }

        /// <summary>Handles the Click event of the btnSave control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.DefaultProvisionalCutOffDOB = this.dtpProvisionalCutOffDOB.Value.ToString("dd-MM-yyyy");
            Properties.Settings.Default.Save();
            MessageBox.Show("Provisional Cut-Off Date of birth Updated successfully.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Handles the Click event of the btnUpdateSetting control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnUpdateSetting_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(this.txtDCPassword.Text))
                {
                    MessageBox.Show("Please enter DC password", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (string.IsNullOrWhiteSpace(this.txtDCVersion.Text))
                {
                    MessageBox.Show("Please enter DC version", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (System.Text.RegularExpressions.Regex.IsMatch(this.txtDCVersion.Text, "[^0-9]"))
                {
                    MessageBox.Show("Please enter valid DC version.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (string.IsNullOrWhiteSpace(this.txtOccupationVersion.Text))
                {
                    MessageBox.Show("Please enter occupation version", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (System.Text.RegularExpressions.Regex.IsMatch(this.txtOccupationVersion.Text, "[^0-9]"))
                {
                    MessageBox.Show("Please enter valid occupation version.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (string.IsNullOrWhiteSpace(this.txtGeoVersion.Text))
                {
                    MessageBox.Show("Please enter geo version", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (System.Text.RegularExpressions.Regex.IsMatch(this.txtGeoVersion.Text, "[^0-9]"))
                {
                    MessageBox.Show("Please enter valid geo version.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("Are you sure you want to update the setting data?", "PNGEC", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    var updatedSettingModel = new SettingModel();
                    updatedSettingModel.DCPassword = CommonFunction.Encrypt(this.txtDCPassword.Text, Globals.saltKey);
                    if (this.settingModel.DCPassword != updatedSettingModel.DCPassword)
                    {
                        updatedSettingModel.IsDCChange = true;
                    }

                    updatedSettingModel.DCVersion = Convert.ToInt32(this.txtDCVersion.Text);
                    if (this.settingModel.DCVersion != updatedSettingModel.DCVersion)
                    {
                        updatedSettingModel.IsDCChange = true;
                    }

                    updatedSettingModel.OccupationVersion = Convert.ToInt32(this.txtOccupationVersion.Text);
                    if (this.settingModel.OccupationVersion != updatedSettingModel.OccupationVersion)
                    {
                        updatedSettingModel.IsOccupationChange = true;
                    }

                    updatedSettingModel.GeoVersion = Convert.ToInt32(this.txtGeoVersion.Text);
                    if (this.settingModel.GeoVersion != updatedSettingModel.GeoVersion)
                    {
                        updatedSettingModel.IsGeoChange = true;
                    }

                    updatedSettingModel.ActionBy = GlobalsModel.LoggedUser.User.UserId;
                    this.UpdateSetting(updatedSettingModel);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Write("Setting", "btnUpdateSetting_Click", ex);
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the txtDCVersion control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        private void txtDCVersion_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        /// <summary>
        /// Handles the KeyPress event of the txtOccupationVersion control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        private void txtOccupationVersion_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        /// <summary>
        /// Handles the KeyPress event of the txtGeoVersion control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        private void txtGeoVersion_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Gets the setting.
        /// </summary>
        private async void GetSetting()
        {
            GlobalsModel.mainMaster.ShowLoader();

            try
            {
                var response = await GenericHttpClient.GetResponse("Setting/GetSetting");
                if (response != null && response.Data != null)
                {
                    this.AssignSettingData(JsonConvert.DeserializeObject<SettingModel>(response.Data.ToString()));
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Write("Setting", "GetSetting", ex);
            }

            GlobalsModel.mainMaster.HideLoader();
        }

        /// <summary>
        /// Updates the setting.
        /// </summary>
        /// <param name="updatedSettingModel">The updated setting model.</param>
        private async void UpdateSetting(SettingModel updatedSettingModel)
        {
            GlobalsModel.mainMaster.ShowLoader();

            try
            {
                var response = await GenericHttpClient.PostRequestCommon<SettingModel>("Setting/UpdateSetting", updatedSettingModel);
                if (response != null)
                {
                    if (response.Status)
                    {
                        MessageBox.Show(response.Message, "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.AssignSettingData(response.Data);
                    }
                    else
                    {
                        MessageBox.Show(response.Message, "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Write("Setting", "UpdateSetting", ex);
            }

            GlobalsModel.mainMaster.HideLoader();
        }

        /// <summary>
        /// Assigns the setting data.
        /// </summary>
        /// <param name="newSettingModel">The new setting model.</param>
        private void AssignSettingData(SettingModel newSettingModel)
        {
            this.settingModel = newSettingModel;
            if (settingModel != null)
            {
                if (!string.IsNullOrWhiteSpace(settingModel.DCPassword))
                {
                    this.txtDCPassword.Text = CommonFunction.Decrypt(settingModel.DCPassword, Globals.saltKey);
                }

                this.txtDCDateReset.Text = DateTime.SpecifyKind(settingModel.DCDateReset, DateTimeKind.Utc).ToLocalTime().ToString("dd-MM-yyyy HH:mm");
                this.txtDCVersion.Text = Convert.ToString(settingModel.DCVersion);

                this.txtOccupationRefreshDate.Text = DateTime.SpecifyKind(settingModel.OccupationRefreshDate, DateTimeKind.Utc).ToLocalTime().ToString("dd-MM-yyyy HH:mm");
                this.txtOccupationVersion.Text = Convert.ToString(settingModel.OccupationVersion);

                this.txtGeoRefreshDate.Text = DateTime.SpecifyKind(settingModel.GeoRefreshDate, DateTimeKind.Utc).ToLocalTime().ToString("dd-MM-yyyy HH:mm");
                this.txtGeoVersion.Text = Convert.ToString(settingModel.GeoVersion);
            }
        }
        #endregion

    }
}
