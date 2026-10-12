using PNGEC.API.Domain.Writ;
using PNGEC.API.Helper;
using System;
using System.Data;
using System.Windows.Forms;

namespace PNGEC.Desktop.Forms
{
    public partial class WritPrintOptions : Form
    {
        #region Variable Declaration
        /// <summary>
        /// The dt
        /// </summary>
        private DataTable dt = null;

        public WritModel writModel = new WritModel();

        #endregion

        #region Constructor

        public WritPrintOptions()
        {
            InitializeComponent();
        }

        public WritPrintOptions(WritModel owritModel)
        {
            InitializeComponent();
            this.writModel = owritModel;    
        }

        #endregion

        #region Events


        private void WritPrintOptions_Load(object sender, EventArgs e)
        {
            this.BindRegionName();

            if (this.writModel != null)
            { 
                cmbRegionName.Text = this.writModel.RegionName;
                cmbProvinceName.Text = this.writModel.ProvinceName; 
                cmbDistrictName.Text = this.writModel.DistrictName;
                cmbLLGName.Text = this.writModel.LLGName;   
                cmbWardName.Text = this.writModel.WardName;               
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

                writModel.RegionId = selectedValue;
                writModel.RegionName =  cmbRegionName.Text;
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

                writModel.ProvinceId = selectedValue;
                writModel.ProvinceName = cmbProvinceName.Text;
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

                writModel.DistrictId = selectedValue;
                writModel.DistrictName = cmbDistrictName.Text;
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
        /// Handles the SelectedIndexChanged event of the cmbLLG control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cmbLLGName_SelectedIndexChanged(object sender, EventArgs e)
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

                writModel. LLGId= selectedValue;
                writModel.LLGName = cmbLLGName.Text;
            }

            DataRow row = dt.NewRow();
            row[0] = 0;
            row[1] = "Select Ward";
            row["WardName"] = "Select Ward";
            dt.Rows.InsertAt(row, 0);

            cmbWardName.DataSource = dt;
            cmbWardName.ValueMember = "WardId";
            cmbWardName.DisplayMember = "WardName";
        }

        /// <summary>
        ///  cmb ward selection change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbWardName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbWardName.SelectedIndex > 0)
            {
                int selectedValue = Convert.ToInt32(cmbWardName.SelectedValue);

                writModel.WardId = selectedValue;
                writModel.WardName = cmbWardName.Text;
            }

        }

        /// <summary>
        /// print the Writ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (IsValidForm())
            { 
                this.Close();
            }
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
            cmbRegionName.SelectedIndex = 0;
        }

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
                MessageBox.Show("Please select Region", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (Convert.ToInt32(cmbProvinceName.SelectedIndex) <= 0)
            {
                MessageBox.Show("Please select Province", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (Convert.ToInt32(cmbDistrictName.SelectedIndex) <= 0)
            {
                MessageBox.Show("Please select District", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (Convert.ToInt32(cmbLLGName.SelectedIndex) <= 0)
            {
                MessageBox.Show("Please select LLG", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (Convert.ToInt32(cmbWardName.SelectedIndex) <= 0)
            {
                MessageBox.Show("Please select Ward", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
        #endregion

        #endregion

    }

}
