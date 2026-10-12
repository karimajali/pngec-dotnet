using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Domain.Mobile.Voter;
using PNGEC.API.Domain.Report;
using PNGEC.API.Helper;
using PNGEC.Desktop.Model;
using PNGEC.Desktop.Properties;

namespace PNGEC.Desktop.Forms
{
    public partial class MKAWardStatus : Form
    {
        #region variable declaration
        /// <summary>
        /// The geo tree structure
        /// </summary>
        private MKAGeoTreeStructure MKATreeStructure = null;

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
        /// The page name
        /// </summary>
        private string pageName = string.Empty;

        /// <summary>
        /// The ds export report
        /// </summary>
        private DataSet dsExportReport = null;

        /// <summary>
        /// The dt export report
        /// </summary>
        private DataTable dtExportReport = null;

        /// <summary>
        /// The LST voter model
        /// </summary>
        private List<WardRollBookReortModel> lstVoterModel = null;

        /// <summary>
        /// The request model
        /// </summary>
        private WardStatusReportRequestModel requestModel = null;
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="WardStatus"/> class.
        /// </summary>
        /// <param name="pageName">Name of the page.</param>       
        public MKAWardStatus(string pageName)
        {
            InitializeComponent();
            this.pageName = pageName;
        }
        #endregion

        #region Events

        /// <summary>
        /// Handles the Load event of the WardStatus control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>      

