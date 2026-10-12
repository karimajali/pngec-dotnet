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

namespace PNGEC.Desktop.Forms
{
    public partial class MKALocationTransfer : Form
    {
        #region Variable Declaration
        /// <summary>
        /// The dt
        /// </summary>
        private DataTable dt = null;

        /// <summary>
        /// The model
        /// </summary>
        private MKALocationModel mkaLocationModel;

        /// <summary>
        /// The request
        /// </summary>
        public MKALocationModel request;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="MKALocationTransfer"/> class.
        /// </summary>
        public MKALocationTransfer(MKALocationModel mkaLocationModel)
        {
            InitializeComponent();

            if (mkaLocationModel != null)
            {
                this.mkaLocationModel = mkaLocationModel;
            }
        }
        
        #endregion

        #region Events
        /// <summary>
        /// Handles the Load event of the LocationTransfer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>        
        private void MKALocationTransfer_Load(object sender, EventArgs e)
        {
            this.BindMKADistrictName();

            if (this.mkaLocationModel != null && this.mkaLocationModel.LocationId > 0)
            {               
                cmbMKADistrictName.SelectedValue = this.mkaLocationModel.DistrictId;
                cmbMKALLGName.SelectedValue = this.mkaLocationModel.LLGId;
                cmbMKAWardName.SelectedValue = this.mkaLocationModel.WardId;
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
                if (this.mkaLocationModel.WardId == Convert.ToInt32(cmbMKAWardName.SelectedValue))
                {
                    MessageBox.Show("MKA Location can not move the same MKA ward.", "MKA PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure you want to Transfer the location?", "MKA PNGEC", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        this.TransferRecord();
                    }
                }
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cmbMKADistrictName control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
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

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cmbMKALLGName control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
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

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cmbDistrict control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        //private void cmbDistrictName_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    dt = new DataTable();
        //    dt.Clear();
        //    dt.Columns.Add("LLGId");
        //    dt.Columns.Add("LLGName");

        //    if (cmbMKADistrictName.SelectedIndex > 0)
        //    {
        //        dt.Clear();
        //        int selectedValue = Convert.ToInt32(cmbMKADistrictName.SelectedValue);
        //        DataView dataView = Globals.dtLLG.DefaultView;
        //        if (selectedValue > 0)
        //        {
        //            dataView.RowFilter = "districtId = " + selectedValue + "";
        //        }
        //        dt = dataView.ToTable();
        //    }

        //    DataRow row = dt.NewRow();
        //    row[0] = 0;
        //    row[1] = "Select LLG";
        //    dt.Rows.InsertAt(row, 0);

        //    cmbMKALLGName.DataSource = dt;
        //    cmbMKALLGName.ValueMember = "LLGId";
        //    cmbMKALLGName.DisplayMember = "LLGName";
        //}

        ///// <summary>
        ///// Handles the SelectedIndexChanged event of the cmbLLG control.
        ///// </summary>
        ///// <param name="sender">The source of the event.</param>
        ///// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        //private void cmbLLGName_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    dt = new DataTable();
        //    dt.Clear();
        //    dt.Columns.Add("WardId");
        //    dt.Columns.Add("WardName");
        //    dt.Columns.Add("WardNoWardName");

        //    if (cmbMKALLGName.SelectedIndex > 0)
        //    {
        //        dt.Clear();
        //        int selectedValue = Convert.ToInt32(cmbMKALLGName.SelectedValue);
        //        DataView dataView = Globals.dtWard.DefaultView;
        //        if (selectedValue > 0)
        //        {
        //            dataView.RowFilter = "lLGId = " + selectedValue + "";
        //        }
        //        dt = dataView.ToTable();
        //    }

        //    DataRow row = dt.NewRow();
        //    row[0] = 0;
        //    row[1] = "Select Ward";
        //    row["WardNoWardName"] = "Select Ward";
        //    dt.Rows.InsertAt(row, 0);

        //    cmbMKAWardName.DataSource = dt;
        //    cmbMKAWardName.ValueMember = "WardId";
        //    cmbMKAWardName.DisplayMember = "WardNoWardName";
        //}

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
            cmbMKADistrictName.SelectedIndex = 0;
            cmbMKALLGName.SelectedIndex = 0;
            cmbMKAWardName.SelectedIndex = 0;
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

        #region IsValidForm
        /// <summary>
        /// Determines whether [is valid form].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is valid form]; otherwise, <c>false</c>.
        /// </returns>
        private Boolean IsValidForm()
        {
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

        #region TransferRecord
        /// <summary>
        /// Transfers the record.
        /// </summary>
        private async void TransferRecord()
        {
            try
            {
                GlobalsModel.mainMaster.ShowLoader();

                request = new MKALocationModel
                {
                    DistrictId = Convert.ToInt32(cmbMKADistrictName.SelectedValue),
                    LLGId = Convert.ToInt32(cmbMKALLGName.SelectedValue),
                    WardId = Convert.ToInt32(cmbMKAWardName.SelectedValue),
                    LocationId = mkaLocationModel.LocationId,
                    ActionBy = GlobalsModel.LoggedUser.User.UserId
                };

                var response = await GenericHttpClient.PostRequestCommon<Response>("MKALocation/MKALocationTransfer", request);

                GlobalsModel.mainMaster.HideLoader();

                if (response != null)
                {
                    if (response.Status)
                    {
                        this.DialogResult = DialogResult.OK;
                        MessageBox.Show(response.Message, "MKA PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        this.DialogResult = DialogResult.None;
                        MessageBox.Show(response.Message, "MKA PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    this.DialogResult = DialogResult.None;
                    MessageBox.Show(Resources.ResourceManager.GetString("ErrorMessage"), "MKA PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Resources.ResourceManager.GetString("ErrorMessage"), "MKA PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorLog.Write("MKALocationTransfer", "TransferRecord", ex);
            }
        }
        #endregion

        #endregion

        
    }
}
