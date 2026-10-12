using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using PNGEC.API.Domain.Ward;
using PNGEC.API.Helper;
using PNGEC.Desktop.Model;
using PNGEC.Desktop.Properties;

namespace PNGEC.Desktop.Forms
{
    /// <summary>
    /// ward transfer form
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class WardTransfer : Form
    {
        #region Variable Declaration
        /// <summary>
        /// The dt
        /// </summary>
        private DataTable dt = null;

        /// <summary>
        /// The ward model
        /// </summary>
        private WardModel wardModel;

        /// <summary>
        /// The request
        /// </summary>
        public WardModel request;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="WardTransfer"/> class.
        /// </summary>
        /// <param name="wardModel">The ward model.</param>
        public WardTransfer(WardModel wardModel)
        {
            InitializeComponent();

            if (wardModel != null)
            {
                this.wardModel = wardModel;
            }
        }

        #endregion

        #region Events
        /// <summary>
        /// Handles the Load event of the WardTransfer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void WardTransfer_Load(object sender, EventArgs e)
        {
            this.BindRegionName();

            if (this.wardModel != null && this.wardModel.WardId > 0)
            {
                cmbRegionName.SelectedValue = this.wardModel.RegionId;
                cmbProvinceName.SelectedValue = this.wardModel.ProvinceId;
                cmbDistrictName.SelectedValue = this.wardModel.DistrictId;
                cmbLLGName.SelectedValue = this.wardModel.LLGId;
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
                if (this.wardModel.LLGId == Convert.ToInt32(cmbLLGName.SelectedValue))
                {
                    MessageBox.Show("Ward can not move the same LLG.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    var duplicateWardNumber = Globals.GeoList.WardList.ToList().Where(ward => ward.LLGId == Convert.ToInt32(cmbLLGName.SelectedValue)).FirstOrDefault(wnumber => wnumber.WardNumber == this.wardModel.WardNumber);
                    if (duplicateWardNumber != null)
                    {
                        MessageBox.Show($"ward number {this.wardModel.WardNumber} is already exist in {cmbLLGName.Text} LLG.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    DialogResult dialogResult = MessageBox.Show("Are you sure you want to Transfer the ward?", "PNGEC", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        this.TransferRecord();
                    }
                }
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

            return true;
        }
        #endregion

        #region TransferRecord
        /// <summary>
        /// Transfers the record.
        /// </summary>
        private async void TransferRecord()
        {
            GlobalsModel.mainMaster.ShowLoader();

            try
            {
                request = new WardModel
                {
                    RegionId = Convert.ToInt32(cmbRegionName.SelectedValue),
                    ProvinceId = Convert.ToInt32(cmbProvinceName.SelectedValue),
                    DistrictId = Convert.ToInt32(cmbDistrictName.SelectedValue),
                    LLGId = Convert.ToInt32(cmbLLGName.SelectedValue),
                    WardId = this.wardModel.WardId,
                    ActionBy = GlobalsModel.LoggedUser.User.UserId
                };

                var response = await GenericHttpClient.PostRequestCommon<Response>("Ward/WardTransfer", request);

                if (response != null)
                {
                    if (response.Status)
                    {
                        this.DialogResult = DialogResult.OK;
                        MessageBox.Show(response.Message, "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        this.DialogResult = DialogResult.None;
                        MessageBox.Show(response.Message, "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    this.DialogResult = DialogResult.None;
                    MessageBox.Show(Resources.ResourceManager.GetString("ErrorMessage"), "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Resources.ResourceManager.GetString("ErrorMessage"), "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorLog.Write("WardTransfer", "TransferRecord", ex);
            }

            GlobalsModel.mainMaster.HideLoader();
        }
        #endregion

        #endregion
    }
}
