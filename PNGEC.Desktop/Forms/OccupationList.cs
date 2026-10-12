using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Domain.Mobile.General;
using PNGEC.API.Helper;
using PNGEC.Desktop.Model;
using PNGEC.Desktop.Properties;

namespace PNGEC.Desktop.Forms
{
    public partial class OccupationList : Form
    {
        #region Variable declaration

        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="OccupationList"/> class.
        /// </summary>
        public OccupationList()
        {
            InitializeComponent();
        }
        #endregion

        #region Events

        /// <summary>
        /// Handles the Load event of the OccupationList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OccupationList_Load(object sender, EventArgs e)
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
            Occupation Occupation = new Occupation();
            Occupation.AutoScroll = true;
            if (Occupation.ShowDialog() == DialogResult.OK)
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

        private void dgOccupation_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && this.dgOccupation.Columns[e.ColumnIndex].Name == "Edit")
            {
                OccupationModel model = new OccupationModel();
                model.OccupationPK = Convert.ToInt32(this.dgOccupation.Rows[e.RowIndex].Cells["OccupationPK"].Value);
                model.OccupationName = Convert.ToString(this.dgOccupation.Rows[e.RowIndex].Cells["OccupationName"].Value);
                model.IsDeleted = false;
                model.ActionBy = GlobalsModel.LoggedUser.User.UserId;
                if (model != null)
                {
                    Occupation occupation = new Occupation(model);
                    if (occupation.ShowDialog() == DialogResult.OK)
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
            this.BindOccupationGrid();
        }


        #endregion

        #region Methods

        /// <summary>
        /// Binds the occupation grid.
        /// </summary>
        private async void BindOccupationGrid()
        {
            try
            {
                GlobalsModel.mainMaster.ShowLoader();

                PageList model = new PageList();
                model.KeyWord = this.txtSearch.Text;

                var lstOccupation = new List<OccupationModel>();
                var response = await GenericHttpClient.PostRequestCommon<List<OccupationModel>>("Occupation/GetOccupationList", model);
                if (response.Status)
                {
                    lstOccupation = response.Data;
                }

                this.dgOccupation.DataSource = lstOccupation.OrderBy(x =>x.OccupationName).ToList();

                if (lstOccupation != null)
                {
                    this.dgOccupation.Columns["OccupationPK"].Visible = false;
                    this.dgOccupation.Columns["IsDeleted"].Visible = false;
                    this.dgOccupation.Columns["ResponseCode"].Visible = false;
                    this.dgOccupation.Columns["ActionBy"].Visible = false;
                    this.dgOccupation.Columns["OccupationName"].HeaderText = "Occupation";

                    #region Edit Button
                    DataGridViewButtonColumn editButton = new DataGridViewButtonColumn();
                    editButton.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    editButton.FlatStyle = FlatStyle.Standard;
                    editButton.HeaderText = "Action";
                    editButton.Name = "Edit";
                    editButton.Text = "Edit";
                    editButton.UseColumnTextForButtonValue = true;
                    editButton.Width = 80;
                    if (this.dgOccupation.Columns["Edit"] == null)
                    {
                        this.dgOccupation.Columns.Insert(this.dgOccupation.ColumnCount, editButton);
                    }

                    this.dgOccupation.Columns["Edit"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    this.dgOccupation.Columns["Edit"].CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    // col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    #endregion
                }

                GlobalsModel.mainMaster.HideLoader();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Resources.ResourceManager.GetString("ErrorMessage"), "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorLog.Write("OccupationList", "BindOccupationGrid", ex);
            }

            this.Cursor = Cursors.Arrow;
        }

        /// <summary>
        /// Refresh Data
        /// </summary>
        private void RefreshData()
        {
            this.txtSearch.Text = string.Empty;
            this.BindOccupationGrid();
        }

        #endregion
    }
}
