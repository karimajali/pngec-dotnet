using System;
using System.Data;
using System.Windows.Forms;
using PNGEC.API.Domain.MKAGeoStructure;
using PNGEC.API.Helper;
using PNGEC.Desktop.Model;
using PNGEC.Desktop.Properties;

namespace PNGEC.Desktop.Forms
{
    public partial class MKAClan : Form
    {

        #region Variable declaration

        /// <summary>
        /// The dt
        /// </summary>
        private DataTable dt = null;

        /// <summary>
        /// The model
        /// </summary>
        private MKAClanModel model;
        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="MKAClan"/> class.
        /// </summary>
        public MKAClan()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MKAClan"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public MKAClan(MKAClanModel model)
        {
            InitializeComponent();
            this.model = model;
        }
        #endregion

        #region Events

        /// <summary>
        /// Handles the Load event of the MKA Clan control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>        
        private void MKAClan_Load(object sender, EventArgs e)
        {
            this.BindMKADistrictName();
            if (this.model != null)
            {
                this.txtClanName.Text = this.model.ClanName;
                this.txtClanDescription.Text = this.model.Description;
                this.cmbMKADistrictName.SelectedValue = this.model.DistrictId;
                this.cmbMKALLGName.SelectedValue = this.model.LLGId;
                this.cmbMKAWardName.SelectedValue = this.model.WardId;
            }
        }

        /// <summary>
        /// Handles the Click event of the btnSubmit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsValidForm())
                {
                    string processtext = string.Empty;
                    var deviceId = this.model != null ? this.model.ClanId : 0;
                    if (deviceId > 0)
                        processtext = "update";
                    else
                        processtext = "add";

                    DialogResult dialogResult = MessageBox.Show("Are you sure you want to " + processtext + " the record?", "MKA PNGEC", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.Yes)
                    {
                        MKAClanModel request = new MKAClanModel
                        {
                            ClanId = this.model != null ? this.model.ClanId : 0,
                            ClanName = this.txtClanName.Text,
                            Description = this.txtClanDescription.Text,
                            WardId = Convert.ToInt32(cmbMKAWardName.SelectedValue),
                            LLGId = Convert.ToInt32(cmbMKALLGName.SelectedValue),
                            DistrictId = Convert.ToInt32(cmbMKADistrictName.SelectedValue),
                            ActionBy = GlobalsModel.LoggedUser.User.UserId,
                            UpdateByUserName = GlobalsModel.LoggedUser.User.FirstName
                        };

                        GlobalsModel.mainMaster.ShowLoader();

                        var response = GenericHttpClient.PostRequest<MKAClanModel>("MKAClan/AddUpdateMKAClan", request);

                        GlobalsModel.mainMaster.HideLoader();

                        if (response.Result != null && response.Result.Status)
                        {
                            this.DialogResult = DialogResult.OK;
                            MessageBox.Show(response.Result.Message, "MKA PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            this.DialogResult = DialogResult.None;
                            MessageBox.Show(response.Result.Message, "MKA PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                GlobalsModel.mainMaster.HideLoader();

                MessageBox.Show(Resources.ResourceManager.GetString("ErrorMessage"), "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorLog.Write("MKAClan", "AddUpdateRecord", ex);
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

        #endregion

        #region Methods

        #region IsValidForm
        /// <summary>
        /// Determines whether [is valid form].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is valid form]; otherwise, <c>false</c>.
        /// </returns>
        private Boolean IsValidForm()
        {
            if (string.IsNullOrWhiteSpace(this.txtClanName.Text.Trim()))
            {
                MessageBox.Show("MKA Clan can't be empty.", "MKA PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        #endregion


    }
}
