using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Domain.MKAGeoStructure;
using PNGEC.API.Helper;
using PNGEC.Desktop.Model;
using PNGEC.Desktop.Properties;

namespace PNGEC.Desktop.Forms
{
    public partial class MKAClanList : Form
    {

        #region Variable declaration

        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="OccupationList"/> class.
        /// </summary>
        public MKAClanList()
        {
            InitializeComponent();
        }
        #endregion

        #region Events

        /// <summary>
        /// Handles the Load event of the MKAClanList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>       
        private void MKAClanList_Load(object sender, EventArgs e)
        {
            this.RefreshData();
        }

        /// <summary>
        /// Handles the Click event of the btnAddNew control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnAddNew_Click(object sender, EventArgs e)
        {
            MKAClan MKAClan = new MKAClan();
            MKAClan.AutoScroll = true;
            if (MKAClan.ShowDialog() == DialogResult.OK)
            {
                this.RefreshData();
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
        }

        /// <summary>
        /// Handles the Click event of the dgMKAClan control Cell Content Click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgMKAClan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && this.dgMKAClan.Columns[e.ColumnIndex].Name == "Edit")
            {
                MKAClanModel model = new MKAClanModel();
                model.ClanId = Convert.ToInt32(this.dgMKAClan.Rows[e.RowIndex].Cells["ClanId"].Value);
                model.ClanName = Convert.ToString(this.dgMKAClan.Rows[e.RowIndex].Cells["ClanName"].Value);
                model.Description = Convert.ToString(this.dgMKAClan.Rows[e.RowIndex].Cells["Description"].Value);
                model.DistrictId = Convert.ToInt32(this.dgMKAClan.Rows[e.RowIndex].Cells["DistrictId"].Value);
                model.LLGId = Convert.ToInt32(this.dgMKAClan.Rows[e.RowIndex].Cells["LLGId"].Value);
                model.WardId = Convert.ToInt32(this.dgMKAClan.Rows[e.RowIndex].Cells["WardId"].Value);
                model.ActionBy = GlobalsModel.LoggedUser.User.UserId;
                if (model != null)
                {
                    MKAClan clan = new MKAClan(model);
                    if (clan.ShowDialog() == DialogResult.OK)
                    {
                        this.RefreshData();
                    }
                }
            }
        }


        /// <summary>
        /// Handles the Click event of the btnSearch control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.BindMKAClanGrid();
        }


        #endregion

        #region Methods

        /// <summary>
        /// Binds the MKA Clan grid.
        /// </summary>
        private async void BindMKAClanGrid()
        {
            try
            {
                GlobalsModel.mainMaster.ShowLoader();

                PageList model = new PageList();
                model.KeyWord = this.txtSearch.Text;

                var lstMKAClans = new List<MKAClanModel>();
                var response = await GenericHttpClient.PostRequestCommon<List<MKAClanModel>>("MKAClan/GetMKAClanList", model);                

                if (response.Status)
                {
                    lstMKAClans = response.Data;
                }

                this.dgMKAClan.DataSource = lstMKAClans; // lstMKAClans.OrderBy(x=>x.ClanName).ToList();

                if (lstMKAClans != null)
                {
                    this.dgMKAClan.Columns["ClanId"].Visible = false;
                    this.dgMKAClan.Columns["Description"].Visible = false;
                    this.dgMKAClan.Columns["DistrictId"].Visible = false;
                    this.dgMKAClan.Columns["LLGId"].Visible = false;
                    this.dgMKAClan.Columns["WardId"].Visible = false;
                    this.dgMKAClan.Columns["WardNumber"].Visible = false;
                    //this.dgMKAClan.Columns["DistrictName"].Visible = false;
                    //this.dgMKAClan.Columns["LLGName"].Visible = false;
                    //this.dgMKAClan.Columns["WardName"].Visible = false;
                    this.dgMKAClan.Columns["ResponseCode"].Visible = false;
                    this.dgMKAClan.Columns["UpdateByUserName"].Visible = false;
                    this.dgMKAClan.Columns["modifiedDate"].Visible = false;
                    this.dgMKAClan.Columns["CreatedDate"].Visible = false;
                    this.dgMKAClan.Columns["ModifiedDate"].Visible = false;
                    this.dgMKAClan.Columns["LastChangeDate"].Visible = false;
                    this.dgMKAClan.Columns["ActionBy"].Visible = false;
                    this.dgMKAClan.Columns["ClanName"].HeaderText = "Clan";
                    this.dgMKAClan.Columns["DistrictName"].HeaderText = "MKA District";
                    this.dgMKAClan.Columns["LLGName"].HeaderText = "MKA LLG";
                    this.dgMKAClan.Columns["WardName"].HeaderText = "MKA Ward";

                    #region Edit Button
                    DataGridViewButtonColumn editButton = new DataGridViewButtonColumn();
                    editButton.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    editButton.FlatStyle = FlatStyle.Standard;
                    editButton.HeaderText = "Action";
                    editButton.Name = "Edit";
                    editButton.Text = "Edit";
                    editButton.UseColumnTextForButtonValue = true;
                    editButton.Width = 80;
                    if (this.dgMKAClan.Columns["Edit"] == null)
                    {
                        this.dgMKAClan.Columns.Insert(this.dgMKAClan.ColumnCount, editButton);
                    }

                    this.dgMKAClan.Columns["Edit"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    this.dgMKAClan.Columns["Edit"].CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    // col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    #endregion
                }

                GlobalsModel.mainMaster.HideLoader();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Resources.ResourceManager.GetString("ErrorMessage"), "MKA PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorLog.Write("MKAClanList", "BindMKAClanGrid", ex);
            }

            this.Cursor = Cursors.Arrow;
        }

        /// <summary>
        /// Refresh Data
        /// </summary>
        private void RefreshData()
        {
            this.txtSearch.Text = string.Empty;
            this.BindMKAClanGrid();
        }

        #endregion
    }
}
