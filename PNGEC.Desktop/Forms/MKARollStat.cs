using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Domain.ScheduleLocSummary;
using PNGEC.API.Helper;
using PNGEC.Desktop.Model;
using PNGEC.Desktop.Properties;

namespace PNGEC.Desktop.Forms
{
    public partial class MKARollStat : Form
    {
        #region variable declaration
        /// <summary>
        /// The geo tree structure
        /// </summary>
        private MKAGeoTreeStructure TreeStructure = null;

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

        /// <summary>
        /// Gets or sets the name of the report.
        /// </summary>
        /// <value>
        /// The name of the report.
        /// </value>
        public string ReportName { get; set; }
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="MKARollStat"/> class.
        /// </summary>
        public MKARollStat(string pageName)
        {
            InitializeComponent();
            if (pageName == "LatestRollState")
            {
                this.pnlControl.Visible = false;
            }
            else
            {
                this.pnlControl.Visible = true;
            }
        }
        #endregion

        #region Events

        /// <summary>
        /// Handles the Load event of the MKARollStat control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void MKARollStat_Load(object sender, EventArgs e)
        {
            dtpFromDate.MaxDate = DateTime.Now.Date;
            dtpToDate.MaxDate = DateTime.Now.Date;

            GlobalsModel.mainMaster.TreeViewSelectedChanged -= TreeViewSelectedChanged_AfterSelect;
            GlobalsModel.mainMaster.TreeViewSelectedChanged += TreeViewSelectedChanged_AfterSelect;

            if (GlobalsModel.MKATreeViewStructure != null)
            {
                LoadGridViewSetting();
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
            TreeStructure = GlobalsModel.MKATreeViewStructure;
            await GridViewSetting("AfterSelect");
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
        /// Handles the Click event of the btnSearch control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void btnSearch_Click(object sender, EventArgs e)
        {
            if (dtpFromDate.Value == null)
            {
                MessageBox.Show("To date is not be empty", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if ((dtpFromDate.Value != null && dtpToDate.Value != null) && (dtpFromDate.Value > dtpToDate.Value))
            {
                MessageBox.Show("The To date must be greater than the From date.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (dtpFromDate.Value != null)
            {
                await GridViewSetting("Search");
            }
            else if (dtpFromDate.Value != null && (dtpFromDate.Value > dtpToDate.Value))
            {
                await GridViewSetting("Search");
            }
        }

        /// <summary>
        /// Handles the Click event of the btnRefresh control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await GridViewSetting("Refresh");
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
                if (dgMKARollStat != null && dgMKARollStat.Rows.Count > 0)
                {
                    var saveFileDialog = new SaveFileDialog();
                    saveFileDialog.FileName = "MKAScheduleLocationSummary";
                    saveFileDialog.DefaultExt = ".csv";
                    saveFileDialog.Filter = "CSV (*.csv)|*.csv";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        GlobalsModel.mainMaster.ShowLoader();

                        DataTable data = GetDataTableFromDGV(dgMKARollStat);
                        //byte[] bytes = ExportReportSource(data);

                        //using (FileStream stream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                        //{
                        //    stream.Write(bytes, 0, bytes.Length);
                        //}

                        #region write
                        StreamWriter sw = new StreamWriter(saveFileDialog.FileName, false);
                        //headers    
                        for (int i = 0; i < data.Columns.Count; i++)
                        {
                            sw.Write(data.Columns[i]);
                            if (i < data.Columns.Count - 1)
                            {
                                sw.Write(",");
                            }
                        }
                        sw.Write(sw.NewLine);
                        //data
                        foreach (DataRow dr in data.Rows)
                        {
                            for (int i = 0; i < data.Columns.Count; i++)
                            {
                                if (!Convert.IsDBNull(dr[i]))
                                {
                                    string value = dr[i].ToString();
                                    if (value.Contains(','))
                                    {
                                        value = String.Format("\"{0}\"", value);
                                        sw.Write(value);
                                    }
                                    else
                                    {
                                        sw.Write(dr[i].ToString());
                                    }
                                }
                                if (i < data.Columns.Count - 1)
                                {
                                    sw.Write(",");
                                }
                            }
                            sw.Write(sw.NewLine);
                        }
                        sw.Close();
                        #endregion


                        messageBoxText = string.Format("MKA Roll stat is exported sucessfully at {0}", saveFileDialog.FileName);
                        MessageBox.Show(messageBoxText, "MKA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("No data", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    messageBoxText = string.Empty;
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message, "MKA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Resources.ResourceManager.GetString("ErrorMessage"), "MKA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorLog.Write("MKARollStat", "btnExport_Click", ex);
            }
            finally
            {
                GlobalsModel.mainMaster.HideLoader();
                messageBoxText = string.Empty;
            }
        }

        /// <summary>
        /// Handles the FormClosing event of the FormClosing control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
        private void MKARollStat_FormClosing(object sender, FormClosingEventArgs e)
        {
            GlobalsModel.mainMaster.TreeViewSelectedChanged -= TreeViewSelectedChanged_AfterSelect;
        }

        /// <summary>
        /// Handles the RowPrePaint event of the dgMKARollStat control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewRowPrePaintEventArgs"/> instance containing the event data.</param>
        private void dgMKARollStat_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            //dgMKARollStat.Columns["TRANSFEROUT"].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#FEB6B7");
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
            TreeStructure = GlobalsModel.MKATreeViewStructure;
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
                pageList.SortField = "MKADistrictId";
                pageList.SortOrder = "DESC";

                // Filter Filed
                if (dtpFromDate.Value != null && dtpFromDate.Value == null)
                {
                    //pageList.FromDate = dtpFromDate.Value.ToString("yyyy-MM-dd");
                    //pageList.ToDate = DateTime.Now.ToString("yyyy-MM-dd");

                    TimeSpan FromTime = new TimeSpan(00, 00, 00);
                    var FromDate = dtpFromDate.Value.Date + FromTime;
                    pageList.DateFrom = FromDate;

                    TimeSpan ToTime = new TimeSpan(23, 59, 59);
                    var ToDate = DateTime.Now.Date + ToTime;
                    pageList.DateTo = ToDate;
                }
                else if (dtpFromDate.Value != null && dtpToDate.Value != null)
                {
                    //pageList.FromDate = dtpFromDate.Value.ToString("yyyy-MM-dd");
                    //pageList.ToDate = dtpToDate.Value.ToString("yyyy-MM-dd");

                    TimeSpan FromTime = new TimeSpan(00, 00, 00);
                    var FromDate = dtpFromDate.Value.Date + FromTime;
                    pageList.DateFrom = FromDate;

                    TimeSpan ToTime = new TimeSpan(23, 59, 59);
                    var ToDate = dtpToDate.Value.Date + ToTime;
                    pageList.DateTo = ToDate;
                }

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
                ErrorLog.Write("MKARollStat", "GridViewSetting", ex);
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
            Response<MKAScheduleLocSummaryModel> mkaresponse = await GenericHttpClient.PostRequest<MKAScheduleLocSummaryModel>("ScheduleLocSummary/GetMKARollStatList", model);
            if (mkaresponse.Status)
            {
                if (mkaresponse.Data.ListItems.ToList() != null && mkaresponse.Data.ListItems.ToList().Count > 0)
                {
                    foreach (var item in mkaresponse.Data.ListItems.ToList())
                    {
                        DateTime utcDate = DateTime.SpecifyKind(item.Scheduledate, DateTimeKind.Utc);
                        DateTime localDate = utcDate.ToLocalTime();
                        item.ScheduleStringDate = String.Format("{0:dd-MM-yyyy}", localDate);
                    }
                }

                BindScheduleLocSummaryGird(mkaresponse.Data.ListItems.ToList());
                PopulatePager(mkaresponse.Data.TotalRecordCount, model.PageNo, model.PageSize);
            }
        }
        #endregion

        #region Bind ScheduleLocSummary Gird
        /// <summary>
        /// Binds the schedule loc summary gird.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <exception cref="NotImplementedException"></exception>
        private void BindScheduleLocSummaryGird(List<MKAScheduleLocSummaryModel> lstScheduleLocSummary)
        {
            try
            {
                if (this.dgMKARollStat.Columns.Count > 0)
                {
                    this.dgMKARollStat.Columns.Clear();
                }

                //this.dgFile.DataSource = null;
                this.dgMKARollStat.DataSource = lstScheduleLocSummary;
                this.dgMKARollStat.Columns["MKADistrictId"].Visible = false;
                this.dgMKARollStat.Columns["MKALLGId"].Visible = false;
                this.dgMKARollStat.Columns["MKAWardId"].Visible = false;
                this.dgMKARollStat.Columns["MKALocationId"].Visible = false;
                this.dgMKARollStat.Columns["MKAClanId"].Visible = false;
                this.dgMKARollStat.Columns["Scheduledate"].Visible = false;
                this.dgMKARollStat.Columns["ScheduleStringDate"].Visible = false;

                if (TreeStructure.MainTagName == "MKAGeo")
                {
                    this.dgMKARollStat.Columns["MKADistrictName"].HeaderText = "MKA District";
                    this.dgMKARollStat.Columns["MKALLGName"].Visible = false;
                    this.dgMKARollStat.Columns["MKAWardName"].Visible = false;
                    this.dgMKARollStat.Columns["MKALocationName"].Visible = false;
                    this.dgMKARollStat.Columns["MKAClanName"].Visible = false;
                    this.dgMKARollStat.Columns["Scheduledate"].Visible = false;
                    this.ReportName = "MKAGeo";
                }
                else if (TreeStructure.MainTagName == "MKADistrict")
                {
                    this.dgMKARollStat.Columns["MKADistrictName"].HeaderText = "MKA District";
                    this.dgMKARollStat.Columns["MKALLGName"].Visible = false;
                    this.dgMKARollStat.Columns["MKAWardName"].Visible = false;
                    this.dgMKARollStat.Columns["MKALocationName"].Visible = false;
                    this.dgMKARollStat.Columns["MKAClanName"].Visible = false;
                    this.dgMKARollStat.Columns["Scheduledate"].Visible = false;
                    this.ReportName = "MKADistrict";
                }
                else if (TreeStructure.MainTagName == "MKALLG")
                {
                    this.dgMKARollStat.Columns["MKADistrictName"].HeaderText = "MKA District";
                    this.dgMKARollStat.Columns["MKALLGName"].HeaderText = "MKA LLG";                     
                    this.dgMKARollStat.Columns["MKAWardName"].Visible = false;
                    this.dgMKARollStat.Columns["MKALocationName"].Visible = false;
                    this.dgMKARollStat.Columns["MKAClanName"].Visible = false;
                    this.dgMKARollStat.Columns["Scheduledate"].Visible = false;
                    this.ReportName = "MKALLG";

                }
                else if (TreeStructure.MainTagName == "MKAWard")
                {
                    this.dgMKARollStat.Columns["MKAWardName"].HeaderText = "MKA Ward";
                    this.dgMKARollStat.Columns["MKADistrictName"].HeaderText = "MKA District";
                    this.dgMKARollStat.Columns["MKALLGName"].HeaderText = "MKA LLG";
                    this.dgMKARollStat.Columns["MKALocationName"].Visible = false;
                    this.dgMKARollStat.Columns["MKAClanName"].Visible = false;
                    this.dgMKARollStat.Columns["Scheduledate"].Visible = false;
                    this.ReportName = "MKAWard";

                }
                else if (TreeStructure.MainTagName == "MKALocation")
                {
                    this.dgMKARollStat.Columns["MKALocationName"].HeaderText = "MKA Location";
                    this.dgMKARollStat.Columns["MKAWardName"].HeaderText = "MKA Ward";
                    this.dgMKARollStat.Columns["MKADistrictName"].HeaderText = "MKA District";
                    this.dgMKARollStat.Columns["MKALLGName"].HeaderText = "MKA LLG";
                    this.dgMKARollStat.Columns["MKAClanName"].Visible = false;
                    this.dgMKARollStat.Columns["Scheduledate"].Visible = false;
                    this.ReportName = "MKALocation";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Resources.ResourceManager.GetString("ErrorMessage"), "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorLog.Write("MKARollStat", "BindScheduleLocSummaryGird", ex);
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

            if (dgMKARollStat != null && dgMKARollStat.Rows.Count > 0)
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

        #region ExportGridToPDF
        /////// <summary>
        /////// Exports the grid to PDF.
        /////// </summary>
        /////// <param name="dataGridView">The data grid view.</param>
        /////// <param name="fileName">Name of the file.</param>
        ////public void ExportGridToPDF(DataGridView dataGridView, string fileName)
        ////{
        ////    BaseFont baseFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, BaseFont.EMBEDDED);
        ////    PdfPTable pdfTable = new PdfPTable(17);
        ////    pdfTable.DefaultCell.Padding = 3;
        ////    pdfTable.WidthPercentage = 100;
        ////    pdfTable.HorizontalAlignment = Element.ALIGN_CENTER;
        ////    pdfTable.DefaultCell.BorderWidth = 0.1f;
        ////    pdfTable.HeaderRows = 1;
        ////    iTextSharp.text.Font text = new iTextSharp.text.Font(baseFont, 8, iTextSharp.text.Font.NORMAL);


        ////    foreach (DataGridViewColumn column in dataGridView.Columns)
        ////    {
        ////        if (column.HeaderText == "Schedule" ||
        ////            column.HeaderText == "Region" ||
        ////            column.HeaderText == "Province" ||
        ////            column.HeaderText == "District" ||
        ////            column.HeaderText == "LLG" ||
        ////            column.HeaderText == "Ward" ||
        ////            column.HeaderText == "Location" ||
        ////            column.HeaderText == "Previous Total" ||
        ////            column.HeaderText == "OLD" ||
        ////            column.HeaderText == "NEW" ||
        ////            column.HeaderText == "UPDATED" ||
        ////            column.HeaderText == "TRANSFERIN" ||
        ////            column.HeaderText == "TRANSFEROUT" ||
        ////            column.HeaderText == "DECEASED" ||
        ////            column.HeaderText == "DUPLICATE" ||
        ////            column.HeaderText == "GHOST" ||
        ////             column.HeaderText == "TOTAL")
        ////        {
        ////            string columnHeaderName = ColumnHeaderName(column.HeaderText);
        ////            iTextSharp.text.Font CoumnHeadertext = new iTextSharp.text.Font(baseFont, 8, iTextSharp.text.Font.NORMAL);
        ////            //iTextSharp.text.Font CoumnHeadertext = new iTextSharp.text.Font(baseFont, 12, iTextSharp.text.Font.BOLD);
        ////            PdfPCell cell = new PdfPCell(new Phrase(columnHeaderName, CoumnHeadertext));
        ////            cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240);
        ////            pdfTable.AddCell(cell);
        ////        }
        ////    }

        ////    foreach (DataGridViewRow row in dataGridView.Rows)
        ////    {
        ////        foreach (DataGridViewCell cell in row.Cells)
        ////        {
        ////            if ((cell.ColumnIndex >= 7 && cell.ColumnIndex <= 23))
        ////            {
        ////                pdfTable.AddCell(new Phrase(cell.Value == null ? string.Empty : cell.Value.ToString(), text));
        ////            }
        ////        }
        ////    }

        ////    var saveFileDialog = new SaveFileDialog();
        ////    saveFileDialog.FileName = string.Format(@"{0}_{1}", fileName, Guid.NewGuid());
        ////    saveFileDialog.DefaultExt = ".pdf";
        ////    if (saveFileDialog.ShowDialog() == DialogResult.OK)
        ////    {
        ////        using (FileStream stream = new FileStream(saveFileDialog.FileName, FileMode.Create))
        ////        {
        ////            Document pdfDoc = new Document(PageSize.A4, 10, 10, 80, 80);
        ////            PdfWriter wri = PdfWriter.GetInstance(pdfDoc, stream);
        ////            wri.PageEvent = new ITextEvents("Schedule Location Summary", TreeStructure.MainTagName, TreeStructure.Name);
        ////            pdfDoc.Open();
        ////            pdfDoc.Add(pdfTable);
        ////            pdfDoc.Close();
        ////            stream.Close();
        ////        }
        ////    }
        ////}
        #endregion

        #region Column Header Name
        /// <summary>
        /// Columns the name of the header.
        /// </summary>
        /// <param name="headerText">The header text.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        ////private string ColumnHeaderName(string headerText)
        ////{
        ////    string headerName = string.Empty;

        ////    if (headerText == "Region")
        ////    {
        ////        headerName = "Region";
        ////    }
        ////    else if (headerText == "Province")
        ////    {
        ////        headerName = "Province";
        ////    }
        ////    else if (headerText == "District")
        ////    {
        ////        headerName = "District";
        ////    }
        ////    else if (headerText == "LLG")
        ////    {
        ////        headerName = "LLG";
        ////    }
        ////    else if (headerText == "Ward")
        ////    {
        ////        headerName = "Ward";
        ////    }
        ////    else if (headerText == "Location")
        ////    {
        ////        headerName = "Location";
        ////    }
        ////    else if (headerText == "Schedule")
        ////    {
        ////        headerName = "Date";
        ////    }
        ////    else if (headerText == "Previous Total")
        ////    {
        ////        headerName = "PTotal";
        ////    }
        ////    else if (headerText == "OLD")
        ////    {
        ////        headerName = "OLD";
        ////    }
        ////    else if (headerText == "NEW")
        ////    {
        ////        headerName = "NEW";
        ////    }
        ////    else if (headerText == "UPDATED")
        ////    {
        ////        headerName = "UPD";
        ////    }
        ////    else if (headerText == "TRANSFERIN")
        ////    {
        ////        headerName = "TIN";
        ////    }
        ////    else if (headerText == "TRANSFEROUT")
        ////    {
        ////        headerName = "TOUT";
        ////    }
        ////    else if (headerText == "DECEASED")
        ////    {
        ////        headerName = "DEC";
        ////    }
        ////    else if (headerText == "DUPLICATE")
        ////    {
        ////        headerName = "DUP";
        ////    }
        ////    else if (headerText == "GHOST")
        ////    {
        ////        headerName = "GHO";
        ////    }
        ////    else if (headerText == "UNDERAGE")
        ////    {
        ////        headerName = "UAGE";
        ////    }
        ////    else if (headerText == "TOTAL")
        ////    {
        ////        headerName = "TOT";
        ////    }
        ////    return headerName;
        ////}        ////private string ColumnHeaderName(string headerText)
        ////{
        ////    string headerName = string.Empty;

        ////    if (headerText == "Region")
        ////    {
        ////        headerName = "Region";
        ////    }
        ////    else if (headerText == "Province")
        ////    {
        ////        headerName = "Province";
        ////    }
        ////    else if (headerText == "District")
        ////    {
        ////        headerName = "District";
        ////    }
        ////    else if (headerText == "LLG")
        ////    {
        ////        headerName = "LLG";
        ////    }
        ////    else if (headerText == "Ward")
        ////    {
        ////        headerName = "Ward";
        ////    }
        ////    else if (headerText == "Location")
        ////    {
        ////        headerName = "Location";
        ////    }
        ////    else if (headerText == "Schedule")
        ////    {
        ////        headerName = "Date";
        ////    }
        ////    else if (headerText == "Previous Total")
        ////    {
        ////        headerName = "PTotal";
        ////    }
        ////    else if (headerText == "OLD")
        ////    {
        ////        headerName = "OLD";
        ////    }
        ////    else if (headerText == "NEW")
        ////    {
        ////        headerName = "NEW";
        ////    }
        ////    else if (headerText == "UPDATED")
        ////    {
        ////        headerName = "UPD";
        ////    }
        ////    else if (headerText == "TRANSFERIN")
        ////    {
        ////        headerName = "TIN";
        ////    }
        ////    else if (headerText == "TRANSFEROUT")
        ////    {
        ////        headerName = "TOUT";
        ////    }
        ////    else if (headerText == "DECEASED")
        ////    {
        ////        headerName = "DEC";
        ////    }
        ////    else if (headerText == "DUPLICATE")
        ////    {
        ////        headerName = "DUP";
        ////    }
        ////    else if (headerText == "GHOST")
        ////    {
        ////        headerName = "GHO";
        ////    }
        ////    else if (headerText == "UNDERAGE")
        ////    {
        ////        headerName = "UAGE";
        ////    }
        ////    else if (headerText == "TOTAL")
        ////    {
        ////        headerName = "TOT";
        ////    }
        ////    return headerName;
        ////}
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
                if (column.HeaderText == "INIT")
                {
                    //column.HeaderText = "PreviousTotal";
                    dt.Columns.Add("Total Count");
                }
                else if (column.HeaderText == "Cur RT")
                {
                    //column.HeaderText = "Total";
                    dt.Columns.Add("Total");
                }
                else if (column.HeaderText == "TRANSFER IN")
                {
                    dt.Columns.Add("TRANSFERIN");
                }
                else if (column.HeaderText == "TRANSFER OUT")
                {
                    dt.Columns.Add("TRANSFEROUT");
                }
                else if (column.HeaderText == "UNDER AGE")
                {
                    dt.Columns.Add("UNDERAGE");
                }
                else
                {
                    dt.Columns.Add(column.HeaderText);
                }
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
                string[] ColumnsToBeDeleted = { };

                //Hide columns based on selection
                if(TreeStructure.MainTagName == "MKAGeo" || TreeStructure.MainTagName == "MKADistrict")
                    ColumnsToBeDeleted = new string[] { "MKADistrictId", "MKALLGId", "MKAWardId", "MKALocationId", "MKAClanId", "MKALLGName", "MKALocationName", "MKAWardName", "MKAClanName","ScheduleStringDate", "ScheduleDate", "UserEmails" };
                else if (TreeStructure.MainTagName == "MKALLG")
                    ColumnsToBeDeleted = new string[] { "MKADistrictId", "MKALLGId", "MKAWardId", "MKALocationId", "MKAClanId", "MKALocationName", "MKAWardName", "MKAClanName", "ScheduleStringDate", "ScheduleDate", "UserEmails" };
                else if (TreeStructure.MainTagName == "MKAWard")
                    ColumnsToBeDeleted = new string[] { "MKADistrictId", "MKALLGId", "MKAWardId", "MKALocationId", "MKAClanId", "MKALocationName", "MKAClanName", "ScheduleStringDate", "ScheduleDate", "UserEmails" };
                else 
                    ColumnsToBeDeleted = new string[] { "MKADistrictId", "MKALLGId", "MKAWardId", "MKALocationId", "MKAClanId","MKAClanName", "ScheduleStringDate", "ScheduleDate", "UserEmails" };


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
            dt.TableName = "tblScheduleLocationSummary";

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

                int paramCount = 1;

                //This is optional if you have parameter then you can add parameters as much as you want
                ReportParameter[] param = new ReportParameter[paramCount];
                param[0] = new ReportParameter("ReportName", this.ReportName, true);

                ReportDataSource dataSource = new ReportDataSource("ds_ScheduleLocationSummary", ds.Tables[0]);
                ReportViewer viewer = new ReportViewer();
                viewer.LocalReport.Refresh();
                viewer.LocalReport.ReportPath = "Report/ScheduleLocationSummary.rdlc"; //This is your rdlc name.
                viewer.LocalReport.SetParameters(param);
                viewer.LocalReport.DataSources.Add(dataSource); // Add  datasource here         
                bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
            }

            return bytes;
        }
        #endregion

        #endregion
    }
}
