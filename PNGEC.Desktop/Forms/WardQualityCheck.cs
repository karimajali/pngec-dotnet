using Microsoft.AspNetCore.Http;
using PNGEC.API.Domain.EnrolmentForm;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Helper;
using PNGEC.Desktop.Model;
using PNGEC.Desktop.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PNGEC.Desktop.Forms
{
    public partial class WardQualityCheck : Form
    {
        #region Variable Declaration
        /// <summary>
        /// The page list
        /// </summary>
        private PageList pageList = null;

        /// <summary>
        /// The BTN pager
        /// </summary>
        private Button btnPager = new Button();

        /// <summary>
        /// The geo tree structure
        /// </summary>
        private GeoTreeStructure TreeStructure = null;

        /// <summary>
        /// The page size value
        /// </summary>
        private int pageSizeValue;

        /// <summary>
        /// The page size array
        /// </summary>
        private string[] pageSizeArray = { "15", "50", "100", "500" };

        /// <summary>
        /// The page name
        /// </summary>
        private string pageName = string.Empty;

        /// <summary>
        /// The list of enrols
        /// </summary>
        private List<EnrolmentFormModel> ListOfEnrols = new List<EnrolmentFormModel>();

        /// <summary>
        /// The LST ward quality check response
        /// </summary>
        private List<WardQualityCheckResponseModel> lstWardQualityCheckResponse;

        /// <summary>
        /// Gets or sets the region.
        /// </summary>
        /// <value>
        /// The region.
        /// </value>
        private API.Domain.Region.RegionModel Region { get; set; }

        /// <summary>
        /// Gets or sets the province.
        /// </summary>
        /// <value>
        /// The province.
        /// </value>
        private API.Domain.Province.ProvinceModel Province { get; set; }

        /// <summary>
        /// Gets or sets the district.
        /// </summary>
        /// <value>
        /// The district.
        /// </value>
        private API.Domain.District.DistrictModel District { get; set; }

        /// <summary>
        /// Gets or sets the LLG.
        /// </summary>
        /// <value>
        /// The LLG.
        /// </value>
        private API.Domain.LLG.LLGModel LLG { get; set; }

        /// <summary>
        /// Gets or sets the ward.
        /// </summary>
        /// <value>
        /// The ward.
        /// </value>
        private API.Domain.Ward.WardModel Ward { get; set; }

        /// <summary>
        /// The enrolment form model
        /// </summary>
        private EnrolmentFormModel enrolmentFormModel;

        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="EnrolmentForm"/> class.
        /// </summary>
        public WardQualityCheck(string pageName)
        {
            InitializeComponent();
            this.pageName = pageName;
        }
        #endregion

        #region Events

        /// <summary>
        /// Handles the Load event of the EnrolmentForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void EnrolmentForm_Load(object sender, EventArgs e)
        {
            GlobalsModel.mainMaster.TreeViewSelectedChanged -= TreeViewSelectedChanged_AfterSelect;
            GlobalsModel.mainMaster.TreeViewSelectedChanged += TreeViewSelectedChanged_AfterSelect;

            if (GlobalsModel.TreeViewStructure != null)
            {
                await LoadGridViewSetting();
            }
            //RefreshData();
        }

        /// <summary>
        /// Handles the Click event of the btnRefresh control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            //RefreshData();
            txtSearch.Text = string.Empty;
            await GridViewSetting("Refresh");
        }

        /// <summary>
        /// Handles the Click event of the btnSearch control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void btnSearch_Click(object sender, EventArgs e)
        {
            await GridViewSetting("Search");
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
        /// Handles the AfterSelect event of the TreeViewSelectedChanged control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        public async void TreeViewSelectedChanged_AfterSelect(object sender, EventArgs e)
        {
            await LoadGridViewSetting();
        }

        /// <summary>
        /// Handles the FormClosing event of the EnrolmentForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
        private void EnrolmentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            GlobalsModel.mainMaster.TreeViewSelectedChanged -= TreeViewSelectedChanged_AfterSelect;
        }

        /// <summary>
        /// Handles the CellClick event of the dgEnrolmentForms control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private async void dgEnrolmentForms_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgEnrolmentForms.Columns[e.ColumnIndex].Name == "View")
            {
                EnrolmentFormModel enrolmentForm = GetGridCellData(e.RowIndex);

                if (enrolmentForm?.EnrolmentFormId > 0)
                {
                    List<WardQualityCheckResponseModel> lstwardQualityCheck = await GetWardQualityCheckList(enrolmentForm);
                    if (lstWardQualityCheckResponse?.Count > 0)
                    {
                        MissingForm11Number missingForm11Number = new MissingForm11Number(enrolmentForm, lstwardQualityCheck, TreeStructure);
                        if (missingForm11Number.ShowDialog() == DialogResult.OK)
                        {
                            //----
                        }
                    }
                    else
                    {
                        MessageBox.Show(ResponseMessage.DataNotFound, "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show(Resources.ErrorMessage, "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ErrorLog.WriteLog("WardQualityCheck => dgEnrolmentForms_CellClick", $"selected row enrolment form id is invalid, enrolment form id is {enrolmentForm?.EnrolmentFormId}");
                }
            }
        }
        #endregion

        #region Methods

        #region Get Grid Cell Data
        /// <summary>
        /// Gets the grid cell data.
        /// </summary>
        private EnrolmentFormModel GetGridCellData(int rowIndex)
        {
            enrolmentFormModel = new EnrolmentFormModel();

            enrolmentFormModel.EnrolmentFormId = Convert.ToInt32(dgEnrolmentForms.Rows[rowIndex].Cells["EnrolmentFormId"].Value);

            enrolmentFormModel.UserId = Convert.ToInt32(dgEnrolmentForms.Rows[rowIndex].Cells["UserId"].Value);
            enrolmentFormModel.EmailAddress = Convert.ToString(dgEnrolmentForms.Rows[rowIndex].Cells["EmailAddress"].Value);

            enrolmentFormModel.RegionId = Convert.ToInt32(dgEnrolmentForms.Rows[rowIndex].Cells["RegionId"].Value);
            enrolmentFormModel.ProvinceId = Convert.ToInt32(dgEnrolmentForms.Rows[rowIndex].Cells["ProvinceId"].Value);
            enrolmentFormModel.DistrictId = Convert.ToInt32(dgEnrolmentForms.Rows[rowIndex].Cells["DistrictId"].Value);
            enrolmentFormModel.LLGId = Convert.ToInt32(dgEnrolmentForms.Rows[rowIndex].Cells["LLGId"].Value);
            enrolmentFormModel.WardId = Convert.ToInt32(dgEnrolmentForms.Rows[rowIndex].Cells["WardId"].Value);

            enrolmentFormModel.UsedFromRegistration = Convert.ToInt32(dgEnrolmentForms.Rows[rowIndex].Cells["UsedFromRegistration"].Value);
            enrolmentFormModel.UsedFromTransferIn = Convert.ToInt32(dgEnrolmentForms.Rows[rowIndex].Cells["UsedFromTransferIn"].Value);
            enrolmentFormModel.AttachmnetToUpoad = new List<IFormFile>();
            enrolmentFormModel.Attachment = Convert.ToString(dgEnrolmentForms.Rows[rowIndex].Cells["Attachment"].Value);
            enrolmentFormModel.AttachmentUrlList = new List<AttachmnetModel>();
            enrolmentFormModel.Comment = Convert.ToString(dgEnrolmentForms.Rows[rowIndex].Cells["Comment"].Value);

            enrolmentFormModel.RegionName = Convert.ToString(dgEnrolmentForms.Rows[rowIndex].Cells["RegionName"].Value);
            enrolmentFormModel.ProvinceName = Convert.ToString(dgEnrolmentForms.Rows[rowIndex].Cells["ProvinceName"].Value);
            enrolmentFormModel.DistrictName = Convert.ToString(dgEnrolmentForms.Rows[rowIndex].Cells["DistrictName"].Value);
            enrolmentFormModel.LLGName = Convert.ToString(dgEnrolmentForms.Rows[rowIndex].Cells["LLGName"].Value);
            enrolmentFormModel.WardName = Convert.ToString(dgEnrolmentForms.Rows[rowIndex].Cells["WardName"].Value);

            enrolmentFormModel.BatchName = Convert.ToString(dgEnrolmentForms.Rows[rowIndex].Cells["BatchName"].Value);
            enrolmentFormModel.NumberOfBatch = Convert.ToInt32(dgEnrolmentForms.Rows[rowIndex].Cells["NumberOfBatch"].Value);
            enrolmentFormModel.NumberOfForms = Convert.ToInt32(dgEnrolmentForms.Rows[rowIndex].Cells["NumberOfForms"].Value);
            enrolmentFormModel.StartRange = Convert.ToInt32(dgEnrolmentForms.Rows[rowIndex].Cells["StartRange"].Value);
            enrolmentFormModel.EndRange = Convert.ToInt32(dgEnrolmentForms.Rows[rowIndex].Cells["EndRange"].Value);

            enrolmentFormModel.ResponseCode = Convert.ToInt32(dgEnrolmentForms.Rows[rowIndex].Cells["ResponseCode"].Value);
            enrolmentFormModel.CreatedDate = Convert.ToDateTime(dgEnrolmentForms.Rows[rowIndex].Cells["CreatedDate"].Value);


            return enrolmentFormModel;
        }
        #endregion

        #region Load Grid View Setting
        /// <summary>
        /// Loads the grid view setting.
        /// </summary>
        private async Task LoadGridViewSetting()
        {
            TreeStructure = GlobalsModel.TreeViewStructure;

            // isLocation
            if (TreeStructure.MainTagName == "Ward" || TreeStructure.MainTagName == "Location")
            {
                if (TreeStructure.MainTagName == "Ward")
                {
                    this.Ward = Globals.GeoList.WardList.Where(x => x.Status && x.WardId == TreeStructure.Id).SingleOrDefault();
                    this.LLG = Globals.GeoList.LLGList.Where(x => x.Status && x.LLGId == this.Ward.LLGId).SingleOrDefault();
                    this.District = Globals.GeoList.DistrictList.Where(x => x.Status && x.DistrictId == this.Ward.DistrictId).SingleOrDefault();
                    this.Province = Globals.GeoList.ProvinceList.Where(x => x.Status && x.ProvinceId == this.Ward.ProvinceId).SingleOrDefault();
                    this.Region = Globals.GeoList.RegionList.Where(x => x.Status && x.RegionId == this.Ward.RegionId).SingleOrDefault();

                    if (this.Ward.WardId > 0)
                    {
                        var ward = Convert.ToString(this.Ward.WardId).PadLeft(4, '0');
                    }
                }
                else if (TreeStructure.MainTagName == "Location")
                {
                    this.Ward = Globals.GeoList.WardList.Where(x => x.Status && x.WardId == TreeStructure.ParentId).SingleOrDefault();
                    this.LLG = Globals.GeoList.LLGList.Where(x => x.Status && x.LLGId == this.Ward.LLGId).SingleOrDefault();
                    this.District = Globals.GeoList.DistrictList.Where(x => x.Status && x.DistrictId == this.Ward.DistrictId).SingleOrDefault();
                    this.Province = Globals.GeoList.ProvinceList.Where(x => x.Status && x.ProvinceId == this.Ward.ProvinceId).SingleOrDefault();
                    this.Region = Globals.GeoList.RegionList.Where(x => x.Status && x.RegionId == this.Ward.RegionId).SingleOrDefault();

                    if (this.Ward.WardId > 0)
                    {
                        var ward = Convert.ToString(this.Ward.WardId).PadLeft(4, '0');
                    }
                }
            }

            await GridViewSetting("AfterSelect");
        }
        #endregion

        #region RefreshData
        /// <summary>
        /// Refresh Data
        /// </summary>
        private async void RefreshData()
        {
            PageList model = new PageList();
            model.PageNo = 1;
            model.PageSize = 15;
            model.KeyWord = "";
            model.SortField = "EnrolmentFormId";
            model.SortOrder = "DESC";

            Response<EnrolmentFormModel> response = await GenericHttpClient.PostRequest<EnrolmentFormModel>("EnrolmentForm/GetEnrolmentFormList", model);
            if (response.Status)
            {
                BindEnrolmentFormGrid(response.Data.ListItems.ToList());
            }
        }
        #endregion

        #region GridViewSetting
        /// <summary>
        /// Grids the view setting.
        /// </summary>
        private async Task GridViewSetting(string eventName = "")
        {
            GlobalsModel.mainMaster.ShowLoader();
            pageList = new PageList();

            try
            {
                // Current Database Filed 
                pageList.DatabaseName = GlobalsModel.LoggedUser.Database.ToString();

                // Pagination Filed
                string pageIndex = string.Empty;
                if (eventName == "AfterSelect" || eventName == "Refresh" || eventName == "Search" || eventName == "PageSize")
                {
                    pageIndex = "1";
                }
                else
                {
                    pageIndex = btnPager.Name.Equals("") ? "1" : btnPager.Name;
                }
                pageList.PageNo = int.Parse(pageIndex);
                pageList.PageSize = pageSizeValue.Equals(0) ? Convert.ToInt32(pageSizeArray.First()) : pageSizeValue;
                pageList.KeyWord = txtSearch.Text;
                pageList.SortField = "EnrolmentFormId";
                pageList.SortOrder = "DESC";

                // TreeView Filed
                pageList.GeoName = TreeStructure.MainTagName;
                pageList.GeoId = TreeStructure.Id;

                // Login User
                pageList.LoggedUserId = GlobalsModel.LoggedUser.User.UserId;

                if (pageList != null && TreeStructure.MainTagName != "Location")
                {
                    await BindGrid(pageList);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Resources.ErrorMessage, "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorLog.Write("WardQualityCheck", "GridViewSetting", ex);
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
            Response<EnrolmentFormModel> response = await GenericHttpClient.PostRequest<EnrolmentFormModel>("EnrolmentForm/GetEnrolmentFormList", model);
            if (response.Status)
            {
                if (response.Data.ListItems.ToList() != null && response.Data.ListItems.ToList().Count > 0)
                {
                    foreach (var item in response.Data.ListItems.ToList())
                    {
                        DateTime utcDate = DateTime.SpecifyKind(item.CreatedDate, DateTimeKind.Utc);
                        DateTime localDate = utcDate.ToLocalTime();
                        item.Date = String.Format("{0:dd-MM-yyyy HH:mm}", localDate);
                    }
                }

                BindEnrolmentFormGrid(response.Data.ListItems.ToList());
                PopulatePager(response.Data.TotalRecordCount, model.PageNo, model.PageSize);
            }

            //clearData();
        }
        #endregion

        #region Bind EnrolmentForm Grid
        /// <summary>
        /// Binds the EnrolmentForm gird.
        /// </summary>
        /// <param name="model">The model.</param>
        private void BindEnrolmentFormGrid(List<EnrolmentFormModel> lstEnrolmentForms)
        {
            if (dgEnrolmentForms.Columns.Count > 0)
            {
                dgEnrolmentForms.Columns.Clear();
            }
            this.ListOfEnrols = lstEnrolmentForms;
            dgEnrolmentForms.DataSource = lstEnrolmentForms;

            dgEnrolmentForms.Columns["EnrolmentFormId"].Visible = false;
            dgEnrolmentForms.Columns["UserId"].Visible = false;
            dgEnrolmentForms.Columns["ProvinceId"].Visible = false;
            dgEnrolmentForms.Columns["RegionId"].Visible = false;
            dgEnrolmentForms.Columns["DistrictId"].Visible = false;
            dgEnrolmentForms.Columns["LLGId"].Visible = false;
            dgEnrolmentForms.Columns["WardId"].Visible = false;
            dgEnrolmentForms.Columns["ResponseCode"].Visible = false;
            dgEnrolmentForms.Columns["CreatedDate"].Visible = false;
            dgEnrolmentForms.Columns["Comment"].Visible = false;
            dgEnrolmentForms.Columns["Attachment"].Visible = false;
            dgEnrolmentForms.Columns["RegionName"].Visible = false;
            dgEnrolmentForms.Columns["NumberOfForms"].Visible = false;
            dgEnrolmentForms.Columns["Date"].Visible = false;
            dgEnrolmentForms.Columns["EmailAddress"].Visible = false;
            dgEnrolmentForms.Columns["ActionBy"].Visible = false;
            dgEnrolmentForms.Columns["UsedFromRegistration"].Visible = false;
            dgEnrolmentForms.Columns["UsedFromTransferIn"].Visible = false;

            dgEnrolmentForms.Columns["ProvinceName"].HeaderText = "Province";
            dgEnrolmentForms.Columns["DistrictName"].HeaderText = "District";
            dgEnrolmentForms.Columns["LLGName"].HeaderText = "LLG";
            dgEnrolmentForms.Columns["WardName"].HeaderText = "Ward";
            dgEnrolmentForms.Columns["NumberOfBatch"].HeaderText = "Number Of Form";

            DataGridViewButtonColumn viewButton = new DataGridViewButtonColumn();
            viewButton.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            viewButton.FlatStyle = FlatStyle.Standard;
            viewButton.HeaderText = "Un Used/Missing Form11";
            viewButton.Name = "View";
            viewButton.Text = "View";
            viewButton.UseColumnTextForButtonValue = true;
            viewButton.Width = 120;
            if (dgEnrolmentForms.Columns["View"] == null)
            {
                dgEnrolmentForms.Columns.Insert(dgEnrolmentForms.ColumnCount, viewButton);
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

            if (dgEnrolmentForms != null && dgEnrolmentForms.Rows.Count > 0)
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

        #region GetWardQualityCheckList
        /// <summary>
        /// Gets the ward quality check list.
        /// </summary>
        /// <param name="enrolmentForm">The enrolment form.</param>
        /// <returns></returns>
        private async Task<List<WardQualityCheckResponseModel>> GetWardQualityCheckList(EnrolmentFormModel enrolmentForm)
        {
            GlobalsModel.mainMaster.ShowLoader();
            lstWardQualityCheckResponse = new List<WardQualityCheckResponseModel>();

            try
            {
                var response = await GenericHttpClient.PostRequestCommon<List<WardQualityCheckResponseModel>>("EnrolmentForm/GetWardQualityCheckData", enrolmentForm);
                if (response.Status)
                {
                    lstWardQualityCheckResponse = response.Data;
                }
                else
                {
                    MessageBox.Show(response.Message, "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ErrorLog.WriteLog("WardQualityCheck => GetWardQualityCheckList", response.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Resources.ErrorMessage, "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorLog.Write("WardQualityCheck", "GetWardQualityCheckList", ex);
            }
            finally
            {
                GlobalsModel.mainMaster.HideLoader();
            }

            return lstWardQualityCheckResponse;
        }
        #endregion

        #endregion
    }
}