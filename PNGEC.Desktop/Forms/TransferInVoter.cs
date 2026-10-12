using Microsoft.Reporting.WinForms;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Domain.Region;
using PNGEC.API.Domain.Report;
using PNGEC.API.Helper;
using PNGEC.Desktop.Model;
using PNGEC.Desktop.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PNGEC.Desktop.Forms
{
    public partial class TransferInVoter : Form
    {
        #region variable declaration
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
        private DataSet dsExportReport = null;

        /// <summary>
        /// The dt
        /// </summary>
        private DataTable dtExportReport = null;

        /// <summary>
        /// The LST ward roll
        /// </summary>
        private List<WardRollBookReortModel> lstWardRoll = null;

        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="TransferInVoter"/> class.
        /// </summary>
        public TransferInVoter()
        {
            InitializeComponent();
        }
        #endregion

        #region Events

        /// <summary>
        /// Handles the Load event of the TransferInVoter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void TransferInVoter_Load(object sender, EventArgs e)
        {
            this.BindRegionName();
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
        /// Handles the Click event of the btnRefresh control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
            await GridViewSetting("Refresh");
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cmbRegion control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void cmbRegionName_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbProvinceName.SelectedIndexChanged -= cmbProvinceName_SelectedIndexChanged;
            cmbDistrictName.SelectedIndexChanged -= cmbDistrictName_SelectedIndexChanged;
            cmbLLGName.SelectedIndexChanged -= cmbLLGName_SelectedIndexChanged;
            cmbWardName.SelectedIndexChanged -= cmbWardName_SelectedIndexChanged;

            BindProvince();

            await GridViewSetting("SelectedIndexChanged");
            cmbProvinceName.SelectedIndexChanged += cmbProvinceName_SelectedIndexChanged;
            cmbDistrictName.SelectedIndexChanged += cmbDistrictName_SelectedIndexChanged;
            cmbLLGName.SelectedIndexChanged += cmbLLGName_SelectedIndexChanged;
            cmbWardName.SelectedIndexChanged += cmbWardName_SelectedIndexChanged;
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cmbProvinceName control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void cmbProvinceName_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbDistrictName.SelectedIndexChanged -= cmbDistrictName_SelectedIndexChanged;
            cmbLLGName.SelectedIndexChanged -= cmbLLGName_SelectedIndexChanged;
            cmbWardName.SelectedIndexChanged -= cmbWardName_SelectedIndexChanged;

            BindDistrictName();

            await GridViewSetting("SelectedIndexChanged");
            cmbDistrictName.SelectedIndexChanged += cmbDistrictName_SelectedIndexChanged;
            cmbLLGName.SelectedIndexChanged += cmbLLGName_SelectedIndexChanged;
            cmbWardName.SelectedIndexChanged += cmbWardName_SelectedIndexChanged;
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cmbDistrictName control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void cmbDistrictName_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbLLGName.SelectedIndexChanged -= cmbLLGName_SelectedIndexChanged;
            cmbWardName.SelectedIndexChanged -= cmbWardName_SelectedIndexChanged;

            BindLLGName();

            await GridViewSetting("SelectedIndexChanged");
            cmbLLGName.SelectedIndexChanged += cmbLLGName_SelectedIndexChanged;
            cmbWardName.SelectedIndexChanged += cmbWardName_SelectedIndexChanged;
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cmbLLGName control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void cmbLLGName_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbWardName.SelectedIndexChanged -= cmbWardName_SelectedIndexChanged;

            BindWardName();

            await GridViewSetting("SelectedIndexChanged");
            cmbWardName.SelectedIndexChanged += cmbWardName_SelectedIndexChanged;
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cmbWardName control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void cmbWardName_SelectedIndexChanged(object sender, EventArgs e)
        {
            await GridViewSetting("SelectedIndexChanged");
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
        /// Handles the Click event of the btnExport control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void btnExport_Click(object sender, EventArgs e)
        {
            string messageBoxText = string.Empty;

            try
            {
                if (ValidWard())
                {
                    #region Ward Roll Book TransferIn Voter

                    var saveFileDialog = new SaveFileDialog();
                    saveFileDialog.FileName = "TransferInVoter";
                    saveFileDialog.DefaultExt = ".pdf";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        GlobalsModel.mainMaster.ShowLoader();

                        await GetExportRecordsData();

                        if (dtExportReport != null && dtExportReport.Rows.Count > 0)
                        {
                            byte[] bytes = TransferInVoterExportReportSource();

                            using (FileStream stream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                            {
                                stream.Write(bytes, 0, bytes.Length);
                            }

                            messageBoxText = string.Format("Transfer In Voter is exported sucessfully at {0}", saveFileDialog.FileName);
                            MessageBox.Show(messageBoxText, "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Data was not found.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }

                    #endregion
                }
            }
            catch (Exception ex)
            {
                messageBoxText = string.Empty;
                ErrorLog.Write("TransferInVoter", "btnExport_Click", ex);
                MessageBox.Show("Something went wrong", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (dtExportReport != null)
                    dtExportReport.Clear();

                if (dsExportReport != null)
                    dsExportReport.Clear();

                messageBoxText = string.Empty;

                GlobalsModel.mainMaster.HideLoader();
            }
        }

        #endregion

        #region Methods

        #region BindRegionName
        /// <summary>
        /// Binds the name of the region.
        /// </summary>
        private void BindRegionName()
        {
            var list = Globals.GeoList.RegionList.Where(x => x.Status).ToList();
            list.Insert(0, new RegionModel { RegionId = 0, RegionName = "Select Region" });
            cmbRegionName.DataSource = list;
            cmbRegionName.ValueMember = "RegionId";
            cmbRegionName.DisplayMember = "RegionName";
        }
        #endregion

        #region BindProvinceName
        /// <summary>
        /// Binds the province.
        /// </summary>
        private void BindProvince()
        {
            if (cmbRegionName.SelectedIndex > 0)
            {
                var selectedValue = Convert.ToInt32(cmbRegionName.SelectedValue);
                var list = Globals.GeoList.ProvinceList.Where(x => x.Status && x.RegionId == selectedValue).ToList();
                list.Insert(0, new API.Domain.Province.ProvinceModel { ProvinceId = 0, ProvinceName = "Select Province" });
                cmbProvinceName.DataSource = list;
                cmbProvinceName.ValueMember = "ProvinceId";
                cmbProvinceName.DisplayMember = "ProvinceName";

                BindDistrictName();
            }
            else
            {
                cmbProvinceName.SelectedIndex = -1;
                cmbProvinceName.DataSource = null;

                cmbDistrictName.SelectedIndex = -1;
                cmbDistrictName.DataSource = null;

                cmbLLGName.SelectedIndex = -1;
                cmbLLGName.DataSource = null;

                cmbWardName.SelectedIndex = -1;
                cmbWardName.DataSource = null;
            }
        }
        #endregion

        #region BindDistrictName
        /// <summary>
        /// Binds the name of the district.
        /// </summary>
        private void BindDistrictName()
        {
            if (cmbProvinceName.SelectedIndex > 0)
            {
                var selectedValue = Convert.ToInt32(cmbProvinceName.SelectedValue);
                var list = Globals.GeoList.DistrictList.Where(x => x.Status && x.ProvinceId == selectedValue).ToList();
                list.Insert(0, new API.Domain.District.DistrictModel { DistrictId = 0, DistrictName = "Select District" });
                cmbDistrictName.DataSource = list;
                cmbDistrictName.ValueMember = "DistrictId";
                cmbDistrictName.DisplayMember = "DistrictName";

                BindLLGName();
            }
            else
            {
                cmbDistrictName.SelectedIndex = -1;
                cmbDistrictName.DataSource = null;

                cmbLLGName.SelectedIndex = -1;
                cmbLLGName.DataSource = null;

                cmbWardName.SelectedIndex = -1;
                cmbWardName.DataSource = null;
            }
        }
        #endregion

        #region BindLLGName
        /// <summary>
        /// Binds the name of the LLG.
        /// </summary>
        private void BindLLGName()
        {
            if (cmbDistrictName.SelectedIndex > 0)
            {
                var selectedValue = Convert.ToInt32(cmbDistrictName.SelectedValue);
                var list = Globals.GeoList.LLGList.Where(x => x.Status && x.DistrictId == selectedValue).ToList();
                list.Insert(0, new API.Domain.LLG.LLGModel { LLGId = 0, LLGName = "Select LLG" });
                cmbLLGName.DataSource = list;
                cmbLLGName.ValueMember = "LLGId";
                cmbLLGName.DisplayMember = "LLGName";

                BindWardName();
            }
            else
            {
                cmbLLGName.SelectedIndex = -1;
                cmbLLGName.DataSource = null;

                cmbWardName.SelectedIndex = -1;
                cmbWardName.DataSource = null;
            }
        }
        #endregion

        #region BindWardName
        /// <summary>
        /// Binds the name of the ward.
        /// </summary>
        private void BindWardName()
        {
            if (cmbLLGName.SelectedIndex > 0)
            {
                var selectedValue = Convert.ToInt32(cmbLLGName.SelectedValue);
                var list = Globals.GeoList.WardList.Where(x => x.Status && x.LLGId == selectedValue).ToList();
                list.Insert(0, new API.Domain.Ward.WardModel { WardId = 0, WardNoWardName = "Select Ward" });
                cmbWardName.DataSource = list;
                cmbWardName.ValueMember = "WardId";
                cmbWardName.DisplayMember = "WardNoWardName";
            }
            else
            {
                cmbWardName.SelectedIndex = -1;
                cmbWardName.DataSource = null;
            }
        }
        #endregion

        #region GridViewSetting
        /// <summary>
        /// Grids the view setting.
        /// </summary>
        /// <param name="eventName">Name of the event.</param>
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
                if (eventName == "Refresh" || eventName == "Search" || eventName == "PageSize" || eventName == "SelectedIndexChanged")
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
                pageList.SortField = "ElectorId";
                pageList.SortOrder = "DESC";

                // Filter Filed
                pageList.FiledName = "Status";
                pageList.FiledValue = "TRANSFERRED IN";

                // Geo Filed
                #region Geo Filed
                if (cmbWardName.SelectedIndex > 0)
                {
                    pageList.GeoName = "Ward";
                    var model = (API.Domain.Ward.WardModel)cmbWardName.SelectedItem;
                    pageList.GeoId = model.WardId;
                }
                else if (cmbLLGName.SelectedIndex > 0)
                {
                    pageList.GeoName = "LLG";
                    var model = (API.Domain.LLG.LLGModel)cmbLLGName.SelectedItem;
                    pageList.GeoId = model.LLGId;
                }
                else if (cmbDistrictName.SelectedIndex > 0)
                {
                    pageList.GeoName = "District";
                    var model = (API.Domain.District.DistrictModel)cmbDistrictName.SelectedItem;
                    pageList.GeoId = model.DistrictId;
                }
                else if (cmbProvinceName.SelectedIndex > 0)
                {
                    pageList.GeoName = "Province";
                    var model = (API.Domain.Province.ProvinceModel)cmbProvinceName.SelectedItem;
                    pageList.GeoId = model.ProvinceId;
                }
                else if (cmbRegionName.SelectedIndex > 0)
                {
                    pageList.GeoName = "Region";
                    var model = (API.Domain.Region.RegionModel)cmbRegionName.SelectedItem;
                    pageList.GeoId = model.RegionId;
                }
                else
                {
                    pageList.GeoName = "Geo";
                    pageList.GeoId = 0;
                }
                #endregion

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
                ErrorLog.Write("TransferInVoter", "GridViewSetting", ex);
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
            Response<WardRollBookReortModel> response = await GenericHttpClient.PostRequest<WardRollBookReortModel>("File/GetTransferInVoterList", model);
            if (response.Status)
            {
                if (response.Data.ListItems.ToList() != null)
                {
                    BindTransferInVoterGird(response.Data.ListItems.ToList());
                    PopulatePager(response.Data.TotalRecordCount, model.PageNo, model.PageSize);
                }
            }
        }
        #endregion

        #region Bind TransferInVoter Gird
        /// <summary>
        /// Binds the transfer in voter gird.
        /// </summary>
        /// <param name="lstVoters">The LST voters.</param>
        private void BindTransferInVoterGird(List<WardRollBookReortModel> lstVoters)
        {
            try
            {
                if (this.dgTransferInVoter.Columns.Count > 0)
                {
                    this.dgTransferInVoter.Columns.Clear();
                }

                //this.dgTransferInVoter.DataSource = null;
                this.dgTransferInVoter.DataSource = lstVoters;
                this.dgTransferInVoter.Columns["Seq"].HeaderText = "Seq";
                this.dgTransferInVoter.Columns["ElectorId"].HeaderText = "Elector Id";
                this.dgTransferInVoter.Columns["Name"].HeaderText = "Name";
                this.dgTransferInVoter.Columns["Gender"].HeaderText = "Gender";
                this.dgTransferInVoter.Columns["Location"].HeaderText = "Location";
                this.dgTransferInVoter.Columns["Occupation"].HeaderText = "Occupation";
                this.dgTransferInVoter.Columns["DOB"].Visible = false;
                this.dgTransferInVoter.Columns["DateOfBirth"].HeaderText = "DOB";
                this.dgTransferInVoter.Columns["StatusCode"].Visible = false;
                this.dgTransferInVoter.Columns["Comments"].HeaderText = "Comments";
                this.dgTransferInVoter.Columns["WardNumberWardName"].Visible = false;
                this.dgTransferInVoter.Columns["Count"].Visible = false;
                this.dgTransferInVoter.Columns["LName"].Visible = false;
                this.dgTransferInVoter.Columns["FName"].Visible = false;
                this.dgTransferInVoter.Columns["Gen"].Visible = false;
                this.dgTransferInVoter.Columns["BDate"].Visible = false;
                this.dgTransferInVoter.Columns["ElectorProfileImage"].Visible = false;
                this.dgTransferInVoter.Columns["isPWD"].Visible = false;
                this.dgTransferInVoter.Columns["WithDisability"].Visible = false;
                this.dgTransferInVoter.Columns["ElectorProfileImageUrl"].Visible = false;
                this.dgTransferInVoter.Columns["Christianity"].Visible = false;
                this.dgTransferInVoter.Columns["Religion"].HeaderText = "Religion";
                this.dgTransferInVoter.Columns["Region"].HeaderText = "Region";
                this.dgTransferInVoter.Columns["Province"].HeaderText = "Province";
                this.dgTransferInVoter.Columns["District"].HeaderText = "District";
                this.dgTransferInVoter.Columns["LLG"].HeaderText = "LLG";
                this.dgTransferInVoter.Columns["Ward"].HeaderText = "Ward";
                this.dgTransferInVoter.Columns["Electors"].Visible = false;
                this.dgTransferInVoter.Columns["DateTime"].Visible = false;
                this.dgTransferInVoter.Columns["DuplicateId"].Visible = false;
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(Resources.ResourceManager.GetString("ErrorMessage"), "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorLog.Write("TransferInVoter", "BindVoterGird", ex);
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

            if (dgTransferInVoter != null && dgTransferInVoter.Rows.Count > 0)
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

        #region Get Export Records List
        /// <summary>
        /// Gets the export records list.
        /// </summary>
        private async Task GetExportRecordsData()
        {
            #region pageList
            PageList pageList = new PageList();

            // Current Database Filed 
            pageList.DatabaseName = GlobalsModel.LoggedUser.Database.ToString();

            pageList.KeyWord = txtSearch.Text;

            // Filter Filed
            pageList.FiledName = "Status";
            pageList.FiledValue = "TRANSFERRED IN";

            // Geo Filed
            #region Geo Filed
            if (cmbWardName.SelectedIndex > 0)
            {
                pageList.GeoName = "Ward";
                var model = (API.Domain.Ward.WardModel)cmbWardName.SelectedItem;
                pageList.GeoId = model.WardId;
            }
            else if (cmbLLGName.SelectedIndex > 0)
            {
                pageList.GeoName = "LLG";
                var model = (API.Domain.LLG.LLGModel)cmbLLGName.SelectedItem;
                pageList.GeoId = model.LLGId;
            }
            else if (cmbDistrictName.SelectedIndex > 0)
            {
                pageList.GeoName = "District";
                var model = (API.Domain.District.DistrictModel)cmbDistrictName.SelectedItem;
                pageList.GeoId = model.DistrictId;
            }
            else if (cmbProvinceName.SelectedIndex > 0)
            {
                pageList.GeoName = "Province";
                var model = (API.Domain.Province.ProvinceModel)cmbProvinceName.SelectedItem;
                pageList.GeoId = model.ProvinceId;
            }
            else if (cmbRegionName.SelectedIndex > 0)
            {
                pageList.GeoName = "Region";
                var model = (API.Domain.Region.RegionModel)cmbRegionName.SelectedItem;
                pageList.GeoId = model.RegionId;
            }
            else
            {
                pageList.GeoName = "Geo";
                pageList.GeoId = 0;
            }
            #endregion

            // Login User
            pageList.LoggedUserId = GlobalsModel.LoggedUser.User.UserId;

            // Page No
            int PageNo = 1;

            // Page Size
            pageList.PageSize = 1000;
            #endregion

            lstWardRoll = new List<WardRollBookReortModel>();

            bool DataFound = false;

            do
            {
                pageList.PageNo = PageNo;
                Response<WardRollBookReortModel> response = await GenericHttpClient.PostRequest<WardRollBookReortModel>("File/GetTransferInVoterList", pageList);
                if (response.Status && response.Data.ListItems.ToList() != null && response.Data.ListItems.ToList().Count > 0)
                {
                    lstWardRoll.AddRange(response.Data.ListItems.ToList());
                    DataFound = true;
                }
                else
                {
                    DataFound = false;
                }

                PageNo++;

            } while (DataFound);

            if (lstWardRoll != null && lstWardRoll.Count > 0)
            {
                dtExportReport = new DataTable();
                dtExportReport = ToDataTable(lstWardRoll);
            }

            lstWardRoll.Clear();
        }
        #endregion

        #region TransferIn Voter Export ReportSource
        /// <summary>
        /// Wards the roll book transfer in voter export report source.
        /// </summary>
        /// <returns></returns>
        private byte[] TransferInVoterExportReportSource()
        {
            dsExportReport = new DataSet();
            byte[] bytes = null;

            this.dtExportReport.TableName = "tblTransferInVoter";
            if (!dsExportReport.Tables.Contains(this.dtExportReport.TableName))
            {
                dsExportReport.Tables.Add(this.dtExportReport);
            }
            if (dsExportReport.Tables[0].Rows != null && dsExportReport.Tables[0].Rows.Count > 0)
            {
                Warning[] warnings;
                string[] streamIds;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;

                //This is optional if you have parameter then you can add parameters as much as you want
                #region param
                ReportParameter[] param = new ReportParameter[5];

                if (cmbRegionName.SelectedIndex > 0)
                {
                    param[0] = new ReportParameter("RegionName", cmbRegionName.Text, true);
                }
                else
                {
                    param[0] = new ReportParameter("RegionName", "", true);
                }

                if (cmbProvinceName.SelectedIndex > 0)
                {
                    param[1] = new ReportParameter("ProvinceName", cmbProvinceName.Text, true);
                }
                else
                {
                    param[1] = new ReportParameter("ProvinceName", "", true);
                }

                if (cmbDistrictName.SelectedIndex > 0)
                {
                    param[2] = new ReportParameter("DistrictName", cmbDistrictName.Text, true);
                }
                else
                {
                    param[2] = new ReportParameter("DistrictName", "", true);
                }

                if (cmbLLGName.SelectedIndex > 0)
                {
                    param[3] = new ReportParameter("LLGName", cmbLLGName.Text, true);
                }
                else
                {
                    param[3] = new ReportParameter("LLGName", "", true);
                }

                if (cmbWardName.SelectedIndex > 0)
                {
                    param[4] = new ReportParameter("WardName", cmbWardName.Text, true);
                }
                else
                {
                    param[4] = new ReportParameter("WardName", "", true);
                }
                #endregion

                ReportDataSource dataSource = new ReportDataSource("ds_TransferInVoter", dsExportReport.Tables[0].Copy());
                ReportViewer viewer = new ReportViewer();
                viewer.LocalReport.Refresh();
                viewer.LocalReport.ReportPath = "Report/TransferInVoter.rdlc";
                viewer.LocalReport.SetParameters(param);
                viewer.LocalReport.DataSources.Add(dataSource);
                bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
            }


            return bytes;
        }
        #endregion

        #region ToDataTable
        /// <summary>
        /// Converts to datatable.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }
        #endregion

        #region Valid Ward
        /// <summary>
        /// Valids the ward.
        /// </summary>
        /// <returns></returns>
        private Boolean ValidWard()
        {
            if(cmbRegionName.SelectedIndex <= 0)
            {
                MessageBox.Show("Plese Select Region", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (cmbProvinceName.SelectedIndex <= 0)
            {
                MessageBox.Show("Plese Select Province", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (cmbDistrictName.SelectedIndex <= 0)
            {
                MessageBox.Show("Plese Select District", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (cmbLLGName.SelectedIndex <= 0)
            {
                MessageBox.Show("Plese Select LLG", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (cmbWardName.SelectedIndex <= 0)
            {
                MessageBox.Show("Plese Select Ward", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #endregion
    }
}
