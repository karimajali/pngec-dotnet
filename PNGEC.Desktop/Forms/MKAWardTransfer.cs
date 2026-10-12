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
    public partial class MKAWardTransfer : Form
    {
       
        #region Variable Declaration
        /// <summary>
        /// The dt
        /// </summary>
        private DataTable dt = null;

        /// <summary>
        /// The ward model
        /// </summary>
        private MKAWardModel mkaWardModel;

        /// <summary>
        /// The request
        /// </summary>
        public MKAWardModel request;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="WardTransfer"/> class.
        /// </summary>
        /// <param name="mkaWardModel">The ward model.</param>
        public MKAWardTransfer(MKAWardModel mkaWardModel)
        {
            InitializeComponent();

            if (mkaWardModel != null)
            {
                this.mkaWardModel = mkaWardModel;
            }
        }
       

        #endregion

        #region Events
        /// <summary>
        /// Handles the Load event of the WardTransfer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>       
        private void MKAWardTransfer_Load(object sender, EventArgs e)
        {
            this.BindMKADistrictName();

            if (this.mkaWardModel != null && this.mkaWardModel.WardId > 0)
            {              
                cmbMKADistrictName.SelectedValue = this.mkaWardModel.DistrictId;
                cmbMKALLGName.SelectedValue = this.mkaWardModel.LLGId;
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
                if (this.mkaWardModel.LLGId == Convert.ToInt32(cmbMKALLGName.SelectedValue))
                {
                    MessageBox.Show("Ward can not move the same MKA LLG.", "MKA PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    var duplicateWardNumber = Globals.MKAGeoList.MKAWardList.ToList().Where(ward => ward.LLGId == Convert.ToInt32(cmbMKALLGName.SelectedValue)).FirstOrDefault(wnumber => wnumber.WardNumber == this.mkaWardModel.WardNumber);
                    if (duplicateWardNumber != null)
                    {
                        MessageBox.Show($"ward number {this.mkaWardModel.WardNumber} is already exist in {cmbMKALLGName.Text} LLG.", "MKA PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    DialogResult dialogResult = MessageBox.Show("Are you sure you want to Transfer the ward?", "MKA PNGEC", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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

        //    cmbMKADistrictName.DataSource = dt;
        //    cmbMKADistrictName.ValueMember = "DistrictId";
        //    cmbMKADistrictName.DisplayMember = "DistrictName";
        //}

        ///// <summary>
        ///// Handles the SelectedIndexChanged event of the cmbDistrict control.
        ///// </summary>
        ///// <param name="sender">The source of the event.</param>
        ///// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
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
                request = new MKAWardModel
                {                   
                    DistrictId = Convert.ToInt32(cmbMKADistrictName.SelectedValue),
                    LLGId = Convert.ToInt32(cmbMKALLGName.SelectedValue),
                    WardId = this.mkaWardModel.WardId,
                    ActionBy = GlobalsModel.LoggedUser.User.UserId
                };

                var response = await GenericHttpClient.PostRequestCommon<Response>("MKAWard/MKAWardTransfer", request);

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
                ErrorLog.Write("MKAWardTransfer", "TransferRecord", ex);
            }

            GlobalsModel.mainMaster.HideLoader();
        }

        #endregion

        #endregion

        
    }
}
