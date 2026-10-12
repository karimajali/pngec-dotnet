using Microsoft.Reporting.WinForms;
using PNGEC.API.Domain.AugmentedRollStat;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Helper;
using PNGEC.Desktop.Model;
using PNGEC.Desktop.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PNGEC.Desktop.Forms
{
    public partial class AugmentedRollStat : Form
    {
        #region variable declaration
        /// <summary>
        /// The geo tree structure
        /// </summary>
        private GeoTreeStructure TreeStructure = null;

        /// <summary>
        /// The page list
        /// </summary>
        private PageList pageList = null;

        /// <summary>
        /// The BTN pager
        /// </summary>
        private Button btnPager = new Button();

        /// <summary>
        /// The page size value
        /// </summary>
        private int pageSizeValue;

        /// <summary>
        /// The page size array
        /// </summary>
        private string[] pageSizeArray = { "15", "50", "100", "500" };

        /// <summary>
        /// The ds
        /// </summary>
        private DataSet ds = null;
        #endregion

        #region constructor 
        /// <summary>
        /// Initializes a new instance of the <see cref="AugmentedRollStat"/> class.
        /// </summary>
        public AugmentedRollStat()
        {
            InitializeComponent();
        }
        #endregion

        #region Events

        /// <summary>
        /// Handles the Load event of the AugmentedRollStat control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void AugmentedRollStat_Load(object sender, EventArgs e)
        {
            GlobalsModel.mainMaster.TreeViewSelectedChanged -= TreeViewSelectedChanged_AfterSelect;
            GlobalsModel.mainMaster.TreeViewSelectedChanged += TreeViewSelectedChanged_AfterSelect;

            if (GlobalsModel.TreeViewStructure != null)
            {
                TreeStructure = GlobalsModel.TreeViewStructure;

                if (TreeStructure.MainTagName != "Location")
                {
                    pnlExport.Visible = true;
                    LoadGridViewSetting();
                }
                else
                {
                    pnlExport.Visible = false;
                }
            }
        }

        /// <summary>
        /// Handles the AfterSelect event of the TreeViewSelectedChanged control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <exception cref="NotImplementedException"></exception>
        private async void TreeViewSelectedChanged_AfterSelect(object sender, EventArgs e)
        {
            TreeStructure = GlobalsModel.TreeViewStructure;

            if(TreeStructure.MainTagName != "Location")
            {
                pnlExport.Visible = true;
                await GridViewSetting("AfterSelect");
            }
            else
            {
                pnlExport.Visible = false;
            }
        }

        /// <summary>
        /// Handles the Click event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void Page_Click(object sender, EventArgs e)
        {
            btnPager = (sender as Button);
            await GridViewSetting();
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the CmbPageSize control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void CmbPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            pageSizeValue = Convert.ToInt32(cmbPageSize.SelectedValue);
            await GridViewSetting("PageSize");
        }

        /// <summary>
        /// Handles the CellClick event of the dgAugmentedRollStat control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void dgAugmentedRollStat_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgAugmentedRollStat.Columns[e.ColumnIndex].Name == "Edit")
            {
                AugmentedRollStatModel model = (AugmentedRollStatModel)dgAugmentedRollStat.Rows[e.RowIndex].DataBoundItem;
                if (model != null)
                {
                    AugmentedRollStatUpdate augmentedRollStat = new AugmentedRollStatUpdate(model);
                    if (augmentedRollStat.ShowDialog() == DialogResult.OK)
                    { 
                        dgAugmentedRollStat.Rows[e.RowIndex].Cells["ChangeStringDate"].Value = augmentedRollStat.augmentedRollStatModel.ChangeStringDate.ToString();
                        dgAugmentedRollStat.Rows[e.RowIndex].Cells["OLD"].Value = augmentedRollStat.augmentedRollStatModel.OLD.ToString();
                        dgAugmentedRollStat.Rows[e.RowIndex].Cells["NEW"].Value = augmentedRollStat.augmentedRollStatModel.NEW.ToString();
                        dgAugmentedRollStat.Rows[e.RowIndex].Cells["UPDATED"].Value = augmentedRollStat.augmentedRollStatModel.UPDATED.ToString();
                        dgAugmentedRollStat.Rows[e.RowIndex].Cells["TRANSFERIN"].Value = augmentedRollStat.augmentedRollStatModel.TRANSFERIN.ToString();
                        dgAugmentedRollStat.Rows[e.RowIndex].Cells["TRANSFEROUT"].Value = augmentedRollStat.augmentedRollStatModel.TRANSFEROUT.ToString();
                        dgAugmentedRollStat.Rows[e.RowIndex].Cells["DECEASED"].Value = augmentedRollStat.augmentedRollStatModel.DECEASED.ToString();
                        dgAugmentedRollStat.Rows[e.RowIndex].Cells["DUPLICATE"].Value = augmentedRollStat.augmentedRollStatModel.DUPLICATE.ToString();
                        dgAugmentedRollStat.Rows[e.RowIndex].Cells["GHOST"].Value = augmentedRollStat.augmentedRollStatModel.GHOST.ToString();
                        dgAugmentedRollStat.Rows[e.RowIndex].Cells["UNDERAGE"].Value = augmentedRollStat.augmentedRollStatModel.UNDERAGE.ToString();
                        dgAugmentedRollStat.Rows[e.RowIndex].Cells["TOTAL"].Value = augmentedRollStat.augmentedRollStatModel.TOTAL.ToString();
                        //await GridViewSetting();
                    }
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the btnExport control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnExport_Click(object sender, EventArgs e)
        {
            string messageBoxText = string.Empty;

            try
            {
                if (dgAugmentedRollStat != null && dgAugmentedRollStat.Rows.Count > 0)
                {
                    var saveFileDialog = new SaveFileDialog();
                    saveFileDialog.FileName = GlobalsModel.reportName + "_AugmentedRollStat";
                    saveFileDialog.DefaultExt = ".pdf";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        GlobalsModel.mainMaster.ShowLoader();

                        DataTable data = GetDataTableFromDGV(dgAugmentedRollStat);
                        byte[] bytes = ExportReportSource(data);

                        using (FileStream stream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                        {
                            stream.Write(bytes, 0, bytes.Length);
                        }

                        messageBoxText = string.Format("Augmented Roll Stat is exported sucessfully at {0}", saveFileDialog.FileName);
                        MessageBox.Show(messageBoxText, "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("No data", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                messageBoxText = string.Empty;

                MessageBox.Show(Resources.ResourceManager.GetString("ErrorMessage"), "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorLog.Write("dgAugmentedRollStat", "btnExport_Click", ex);
            }
            finally
            {
                messageBoxText = string.Empty;
                GlobalsModel.mainMaster.HideLoader();
            }
        }

        /// <summary>
        /// Handles the FormClosing event of the AugmentedRollStat control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
        private void AugmentedRollStat_FormClosing(object sender, FormClosingEventArgs e)
        {
            GlobalsModel.mainMaster.TreeViewSelectedChanged -= TreeViewSelectedChanged_AfterSelect;
        }

        #endregion

        #region Methods

        #region Load Grid View Setting
        /// <summary>
        /// Loads the grid view setting.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private async void LoadGridViewSetting()
        {
            TreeStructure = GlobalsModel.TreeViewStructure;
            await GridViewSetting("AfterSelect");
        }
        #endregion

        #region GridViewSetting
        /// <summary>
        /// Grids the view setting.
        /// </summary>
        private async Task GridViewSetting(string eventName = "")
        {
            GlobalsModel.mainMaster.ShowLoader();

            try
            {
                pageList = new PageList();

                // Current Database Filed 
                pageList.DatabaseName = GlobalsModel.LoggedUser.Database.ToString();

                // Pagination Filed
                string pageIndex = string.Empty;

                if (eventName == "Refresh" || eventName == "Search" || eventName == "PageSize" || eventName == "AfterSelect")
                {
                    pageIndex = "1";
                }
                else
                {
                    pageIndex = btnPager.Name.Equals("") ? "1" : btnPager.Name;
                }

                pageList.PageNo = int.Parse(pageIndex);
                pageList.PageSize = pageSizeValue.Equals(0) ? Convert.ToInt32(pageSizeArray.First()) : pageSizeValue;
                pageList.KeyWord = "";
                pageList.SortField = "RegionId";
                pageList.SortOrder = "DESC";

                // TreeView Filed
                pageList.GeoName = TreeStructure.MainTagName;
                pageList.GeoId = TreeStructure.Id;

                // Login User
                pageList.LoggedUserId = GlobalsModel.LoggedUser.User.UserId;

                if (pageList != null)
                {
                    await this.BindGrid(pageList);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Resources.ErrorMessage, "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorLog.Write("AugmentedRollStat", "GridViewSetting", ex);
            }

            GlobalsModel.mainMaster.HideLoader();
        }
        #endregion

        #region Bind Grid
        /// <summary>
        /// Binds the grid.
        /// </summary>
        /// <param name="model">The model.</param>
        private async Task BindGrid(PageList model)
        {
            Response<AugmentedRollStatModel> response = await GenericHttpClient.PostRequest<AugmentedRollStatModel>("AugmentedRollStat/GetAugmentedRollStatList", model);
            if (response.Status)
            {
                if (response.Data.ListItems.ToList() != null && response.Data.ListItems.ToList().Count > 0)
                {
                    foreach (var item in response.Data.ListItems.ToList())
                    {
                        DateTime utcDate = DateTime.SpecifyKind(item.Changedate, DateTimeKind.Utc);
                        DateTime localDate = utcDate.ToLocalTime();
                        item.ChangeStringDate = String.Format("{0:dd-MM-yyyy}", localDate);
                    }

                    BindAugmentedRollStatGird(response.Data.ListItems.ToList());
                    PopulatePager(response.Data.TotalRecordCount, model.PageNo, model.PageSize);
                }
            }
        }
        #endregion

        #region Bind AugmentedRollStat Gird
        /// <summary>
        /// Binds the augmented roll stat gird.
        /// </summary>
        /// <param name="lstAugmentedRollStat">The LST augmented roll stat.</param>
        private void BindAugmentedRollStatGird(List<AugmentedRollStatModel> lstAugmentedRollStat)
        {
            try
            {
                if (this.dgAugmentedRollStat.Columns.Count > 0)
                {
                    this.dgAugmentedRollStat.Columns.Clear();
                }

                // Unvisible Column
                //this.dgFile.DataSource = null;
                this.dgAugmentedRollStat.DataSource = lstAugmentedRollStat;
                this.dgAugmentedRollStat.Columns["RegionId"].Visible = false;
                this.dgAugmentedRollStat.Columns["ProvinceId"].Visible = false;
                this.dgAugmentedRollStat.Columns["DistrictId"].Visible = false;
                this.dgAugmentedRollStat.Columns["LLGId"].Visible = false;
                this.dgAugmentedRollStat.Columns["WardId"].Visible = false;
                this.dgAugmentedRollStat.Columns["LocationId"].Visible = false;
                this.dgAugmentedRollStat.Columns["Changedate"].Visible = false;

                this.dgAugmentedRollStat.Columns["DatabaseName"].Visible = false;
                this.dgAugmentedRollStat.Columns["ResponseCode"].Visible = false;

                this.dgAugmentedRollStat.Columns["RegionName"].HeaderText = "Region";
                this.dgAugmentedRollStat.Columns["ProvinceName"].HeaderText = "Province";
                this.dgAugmentedRollStat.Columns["DistrictName"].HeaderText = "District";
                this.dgAugmentedRollStat.Columns["LLGName"].HeaderText = "LLG";
                this.dgAugmentedRollStat.Columns["WardName"].HeaderText = "Ward";
                this.dgAugmentedRollStat.Columns["LocationName"].Visible = false;
                this.dgAugmentedRollStat.Columns["ChangeStringDate"].HeaderText = "Input Date";
                this.dgAugmentedRollStat.Columns["OLD"].HeaderText = "OLD";
                this.dgAugmentedRollStat.Columns["NEW"].HeaderText = "NEW";
                this.dgAugmentedRollStat.Columns["UPDATED"].HeaderText = "UPDATED";
                this.dgAugmentedRollStat.Columns["TRANSFERIN"].HeaderText = "TRANSFERIN";
                this.dgAugmentedRollStat.Columns["TRANSFEROUT"].HeaderText = "TRANSFEROUT";
                this.dgAugmentedRollStat.Columns["DECEASED"].HeaderText = "DECEASED";
                this.dgAugmentedRollStat.Columns["DUPLICATE"].HeaderText = "DUPLICATE";
                this.dgAugmentedRollStat.Columns["GHOST"].HeaderText = "GHOST";
                this.dgAugmentedRollStat.Columns["UNDERAGE"].HeaderText = "UNDERAGE";
                this.dgAugmentedRollStat.Columns["TOTAL"].HeaderText = "TOTAL";

                #region Edit Button
                DataGridViewButtonColumn editButton = new DataGridViewButtonColumn();
                editButton.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                editButton.FlatStyle = FlatStyle.Standard;
                editButton.HeaderText = "Action";
                editButton.Name = "Edit";
                editButton.Text = "Edit";
                editButton.UseColumnTextForButtonValue = true;
                editButton.Width = 60;
                if (dgAugmentedRollStat.Columns["Edit"] == null)
                {
                    dgAugmentedRollStat.Columns.Insert(dgAugmentedRollStat.ColumnCount, editButton);
                }
                #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show(Resources.ResourceManager.GetString("ErrorMessage"), "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorLog.Write("AugmentedRollStat", "BindAugmentedRollStatGird", ex);
            }
        }
        #endregion

        #region Populate Pager
        /// <summary>
        /// Populates the pager.
        /// </summary>
        /// <param name="recordCount">The record count.</param>
        /// <param name="currentPage">The current page.</param>
        private void PopulatePager(int recordCount, int currentPage, int pageSize)
        {
            List<Page> pages = new List<Page>();
            int startIndex, endIndex;
            int pagerSpan = 5;

            //Calculate the Start and End Index of pages to be displayed.
            double dblPageCount = (double)(recordCount / Convert.ToDecimal(pageList.PageSize));
            int pageCount = (int)Math.Ceiling(dblPageCount);
            startIndex = currentPage > 1 && currentPage + pagerSpan - 1 < pagerSpan ? currentPage : 1;
            endIndex = pageCount > pagerSpan ? pagerSpan : pageCount;
            if (currentPage > pagerSpan % 2)
            {
                if (currentPage == 2)
                {
                    endIndex = 5;
                }
                else
                {
                    endIndex = currentPage + 2;
                }
            }
            else
            {
                endIndex = (pagerSpan - currentPage) + 1;
            }

            if (endIndex - (pagerSpan - 1) > startIndex)
            {
                startIndex = endIndex - (pagerSpan - 1);
            }

            if (endIndex > pageCount)
            {
                endIndex = pageCount;
                startIndex = ((endIndex - pagerSpan) + 1) > 0 ? (endIndex - pagerSpan) + 1 : 1;
            }

            //Add the First Page Button.
            if (currentPage > 1)
            {
                //var bitmap = new Bitmap(global::PNGEC.Desktop.Properties.Resources.first);
                pages.Add(new Page { Text = "First", Value = "1" });
            }

            //Add the Previous Button.
            if (currentPage > 5)
            {
                pages.Add(new Page { Text = "<<", Value = (currentPage - 5).ToString() });
            }

            if (currentPage > 1)
            {
                pages.Add(new Page { Text = "<", Value = (currentPage - 1).ToString() });
            }

            for (int i = startIndex; i <= endIndex; i++)
            {
                pages.Add(new Page { Text = i.ToString(), Value = i.ToString(), Selected = i == currentPage });
            }

            //Add the Next Button.
            if (currentPage < pageCount)
            {
                pages.Add(new Page { Text = ">", Value = (currentPage + 1).ToString() });
            }

            if (currentPage < pageCount)
            {
                pages.Add(new Page { Text = ">>", Value = (currentPage + 5).ToString() });
            }

            //Add the Last Button.
            if (currentPage != pageCount)
            {
                pages.Add(new Page { Text = "Last", Value = pageCount.ToString() });
            }

            //Clear existing Pager Buttons.
            pnlPaginationLeft.Controls.Clear();

            if (dgAugmentedRollStat != null && dgAugmentedRollStat.Rows.Count > 0)
            {
                //Loop and add Buttons for Pager.
                int count = 0;
                foreach (Page page in pages)
                {
                    Button btnPage = new Button();
                    btnPage.Location = new System.Drawing.Point(65 * count, 5);
                    btnPage.Size = new System.Drawing.Size(65, 30);
                    btnPage.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
                    btnPage.Name = page.Value;
                    btnPage.Text = page.Text;
                    btnPage.Enabled = !page.Selected;
                    btnPage.Click += new System.EventHandler(this.Page_Click);
                    pnlPaginationLeft.Controls.Add(btnPage);
                    count++;
                }
                this.cmbPageSize.SelectedIndexChanged -= this.CmbPageSize_SelectedIndexChanged;
                cmbPageSize.DataSource = pageSizeArray;
                pnlPaginationRight.Visible = true;
                this.cmbPageSize.SelectedIndexChanged += this.CmbPageSize_SelectedIndexChanged;
            }
        }
        #endregion

        #region Get DataTable FromDGV
        /// <summary>
        /// Gets the data table from DGV.
        /// </summary>
        /// <param name="dgv">The DGV.</param>
        /// <returns></returns>
        private DataTable GetDataTableFromDGV(DataGridView dgv)
        {
            var dt = new DataTable();
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                if (column.HeaderText == "Input Date")
                {
                    column.HeaderText = "InputDate";
                }
                dt.Columns.Add(column.HeaderText);
            }

            object[] cellValues = new object[dgv.Columns.Count];
            foreach (DataGridViewRow row in dgv.Rows)
            {
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    cellValues[i] = row.Cells[i].Value;
                }
                dt.Rows.Add(cellValues);
            }

            if (dt != null && dt.Rows.Count > 0)
            {
                string[] ColumnsToBeDeleted = {"Action", "RegionId", "ProvinceId", "DistrictId", "LLGId", "WardId", "LocationId", "Changedate",
                "DatabaseName", "ResponseCode", "LocationName" };
                foreach (string ColName in ColumnsToBeDeleted)
                {
                    if (dt.Columns.Contains(ColName))
                    {
                        dt.Columns.Remove(ColName);
                    }
                }
            }

            return dt;
        }
        #endregion

        #region Export Report Source   
        /// <summary>
        /// Exports the report source.
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <returns></returns>
        private byte[] ExportReportSource(DataTable dt)
        {
            byte[] bytes = null;

            ds = new DataSet();
            dt.TableName = "tblAugmentedRollStat";

            if (!ds.Tables.Contains(dt.TableName))
            {
                ds.Tables.Add(dt);
            }

            if (ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
            {
                Warning[] warnings;
                string[] streamIds;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;

                ReportDataSource dataSource = new ReportDataSource("ds_AugmentedRollStat", ds.Tables[0]);
                ReportViewer viewer = new ReportViewer();
                viewer.LocalReport.Refresh();
                viewer.LocalReport.ReportPath = "Report/AugmentedRollStat.rdlc"; //This is your rdlc name.
                viewer.LocalReport.DataSources.Add(dataSource); // Add  datasource here         
                bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
            }

            return bytes;
        }
        #endregion

        #endregion

    }
}
