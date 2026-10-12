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
    public partial class MKALLGTransfer : Form
    {
        
        #region Variable Declaration
        /// <summary>
        /// The dt
        /// </summary>
        private DataTable dt = null;

        /// <summary>
        /// The ward model
        /// </summary>
        private MKALLGModel mkaLLGModel;

        /// <summary>
        /// The request
        /// </summary>
        public MKALLGModel request;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="LLGTransfer"/> class.
        /// </summary>
        /// <param name="mkaLLGModel">The LLG model.</param>
        public MKALLGTransfer(MKALLGModel mkaLLGModel)
        {
            InitializeComponent();

            if (mkaLLGModel != null)
            {
                this.mkaLLGModel = mkaLLGModel;
            }
        }        
        #endregion

        #region Events
        /// <summary>
        /// Handles the Load event of the WardTransfer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>        
        private void MKALLGTransfer_Load(object sender, EventArgs e)
        {
            this.BindMKADistrictName();

            if (this.mkaLLGModel != null && this.mkaLLGModel.LLGId > 0)
            {
                cmbMKADistrictName.SelectedValue = this.mkaLLGModel.DistrictId;
               
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
                if (this.mkaLLGModel.DistrictId == Convert.ToInt32(cmbMKADistrictName.SelectedValue))
                {
                    MessageBox.Show("MKA LLG can not move the same District.", "MKA PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure you want to Transfer the MKA llg?", "MKA PNGEC", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
        //private void cmbRegionName_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    dt = new DataTable();
        //    dt.Clear();
        //    dt.Columns.Add("ProvinceId");
        //    dt.Columns.Add("ProvinceName");

        //    if (cmbRegionName.SelectedIndex > 0)
        //    {
        //        dt.Clear();
        //        int selectedValue = Convert.ToInt32(cmbRegionName.SelectedValue);
        //        DataView dataView = Globals.dtProvince.DefaultView;
        //        if (selectedValue > 0)
        //        {
        //            dataView.RowFilter = "regionId = " + selectedValue + "";
        //        }
        //        dt = dataView.ToTable();
        //    }

        //    DataRow row = dt.NewRow();
        //    row[0] = 0;
        //    row[1] = "Select Province";
        //    dt.Rows.InsertAt(row, 0);

        //    cmbProvinceName.DataSource = dt;
        //    cmbProvinceName.ValueMember = "ProvinceId";
        //    cmbProvinceName.DisplayMember = "ProvinceName";
        //}

        ///// <summary>
        ///// Handles the SelectedIndexChanged event of the cmbProvince control.
        ///// </summary>
        ///// <param name="sender">The source of the event.</param>
        ///// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        //private void cmbProvinceName_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    dt = new DataTable();
        //    dt.Clear();
        //    dt.Columns.Add("DistrictId");
        //    dt.Columns.Add("DistrictName");

        //    if (cmbProvinceName.SelectedIndex > 0)
        //    {
        //        dt.Clear();
        //        int selectedValue = Convert.ToInt32(cmbProvinceName.SelectedValue);
        //        DataView dataView = Globals.dtDistrict.DefaultView;
        //        if (selectedValue > 0)
        //        {
        //            dataView.RowFilter = "provinceId = " + selectedValue + "";
        //        }
        //        dt = dataView.ToTable();
        //    }

        //    DataRow row = dt.NewRow();
        //    row[0] = 0;
        //    row[1] = "Select District";
        //    dt.Rows.InsertAt(row, 0);

        //    cmbDistrictName.DataSource = dt;
        //    cmbDistrictName.ValueMember = "DistrictId";
        //    cmbDistrictName.DisplayMember = "DistrictName";
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
        }
        #endregion

        #region BindMKADistrictName();
        /// <summary>
        /// Binds the name of the region.
        /// </summary>
        private void BindMKADistrictName()
        {
            dt = new DataTable();
            dt = Globals.dtMKADistricts.Copy();

            DataRow row = dt.NewRow();
            row[0] = 0;
            row[1] = "Select District";
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
                request = new MKALLGModel
                {                   
                    DistrictId = Convert.ToInt32(cmbMKADistrictName.SelectedValue),
                    LLGId = this.mkaLLGModel.LLGId,
                    ActionBy = GlobalsModel.LoggedUser.User.UserId
                };

                var response = await GenericHttpClient.PostRequestCommon<Response>("MKALLG/MKALLGTransfer", request);

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
                ErrorLog.Write("MKALLGTransfer", "TransferRecord", ex);
            }

            GlobalsModel.mainMaster.HideLoader();
        }
        #endregion

        #endregion
    }
}