        private void MKAWardStatus_Load(object sender, EventArgs e)
        {
            dtpFromDate.MaxDate = DateTime.Now.Date;
            dtpToDate.MaxDate = DateTime.Now.Date;

            GlobalsModel.mainMaster.TreeViewSelectedChanged -= TreeViewSelectedChanged_AfterSelect;
            GlobalsModel.mainMaster.TreeViewSelectedChanged += TreeViewSelectedChanged_AfterSelect;

            dtpFromDate.Checked = false;
            dtpToDate.Checked = false;

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
            MKATreeStructure = GlobalsModel.MKATreeViewStructure;
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
                MessageBox.Show("To date is not be empty", "MKA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if ((dtpFromDate.Value != null && dtpToDate.Value != null) && (dtpFromDate.Value > dtpToDate.Value))
            {
                MessageBox.Show("The To date must be greater than the From date.", "MKA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            dtpFromDate.Checked = false;
            dtpToDate.Checked = false;

            await GridViewSetting("Refresh");
        }

        /// <summary>
        /// Handles the Click event of the btnExport control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (pageName == "WardStatus")
                {
                    #region Ward Status

                    //string exportReportName = GlobalsModel.reportName + "_" + MKATreeStructure.MainTagName + "_WardStatus";

                    //var saveFileDialog = new SaveFileDialog();
                    //saveFileDialog.FileName = exportReportName;
                    //saveFileDialog.DefaultExt = ".pdf";
                    //if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    //{
                    //    GlobalsModel.mainMaster.ShowLoader();

                    //    string folderBasePath = saveFileDialog.FileName.Replace(exportReportName, string.Empty).Replace(".pdf", string.Empty);

                    //    await GetExportRecordsData();

                    //    string WardStatusMessage = string.Empty;

                    //    if (dtExportReport != null && dtExportReport.Rows.Count > 0)
                    //    {
                    //        byte[] bytes = WardStatusExportReport();

                    //        using (FileStream stream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                    //        {
                    //            stream.Write(bytes, 0, bytes.Length);
                    //        }

                    //        WardStatusMessage = string.Format("{0} Ward Status is exported sucessfully at {1}", MKATreeStructure.MainTagName, saveFileDialog.FileName);
                    //    }
                    //    else
                    //    {
                    //        WardStatusMessage = string.Format("{0} Ward Status is No record founds.", MKATreeStructure.MainTagName);
                    //    }


                    //    MessageBox.Show(WardStatusMessage, "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //}

                    #endregion
                }
                else if (pageName == "FieldTallySummarySheet")
                {
                    #region Field Tally Summary Sheet

                    string exportReportName = GlobalsModel.reportName + "_" + MKATreeStructure.MainTagName + "FieldTallySummarySheet";

                    var saveFileDialog = new SaveFileDialog();
                    saveFileDialog.FileName = exportReportName;
                    saveFileDialog.DefaultExt = ".pdf";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        GlobalsModel.mainMaster.ShowLoader();

                        string folderBasePath = saveFileDialog.FileName.Replace(exportReportName, string.Empty).Replace(".pdf", string.Empty);

                        await GetExportRecordsData();

                        string FieldTallySummarySheetMessage = string.Empty;

                        if (dtExportReport != null && dtExportReport.Rows.Count > 0)
                        {
                            byte[] bytes = FieldTallySummarySheetExportReport();

                            using (FileStream stream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                            {
                                stream.Write(bytes, 0, bytes.Length);
                            }

                            FieldTallySummarySheetMessage = string.Format("{0} Field Tally Summary Sheet is exported sucessfully at {1}", MKATreeStructure.MainTagName, saveFileDialog.FileName);
                        }
                        else
                        {
                            FieldTallySummarySheetMessage = string.Format("{0} Field Tally Summary Sheet is No record founds.", MKATreeStructure.MainTagName);
                        }


                        MessageBox.Show(FieldTallySummarySheetMessage, "MKA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    #endregion
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Resources.ResourceManager.GetString("ErrorMessage"), "MKA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorLog.Write("FieldTallySummarySheet", "btnExport_Click", ex);
            }
            finally
            {
                GlobalsModel.mainMaster.HideLoader();
            }
        }

        /// <summary>
        /// Handles the FormClosing event of the WardStatus control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>               
        private void MKAWardStatus_FormClosing(object sender, FormClosingEventArgs e)
        {
            GlobalsModel.mainMaster.TreeViewSelectedChanged -= TreeViewSelectedChanged_AfterSelect;
        }

        /// <summary>
        /// Handles the RowPrePaint event of the dgWardStatus control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewRowPrePaintEventArgs"/> instance containing the event data.</param>
        private void dgWardStatus_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            //dgWardStatus.Columns["TRANSFEROUT"].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#FEB6B7");
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
            MKATreeStructure = GlobalsModel.MKATreeViewStructure;
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
                pageList.SortField = "ElectorId";
                pageList.SortOrder = "DESC";

                // Filter Filed
                if (dtpFromDate.Checked == true && dtpToDate.Checked == false)
                {
                    //pageList.FromDate = dtpFromDate.Value.ToString("yyyy-MM-dd");
                    //pageList.ToDate = DateTime.Now.ToString("yyyy-MM-dd");

                    TimeSpan FromTime = new TimeSpan(00, 00, 00);
                    var FromDate = dtpFromDate.Value.Date + FromTime;
                    pageList.DateFrom = FromDate.ToUniversalTime();

                    TimeSpan ToTime = new TimeSpan(23, 59, 59);
                    var ToDate = DateTime.Now.Date + ToTime;
                    pageList.DateTo = ToDate.ToUniversalTime();
                }
                else if (dtpFromDate.Checked == true && dtpToDate.Checked == true)
                {
                    //pageList.FromDate = dtpFromDate.Value.ToString("yyyy-MM-dd");
                    //pageList.ToDate = dtpToDate.Value.ToString("yyyy-MM-dd");

                    TimeSpan FromTime = new TimeSpan(00, 00, 00);
                    var FromDate = dtpFromDate.Value.Date + FromTime;
                    pageList.DateFrom = FromDate.ToUniversalTime();

                    TimeSpan ToTime = new TimeSpan(23, 59, 59);
                    var ToDate = dtpToDate.Value.Date + ToTime;
                    pageList.DateTo = ToDate.ToUniversalTime();
                }
                else
                {
                    pageList.DateFrom = null;
                    pageList.DateTo = null;
                }

                pageList.PageName = pageName;

                // TreeView Filed
                pageList.GeoName = MKATreeStructure.MainTagName;
                pageList.GeoId = MKATreeStructure.Id;

                // Login User
                pageList.LoggedUserId = GlobalsModel.LoggedUser.User.UserId;

                if (pageList != null)
                {
                    await this.BindGrid(pageList);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Resources.ErrorMessage, "MKA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorLog.Write("WardStatus", "GridViewSetting", ex);
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
            Response<VoterModel> response = await GenericHttpClient.PostRequest<VoterModel>("File/GetVoterList", model);
            if (response.Status)
            {
                BindWardStatusGird(response.Data.ListItems.ToList());
                PopulatePager(response.Data.TotalRecordCount, model.PageNo, model.PageSize);

                if (GlobalsModel.MKATreeViewStructure.MainTagName == "MKAWard" || GlobalsModel.MKATreeViewStructure.MainTagName == "MKALocation")
                {
                    pnlExport.Visible = true;
                }
                else
                {
                    pnlExport.Visible = false;
                }
            }
        }
        #endregion

        #region Bind WardStatus Gird
        /// <summary>
        /// Binds the ward status gird.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <exception cref="NotImplementedException"></exception>
        private void BindWardStatusGird(List<VoterModel> lstWardStatus)
        {
            try
            {
                if (this.dgWardStatus.Columns.Count > 0)
                {
                    this.dgWardStatus.Columns.Clear();
                }

                // Unvisible Column
                //this.dgWardStatus.DataSource = null;
                this.dgWardStatus.DataSource = lstWardStatus;
                this.dgWardStatus.Columns["Database"].Visible = false;
                this.dgWardStatus.Columns["OldElectorID"].Visible = false;
                this.dgWardStatus.Columns["LocationId"].Visible = false;
                this.dgWardStatus.Columns["WardId"].Visible = false;
                this.dgWardStatus.Columns["LLGId"].Visible = false;
                this.dgWardStatus.Columns["DistrictId"].Visible = false;
                this.dgWardStatus.Columns["ProvinceId"].Visible = false;
                this.dgWardStatus.Columns["RegionId"].Visible = false;
                this.dgWardStatus.Columns["MKALocationId"].Visible = false;
                this.dgWardStatus.Columns["MKAWardId"].Visible = false;
                this.dgWardStatus.Columns["MKALLGId"].Visible = false;
                this.dgWardStatus.Columns["MKADistrictId"].Visible = false;
                this.dgWardStatus.Columns["MKAClanId"].Visible = false;
                this.dgWardStatus.Columns["LName"].Visible = false;
                this.dgWardStatus.Columns["FName"].Visible = false;
                this.dgWardStatus.Columns["Gender"].Visible = false;
                this.dgWardStatus.Columns["DOB"].Visible = false;
                this.dgWardStatus.Columns["isYOB"].Visible = false;
                this.dgWardStatus.Columns["isPWD"].Visible = false;
                this.dgWardStatus.Columns["FatherLName"].Visible = false;
                this.dgWardStatus.Columns["FatherFName"].Visible = false;
                this.dgWardStatus.Columns["MotherLName"].Visible = false;
                this.dgWardStatus.Columns["MotherFName"].Visible = false;
                this.dgWardStatus.Columns["ProvOfBirthFK"].Visible = false;
                this.dgWardStatus.Columns["PlaceOfBirth"].Visible = false;
                this.dgWardStatus.Columns["OccupationFK"].Visible = false;
                this.dgWardStatus.Columns["ClanFK"].Visible = false;
                this.dgWardStatus.Columns["SubClan"].Visible = false;
                this.dgWardStatus.Columns["Address"].Visible = false;
                this.dgWardStatus.Columns["NatID"].Visible = false;
                this.dgWardStatus.Columns["NatIDRecordNo"].Visible = false;
                this.dgWardStatus.Columns["HouseFK"].Visible = false;
                this.dgWardStatus.Columns["HouseSortOrder"].Visible = false;
                this.dgWardStatus.Columns["FamilyFK"].Visible = false;
                this.dgWardStatus.Columns["FamilySortOrder"].Visible = false;
                this.dgWardStatus.Columns["dpo"].Visible = false;
                this.dgWardStatus.Columns["posted"].Visible = false;
                this.dgWardStatus.Columns["ProvinceOfBirth"].Visible = false;
                this.dgWardStatus.Columns["TempElectorId"].Visible = false;
                this.dgWardStatus.Columns["FamilyNumber"].Visible = false;
                this.dgWardStatus.Columns["FamilyName"].Visible = false;
                this.dgWardStatus.Columns["FamilySortOrder"].Visible = false;
                this.dgWardStatus.Columns["OpenElectorate"].Visible = false;
                this.dgWardStatus.Columns["Clan"].Visible = false;
                this.dgWardStatus.Columns["RestHouse"].Visible = false;
                this.dgWardStatus.Columns["SectionNo"].Visible = false;
                this.dgWardStatus.Columns["LotNo"].Visible = false;
                this.dgWardStatus.Columns["StreetName"].Visible = false;
                this.dgWardStatus.Columns["SuburbTownSettlement"].Visible = false;
                this.dgWardStatus.Columns["Comment"].Visible = false;
                this.dgWardStatus.Columns["ElectorProfileImage"].Visible = false;
                this.dgWardStatus.Columns["ElectorProfileImageUrl"].Visible = false;
                this.dgWardStatus.Columns["Lat"].Visible = false;
                this.dgWardStatus.Columns["Long"].Visible = false;
                this.dgWardStatus.Columns["ElectorFormImage"].Visible = false;
                this.dgWardStatus.Columns["ElectorFormImageUrl"].Visible = false;
                this.dgWardStatus.Columns["ElectorFormImageFile"].Visible = false;
                this.dgWardStatus.Columns["ElectorProfileImageFile"].Visible = false;
                this.dgWardStatus.Columns["CreatedBy"].Visible = false;
                this.dgWardStatus.Columns["DuplicateId"].Visible = false;
                this.dgWardStatus.Columns["AuditDescription"].Visible = false;
                this.dgWardStatus.Columns["HasTransferInForm11"].Visible = false;
                this.dgWardStatus.Columns["TransferInRegionId"].Visible = false;
                this.dgWardStatus.Columns["TransferInProvinceId"].Visible = false;
                this.dgWardStatus.Columns["TransferInDistrictId"].Visible = false;
                this.dgWardStatus.Columns["TransferInLLGId"].Visible = false;
                this.dgWardStatus.Columns["TransferInWardId"].Visible = false;
                this.dgWardStatus.Columns["TransferInLocationId"].Visible = false;
                this.dgWardStatus.Columns["TransferOutRegionId"].Visible = false;
                this.dgWardStatus.Columns["TransferOutProvinceId"].Visible = false;
                this.dgWardStatus.Columns["TransferOutDistrictId"].Visible = false;
                this.dgWardStatus.Columns["TransferOutLLGId"].Visible = false;
                this.dgWardStatus.Columns["TransferOutWardId"].Visible = false;
                this.dgWardStatus.Columns["TransferOutLocationId"].Visible = false;
                this.dgWardStatus.Columns["TransferInMKADistrictId"].Visible = false;
                this.dgWardStatus.Columns["TransferInMKALLGId"].Visible = false;
                this.dgWardStatus.Columns["TransferInMKAWardId"].Visible = false;
                this.dgWardStatus.Columns["TransferInMKALocationId"].Visible = false;                
                this.dgWardStatus.Columns["TransferOutMKADistrictId"].Visible = false;
                this.dgWardStatus.Columns["TransferOutMKALLGId"].Visible = false;
                this.dgWardStatus.Columns["TransferOutMKAWardId"].Visible = false;
                this.dgWardStatus.Columns["TransferOutMKALocationId"].Visible = false;
                this.dgWardStatus.Columns["IsRejectedByRD"].Visible = false;
                this.dgWardStatus.Columns["IsRejectedByRD"].Visible = false;
                this.dgWardStatus.Columns["Religion"].Visible = false;
                this.dgWardStatus.Columns["ErrorMessage"].Visible = false;
                this.dgWardStatus.Columns["CreatedDate"].Visible = false;
                this.dgWardStatus.Columns["ARLat"].Visible = false;
                this.dgWardStatus.Columns["ARLong"].Visible = false;
                this.dgWardStatus.Columns["RejectedReason"].Visible = false;
                this.dgWardStatus.Columns["IsApproveOrReject"].Visible = false;
                this.dgWardStatus.Columns["ModifiedDate"].Visible = false;
                this.dgWardStatus.Columns["ModifiedByName"].Visible = false;
                this.dgWardStatus.Columns["IsRejected"].Visible = false;
                this.dgWardStatus.Columns["EnrolmentFormNumber"].Visible = false;
                this.dgWardStatus.Columns["transferInProvinceName"].Visible = false;
                this.dgWardStatus.Columns["transferInDistrictName"].Visible = false;
                this.dgWardStatus.Columns["transferInLLGName"].Visible = false;
                this.dgWardStatus.Columns["transferInWardName"].Visible = false;
                this.dgWardStatus.Columns["transferInLocationName"].Visible = false;
                this.dgWardStatus.Columns["transferOutProvinceName"].Visible = false;
                this.dgWardStatus.Columns["transferOutDistrictName"].Visible = false;
                this.dgWardStatus.Columns["transferOutLLGName"].Visible = false;
                this.dgWardStatus.Columns["transferOutWardName"].Visible = false;
                this.dgWardStatus.Columns["transferOutLocationName"].Visible = false;

               
                this.dgWardStatus.Columns["transferInMKADistrictName"].Visible = false;
                this.dgWardStatus.Columns["transferInMKALLGName"].Visible = false;
                this.dgWardStatus.Columns["transferInMKAWardName"].Visible = false;
                this.dgWardStatus.Columns["transferInMKALocationName"].Visible = false;
                
                this.dgWardStatus.Columns["transferOutMKADistrictName"].Visible = false;
                this.dgWardStatus.Columns["transferOutMKALLGName"].Visible = false;
                this.dgWardStatus.Columns["transferOutMKAWardName"].Visible = false;
                this.dgWardStatus.Columns["transferOutMKALocationName"].Visible = false;
                this.dgWardStatus.Columns["ElectorSignatureImage"].Visible = false;
                this.dgWardStatus.Columns["ElectorFingerPrintImage"].Visible = false;
                this.dgWardStatus.Columns["ElectorProfileImageBinary"].Visible = false;
                this.dgWardStatus.Columns["ElectorSignatureImageBinary"].Visible = false;
                this.dgWardStatus.Columns["OldLocationId"].Visible = false;
                this.dgWardStatus.Columns["IsManualFormNumber"].Visible = false;

                this.dgWardStatus.Columns["LocationName"].Visible = false;
                this.dgWardStatus.Columns["WardName"].Visible = false;
                this.dgWardStatus.Columns["LLGName"].Visible = false;
                this.dgWardStatus.Columns["DistrictName"].Visible = false;
                this.dgWardStatus.Columns["ProvinceName"].Visible = false;
                this.dgWardStatus.Columns["EnrolmentFormType"].Visible = false;


                // Display Column
                this.dgWardStatus.Columns["RowNo"].HeaderText = "Row No";
                this.dgWardStatus.Columns["VoterName"].HeaderText = "Voter Name";
                this.dgWardStatus.Columns["ElectorId"].HeaderText = "Elector Id";
                this.dgWardStatus.Columns["Status"].HeaderText = "Status";                
                this.dgWardStatus.Columns["MKADistrictName"].HeaderText = "MKA District Name";
                this.dgWardStatus.Columns["MKALLGName"].HeaderText = "MKA LLG Name";
                this.dgWardStatus.Columns["MKAWardName"].HeaderText = "MKA Ward Name";
                this.dgWardStatus.Columns["MKALocationName"].HeaderText = "MKA Location Name";
                this.dgWardStatus.Columns["MKAClanName"].HeaderText = "MKA Clan Name";

                this.dgWardStatus.Columns["Gen"].Visible = false;
                this.dgWardStatus.Columns["DateOfBirth"].Visible = false;
                this.dgWardStatus.Columns["DuplicateElectorId"].Visible = false;
                this.dgWardStatus.Columns["DuplicateLName"].Visible = false;
                this.dgWardStatus.Columns["DuplicateFName"].Visible = false;
                this.dgWardStatus.Columns["DuplicateVoterName"].Visible = false;
                this.dgWardStatus.Columns["DuplicateGender"].Visible = false;
                this.dgWardStatus.Columns["DuplicateGen"].Visible = false;
                this.dgWardStatus.Columns["DuplicateDOB"].Visible = false;
                this.dgWardStatus.Columns["DuplicateDateOfBirth"].Visible = false;
                this.dgWardStatus.Columns["DuplicateOccupation"].Visible = false;
                this.dgWardStatus.Columns["DuplicateProvinceName"].Visible = false;
                this.dgWardStatus.Columns["DuplicateDistrictName"].Visible = false;
                this.dgWardStatus.Columns["DuplicateLLGName"].Visible = false;
                this.dgWardStatus.Columns["DuplicateWardName"].Visible = false;
                this.dgWardStatus.Columns["DuplicateLocationName"].Visible = false;

                // Logged User EM or RD or RO
                if ((GlobalsModel.LoggedUser.User.Role == 2 ||
                    GlobalsModel.LoggedUser.User.Role == 3 ||
                    GlobalsModel.LoggedUser.User.Role == 7 ||
                    GlobalsModel.LoggedUser.User.Role == 0) &&
                    (this.pageName == "DataMigration" || this.pageName == "TransferOut" || this.pageName == "Duplicate") &&
                    (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Staging ||
                    GlobalsModel.LoggedUser.Database == CommonEnum.Database.Preliminary))
                {
                    this.dgWardStatus.Columns["RecordType"].HeaderText = "Record Type";
                    this.dgWardStatus.Columns["DuplicateRecordType"].Visible = false;
                }
                else
                {
                    this.dgWardStatus.Columns["RecordType"].Visible = false;
                    this.dgWardStatus.Columns["DuplicateRecordType"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Resources.ResourceManager.GetString("ErrorMessage"), "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorLog.Write("WardStatus", "BindWardStatusGird", ex);
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

            if (dgWardStatus != null && dgWardStatus.Rows.Count > 0)
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

        #region Get Export Records Data
        /// <summary>
        /// Gets the export records data.
        /// </summary>
        private async Task GetExportRecordsData()
        {
            #region requestModel

            requestModel = new WardStatusReportRequestModel();

            if (MKATreeStructure.MainTagName == "MKAWard")
            {
                API.Domain.MKAGeoStructure.MKAWardModel Ward = Globals.MKAGeoList.MKAWardList.Where(x => x.Status && x.WardId == MKATreeStructure.Id).SingleOrDefault();

                requestModel.MKAWardId = Ward.WardId;
                requestModel.MKALLGId = Ward.LLGId;
                requestModel.MKADistrictId = Ward.DistrictId;                

                Ward = null;
            }
            else if (MKATreeStructure.MainTagName == "MKALocation")
            {
                API.Domain.MKAGeoStructure.MKALocationModel Location = Globals.MKAGeoList.MKALocationList.Where(x => x.Status && x.LocationId == MKATreeStructure.Id).SingleOrDefault();

                requestModel.MKALocationId = Location.LocationId;
                requestModel.MKAWardId = Location.WardId;
                requestModel.MKALLGId = Location.LLGId;
                requestModel.MKADistrictId = Location.DistrictId;                

                Location = null;
            }

            if (dtpFromDate.Checked == true && dtpToDate.Checked == false)
            {
                TimeSpan FromTime = new TimeSpan(00, 00, 00);
                var FromDate = dtpFromDate.Value.Date + FromTime;
                requestModel.StartDate = FromDate.ToUniversalTime();

                TimeSpan ToTime = new TimeSpan(23, 59, 59);
                var ToDate = DateTime.Now.Date + ToTime;
                requestModel.EndDate = ToDate.ToUniversalTime();
            }
            else if (dtpFromDate.Checked == true && dtpToDate.Checked == true)
            {
                TimeSpan FromTime = new TimeSpan(00, 00, 00);
                var FromDate = dtpFromDate.Value.Date + FromTime;
                requestModel.StartDate = FromDate.ToUniversalTime();

                TimeSpan ToTime = new TimeSpan(23, 59, 59);
                var ToDate = dtpToDate.Value.Date + ToTime;
                requestModel.EndDate = ToDate.ToUniversalTime();
            }
            else
            {
                requestModel.StartDate = null;
                requestModel.EndDate = null;
            }

            requestModel.PageSize = 1000;

            int PageNo = 1;

            #endregion

            lstVoterModel = new List<WardRollBookReortModel>();

            bool DataFound = false;

            do
            {
                requestModel.RequestType = "MKA";
                requestModel.PageNo = PageNo;
                Response<WardRollBookReortModel> voterResponse = await GenericHttpClient.PostRequest<WardRollBookReortModel>("Reports/GetWardStatusReport", requestModel);
                if (voterResponse.Status && voterResponse.Data.ListItems != null && voterResponse.Data.ListItems.Count() > 0)
                {
                    lstVoterModel.AddRange(voterResponse.Data.ListItems);
                    DataFound = true;
                }
                else
                {
                    DataFound = false;
                }

                PageNo++;
                voterResponse = null;

            } while (DataFound);

            if (lstVoterModel != null && lstVoterModel.Count > 0)
            {
                dtExportReport = new DataTable();
                dtExportReport = ToDataTable(lstVoterModel);
            }

            requestModel = null;
            lstVoterModel.Clear();
        }
        #endregion

        #region Ward Status Export Report
        /// <summary>
        /// Wards the status export report.
        /// </summary>
        /// <returns></returns>
        private byte[] WardStatusExportReport()
        {
            byte[] bytest = null;

            if (dtExportReport != null && dtExportReport.Rows.Count > 0)
            {
                dsExportReport = new DataSet();

                string WardName = "", LocationName = "";

                if (MKATreeStructure.MainTagName == "Ward")
                {
                    WardName = $"{dtExportReport.Rows[0]["Province"]}/{dtExportReport.Rows[0]["District"]}/{dtExportReport.Rows[0]["LLG"]}/{dtExportReport.Rows[0]["WardNumberWardName"]}";
                    LocationName = "";
                }
                else if (MKATreeStructure.MainTagName == "Location")
                {
                    WardName = $"{dtExportReport.Rows[0]["Province"]}/{dtExportReport.Rows[0]["District"]}/{dtExportReport.Rows[0]["LLG"]}/{dtExportReport.Rows[0]["WardNumberWardName"]}";
                    LocationName = $"{dtExportReport.Rows[0]["Location"]}";
                }

                // Create Tables
                DataTable tblWardUpdatedRoll = new DataTable("tblWardUpdatedRoll");
                DataTable tblWardNewEnrollment = new DataTable("tblWardNewEnrollment");
                DataTable tblWardSummary = new DataTable("tblWardSummary");

                #region WardUpdatedRoll
                tblWardUpdatedRoll.Clear();
                var wardUpdatedRoll = from rows in dtExportReport.AsEnumerable()
                                      where (rows.Field<string>("Status") != "OLD")
                                         && (rows.Field<string>("Status") != "NEW")
                                         && (rows.Field<string>("Status") != "TRANSFERRED IN")
                                      select rows;

                if (wardUpdatedRoll != null && wardUpdatedRoll.Count() > 0)
                {
                    tblWardUpdatedRoll.Merge(wardUpdatedRoll.CopyToDataTable());

                    if (tblWardUpdatedRoll != null && tblWardUpdatedRoll.Rows != null && tblWardUpdatedRoll.Rows.Count > 0)
                    {
                        for (int i = 0; i < tblWardUpdatedRoll.Rows.Count; i++)
                        {
                            if (tblWardUpdatedRoll.Rows[i]["Seq"].ToString() == "0")
                            {
                                tblWardUpdatedRoll.Rows[i]["Seq"] = (i + 1);
                            }
                        }
                    }
                }
                #endregion

                #region WardNewEnrollment
                tblWardNewEnrollment.Clear();
                var WardNewEnrollment = from rows in dtExportReport.AsEnumerable()
                                        where (rows.Field<string>("Status") == "NEW")
                                           || (rows.Field<string>("Status") == "TRANSFERRED IN")
                                        select rows;
                if (WardNewEnrollment != null && WardNewEnrollment.Count() > 0)
                {
                    tblWardNewEnrollment.Merge(WardNewEnrollment.CopyToDataTable());

                    if (tblWardNewEnrollment != null && tblWardNewEnrollment.Rows != null && tblWardNewEnrollment.Rows.Count > 0)
                    {
                        for (int i = 0; i < tblWardNewEnrollment.Rows.Count; i++)
                        {
                            if (tblWardNewEnrollment.Rows[i]["Seq"].ToString() == "0")
                            {
                                tblWardNewEnrollment.Rows[i]["Seq"] = (i + 1);
                            }
                        }
                    }
                }
                #endregion

                #region WardSummary
                DateTime? CutOffDate = null;
                try
                {
                    CutOffDate = DateTime.ParseExact(Properties.Settings.Default.DefaultProvisionalCutOffDOB, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                }
                catch { }



                tblWardSummary.Clear();

                tblWardSummary.Columns.Add("New");
                tblWardSummary.Columns.Add("Updated");
                tblWardSummary.Columns.Add("Deceased");
                tblWardSummary.Columns.Add("Ghost");
                tblWardSummary.Columns.Add("Duplicate");
                tblWardSummary.Columns.Add("TranOut");
                tblWardSummary.Columns.Add("TranIn");
                tblWardSummary.Columns.Add("UnderAge");
                tblWardSummary.Columns.Add("Total");

                DataRow dtRow = tblWardSummary.NewRow();

                int New = dtExportReport.AsEnumerable().Count(row => row.Field<string>("Status") == "NEW");
                int Updated = dtExportReport.AsEnumerable().Count(row => row.Field<string>("Status") == "UPDATED");
                int Deceased = dtExportReport.AsEnumerable().Count(row => row.Field<string>("Status") == "DECEASED");
                int Ghost = dtExportReport.AsEnumerable().Count(row => row.Field<string>("Status") == "GHOST");
                int Duplicate = dtExportReport.AsEnumerable().Count(row => row.Field<string>("Status") == "DUPLICATE");
                int TranOut = dtExportReport.AsEnumerable().Count(row => row.Field<string>("Status") == "TRANSFERRED OUT" || row.Field<string>("Status") == "DELETED/ TRF OUT");
                int TranIn = dtExportReport.AsEnumerable().Count(row => row.Field<string>("Status") == "TRANSFERRED IN");
                int UnderAge = dtExportReport.AsEnumerable().Count(row => row.Field<string>("Status") == "UNDER AGE");

                dtRow["New"] = New;
                dtRow["Updated"] = Updated;
                dtRow["Deceased"] = Deceased;
                dtRow["Ghost"] = Ghost;
                dtRow["Duplicate"] = Duplicate;
                dtRow["TranOut"] = TranOut;
                dtRow["TranIn"] = TranIn;
                dtRow["UnderAge"] = UnderAge;
                dtRow["Total"] = (New + Updated + Deceased + Ghost + Duplicate + TranOut + TranIn + UnderAge);

                if (CutOffDate != null)
                {
                    tblWardNewEnrollment.AsEnumerable().Where(s => Convert.ToDateTime(s["DOB"]) != null && Convert.ToDateTime(s["DOB"]) == (CutOffDate)).ToList()
                        .ForEach(D => D.SetField("Status", "PROVISIONAL"));

                    tblWardUpdatedRoll.AsEnumerable().Where(s => Convert.ToDateTime(s["DOB"]) != null && Convert.ToDateTime(s["DOB"]) == (CutOffDate)).ToList()
                       .ForEach(D => D.SetField("Status", "PROVISIONAL"));
                }

                tblWardSummary.Rows.Add(dtRow);
                #endregion

                dsExportReport.Tables.AddRange(new DataTable[] { tblWardUpdatedRoll, tblWardNewEnrollment, tblWardSummary });
                bytest = WardStatusExportReportSource(WardName, LocationName);

                // Clear Tables
                tblWardUpdatedRoll.Clear();
                tblWardNewEnrollment.Clear();
                tblWardSummary.Clear();
            }

            return bytest;
        }
        #endregion

        #region Export Report Source   
        /// <summary>
        /// Exports the report source.
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <returns></returns>
        private byte[] WardStatusExportReportSource(string WardName, string LocationName)
        {
            byte[] bytes = null;

            if (dsExportReport.Tables != null && dsExportReport.Tables.Count > 0)
            {
                bool IsLocation = false;

                #region TransferredOut With Form11
                int TransferredOutWithForm11 = (from rows in dtExportReport.AsEnumerable()
                                                where (rows.Field<string>("Status") == "TRANSFERRED OUT" || rows.Field<string>("Status") == "DELETED/ TRF OUT")
                                                    && (rows.Field<int>("TransferInLocationId") > 0)
                                                select rows).Count();
                #endregion


                // isLocation
                if (MKATreeStructure.MainTagName == "Location")
                {
                    IsLocation = true;
                }

                Warning[] warnings;
                string[] streamIds;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;

                #region RDLC parameters
                int paramCount = 5;

                //This is optional if you have parameter then you can add parameters as much as you want
                ReportParameter[] param = new ReportParameter[paramCount];
                param[0] = new ReportParameter("User", $"{GlobalsModel.LoggedUser.User.FirstName} {GlobalsModel.LoggedUser.User.LastName}", true);
                param[1] = new ReportParameter("WardName", WardName, true);
                param[2] = new ReportParameter("LocationName", LocationName, true);
                param[3] = new ReportParameter("IsLocation", Convert.ToString(IsLocation), true);
                param[4] = new ReportParameter("TransferredOutWithForm11", Convert.ToString(TransferredOutWithForm11), true);
                #endregion

                ReportDataSource dataSourceWardUpdatedRoll = new ReportDataSource("ds_WardUpdatedRoll", dsExportReport.Tables[0].Copy());
                ReportDataSource datasourceWardNewEnrollment = new ReportDataSource("ds_WardNewEnrollment", dsExportReport.Tables[1].Copy());
                ReportDataSource datasourceWardSummary = new ReportDataSource("ds_WardSummary", dsExportReport.Tables[2].Copy());

                ReportViewer viewer = new ReportViewer();
                viewer.LocalReport.Refresh();
                viewer.LocalReport.ReportPath = "Report/WardStatus.rdlc"; //This is your rdlc name.
                viewer.LocalReport.SetParameters(param);

                viewer.LocalReport.DataSources.Add(dataSourceWardUpdatedRoll); // Add  datasource here
                viewer.LocalReport.DataSources.Add(datasourceWardNewEnrollment); // Add Deletion datasource here
                viewer.LocalReport.DataSources.Add(datasourceWardSummary); // Add Deletion datasource here

                bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
            }

            return bytes;
        }
        #endregion

        #region Field Tally Summary Sheet Export Report
        /// <summary>
        /// Fields the tally summary sheet export report.
        /// </summary>
        /// <returns></returns>
        private byte[] FieldTallySummarySheetExportReport()
        {
            byte[] bytest = null;

            if (dtExportReport != null && dtExportReport.Rows.Count > 0)
            {
                #region TransferredOut With MKA Form11
                int TransferredOutWithForm11 = (from rows in dtExportReport.AsEnumerable()
                                                where (rows.Field<string>("Status") == "TRANSFERRED OUT" || rows.Field<string>("Status") == "DELETED/ TRF OUT")
                                                    && (rows.Field<int>("TransferInMKALocationId") > 0)
                                                select rows).Count();
                #endregion

                string WardName = "", LocationName = "";

                if (MKATreeStructure.MainTagName == "MKAWard")
                {
                    WardName = $"{dtExportReport.Rows[0]["MKADistrict"]}/{dtExportReport.Rows[0]["MKALLG"]}/{dtExportReport.Rows[0]["MKAWardNumberWardName"]}";
                }
                else if (MKATreeStructure.MainTagName == "MKALocation")
                {
                    LocationName = $"{dtExportReport.Rows[0]["MKADistrict"]}/{dtExportReport.Rows[0]["MKALLG"]}/{dtExportReport.Rows[0]["MKAWardNumberWardName"]}/{dtExportReport.Rows[0]["MKALocation"]}";
                }

                // Create Tables
                DataTable tblEditables = new DataTable("tblEditables");
                DataTable tblNewEnrollments = new DataTable("tblNewEnrollments");
                DateTime? CutOffDate = null;
                int updated = 0, ghosts = 0, duplicates = 0, transferredOut = 0, deceased = 0, underAge = 0, newStatus = 0, withMKALLG = 0, fromDifferentMKALLG = 0, provisional = 0;
                int editTotal = 0, newEnrolmentsTotal = 0, grandTotal = 0;

                try
                {
                    CutOffDate = DateTime.ParseExact(Properties.Settings.Default.DefaultProvisionalCutOffDOB, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                }
                catch { }

                #region Editables
                tblEditables.Clear();
                var Editables = from rows in dtExportReport.AsEnumerable()
                                where (rows.Field<string>("Status") != "OLD")
                                    && (rows.Field<string>("Status") != "NEW")
                                    && (rows.Field<string>("Status") != "TRANSFERRED IN")
                                select rows;

                if (Editables != null && Editables.Count() > 0)
                {
                    tblEditables.Merge(Editables.CopyToDataTable());
                }
                #endregion

                #region NewEnrollments
                tblNewEnrollments.Clear();
                var NewEnrollments = from rows in dtExportReport.AsEnumerable()
                                     where (rows.Field<string>("Status") == "NEW")
                                         || (rows.Field<string>("Status") == "TRANSFERRED IN")
                                     select rows;
                if (NewEnrollments != null && NewEnrollments.Count() > 0)
                {
                    tblNewEnrollments.Merge(NewEnrollments.CopyToDataTable());
                }
                #endregion

                #region Editable records check with Cut off date and matched records are moved to underage or provisional

                #region Editables
                if (tblEditables != null && tblEditables.Rows != null && tblEditables.Rows.Count > 0)
                {
                    //if (CutOffDate != null)
                    //{
                    //    tblEditables.AsEnumerable().Where(s => Convert.ToDateTime(s["DOB"]) != null && Convert.ToDateTime(s["DOB"]) > CutOffDate).ToList()
                    //    .ForEach(D => D.SetField("Status", "UNDER AGE"));
                    //}

                    updated = tblEditables.AsEnumerable().Count(row => row.Field<string>("Status") == "UPDATED");
                    ghosts = tblEditables.AsEnumerable().Count(row => row.Field<string>("Status") == "GHOST");
                    duplicates = tblEditables.AsEnumerable().Count(row => row.Field<string>("Status") == "DUPLICATE");
                    transferredOut = tblEditables.AsEnumerable().Count(row => row.Field<string>("Status") == "TRANSFERRED OUT" || row.Field<string>("Status") == "DELETED/ TRF OUT");
                    deceased = tblEditables.AsEnumerable().Count(row => row.Field<string>("Status") == "DECEASED");
                    underAge = tblEditables.AsEnumerable().Count(row => row.Field<string>("Status") == "UNDER AGE");
                }
                editTotal = (updated + ghosts + duplicates + transferredOut + deceased + underAge);
                #endregion

                #region NewEnrollments
                if (tblNewEnrollments != null && tblNewEnrollments.Rows != null && tblNewEnrollments.Rows.Count > 0)
                {
                    if (CutOffDate != null)
                    {
                        tblNewEnrollments.AsEnumerable().Where(s => Convert.ToDateTime(s["DOB"]) != null && Convert.ToDateTime(s["DOB"]) == (CutOffDate)).ToList()
                            .ForEach(D => D.SetField("Status", "PROVISIONAL"));
                    }

                    //MKA District Transfer
                    //tblNewEnrollments.AsEnumerable().Where(s => Convert.ToInt32(s["TransferOutMKADistrictId"]) > 0 && Convert.ToInt32(s["MKADistrictId"]) == Convert.ToInt32(s["TransferOutMKADistrictId"])).ToList()
                    //    .ForEach(D => D.SetField("Status", "WITHMKADISTRICT"));

                    //tblNewEnrollments.AsEnumerable().Where(s => Convert.ToInt32(s["TransferOutMKADistrictId"]) > 0 && Convert.ToInt32(s["MKADistrictId"]) != Convert.ToInt32(s["TransferOutMKADistrictId"])).ToList()
                    //    .ForEach(D => D.SetField("Status", "FROMDIFFERENTMKADISTRICT"));

                    //MKA LLG Transfer
                    tblNewEnrollments.AsEnumerable().Where(s => Convert.ToInt32(s["TransferOutMKALLGId"]) > 0 && Convert.ToInt32(s["MKALLGId"]) == Convert.ToInt32(s["TransferOutMKALLGId"])).ToList()
                    .ForEach(D => D.SetField("Status", "WITHMKALLG"));

                    tblNewEnrollments.AsEnumerable().Where(s => Convert.ToInt32(s["TransferOutMKALLGId"]) > 0 && Convert.ToInt32(s["MKALLGId"]) != Convert.ToInt32(s["TransferOutMKALLGId"])).ToList()
                        .ForEach(D => D.SetField("Status", "FROMDIFFERENTMKALLG"));


                    newStatus = tblNewEnrollments.AsEnumerable().Count(row => row.Field<string>("Status") == "NEW");
                    withMKALLG = tblNewEnrollments.AsEnumerable().Count(row => row.Field<string>("Status") == "WITHMKALLG");
                    fromDifferentMKALLG = tblNewEnrollments.AsEnumerable().Count(row => row.Field<string>("Status") == "FROMDIFFERENTMKALLG");
                    provisional = tblNewEnrollments.AsEnumerable().Count(row => row.Field<string>("Status") == "PROVISIONAL");
                }
                newEnrolmentsTotal = (newStatus + withMKALLG + fromDifferentMKALLG + provisional);
                #endregion

                #region grandTotal
                grandTotal = (editTotal + newEnrolmentsTotal);
                #endregion

                #endregion

                tblEditables.Clear();
                tblNewEnrollments.Clear();
                CutOffDate = null;

                bool IsLocation = false;

                // isLocation
                if (MKATreeStructure.MainTagName == "MKALocation")
                {
                    IsLocation = true;
                }

                Warning[] warnings;
                string[] streamIds;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;

                #region RDLC parameters
                int paramCount = 22;

                //This is optional if you have parameter then you can add parameters as much as you want
                ReportParameter[] param = new ReportParameter[paramCount];
                param[0] = new ReportParameter("User", $"{GlobalsModel.LoggedUser.User.FirstName} {GlobalsModel.LoggedUser.User.LastName}", true);
                param[1] = new ReportParameter("WardName", WardName, true);
                param[2] = new ReportParameter("LocationName", LocationName, true);
                param[3] = new ReportParameter("IsLocation", Convert.ToString(IsLocation), true);
                param[4] = new ReportParameter("Ghosts", Convert.ToString(ghosts), true);
                param[5] = new ReportParameter("Duplicates", Convert.ToString(duplicates), true);
                param[6] = new ReportParameter("TransferredOut", Convert.ToString(transferredOut), true);
                param[7] = new ReportParameter("Deceased", Convert.ToString(deceased), true);
                param[8] = new ReportParameter("UnderAge", Convert.ToString(underAge), true);
                param[9] = new ReportParameter("NewStatus", Convert.ToString(newStatus), true);
                param[10] = new ReportParameter("WithMKALLG", Convert.ToString(withMKALLG), true);
                param[11] = new ReportParameter("FromDifferentMKALLG", Convert.ToString(fromDifferentMKALLG), true);
                param[12] = new ReportParameter("Provisional", Convert.ToString(provisional), true);
                param[13] = new ReportParameter("EditTotal", Convert.ToString(editTotal), true);
                param[14] = new ReportParameter("NewEnrolmentsTotal", Convert.ToString(newEnrolmentsTotal), true);
                param[15] = new ReportParameter("GrandTotal", Convert.ToString(grandTotal), true);
                param[16] = new ReportParameter("ProvinceName", Convert.ToString(dtExportReport.Rows[0]["Province"]), true);
                param[17] = new ReportParameter("DistrictName", Convert.ToString(dtExportReport.Rows[0]["MKADistrict"]), true);
                param[18] = new ReportParameter("LLGName", Convert.ToString(dtExportReport.Rows[0]["MKALLG"]), true);
                param[19] = new ReportParameter("WardNumberWardName", Convert.ToString(dtExportReport.Rows[0]["MKAWardNumberWardName"]), true);
                param[20] = new ReportParameter("Updated", Convert.ToString(updated), true);
                param[21] = new ReportParameter("TransferredOutWithForm11", Convert.ToString(TransferredOutWithForm11), true);

                #endregion

                ReportDataSource dataSourceFieldTallySummarySheet = new ReportDataSource("ds_MKAFieldTallySummarySheet", new DataTable());

                ReportViewer viewer = new ReportViewer();
                viewer.LocalReport.Refresh();
                viewer.LocalReport.ReportPath = "Report/MKAFieldTallySummarySheet.rdlc"; //This is your rdlc name.
                viewer.LocalReport.SetParameters(param);

                viewer.LocalReport.DataSources.Add(dataSourceFieldTallySummarySheet); // Add  datasource here

                bytest = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
            }

            return bytest;
        }
        #endregion

        #region ToDataTable List<T>
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

        #endregion
    }
}
