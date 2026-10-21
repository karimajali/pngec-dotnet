using Microsoft.Reporting.WinForms;
using PNGEC.API.Domain;
using PNGEC.API.Domain.Clan;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Domain.Mobile.Voter;
using PNGEC.API.Domain.Report;
using PNGEC.API.Helper;
using PNGEC.Desktop.Model;
using PNGEC.Desktop.Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;

namespace PNGEC.Desktop.Forms
{
    public partial class File : Form
    {
        #region variable declaration
        /// <summary>
        /// The header CheckBox
        /// </summary>
        private CheckBox headerCheckBox = new CheckBox();

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
        /// The filed array
        /// </summary>
        private string[] filedArray = { "Status", "SevisPass" };

        /// <summary>
        /// The SevisPass status filter array
        /// </summary>
        private string[] SevisPassStatusArray = { "ALL", "Verified", "Mismatch", "NotFound", "Pending" };

        /// <summary>
        /// The status array
        /// </summary>
        /// "UNDER AGE"
        private string[] StatusArray = { "ALL", "WARD ROLL", "OLD", "NEW", "UPDATED", "DECEASED", "TRANSFERRED OUT", "GHOST", "DELETED", "DUPLICATE", "TRANSFERRED IN", "UNDER AGE" };

        /// <summary>
        /// The status array ro
        /// </summary>
        private string[] StatusArrayTransferOutRO = { "TRANSFERRED OUT", "TRANSFER OUT with FORM 11", "TRANSFER OUT without FORM 11" };

        /// <summary>
        /// The status array duplicate ro
        /// </summary>
        private string[] StatusArrayDuplicateRO = { "DUPLICATE" };

        /// <summary>
        /// The status array final database
        /// </summary>
        private string[] StatusArrayFinalDB = { "OLD" };

        /// <summary>
        /// The status array transfer in ro
        /// </summary>
        private string[] StatusArrayTransferInRO = { "TRANSFERRED IN" };
        /// <summary>
        /// The gender array
        /// </summary>
        private string[] GenderArray = { "ALL", "MALE", "FEMALE", "OTHER" };

        /// <summary>
        /// The status array data migration report
        /// </summary>
        private string[] StatusArrayDataMigrationReport = { "ALL", "APPROVED", "REJECTED", "YET TO BE APPROVED" };

        /// <summary>
        /// The page name
        /// </summary>
        private string pageName = string.Empty;

        /// <summary>
        /// The request model
        /// </summary>
        private CommonRequest requestModel = null;

        /// <summary>
        /// The response
        /// </summary>
        private Response response = null;

        /// <summary>
        /// The ds
        /// </summary>
        private DataSet dsExportReport = null, dsVoterCard = null;

        /// <summary>
        /// The dt
        /// </summary>
        private DataTable dtExportReport = null, dtVoterCard = null, dtSubVoterCard = null, dtExportCsvReport = null, dtDeletionExportReport = null, dtTransferOut = null, dtExportCsvReportData = null, dtWardRollFrontCover = null, dtWardClanSectionSummary;

        /// <summary>
        /// The LST ward roll
        /// </summary>
        private List<WardRollBookReortModel> lstWardRoll = null;

        /// <summary>
        /// The LST ward roll
        /// </summary>
        private List<WardRollBookCsvReportModel> lstWardRollCsv = null;

        /// <summary>
        /// The LST deletion ward roll
        /// </summary>
        private List<WardRollBookReortModel> lstDeletionWardRoll = null;

        /// <summary>
        /// The export model
        /// </summary>
        private WardRollBookExportModel exportModel = null;

        /// <summary>
        /// The elector i ds
        /// </summary>
        private List<string> ElectorIDs = null;

        /// <summary>
        /// The rejected reason
        /// </summary>
        private RejectedReason rejectedReason = null;

        /// <summary>
        /// The lstvoters
        /// </summary>
        private List<VoterModel> lstvoters = null;

        /// <summary>
        /// The dg pages
        /// </summary>
        private int dgPages = 0;
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="File"/> class.
        /// </summary>
        public File()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="File"/> class.
        /// </summary>
        /// <param name="pageName">Name of the page.</param>
        public File(string pageName)
        {
            InitializeComponent();
            this.pageName = pageName;
        }
        #endregion

        #region Events

        /// <summary>
        /// Handles the Load event of the File control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void File_Load(object sender, EventArgs e)
        {
            //InitTreeData();

            headerCheckBox.Click -= new EventHandler(HeaderCheckBox_Clicked);
            headerCheckBox.Click += new EventHandler(HeaderCheckBox_Clicked);

            dgFile.CellContentClick -= new DataGridViewCellEventHandler(dgFile_CellClick);
            dgFile.CellContentClick += new DataGridViewCellEventHandler(dgFile_CellClick);

            dgFile.CellFormatting -= new DataGridViewCellFormattingEventHandler(dgFile_CellFormatting);
            dgFile.CellFormatting += new DataGridViewCellFormattingEventHandler(dgFile_CellFormatting);

            GlobalsModel.mainMaster.TreeViewSelectedChanged -= TreeViewSelectedChanged_AfterSelect;
            GlobalsModel.mainMaster.TreeViewSelectedChanged += TreeViewSelectedChanged_AfterSelect;

            FiledComboboxBind();

            if (GlobalsModel.LoggedUser.Database != CommonEnum.Database.Final && this.pageName == "WardRollBook")
            {
                cmbStatus.SelectedIndex = cmbStatus.FindStringExact("WARD ROLL");
            }

            ManagePanel();

            if (GlobalsModel.TreeViewStructure != null)
            {
                LoadGridViewSetting();
            }
        }

        /// <summary>
        /// Handles the AfterCollapse event of the tvGeoStructure control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="TreeViewEventArgs"/> instance containing the event data.</param>
        private void tvGeoStructure_AfterCollapse(object sender, TreeViewEventArgs e)
        {

        }

        /// <summary>
        /// Handles the AfterSelect event of the tvGeoStructure control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="TreeViewEventArgs"/> instance containing the event data.</param>
        private async void tvGeoStructure_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeStructure = (GeoTreeStructure)e.Node.Tag;
            await GridViewSetting();
            //GridViewSetting((GeoTreeStructure)e.Node.Tag);
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
            //txtSearch.Text = string.Empty;
            await GridViewSetting("Refresh");

            // Clear all checkbox
            if (Convert.ToString(cmbStatus.SelectedValue) != "OLD")
            {
                if ((this.pageName == "DataMigration"
                        && (GlobalsModel.LoggedUser.User.Role == 2 || GlobalsModel.LoggedUser.User.Role == 7 && GlobalsModel.LoggedUser.Database == CommonEnum.Database.Staging)
                        || (GlobalsModel.LoggedUser.User.Role == 3 && GlobalsModel.LoggedUser.Database == CommonEnum.Database.Preliminary)) || this.pageName == "WardRollBook")
                {
                    headerCheckBox.Checked = false;
                }
            }
        }

        /// <summary>
        /// Handles the Clicked event of the HeaderCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void HeaderCheckBox_Clicked(object sender, EventArgs e)
        {
            //Necessary to end the edit mode of the Cell.
            dgFile.EndEdit();

            //Loop and check and uncheck all row CheckBoxes based on Header Cell CheckBox.
            foreach (DataGridViewRow row in dgFile.Rows)
            {
                DataGridViewCheckBoxCell checkBox = (row.Cells["checkBoxColumn"] as DataGridViewCheckBoxCell);
                checkBox.Value = headerCheckBox.Checked;
            }
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
        /// Handles the SelectedIndexChanged event of the cmbFiled control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cmbFiled_SelectedIndexChanged(object sender, EventArgs e)
        {
            SatusComboboxBind();
        }

        /// <summary>
        /// Handles the Click event of the btnApprove control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnApprove_Click(object sender, EventArgs e)
        {
            ManageVoterStatus("Approve");
        }

        /// <summary>
        /// Handles the Click event of the btnReject control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnReject_Click(object sender, EventArgs e)
        {
            ManageVoterStatus("Reject");
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
                if (this.pageName == "WardRollBook")
                {
                    exportModel = new WardRollBookExportModel();
                    if (exportModel.ShowDialog() == DialogResult.OK)
                    {
                        if (exportModel.IsExportToCsv)
                        {
                            if (exportModel.rdbWardRollBook.Checked == true)
                            {
                                #region Ward Roll Book

                                string exportReportName = string.Empty;

                                #region filename
                                if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Staging && cmbStatus.Text != "DUPLICATE")
                                {
                                    exportReportName = GlobalsModel.reportName + "_Working_" + TreeStructure.MainTagName + "RollBook";
                                }
                                else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Preliminary && cmbStatus.Text != "DUPLICATE")
                                {
                                    exportReportName = GlobalsModel.reportName + "_Preliminary_" + TreeStructure.MainTagName + "RollBook";
                                }
                                else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Final && cmbStatus.Text != "DUPLICATE")
                                {
                                    exportReportName = GlobalsModel.reportName + "_Final_" + TreeStructure.MainTagName + "RollBook";
                                }
                                else if (cmbStatus.Text != "")
                                {
                                    exportReportName = TreeStructure.MainTagName + "RollBook_DuplicateVoters";
                                }
                                #endregion

                                var saveFileDialog = new SaveFileDialog();
                                saveFileDialog.FileName = exportReportName;
                                saveFileDialog.Filter = "CSV (*.csv)|*.csv";
                                saveFileDialog.DefaultExt = ".csv";
                                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                                {
                                    GlobalsModel.mainMaster.ShowLoader();

                                    await GetExportCsvRecordsData();

                                    if (dtExportCsvReport != null && dtExportCsvReport.Rows != null && dtExportCsvReport.Rows.Count > 0)
                                    {
                                        // Delete column
                                        string[] ColumnsToBeDeleted = { "Gender", "DOB" };
                                        foreach (string ColName in ColumnsToBeDeleted)
                                        {
                                            if (dtExportCsvReport.Columns.Contains(ColName))
                                                dtExportCsvReport.Columns.Remove(ColName);
                                        }

                                        // rename column
                                        dtExportCsvReport.Columns["ElectorId"].ColumnName = "ElectoralID";
                                        dtExportCsvReport.Columns["LocationId"].ColumnName = "LocationFK";
                                        dtExportCsvReport.Columns["LName"].ColumnName = "LastName";
                                        dtExportCsvReport.Columns["FName"].ColumnName = "FirstName";
                                        dtExportCsvReport.Columns["Gen"].ColumnName = "Gender";
                                        dtExportCsvReport.Columns["BDate"].ColumnName = "DateOfBirth";
                                        dtExportCsvReport.Columns["FatherLName"].ColumnName = "FatherLastName";
                                        dtExportCsvReport.Columns["FatherFName"].ColumnName = "FatherFirstName";
                                        dtExportCsvReport.Columns["MotherLName"].ColumnName = "MotherLastName";
                                        dtExportCsvReport.Columns["MotherFName"].ColumnName = "MotherFirstName";
                                        dtExportCsvReport.Columns["NatID"].ColumnName = "NationalID";
                                        dtExportCsvReport.Columns["ProvinceName"].ColumnName = "Province";
                                        dtExportCsvReport.Columns["LLGName"].ColumnName = "Llg";
                                        dtExportCsvReport.Columns["WardName"].ColumnName = "Ward";
                                        dtExportCsvReport.Columns["DistrictName"].ColumnName = "District";

                                        #region write
                                        StreamWriter sw = new StreamWriter(saveFileDialog.FileName, false);
                                        //headers    
                                        for (int i = 0; i < dtExportCsvReport.Columns.Count; i++)
                                        {
                                            sw.Write(dtExportCsvReport.Columns[i]);
                                            if (i < dtExportCsvReport.Columns.Count - 1)
                                            {
                                                sw.Write(",");
                                            }
                                        }
                                        sw.Write(sw.NewLine);
                                        //data
                                        foreach (DataRow dr in dtExportCsvReport.Rows)
                                        {
                                            for (int i = 0; i < dtExportCsvReport.Columns.Count; i++)
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
                                                if (i < dtExportCsvReport.Columns.Count - 1)
                                                {
                                                    sw.Write(",");
                                                }
                                            }
                                            sw.Write(sw.NewLine);
                                        }
                                        sw.Close();
                                        #endregion

                                        messageBoxText = string.Format("{0} Roll Book CSV is exported sucessfully at {1}", TreeStructure.MainTagName, saveFileDialog.FileName);
                                        MessageBox.Show(messageBoxText, "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else
                                    {
                                        MessageBox.Show("No record founds.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                }

                                #endregion
                            }
                        }
                        else
                        {
                            if (exportModel.rdbWardClanRoll.Checked == true)
                            {

                                #region Ward Clan Roll Book

                                #region PDF Report Name
                                string exportReportName = string.Empty, genderSplit = string.Empty;

                                if (exportModel.rdbGenderMale.Checked)
                                {
                                    genderSplit = "_" + exportModel.rdbGenderMale.Text + "_";
                                }
                                else if (exportModel.rdbGenderFemale.Checked)
                                {
                                    genderSplit = "_" + exportModel.rdbGenderFemale.Text + "_";
                                }
                                else if (exportModel.rdbGenderOther.Checked)
                                {
                                    genderSplit = "_" + exportModel.rdbGenderOther.Text + "_";
                                }
                                else
                                {
                                    genderSplit = "_";
                                }

                                if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Staging && cmbStatus.Text != "DUPLICATE")
                                {
                                    exportReportName = "Working_" + GlobalsModel.reportName + genderSplit + TreeStructure.MainTagName + "ClanRollBook";
                                }
                                else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Preliminary && cmbStatus.Text != "DUPLICATE")
                                {
                                    exportReportName = "Preliminary_" + GlobalsModel.reportName + genderSplit + TreeStructure.MainTagName + "ClanRollBook";
                                }
                                else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Final && cmbStatus.Text != "DUPLICATE")
                                {
                                    exportReportName = "Final_" + GlobalsModel.reportName + genderSplit + TreeStructure.MainTagName + "ClanRollBook";
                                }
                                else if (cmbStatus.Text != "")
                                {
                                    exportReportName = TreeStructure.MainTagName + "RollBook_DuplicateVoters";
                                }
                                #endregion

                                var saveFileDialog = new SaveFileDialog();
                                saveFileDialog.FileName = exportReportName;
                                saveFileDialog.DefaultExt = ".pdf";
                                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                                {
                                    GlobalsModel.mainMaster.ShowLoader();
                                    FileInfo fi = new FileInfo(saveFileDialog.FileName);
                                    string folderBasePath = saveFileDialog.FileName.Replace(fi.Name, string.Empty).Replace(".pdf", string.Empty);
                                    await GetExportRecordsData();

                                    if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Final)
                                    {
                                        await GetWardClanSectionSummary();
                                    }

                                    string WardRollMessage = string.Empty;
                                    if (exportModel.chkIncludePossibleDeletionReport.Checked)
                                    {
                                        await GetDeletionExportRecordsData();
                                    }
                                    else
                                    {
                                        dtDeletionExportReport = new DataTable();
                                    }

                                    if (dtExportReport != null && dtExportReport.Rows.Count > 0)
                                    {
                                        byte[] bytes = WardClanRollBookExportReport();
                                        //ExportGridToPDF(dgFile, "WardRollBook");

                                        using (FileStream stream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                                        {
                                            stream.Write(bytes, 0, bytes.Length);
                                        }

                                        WardRollMessage = string.Format("{0} Clan Roll Book is exported sucessfully at {1}", TreeStructure.MainTagName, saveFileDialog.FileName);
                                        //messageBoxText = string.Format("{0} Roll Book is exported sucessfully at {1}", TreeStructure.MainTagName, saveFileDialog.FileName);
                                        //MessageBox.Show(messageBoxText, "MKA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else
                                    {
                                        WardRollMessage = string.Format("{0} Clan Roll Book is No record founds.", TreeStructure.MainTagName);
                                        //MessageBox.Show("No record founds.", "MKA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }

                                    string WardRollEnvelopeMessage = string.Empty;
                                    if (exportModel.chkWardRollEnvelope.Checked)
                                    {
                                        #region VoterEnvelope

                                        int regionId, provinceId, districtId, llgId, wardId, locationId;
                                        string exportWardRollEnvelopeName = string.Empty;

                                        if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Staging)
                                        {
                                            exportWardRollEnvelopeName = "Working_" + GlobalsModel.reportName + "_" + TreeStructure.MainTagName + "RollEnvelope.pdf";
                                        }
                                        else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Preliminary)
                                        {
                                            exportWardRollEnvelopeName = "Preliminary_" + GlobalsModel.reportName + "_" + TreeStructure.MainTagName + "RollEnvelope.pdf";
                                        }
                                        else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Final)
                                        {
                                            exportWardRollEnvelopeName = "Final_" + GlobalsModel.reportName + "_" + TreeStructure.MainTagName + "RollEnvelope.pdf";
                                        }

                                        exportWardRollEnvelopeName = Path.Combine(folderBasePath, exportWardRollEnvelopeName);

                                        if (TreeStructure.MainTagName == "Ward")
                                        {
                                            var Ward = Globals.GeoList.WardList.Where(x => x.WardId == TreeStructure.Id).FirstOrDefault();
                                            regionId = Ward.RegionId;
                                            provinceId = Ward.ProvinceId;
                                            districtId = Ward.DistrictId;
                                            llgId = Ward.LLGId;
                                            wardId = Ward.WardId;
                                            locationId = 0;
                                        }
                                        else
                                        {
                                            var Location = Globals.GeoList.LocationList.Where(x => x.LocationId == TreeStructure.Id).FirstOrDefault();
                                            regionId = Location.RegionId;
                                            provinceId = Location.ProvinceId;
                                            districtId = Location.DistrictId;
                                            llgId = Location.LLGId;
                                            wardId = Location.WardId;
                                            locationId = Location.LocationId;
                                        }

                                        byte[] bytesWardRollEnvelope = VoterRollEnvelopeExportReportSource(regionId, provinceId, districtId, llgId, wardId, locationId);

                                        using (FileStream stream = new FileStream(exportWardRollEnvelopeName, FileMode.Create))
                                        {
                                            stream.Write(bytesWardRollEnvelope, 0, bytesWardRollEnvelope.Length);
                                        }

                                        WardRollEnvelopeMessage = string.Format("{0} RollEnvelope is exported sucessfully at {1}", TreeStructure.MainTagName, exportWardRollEnvelopeName);
                                        //messageBoxText = string.Format("{0} RollEnvelope is exported sucessfully at {1}", TreeStructure.MainTagName, exportWardRollEnvelopeName);
                                        //MessageBox.Show(messageBoxText, "MKA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        #endregion
                                    }

                                    string WardWardPreFieldWorkSummary = string.Empty;
                                    if (exportModel.chkWardPreFieldWorkSummary.Checked)
                                    {
                                        #region Pre-Field Work Summary

                                        string exportWardPreFieldWorkSummaryName = string.Empty;

                                        if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Staging)
                                        {
                                            exportWardPreFieldWorkSummaryName = "Working_" + GlobalsModel.reportName + "_" + TreeStructure.MainTagName + "PreFieldWorkSummary.pdf";
                                        }
                                        else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Preliminary)
                                        {
                                            exportWardPreFieldWorkSummaryName = "Preliminary_" + GlobalsModel.reportName + "_" + TreeStructure.MainTagName + "PreFieldWorkSummary.pdf";
                                        }
                                        else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Final)
                                        {
                                            exportWardPreFieldWorkSummaryName = "Final_" + GlobalsModel.reportName + "_" + TreeStructure.MainTagName + "PreFieldWorkSummary.pdf";
                                        }

                                        exportWardPreFieldWorkSummaryName = Path.Combine(folderBasePath, exportWardPreFieldWorkSummaryName);

                                        await GetExportRecordsData();

                                        if (dtExportReport != null && dtExportReport.Rows.Count > 0)
                                        {
                                            byte[] bytesPreFieldWorkSummary = WardRollPreFieldWorkSummary();

                                            using (FileStream stream = new FileStream(exportWardPreFieldWorkSummaryName, FileMode.Create))
                                            {
                                                stream.Write(bytesPreFieldWorkSummary, 0, bytesPreFieldWorkSummary.Length);
                                            }

                                            WardWardPreFieldWorkSummary = string.Format("{0} Roll Pre Field Work Summary is exported sucessfully at {1}", TreeStructure.MainTagName, exportWardPreFieldWorkSummaryName);
                                            //messageBoxText = string.Format("{0} Roll Book is exported sucessfully at {1}", TreeStructure.MainTagName, exportWardPreFieldWorkSummaryName);
                                            //MessageBox.Show(messageBoxText, "MKA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }
                                        else
                                        {
                                            WardWardPreFieldWorkSummary = string.Format("{0} Roll Pre Field Work Summary is No record founds.", TreeStructure.MainTagName);
                                            //MessageBox.Show("No record founds.", "MKA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        }
                                        #endregion
                                    }

                                    messageBoxText = string.Format("{0}{1}{2}{3}{4}{5}{6}",
                                       WardRollMessage,
                                       Environment.NewLine,
                                       Environment.NewLine,
                                       WardRollEnvelopeMessage,
                                       Environment.NewLine,
                                       Environment.NewLine,
                                       WardWardPreFieldWorkSummary);
                                    MessageBox.Show(messageBoxText, "MKA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }

                                #endregion
                            }
                            else if (exportModel.rdbWardRollBook.Checked == true)
                            {
                                #region Ward Roll Book

                                #region PDF Report Name
                                string exportReportName = string.Empty, genderSplit = string.Empty;

                                if (exportModel.rdbGenderMale.Checked)
                                {
                                    genderSplit = "_" + exportModel.rdbGenderMale.Text + "_";
                                }
                                else if (exportModel.rdbGenderFemale.Checked)
                                {
                                    genderSplit = "_" + exportModel.rdbGenderFemale.Text + "_";
                                }
                                else if (exportModel.rdbGenderOther.Checked)
                                {
                                    genderSplit = "_" + exportModel.rdbGenderOther.Text + "_";
                                }
                                else
                                {
                                    genderSplit = "_";
                                }

                                if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Staging && cmbStatus.Text != "DUPLICATE")
                                {
                                    exportReportName = "Working_" + GlobalsModel.reportName + genderSplit + TreeStructure.MainTagName + "RollBook";
                                }
                                else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Preliminary && cmbStatus.Text != "DUPLICATE")
                                {
                                    exportReportName = "Preliminary_" + GlobalsModel.reportName + genderSplit + TreeStructure.MainTagName + "RollBook";
                                }
                                else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Final && cmbStatus.Text != "DUPLICATE")
                                {
                                    exportReportName = "Final_" + GlobalsModel.reportName + genderSplit + TreeStructure.MainTagName + "RollBook";
                                }
                                else if (cmbStatus.Text != "")
                                {
                                    exportReportName = TreeStructure.MainTagName + "RollBook_DuplicateVoters";
                                }
                                #endregion

                                var saveFileDialog = new SaveFileDialog();
                                saveFileDialog.FileName = exportReportName;
                                saveFileDialog.DefaultExt = ".pdf";
                                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                                {
                                    GlobalsModel.mainMaster.ShowLoader();
                                    FileInfo fi = new FileInfo(saveFileDialog.FileName);
                                    string folderBasePath = saveFileDialog.FileName.Replace(fi.Name, string.Empty).Replace(".pdf", string.Empty);
                                    await GetExportRecordsData();

                                    string WardRollMessage = string.Empty;
                                    if (exportModel.chkIncludePossibleDeletionReport.Checked)
                                    {
                                        await GetDeletionExportRecordsData();
                                    }
                                    else
                                    {
                                        dtDeletionExportReport = new DataTable();
                                    }

                                    if (dtExportReport != null && dtExportReport.Rows.Count > 0)
                                    {
                                        byte[] bytes = WardRollBookExportReport();
                                        //ExportGridToPDF(dgFile, "WardRollBook");

                                        using (FileStream stream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                                        {
                                            stream.Write(bytes, 0, bytes.Length);
                                        }

                                        WardRollMessage = string.Format("{0} Roll Book is exported sucessfully at {1}", TreeStructure.MainTagName, saveFileDialog.FileName);
                                        //messageBoxText = string.Format("{0} Roll Book is exported sucessfully at {1}", TreeStructure.MainTagName, saveFileDialog.FileName);
                                        //MessageBox.Show(messageBoxText, "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else
                                    {
                                        WardRollMessage = string.Format("{0} Roll Book is No record founds.", TreeStructure.MainTagName);
                                        //MessageBox.Show("No record founds.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }

                                    string WardRollEnvelopeMessage = string.Empty;
                                    if (exportModel.chkWardRollEnvelope.Checked)
                                    {
                                        #region VoterEnvelope

                                        int regionId, provinceId, districtId, llgId, wardId, locationId;
                                        string exportWardRollEnvelopeName = string.Empty;

                                        if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Staging)
                                        {
                                            exportWardRollEnvelopeName = "Working_" + GlobalsModel.reportName + "_" + TreeStructure.MainTagName + "RollEnvelope.pdf";
                                        }
                                        else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Preliminary)
                                        {
                                            exportWardRollEnvelopeName = "Preliminary_" + GlobalsModel.reportName + "_" + TreeStructure.MainTagName + "RollEnvelope.pdf";
                                        }
                                        else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Final)
                                        {
                                            exportWardRollEnvelopeName = "Final_" + GlobalsModel.reportName + "_" + TreeStructure.MainTagName + "RollEnvelope.pdf";
                                        }

                                        exportWardRollEnvelopeName = Path.Combine(folderBasePath, exportWardRollEnvelopeName);

                                        if (TreeStructure.MainTagName == "Ward")
                                        {
                                            var Ward = Globals.GeoList.WardList.Where(x => x.WardId == TreeStructure.Id).FirstOrDefault();
                                            regionId = Ward.RegionId;
                                            provinceId = Ward.ProvinceId;
                                            districtId = Ward.DistrictId;
                                            llgId = Ward.LLGId;
                                            wardId = Ward.WardId;
                                            locationId = 0;
                                        }
                                        else
                                        {
                                            var Location = Globals.GeoList.LocationList.Where(x => x.LocationId == TreeStructure.Id).FirstOrDefault();
                                            regionId = Location.RegionId;
                                            provinceId = Location.ProvinceId;
                                            districtId = Location.DistrictId;
                                            llgId = Location.LLGId;
                                            wardId = Location.WardId;
                                            locationId = Location.LocationId;
                                        }

                                        byte[] bytesWardRollEnvelope = VoterRollEnvelopeExportReportSource(regionId, provinceId, districtId, llgId, wardId, locationId);

                                        using (FileStream stream = new FileStream(exportWardRollEnvelopeName, FileMode.Create))
                                        {
                                            stream.Write(bytesWardRollEnvelope, 0, bytesWardRollEnvelope.Length);
                                        }

                                        WardRollEnvelopeMessage = string.Format("{0} RollEnvelope is exported sucessfully at {1}", TreeStructure.MainTagName, exportWardRollEnvelopeName);
                                        //messageBoxText = string.Format("{0} RollEnvelope is exported sucessfully at {1}", TreeStructure.MainTagName, exportWardRollEnvelopeName);
                                        //MessageBox.Show(messageBoxText, "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        #endregion
                                    }

                                    string WardWardPreFieldWorkSummary = string.Empty;
                                    if (exportModel.chkWardPreFieldWorkSummary.Checked)
                                    {
                                        #region Pre-Field Work Summary

                                        string exportWardPreFieldWorkSummaryName = string.Empty;

                                        if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Staging)
                                        {
                                            exportWardPreFieldWorkSummaryName = "Working_" + GlobalsModel.reportName + "_" + TreeStructure.MainTagName + "PreFieldWorkSummary.pdf";
                                        }
                                        else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Preliminary)
                                        {
                                            exportWardPreFieldWorkSummaryName = "Preliminary_" + GlobalsModel.reportName + "_" + TreeStructure.MainTagName + "PreFieldWorkSummary.pdf";
                                        }
                                        else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Final)
                                        {
                                            exportWardPreFieldWorkSummaryName = "Final_" + GlobalsModel.reportName + "_" + TreeStructure.MainTagName + "PreFieldWorkSummary.pdf";
                                        }

                                        exportWardPreFieldWorkSummaryName = Path.Combine(folderBasePath, exportWardPreFieldWorkSummaryName);

                                        await GetExportRecordsData();

                                        if (dtExportReport != null && dtExportReport.Rows.Count > 0)
                                        {
                                            byte[] bytesPreFieldWorkSummary = WardRollPreFieldWorkSummary();

                                            using (FileStream stream = new FileStream(exportWardPreFieldWorkSummaryName, FileMode.Create))
                                            {
                                                stream.Write(bytesPreFieldWorkSummary, 0, bytesPreFieldWorkSummary.Length);
                                            }

                                            WardWardPreFieldWorkSummary = string.Format("{0} Roll Pre Field Work Summary is exported sucessfully at {1}", TreeStructure.MainTagName, exportWardPreFieldWorkSummaryName);
                                            //messageBoxText = string.Format("{0} Roll Book is exported sucessfully at {1}", TreeStructure.MainTagName, exportWardPreFieldWorkSummaryName);
                                            //MessageBox.Show(messageBoxText, "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }
                                        else
                                        {
                                            WardWardPreFieldWorkSummary = string.Format("{0} Roll Pre Field Work Summary is No record founds.", TreeStructure.MainTagName);
                                            //MessageBox.Show("No record founds.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        }
                                        #endregion
                                    }

                                    messageBoxText = string.Format("{0}{1}{2}{3}{4}{5}{6}",
                                       WardRollMessage,
                                       Environment.NewLine,
                                       Environment.NewLine,
                                       WardRollEnvelopeMessage,
                                       Environment.NewLine,
                                       Environment.NewLine,
                                       WardWardPreFieldWorkSummary);
                                    MessageBox.Show(messageBoxText, "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }

                                #endregion
                            }
                            else if (exportModel.rdbWardPhotoRollBook.Checked == true)
                            {
                                #region Ward Photo Roll Book

                                #region PDF Report Name
                                string exportReportName = string.Empty, genderSplit = string.Empty;

                                if (exportModel.rdbGenderMale.Checked)
                                {
                                    genderSplit = "_" + exportModel.rdbGenderMale.Text + "_";
                                }
                                else if (exportModel.rdbGenderFemale.Checked)
                                {
                                    genderSplit = "_" + exportModel.rdbGenderFemale.Text + "_";
                                }
                                else if (exportModel.rdbGenderOther.Checked)
                                {
                                    genderSplit = "_" + exportModel.rdbGenderOther.Text + "_";
                                }
                                else
                                {
                                    genderSplit = "_";
                                }

                                if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Staging && cmbStatus.Text != "DUPLICATE")
                                {
                                    exportReportName = "Working_" + GlobalsModel.reportName + genderSplit + TreeStructure.MainTagName + "PhotoRollBook";
                                }
                                else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Preliminary && cmbStatus.Text != "DUPLICATE")
                                {
                                    exportReportName = "Preliminary_" + GlobalsModel.reportName + genderSplit + TreeStructure.MainTagName + "PhotoRollBook";
                                }
                                else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Final && cmbStatus.Text != "DUPLICATE")
                                {
                                    exportReportName = "Final_" + GlobalsModel.reportName + genderSplit + TreeStructure.MainTagName + "PhotoRollBook";
                                }
                                else if (cmbStatus.Text != "")
                                {
                                    exportReportName = TreeStructure.MainTagName + "PhotoRollBook_DuplicateVoters";
                                }
                                #endregion

                                var saveFileDialog = new SaveFileDialog();
                                saveFileDialog.FileName = exportReportName;
                                saveFileDialog.DefaultExt = ".pdf";
                                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                                {
                                    GlobalsModel.mainMaster.ShowLoader();

                                    string WardPhotoRollMessage = string.Empty;
                                    string folderBasePath = saveFileDialog.FileName.Replace(exportReportName, string.Empty).Replace(".pdf", string.Empty);

                                    if (exportModel.rdbElectorByIDsSelected.Checked == true)
                                    {
                                        ElectorIDs = new List<string>();

                                        if (dgFile.Columns.Contains("checkBoxColumn"))
                                        {
                                            foreach (DataGridViewRow row in dgFile.Rows)
                                            {
                                                bool isSelected = Convert.ToBoolean(row.Cells["checkBoxColumn"].Value);
                                                if (isSelected)
                                                {
                                                    ElectorIDs.Add(row.Cells["ElectorId"].Value.ToString());
                                                }
                                            }
                                        }

                                        if (ElectorIDs != null && ElectorIDs.Count <= 0)
                                        {
                                            ElectorIDs.Add("0");
                                        }
                                    }

                                    await GetExportRecordsDataForWardPhoto();

                                    if (exportModel.chkIncludePossibleDeletionReport.Checked)
                                    {
                                        await GetDeletionExportRecordsData();
                                    }
                                    else
                                    {
                                        dtDeletionExportReport = new DataTable();
                                    }

                                    if (dtExportReport != null && dtExportReport.Rows.Count > 0)
                                    {
                                        byte[] bytes = WardPhotoRollBookExportReport();
                                        //ExportGridToPDF(dgFile, "WardRollBook");

                                        using (FileStream stream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                                        {
                                            stream.Write(bytes, 0, bytes.Length);
                                        }

                                        WardPhotoRollMessage = string.Format("{0} Photo Roll Book is exported sucessfully at {1}", TreeStructure.MainTagName, saveFileDialog.FileName);
                                        //messageBoxText = string.Format("{0} Photo Roll Book is exported sucessfully at {1}", TreeStructure.MainTagName, saveFileDialog.FileName);
                                        //MessageBox.Show(messageBoxText, "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else
                                    {
                                        WardPhotoRollMessage = string.Format("{0} Photo Roll Book is No record founds.", TreeStructure.MainTagName);
                                        //MessageBox.Show("No record founds.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }

                                    string WardRollEnvelopeMessage = string.Empty;
                                    if (exportModel.chkWardRollEnvelope.Checked)
                                    {
                                        #region VoterEnvelope

                                        int regionId, provinceId, districtId, llgId, wardId, locationId;
                                        string exportWardRollEnvelopeName = string.Empty;

                                        if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Staging)
                                        {
                                            exportWardRollEnvelopeName = "Working_" + GlobalsModel.reportName + "_" + TreeStructure.MainTagName + "RollEnvelope.pdf";
                                        }
                                        else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Preliminary)
                                        {
                                            exportWardRollEnvelopeName = "Preliminary_" + GlobalsModel.reportName + "_" + TreeStructure.MainTagName + "RollEnvelope.pdf";
                                        }
                                        else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Final)
                                        {
                                            exportWardRollEnvelopeName = "Final_" + GlobalsModel.reportName + "_" + TreeStructure.MainTagName + "RollEnvelope.pdf";
                                        }

                                        exportWardRollEnvelopeName = Path.Combine(folderBasePath, exportWardRollEnvelopeName);

                                        if (TreeStructure.MainTagName == "Ward")
                                        {
                                            var Ward = Globals.GeoList.WardList.Where(x => x.WardId == TreeStructure.Id).FirstOrDefault();
                                            regionId = Ward.RegionId;
                                            provinceId = Ward.ProvinceId;
                                            districtId = Ward.DistrictId;
                                            llgId = Ward.LLGId;
                                            wardId = Ward.WardId;
                                            locationId = 0;
                                        }
                                        else
                                        {
                                            var Location = Globals.GeoList.LocationList.Where(x => x.LocationId == TreeStructure.Id).FirstOrDefault();
                                            regionId = Location.RegionId;
                                            provinceId = Location.ProvinceId;
                                            districtId = Location.DistrictId;
                                            llgId = Location.LLGId;
                                            wardId = Location.WardId;
                                            locationId = Location.LocationId;
                                        }

                                        byte[] bytesWardRollEnvelope = VoterRollEnvelopeExportReportSource(regionId, provinceId, districtId, llgId, wardId, locationId);

                                        using (FileStream stream = new FileStream(exportWardRollEnvelopeName, FileMode.Create))
                                        {
                                            stream.Write(bytesWardRollEnvelope, 0, bytesWardRollEnvelope.Length);
                                        }

                                        WardRollEnvelopeMessage = string.Format("{0} RollEnvelope is exported sucessfully at {1}", TreeStructure.MainTagName, exportWardRollEnvelopeName);
                                        //messageBoxText = string.Format("{0} RollEnvelope is exported sucessfully at {1}", TreeStructure.MainTagName, exportWardRollEnvelopeName);
                                        //MessageBox.Show(messageBoxText, "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        #endregion
                                    }

                                    string WardWardPreFieldWorkSummary = string.Empty;
                                    if (exportModel.chkWardPreFieldWorkSummary.Checked)
                                    {
                                        #region Pre-Field Work Summary

                                        string exportWardPreFieldWorkSummaryName = string.Empty;

                                        if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Staging)
                                        {
                                            exportWardPreFieldWorkSummaryName = "Working_" + GlobalsModel.reportName + "_" + TreeStructure.MainTagName + "PreFieldWorkSummary.pdf";
                                        }
                                        else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Preliminary)
                                        {
                                            exportWardPreFieldWorkSummaryName = "Preliminary_" + GlobalsModel.reportName + "_" + TreeStructure.MainTagName + "PreFieldWorkSummary.pdf";
                                        }
                                        else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Final)
                                        {
                                            exportWardPreFieldWorkSummaryName = "Final_" + GlobalsModel.reportName + "_" + TreeStructure.MainTagName + "PreFieldWorkSummary.pdf";
                                        }

                                        exportWardPreFieldWorkSummaryName = Path.Combine(folderBasePath, exportWardPreFieldWorkSummaryName);

                                        await GetExportRecordsData();

                                        if (dtExportReport != null && dtExportReport.Rows.Count > 0)
                                        {
                                            byte[] bytesPreFieldWorkSummary = WardRollPreFieldWorkSummary();

                                            using (FileStream stream = new FileStream(exportWardPreFieldWorkSummaryName, FileMode.Create))
                                            {
                                                stream.Write(bytesPreFieldWorkSummary, 0, bytesPreFieldWorkSummary.Length);
                                            }

                                            WardWardPreFieldWorkSummary = string.Format("{0} Roll Pre Field Work Summary is exported sucessfully at {1}", TreeStructure.MainTagName, exportWardPreFieldWorkSummaryName);
                                            //messageBoxText = string.Format("{0} Roll Book is exported sucessfully at {1}", TreeStructure.MainTagName, exportWardPreFieldWorkSummaryName);
                                            //MessageBox.Show(messageBoxText, "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }
                                        else
                                        {
                                            WardWardPreFieldWorkSummary = string.Format("{0} Roll Pre Field Work Summary is No record founds.", TreeStructure.MainTagName);
                                            //MessageBox.Show("No record founds.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        }
                                        #endregion
                                    }

                                    messageBoxText = string.Format("{0}{1}{2}{3}{4}{5}{6}",
                                        WardPhotoRollMessage,
                                        Environment.NewLine,
                                        Environment.NewLine,
                                        WardRollEnvelopeMessage,
                                        Environment.NewLine,
                                        Environment.NewLine,
                                        WardWardPreFieldWorkSummary);
                                    MessageBox.Show(messageBoxText, "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }

                                #endregion
                            }
                            else if (exportModel.rdbVoterIdCard.Checked == true)
                            {
                                #region VoterIdCard
                                var saveFileDialog = new SaveFileDialog();
                                saveFileDialog.FileName = GlobalsModel.reportName + "_" + TreeStructure.MainTagName + "VoterIdCards";
                                saveFileDialog.DefaultExt = ".pdf";
                                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                                {
                                    GlobalsModel.mainMaster.ShowLoader();

                                    dsVoterCard = new DataSet();
                                    await GetExportRecordsData();

                                    if (dtExportReport != null && dtExportReport.Rows.Count > 0)
                                    {
                                        DataView view = new DataView(dtExportReport);
                                        dtVoterCard = view.ToTable("tblVoterIdCard", false, "ElectorId", "Seq");
                                        dtSubVoterCard = view.ToTable("tblSubVoterIdCard", false,
                                        "ElectorId",
                                        "Seq",
                                        "LName",
                                        "FName",
                                        "Gen",
                                        "Gender",
                                        "BDate",
                                        "WithDisability",
                                        "Province",
                                        "District",
                                        "LLG",
                                        "Ward",
                                        "Location",
                                        "ElectorProfileImageUrl",
                                        "NID",
                                        "NatIDRecordNo",
                                        "Religion",
                                        "Occupation",
                                        "WardNo",
                                        "ProvinceCode",
                                        "Clan");
                                        //dtSubVoterCard = view.ToTable("tblSubVoterIdCard", false, "ElectorId", "Seq", "LName", "FName", "Gen", "BDate", "WithDisability", "Province", "District", "LLG", "Ward", "Location");
                                        dsVoterCard.Tables.AddRange(new DataTable[] { dtVoterCard, dtSubVoterCard });

                                        if (dsVoterCard.Tables[0] != null && dsVoterCard.Tables[0].Rows.Count > 0)
                                        {
                                            byte[] bytes = VoterIdCardExportReportSource();

                                            using (FileStream stream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                                            {
                                                stream.Write(bytes, 0, bytes.Length);
                                            }

                                            messageBoxText = string.Format("{0} Voter Id Cards is exported sucessfully at {1}", TreeStructure.MainTagName, saveFileDialog.FileName);
                                            MessageBox.Show(messageBoxText, "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                            //VoterIdCardReport voterCard = new VoterIdCardReport(dsVoterCard);
                                            //voterCard.Show();
                                        }
                                        else
                                        {
                                            MessageBox.Show("No record founds.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("No record founds.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                }
                                #endregion
                            }
                            else if (exportModel.rdbVoterPhotoCard.Checked == true)
                            {
                                #region VoterPhotoCard
                                var saveFileDialog = new SaveFileDialog();
                                saveFileDialog.FileName = GlobalsModel.reportName + "_" + TreeStructure.MainTagName + "VoterPhotoCards";
                                saveFileDialog.DefaultExt = ".pdf";
                                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                                {
                                    GlobalsModel.mainMaster.ShowLoader();

                                    if (exportModel.rdbElectorByIDsSelected.Checked == true)
                                    {
                                        ElectorIDs = new List<string>();
                                        if (dgFile.Columns.Contains("checkBoxColumn"))
                                        {
                                            foreach (DataGridViewRow row in dgFile.Rows)
                                            {
                                                bool isSelected = Convert.ToBoolean(row.Cells["checkBoxColumn"].Value);
                                                if (isSelected)
                                                {
                                                    ElectorIDs.Add(row.Cells["ElectorId"].Value.ToString());
                                                }
                                            }
                                        }

                                        if (ElectorIDs != null && ElectorIDs.Count <= 0)
                                        {
                                            ElectorIDs.Add("0");
                                        }
                                    }

                                    dsVoterCard = new DataSet();
                                    await GetExportRecordsData();

                                    if (dtExportReport != null && dtExportReport.Rows.Count > 0)
                                    {
                                        DataView view = new DataView(dtExportReport);
                                        dtVoterCard = view.ToTable("tblVoterPhotoCard", false, "ElectorId", "Seq");
                                        dtSubVoterCard = view.ToTable("tblSubVoterPhotoCard", false,
                                            "ElectorId",
                                            "Seq",
                                            "LName",
                                            "FName",
                                            "Gen",
                                            "Gender",
                                            "BDate",
                                            "WithDisability",
                                            "Province",
                                            "District",
                                            "LLG",
                                            "Ward",
                                            "Location",
                                            "ElectorProfileImageUrl",
                                            "NID",
                                            "NatIDRecordNo",
                                            "Religion",
                                            "Occupation",
                                            "WardNo",
                                            "ProvinceCode",
                                            "Clan");
                                        dsVoterCard.Tables.AddRange(new DataTable[] { dtVoterCard, dtSubVoterCard });

                                        if (dsVoterCard.Tables[0] != null && dsVoterCard.Tables[0].Rows.Count > 0)
                                        {
                                            byte[] bytes = VoterPhotoCardExportReportSource();

                                            using (FileStream stream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                                            {
                                                stream.Write(bytes, 0, bytes.Length);
                                            }

                                            messageBoxText = string.Format("{0} Voter Photo Card is exported sucessfully at {1}", TreeStructure.MainTagName, saveFileDialog.FileName);
                                            MessageBox.Show(messageBoxText, "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                            //VoterPhotoCard voterPhotoCard = new VoterPhotoCard(dsVoterCard);
                                            //voterPhotoCard.Show();
                                        }
                                        else
                                        {
                                            MessageBox.Show("No record founds.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("No record founds.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                }
                                #endregion
                            }
                            else if (exportModel.rdbVoterEnvelope.Checked == true)
                            {
                                #region VoterEnvelope
                                GlobalsModel.mainMaster.ShowLoader();

                                var saveFileDialog = new SaveFileDialog();
                                saveFileDialog.FileName = GlobalsModel.reportName + "_" + TreeStructure.MainTagName + "RollEnvelope";
                                saveFileDialog.DefaultExt = ".pdf";
                                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                                {
                                    int regionId, provinceId, districtId, llgId, wardId, locationId;

                                    if (TreeStructure.MainTagName == "Ward")
                                    {
                                        var Ward = Globals.GeoList.WardList.Where(x => x.WardId == TreeStructure.Id).FirstOrDefault();
                                        regionId = Ward.RegionId;
                                        provinceId = Ward.ProvinceId;
                                        districtId = Ward.DistrictId;
                                        llgId = Ward.LLGId;
                                        wardId = Ward.WardId;
                                        locationId = 0;
                                    }
                                    else
                                    {
                                        var Location = Globals.GeoList.LocationList.Where(x => x.LocationId == TreeStructure.Id).FirstOrDefault();
                                        regionId = Location.RegionId;
                                        provinceId = Location.ProvinceId;
                                        districtId = Location.DistrictId;
                                        llgId = Location.LLGId;
                                        wardId = Location.WardId;
                                        locationId = Location.LocationId;
                                    }

                                    byte[] bytes = VoterRollEnvelopeExportReportSource(regionId, provinceId, districtId, llgId, wardId, locationId);

                                    using (FileStream stream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                                    {
                                        stream.Write(bytes, 0, bytes.Length);
                                    }

                                    messageBoxText = string.Format("{0} RollEnvelope is exported sucessfully at {1}", TreeStructure.MainTagName, saveFileDialog.FileName);
                                    MessageBox.Show(messageBoxText, "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                #endregion
                            }
                            else if (exportModel.rdbWardRollDeletion.Checked == true)
                            {
                                #region Ward Roll Deletion

                                string exportReportName = string.Empty;
                                if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Staging)
                                {
                                    exportReportName = GlobalsModel.reportName + "_Staging_" + TreeStructure.MainTagName + "Deletion";
                                }
                                else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Preliminary)
                                {
                                    exportReportName = GlobalsModel.reportName + "_Preliminary_" + TreeStructure.MainTagName + "Deletion";
                                }
                                else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Final)
                                {
                                    exportReportName = GlobalsModel.reportName + "_Final_" + TreeStructure.MainTagName + "Deletion";
                                }

                                var saveFileDialog = new SaveFileDialog();
                                saveFileDialog.FileName = exportReportName;
                                saveFileDialog.DefaultExt = ".pdf";
                                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                                {
                                    GlobalsModel.mainMaster.ShowLoader();

                                    await GetExportRecordsData();

                                    if (dtExportReport != null && dtExportReport.Rows.Count > 0)
                                    {
                                        byte[] bytes = WardRollDeletionExportReport();

                                        using (FileStream stream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                                        {
                                            stream.Write(bytes, 0, bytes.Length);
                                        }

                                        messageBoxText = string.Format("{0} Deletion is exported sucessfully at {1}", TreeStructure.MainTagName, saveFileDialog.FileName);
                                        MessageBox.Show(messageBoxText, "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else
                                    {
                                        MessageBox.Show("No record founds.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                }

                                #endregion
                            }
                            else if (exportModel.rdbPreFieldWorkSummary.Checked == true)
                            {
                                #region Pre-Field Work Summary

                                string exportReportName = string.Empty;
                                if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Staging)
                                {
                                    exportReportName = GlobalsModel.reportName + "_Working_" + TreeStructure.MainTagName + "PreFieldWorkSummary";
                                }
                                else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Preliminary)
                                {
                                    exportReportName = GlobalsModel.reportName + "_Preliminary_" + TreeStructure.MainTagName + "PreFieldWorkSummary";
                                }
                                else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Final)
                                {
                                    exportReportName = GlobalsModel.reportName + "_Final_" + TreeStructure.MainTagName + "PreFieldWorkSummary";
                                }

                                var saveFileDialog = new SaveFileDialog();
                                saveFileDialog.FileName = exportReportName;
                                saveFileDialog.DefaultExt = ".pdf";
                                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                                {
                                    GlobalsModel.mainMaster.ShowLoader();

                                    await GetExportRecordsData();

                                    if (dtExportReport != null && dtExportReport.Rows.Count > 0)
                                    {
                                        byte[] bytes = WardRollPreFieldWorkSummary();

                                        using (FileStream stream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                                        {
                                            stream.Write(bytes, 0, bytes.Length);
                                        }

                                        messageBoxText = string.Format("{0} Roll Book is exported sucessfully at {1}", TreeStructure.MainTagName, saveFileDialog.FileName);
                                        MessageBox.Show(messageBoxText, "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else
                                    {
                                        MessageBox.Show("No record founds.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                }

                                #endregion
                            }
                            else if (exportModel.rdbExisitngVoterDetails.Checked == true)
                            {
                                #region ExisitngVoterDetails

                                string exportReportName = string.Empty;
                                if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Staging)
                                {
                                    exportReportName = GlobalsModel.reportName + "_Working_" + TreeStructure.MainTagName + "ExisitngVoterDetails";
                                }
                                else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Preliminary)
                                {
                                    exportReportName = GlobalsModel.reportName + "_Preliminary_" + TreeStructure.MainTagName + "ExisitngVoterDetails";
                                }
                                else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Final)
                                {
                                    exportReportName = GlobalsModel.reportName + "_Final_" + TreeStructure.MainTagName + "ExisitngVoterDetails";
                                }

                                var saveFileDialog = new SaveFileDialog();
                                saveFileDialog.FileName = exportReportName;
                                saveFileDialog.DefaultExt = ".pdf";
                                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                                {
                                    GlobalsModel.mainMaster.ShowLoader();

                                    if (exportModel.rdbElectorByIDsSelected.Checked == true)
                                    {
                                        ElectorIDs = new List<string>();

                                        if (dgFile.Columns.Contains("checkBoxColumn"))
                                        {
                                            foreach (DataGridViewRow row in dgFile.Rows)
                                            {
                                                bool isSelected = Convert.ToBoolean(row.Cells["checkBoxColumn"].Value);
                                                if (isSelected)
                                                {
                                                    ElectorIDs.Add(row.Cells["ElectorId"].Value.ToString());
                                                }
                                            }
                                        }

                                        if (ElectorIDs != null && ElectorIDs.Count <= 0)
                                        {
                                            ElectorIDs.Add("0");
                                        }
                                    }

                                    dsExportReport = new DataSet();
                                    await GetExportRecordsData();

                                    if (dtExportReport != null && dtExportReport.Rows.Count > 0)
                                    {
                                        DataView view = new DataView(dtExportReport);
                                        dtVoterCard = view.ToTable("tblExisitngVoterDetails", false, "Seq", "ElectorId", "Province", "District", "LLG", "Ward");
                                        dtSubVoterCard = view.ToTable("tblSubExisitngVoterDetails", false,
                                        "ElectorId",
                                        "LName",
                                        "FName",
                                        "NatIDRecordNo",
                                        "Gender",
                                        "DateOfBirth",
                                        "PlaceOfBirth",
                                        "ProvOfBirthName",
                                        "Occupation",
                                        "WithDisability",
                                        "Clan",
                                        "Religion",
                                        "Location",
                                        "Seq",
                                        "Gen");
                                        dsExportReport.Tables.AddRange(new DataTable[] { dtVoterCard, dtSubVoterCard });

                                        if (dsExportReport.Tables[0] != null && dsExportReport.Tables[0].Rows.Count > 0)
                                        {
                                            byte[] bytes = WardExisitngVoterDetails();

                                            using (FileStream stream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                                            {
                                                stream.Write(bytes, 0, bytes.Length);
                                            }

                                            messageBoxText = string.Format("{0} Exisitng Voter Details is exported sucessfully at {1}", TreeStructure.MainTagName, saveFileDialog.FileName);
                                            MessageBox.Show(messageBoxText, "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }
                                        else
                                        {
                                            MessageBox.Show("No record founds.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("No record founds.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                }
                                #endregion
                            }
                        }
                    }
                }
                else //DataMigrationReport
                {
                    #region PDF Report Name
                    string exportReportName = string.Empty;

                    if (Convert.ToInt16(GlobalsModel.LoggedUser.Server) == 1)//Development
                    {
                        exportReportName = $"DEV_{GlobalsModel.LoggedUser.Database}_{GlobalsModel.reportName}_{TreeStructure.MainTagName}_DataMigration";
                    }
                    else if (Convert.ToInt16(GlobalsModel.LoggedUser.Server) == 2)//Training
                    {
                        exportReportName = $"TRAIN_{GlobalsModel.LoggedUser.Database}_{GlobalsModel.reportName}_{TreeStructure.MainTagName}_DataMigration";
                    }
                    else if (Convert.ToInt16(GlobalsModel.LoggedUser.Server) == 3)//Production
                    {
                        exportReportName = $"PROD_{GlobalsModel.LoggedUser.Database}_{GlobalsModel.reportName}_{TreeStructure.MainTagName}_DataMigration";
                    }

                    //if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Staging)
                    //{
                    //    exportReportName = $"Working_{GlobalsModel.reportName}_{TreeStructure.MainTagName}_DataMigration";
                    //}
                    //else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Preliminary)
                    //{
                    //    exportReportName = $"Preliminary_{GlobalsModel.reportName}_{TreeStructure.MainTagName}_DataMigration";
                    //}
                    #endregion

                    var saveFileDialog = new SaveFileDialog();
                    saveFileDialog.FileName = exportReportName;
                    saveFileDialog.DefaultExt = ".pdf";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        GlobalsModel.mainMaster.ShowLoader();

                        string folderBasePath = saveFileDialog.FileName.Replace(exportReportName, string.Empty).Replace(".pdf", string.Empty);
                        await GetDataMigrationReportData();

                        string WardRollMessage = string.Empty;

                        if (dtExportReport != null && dtExportReport.Rows.Count > 0)
                        {
                            byte[] bytes = DataMigrationExportReport();

                            using (FileStream stream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                            {
                                stream.Write(bytes, 0, bytes.Length);
                            }

                            WardRollMessage = string.Format("{0} data migration is exported sucessfully at {1}", TreeStructure.MainTagName, saveFileDialog.FileName);
                        }
                        else
                        {
                            WardRollMessage = string.Format("{0} data migration is No record founds.", TreeStructure.MainTagName);
                        }

                        MessageBox.Show(WardRollMessage, "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

            }
            catch (Exception ex)
            {
                messageBoxText = string.Empty;

                ErrorLog.Write("File", "btnExport_Click", ex);
                MessageBox.Show("Something went wrong", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (dtExportReport != null)
                    dtExportReport.Clear();

                if (dtDeletionExportReport != null)
                    dtDeletionExportReport.Clear();

                if (dsExportReport != null)
                    dsExportReport.Clear();

                if (dtSubVoterCard != null)
                    dtSubVoterCard.Clear();

                if (dtVoterCard != null)
                    dtVoterCard.Clear();

                if (dsVoterCard != null)
                    dsVoterCard.Clear();

                if (exportModel != null && exportModel.rdbElectorByIDsSelected.Checked == true)
                {
                    foreach (DataGridViewRow row in dgFile.Rows)
                    {
                        DataGridViewCheckBoxCell checkBox = (row.Cells["checkBoxColumn"] as DataGridViewCheckBoxCell);
                        checkBox.Value = false;
                    }

                    headerCheckBox.Checked = false;
                }

                GlobalsModel.mainMaster.HideLoader();

                messageBoxText = string.Empty;
            }
        }

        /// <summary>
        /// Handles the CellClick event of the dgFile control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs" /> instance containing the event data.</param>
        /// <exception cref="NotImplementedException"></exception>
        private async void dgFile_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Check to ensure that the row CheckBox is clicked.
            if (e.RowIndex >= 0 && dgFile.Columns[e.ColumnIndex].Name == "checkBoxColumn" && e.ColumnIndex == 0)
            {
                DataGridViewCheckBoxCell crntCheckBoxCell = (DataGridViewCheckBoxCell)this.dgFile.Rows[e.RowIndex].Cells["checkBoxColumn"];
                if (crntCheckBoxCell.Value == null)
                {
                    crntCheckBoxCell.Value = true;
                }
                else
                {
                    crntCheckBoxCell.Value = !(bool)crntCheckBoxCell.Value;
                }

                //Loop to verify whether all row CheckBoxes are checked or not.
                bool isChecked = true;
                foreach (DataGridViewRow row in dgFile.Rows)
                {
                    if (Convert.ToBoolean(row.Cells["checkBoxColumn"].EditedFormattedValue) == false)
                    {
                        isChecked = false;
                        break;
                    }
                }
                headerCheckBox.Checked = isChecked;
            }
            else if (e.RowIndex >= 0 && dgFile.Columns[e.ColumnIndex].Name == "View")
            {
                // Clear all checkbox
                if (Convert.ToString(cmbStatus.SelectedValue) != "OLD")
                {
                    if ((this.pageName == "DataMigration"
                        && (GlobalsModel.LoggedUser.User.Role == 7 && GlobalsModel.LoggedUser.Database == CommonEnum.Database.Staging)
                        || (GlobalsModel.LoggedUser.User.Role == 3 && GlobalsModel.LoggedUser.Database == CommonEnum.Database.Preliminary)) || this.pageName == "WardRollBook")
                    {
                        this.ClearCheckbox();
                    }
                }

                //VoterModel model = GetGridCellData(e.RowIndex);
                VoterModel model = (VoterModel)dgFile.Rows[e.RowIndex].DataBoundItem;
                if (model != null)
                {
                    VoterDetails user = new VoterDetails(model, this.pageName);
                    user.Width = (GlobalsModel.mainMaster.Width - 100);
                    user.Height = (GlobalsModel.mainMaster.Height - 100);
                    if (user.ShowDialog() == DialogResult.OK)
                    {
                        await GridViewSetting();
                    }
                }
            }
            else if (e.RowIndex >= 0 && dgFile.Columns[e.ColumnIndex].Name == "Edit")
            {
                // Clear all checkbox
                if (Convert.ToString(cmbStatus.SelectedValue) != "OLD")
                {
                    if ((this.pageName == "DataMigration"
                        && (GlobalsModel.LoggedUser.User.Role == 2 || GlobalsModel.LoggedUser.User.Role == 7 && GlobalsModel.LoggedUser.Database == CommonEnum.Database.Staging)
                        || (GlobalsModel.LoggedUser.User.Role == 3 && GlobalsModel.LoggedUser.Database == CommonEnum.Database.Preliminary)) || this.pageName == "WardRollBook")
                    {
                        this.ClearCheckbox();
                    }
                }

                VoterModel model = (VoterModel)dgFile.Rows[e.RowIndex].DataBoundItem;
                if (model != null && (model.Status == "OLD" || model.Status == "UPDATED"))
                {
                    if (GlobalsModel.LoggedUser.User.Role == 7 || GlobalsModel.LoggedUser.User.Role == 6 || GlobalsModel.LoggedUser.User.Role == 0)
                    {
                        ROVoterDetails roUser = new ROVoterDetails(model, this.pageName);
                        roUser.Width = (GlobalsModel.mainMaster.Width - 100);
                        roUser.Height = (GlobalsModel.mainMaster.Height - 100);
                        if (roUser.ShowDialog() == DialogResult.OK)
                        {
                            await GridViewSetting();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("You can edit only OLD and UPDATED status data", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else if (e.RowIndex >= 0 && dgFile.Columns[e.ColumnIndex].Name == "Transfer")
            {
                // Clear all checkbox
                if (Convert.ToString(cmbStatus.SelectedValue) != "OLD")
                {
                    if ((this.pageName == "DataMigration"
                        && (GlobalsModel.LoggedUser.User.Role == 2 || GlobalsModel.LoggedUser.User.Role == 7 && GlobalsModel.LoggedUser.Database == CommonEnum.Database.Staging)
                        || (GlobalsModel.LoggedUser.User.Role == 3 && GlobalsModel.LoggedUser.Database == CommonEnum.Database.Preliminary)) || this.pageName == "WardRollBook")
                    {
                        this.ClearCheckbox();
                    }
                }

                VoterModel model = (VoterModel)dgFile.Rows[e.RowIndex].DataBoundItem;
                if (model != null && (model.Status == "OLD" || model.Status == "UPDATED"))
                {
                    if (GlobalsModel.LoggedUser.User.Role == 7 || GlobalsModel.LoggedUser.User.Role == 6 || GlobalsModel.LoggedUser.User.Role == 0)
                    {
                        lstvoters = new List<VoterModel>();
                        lstvoters.Add(model);
                        TransferVoter transferVoter = new TransferVoter(lstvoters, this.pageName);
                        //transferVoter.Width = (GlobalsModel.mainMaster.Width - 100);
                        //transferVoter.Height = (GlobalsModel.mainMaster.Height - 100);
                        if (transferVoter.ShowDialog() == DialogResult.OK)
                        {
                            await GridViewSetting();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("You can transfer only OLD and UPDATED status data", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        /// <summary>
        /// Handles the AfterSelect event of the TreeViewSelectedChanged control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        public void TreeViewSelectedChanged_AfterSelect(object sender, EventArgs e)
        {
            LoadGridViewSetting();
        }

        /// <summary>
        /// Handles the FormClosing event of the File control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
        private void File_FormClosing(object sender, FormClosingEventArgs e)
        {
            GlobalsModel.mainMaster.TreeViewSelectedChanged -= TreeViewSelectedChanged_AfterSelect;
        }

        /// <summary>
        /// Handles the CellPainting event of the dgFile control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellPaintingEventArgs"/> instance containing the event data.</param>
        private void dgFile_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex == 0)
            {
                ResetHeaderCheckBoxLocation(e.ColumnIndex, e.RowIndex);
            }
        }

        /// <summary>
        /// Handles the Scroll event of the dgFile control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ScrollEventArgs"/> instance containing the event data.</param>
        private void dgFile_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation.Equals(ScrollOrientation.HorizontalScroll))
            {
                headerCheckBox.Location = new Point(headerCheckBox.Location.X - (e.NewValue - e.OldValue), headerCheckBox.Location.Y);
            }
            if (headerCheckBox.Location.X < dgFile.Location.X + 40)
            {
                headerCheckBox.Visible = false;
            }
            else
            {
                headerCheckBox.Visible = true;
            }
        }

        /// <summary>
        /// Handles the Click event of the btnExportToCSV control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private async void btnExportToCSV_Click(object sender, EventArgs e)
        {
            try
            {
                string exportReportName = string.Empty;
                string messageBoxText = string.Empty;

                if (this.pageName == "TransferOut")
                {
                    #region filename
                    if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Staging)
                    {
                        exportReportName = GlobalsModel.reportName + "_Working_" + TreeStructure.MainTagName + "_TransferOut";
                    }
                    else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Preliminary)
                    {
                        exportReportName = GlobalsModel.reportName + "_Preliminary_" + TreeStructure.MainTagName + "_TransferOut";
                    }
                    else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Final)
                    {
                        exportReportName = GlobalsModel.reportName + "_Final_" + TreeStructure.MainTagName + "_TransferOut";
                    }
                    #endregion
                }
                else if (this.pageName == "Duplicate")
                {
                    #region filename
                    if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Staging)
                    {
                        exportReportName = GlobalsModel.reportName + "_Working_" + TreeStructure.MainTagName + "_Duplicate";
                    }
                    else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Preliminary)
                    {
                        exportReportName = GlobalsModel.reportName + "_Preliminary_" + TreeStructure.MainTagName + "_Duplicate";
                    }
                    else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Final)
                    {
                        exportReportName = GlobalsModel.reportName + "_Final_" + TreeStructure.MainTagName + "_Duplicate";
                    }
                    #endregion
                }
                else if (this.pageName == "WardRollBook" && btnExportToCSV.Visible == true)
                {
                    //#region filename
                    //if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Staging)
                    //{
                    //    exportReportName = GlobalsModel.reportName + "_Working_" + TreeStructure.MainTagName + "_WardRollBook";
                    //}
                    //else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Preliminary)
                    //{
                    //    exportReportName = GlobalsModel.reportName + "_Preliminary_" + TreeStructure.MainTagName + "_WardRollBook";
                    //}
                    //else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Final)
                    //{
                    //    exportReportName = GlobalsModel.reportName + "_Final_" + TreeStructure.MainTagName + "_WardRollBook";
                    //}
                    //#endregion
                    exportReportName = $"{GlobalsModel.LoggedUser.Server}-{GlobalsModel.LoggedUser.Database}-{Convert.ToString(cmbStatus.SelectedValue)}-{GlobalsModel.reportName}";
                }

                var saveFileDialog = new SaveFileDialog();
                saveFileDialog.FileName = exportReportName;
                saveFileDialog.Filter = "CSV (*.csv)|*.csv";
                saveFileDialog.DefaultExt = ".csv";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    GlobalsModel.mainMaster.ShowLoader();

                    if (this.pageName == "WardRollBook" && btnExportToCSV.Visible == true)
                    {
                        await GetWardRollStatusCountExportCsvData();
                    }

                    if (dtExportCsvReportData?.Rows?.Count > 0)
                    {
                        if (this.pageName == "TransferOut")
                        {
                            #region TransferOut
                            DataView view = new DataView(dtExportCsvReportData);
                            dtTransferOut = view.ToTable(false, "RowNo",
                                "VoterName",
                                "ElectorId",
                                "Gen",
                                "DateOfBirth",
                                "Status",
                                "ProvinceName",
                                "DistrictName",
                                "LLGName",
                                "WardName",
                                "LocationName",
                                "EnrolmentFormNumber",

                                "transferInProvinceName",
                                "transferInDistrictName",
                                "transferInLLGName",
                                "transferInWardName",
                                "transferInLocationName",

                                "transferOutProvinceName",
                                "transferOutDistrictName",
                                "transferOutLLGName",
                                "transferOutWardName",
                                "transferOutLocationName",
                                "RecordType");

                            // rename column
                            dtTransferOut.Columns["RowNo"].ColumnName = "Row No";
                            dtTransferOut.Columns["VoterName"].ColumnName = "Voter Name";
                            dtTransferOut.Columns["ElectorId"].ColumnName = "Elector Id";
                            dtTransferOut.Columns["Gen"].ColumnName = "Gender";
                            dtTransferOut.Columns["DateOfBirth"].ColumnName = "DOB";
                            dtTransferOut.Columns["Status"].ColumnName = "Status";
                            dtTransferOut.Columns["ProvinceName"].ColumnName = "Province Name";
                            dtTransferOut.Columns["DistrictName"].ColumnName = "District Name";
                            dtTransferOut.Columns["LLGName"].ColumnName = "LLG Name";
                            dtTransferOut.Columns["WardName"].ColumnName = "Ward Name";
                            dtTransferOut.Columns["LocationName"].ColumnName = "Location Name";
                            dtTransferOut.Columns["EnrolmentFormNumber"].ColumnName = "Enrolment Form Number";

                            dtTransferOut.Columns["transferInProvinceName"].ColumnName = "TransferIn Province Name";
                            dtTransferOut.Columns["transferInDistrictName"].ColumnName = "TransferIn District Name";
                            dtTransferOut.Columns["transferInLLGName"].ColumnName = "TransferIn LLG Name";
                            dtTransferOut.Columns["transferInWardName"].ColumnName = "TransferIn Ward Name";
                            dtTransferOut.Columns["transferInLocationName"].ColumnName = "TransferIn Location Name";

                            dtTransferOut.Columns["transferOutProvinceName"].ColumnName = "TransferOut Province Name";
                            dtTransferOut.Columns["transferOutDistrictName"].ColumnName = "TransferOut District Name";
                            dtTransferOut.Columns["transferOutLLGName"].ColumnName = "TransferOut LLG Name";
                            dtTransferOut.Columns["transferOutWardName"].ColumnName = "TransferOut Ward Name";
                            dtTransferOut.Columns["transferOutLocationName"].ColumnName = "TransferOut Location Name";

                            dtTransferOut.Columns["RecordType"].ColumnName = "Record Type";
                            #endregion
                        }
                        else if (this.pageName == "Duplicate")
                        {
                            #region Duplicate
                            DataView view = new DataView(dtExportCsvReportData);
                            dtTransferOut = view.ToTable(false, "RowNo",
                                "VoterName",
                                "ElectorId",
                                "Gen",
                                "DateOfBirth",
                                "Status",
                                "ProvinceName",
                                "DistrictName",
                                "LLGName",
                                "WardName",
                                "LocationName",
                                "DuplicateElectorId",
                                "DuplicateVoterName",
                                "DuplicateGen",
                                "DuplicateDateOfBirth",
                                "DuplicateOccupation",
                                "DuplicateProvinceName",
                                "DuplicateDistrictName",
                                "DuplicateLLGName",
                                "DuplicateWardName",
                                "DuplicateLocationName",
                                "RecordType");

                            // rename column
                            dtTransferOut.Columns["RowNo"].ColumnName = "Row No";
                            dtTransferOut.Columns["VoterName"].ColumnName = "Voter Name";
                            dtTransferOut.Columns["ElectorId"].ColumnName = "Elector Id";
                            dtTransferOut.Columns["Gen"].ColumnName = "Gender";
                            dtTransferOut.Columns["DateOfBirth"].ColumnName = "DOB";
                            dtTransferOut.Columns["Status"].ColumnName = "Status";
                            dtTransferOut.Columns["ProvinceName"].ColumnName = "Province Name";
                            dtTransferOut.Columns["DistrictName"].ColumnName = "District Name";
                            dtTransferOut.Columns["LLGName"].ColumnName = "LLG Name";
                            dtTransferOut.Columns["WardName"].ColumnName = "Ward Name";
                            dtTransferOut.Columns["LocationName"].ColumnName = "Location Name";
                            dtTransferOut.Columns["DuplicateElectorId"].ColumnName = "Duplicate Elector Id";
                            dtTransferOut.Columns["DuplicateVoterName"].ColumnName = "Duplicate Voter Name";
                            dtTransferOut.Columns["DuplicateGen"].ColumnName = "Duplicate Gender";
                            dtTransferOut.Columns["DuplicateDateOfBirth"].ColumnName = "Duplicate DOB";
                            dtTransferOut.Columns["DuplicateOccupation"].ColumnName = "Duplicate Occupation";
                            dtTransferOut.Columns["DuplicateProvinceName"].ColumnName = "Duplicate Province Name";
                            dtTransferOut.Columns["DuplicateDistrictName"].ColumnName = "Duplicate District Name";
                            dtTransferOut.Columns["DuplicateLLGName"].ColumnName = "Duplicate LLG Name";
                            dtTransferOut.Columns["DuplicateWardName"].ColumnName = "Duplicate Ward Name";
                            dtTransferOut.Columns["DuplicateLocationName"].ColumnName = "Duplicate Location Name";
                            dtTransferOut.Columns["RecordType"].ColumnName = "Record Type";
                            #endregion
                        }
                        else if (this.pageName == "WardRollBook")
                        {
                            #region WardRollBook
                            int provinceId = 0;

                            if (TreeStructure.MainTagName == "LLG")
                            {
                                provinceId = Globals.GeoList.LLGList.Where(x => x.LLGId == TreeStructure.Id).Select(s => s.ProvinceId).FirstOrDefault();
                            }
                            else if (TreeStructure.MainTagName == "District")
                            {
                                provinceId = Globals.GeoList.DistrictList.Where(x => x.DistrictId == TreeStructure.Id).Select(s => s.ProvinceId).FirstOrDefault();
                            }
                            else if (TreeStructure.MainTagName == "Province")
                            {
                                provinceId = TreeStructure.Id;
                            }

                            if (provinceId > 0 && provinceId == 13) //13 is NCD Province so display location
                            {
                                DataView view = new DataView(dtExportCsvReportData);
                                dtTransferOut = view.ToTable(false, "Province",
                                    "District",
                                    "LLG",
                                    "WardNo",
                                    "Ward",
                                    "Location",
                                    "Status",
                                    "Count");

                                // rename column
                                dtTransferOut.Columns["Province"].ColumnName = "Province Name";
                                dtTransferOut.Columns["District"].ColumnName = "District Name";
                                dtTransferOut.Columns["LLG"].ColumnName = "LLG Name";
                                dtTransferOut.Columns["WardNo"].ColumnName = "Ward Number";
                                dtTransferOut.Columns["Ward"].ColumnName = "Ward Name";
                                dtTransferOut.Columns["Location"].ColumnName = "Location Name";
                                dtTransferOut.Columns["Status"].ColumnName = "Status";
                                dtTransferOut.Columns["Count"].ColumnName = "Voters";
                            }
                            else
                            {
                                DataView view = new DataView(dtExportCsvReportData);
                                dtTransferOut = view.ToTable(false, "Province",
                                    "District",
                                    "LLG",
                                    "WardNo",
                                    "Ward",
                                    "Status",
                                    "Count");

                                // rename column
                                dtTransferOut.Columns["Province"].ColumnName = "Province Name";
                                dtTransferOut.Columns["District"].ColumnName = "District Name";
                                dtTransferOut.Columns["LLG"].ColumnName = "LLG Name";
                                dtTransferOut.Columns["WardNo"].ColumnName = "Ward Number";
                                dtTransferOut.Columns["Ward"].ColumnName = "Ward Name";
                                dtTransferOut.Columns["Status"].ColumnName = "Status";
                                dtTransferOut.Columns["Count"].ColumnName = "Voters";
                            }

                            #endregion
                        }

                        #region write
                        StreamWriter sw = new StreamWriter(saveFileDialog.FileName, false);
                        //headers    
                        for (int i = 0; i < dtTransferOut.Columns.Count; i++)
                        {
                            sw.Write(dtTransferOut.Columns[i]);
                            if (i < dtTransferOut.Columns.Count - 1)
                            {
                                sw.Write(",");
                            }
                        }
                        sw.Write(sw.NewLine);
                        //data
                        foreach (DataRow dr in dtTransferOut.Rows)
                        {
                            for (int i = 0; i < dtTransferOut.Columns.Count; i++)
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
                                if (i < dtTransferOut.Columns.Count - 1)
                                {
                                    sw.Write(",");
                                }
                            }
                            sw.Write(sw.NewLine);
                        }
                        sw.Close();
                        #endregion

                        messageBoxText = $"{TreeStructure.MainTagName} {this.pageName} CSV is exported sucessfully at {saveFileDialog.FileName}";
                        //messageBoxText = string.Format("{0} {1} CSV is exported sucessfully at {2}", TreeStructure.MainTagName, this.pageName, saveFileDialog.FileName);
                        MessageBox.Show(messageBoxText, "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No record founds.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    GlobalsModel.mainMaster.HideLoader();
                }
            }
            catch (Exception ex)
            {
                GlobalsModel.mainMaster.HideLoader();
                MessageBox.Show(Resources.ErrorMessage, "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ErrorLog.Write("File", "btnExportToCSV_Click", ex);
            }
            finally
            {
                //if (dtExportCsvReportData != null)
                //    dtExportCsvReportData.Clear();
            }
        }

        /// <summary>
        /// Handles the Click event of the btnTransfer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private async void btnTransfer_Click(object sender, EventArgs e)
        {
            lstvoters = new List<VoterModel>();

            if (dgFile.Columns.Contains("checkBoxColumn"))
            {
                foreach (DataGridViewRow row in dgFile.Rows)
                {
                    bool isSelected = Convert.ToBoolean(row.Cells["checkBoxColumn"].Value);
                    if (isSelected)
                    {
                        VoterModel model = (VoterModel)row.DataBoundItem;
                        if (model != null && (model.Status == "OLD" || model.Status == "UPDATED") && (GlobalsModel.LoggedUser.User.Role == 7 || GlobalsModel.LoggedUser.User.Role == 6 || GlobalsModel.LoggedUser.User.Role == 0))
                        {
                            lstvoters.Add(model);
                        }
                    }
                }
            }

            if (lstvoters != null && lstvoters.Count > 0)
            {
                TransferVoter transferVoter = new TransferVoter(lstvoters, this.pageName);
                if (transferVoter.ShowDialog() == DialogResult.OK)
                {
                    await GridViewSetting();
                }
            }
            else
            {
                MessageBox.Show("At least one OLD and UPDATED status records checkbox must be selected.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //MessageBox.Show("You can transfer only OLD and UPDATED status data.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //MessageBox.Show("At least one checkbox must be selected.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.txtFName.Text = string.Empty;
            this.txtLName.Text = string.Empty;
            if (this.cmbGender.Items != null && this.cmbGender.Items.Count > 0)
            {
                this.cmbGender.SelectedIndex = 0;
            }

            if (this.cmbApproval.Items != null && this.cmbApproval.Items.Count > 0)
            {
                this.cmbApproval.SelectedIndex = 0;
            }

            if (this.cmbClan.Items != null && this.cmbClan.Items.Count > 0)
            {
                this.cmbClan.SelectedIndex = 0;
            }

            ////await GridViewSetting("Refresh");
        }
        #endregion

        #region Methods

        #region Load Grid View Setting
        /// <summary>
        /// Loads the grid view setting.
        /// </summary>
        private async void LoadGridViewSetting()
        {
            TreeStructure = GlobalsModel.TreeViewStructure;
            await GridViewSetting("AfterSelect");
        }
        #endregion

        #region ManagePanel
        /// <summary>
        /// Manages the panel.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void ManagePanel()
        {
            if (((this.pageName == "DataMigration" || this.pageName == "TransferOut" || this.pageName == "Duplicate" || this.pageName == "TransferInApproval") &&
                (GlobalsModel.LoggedUser.User.Role == 7 && GlobalsModel.LoggedUser.Database == CommonEnum.Database.Staging) ||
                (GlobalsModel.LoggedUser.User.Role == 3 && GlobalsModel.LoggedUser.Database == CommonEnum.Database.Preliminary)) &&
                Convert.ToString(cmbStatus.SelectedValue) != "OLD" &&
                Convert.ToString(cmbStatus.SelectedValue) != "ALL" &&
                Convert.ToString(cmbStatus.SelectedValue) != "WARD ROLL")
            {
                pnlExport.Visible = false;
                pnlApproveReject.Visible = true;
                pnlApproval.Visible = true;

                if (this.pageName == "TransferOut" || this.pageName == "Duplicate")
                {
                    btnExportToCSV.Visible = true;
                }
                else
                {
                    btnExportToCSV.Visible = false;
                }
            }
            else if (this.pageName == "WardRollBook" || this.pageName == "DataMigrationReport")
            {
                chkLookUp.Checked = true;
                pnlApproveReject.Visible = false;
                pnlApproval.Visible = false;

                //if (GlobalsModel.TreeViewStructure.MainTagName == "Ward" || GlobalsModel.TreeViewStructure.MainTagName == "LLG" || GlobalsModel.TreeViewStructure.MainTagName == "District")
                if (GlobalsModel.TreeViewStructure.MainTagName == "Ward" || GlobalsModel.TreeViewStructure.MainTagName == "Location")
                {
                    pnlExport.Visible = true;
                }
                else
                {
                    pnlExport.Visible = false;
                }

                btnExportToCSV.Visible = false;

                if (this.pageName == "WardRollBook")
                {
                    #region Export CSV report for Geo(Country), Region, Province, District and LLG 
                    if (GlobalsModel.TreeViewStructure.MainTagName == "Geo" ||
                        GlobalsModel.TreeViewStructure.MainTagName == "Region" ||
                        GlobalsModel.TreeViewStructure.MainTagName == "Province" ||
                        GlobalsModel.TreeViewStructure.MainTagName == "District" ||
                        GlobalsModel.TreeViewStructure.MainTagName == "LLG")
                    {
                        if (GlobalsModel.LoggedUser.Database != CommonEnum.Database.Final)
                        {
                            var selectedIndex = cmbStatus.SelectedIndex;
                            StatusArray = new string[] { "ALL", "WARD ROLL", "OLD", "NEW", "UPDATED", "DECEASED", "TRANSFERRED OUT", "GHOST", "DELETED", "DUPLICATE", "TRANSFERRED IN", "UNDER AGE", "TRANSFER OUT with FORM 11", "TRANSFER OUT without FORM 11" };
                            cmbStatus.DataSource = StatusArray;
                            cmbStatus.SelectedIndex = selectedIndex;
                        }

                        btnExportToCSV.Visible = true;
                    }
                    else
                    {
                        if (GlobalsModel.LoggedUser.Database != CommonEnum.Database.Final)
                        {
                            var selectedIndex = cmbStatus.SelectedIndex;
                            StatusArray = new string[] { "ALL", "WARD ROLL", "OLD", "NEW", "UPDATED", "DECEASED", "TRANSFERRED OUT", "GHOST", "DELETED", "DUPLICATE", "TRANSFERRED IN", "UNDER AGE" };
                            cmbStatus.DataSource = StatusArray;
                            if (selectedIndex >= StatusArray.Length)
                            {
                                cmbStatus.SelectedIndex = 0;
                            }
                            else
                            {
                                cmbStatus.SelectedIndex = selectedIndex;
                            }
                        }
                    }
                    #endregion
                }
            }
            else if ((this.pageName == "DataMigration" || this.pageName == "TransferOut" || this.pageName == "Duplicate" || this.pageName == "TransferInApproval") &&
                (GlobalsModel.LoggedUser.User.Role == (int)CommonEnum.Role.RD || GlobalsModel.LoggedUser.User.Role == (int)CommonEnum.Role.RO
                 || GlobalsModel.LoggedUser.User.Role == (int)CommonEnum.Role.Admin || GlobalsModel.LoggedUser.User.Role == (int)CommonEnum.Role.SelectRole))
            {
                // chkLookUp.Checked = true;
                pnlApproval.Visible = true;
                pnlApproveReject.Visible = false;
                pnlExport.Visible = false;
                btnExportToCSV.Visible = false;
            }
            else
            {
                pnlApproveReject.Visible = false;
                pnlExport.Visible = false;
                btnExportToCSV.Visible = false;
                pnlApproval.Visible = false;
            }

        }
        #endregion

        #region Filed Combobox Bind
        /// <summary>
        /// Fileds the combobox bind.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void FiledComboboxBind()
        {
            cmbFiled.DataSource = filedArray;
            cmbGender.DataSource = GenderArray;
            cmbApproval.DataSource = StatusArrayDataMigrationReport;
        }
        #endregion

        #region Satus Combobox Bind
        /// <summary>
        /// Satuses the combobox bind.
        /// </summary>
        private void SatusComboboxBind()
        {
            if (Convert.ToString(cmbFiled.SelectedValue) == "SevisPass")
            {
                cmbStatus.DataSource = SevisPassStatusArray;
                return;
            }

            if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Final && (this.pageName == "File" || this.pageName == "WardRollBook"))
            {
                cmbStatus.DataSource = StatusArrayFinalDB;
            }
            //else if ((GlobalsModel.LoggedUser.User.Role == 7 || GlobalsModel.LoggedUser.User.Role == 0) && this.pageName == "TransferOut")
            else if ((GlobalsModel.LoggedUser.User.Role == (int)CommonEnum.Role.RO || GlobalsModel.LoggedUser.User.Role == (int)CommonEnum.Role.RD || GlobalsModel.LoggedUser.User.Role == 0) && this.pageName == "TransferOut")
            {
                cmbStatus.DataSource = StatusArrayTransferOutRO;
            }
            //else if ((GlobalsModel.LoggedUser.User.Role == 7 || GlobalsModel.LoggedUser.User.Role == 0) && this.pageName == "Duplicate")
            else if ((GlobalsModel.LoggedUser.User.Role == (int)CommonEnum.Role.RO || GlobalsModel.LoggedUser.User.Role == (int)CommonEnum.Role.RD || GlobalsModel.LoggedUser.User.Role == 0) && this.pageName == "Duplicate")
            {
                cmbStatus.DataSource = StatusArrayDuplicateRO;
            }
            //else if ((GlobalsModel.LoggedUser.User.Role == 7 || GlobalsModel.LoggedUser.User.Role == 0) && this.pageName == "TransferInApproval")
            else if ((GlobalsModel.LoggedUser.User.Role == (int)CommonEnum.Role.RO || GlobalsModel.LoggedUser.User.Role == (int)CommonEnum.Role.RD || GlobalsModel.LoggedUser.User.Role == 0) && this.pageName == "TransferInApproval")
            {
                cmbStatus.DataSource = StatusArrayTransferInRO;
            }
            else if (this.pageName == "DataMigrationReport")
            {
                cmbStatus.DataSource = StatusArrayDataMigrationReport;
            }
            else
            {
                cmbStatus.DataSource = StatusArray;
            }
        }
        #endregion

        #region InitTreeData
        /// <summary>
        /// Initializes the tree data.
        /// </summary>
        private void InitTreeData()
        {
            SetData();
        }
        #endregion

        #region SetData
        /// <summary>
        /// Sets the data.
        /// </summary>
        private void SetData()
        {
            //List<GeoTreeStructure> locations = (from DataRow dr in Globals.dtLocation.Rows
            //                                    select new GeoTreeStructure()
            //                                    {
            //                                        Id = Convert.ToInt32(dr["LocationId"]),
            //                              s          Name = dr["LocationName"].ToString(),
            //                                        MainTagName = "Location",
            //                                        ParentId = Convert.ToInt32(dr["WardId"]),
            //                                        Children = null
            //                                    }).ToList();
            //List<GeoTreeStructure> wards = (from DataRow dr in Globals.dtWard.Rows
            //                                select new GeoTreeStructure()
            //                                {
            //                                    Id = Convert.ToInt32(dr["WardId"]),
            //                                    Name = dr["WardName"].ToString(),
            //                                    MainTagName = "Ward",
            //                                    ParentId = Convert.ToInt32(dr["LLGId"]),
            //                                    Children = locations.Where(l => l.ParentId == Convert.ToInt32(dr["WardId"])).ToList()
            //                                }).ToList();
            //List<GeoTreeStructure> llgs = (from DataRow dr in Globals.dtLLG.Rows
            //                               select new GeoTreeStructure()
            //                               {
            //                                   Name = dr["LLGName"].ToString(),
            //                                   Id = Convert.ToInt32(dr["LLGId"]),
            //                                   ParentId = Convert.ToInt32(dr["DistrictId"]),
            //                                   MainTagName = "LLG",
            //                                   Children = wards.Where(w => w.ParentId == Convert.ToInt32(dr["LLGId"])).ToList()
            //                               }).ToList();
            //List<GeoTreeStructure> districts = (from DataRow dr in Globals.dtDistrict.Rows
            //                                    select new GeoTreeStructure()
            //                                    {
            //                                        Name = dr["DistrictName"].ToString(),
            //                                        ParentId = Convert.ToInt32(dr["ProvinceId"]),
            //                                        Id = Convert.ToInt32(dr["DistrictId"]),
            //                                        MainTagName = "District",
            //                                        Children = llgs.Where(w => w.ParentId == Convert.ToInt32(dr["DistrictId"])).ToList()
            //                                    }).ToList();
            //List<GeoTreeStructure> provinces = (from DataRow dr in Globals.dtProvince.Rows
            //                                    select new GeoTreeStructure()
            //                                    {
            //                                        Name = dr["ProvinceName"].ToString(),
            //                                        Id = Convert.ToInt32(dr["ProvinceId"]),
            //                                        ParentId = Convert.ToInt32(dr["RegionId"]),
            //                                        MainTagName = "Province",
            //                                        Children = districts.Where(w => w.ParentId == Convert.ToInt32(dr["ProvinceId"])).ToList()
            //                                    }).ToList();
            //List<GeoTreeStructure> regionList = (from DataRow dr in Globals.dtRegion.Rows
            //                                     select new GeoTreeStructure()
            //                                     {
            //                                         Id = Convert.ToInt32(dr["RegionId"]),
            //                                         Name = dr["RegionName"].ToString(),
            //                                         ParentId = default(int),
            //                                         Children = provinces.Where(p => p.ParentId == Convert.ToInt32(dr["RegionId"])).ToList()
            //                                     }).ToList();

            //PopulateTreeView(regionList, null);

            var loc = Globals.GeoList.LocationList.Select(l => new GeoTreeStructure()
            {
                Id = l.LocationId,
                Name = l.LocationName,
                MainTagName = "Location",
                ParentId = l.WardId,
                Children = null
            });
            var ward = Globals.GeoList.WardList.Select(w => new GeoTreeStructure()
            {
                Id = w.WardId,
                Name = w.WardName,
                MainTagName = "Ward",
                ParentId = w.LLGId,
                Children = loc.Where(l => l.ParentId == w.WardId)
            });
            var llg = Globals.GeoList.LLGList.Select(w => new GeoTreeStructure()
            {
                Id = w.LLGId,
                Name = w.LLGName,
                MainTagName = "LLG",
                ParentId = w.DistrictId,
                Children = ward.Where(l => l.ParentId == w.LLGId)
            });
            var dis = Globals.GeoList.DistrictList.Select(w => new GeoTreeStructure()
            {
                Id = w.DistrictId,
                Name = w.DistrictName,
                MainTagName = "District",
                ParentId = w.ProvinceId,
                Children = llg.Where(l => l.ParentId == w.DistrictId)
            });
            var pro = Globals.GeoList.ProvinceList.Select(w => new GeoTreeStructure()
            {
                Id = w.ProvinceId,
                Name = w.ProvinceName,
                MainTagName = "Province",
                ParentId = w.RegionId,
                Children = dis.Where(l => l.ParentId == w.ProvinceId)
            });
            var reg = Globals.GeoList.RegionList.Select(w => new GeoTreeStructure()
            {
                Id = w.RegionId,
                Name = w.RegionName,
                MainTagName = "Region",
                ParentId = default(int),
                Children = pro.Where(l => l.ParentId == w.RegionId)
            });
            InitializeActualTreeLogic(reg);
        }
        #endregion

        #region InitializeActualTreeLogic
        /// <summary>
        /// Initializes the actual tree logic.
        /// </summary>
        /// <param name="region">The region.</param>
        private async void InitializeActualTreeLogic(IEnumerable<GeoTreeStructure> region)
        {
            //this.Cursor = Cursors.WaitCursor;
            GeoTreeStructure geoTreeStructure = new GeoTreeStructure();
            geoTreeStructure.Id = 0;
            geoTreeStructure.MainTagName = "Geo";
            await Task.Delay(50);
            tvGeoStructure.Invoke((Action)delegate ()
            {
                this.Cursor = Cursors.WaitCursor;
                TreeNode t = new TreeNode();
                t.Text = "PNGEC";
                t.Tag = region;
                t.Name = "Region";
                t.ImageKey = "PNGEC";
                t.Tag = geoTreeStructure;
                tvGeoStructure.BeginUpdate(); // No visual updates until we say 
                tvGeoStructure.Nodes.Clear(); // Remove existing nodes
                tvGeoStructure.Nodes.Add(t);
                PopulateTreeView(region, t);
                tvGeoStructure.SelectedNode = t;
                tvGeoStructure.SelectedNode.Expand();
                tvGeoStructure.EndUpdate(); // Allow the treeview to update visually
                this.Cursor = Cursors.Default;
                //this.pnlGeoButtons.Enabled = true;
            });
        }
        #endregion

        #region PopulateTreeView
        /// <summary>
        /// Populates the TreeView.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="parentNode">The parent node.</param>
        private void PopulateTreeView(IEnumerable<GeoTreeStructure> data, TreeNode parentNode)
        {
            TreeNode childNode;
            foreach (GeoTreeStructure row in data)
            {
                TreeNode t = new TreeNode();
                t.Text = row.Name.ToString();
                t.Tag = row;
                t.Name = row.MainTagName;
                t.ImageKey = Convert.ToString(row.Id);
                if (parentNode == null)
                {
                    tvGeoStructure.Nodes.Add(t);
                    childNode = t;
                }
                else
                {
                    parentNode.Nodes.Add(t);
                    childNode = t;
                }
                if (row.Children != null)
                    PopulateTreeView(row.Children, childNode);
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
            ManagePanel();
            try
            {
                pageList = new PageList();

                // Page Name
                pageList.PageName = pageName;

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
                    if (dgPages == 1)
                    {
                        pageIndex = "1";
                    }
                    else
                    {
                        pageIndex = btnPager.Name.Equals("") ? "1" : btnPager.Name;
                    }
                }
                pageList.PageNo = int.Parse(pageIndex);
                pageList.PageSize = pageSizeValue.Equals(0) ? Convert.ToInt32(pageSizeArray.First()) : pageSizeValue;
                //pageList.KeyWord = txtSearch.Text;
                pageList.SortField = "ElectorId";
                pageList.SortOrder = "DESC";

                // Filter Filed
                pageList.FiledName = Convert.ToString(cmbFiled.SelectedValue);
                pageList.FiledValue = Convert.ToString(cmbStatus.SelectedValue);

                // TreeView Filed
                pageList.GeoName = TreeStructure.MainTagName;
                pageList.GeoId = TreeStructure.Id;

                // Filter User Details
                pageList.FName = txtFName.Text.Trim();
                pageList.LName = txtLName.Text.Trim();

                pageList.GeoClanId = Convert.ToInt16(cmbClan.SelectedValue);

                string gender = "All";

                if (cmbGender.Text.Trim() == "MALE")
                {
                    gender = "M";
                }
                else if (cmbGender.Text.Trim() == "FEMALE")
                {
                    gender = "F";
                }
                else if (cmbGender.Text.Trim() == "OTHER")
                {
                    gender = "O";
                }

                pageList.ApprovalStatus = "ALL";
                if (cmbApproval.Visible)
                {
                    pageList.ApprovalStatus = cmbApproval.Text.Trim();
                }

                pageList.Gender = gender;

                // Login User
                pageList.LoggedUserId = GlobalsModel.LoggedUser.User.UserId;


                if (pageList != null)
                {
                    if (chkLookUp.Checked == false && (pageList.GeoName == "Geo" || pageList.GeoName == "Region" || pageList.GeoName == "Province" || pageList.GeoName == "District" || pageList.GeoName == "LLG"))
                    {
                        BindVoterGird(new List<VoterModel>());
                        this.dgFile.BackgroundColor = Color.DarkGray;
                    }
                    else
                    {
                        await this.BindGrid(pageList);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Resources.ErrorMessage, "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorLog.Write("File", "GridViewSetting", ex);
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
                if (response.Data.ListItems.ToList() != null)
                {
                    this.dgFile.BackgroundColor = Color.White;
                    if (this.pageName == "TransferOut" || this.pageName == "Duplicate" || this.pageName == "TransferInApproval")
                    {
                        dgFile.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                    }
                    else
                    {
                        //dgFile.AutoResizeColumns();
                        dgFile.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    }

                    if (response != null && response.Data != null && response.Data.ListItems != null && response.Data.ListItems.Count() > 0)
                    {
                        foreach (var item in response.Data.ListItems.ToList())
                        {
                            if (!string.IsNullOrWhiteSpace(item.EnrolmentFormNumber))
                            {
                                string[] EnrolmentFormArray = item.EnrolmentFormNumber.Split('#');

                                if (EnrolmentFormArray.Length == 7)
                                {
                                    item.EnrolmentFormNumber = EnrolmentFormArray[1] + EnrolmentFormArray[4].ToString().PadLeft(4, '0') + EnrolmentFormArray[6];
                                }
                            }
                        }
                    }

                    #region unused code
                    //if (this.pageName == "TransferOut" || this.pageName == "TransferInApproval")
                    //{
                    //    foreach (var item in response.Data.ListItems.ToList())
                    //    {
                    //        if (!string.IsNullOrWhiteSpace(item.EnrolmentFormNumber))
                    //        {
                    //            string[] EnrolmentFormArray = item.EnrolmentFormNumber.Split('#');

                    //            if (EnrolmentFormArray.Length == 7)
                    //            {
                    //                item.EnrolmentFormNumber = EnrolmentFormArray[1] + EnrolmentFormArray[4].ToString().PadLeft(4, '0') + EnrolmentFormArray[6];
                    //            }
                    //        }
                    //    }

                    //    BindVoterGird(response.Data.ListItems.ToList());
                    //}
                    //else
                    //{
                    //    BindVoterGird(response.Data.ListItems.ToList());
                    //}
                    #endregion

                    BindVoterGird(response.Data.ListItems.ToList());

                    PopulatePager(response.Data.TotalRecordCount, model.PageNo, model.PageSize);
                    //ManagePanel();

                    if ((this.pageName == "TransferOut" || this.pageName == "Duplicate") && response != null && response.Data != null && response.Data.ListItems != null && response.Data.ListItems.Count<VoterModel>() > 0)
                    {
                        if (dtExportCsvReportData != null)
                        {
                            dtExportCsvReportData.Clear();
                        }
                        else
                        {
                            dtExportCsvReportData = new DataTable();
                        }

                        dtExportCsvReportData = ToDataTable(response.Data.ListItems.ToList());
                    }
                }
                else
                {
                    this.dgFile.BackgroundColor = Color.DarkGray;
                }
            }
        }
        #endregion

        #region Bind Voter Gird
        /// <summary>
        /// Binds the voter gird.
        /// </summary>
        /// <param name="lstVoters">The LST voters.</param>
        private void BindVoterGird(List<VoterModel> lstVoters)
        {
            try
            {
                if (this.dgFile.Columns.Count > 0)
                {
                    this.dgFile.Columns.Clear();
                }

                if (this.pageName != "Duplicate")
                {
                    // Unvisible Column
                    //this.dgFile.DataSource = null;
                    this.dgFile.DataSource = lstVoters;
                    this.dgFile.Columns["Database"].Visible = false;
                    this.dgFile.Columns["OldElectorID"].Visible = false;
                    this.dgFile.Columns["LocationId"].Visible = false;
                    this.dgFile.Columns["WardId"].Visible = false;
                    this.dgFile.Columns["LLGId"].Visible = false;
                    this.dgFile.Columns["DistrictId"].Visible = false;
                    this.dgFile.Columns["ProvinceId"].Visible = false;
                    this.dgFile.Columns["RegionId"].Visible = false;
                    this.dgFile.Columns["LName"].Visible = false;
                    this.dgFile.Columns["FName"].Visible = false;
                    this.dgFile.Columns["Gender"].Visible = false;
                    this.dgFile.Columns["DOB"].Visible = false;
                    this.dgFile.Columns["isYOB"].Visible = false;
                    this.dgFile.Columns["isPWD"].Visible = false;
                    this.dgFile.Columns["FatherLName"].Visible = false;
                    this.dgFile.Columns["FatherFName"].Visible = false;
                    this.dgFile.Columns["MotherLName"].Visible = false;
                    this.dgFile.Columns["MotherFName"].Visible = false;
                    this.dgFile.Columns["ProvOfBirthFK"].Visible = false;
                    this.dgFile.Columns["PlaceOfBirth"].Visible = false;
                    this.dgFile.Columns["OccupationFK"].Visible = false;
                    this.dgFile.Columns["ClanFK"].Visible = false;
                    this.dgFile.Columns["SubClan"].Visible = false;
                    this.dgFile.Columns["Address"].Visible = false;
                    this.dgFile.Columns["NatID"].Visible = false;
                    this.dgFile.Columns["NatIDRecordNo"].Visible = false;
                    this.dgFile.Columns["HouseFK"].Visible = false;
                    this.dgFile.Columns["HouseSortOrder"].Visible = false;
                    this.dgFile.Columns["FamilyFK"].Visible = false;
                    this.dgFile.Columns["FamilySortOrder"].Visible = false;
                    //this.dgFile.Columns["changed"].Visible = false;
                    this.dgFile.Columns["dpo"].Visible = false;
                    this.dgFile.Columns["posted"].Visible = false;
                    //this.dgFile.Columns["gps"].Visible = false;

                    this.dgFile.Columns["ProvinceOfBirth"].Visible = false;
                    this.dgFile.Columns["TempElectorId"].Visible = false;

                    // New Add Column
                    this.dgFile.Columns["FamilyNumber"].Visible = false;
                    this.dgFile.Columns["FamilyName"].Visible = false;
                    this.dgFile.Columns["FamilySortOrder"].Visible = false;
                    this.dgFile.Columns["OpenElectorate"].Visible = false;
                    this.dgFile.Columns["Clan"].Visible = false;
                    this.dgFile.Columns["RestHouse"].Visible = false;
                    this.dgFile.Columns["SectionNo"].Visible = false;
                    this.dgFile.Columns["LotNo"].Visible = false;
                    this.dgFile.Columns["StreetName"].Visible = false;
                    this.dgFile.Columns["SuburbTownSettlement"].Visible = false;
                    this.dgFile.Columns["Comment"].Visible = false;
                    this.dgFile.Columns["ElectorProfileImage"].Visible = false;
                    this.dgFile.Columns["ElectorProfileImageUrl"].Visible = false;
                    this.dgFile.Columns["Lat"].Visible = false;
                    this.dgFile.Columns["Long"].Visible = false;
                    this.dgFile.Columns["ElectorFormImage"].Visible = false;
                    this.dgFile.Columns["ElectorFormImageUrl"].Visible = false;
                    this.dgFile.Columns["ElectorFormImageFile"].Visible = false;
                    this.dgFile.Columns["ElectorProfileImageFile"].Visible = false;
                    this.dgFile.Columns["CreatedBy"].Visible = false;

                    this.dgFile.Columns["DuplicateId"].Visible = false;
                    this.dgFile.Columns["AuditDescription"].Visible = false;
                    this.dgFile.Columns["HasTransferInForm11"].Visible = false;

                    this.dgFile.Columns["TransferInRegionId"].Visible = false;
                    this.dgFile.Columns["TransferInProvinceId"].Visible = false;
                    this.dgFile.Columns["TransferInDistrictId"].Visible = false;
                    this.dgFile.Columns["TransferInLLGId"].Visible = false;
                    this.dgFile.Columns["TransferInWardId"].Visible = false;
                    this.dgFile.Columns["TransferInLocationId"].Visible = false;

                    this.dgFile.Columns["TransferOutRegionId"].Visible = false;
                    this.dgFile.Columns["TransferOutProvinceId"].Visible = false;
                    this.dgFile.Columns["TransferOutDistrictId"].Visible = false;
                    this.dgFile.Columns["TransferOutLLGId"].Visible = false;
                    this.dgFile.Columns["TransferOutWardId"].Visible = false;
                    this.dgFile.Columns["TransferOutLocationId"].Visible = false;

                    this.dgFile.Columns["IsRejectedByRD"].Visible = false;
                    this.dgFile.Columns["IsRejectedByRD"].Visible = false;

                    //NEW ADDED COLUMN RELIGION
                    this.dgFile.Columns["Religion"].Visible = false;
                    this.dgFile.Columns["ErrorMessage"].Visible = false;
                    this.dgFile.Columns["CreatedDate"].Visible = false;
                    this.dgFile.Columns["ARLat"].Visible = false;
                    this.dgFile.Columns["ARLong"].Visible = false;
                    this.dgFile.Columns["RejectedReason"].Visible = false;
                    this.dgFile.Columns["IsApproveOrReject"].Visible = false;
                    this.dgFile.Columns["ModifiedDate"].Visible = false;
                    this.dgFile.Columns["ModifiedByName"].Visible = false;
                    this.dgFile.Columns["IsRejected"].Visible = false;
                    this.dgFile.Columns["ElectorSignatureImage"].Visible = false;
                    this.dgFile.Columns["ElectorFingerPrintImage"].Visible = false;
                    this.dgFile.Columns["ElectorProfileImageBinary"].Visible = false;
                    this.dgFile.Columns["ElectorSignatureImageBinary"].Visible = false;
                    this.dgFile.Columns["OldLocationId"].Visible = false;
                    this.dgFile.Columns["IsManualFormNumber"].Visible = false;

                    //Do not show MKA Columns
                    this.dgFile.Columns["MKALocationId"].Visible = false;
                    this.dgFile.Columns["MKAWardId"].Visible = false;
                    this.dgFile.Columns["MKALLGId"].Visible = false;
                    this.dgFile.Columns["MKADistrictId"].Visible = false;
                    this.dgFile.Columns["MKADistrictName"].Visible = false;
                    this.dgFile.Columns["MKALLGName"].Visible = false;
                    this.dgFile.Columns["MKAWardName"].Visible = false;
                    this.dgFile.Columns["MKALocationName"].Visible = false;
                    this.dgFile.Columns["MKAClanId"].Visible = false;
                    this.dgFile.Columns["MKAClanName"].Visible = false;

                    //MKA Columns
                    this.dgFile.Columns["TransferInMKADistrictId"].Visible = false;
                    this.dgFile.Columns["TransferInMKALLGId"].Visible = false;
                    this.dgFile.Columns["TransferInMKAWardId"].Visible = false;
                    this.dgFile.Columns["TransferInMKALocationId"].Visible = false;

                    this.dgFile.Columns["TransferOutMKADistrictId"].Visible = false;
                    this.dgFile.Columns["TransferOutMKALLGId"].Visible = false;
                    this.dgFile.Columns["TransferOutMKAWardId"].Visible = false;
                    this.dgFile.Columns["TransferOutMKALocationId"].Visible = false;

                    this.dgFile.Columns["transferInMKADistrictName"].Visible = false;
                    this.dgFile.Columns["transferInMKALLGName"].Visible = false;
                    this.dgFile.Columns["transferInMKAWardName"].Visible = false;
                    this.dgFile.Columns["transferInMKALocationName"].Visible = false;


                    this.dgFile.Columns["transferOutMKADistrictName"].Visible = false;
                    this.dgFile.Columns["transferOutMKALLGName"].Visible = false;
                    this.dgFile.Columns["transferOutMKAWardName"].Visible = false;
                    this.dgFile.Columns["transferOutMKALocationName"].Visible = false;

                    this.dgFile.Columns["ClanName"].Visible = false;
                    this.dgFile.Columns["GeoClanID"].Visible = false;
                    this.dgFile.Columns["ClanID"].Visible = false;

                   
                    // Display Column
                    this.dgFile.Columns["RowNo"].HeaderText = "No";
                    this.dgFile.Columns["RowNo"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    this.dgFile.Columns["VoterName"].HeaderText = "Voter";
                    this.dgFile.Columns["VoterName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    //this.dgFile.Columns["VoterName"].DisplayIndex = 1;
                    this.dgFile.Columns["ElectorId"].HeaderText = "Elector";
                    this.dgFile.Columns["ElectorId"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    this.dgFile.Columns["Status"].HeaderText = "Status";
                    this.dgFile.Columns["ProvinceName"].HeaderText = "Province";
                    this.dgFile.Columns["ProvinceName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    this.dgFile.Columns["DistrictName"].HeaderText = "District";
                    this.dgFile.Columns["DistrictName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    this.dgFile.Columns["LLGName"].HeaderText = "LLG";
                    this.dgFile.Columns["LLGName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    this.dgFile.Columns["WardName"].HeaderText = "Ward";
                    this.dgFile.Columns["WardName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    this.dgFile.Columns["LocationName"].HeaderText = "Location";
                    this.dgFile.Columns["LocationName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    this.dgFile.Columns["ClanName"].HeaderText = "Clan";
                    this.dgFile.Columns["ClanName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                    if (this.dgFile.Columns.Contains("SevisPassId"))
                    {
                        this.dgFile.Columns["SevisPassId"].HeaderText = "SevisPass ID";
                        this.dgFile.Columns["SevisPassId"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        this.dgFile.Columns["SevisPassId"].Visible = true;
                    }
                    if (this.dgFile.Columns.Contains("SevisPassStatus"))
                    {
                        this.dgFile.Columns["SevisPassStatus"].HeaderText = "SevisPass Status";
                        this.dgFile.Columns["SevisPassStatus"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        this.dgFile.Columns["SevisPassStatus"].Visible = true;
                    }
                    if (this.dgFile.Columns.Contains("SevisPassVerifiedDate"))
                    {
                        this.dgFile.Columns["SevisPassVerifiedDate"].Visible = false;
                    }
                    if (this.dgFile.Columns.Contains("SevisPassDiscrepancy"))
                    {
                        this.dgFile.Columns["SevisPassDiscrepancy"].Visible = false;
                    }

                    if (this.pageName == "TransferOut" || this.pageName == "TransferInApproval")
                    {
                        this.dgFile.Columns["Gen"].HeaderText = "Gender";
                        this.dgFile.Columns["DateOfBirth"].HeaderText = "DOB";
                        this.dgFile.Columns["Occupation"].HeaderText = "Occupation";
                        if (this.pageName == "TransferInApproval")
                        {
                            this.dgFile.Columns["Religion"].Visible = true;
                            this.dgFile.Columns["Religion"].HeaderText = "Religion";

                            this.dgFile.Columns["transferInProvinceName"].Visible = false;
                            this.dgFile.Columns["transferInDistrictName"].Visible = false;
                            this.dgFile.Columns["transferInLLGName"].Visible = false;
                            this.dgFile.Columns["transferInWardName"].Visible = false;
                            this.dgFile.Columns["transferInLocationName"].Visible = false;

                            this.dgFile.Columns["transferOutProvinceName"].HeaderText = "TransferOut Province Name";
                            this.dgFile.Columns["transferOutDistrictName"].HeaderText = "TransferOut District Name";
                            this.dgFile.Columns["transferOutLLGName"].HeaderText = "TransferOut LLG Name";
                            this.dgFile.Columns["transferOutWardName"].HeaderText = "TransferOut Ward Name";
                            this.dgFile.Columns["transferOutLocationName"].HeaderText = "TransferOut Location Name";
                        }
                        else
                        {
                            this.dgFile.Columns["transferOutProvinceName"].Visible = false;
                            this.dgFile.Columns["transferOutDistrictName"].Visible = false;
                            this.dgFile.Columns["transferOutLLGName"].Visible = false;
                            this.dgFile.Columns["transferOutWardName"].Visible = false;
                            this.dgFile.Columns["transferOutLocationName"].Visible = false;

                            this.dgFile.Columns["transferInProvinceName"].HeaderText = "TransferIn Province Name";
                            this.dgFile.Columns["transferInDistrictName"].HeaderText = "TransferIn District Name";
                            this.dgFile.Columns["transferInLLGName"].HeaderText = "TransferIn LLG Name";
                            this.dgFile.Columns["transferInWardName"].HeaderText = "TransferIn Ward Name";
                            this.dgFile.Columns["transferInLocationName"].HeaderText = "TransferIn Location Name";
                        }
                        this.dgFile.Columns["EnrolmentFormNumber"].HeaderText = "Enrolment Form Number";
                        this.dgFile.Columns["EnrolmentFormType"].HeaderText = "Enrolment Form Type";
                    }
                    else
                    {
                        this.dgFile.Columns["Gen"].Visible = false;
                        this.dgFile.Columns["DateOfBirth"].Visible = false;
                        this.dgFile.Columns["Occupation"].Visible = false;
                        this.dgFile.Columns["EnrolmentFormNumber"].Visible = false;
                        this.dgFile.Columns["EnrolmentFormType"].Visible = false;

                        this.dgFile.Columns["transferInProvinceName"].Visible = false;
                        this.dgFile.Columns["transferInDistrictName"].Visible = false;
                        this.dgFile.Columns["transferInLLGName"].Visible = false;
                        this.dgFile.Columns["transferInWardName"].Visible = false;
                        this.dgFile.Columns["transferInLocationName"].Visible = false;

                        this.dgFile.Columns["transferOutProvinceName"].Visible = false;
                        this.dgFile.Columns["transferOutDistrictName"].Visible = false;
                        this.dgFile.Columns["transferOutLLGName"].Visible = false;
                        this.dgFile.Columns["transferOutWardName"].Visible = false;
                        this.dgFile.Columns["transferOutLocationName"].Visible = false;
                    }

                    this.dgFile.Columns["DuplicateElectorId"].Visible = false;
                    this.dgFile.Columns["DuplicateLName"].Visible = false;
                    this.dgFile.Columns["DuplicateFName"].Visible = false;
                    this.dgFile.Columns["DuplicateVoterName"].Visible = false;
                    this.dgFile.Columns["DuplicateGender"].Visible = false;
                    this.dgFile.Columns["DuplicateGen"].Visible = false;
                    this.dgFile.Columns["DuplicateDOB"].Visible = false;
                    this.dgFile.Columns["DuplicateDateOfBirth"].Visible = false;
                    this.dgFile.Columns["DuplicateOccupation"].Visible = false;
                    this.dgFile.Columns["DuplicateProvinceName"].Visible = false;
                    this.dgFile.Columns["DuplicateDistrictName"].Visible = false;
                    this.dgFile.Columns["DuplicateLLGName"].Visible = false;
                    this.dgFile.Columns["DuplicateWardName"].Visible = false;
                    this.dgFile.Columns["DuplicateLocationName"].Visible = false;
                    this.dgFile.Columns["DuplicateRecordType"].Visible = false;

                    // Logged User EM or RD or RO
                    if ((GlobalsModel.LoggedUser.User.Role == 2 ||
                        GlobalsModel.LoggedUser.User.Role == 3 ||
                        GlobalsModel.LoggedUser.User.Role == 7 ||
                        GlobalsModel.LoggedUser.User.Role == 0) &&
                        (this.pageName == "DataMigration" || this.pageName == "TransferOut" || this.pageName == "TransferInApproval") &&
                        (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Staging ||
                        GlobalsModel.LoggedUser.Database == CommonEnum.Database.Preliminary))
                    {
                        this.dgFile.Columns["RecordType"].HeaderText = "Record Type";
                    }
                    else
                    {
                        this.dgFile.Columns["RecordType"].Visible = false;
                    }
                }
                else
                {
                    // Unvisible Column
                    //this.dgFile.DataSource = null;
                    this.dgFile.DataSource = lstVoters;
                    this.dgFile.Columns["Database"].Visible = false;
                    this.dgFile.Columns["OldElectorID"].Visible = false;
                    this.dgFile.Columns["LocationId"].Visible = false;
                    this.dgFile.Columns["WardId"].Visible = false;
                    this.dgFile.Columns["LLGId"].Visible = false;
                    this.dgFile.Columns["DistrictId"].Visible = false;
                    this.dgFile.Columns["ProvinceId"].Visible = false;
                    this.dgFile.Columns["RegionId"].Visible = false;
                    this.dgFile.Columns["LName"].Visible = false;
                    this.dgFile.Columns["FName"].Visible = false;
                    this.dgFile.Columns["Gender"].Visible = false;
                    this.dgFile.Columns["DOB"].Visible = false;
                    this.dgFile.Columns["isYOB"].Visible = false;
                    this.dgFile.Columns["isPWD"].Visible = false;
                    this.dgFile.Columns["FatherLName"].Visible = false;
                    this.dgFile.Columns["FatherFName"].Visible = false;
                    this.dgFile.Columns["MotherLName"].Visible = false;
                    this.dgFile.Columns["MotherFName"].Visible = false;
                    this.dgFile.Columns["ProvOfBirthFK"].Visible = false;
                    this.dgFile.Columns["PlaceOfBirth"].Visible = false;
                    this.dgFile.Columns["OccupationFK"].Visible = false;
                    this.dgFile.Columns["ClanFK"].Visible = false;
                    this.dgFile.Columns["SubClan"].Visible = false;
                    this.dgFile.Columns["Address"].Visible = false;
                    this.dgFile.Columns["NatID"].Visible = false;
                    this.dgFile.Columns["NatIDRecordNo"].Visible = false;
                    this.dgFile.Columns["HouseFK"].Visible = false;
                    this.dgFile.Columns["HouseSortOrder"].Visible = false;
                    this.dgFile.Columns["FamilyFK"].Visible = false;
                    this.dgFile.Columns["FamilySortOrder"].Visible = false;
                    //this.dgFile.Columns["changed"].Visible = false;
                    this.dgFile.Columns["dpo"].Visible = false;
                    this.dgFile.Columns["posted"].Visible = false;
                    //this.dgFile.Columns["gps"].Visible = false;
                    this.dgFile.Columns["ProvinceOfBirth"].Visible = false;
                    this.dgFile.Columns["TempElectorId"].Visible = false;
                    // New Add Column
                    this.dgFile.Columns["FamilyNumber"].Visible = false;
                    this.dgFile.Columns["FamilyName"].Visible = false;
                    this.dgFile.Columns["FamilySortOrder"].Visible = false;
                    this.dgFile.Columns["OpenElectorate"].Visible = false;
                    this.dgFile.Columns["Clan"].Visible = false;
                    this.dgFile.Columns["RestHouse"].Visible = false;
                    this.dgFile.Columns["SectionNo"].Visible = false;
                    this.dgFile.Columns["LotNo"].Visible = false;
                    this.dgFile.Columns["StreetName"].Visible = false;
                    this.dgFile.Columns["SuburbTownSettlement"].Visible = false;
                    this.dgFile.Columns["Comment"].Visible = false;
                    this.dgFile.Columns["ElectorProfileImage"].Visible = false;
                    this.dgFile.Columns["ElectorProfileImageUrl"].Visible = false;
                    this.dgFile.Columns["Lat"].Visible = false;
                    this.dgFile.Columns["Long"].Visible = false;
                    this.dgFile.Columns["ElectorFormImage"].Visible = false;
                    this.dgFile.Columns["ElectorFormImageUrl"].Visible = false;
                    this.dgFile.Columns["ElectorFormImageFile"].Visible = false;
                    this.dgFile.Columns["ElectorProfileImageFile"].Visible = false;
                    this.dgFile.Columns["CreatedBy"].Visible = false;
                    this.dgFile.Columns["DuplicateId"].Visible = false;
                    this.dgFile.Columns["AuditDescription"].Visible = false;
                    this.dgFile.Columns["HasTransferInForm11"].Visible = false;

                    this.dgFile.Columns["TransferInRegionId"].Visible = false;
                    this.dgFile.Columns["TransferInProvinceId"].Visible = false;
                    this.dgFile.Columns["TransferInDistrictId"].Visible = false;
                    this.dgFile.Columns["TransferInLLGId"].Visible = false;
                    this.dgFile.Columns["TransferInWardId"].Visible = false;
                    this.dgFile.Columns["TransferInLocationId"].Visible = false;

                    this.dgFile.Columns["TransferOutRegionId"].Visible = false;
                    this.dgFile.Columns["TransferOutProvinceId"].Visible = false;
                    this.dgFile.Columns["TransferOutDistrictId"].Visible = false;
                    this.dgFile.Columns["TransferOutLLGId"].Visible = false;
                    this.dgFile.Columns["TransferOutWardId"].Visible = false;
                    this.dgFile.Columns["TransferOutLocationId"].Visible = false;

                    //MKA Columns
                    this.dgFile.Columns["TransferInMKADistrictId"].Visible = false;
                    this.dgFile.Columns["TransferInMKALLGId"].Visible = false;
                    this.dgFile.Columns["TransferInMKAWardId"].Visible = false;
                    this.dgFile.Columns["TransferInMKALocationId"].Visible = false;

                    this.dgFile.Columns["TransferOutMKADistrictId"].Visible = false;
                    this.dgFile.Columns["TransferOutMKALLGId"].Visible = false;
                    this.dgFile.Columns["TransferOutMKAWardId"].Visible = false;
                    this.dgFile.Columns["TransferOutMKALocationId"].Visible = false;

                    this.dgFile.Columns["IsRejectedByRD"].Visible = false;
                    this.dgFile.Columns["IsRejectedByRD"].Visible = false;

                    //NEW ADDED COLUMN RELIGION
                    this.dgFile.Columns["Religion"].Visible = false;
                    this.dgFile.Columns["ErrorMessage"].Visible = false;
                    this.dgFile.Columns["CreatedDate"].Visible = false;
                    this.dgFile.Columns["ARLat"].Visible = false;
                    this.dgFile.Columns["ARLong"].Visible = false;
                    this.dgFile.Columns["RejectedReason"].Visible = false;
                    this.dgFile.Columns["IsApproveOrReject"].Visible = false;
                    this.dgFile.Columns["ModifiedDate"].Visible = false;
                    this.dgFile.Columns["ModifiedByName"].Visible = false;
                    this.dgFile.Columns["IsRejected"].Visible = false;

                    this.dgFile.Columns["EnrolmentFormNumber"].Visible = false;
                    this.dgFile.Columns["EnrolmentFormType"].Visible = false;

                    this.dgFile.Columns["transferInProvinceName"].Visible = false;
                    this.dgFile.Columns["transferInDistrictName"].Visible = false;
                    this.dgFile.Columns["transferInLLGName"].Visible = false;
                    this.dgFile.Columns["transferInWardName"].Visible = false;
                    this.dgFile.Columns["transferInLocationName"].Visible = false;

                    this.dgFile.Columns["transferOutProvinceName"].Visible = false;
                    this.dgFile.Columns["transferOutDistrictName"].Visible = false;
                    this.dgFile.Columns["transferOutLLGName"].Visible = false;
                    this.dgFile.Columns["transferOutWardName"].Visible = false;
                    this.dgFile.Columns["transferOutLocationName"].Visible = false;

                    this.dgFile.Columns["ElectorSignatureImage"].Visible = false;
                    this.dgFile.Columns["ElectorFingerPrintImage"].Visible = false;

                    this.dgFile.Columns["ElectorProfileImageBinary"].Visible = false;
                    this.dgFile.Columns["ElectorSignatureImageBinary"].Visible = false;
                    this.dgFile.Columns["OldLocationId"].Visible = false;
                    this.dgFile.Columns["IsManualFormNumber"].Visible = false;

                 
                    // Display Column
                    this.dgFile.Columns["RowNo"].HeaderText = "No";
                    this.dgFile.Columns["RowNo"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    this.dgFile.Columns["VoterName"].HeaderText = "Voter";
                    this.dgFile.Columns["VoterName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    this.dgFile.Columns["ElectorId"].HeaderText = "Elector";
                    this.dgFile.Columns["ElectorId"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    this.dgFile.Columns["Gen"].HeaderText = "Gender";
                    this.dgFile.Columns["DateOfBirth"].HeaderText = "DOB";
                    this.dgFile.Columns["Status"].HeaderText = "Status";
                    this.dgFile.Columns["ProvinceName"].HeaderText = "Province";
                    this.dgFile.Columns["ProvinceName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    this.dgFile.Columns["DistrictName"].HeaderText = "District";
                    this.dgFile.Columns["DistrictName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    this.dgFile.Columns["LLGName"].HeaderText = "LLG";
                    this.dgFile.Columns["LLGName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    this.dgFile.Columns["WardName"].HeaderText = "Ward";
                    this.dgFile.Columns["WardName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    this.dgFile.Columns["LocationName"].HeaderText = "Location";
                    this.dgFile.Columns["LocationName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                    if (this.dgFile.Columns.Contains("SevisPassId"))
                    {
                        this.dgFile.Columns["SevisPassId"].HeaderText = "SevisPass ID";
                        this.dgFile.Columns["SevisPassId"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        this.dgFile.Columns["SevisPassId"].Visible = true;
                    }
                    if (this.dgFile.Columns.Contains("SevisPassStatus"))
                    {
                        this.dgFile.Columns["SevisPassStatus"].HeaderText = "SevisPass Status";
                        this.dgFile.Columns["SevisPassStatus"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        this.dgFile.Columns["SevisPassStatus"].Visible = true;
                    }
                    if (this.dgFile.Columns.Contains("SevisPassVerifiedDate"))
                    {
                        this.dgFile.Columns["SevisPassVerifiedDate"].Visible = false;
                    }
                    if (this.dgFile.Columns.Contains("SevisPassDiscrepancy"))
                    {
                        this.dgFile.Columns["SevisPassDiscrepancy"].Visible = false;
                    }

                    this.dgFile.Columns["DuplicateElectorId"].HeaderText = "Duplicate Elector Id";
                    this.dgFile.Columns["DuplicateLName"].Visible = false;
                    this.dgFile.Columns["DuplicateFName"].Visible = false;
                    this.dgFile.Columns["DuplicateVoterName"].HeaderText = "Duplicate Voter Name";
                    this.dgFile.Columns["DuplicateGender"].Visible = false;
                    this.dgFile.Columns["DuplicateGen"].HeaderText = "Duplicate Gender";
                    this.dgFile.Columns["DuplicateDOB"].Visible = false;
                    this.dgFile.Columns["DuplicateDateOfBirth"].HeaderText = "Duplicate DOB";
                    this.dgFile.Columns["DuplicateOccupation"].HeaderText = "Duplicate Occupation";
                    this.dgFile.Columns["DuplicateProvinceName"].HeaderText = "Duplicate Province Name";
                    this.dgFile.Columns["DuplicateDistrictName"].HeaderText = "Duplicate District Name";
                    this.dgFile.Columns["DuplicateLLGName"].HeaderText = "Duplicate LLG Name";
                    this.dgFile.Columns["DuplicateWardName"].HeaderText = "Duplicate Ward Name";
                    this.dgFile.Columns["DuplicateLocationName"].HeaderText = "Duplicate Location Name";


                    // Logged User EM or RD or RO
                    if ((GlobalsModel.LoggedUser.User.Role == 2 ||
                        GlobalsModel.LoggedUser.User.Role == 3 ||
                        GlobalsModel.LoggedUser.User.Role == 7 ||
                        GlobalsModel.LoggedUser.User.Role == 0) &&
                        (this.pageName == "DataMigration" || this.pageName == "TransferOut" || this.pageName == "Duplicate") &&
                        (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Staging ||
                        GlobalsModel.LoggedUser.Database == CommonEnum.Database.Preliminary))
                    {
                        this.dgFile.Columns["RecordType"].HeaderText = "Record Type";
                        this.dgFile.Columns["DuplicateRecordType"].Visible = false;
                    }
                    else
                    {
                        this.dgFile.Columns["RecordType"].Visible = false;
                        this.dgFile.Columns["DuplicateRecordType"].Visible = false;
                    }
                }


                #region CheckBox
                if ((Convert.ToString(cmbStatus.SelectedValue) != "OLD") ||
                    (GlobalsModel.LoggedUser.User.Role == 7 || GlobalsModel.LoggedUser.User.Role == 6 || GlobalsModel.LoggedUser.User.Role == 0) &&
                    this.pageName == "DataMigration" &&
                    (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Staging || GlobalsModel.LoggedUser.Database == CommonEnum.Database.Final)
                    && cmbStatus.Text == "OLD")
                {
                    if (((this.pageName == "DataMigration" || this.pageName == "TransferOut" || this.pageName == "Duplicate" || this.pageName == "TransferInApproval") &&
                        ((GlobalsModel.LoggedUser.User.Role == 7 && GlobalsModel.LoggedUser.Database == CommonEnum.Database.Staging) ||
                          ((GlobalsModel.LoggedUser.User.Role == 6 || GlobalsModel.LoggedUser.User.Role == 0) && GlobalsModel.LoggedUser.Database == CommonEnum.Database.Final) ||
                        (GlobalsModel.LoggedUser.User.Role == 3 && GlobalsModel.LoggedUser.Database == CommonEnum.Database.Preliminary))) || this.pageName == "WardRollBook")
                    {
                        //Add a CheckBox Column to the DataGridView at the first position.
                        DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
                        checkBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        ////checkBoxColumn.CellTemplate = new DataGridViewCheckBoxCell();
                        checkBoxColumn.FlatStyle = FlatStyle.Standard;
                        checkBoxColumn.ReadOnly = false;
                        checkBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        checkBoxColumn.HeaderText = string.Empty;
                        checkBoxColumn.Width = 40;
                        checkBoxColumn.Name = "checkBoxColumn";
                        if (dgFile.Columns["checkBoxColumn"] == null)
                        {
                            dgFile.Columns.Insert(0, checkBoxColumn);
                        }
                        ;

                        System.Drawing.Rectangle oRectangle = this.dgFile.GetCellDisplayRectangle(0, -1, true);
                        Point oPoint = new Point();
                        headerCheckBox.Size = new Size(15, 15);
                        oPoint.X = oRectangle.Location.X + (oRectangle.Width - headerCheckBox.Width) / 2 + 1;
                        oPoint.Y = oRectangle.Location.Y + (oRectangle.Height - headerCheckBox.Height) / 2 + 1;
                        headerCheckBox.BackColor = Color.FromArgb(180, 180, 180);
                        //Change the location of the CheckBox to make it stay on the header
                        headerCheckBox.Location = oPoint;
                        headerCheckBox.Checked = false;
                        headerCheckBox.Visible = true;
                        dgFile.Controls.Add(headerCheckBox);

                        //Assign Click event to the DataGridView Cell.
                        //dgVoters.CellContentClick += new DataGridViewCellEventHandler(dgVoters_CellClick);
                    }
                }
                else
                {
                    headerCheckBox.Checked = false;
                    headerCheckBox.Visible = false;
                }
                #endregion

                #region View Button



                if (this.pageName == "File" || this.pageName == "DataMigration" || this.pageName == "TransferOut" || this.pageName == "Duplicate" || this.pageName == "TransferInApproval")
                {
                    DataGridViewButtonColumn viewButton = new DataGridViewButtonColumn();
                    viewButton.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    viewButton.FlatStyle = FlatStyle.Standard;
                    viewButton.HeaderText = "Action";
                    viewButton.Name = "View";
                    viewButton.Text = "View";
                    viewButton.UseColumnTextForButtonValue = true;
                    viewButton.Width = 60;
                    if (dgFile.Columns["View"] == null)
                    {
                        if ((GlobalsModel.LoggedUser.User.Role == 2 ||
                        GlobalsModel.LoggedUser.User.Role == 3 || GlobalsModel.LoggedUser.User.Role == 7 ||
                        GlobalsModel.LoggedUser.User.Role == 0) && (this.pageName == "DataMigration"
                        || this.pageName == "TransferOut" || this.pageName == "Duplicate" || this.pageName == "TransferInApproval"))
                        {
                            dgFile.Columns.Insert(dgFile.ColumnCount, viewButton);
                        }
                        else
                        {
                            dgFile.Columns.Insert(dgFile.ColumnCount - 1, viewButton);

                            if (GlobalsModel.LoggedUser.User.Role == (int)CommonEnum.Role.SelectRole || GlobalsModel.LoggedUser.User.Role == (int)CommonEnum.Role.Admin || GlobalsModel.LoggedUser.User.Role == (int)CommonEnum.Role.Management)
                            {
                                if (GlobalsModel.TreeViewStructure.MainTagName == "Ward" || GlobalsModel.TreeViewStructure.MainTagName == "Location")
                                {
                                    var clanData = Globals.GeoList.ClanList.Where(d => d.WardId == GlobalsModel.TreeViewStructure.Id).ToList();
                                    ClanModel cmAll = new ClanModel();
                                    cmAll.ClanId = 0;
                                    cmAll.ClanName = "ALL";
                                    clanData.Insert(0, cmAll);
                                    cmbClan.DataSource = null;
                                    cmbClan.Items.Clear();
                                    cmbClan.DataSource = clanData;
                                    cmbClan.DisplayMember = "ClanName";
                                    cmbClan.ValueMember = "ClanId";
                                    cmbClan.SelectedValue = pageList.GeoClanId;

                                    this.dgFile.Columns["ClanName"].Visible = true;
                                    this.pnlClan.Visible = true;

                                }
                                else
                                {
                                    this.dgFile.Columns["ClanName"].Visible = false;
                                    this.pnlClan.Visible = false;
                                }
                            }
                        }
                    }
                }
                else if (this.pageName == "WardRollBook" && (GlobalsModel.TreeViewStructure.MainTagName == "Ward" || GlobalsModel.TreeViewStructure.MainTagName == "Location"))
                {
                    if (GlobalsModel.LoggedUser.User.Role == (int)CommonEnum.Role.SelectRole || GlobalsModel.LoggedUser.User.Role == (int)CommonEnum.Role.Admin || GlobalsModel.LoggedUser.User.Role == (int)CommonEnum.Role.Management)
                    {
                        var clanData = Globals.GeoList.ClanList.Where(d => d.WardId == GlobalsModel.TreeViewStructure.Id).ToList();
                        ClanModel cmAll = new ClanModel();
                        cmAll.ClanId = 0;
                        cmAll.ClanName = "ALL";
                        clanData.Insert(0, cmAll);
                        cmbClan.DataSource = null;
                        cmbClan.Items.Clear();
                        cmbClan.DataSource = clanData;
                        cmbClan.DisplayMember = "ClanName";
                        cmbClan.ValueMember = "ClanId";
                        cmbClan.SelectedValue = pageList.GeoClanId;

                        this.dgFile.Columns["ClanName"].Visible = true;
                        this.pnlClan.Visible = true;

                        pnlExport.Location = new System.Drawing.Point(pnlExport.Location.X + 175, pnlExport.Location.Y);
                    }
                }
                else
                {
                    this.dgFile.Columns["ClanName"].Visible = false;
                    this.pnlClan.Visible = false;
                }
                #endregion

                if (((GlobalsModel.LoggedUser.User.Role == 7 || GlobalsModel.LoggedUser.User.Role == 6 || GlobalsModel.LoggedUser.User.Role == 0) &&
                    this.pageName == "DataMigration" && GlobalsModel.LoggedUser.Database == CommonEnum.Database.Final) &&
                    (cmbStatus.Text == "ALL" || cmbStatus.Text == "OLD" || cmbStatus.Text == "UPDATED"))
                {
                    #region Edit Button
                    DataGridViewButtonColumn editButton = new DataGridViewButtonColumn();
                    //DataGridViewButtonColumn editButton = new DataGridViewButtonColumn();
                    editButton.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    editButton.FlatStyle = FlatStyle.Standard;
                    editButton.HeaderText = "Update";
                    editButton.Name = "Edit";
                    editButton.Text = "Edit";
                    editButton.UseColumnTextForButtonValue = true;
                    editButton.Width = 60;
                    if (dgFile.Columns["Edit"] == null)
                    {
                        if (GlobalsModel.LoggedUser.User.Role == 7 && this.pageName == "DataMigration")
                        {
                            dgFile.Columns.Insert(dgFile.ColumnCount, editButton);
                        }
                        else
                        {
                            dgFile.Columns.Insert(dgFile.ColumnCount - 1, editButton);
                        }
                    }
                    #endregion

                    #region Transfer Button
                    DataGridViewButtonColumn transferButton = new DataGridViewButtonColumn();
                    transferButton.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    transferButton.FlatStyle = FlatStyle.Standard;
                    transferButton.HeaderText = "Transfer";
                    transferButton.Name = "Transfer";
                    transferButton.Text = "Transfer";
                    transferButton.UseColumnTextForButtonValue = true;
                    transferButton.Width = 60;
                    if (dgFile.Columns["Transfer"] == null)
                    {
                        if (GlobalsModel.LoggedUser.User.Role == 7 && this.pageName == "DataMigration")
                        {
                            dgFile.Columns.Insert(dgFile.ColumnCount, transferButton);
                        }
                        else
                        {
                            dgFile.Columns.Insert(dgFile.ColumnCount - 1, transferButton);
                        }
                    }
                    #endregion

                    btnTransfer.Enabled = true;
                }
                else
                {
                    //DB - Enable Edit button for MKA DPO user for Geo Voter
                    #region Edit button for MKA
                    //if (GlobalsModel.LoggedUser.User.Role == 0 && GlobalsModel.LoggedUser.Database == CommonEnum.Database.Staging && this.pageName == "DataMigration")
                    //{
                    //    DataGridViewButtonColumn editButton = new DataGridViewButtonColumn();
                    //    //DataGridViewButtonColumn editButton = new DataGridViewButtonColumn();
                    //    editButton.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    //    editButton.FlatStyle = FlatStyle.Standard;
                    //    editButton.HeaderText = "Update";
                    //    editButton.Name = "Edit";
                    //    editButton.Text = "Edit";
                    //    editButton.UseColumnTextForButtonValue = true;
                    //    editButton.Width = 60;
                    //    if (dgFile.Columns["Edit"] == null)
                    //    {
                    //        //if (GlobalsModel.LoggedUser.User.Role == 9 && this.pageName == "DataMigration")
                    //        //{
                    //        dgFile.Columns.Insert(dgFile.ColumnCount, editButton);
                    //        //}
                    //        //else
                    //        //{
                    //        //    dgFile.Columns.Insert(dgFile.ColumnCount - 1, editButton);
                    //        //}
                    //    }
                    //}
                    #endregion
                    btnTransfer.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Resources.ResourceManager.GetString("ErrorMessage"), "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorLog.Write("File", "BindVoterGird", ex);
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

            if (dgFile != null && dgFile.Rows.Count > 0)
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

                dgPages = pages.Count;
            }
        }
        #endregion

        #region Clear Checkbox
        /// <summary>
        /// Clears the checkbox.
        /// </summary>
        private void ClearCheckbox()
        {
            headerCheckBox.Checked = false;

            //Loop and check and uncheck all row CheckBoxes based on Header Cell CheckBox.
            foreach (DataGridViewRow row in dgFile.Rows)
            {
                DataGridViewCheckBoxCell checkBox = (row.Cells["checkBoxColumn"] as DataGridViewCheckBoxCell);
                checkBox.Value = headerCheckBox.Checked;
            }
        }
        #endregion

        #region Get Grid Cell Data
        /////// <summary>
        /////// Gets the grid cell data.
        /////// </summary>
        ////private VoterModel GetGridCellData(int rowIndex)
        ////{
        ////    VoterModel model = new VoterModel();

        ////    model.ElectorId = Convert.ToInt64(dgFile.Rows[rowIndex].Cells["ElectorId"].Value);

        ////    // Personal Particulars
        ////    model.FamilyNumber = Convert.ToString(dgFile.Rows[rowIndex].Cells["FamilyNumber"].Value);
        ////    model.FamilyName = Convert.ToString(dgFile.Rows[rowIndex].Cells["FamilyName"].Value);
        ////    model.FamilySortOrder = Convert.ToInt32(dgFile.Rows[rowIndex].Cells["FamilySortOrder"].Value);
        ////    model.LName = Convert.ToString(dgFile.Rows[rowIndex].Cells["LName"].Value);
        ////    model.FName = Convert.ToString(dgFile.Rows[rowIndex].Cells["FName"].Value);
        ////    model.NatIDRecordNo = Convert.ToString(dgFile.Rows[rowIndex].Cells["NatIDRecordNo"].Value);
        ////    model.DOB = Convert.ToDateTime(dgFile.Rows[rowIndex].Cells["DOB"].Value);
        ////    model.PlaceOfBirth = Convert.ToString(dgFile.Rows[rowIndex].Cells["PlaceOfBirth"].Value);
        ////    model.ProvinceOfBirth = Convert.ToString(dgFile.Rows[rowIndex].Cells["ProvinceOfBirth"].Value);
        ////    model.Gender = Convert.ToString(dgFile.Rows[rowIndex].Cells["Gender"].Value);
        ////    model.Occupation = Convert.ToString(dgFile.Rows[rowIndex].Cells["Occupation"].Value);
        ////    model.isPWD = Convert.ToBoolean(dgFile.Rows[rowIndex].Cells["isPWD"].Value);
        ////    model.FatherFName = Convert.ToString(dgFile.Rows[rowIndex].Cells["FatherFName"].Value);
        ////    model.FatherLName = Convert.ToString(dgFile.Rows[rowIndex].Cells["FatherLName"].Value);
        ////    model.MotherFName = Convert.ToString(dgFile.Rows[rowIndex].Cells["MotherFName"].Value);
        ////    model.MotherLName = Convert.ToString(dgFile.Rows[rowIndex].Cells["MotherLName"].Value);

        ////    // Place of Residence
        ////    model.ProvinceName = Convert.ToString(dgFile.Rows[rowIndex].Cells["ProvinceName"].Value);
        ////    model.OpenElectorate = Convert.ToString(dgFile.Rows[rowIndex].Cells["OpenElectorate"].Value);
        ////    model.LLGName = Convert.ToString(dgFile.Rows[rowIndex].Cells["LLGName"].Value);
        ////    model.WardName = Convert.ToString(dgFile.Rows[rowIndex].Cells["WardName"].Value);

        ////    // Rural
        ////    model.LocationName = Convert.ToString(dgFile.Rows[rowIndex].Cells["LocationName"].Value);
        ////    model.Clan = Convert.ToString(dgFile.Rows[rowIndex].Cells["Clan"].Value);
        ////    model.SubClan = Convert.ToString(dgFile.Rows[rowIndex].Cells["SubClan"].Value);
        ////    model.RestHouse = Convert.ToString(dgFile.Rows[rowIndex].Cells["RestHouse"].Value);

        ////    // Urban
        ////    model.SectionNo = Convert.ToString(dgFile.Rows[rowIndex].Cells["SectionNo"].Value);
        ////    model.LotNo = Convert.ToString(dgFile.Rows[rowIndex].Cells["LotNo"].Value);
        ////    model.StreetName = Convert.ToString(dgFile.Rows[rowIndex].Cells["StreetName"].Value);
        ////    model.SuburbTownSettlement = Convert.ToString(dgFile.Rows[rowIndex].Cells["SuburbTownSettlement"].Value);

        ////    // Status
        ////    model.Status = Convert.ToString(dgFile.Rows[rowIndex].Cells["Status"].Value);
        ////    model.Comment = Convert.ToString(dgFile.Rows[rowIndex].Cells["Comment"].Value);

        ////    // 
        ////    model.ElectorProfileImage = Convert.ToString(dgFile.Rows[rowIndex].Cells["ElectorProfileImage"].Value);
        ////    model.ElectorProfileImageUrl = Convert.ToString(dgFile.Rows[rowIndex].Cells["ElectorProfileImageUrl"].Value);

        ////    // Elector Form Image
        ////    model.ElectorFormImage = Convert.ToString(dgFile.Rows[rowIndex].Cells["ElectorFormImage"].Value);
        ////    model.ElectorFormImageUrl = Convert.ToString(dgFile.Rows[rowIndex].Cells["ElectorFormImageUrl"].Value);

        ////    model.EnrolmentFormNumber = Convert.ToString(dgFile.Rows[rowIndex].Cells["EnrolmentFormNumber"].Value);

        ////    return model;
        ////}
        #endregion

        #region Manage Voter Status
        /// <summary>
        /// Manages the voter status.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private async void ManageVoterStatus(string isRequest)
        {


            List<long> electorId = new List<long>();
            lstvoters = new List<VoterModel>();

            if (dgFile.Columns.Contains("checkBoxColumn"))
            {
                //if(dgFile !=null && dgFile.Rows!=null && )
                foreach (DataGridViewRow row in dgFile.Rows)
                {
                    bool isSelected = Convert.ToBoolean(row.Cells["checkBoxColumn"].Value);
                    if (isSelected)
                    {
                        electorId.Add(Convert.ToInt64(row.Cells["ElectorId"].Value));
                        lstvoters.Add((VoterModel)row.DataBoundItem);
                    }
                }
            }

            if (electorId.Count > 0)
            {
                DialogResult dialogResult = DialogResult.None;
                if (isRequest == "Approve")
                {
                    dialogResult = MessageBox.Show("Are you sure you want to " + isRequest + " the record?", "PNGEC", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        dialogResult = DialogResult.OK;
                    }
                }
                else
                {
                    rejectedReason = new RejectedReason();
                    dialogResult = rejectedReason.ShowDialog();
                }


                if (dialogResult == DialogResult.OK)
                {
                    GlobalsModel.mainMaster.ShowLoader();

                    try
                    {
                        this.Cursor = Cursors.WaitCursor;
                        //Get location class

                        foreach (var item in lstvoters)
                        {
                            if (rejectedReason != null && !string.IsNullOrWhiteSpace(rejectedReason.txtRejectedReason.Text))
                            {
                                item.RejectedReason = rejectedReason.txtRejectedReason.Text;
                            }

                            item.ARLat = Convert.ToDecimal(GlobalsModel.LocationDetails.Lat);
                            item.ARLong = Convert.ToDecimal(GlobalsModel.LocationDetails.Lon);
                        }
                        requestModel = new CommonRequest();
                        requestModel.database = Convert.ToString(GlobalsModel.LoggedUser.Database);
                        requestModel.ids = string.Join(",", electorId.Select(n => n.ToString()).ToArray());
                        requestModel.recordType = Convert.ToString(cmbStatus.SelectedValue);
                        requestModel.id = GlobalsModel.LoggedUser.User.UserId;
                        requestModel.isApproved = isRequest.Equals("Approve") ? true : false;
                        requestModel.isRejected = isRequest.Equals("Reject") ? true : false;

                        if (this.pageName == "DataMigration")
                        {
                            if (requestModel.isRejected)
                            {
                                requestModel.VoterList = lstvoters.Where(x => x.RecordType == "NORMAL" || x.RecordType == "REJECTED BY RD").ToList();
                            }
                            else
                            {
                                requestModel.VoterList = lstvoters.Where(x => x.RecordType == "NORMAL").ToList();
                            }
                        }
                        else if (this.pageName == "TransferOut" || this.pageName == "Duplicate" || this.pageName == "TransferInApproval")
                        {
                            requestModel.VoterList = lstvoters.Where(x => x.RecordType == "NORMAL").ToList();
                        }
                        else
                        {
                            requestModel.VoterList = lstvoters;
                        }

                        if (requestModel != null && requestModel.VoterList != null && requestModel.VoterList.Count > 0)
                        {
                            response = await GenericHttpClient.PostRequest("DataMigration/ManageVoterStatus", requestModel);
                            if (response.Status)
                            {
                                this.Cursor = Cursors.Arrow;
                                DialogResult result = MessageBox.Show("Status changed successfully", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                if (result == DialogResult.OK)
                                {
                                    await GridViewSetting();
                                }
                            }
                            else
                            {
                                this.Cursor = Cursors.Arrow;
                                MessageBox.Show("Something went wrong, try again later", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            this.Cursor = Cursors.Arrow;

                            MessageBox.Show("At least one NORMAL Record must be selected.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            #region unsed
                            //if (this.pageName == "TransferOut")
                            //{
                            //    MessageBox.Show("At least one NORMAL Record checkbox must be selected.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            //}
                            //else
                            //{
                            //    MessageBox.Show("At least one checkbox must be selected.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            //}
                            #endregion
                        }
                    }
                    catch (Exception ex)
                    {
                        this.Cursor = Cursors.Arrow;
                        MessageBox.Show(Resources.ResourceManager.GetString("ErrorMessage"), "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ErrorLog.Write("File", "ManageVoterStatus", ex);
                    }

                    GlobalsModel.mainMaster.HideLoader();
                }
                //else
                //{
                //    MessageBox.Show("Something went wrong, try again later", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //}

                // Clear all checkbox
                this.ClearCheckbox();
            }
            else
            {
                MessageBox.Show("At least one checkbox must be selected.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        ////    PdfPTable pdfTable = new PdfPTable(3);
        ////    pdfTable.DefaultCell.Padding = 3;
        ////    pdfTable.WidthPercentage = 100;
        ////    pdfTable.HorizontalAlignment = Element.ALIGN_CENTER;
        ////    pdfTable.DefaultCell.BorderWidth = 0.1f;
        ////    pdfTable.HeaderRows = 1;
        ////    iTextSharp.text.Font text = new iTextSharp.text.Font(baseFont, 10, iTextSharp.text.Font.NORMAL);

        ////    foreach (DataGridViewColumn column in dataGridView.Columns)
        ////    {
        ////        if (column.HeaderText == "Voter Name" || column.HeaderText == "Elector Id" || column.HeaderText == "Status")
        ////        {

        ////            iTextSharp.text.Font CoumnHeadertext = new iTextSharp.text.Font(baseFont, 12, iTextSharp.text.Font.BOLD);
        ////            PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText, CoumnHeadertext));
        ////            cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240);
        ////            pdfTable.AddCell(cell);
        ////        }
        ////    }

        ////    foreach (DataGridViewRow row in dataGridView.Rows)
        ////    {
        ////        foreach (DataGridViewCell cell in row.Cells)
        ////        {
        ////            if (cell.ColumnIndex == 1 || cell.ColumnIndex == 2 || cell.ColumnIndex == 32)
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
        ////            Document pdfDoc = new Document(PageSize.A4, 30, 20, 80, 80);
        ////            PdfWriter wri = PdfWriter.GetInstance(pdfDoc, stream);
        ////            wri.PageEvent = new ITextEvents("Ward Roll Book", "", "");
        ////            pdfDoc.Open();
        ////            pdfDoc.Add(pdfTable);
        ////            pdfDoc.Close();
        ////            stream.Close();
        ////        }
        ////    }
        ////}

        #endregion

        #region Get Export Records
        /// <summary>
        /// Gets the export records list.
        /// </summary>
        private byte[] WardRollBookExportReport()
        {
            byte[] bytest = null;

            if (dtExportReport != null && dtExportReport.Rows.Count > 0)
            {
                if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Staging && cmbStatus.Text != "DUPLICATE")
                {
                    bytest = WardRollBookExportReportSource("Staging");
                }
                else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Preliminary && cmbStatus.Text != "DUPLICATE")
                {
                    bytest = WardRollBookExportReportSource("Preliminary");
                }
                else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Final && cmbStatus.Text != "DUPLICATE")
                {
                    bytest = WardRollBookExportReportSource("Final");
                }
                else if (cmbStatus.Text == "DUPLICATE")
                {
                    bytest = WardRollBookDuplicateVoterExportReportSource();
                }
            }

            return bytest;
        }
        #endregion

        #region Get Export Records Clan
        /// <summary>
        /// Gets the export records list.
        /// </summary>
        private byte[] WardClanRollBookExportReport()
        {
            byte[] bytest = null;

            if (dtExportReport != null && dtExportReport.Rows.Count > 0)
            {
                if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Staging && cmbStatus.Text != "DUPLICATE")
                {
                    bytest = WardClanRollBookExportReportSource("Staging");
                }
                else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Preliminary && cmbStatus.Text != "DUPLICATE")
                {
                    bytest = WardClanRollBookExportReportSource("Preliminary");
                }
                else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Final && cmbStatus.Text != "DUPLICATE")
                {
                    bytest = WardClanRollBookExportReportSource("Final");
                }
                else if (cmbStatus.Text == "DUPLICATE")
                {
                    bytest = WardRollBookDuplicateVoterExportReportSource();
                }
            }

            return bytest;
        }
        #endregion

        #region Get Export Records Ward Photo
        /// <summary>
        /// Gets the export records list.
        /// </summary>
        private byte[] WardPhotoRollBookExportReport()
        {
            byte[] bytest = null;

            if (dtExportReport != null && dtExportReport.Rows.Count > 0)
            {
                if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Staging && cmbStatus.Text != "DUPLICATE")
                {
                    bytest = WardPhotoRollBookExportReportSource("Staging");
                }
                else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Preliminary && cmbStatus.Text != "DUPLICATE")
                {
                    bytest = WardPhotoRollBookExportReportSource("Preliminary");
                }
                else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Final && cmbStatus.Text != "DUPLICATE")
                {
                    bytest = WardPhotoRollBookExportReportSource("Final");
                }
                else if (cmbStatus.Text == "DUPLICATE")
                {
                    bytest = WardRollBookDuplicateVoterExportReportSource();
                }
            }

            return bytest;
        }
        #endregion

        #region MKAWardRollBook Export ReportSource
        /// <summary>
        /// Wards the roll book export report source.
        /// </summary>
        /// <param name="report">The report.</param>
        /// <param name="dt">The dt.</param>
        /// <returns></returns>
        private byte[] WardClanRollBookExportReportSource(string report)
        {
            dsExportReport = new DataSet();
            byte[] bytes = null;

            this.dtExportReport.TableName = "tblWardRollBook";
            if (!dsExportReport.Tables.Contains(this.dtExportReport.TableName))
            {
                dsExportReport.Tables.Add(this.dtExportReport);
            }

            if (dsExportReport.Tables[0].Rows != null && dsExportReport.Tables[0].Rows.Count > 0)
            {
                string ReportTitle = string.Empty, Version = string.Empty, rdlcReport = string.Empty, GenderSplit = string.Empty, AlphaSplitStart = string.Empty, AlphaSplitEnd = string.Empty, ClanName = string.Empty;
                bool IsLocation = false, IsGenderSplit = false, IsAlphaSplit = false;

                // isLocation
                if (TreeStructure.MainTagName == "Location")
                {
                    IsLocation = true;
                }

                // isGenderSplit
                if (exportModel.rdbGenderAll.Checked == false)
                {
                    IsGenderSplit = true;

                    if (exportModel.rdbGenderMale.Checked)
                    {
                        GenderSplit = "MALE";
                    }
                    else if (exportModel.rdbGenderFemale.Checked)
                    {
                        GenderSplit = "FEMALE";
                    }
                    else if (exportModel.rdbGenderOther.Checked)
                    {
                        GenderSplit = "OTHER";
                    }
                }

                // get Clan Name
                if (Convert.ToInt32(cmbClan.SelectedValue) == 0)
                {
                    int wardId = 0;

                    if (TreeStructure.MainTagName == "Ward")
                    {
                        wardId = TreeStructure.Id;
                    }
                    else
                    {
                        wardId = TreeStructure.ParentId;
                    }

                    var clans = Globals.GeoList.ClanList.Where(c => c.WardId == wardId).Select(x => x.ClanName).OrderBy(name => name).ToList();

                    ClanName = string.Join(", ", clans);
                }
                else
                {
                    ClanName = cmbClan.Text.Trim();
                }

                // isAlphaSplit
                if (exportModel.rdbAlphaSplit.Checked)
                {
                    IsAlphaSplit = true;
                    AlphaSplitStart = exportModel.txtAlphaSplitStart.Text.Trim().ToUpper();
                    AlphaSplitEnd = exportModel.txtAlphaSplitEnd.Text.Trim().ToUpper();
                }

                #region RDLC Report Name
                if (report == "Staging")
                {
                    if (!string.IsNullOrWhiteSpace(exportModel.txtReportTitle.Text.Trim()))
                    {
                        ReportTitle = exportModel.txtReportTitle.Text.Trim();
                    }
                    else
                    {
                        ReportTitle = $"WORKING ROLL {dsExportReport.Tables[0].Rows[0]["MKADistrict"].ToString().ToUpper()} ASSEMBLY ELECTION {DateTime.Now.ToString("yyyy")}";
                    }

                    Version = "V1";

                    rdlcReport = "WardClanRollBook.rdlc";
                }
                else if (report == "Preliminary")
                {
                    if (!string.IsNullOrWhiteSpace(exportModel.txtReportTitle.Text.Trim()))
                    {
                        ReportTitle = exportModel.txtReportTitle.Text.Trim();
                    }
                    else
                    {
                        //ReportTitle = $"PRELIMINARY ELECTORAL ROLL {DateTime.Now.ToString("yyyy")}";
                        ReportTitle = $"PRELIMINARY ELECTORAL ROLL {dsExportReport.Tables[0].Rows[0]["MKADistrict"].ToString().ToUpper()} ASSEMBLY ELECTION {DateTime.Now.ToString("yyyy")}";

                    }

                    Version = "version 1";
                    rdlcReport = "WardClanRollBookPreliminary.rdlc";

                }
                else if (report == "Final")
                {
                    if (exportModel.rdbWardRollBook.Checked == true && GlobalsModel.LoggedUser.Database == CommonEnum.Database.Final)
                    {
                        if (!string.IsNullOrWhiteSpace(exportModel.txtReportTitle.Text.Trim()))
                        {
                            ReportTitle = exportModel.txtReportTitle.Text.Trim();
                        }
                        else
                        {
                            //ReportTitle = $"ELECTORAL ROLL {DateTime.Now.ToString("yyyy")}";
                            ReportTitle = $"ELECTORAL ROLL {dsExportReport.Tables[0].Rows[0]["MKADistrict"].ToString().ToUpper()} ASSEMBLY ELECTION {DateTime.Now.ToString("yyyy")}";

                        }
                    }

                    //ReportTitle = "ELECTORAL ROLL 2022";
                    Version = "version 1";
                    rdlcReport = "WardClanRollBookFinal.rdlc";

                }
                #endregion

                Warning[] warnings;
                string[] streamIds;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;

                #region RDLC parameters
                int paramCount = 19;
                if (report == "Final")
                {
                    paramCount = 21;
                }

                //This is optional if you have parameter then you can add parameters as much as you want
                ReportParameter[] param = new ReportParameter[paramCount];
                param[0] = new ReportParameter("Region", dsExportReport.Tables[0].Rows[0]["Region"].ToString(), true);
                param[1] = new ReportParameter("Province", dsExportReport.Tables[0].Rows[0]["Province"].ToString(), true);
                param[2] = new ReportParameter("District", dsExportReport.Tables[0].Rows[0]["District"].ToString(), true);
                param[3] = new ReportParameter("LLG", dsExportReport.Tables[0].Rows[0]["LLG"].ToString(), true);
                param[4] = new ReportParameter("Ward", string.Format("({0}) - {1}", dsExportReport.Tables[0].Rows[0]["WardNo"].ToString(), dsExportReport.Tables[0].Rows[0]["MKAWard"].ToString()), true);
                //param[4] = new ReportParameter("Ward", dsExportReport.Tables[0].Rows[0]["Ward"].ToString(), true);
                if (IsLocation)
                {
                    param[5] = new ReportParameter("Location", dsExportReport.Tables[0].Rows[0]["Location"].ToString(), true);
                }
                else
                {
                    IsLocation = true;
                    var locations = (from DataRow dr in dsExportReport.Tables[0].Rows where dr["Location"] != DBNull.Value select (string)dr["Location"]).Distinct().OrderBy(name => name);
                    //var locations = dsExportReport.Tables[0].AsEnumerable().Where(r => r.Field<string>("MKALocation") != null).Select(x => x.Field<string>("MKALocation")).Distinct().OrderBy(name => name);

                    param[5] = new ReportParameter("Location", string.Join(", ", locations), true);
                }
                param[6] = new ReportParameter("Clan", ClanName, true);
                param[7] = new ReportParameter("Electors", dsExportReport.Tables[0].Rows.Count.ToString(), true);
                param[8] = new ReportParameter("DateTime", DateTime.Now.ToString("dddd, MMMM dd, yyyy"), true);
                param[9] = new ReportParameter("ReportTitle", ReportTitle, true);
                param[10] = new ReportParameter("Version", Version, true);
                param[11] = new ReportParameter("WardNumberWardName", dsExportReport.Tables[0].Rows[0]["WardNumberWardName"].ToString(), true);
                param[12] = new ReportParameter("IsDeletionReport", Convert.ToString(exportModel.chkIncludePossibleDeletionReport.Checked), true);
                param[13] = new ReportParameter("IsLocation", Convert.ToString(IsLocation), true);
                param[14] = new ReportParameter("IsGenderSplit", Convert.ToString(IsGenderSplit), true);
                param[15] = new ReportParameter("GenderSplit", GenderSplit, true);
                param[16] = new ReportParameter("IsAlphaSplit", Convert.ToString(IsAlphaSplit), true);
                param[17] = new ReportParameter("AlphaSplitStart", AlphaSplitStart, true);
                param[18] = new ReportParameter("AlphaSplitEnd", AlphaSplitEnd, true);

                if (report == "Final")
                {
                    param[19] = new ReportParameter("GeoNameAndYear", "Electorate Election 2023", true);
                    param[20] = new ReportParameter("FooterDate", DateTime.Now.ToString("dd-MMM-yyyy"), true);
                }
                #endregion

                ReportDataSource dataSource = new ReportDataSource("ds_MKAWardRollBook", dsExportReport.Tables[0].Copy());

                #region Possible Deletion Report

                dtDeletionExportReport.TableName = "tblWardRollDeletion";
                dsExportReport.Tables.Add(dtDeletionExportReport.Copy());
                ReportDataSource datasourceDeletion = new ReportDataSource("ds_WardRollDeletion", dsExportReport.Tables[1].Copy());

                // Create Tables
                DataTable tblMKAWardClanSectionSummary = new DataTable("tblMKAWardClanSectionSummary");
                DataTable tblWardRollDeletion = new DataTable("tblWardRollDeletion");
                DataTable tblWardRollDeletionTrnsOutStatus = new DataTable("tblWardRollDeletionTrnsOutStatus");
                DataTable tblWardRollDeletionTrnsOutForm11 = new DataTable("tblWardRollDeletionTrnsOutForm11");

                #region Ward Roll Deletion
                tblWardRollDeletion.Clear();
                var wardRollDeletion = from rows in dtDeletionExportReport.AsEnumerable()
                                       where (rows.Field<string>("Status") != "TRANSFERRED OUT")
                                       select rows;

                if (wardRollDeletion != null && wardRollDeletion.Count() > 0)
                {
                    tblWardRollDeletion.Merge(wardRollDeletion.CopyToDataTable());

                    if (tblWardRollDeletion != null && tblWardRollDeletion.Rows != null && tblWardRollDeletion.Rows.Count > 0)
                    {
                        for (int i = 0; i < tblWardRollDeletion.Rows.Count; i++)
                        {
                            tblWardRollDeletion.Rows[i]["Seq"] = (i + 1);
                        }
                    }
                }
                #endregion

                #region Ward Roll Deletion Transferred Out Status
                tblWardRollDeletionTrnsOutStatus.Clear();
                var wardRollDeletionTrnsOutStatus = from rows in dtDeletionExportReport.AsEnumerable()
                                                    where (rows.Field<string>("Status") == "TRANSFERRED OUT")
                                                       && (rows.Field<int>("TransferInLocationId") == 0)
                                                    select rows;
                if (wardRollDeletionTrnsOutStatus != null && wardRollDeletionTrnsOutStatus.Count() > 0)
                {
                    tblWardRollDeletionTrnsOutStatus.Merge(wardRollDeletionTrnsOutStatus.CopyToDataTable());

                    if (tblWardRollDeletionTrnsOutStatus != null && tblWardRollDeletionTrnsOutStatus.Rows != null && tblWardRollDeletionTrnsOutStatus.Rows.Count > 0)
                    {
                        for (int i = 0; i < tblWardRollDeletionTrnsOutStatus.Rows.Count; i++)
                        {
                            tblWardRollDeletionTrnsOutStatus.Rows[i]["Seq"] = (i + 1);
                        }
                    }
                }
                #endregion

                #region Ward Roll Deletion Transferred Out Form11
                tblWardRollDeletionTrnsOutForm11.Clear();
                var wardRollDeletionTrnsOutForm11 = from rows in dtDeletionExportReport.AsEnumerable()
                                                    where (rows.Field<string>("Status") == "TRANSFERRED OUT")
                                                       && (rows.Field<int>("TransferInLocationId") > 0)
                                                    select rows;
                if (wardRollDeletionTrnsOutForm11 != null && wardRollDeletionTrnsOutForm11.Count() > 0)
                {
                    tblWardRollDeletionTrnsOutForm11.Merge(wardRollDeletionTrnsOutForm11.CopyToDataTable());

                    if (tblWardRollDeletionTrnsOutForm11 != null && tblWardRollDeletionTrnsOutForm11.Rows != null && tblWardRollDeletionTrnsOutForm11.Rows.Count > 0)
                    {
                        for (int i = 0; i < tblWardRollDeletionTrnsOutForm11.Rows.Count; i++)
                        {
                            tblWardRollDeletionTrnsOutForm11.Rows[i]["Seq"] = (i + 1);
                        }
                    }
                }
                #endregion

                #endregion

                if (dtWardRollFrontCover == null)
                {
                    dtWardRollFrontCover = new DataTable("tblWardRollFrontCover");
                }
                dtWardRollFrontCover = new DataTable("tblWardRollFrontCover");
                ReportDataSource datasourceWardRollFrontCover = new ReportDataSource("ds_MKAWardRollFrontCover", dtWardRollFrontCover.Copy());

                #region MKAWardClanSectionSummary
                if (report == "Final")
                {
                    if (dtWardClanSectionSummary.Rows != null && dtWardClanSectionSummary.Rows.Count > 0)
                    {
                        tblMKAWardClanSectionSummary.Clear();
                        tblMKAWardClanSectionSummary = dtWardClanSectionSummary.Copy();
                    }
                }
                #endregion

                ReportDataSource datasourceMKAWardClanSectionSummary = new ReportDataSource("ds_MKAWardClanSectionSummary", tblMKAWardClanSectionSummary);
                ReportDataSource dataSourceWardRollDeletionReport = new ReportDataSource("ds_MKAWardRollDeletion", tblWardRollDeletion);
                ReportDataSource datasourceWardRollDeletionTrnsOutStatus = new ReportDataSource("ds_MKAWardRollDeletionTrnsOutStatus", tblWardRollDeletionTrnsOutStatus);
                ReportDataSource datasourceWardRollDeletionTrnsOutForm11 = new ReportDataSource("ds_MKAWardRollDeletionTrnsOutForm11", tblWardRollDeletionTrnsOutForm11);

                ReportViewer viewer = new ReportViewer();
                viewer.LocalReport.Refresh();
                viewer.LocalReport.ReportPath = "Report/" + rdlcReport; //This is your rdlc name.
                viewer.LocalReport.SetParameters(param);

                viewer.LocalReport.DataSources.Add(dataSource); // Add  datasource here
                if (report == "Final")
                {
                    viewer.LocalReport.DataSources.Add(datasourceMKAWardClanSectionSummary); // Add Ward clan section summary here
                }
                //viewer.LocalReport.DataSources.Add(datasourceDeletion); // Add Deletion datasource here
                viewer.LocalReport.DataSources.Add(dataSourceWardRollDeletionReport); // Add Deletion datasource here
                viewer.LocalReport.DataSources.Add(datasourceWardRollDeletionTrnsOutStatus); // Add Deletion datasource here
                viewer.LocalReport.DataSources.Add(datasourceWardRollDeletionTrnsOutForm11); // Add Deletion datasource here
                viewer.LocalReport.DataSources.Add(datasourceWardRollFrontCover); // Add Deletion datasource here

                bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
            }
            return bytes;
        }
        #endregion


        //#region WardRollBook Export ReportSource
        ///// <summary>
        ///// Wards the roll book export report source.
        ///// </summary>
        ///// <param name="report">The report.</param>
        ///// <param name="dt">The dt.</param>
        ///// <returns></returns>
        //private byte[] WardClanRollBookExportReportSource(string report)
        //{
        //    dsExportReport = new DataSet();
        //    byte[] bytes = null;

        //    this.dtExportReport.TableName = "tblWardRollBook";
        //    if (!dsExportReport.Tables.Contains(this.dtExportReport.TableName))
        //    {
        //        dsExportReport.Tables.Add(this.dtExportReport);
        //    }

        //    if (dsExportReport.Tables[0].Rows != null && dsExportReport.Tables[0].Rows.Count > 0)
        //    {
        //        string ReportTitle = string.Empty, Version = string.Empty, rdlcReport = string.Empty, GenderSplit = string.Empty, AlphaSplitStart = string.Empty, AlphaSplitEnd = string.Empty, ClanName = string.Empty;
        //        bool IsLocation = false, IsGenderSplit = false, IsAlphaSplit = false;

        //        // isLocation
        //        if (TreeStructure.MainTagName == "Location")
        //        {
        //            IsLocation = true;
        //        }

        //        // isGenderSplit
        //        if (exportModel.rdbGenderAll.Checked == false)
        //        {
        //            IsGenderSplit = true;

        //            if (exportModel.rdbGenderMale.Checked)
        //            {
        //                GenderSplit = "MALE";
        //            }
        //            else if (exportModel.rdbGenderFemale.Checked)
        //            {
        //                GenderSplit = "FEMALE";
        //            }
        //            else if (exportModel.rdbGenderOther.Checked)
        //            {
        //                GenderSplit = "OTHER";
        //            }
        //        }

        //        // get Clan Name
        //        if (Convert.ToInt32(cmbClan.SelectedValue) == 0)
        //        {
        //            int wardId = 0;

        //            if (TreeStructure.MainTagName == "Ward")
        //            {
        //                wardId = TreeStructure.Id;
        //            }
        //            else
        //            {
        //                wardId = TreeStructure.ParentId;
        //            }

        //            var clans = Globals.GeoList.ClanList.Where(c => c.WardId == wardId).Select(x => x.ClanName).OrderBy(name => name).ToList();

        //            ClanName = string.Join(", ", clans);
        //        }
        //        else
        //        {
        //            ClanName = cmbClan.Text.Trim();
        //        }

        //        // isAlphaSplit
        //        if (exportModel.rdbAlphaSplit.Checked)
        //        {
        //            IsAlphaSplit = true;
        //            AlphaSplitStart = exportModel.txtAlphaSplitStart.Text.Trim().ToUpper();
        //            AlphaSplitEnd = exportModel.txtAlphaSplitEnd.Text.Trim().ToUpper();
        //        }

        //        #region RDLC Report Name
        //        if (report == "Staging")
        //        {
        //            if (!string.IsNullOrWhiteSpace(exportModel.txtReportTitle.Text.Trim()))
        //            {
        //                ReportTitle = exportModel.txtReportTitle.Text.Trim();
        //            }
        //            else
        //            {
        //                ReportTitle = $"WORKING ROLL {dsExportReport.Tables[0].Rows[0]["District"].ToString().ToUpper()} ASSEMBLY ELECTION {DateTime.Now.ToString("yyyy")}";
        //            }

        //            Version = "V1";

        //            rdlcReport = "WardClanBaseRoll.rdlc";
        //        }
        //        else if (report == "Preliminary")
        //        {
        //            if (!string.IsNullOrWhiteSpace(exportModel.txtReportTitle.Text.Trim()))
        //            {
        //                ReportTitle = exportModel.txtReportTitle.Text.Trim();
        //            }
        //            else
        //            {
        //                //ReportTitle = $"PRELIMINARY ELECTORAL ROLL {DateTime.Now.ToString("yyyy")}";
        //                ReportTitle = $"PRELIMINARY ELECTORAL ROLL {dsExportReport.Tables[0].Rows[0]["District"].ToString().ToUpper()} ASSEMBLY ELECTION {DateTime.Now.ToString("yyyy")}";

        //            }

        //            Version = "version 1";
        //            rdlcReport = "WardClanBaseRollPreliminary.rdlc";

        //        }
        //        else if (report == "Final")
        //        {
        //            if (exportModel.rdbWardRollBook.Checked == true && GlobalsModel.LoggedUser.Database == CommonEnum.Database.Final)
        //            {
        //                if (!string.IsNullOrWhiteSpace(exportModel.txtReportTitle.Text.Trim()))
        //                {
        //                    ReportTitle = exportModel.txtReportTitle.Text.Trim();
        //                }
        //                else
        //                {
        //                    //ReportTitle = $"ELECTORAL ROLL {DateTime.Now.ToString("yyyy")}";
        //                    ReportTitle = $"ELECTORAL ROLL {dsExportReport.Tables[0].Rows[0]["District"].ToString().ToUpper()} ASSEMBLY ELECTION {DateTime.Now.ToString("yyyy")}";

        //                }
        //            }

        //            //ReportTitle = "ELECTORAL ROLL 2022";
        //            Version = "version 1";
        //            rdlcReport = "WardClanBaseRollFinal.rdlc";

        //        }
        //        #endregion

        //        Warning[] warnings;
        //        string[] streamIds;
        //        string mimeType = string.Empty;
        //        string encoding = string.Empty;
        //        string extension = string.Empty;

        //        #region RDLC parameters
        //        int paramCount = 19;
        //        if (report == "Final")
        //        {
        //            paramCount = 21;
        //        }

        //        //This is optional if you have parameter then you can add parameters as much as you want
        //        ReportParameter[] param = new ReportParameter[paramCount];
        //        param[0] = new ReportParameter("Region", dsExportReport.Tables[0].Rows[0]["Region"].ToString(), true);
        //        param[1] = new ReportParameter("Province", dsExportReport.Tables[0].Rows[0]["Province"].ToString(), true);
        //        param[2] = new ReportParameter("District", dsExportReport.Tables[0].Rows[0]["District"].ToString(), true);
        //        param[3] = new ReportParameter("LLG", dsExportReport.Tables[0].Rows[0]["LLG"].ToString(), true);
        //        param[4] = new ReportParameter("Ward", string.Format("({0}) - {1}", dsExportReport.Tables[0].Rows[0]["WardNo"].ToString(), dsExportReport.Tables[0].Rows[0]["Ward"].ToString()), true);
        //        //param[4] = new ReportParameter("Ward", dsExportReport.Tables[0].Rows[0]["Ward"].ToString(), true);
        //        if (IsLocation)
        //        {
        //            param[5] = new ReportParameter("Location", dsExportReport.Tables[0].Rows[0]["Location"].ToString(), true);
        //        }
        //        else
        //        {
        //            IsLocation = true;
        //            var locations = (from DataRow dr in dsExportReport.Tables[0].Rows where dr["Location"] != DBNull.Value select (string)dr["Location"]).Distinct().OrderBy(name => name);
        //            //var locations = dsExportReport.Tables[0].AsEnumerable().Where(r => r.Field<string>("Location") != null).Select(x => x.Field<string>("Location")).Distinct().OrderBy(name => name);

        //            param[5] = new ReportParameter("Location", string.Join(", ", locations), true);
        //        }
        //        param[6] = new ReportParameter("Clan", ClanName, true);
        //        param[7] = new ReportParameter("Electors", dsExportReport.Tables[0].Rows.Count.ToString(), true);
        //        param[8] = new ReportParameter("DateTime", DateTime.Now.ToString("dddd, MMMM dd, yyyy"), true);
        //        param[9] = new ReportParameter("ReportTitle", ReportTitle, true);
        //        param[10] = new ReportParameter("Version", Version, true);
        //        param[11] = new ReportParameter("WardNumberWardName", dsExportReport.Tables[0].Rows[0]["WardNumberWardName"].ToString(), true);
        //        param[12] = new ReportParameter("IsDeletionReport", Convert.ToString(exportModel.chkIncludePossibleDeletionReport.Checked), true);
        //        param[13] = new ReportParameter("IsLocation", Convert.ToString(IsLocation), true);
        //        param[14] = new ReportParameter("IsGenderSplit", Convert.ToString(IsGenderSplit), true);
        //        param[15] = new ReportParameter("GenderSplit", GenderSplit, true);
        //        param[16] = new ReportParameter("IsAlphaSplit", Convert.ToString(IsAlphaSplit), true);
        //        param[17] = new ReportParameter("AlphaSplitStart", AlphaSplitStart, true);
        //        param[18] = new ReportParameter("AlphaSplitEnd", AlphaSplitEnd, true);

        //        if (report == "Final")
        //        {
        //            param[19] = new ReportParameter("GeoNameAndYear", "Electorate Election 2024", true);
        //            param[20] = new ReportParameter("FooterDate", DateTime.Now.ToString("dd-MMM-yyyy"), true);
        //        }
        //        #endregion

        //        ReportDataSource dataSource = new ReportDataSource("ds_WardRollBook", dsExportReport.Tables[0].Copy());

        //        #region Possible Deletion Report

        //        dtDeletionExportReport.TableName = "tblWardRollDeletion";
        //        dsExportReport.Tables.Add(dtDeletionExportReport.Copy());
        //        ReportDataSource datasourceDeletion = new ReportDataSource("ds_WardRollDeletion", dsExportReport.Tables[1].Copy());

        //        // Create Tables
        //        DataTable tblWardClanSectionSummary = new DataTable("tblWardClanSectionSummary");
        //        DataTable tblWardRollDeletion = new DataTable("tblWardRollDeletion");
        //        DataTable tblWardRollDeletionTrnsOutStatus = new DataTable("tblWardRollDeletionTrnsOutStatus");
        //        DataTable tblWardRollDeletionTrnsOutForm11 = new DataTable("tblWardRollDeletionTrnsOutForm11");

        //        #region Ward Roll Deletion
        //        tblWardRollDeletion.Clear();
        //        var wardRollDeletion = from rows in dtDeletionExportReport.AsEnumerable()
        //                               where (rows.Field<string>("Status") != "TRANSFERRED OUT")
        //                               select rows;

        //        if (wardRollDeletion != null && wardRollDeletion.Count() > 0)
        //        {
        //            tblWardRollDeletion.Merge(wardRollDeletion.CopyToDataTable());

        //            if (tblWardRollDeletion != null && tblWardRollDeletion.Rows != null && tblWardRollDeletion.Rows.Count > 0)
        //            {
        //                for (int i = 0; i < tblWardRollDeletion.Rows.Count; i++)
        //                {
        //                    tblWardRollDeletion.Rows[i]["Seq"] = (i + 1);
        //                }
        //            }
        //        }
        //        #endregion

        //        #region Ward Roll Deletion Transferred Out Status
        //        tblWardRollDeletionTrnsOutStatus.Clear();
        //        var wardRollDeletionTrnsOutStatus = from rows in dtDeletionExportReport.AsEnumerable()
        //                                            where (rows.Field<string>("Status") == "TRANSFERRED OUT")
        //                                               && (rows.Field<int>("TransferInLocationId") == 0)
        //                                            select rows;
        //        if (wardRollDeletionTrnsOutStatus != null && wardRollDeletionTrnsOutStatus.Count() > 0)
        //        {
        //            tblWardRollDeletionTrnsOutStatus.Merge(wardRollDeletionTrnsOutStatus.CopyToDataTable());

        //            if (tblWardRollDeletionTrnsOutStatus != null && tblWardRollDeletionTrnsOutStatus.Rows != null && tblWardRollDeletionTrnsOutStatus.Rows.Count > 0)
        //            {
        //                for (int i = 0; i < tblWardRollDeletionTrnsOutStatus.Rows.Count; i++)
        //                {
        //                    tblWardRollDeletionTrnsOutStatus.Rows[i]["Seq"] = (i + 1);
        //                }
        //            }
        //        }
        //        #endregion

        //        #region Ward Roll Deletion Transferred Out Form11
        //        tblWardRollDeletionTrnsOutForm11.Clear();
        //        var wardRollDeletionTrnsOutForm11 = from rows in dtDeletionExportReport.AsEnumerable()
        //                                            where (rows.Field<string>("Status") == "TRANSFERRED OUT")
        //                                               && (rows.Field<int>("TransferInLocationId") > 0)
        //                                            select rows;
        //        if (wardRollDeletionTrnsOutForm11 != null && wardRollDeletionTrnsOutForm11.Count() > 0)
        //        {
        //            tblWardRollDeletionTrnsOutForm11.Merge(wardRollDeletionTrnsOutForm11.CopyToDataTable());

        //            if (tblWardRollDeletionTrnsOutForm11 != null && tblWardRollDeletionTrnsOutForm11.Rows != null && tblWardRollDeletionTrnsOutForm11.Rows.Count > 0)
        //            {
        //                for (int i = 0; i < tblWardRollDeletionTrnsOutForm11.Rows.Count; i++)
        //                {
        //                    tblWardRollDeletionTrnsOutForm11.Rows[i]["Seq"] = (i + 1);
        //                }
        //            }
        //        }
        //        #endregion

        //        #endregion

        //        if (dtWardRollFrontCover == null)
        //        {
        //            dtWardRollFrontCover = new DataTable("tblWardRollFrontCover");
        //        }
        //        dtWardRollFrontCover = new DataTable("tblWardRollFrontCover");
        //        ReportDataSource datasourceWardRollFrontCover = new ReportDataSource("ds_WardRollFrontCover", dtWardRollFrontCover.Copy());

        //        #region WardClanSectionSummary
        //        if (report == "Final")
        //        {
        //            if (dtWardClanSectionSummary.Rows != null && dtWardClanSectionSummary.Rows.Count > 0)
        //            {
        //                tblWardClanSectionSummary.Clear();
        //                tblWardClanSectionSummary = dtWardClanSectionSummary.Copy();
        //            }
        //        }
        //        #endregion

        //        ReportDataSource datasourceWardClanSectionSummary = new ReportDataSource("ds_WardClanSectionSummary", tblWardClanSectionSummary);
        //        ReportDataSource dataSourceWardRollDeletionReport = new ReportDataSource("ds_WardRollDeletion", tblWardRollDeletion);
        //        ReportDataSource datasourceWardRollDeletionTrnsOutStatus = new ReportDataSource("ds_WardRollDeletionTrnsOutStatus", tblWardRollDeletionTrnsOutStatus);
        //        ReportDataSource datasourceWardRollDeletionTrnsOutForm11 = new ReportDataSource("ds_WardRollDeletionTrnsOutForm11", tblWardRollDeletionTrnsOutForm11);

        //        ReportViewer viewer = new ReportViewer();
        //        viewer.LocalReport.Refresh();
        //        viewer.LocalReport.ReportPath = "Report/" + rdlcReport; //This is your rdlc name.
        //        viewer.LocalReport.SetParameters(param);

        //        viewer.LocalReport.DataSources.Add(dataSource); // Add  datasource here
        //        if (report == "Final")
        //        {
        //            viewer.LocalReport.DataSources.Add(datasourceWardClanSectionSummary); // Add Ward clan section summary here
        //        }
        //        //viewer.LocalReport.DataSources.Add(datasourceDeletion); // Add Deletion datasource here
        //        viewer.LocalReport.DataSources.Add(dataSourceWardRollDeletionReport); // Add Deletion datasource here
        //        viewer.LocalReport.DataSources.Add(datasourceWardRollDeletionTrnsOutStatus); // Add Deletion datasource here
        //        viewer.LocalReport.DataSources.Add(datasourceWardRollDeletionTrnsOutForm11); // Add Deletion datasource here
        //        viewer.LocalReport.DataSources.Add(datasourceWardRollFrontCover); // Add Deletion datasource here

        //        bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
        //    }
        //    return bytes;
        //}
        //#endregion

        #region WardRollBook Export ReportSource
        /// <summary>
        /// Wards the roll book export report source.
        /// </summary>
        /// <param name="report">The report.</param>
        /// <param name="dt">The dt.</param>
        /// <returns></returns>
        private byte[] WardRollBookExportReportSource(string report)
        {
            dsExportReport = new DataSet();
            byte[] bytes = null;

            this.dtExportReport.TableName = "tblWardRollBook";
            if (!dsExportReport.Tables.Contains(this.dtExportReport.TableName))
            {
                if (exportModel.chkWardRollBookLandscap.Checked)
                {
                    var sortedVyLocation = this.dtExportReport.AsEnumerable()
               .OrderBy(r => r.Field<string>("Location")).CopyToDataTable();

                    for (int i = 0; i < sortedVyLocation.Rows.Count; i++)
                    {
                        sortedVyLocation.Rows[i]["Seq"] = (i + 1);
                    }

                    dsExportReport.Tables.Add(sortedVyLocation);
                }
                else
                {
                    dsExportReport.Tables.Add(this.dtExportReport);
                }
            }

            if (dsExportReport.Tables[0].Rows != null && dsExportReport.Tables[0].Rows.Count > 0)
            {
                string ReportTitle = string.Empty, Version = string.Empty, rdlcReport = string.Empty, GenderSplit = string.Empty, AlphaSplitStart = string.Empty, AlphaSplitEnd = string.Empty;
                bool IsLocation = false, IsGenderSplit = false, IsAlphaSplit = false;

                // isLocation
                if (TreeStructure.MainTagName == "Location" || TreeStructure.MainTagName == "Ward")
                {
                    IsLocation = true;
                }

                // isGenderSplit
                if (exportModel.rdbGenderAll.Checked == false)
                {
                    IsGenderSplit = true;

                    if (exportModel.rdbGenderMale.Checked)
                    {
                        GenderSplit = "MALE";
                    }
                    else if (exportModel.rdbGenderFemale.Checked)
                    {
                        GenderSplit = "FEMALE";
                    }
                    else if (exportModel.rdbGenderOther.Checked)
                    {
                        GenderSplit = "OTHER";
                    }
                }

                // isAlphaSplit
                if (exportModel.rdbAlphaSplit.Checked)
                {
                    IsAlphaSplit = true;
                    AlphaSplitStart = exportModel.txtAlphaSplitStart.Text.Trim().ToUpper();
                    AlphaSplitEnd = exportModel.txtAlphaSplitEnd.Text.Trim().ToUpper();
                }

                #region RDLC Report Name
                if (report == "Staging")
                {
                    if (!string.IsNullOrWhiteSpace(exportModel.txtReportTitle.Text.Trim()))
                    {
                        ReportTitle = exportModel.txtReportTitle.Text.Trim();
                    }
                    else
                    {
                        ReportTitle = $"WORKING ROLL {DateTime.Now.ToString("yyyy")}";
                    }

                    Version = "V1";

                    if (exportModel.chkWardRollBookLandscap.Checked)
                    {
                        rdlcReport = "WardRollBookLandscap.rdlc";
                    }
                    else
                    {
                        rdlcReport = "WardRollBook.rdlc";
                    }
                }
                else if (report == "Preliminary")
                {
                    if (!string.IsNullOrWhiteSpace(exportModel.txtReportTitle.Text.Trim()))
                    {
                        ReportTitle = exportModel.txtReportTitle.Text.Trim();
                    }
                    else
                    {
                        ReportTitle = $"PRELIMINARY ELECTORAL ROLL {DateTime.Now.ToString("yyyy")}";
                    }

                    Version = "version 1";

                    rdlcReport = "WardRollBookPreliminary.rdlc";
                }
                else if (report == "Final")
                {
                    if (exportModel.rdbWardRollBook.Checked == true && GlobalsModel.LoggedUser.Database == CommonEnum.Database.Final)
                    {
                        if (!string.IsNullOrWhiteSpace(exportModel.txtReportTitle.Text.Trim()))
                        {
                            ReportTitle = exportModel.txtReportTitle.Text.Trim();
                        }
                        else
                        {
                            ReportTitle = $"ELECTORAL ROLL {DateTime.Now.ToString("yyyy")}";
                        }
                    }

                    //ReportTitle = "ELECTORAL ROLL 2022";
                    Version = "version 1";

                    rdlcReport = "WardRollBookFinal.rdlc";
                }
                #endregion

                Warning[] warnings;
                string[] streamIds;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;

                #region RDLC parameters
                int paramCount = 18;
                if (report == "Final")
                {
                    paramCount = 20;
                }

                //This is optional if you have parameter then you can add parameters as much as you want
                ReportParameter[] param = new ReportParameter[paramCount];
                param[0] = new ReportParameter("Region", dsExportReport.Tables[0].Rows[0]["Region"].ToString(), true);
                param[1] = new ReportParameter("Province", dsExportReport.Tables[0].Rows[0]["Province"].ToString(), true);
                param[2] = new ReportParameter("District", dsExportReport.Tables[0].Rows[0]["District"].ToString(), true);
                param[3] = new ReportParameter("LLG", dsExportReport.Tables[0].Rows[0]["LLG"].ToString(), true);
                param[4] = new ReportParameter("Ward", string.Format("({0}) - {1}", dsExportReport.Tables[0].Rows[0]["WardNo"].ToString(), dsExportReport.Tables[0].Rows[0]["Ward"].ToString()), true);
                //param[4] = new ReportParameter("Ward", dsExportReport.Tables[0].Rows[0]["Ward"].ToString(), true);

                if (TreeStructure.MainTagName == "Ward" && exportModel.chkWardRollBookLandscap.Checked)
                {

                    var locations = string.Join(", ",
                                  dsExportReport.Tables[0].AsEnumerable()
                                     .Select(row => row.Field<string>("Location"))
                                     .Where(loc => !string.IsNullOrEmpty(loc))
                                     .Distinct()
                                     .OrderBy(loc => loc));

                    param[5] = new ReportParameter("Location", locations, true);
                }
                else
                {
                    param[5] = new ReportParameter("Location", dsExportReport.Tables[0].Rows[0]["Location"].ToString(), true);
                }
                // param[5] = new ReportParameter("Location", dsExportReport.Tables[0].Rows[0]["Location"].ToString(), true);
                param[6] = new ReportParameter("Electors", dsExportReport.Tables[0].Rows.Count.ToString(), true);
                param[7] = new ReportParameter("DateTime", DateTime.Now.ToString("dddd, MMMM dd, yyyy"), true);
                param[8] = new ReportParameter("ReportTitle", ReportTitle, true);
                param[9] = new ReportParameter("Version", Version, true);
                param[10] = new ReportParameter("WardNumberWardName", dsExportReport.Tables[0].Rows[0]["WardNumberWardName"].ToString(), true);
                param[11] = new ReportParameter("IsDeletionReport", Convert.ToString(exportModel.chkIncludePossibleDeletionReport.Checked), true);
                param[12] = new ReportParameter("IsLocation", Convert.ToString(IsLocation), true);
                param[13] = new ReportParameter("IsGenderSplit", Convert.ToString(IsGenderSplit), true);
                param[14] = new ReportParameter("GenderSplit", GenderSplit, true);
                param[15] = new ReportParameter("IsAlphaSplit", Convert.ToString(IsAlphaSplit), true);
                param[16] = new ReportParameter("AlphaSplitStart", AlphaSplitStart, true);
                param[17] = new ReportParameter("AlphaSplitEnd", AlphaSplitEnd, true);

                if (report == "Final")
                {
                    param[18] = new ReportParameter("GeoNameAndYear", "LLG Election 2020 ", true);
                    param[19] = new ReportParameter("FooterDate", DateTime.Now.ToString("dd-MMM-yyyy"), true);
                }
                #endregion

                ReportDataSource dataSource = new ReportDataSource("ds_WardRollBook", dsExportReport.Tables[0].Copy());

                #region Possible Deletion Report

                //dtDeletionExportReport.TableName = "tblWardRollDeletion";
                //dsExportReport.Tables.Add(dtDeletionExportReport.Copy());
                //ReportDataSource datasourceDeletion = new ReportDataSource("ds_WardRollDeletion", dsExportReport.Tables[1].Copy());

                // Create Tables
                DataTable tblWardRollDeletion = new DataTable("tblWardRollDeletion");
                DataTable tblWardRollDeletionTrnsOutStatus = new DataTable("tblWardRollDeletionTrnsOutStatus");
                DataTable tblWardRollDeletionTrnsOutForm11 = new DataTable("tblWardRollDeletionTrnsOutForm11");
                DataTable tblWardRollBookSummaryByLocation = new DataTable("tblWardRollBookSummaryByLocation");


                tblWardRollBookSummaryByLocation.Columns.Add("Location", typeof(string));
                tblWardRollBookSummaryByLocation.Columns.Add("MaleCount", typeof(string));
                tblWardRollBookSummaryByLocation.Columns.Add("FemaleCount", typeof(string));
                tblWardRollBookSummaryByLocation.Columns.Add("OtherCount", typeof(string));
                tblWardRollBookSummaryByLocation.Columns.Add("TotalCount", typeof(string));


                #region Ward Roll Deletion
                tblWardRollDeletion.Clear();
                var wardRollDeletion = from rows in dtDeletionExportReport.AsEnumerable()
                                       where (rows.Field<string>("Status") != "TRANSFERRED OUT")
                                       select rows;

                if (wardRollDeletion != null && wardRollDeletion.Count() > 0)
                {
                    tblWardRollDeletion.Merge(wardRollDeletion.CopyToDataTable());

                    if (tblWardRollDeletion != null && tblWardRollDeletion.Rows != null && tblWardRollDeletion.Rows.Count > 0)
                    {
                        for (int i = 0; i < tblWardRollDeletion.Rows.Count; i++)
                        {
                            tblWardRollDeletion.Rows[i]["Seq"] = (i + 1);
                        }
                    }
                }
                #endregion

                #region Ward Roll Deletion Transferred Out Status
                tblWardRollDeletionTrnsOutStatus.Clear();
                var wardRollDeletionTrnsOutStatus = from rows in dtDeletionExportReport.AsEnumerable()
                                                    where (rows.Field<string>("Status") == "TRANSFERRED OUT")
                                                       && (rows.Field<int>("TransferInLocationId") == 0)
                                                    select rows;
                if (wardRollDeletionTrnsOutStatus != null && wardRollDeletionTrnsOutStatus.Count() > 0)
                {
                    tblWardRollDeletionTrnsOutStatus.Merge(wardRollDeletionTrnsOutStatus.CopyToDataTable());

                    if (tblWardRollDeletionTrnsOutStatus != null && tblWardRollDeletionTrnsOutStatus.Rows != null && tblWardRollDeletionTrnsOutStatus.Rows.Count > 0)
                    {
                        for (int i = 0; i < tblWardRollDeletionTrnsOutStatus.Rows.Count; i++)
                        {
                            tblWardRollDeletionTrnsOutStatus.Rows[i]["Seq"] = (i + 1);
                        }
                    }
                }
                #endregion

                #region Ward Roll Deletion Transferred Out Form11
                tblWardRollDeletionTrnsOutForm11.Clear();
                var wardRollDeletionTrnsOutForm11 = from rows in dtDeletionExportReport.AsEnumerable()
                                                    where (rows.Field<string>("Status") == "TRANSFERRED OUT")
                                                       && (rows.Field<int>("TransferInLocationId") > 0)
                                                    select rows;
                if (wardRollDeletionTrnsOutForm11 != null && wardRollDeletionTrnsOutForm11.Count() > 0)
                {
                    tblWardRollDeletionTrnsOutForm11.Merge(wardRollDeletionTrnsOutForm11.CopyToDataTable());

                    if (tblWardRollDeletionTrnsOutForm11 != null && tblWardRollDeletionTrnsOutForm11.Rows != null && tblWardRollDeletionTrnsOutForm11.Rows.Count > 0)
                    {
                        for (int i = 0; i < tblWardRollDeletionTrnsOutForm11.Rows.Count; i++)
                        {
                            tblWardRollDeletionTrnsOutForm11.Rows[i]["Seq"] = (i + 1);
                        }
                    }
                }
                #endregion



                tblWardRollBookSummaryByLocation.Clear();

                var result = dsExportReport.Tables[0].AsEnumerable()
                            .Where(r => !string.IsNullOrEmpty(r.Field<string>("Location")))
                            .GroupBy(r => r.Field<string>("Location"))
                            .Select(g => new
                            {
                                Location = g.Key,
                                MaleCount = g.Count(r => r.Field<string>("Gen") == "Male"),
                                FemaleCount = g.Count(r => r.Field<string>("Gen") == "Female"),
                                OtherCount = g.Count(r => r.Field<string>("Gen") == "Other"),
                                TotalCount = g.Count()
                            })
                            .OrderBy(x => x.Location)
                            .ToList();

                foreach (var item in result)
                {
                    tblWardRollBookSummaryByLocation.Rows.Add(
                        item.Location,
                        item.MaleCount,
                        item.FemaleCount,
                        item.OtherCount,
                        item.TotalCount
                    );
                }


                #endregion


                if (dtWardRollFrontCover == null)
                {
                    dtWardRollFrontCover = new DataTable("tblWardRollFrontCover");
                }
                dtWardRollFrontCover = new DataTable("tblWardRollFrontCover");


                ReportDataSource datasourceWardRollFrontCover = new ReportDataSource("ds_WardRollFrontCover", dtWardRollFrontCover.Copy());

                ReportDataSource dataSourceWardRollDeletionReport = new ReportDataSource("ds_WardRollDeletion", tblWardRollDeletion);
                ReportDataSource datasourceWardRollDeletionTrnsOutStatus = new ReportDataSource("ds_WardRollDeletionTrnsOutStatus", tblWardRollDeletionTrnsOutStatus);
                ReportDataSource datasourceWardRollDeletionTrnsOutForm11 = new ReportDataSource("ds_WardRollDeletionTrnsOutForm11", tblWardRollDeletionTrnsOutForm11);

                ReportDataSource datasourceWardRollBookSummaryByLocation = new ReportDataSource("ds_WardRollBookSummaryByLocation", tblWardRollBookSummaryByLocation.Copy());


                ReportViewer viewer = new ReportViewer();
                viewer.LocalReport.Refresh();
                viewer.LocalReport.ReportPath = "Report/" + rdlcReport; //This is your rdlc name.
                viewer.LocalReport.SetParameters(param);

                viewer.LocalReport.DataSources.Add(dataSource); // Add  datasource here
                                                                //viewer.LocalReport.DataSources.Add(datasourceDeletion); // Add Deletion datasource here
                viewer.LocalReport.DataSources.Add(datasourceWardRollBookSummaryByLocation); // Add summary by location datasource here

                viewer.LocalReport.DataSources.Add(dataSourceWardRollDeletionReport); // Add Deletion datasource here
                viewer.LocalReport.DataSources.Add(datasourceWardRollDeletionTrnsOutStatus); // Add Deletion datasource here
                viewer.LocalReport.DataSources.Add(datasourceWardRollDeletionTrnsOutForm11); // Add Deletion datasource here
                viewer.LocalReport.DataSources.Add(datasourceWardRollFrontCover); // Add Deletion datasource here

                bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
            }
            return bytes;
        }
        #endregion

        #region WardPhotoRollBook Export ReportSource
        /// <summary>
        /// Wards the roll book export report source.
        /// </summary>
        /// <param name="report">The report.</param>
        /// <param name="dt">The dt.</param>
        /// <returns></returns>
        private byte[] WardPhotoRollBookExportReportSource(string report)
        {
            dsExportReport = new DataSet();
            DataTable dtSubWardPhotoRollBook = new DataTable();

            byte[] bytes = null;

            this.dtExportReport.TableName = "tblWardPhotoRollBook";
            if (!dsExportReport.Tables.Contains(this.dtExportReport.TableName))
            {
                dsExportReport.Tables.Add(this.dtExportReport);
            }

            if (dsExportReport.Tables[0].Rows != null && dsExportReport.Tables[0].Rows.Count > 0)
            {
                #region dt Sub Ward Photo Roll Book

                dtSubWardPhotoRollBook = dsExportReport.Tables[0].Copy();

                dtSubWardPhotoRollBook.TableName = "tblSubWardPhotoRollBook";

                string[] ColumnsToBeDeleted = { "Gender",
                    "DOB",
                    "WardNumberWardName",
                    "Count",
                    "BDate",
                    "ElectorProfileImage",
                    "isPWD",
                    "SetImageUrl",
                    "Christianity",
                    //"Religion",
                    "Region",
                    "Province",
                    "District",
                    "Electors",
                    "DateTime",
                    "DuplicateId",
                    "WardNo",
                    "ProvinceCode"};

                foreach (string ColName in ColumnsToBeDeleted)
                {
                    if (dtSubWardPhotoRollBook.Columns.Contains(ColName))
                        dtSubWardPhotoRollBook.Columns.Remove(ColName);
                }

                dsExportReport.Tables.Add(dtSubWardPhotoRollBook);

                #endregion

                string ReportTitle = string.Empty, Version = string.Empty, rdlcReport = string.Empty, GenderSplit = string.Empty, AlphaSplitStart = string.Empty, AlphaSplitEnd = string.Empty;
                bool IsLocation = false, IsGenderSplit = false, IsAlphaSplit = false;

                // isLocation
                if (TreeStructure.MainTagName == "Location")
                {
                    IsLocation = true;
                }

                // isGenderSplit
                if (exportModel.rdbGenderAll.Checked == false)
                {
                    IsGenderSplit = true;

                    if (exportModel.rdbGenderMale.Checked)
                    {
                        GenderSplit = "MALE";
                    }
                    else if (exportModel.rdbGenderFemale.Checked)
                    {
                        GenderSplit = "FEMALE";
                    }
                    else if (exportModel.rdbGenderOther.Checked)
                    {
                        GenderSplit = "OTHER";
                    }
                }

                // isAlphaSplit
                if (exportModel.rdbAlphaSplit.Checked)
                {
                    IsAlphaSplit = true;
                    AlphaSplitStart = exportModel.txtAlphaSplitStart.Text.Trim().ToUpper();
                    AlphaSplitEnd = exportModel.txtAlphaSplitEnd.Text.Trim().ToUpper();
                }

                if (report == "Staging")
                {
                    if (!string.IsNullOrWhiteSpace(exportModel.txtReportTitle.Text.Trim()))
                    {
                        ReportTitle = exportModel.txtReportTitle.Text.Trim();
                    }
                    else
                    {
                        ReportTitle = $"WORKING ROLL {DateTime.Now.ToString("yyyy")}";
                    }

                    Version = "V1";

                    rdlcReport = "WardPhotoRollBook.rdlc";
                }
                else if (report == "Preliminary")
                {
                    if (!string.IsNullOrWhiteSpace(exportModel.txtReportTitle.Text.Trim()))
                    {
                        ReportTitle = exportModel.txtReportTitle.Text.Trim();
                    }
                    else
                    {
                        ReportTitle = $"PRELIMINARY ELECTORAL ROLL {DateTime.Now.ToString("yyyy")}";
                    }

                    Version = "version 1";

                    rdlcReport = "WardPhotoRollBookPreliminary.rdlc";
                }
                else if (report == "Final")
                {
                    if (exportModel.rdbWardPhotoRollBook.Checked == true && GlobalsModel.LoggedUser.Database == CommonEnum.Database.Final)
                    {
                        if (!string.IsNullOrWhiteSpace(exportModel.txtReportTitle.Text.Trim()))
                        {
                            ReportTitle = exportModel.txtReportTitle.Text.Trim();
                        }
                        else
                        {
                            ReportTitle = $"ELECTORAL ROLL {DateTime.Now.ToString("yyyy")}";
                        }
                    }

                    //ReportTitle = "ELECTORAL ROLL 2022";
                    Version = "version 1";

                    rdlcReport = "WardPhotoRollBookFinal.rdlc";
                }

                Warning[] warnings;
                string[] streamIds;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;

                int paramCount = 18;
                if (report == "Final")
                {
                    paramCount = 20;
                }

                //This is optional if you have parameter then you can add parameters as much as you want
                ReportParameter[] param = new ReportParameter[paramCount];
                param[0] = new ReportParameter("Region", dsExportReport.Tables[0].Rows[0]["Region"].ToString(), true);
                param[1] = new ReportParameter("Province", dsExportReport.Tables[0].Rows[0]["Province"].ToString(), true);
                param[2] = new ReportParameter("District", dsExportReport.Tables[0].Rows[0]["District"].ToString(), true);
                param[3] = new ReportParameter("LLG", dsExportReport.Tables[0].Rows[0]["LLG"].ToString(), true);
                param[4] = new ReportParameter("Ward", string.Format("({0}) - {1}", dsExportReport.Tables[0].Rows[0]["WardNo"].ToString(), dsExportReport.Tables[0].Rows[0]["Ward"].ToString()), true);
                param[5] = new ReportParameter("Location", dsExportReport.Tables[0].Rows[0]["Location"].ToString(), true);
                param[6] = new ReportParameter("Electors", dsExportReport.Tables[0].Rows.Count.ToString(), true);
                param[7] = new ReportParameter("DateTime", DateTime.Now.ToString("dddd, MMMM dd, yyyy"), true);
                param[8] = new ReportParameter("ReportTitle", ReportTitle, true);
                param[9] = new ReportParameter("Version", Version, true);
                param[10] = new ReportParameter("WardNumberWardName", dsExportReport.Tables[0].Rows[0]["WardNumberWardName"].ToString(), true);
                param[11] = new ReportParameter("IsDeletionReport", Convert.ToString(exportModel.chkIncludePossibleDeletionReport.Checked), true);
                param[12] = new ReportParameter("IsLocation", Convert.ToString(IsLocation), true);
                param[13] = new ReportParameter("IsGenderSplit", Convert.ToString(IsGenderSplit), true);
                param[14] = new ReportParameter("GenderSplit", GenderSplit, true);
                param[15] = new ReportParameter("IsAlphaSplit", Convert.ToString(IsAlphaSplit), true);
                param[16] = new ReportParameter("AlphaSplitStart", AlphaSplitStart, true);
                param[17] = new ReportParameter("AlphaSplitEnd", AlphaSplitEnd, true);
                if (report == "Final")
                {
                    param[18] = new ReportParameter("GeoNameAndYear", "LLG Election 2020 ", true);
                    param[19] = new ReportParameter("FooterDate", DateTime.Now.ToString("dd-MMM-yyyy"), true);
                }

                ReportDataSource dataSource = new ReportDataSource("ds_WardPhotoRollBook", dsExportReport.Tables[0].Copy());

                #region Possible Deletion Report

                //dtDeletionExportReport.TableName = "tblWardRollDeletion";
                //dsExportReport.Tables.Add(dtDeletionExportReport.Copy());
                //ReportDataSource datasourceDeletion = new ReportDataSource("ds_WardRollDeletion", dsExportReport.Tables[2].Copy());

                // Create Tables
                DataTable tblWardRollDeletion = new DataTable("tblWardRollDeletion");
                DataTable tblWardRollDeletionTrnsOutStatus = new DataTable("tblWardRollDeletionTrnsOutStatus");
                DataTable tblWardRollDeletionTrnsOutForm11 = new DataTable("tblWardRollDeletionTrnsOutForm11");

                #region Ward Roll Deletion
                tblWardRollDeletion.Clear();
                var wardRollDeletion = from rows in dtDeletionExportReport.AsEnumerable()
                                       where (rows.Field<string>("Status") != "TRANSFERRED OUT")
                                       select rows;

                if (wardRollDeletion != null && wardRollDeletion.Count() > 0)
                {
                    tblWardRollDeletion.Merge(wardRollDeletion.CopyToDataTable());

                    if (tblWardRollDeletion != null && tblWardRollDeletion.Rows != null && tblWardRollDeletion.Rows.Count > 0)
                    {
                        for (int i = 0; i < tblWardRollDeletion.Rows.Count; i++)
                        {
                            tblWardRollDeletion.Rows[i]["Seq"] = (i + 1);
                        }
                    }
                }
                #endregion

                #region Ward Roll Deletion Transferred Out Status
                tblWardRollDeletionTrnsOutStatus.Clear();
                var wardRollDeletionTrnsOutStatus = from rows in dtDeletionExportReport.AsEnumerable()
                                                    where (rows.Field<string>("Status") == "TRANSFERRED OUT")
                                                       && (rows.Field<int>("TransferInLocationId") == 0)
                                                    select rows;
                if (wardRollDeletionTrnsOutStatus != null && wardRollDeletionTrnsOutStatus.Count() > 0)
                {
                    tblWardRollDeletionTrnsOutStatus.Merge(wardRollDeletionTrnsOutStatus.CopyToDataTable());

                    if (tblWardRollDeletionTrnsOutStatus != null && tblWardRollDeletionTrnsOutStatus.Rows != null && tblWardRollDeletionTrnsOutStatus.Rows.Count > 0)
                    {
                        for (int i = 0; i < tblWardRollDeletionTrnsOutStatus.Rows.Count; i++)
                        {
                            tblWardRollDeletionTrnsOutStatus.Rows[i]["Seq"] = (i + 1);
                        }
                    }
                }
                #endregion

                #region Ward Roll Deletion Transferred Out Form11
                tblWardRollDeletionTrnsOutForm11.Clear();
                var wardRollDeletionTrnsOutForm11 = from rows in dtDeletionExportReport.AsEnumerable()
                                                    where (rows.Field<string>("Status") == "TRANSFERRED OUT")
                                                       && (rows.Field<int>("TransferInLocationId") > 0)
                                                    select rows;
                if (wardRollDeletionTrnsOutForm11 != null && wardRollDeletionTrnsOutForm11.Count() > 0)
                {
                    tblWardRollDeletionTrnsOutForm11.Merge(wardRollDeletionTrnsOutForm11.CopyToDataTable());

                    if (tblWardRollDeletionTrnsOutForm11 != null && tblWardRollDeletionTrnsOutForm11.Rows != null && tblWardRollDeletionTrnsOutForm11.Rows.Count > 0)
                    {
                        for (int i = 0; i < tblWardRollDeletionTrnsOutForm11.Rows.Count; i++)
                        {
                            tblWardRollDeletionTrnsOutForm11.Rows[i]["Seq"] = (i + 1);
                        }
                    }
                }
                #endregion

                #endregion

                if (dtWardRollFrontCover == null)
                {
                    dtWardRollFrontCover = new DataTable("tblWardRollFrontCover");
                }
                dtWardRollFrontCover = new DataTable("tblWardRollFrontCover");
                ReportDataSource datasourceWardRollFrontCover = new ReportDataSource("ds_WardRollFrontCover", dtWardRollFrontCover.Copy());

                ReportDataSource dataSourceWardRollDeletionReport = new ReportDataSource("ds_WardRollDeletion", tblWardRollDeletion);
                ReportDataSource datasourceWardRollDeletionTrnsOutStatus = new ReportDataSource("ds_WardRollDeletionTrnsOutStatus", tblWardRollDeletionTrnsOutStatus);
                ReportDataSource datasourceWardRollDeletionTrnsOutForm11 = new ReportDataSource("ds_WardRollDeletionTrnsOutForm11", tblWardRollDeletionTrnsOutForm11);

                ReportViewer viewer = new ReportViewer();
                viewer.LocalReport.Refresh();
                viewer.LocalReport.ReportPath = "Report/" + rdlcReport; //This is your rdlc name.
                viewer.LocalReport.EnableExternalImages = true;
                viewer.LocalReport.SetParameters(param);
                viewer.LocalReport.DataSources.Add(dataSource); // Add  datasource here 
                //viewer.LocalReport.DataSources.Add(datasourceDeletion); // Add Deletion datasource here
                viewer.LocalReport.DataSources.Add(dataSourceWardRollDeletionReport); // Add Deletion datasource here
                viewer.LocalReport.DataSources.Add(datasourceWardRollDeletionTrnsOutStatus); // Add Deletion datasource here
                viewer.LocalReport.DataSources.Add(datasourceWardRollDeletionTrnsOutForm11); // Add Deletion datasource here
                viewer.LocalReport.DataSources.Add(datasourceWardRollFrontCover); // Add Deletion datasource here
                viewer.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SetWardPhotoRollBookSubDataSource);
                bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
            }

            dtSubWardPhotoRollBook.Clear();

            return bytes;
        }
        #endregion

        #region Set  WardPhoto Roll Book Sub DataSource
        /// <summary>
        /// Sets the ward photo roll book sub data source.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="SubreportProcessingEventArgs"/> instance containing the event data.</param>
        private void SetWardPhotoRollBookSubDataSource(object sender, SubreportProcessingEventArgs e)
        {
            int Seq = Convert.ToInt32(e.Parameters[0].Values[0]);

            if (Seq > 0)
            {
                DataTable tblSubRCardFiltered = dsExportReport.Tables[1].AsEnumerable().Where(row => row.Field<int>("Seq") == Seq).CopyToDataTable();
                tblSubRCardFiltered.TableName = "tblSubWardPhotoRollBook";
                ReportDataSource datasource = new ReportDataSource("ds_SubWardPhotoRollBook", tblSubRCardFiltered);
                e.DataSources.Add(datasource);
            }
        }
        #endregion

        #region WardRollBook DUPLICATE Voter Export ReportSource
        /// <summary>
        /// Wards the roll book duplicate voter export report source.
        /// </summary>
        /// <returns></returns>
        private byte[] WardRollBookDuplicateVoterExportReportSource()
        {
            dsExportReport = new DataSet();
            byte[] bytes = null;

            this.dtExportReport.TableName = "tblDuplicateVoter";
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
                ReportParameter[] param = new ReportParameter[5];
                param[0] = new ReportParameter("Region", dsExportReport.Tables[0].Rows[0]["Region"].ToString(), true);
                param[1] = new ReportParameter("Province", dsExportReport.Tables[0].Rows[0]["Province"].ToString(), true);
                param[2] = new ReportParameter("District", dsExportReport.Tables[0].Rows[0]["District"].ToString(), true);
                param[3] = new ReportParameter("LLG", dsExportReport.Tables[0].Rows[0]["LLG"].ToString(), true);
                param[4] = new ReportParameter("Ward", dsExportReport.Tables[0].Rows[0]["Ward"].ToString(), true);

                ReportDataSource dataSource = new ReportDataSource("ds_DuplicateVoter", dsExportReport.Tables[0].Copy());
                ReportViewer viewer = new ReportViewer();
                viewer.LocalReport.Refresh();
                viewer.LocalReport.ReportPath = "Report/DuplicateVoter.rdlc";
                viewer.LocalReport.SetParameters(param);
                viewer.LocalReport.DataSources.Add(dataSource);
                bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
            }
            return bytes;
        }
        #endregion

        #region Voter Id Card Export Report Source
        /// <summary>
        /// Voters the identifier card export report source.
        /// </summary>
        /// <returns></returns>
        private byte[] VoterIdCardExportReportSource()
        {
            byte[] bytes = null;

            if (dsVoterCard.Tables[0].Rows != null && dsVoterCard.Tables[0].Rows.Count > 0)
            {
                Warning[] warnings;
                string[] streamIds;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;

                dsVoterCard.Tables[0].TableName = "tblVoterIdCard";
                ReportDataSource dataSource = new ReportDataSource("ds_VoterIdCard", dsVoterCard.Tables[0].Copy());
                ReportViewer viewer = new ReportViewer();
                viewer.LocalReport.Refresh();

                viewer.LocalReport.ReportPath = "Report/VoterIdCard.rdlc";
                viewer.LocalReport.DataSources.Add(dataSource);
                viewer.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SetVoterIdCardSubDataSource);
                bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
            }
            return bytes;
        }
        #endregion

        #region VoterRollEnvelopeExportReportSource
        /// <summary>
        /// Voters the roll envelope export report source.
        /// </summary>
        /// <returns></returns>
        private byte[] VoterRollEnvelopeExportReportSource(int regionId, int provinceId, int districtId, int llgId, int wardId, int locationId)
        {
            byte[] bytes = null;

            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;

            var RegionName = Globals.GeoList.RegionList.Where(x => x.RegionId == regionId).Select(y => y.RegionName).FirstOrDefault();
            var ProvinceName = Globals.GeoList.ProvinceList.Where(x => x.ProvinceId == provinceId).Select(y => y.ProvinceName).FirstOrDefault();
            var DistrictName = Globals.GeoList.DistrictList.Where(x => x.DistrictId == districtId).Select(y => y.DistrictName).FirstOrDefault();
            var LLGName = Globals.GeoList.LLGList.Where(x => x.LLGId == llgId).Select(y => y.LLGName).FirstOrDefault();
            var WardName = Globals.GeoList.WardList.Where(x => x.WardId == wardId).Select(y => y.WardName).FirstOrDefault();
            var WardNo = Globals.GeoList.WardList.Where(x => x.WardId == wardId).Select(y => y.WardNumber).FirstOrDefault();
            var LocationName = Globals.GeoList.LocationList.Where(x => x.LocationId == locationId).Select(y => y.LocationName).FirstOrDefault();
            var LocationNameList = Globals.GeoList.LocationList.Where(x => x.RegionId == regionId
                && x.ProvinceId == provinceId
                && x.DistrictId == districtId
                && x.LLGId == llgId
                && x.WardId == wardId).Select(y => y.LocationName).ToList();

            string LocationStr = string.Join(",", LocationNameList);

            //This is optional if you have parameter then you can add parameters as much as you want
            ReportParameter[] param = new ReportParameter[12];
            param[0] = new ReportParameter("Region", RegionName, true);
            param[1] = new ReportParameter("Province", ProvinceName, true);
            param[2] = new ReportParameter("District", DistrictName, true);
            param[3] = new ReportParameter("LLG", LLGName, true);
            param[4] = new ReportParameter("Ward", WardName, true);

            if (TreeStructure.MainTagName == "Ward")
            {
                param[5] = new ReportParameter("Location", LocationStr, true);
            }
            else
            {
                param[5] = new ReportParameter("Location", LocationName, true);
            }

            param[6] = new ReportParameter("WardNo", Convert.ToString(WardNo).PadLeft(2, '0'), true);



            if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Staging)
            {
                param[7] = new ReportParameter("RollDescription", "This envelope contains the Working Roll marked up from the Roll update for village / Location (s) :", true);
                param[8] = new ReportParameter("Point1", "1 - Place the updated Working Roll in this envelope and send ARO", true);
                param[9] = new ReportParameter("Point2", "2 - ARO to quality check and sign then send to RO", true);
                param[10] = new ReportParameter("Point3", "3 - RO to quality check and sign, then send to Data Coordinator.", true);
            }
            else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Preliminary)
            {
                param[7] = new ReportParameter("RollDescription", "This envelope contains the Preliminary Roll marked up from the Roll update for village / Location (s) :", true);
                param[8] = new ReportParameter("Point1", "1 - Place the updated Preliminary Roll in this envelope and send ARO", true);
                param[9] = new ReportParameter("Point2", "2 - ARO to quality check and sign then send to RO", true);
                param[10] = new ReportParameter("Point3", "3 - RO to quality check and sign, then send to Data Coordinator.", true);
            }
            else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Final)
            {
                param[7] = new ReportParameter("RollDescription", "This envelope contains the Working Roll marked up from the Roll update for village / Location (s) :", true);
                param[8] = new ReportParameter("Point1", "1 - Place the updated Working Roll in this envelope and send ARO", true);
                param[9] = new ReportParameter("Point2", "2 - ARO to quality check and sign then send to RO", true);
                param[10] = new ReportParameter("Point3", "3 - RO to quality check and sign, then send to Data Coordinator.", true);
            }

            param[11] = new ReportParameter("ReportTitle", $"PNGEC - ELECTORAL ROLL UPDATE {DateTime.Now.ToString("yyyy")}", true);

            ReportViewer viewer = new ReportViewer();
            viewer.LocalReport.Refresh();

            viewer.LocalReport.ReportPath = "Report/WardRollEnvelope.rdlc";
            viewer.LocalReport.SetParameters(param);
            bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

            return bytes;
        }
        #endregion

        #region WardRoll Deletion Export ReportSource
        /// <summary>
        /// Wards the roll deletion export report.
        /// </summary>
        /// <returns></returns>
        private byte[] WardRollDeletionExportReport()
        {
            byte[] bytes = null;

            if (this.dtExportReport != null && this.dtExportReport.Rows.Count > 0)
            {
                dsExportReport = new DataSet();

                string rdlcReport = string.Empty;

                if (TreeStructure.MainTagName == "Location")
                {
                    rdlcReport = "WardRollDeletionReportLocation.rdlc";
                }
                else
                {
                    rdlcReport = "WardRollDeletionReport.rdlc";
                }

                // Create Tables
                DataTable tblWardRollDeletion = new DataTable("tblWardRollDeletion");
                DataTable tblWardRollDeletionTrnsOutStatus = new DataTable("tblWardRollDeletionTrnsOutStatus");
                DataTable tblWardRollDeletionTrnsOutForm11 = new DataTable("tblWardRollDeletionTrnsOutForm11");


                #region Ward Roll Deletion
                tblWardRollDeletion.Clear();
                var wardRollDeletion = from rows in dtExportReport.AsEnumerable()
                                       where (rows.Field<string>("Status") != "TRANSFERRED OUT")
                                       select rows;

                if (wardRollDeletion != null && wardRollDeletion.Count() > 0)
                {
                    tblWardRollDeletion.Merge(wardRollDeletion.CopyToDataTable());

                    if (tblWardRollDeletion != null && tblWardRollDeletion.Rows != null && tblWardRollDeletion.Rows.Count > 0)
                    {
                        for (int i = 0; i < tblWardRollDeletion.Rows.Count; i++)
                        {
                            tblWardRollDeletion.Rows[i]["Seq"] = (i + 1);
                        }
                    }
                }
                #endregion

                #region Ward Roll Deletion Transferred Out Status
                tblWardRollDeletionTrnsOutStatus.Clear();
                var wardRollDeletionTrnsOutStatus = from rows in dtExportReport.AsEnumerable()
                                                    where (rows.Field<string>("Status") == "TRANSFERRED OUT")
                                                       && (rows.Field<int>("TransferInLocationId") == 0)
                                                    select rows;
                if (wardRollDeletionTrnsOutStatus != null && wardRollDeletionTrnsOutStatus.Count() > 0)
                {
                    tblWardRollDeletionTrnsOutStatus.Merge(wardRollDeletionTrnsOutStatus.CopyToDataTable());

                    if (tblWardRollDeletionTrnsOutStatus != null && tblWardRollDeletionTrnsOutStatus.Rows != null && tblWardRollDeletionTrnsOutStatus.Rows.Count > 0)
                    {
                        for (int i = 0; i < tblWardRollDeletionTrnsOutStatus.Rows.Count; i++)
                        {
                            tblWardRollDeletionTrnsOutStatus.Rows[i]["Seq"] = (i + 1);
                        }
                    }
                }
                #endregion

                #region Ward Roll Deletion Transferred Out Form11
                tblWardRollDeletionTrnsOutForm11.Clear();
                var wardRollDeletionTrnsOutForm11 = from rows in dtExportReport.AsEnumerable()
                                                    where (rows.Field<string>("Status") == "TRANSFERRED OUT")
                                                       && (rows.Field<int>("TransferInLocationId") > 0)
                                                    select rows;
                if (wardRollDeletionTrnsOutForm11 != null && wardRollDeletionTrnsOutForm11.Count() > 0)
                {
                    tblWardRollDeletionTrnsOutForm11.Merge(wardRollDeletionTrnsOutForm11.CopyToDataTable());

                    if (tblWardRollDeletionTrnsOutForm11 != null && tblWardRollDeletionTrnsOutForm11.Rows != null && tblWardRollDeletionTrnsOutForm11.Rows.Count > 0)
                    {
                        for (int i = 0; i < tblWardRollDeletionTrnsOutForm11.Rows.Count; i++)
                        {
                            tblWardRollDeletionTrnsOutForm11.Rows[i]["Seq"] = (i + 1);
                        }
                    }
                }
                #endregion

                dsExportReport.Tables.AddRange(new DataTable[] { tblWardRollDeletion, tblWardRollDeletionTrnsOutStatus, tblWardRollDeletionTrnsOutForm11 });

                if (dsExportReport.Tables != null && dsExportReport.Tables.Count > 0)
                {
                    Warning[] warnings;
                    string[] streamIds;
                    string mimeType = string.Empty;
                    string encoding = string.Empty;
                    string extension = string.Empty;

                    int paramCount = 5;

                    //This is optional if you have parameter then you can add parameters as much as you want
                    ReportParameter[] param = new ReportParameter[paramCount];
                    param[0] = new ReportParameter("Region", this.dtExportReport.Rows[0]["Region"].ToString(), true);
                    param[1] = new ReportParameter("Province", this.dtExportReport.Rows[0]["Province"].ToString(), true);
                    param[2] = new ReportParameter("District", this.dtExportReport.Rows[0]["District"].ToString(), true);
                    param[3] = new ReportParameter("Ward", this.dtExportReport.Rows[0]["Ward"].ToString(), true);
                    param[4] = new ReportParameter("Location", this.dtExportReport.Rows[0]["Location"].ToString(), true);

                    ReportDataSource dataSourceWardRollDeletionReport = new ReportDataSource("ds_WardRollDeletionReport", dsExportReport.Tables[0].Copy());
                    ReportDataSource datasourceWardRollDeletionTrnsOutStatus = new ReportDataSource("ds_WardRollDeletionTrnsOutStatus", dsExportReport.Tables[1].Copy());
                    ReportDataSource datasourceWardRollDeletionTrnsOutForm11 = new ReportDataSource("ds_WardRollDeletionTrnsOutForm11", dsExportReport.Tables[2].Copy());

                    //ReportDataSource dataSource = new ReportDataSource("ds_WardRollDeletionReport", dsExportReport.Tables[0].Copy());
                    ReportViewer viewer = new ReportViewer();
                    viewer.LocalReport.Refresh();
                    viewer.LocalReport.ReportPath = "Report/" + rdlcReport; //This is your rdlc name.
                    viewer.LocalReport.SetParameters(param);

                    viewer.LocalReport.DataSources.Add(dataSourceWardRollDeletionReport); // Add Deletion datasource here
                    viewer.LocalReport.DataSources.Add(datasourceWardRollDeletionTrnsOutStatus); // Add Deletion datasource here
                    viewer.LocalReport.DataSources.Add(datasourceWardRollDeletionTrnsOutForm11); // Add Deletion datasource here
                    //viewer.LocalReport.DataSources.Add(dataSource); // Add  datasource here         
                    bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
                }

                tblWardRollDeletion.Clear();
                tblWardRollDeletionTrnsOutStatus.Clear();
                tblWardRollDeletionTrnsOutForm11.Clear();
            }

            return bytes;
        }

        //private byte[] WardRollDeletionExportReport()
        //{
        //    dsExportReport = new DataSet();
        //    byte[] bytes = null;

        //    this.dtExportReport.TableName = "tblWardRollDeletion";
        //    if (!dsExportReport.Tables.Contains(this.dtExportReport.TableName))
        //    {
        //        dsExportReport.Tables.Add(this.dtExportReport);
        //    }

        //    if (dsExportReport.Tables[0].Rows != null && dsExportReport.Tables[0].Rows.Count > 0)
        //    {
        //        string rdlcReport = string.Empty;

        //        if (TreeStructure.MainTagName == "Location")
        //        {
        //            rdlcReport = "WardRollDeletionReportLocation.rdlc";
        //        }
        //        else
        //        {
        //            rdlcReport = "WardRollDeletionReport.rdlc";
        //        }

        //        Warning[] warnings;
        //        string[] streamIds;
        //        string mimeType = string.Empty;
        //        string encoding = string.Empty;
        //        string extension = string.Empty;

        //        int paramCount = 5;

        //        //This is optional if you have parameter then you can add parameters as much as you want
        //        ReportParameter[] param = new ReportParameter[paramCount];
        //        param[0] = new ReportParameter("Region", dsExportReport.Tables[0].Rows[0]["Region"].ToString(), true);
        //        param[1] = new ReportParameter("Province", dsExportReport.Tables[0].Rows[0]["Province"].ToString(), true);
        //        param[2] = new ReportParameter("District", dsExportReport.Tables[0].Rows[0]["District"].ToString(), true);
        //        param[3] = new ReportParameter("Ward", dsExportReport.Tables[0].Rows[0]["Ward"].ToString(), true);
        //        param[4] = new ReportParameter("Location", dsExportReport.Tables[0].Rows[0]["Location"].ToString(), true);

        //        ReportDataSource dataSource = new ReportDataSource("ds_WardRollDeletionReport", dsExportReport.Tables[0].Copy());
        //        ReportViewer viewer = new ReportViewer();
        //        viewer.LocalReport.Refresh();
        //        viewer.LocalReport.ReportPath = "Report/" + rdlcReport; //This is your rdlc name.
        //        viewer.LocalReport.SetParameters(param);
        //        viewer.LocalReport.DataSources.Add(dataSource); // Add  datasource here         
        //        bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
        //    }
        //    return bytes;
        //}
        #endregion

        #region WardRoll Pre Field Work Summary
        /// <summary>
        /// Wards the roll pre field work summary.
        /// </summary>
        /// <returns></returns>
        private byte[] WardRollPreFieldWorkSummary()
        {
            dsExportReport = new DataSet();
            byte[] bytes = null;

            string Region = dtExportReport.Rows[0]["Region"].ToString();
            string Province = dtExportReport.Rows[0]["Province"].ToString();
            string District = dtExportReport.Rows[0]["District"].ToString();
            string LLG = dtExportReport.Rows[0]["LLG"].ToString();
            string Ward = dtExportReport.Rows[0]["Ward"].ToString();
            string TotalVoters = Convert.ToString(dtExportReport.Rows.Count);
            string WardNo = dtExportReport.Rows[0]["WardNo"].ToString();

            string rdlcReport = string.Empty;

            #region dataTable column grouping and count
            // grouping and count
            var query = (from row in dtExportReport.AsEnumerable()
                         group row by row.Field<string>("Location") into voters
                         orderby voters.Key
                         select new
                         {
                             Location = voters.Key,
                             Count = voters.Count(),
                         }).AsEnumerable();

            dtExportReport = new DataTable("tblPreFieldWorkSummary");
            dtExportReport.Columns.Add(
                new DataColumn()
                {
                    //DataType = System.Type.GetType("System.String"),
                    ColumnName = "Location"
                }
            );

            dtExportReport.Columns.Add(
                new DataColumn()
                {
                    DataType = System.Type.GetType("System.Int32"),
                    ColumnName = "Count"
                }
            );

            foreach (var element in query)
            {
                var row = dtExportReport.NewRow();
                row["Location"] = element.Location;
                row["Count"] = element.Count;
                dtExportReport.Rows.Add(row);
            }

            DataRow dtrow = dtExportReport.NewRow();
            dtrow["Location"] = "Total";
            dtrow["Count"] = dtExportReport.Compute("Sum(Count)", "Count > 0");
            dtExportReport.Rows.Add(dtrow);

            dtExportReport.TableName = "tblPreFieldWorkSummary";
            if (!dsExportReport.Tables.Contains(this.dtExportReport.TableName))
            {
                dsExportReport.Tables.Add(this.dtExportReport);
            }
            #endregion

            if (TreeStructure.MainTagName == "Location")
            {
                rdlcReport = "PreFieldWorkSummary.rdlc";
            }
            else
            {
                rdlcReport = "PreFieldWorkSummary.rdlc";
            }

            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;

            int paramCount = 7;

            //This is optional if you have parameter then you can add parameters as much as you want
            ReportParameter[] param = new ReportParameter[paramCount];
            param[0] = new ReportParameter("Region", Region, true);
            param[1] = new ReportParameter("Province", Province, true);
            param[2] = new ReportParameter("District", District, true);
            param[3] = new ReportParameter("LLG", LLG, true);
            param[4] = new ReportParameter("Ward", Ward, true);
            param[5] = new ReportParameter("TotalVoters", TotalVoters, true);
            param[6] = new ReportParameter("WardNo", WardNo, true);

            ReportDataSource dataSource = new ReportDataSource("ds_PreFieldWorkSummary", dsExportReport.Tables[0].Copy());
            ReportViewer viewer = new ReportViewer();
            viewer.LocalReport.Refresh();
            viewer.LocalReport.ReportPath = "Report/" + rdlcReport; //This is your rdlc name.
            viewer.LocalReport.SetParameters(param);
            viewer.LocalReport.DataSources.Add(dataSource); // Add  datasource here         
            bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
            return bytes;
        }
        #endregion

        #region Ward Exisitng Voter Details
        /// <summary>
        /// Wards the exisitng voter details.
        /// </summary>
        /// <returns></returns>
        private byte[] WardExisitngVoterDetails()
        {
            byte[] bytes = null;

            if (dsExportReport.Tables[0].Rows != null && dsExportReport.Tables[0].Rows.Count > 0)
            {
                string rdlcReport = string.Empty;

                if (TreeStructure.MainTagName == "Location")
                {
                    rdlcReport = "ExisitngVoterDetails.rdlc";
                }
                else
                {
                    rdlcReport = "ExisitngVoterDetails.rdlc";
                }

                Warning[] warnings;
                string[] streamIds;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;

                int paramCount = 4;

                //This is optional if you have parameter then you can add parameters as much as you want
                ReportParameter[] param = new ReportParameter[paramCount];
                param[0] = new ReportParameter("Province", dsExportReport.Tables[0].Rows[0]["Province"].ToString(), true);
                param[1] = new ReportParameter("District", dsExportReport.Tables[0].Rows[0]["District"].ToString(), true);
                param[2] = new ReportParameter("LLG", dsExportReport.Tables[0].Rows[0]["LLG"].ToString(), true);
                param[3] = new ReportParameter("Ward", dsExportReport.Tables[0].Rows[0]["Ward"].ToString(), true);

                dsExportReport.Tables[0].TableName = "tblExisitngVoterDetails";
                ReportDataSource dataSource = new ReportDataSource("ds_ExisitngVoterDetails", dsExportReport.Tables[0].Copy());
                ReportViewer viewer = new ReportViewer();
                viewer.LocalReport.Refresh();
                viewer.LocalReport.ReportPath = "Report/" + rdlcReport; ;
                viewer.LocalReport.SetParameters(param);
                viewer.LocalReport.DataSources.Add(dataSource);
                viewer.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SetWardExisitngVoterDetailsSubDataSource);
                bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
            }
            return bytes;
        }
        #endregion

        #region Set Voter Id Card Sub DataSource
        /// <summary>
        /// Sets the voter identifier card sub data source.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="SubreportProcessingEventArgs"/> instance containing the event data.</param>
        private void SetVoterIdCardSubDataSource(object sender, SubreportProcessingEventArgs e)
        {
            int Seq = Convert.ToInt32(e.Parameters[0].Values[0]);

            if (Seq > 0)
            {
                DataTable tblSubRCardFiltered = dsVoterCard.Tables[1].AsEnumerable().Where(row => row.Field<int>("Seq") == Seq).CopyToDataTable();
                tblSubRCardFiltered.TableName = "tblSubVoterIdCard";
                ReportDataSource datasource = new ReportDataSource("ds_SubVoterIdCard", tblSubRCardFiltered);
                e.DataSources.Add(datasource);
            }
        }
        #endregion

        #region Voter Photo Card Export Report Source
        /// <summary>
        /// Voters the photo card export report source.
        /// </summary>
        /// <returns></returns>
        private byte[] VoterPhotoCardExportReportSource()
        {
            byte[] bytes = null;

            if (dsVoterCard.Tables[0].Rows != null && dsVoterCard.Tables[0].Rows.Count > 0)
            {
                Warning[] warnings;
                string[] streamIds;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;
                dsVoterCard.Tables[0].TableName = "tblVoterPhotoCard";
                ReportDataSource dataSource = new ReportDataSource("ds_VoterPhotoCard", dsVoterCard.Tables[0].Copy());
                ReportViewer viewer = new ReportViewer();
                viewer.LocalReport.Refresh();
                viewer.LocalReport.ReportPath = "Report/VoterPhotoCard.rdlc";
                viewer.LocalReport.EnableExternalImages = true;
                viewer.LocalReport.DataSources.Add(dataSource);
                viewer.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SetVoterPhotoCardSubDataSource);
                bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
            }
            return bytes;
        }
        #endregion

        #region Set Voter Photo Card Sub DataSource
        /// <summary>
        /// Sets the voter photo card sub data source.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="SubreportProcessingEventArgs"/> instance containing the event data.</param>
        private void SetVoterPhotoCardSubDataSource(object sender, SubreportProcessingEventArgs e)
        {
            int Seq = Convert.ToInt32(e.Parameters[0].Values[0]);

            if (Seq > 0)
            {
                DataTable tblSubRCardFiltered = dsVoterCard.Tables[1].AsEnumerable().Where(row => row.Field<int>("Seq") == Seq).CopyToDataTable();
                tblSubRCardFiltered.TableName = "tblSubVoterPhotoCard";
                ReportDataSource datasource = new ReportDataSource("ds_SubVoterPhotoCard", tblSubRCardFiltered);
                e.DataSources.Add(datasource);
            }
        }

        #endregion

        #region SetWardExisitngVoterDetailsSubDataSource
        /// <summary>
        /// Sets the ward exisitng voter details sub data source.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="SubreportProcessingEventArgs"/> instance containing the event data.</param>
        private void SetWardExisitngVoterDetailsSubDataSource(object sender, SubreportProcessingEventArgs e)
        {
            int Seq = Convert.ToInt32(e.Parameters[0].Values[0]);

            if (Seq > 0)
            {
                DataTable tblSubRCardFiltered = dsExportReport.Tables[1].AsEnumerable().Where(row => row.Field<int>("Seq") == Seq).CopyToDataTable();
                tblSubRCardFiltered.TableName = "tblSubExisitngVoterDetails";
                ReportDataSource datasource = new ReportDataSource("ds_ExisitngVoterDetails", tblSubRCardFiltered);
                e.DataSources.Add(datasource);
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

            //pageList.KeyWord = txtSearch.Text;

            // Filter Filed
            pageList.FiledName = Convert.ToString(cmbFiled.SelectedValue);
            if (exportModel.rdbWardRollDeletion.Checked)
            {
                pageList.FiledValue = "DELETED";
            }
            else
            {
                pageList.FiledValue = Convert.ToString(cmbStatus.SelectedValue);
            }

            if (exportModel.rdbVoterEnvelope.Checked != true)
            {
                // Gender
                if (exportModel.rdbGenderAll.Checked == true)
                {
                    pageList.Gender = "All";
                }
                else if (exportModel.rdbGenderMale.Checked == true)
                {
                    pageList.Gender = "M";
                }
                else if (exportModel.rdbGenderFemale.Checked == true)
                {
                    pageList.Gender = "F";
                }
                else if (exportModel.rdbGenderOther.Checked == true)
                {
                    pageList.Gender = "O";
                }

                // Disability
                if (exportModel.rdbDisabilityAll.Checked == true)
                {
                    pageList.Disability = "All";
                }
                else if (exportModel.rdbDisabilityWith.Checked == true)
                {
                    pageList.Disability = "With";
                }
            }
            else
            {
                pageList.Gender = "All";
                pageList.Disability = "All";
            }

            if (exportModel.rdbVoterPhotoCard.Checked == true)
            {
                // Photo
                if (exportModel.rdbPhotoAll.Checked == true)
                {
                    pageList.Photo = "All";
                }
                else if (exportModel.rdbPhotoWith.Checked == true)
                {
                    pageList.Photo = "WithPhoto";
                }
                else if (exportModel.rdbPhotoWithout.Checked == true)
                {
                    pageList.Photo = "WithoutPhoto";
                }

                // Date Range
                if (exportModel.rdbDateRange.Checked == true)
                {
                    pageList.DateRange = "Range";
                    TimeSpan FromTime = new TimeSpan(00, 00, 00);
                    var FromDate = exportModel.dtpFromDate.Value.Date + FromTime;
                    pageList.DateFrom = FromDate.ToUniversalTime();

                    TimeSpan ToTime = new TimeSpan(23, 59, 59);
                    var ToDate = exportModel.dtpToDate.Value.Date + ToTime;
                    pageList.DateTo = ToDate.ToUniversalTime();

                }
                else if (exportModel.rdbDateAll.Checked == true)
                {
                    pageList.DateRange = "All";
                    pageList.DateFrom = null;
                    pageList.DateTo = null;
                }

                // Elector By IDs
                if (exportModel.rdbElectorByIDsSelected.Checked == true)
                {
                    pageList.ElectorByIDs = string.Join(",", ElectorIDs);
                }
                else if (exportModel.rdbElectorByIDsAll.Checked == true)
                {
                    pageList.ElectorByIDs = "All";
                }
            }
            else if (exportModel.rdbExisitngVoterDetails.Checked == true)
            {
                pageList.Photo = "All";
                pageList.DateRange = "All";
                pageList.DateFrom = null;
                pageList.DateTo = null;
                // Elector By IDs
                if (exportModel.rdbElectorByIDsSelected.Checked == true)
                {
                    pageList.ElectorByIDs = string.Join(",", ElectorIDs);
                }
                else if (exportModel.rdbElectorByIDsAll.Checked == true)
                {
                    pageList.ElectorByIDs = "All";
                }
            }
            else
            {
                pageList.Photo = "All";
                pageList.DateRange = "All";
                pageList.DateFrom = null;
                pageList.DateTo = null;
                pageList.ElectorByIDs = "All";
            }

            if (exportModel.rdbAlphaSplit.Checked == true && exportModel.rdbWardRollBook.Checked == true)
            {
                pageList.AlphaSplit = "AlphaSplit";
                pageList.AlphaSplitStart = exportModel.txtAlphaSplitStart.Text.Trim().ToLower();
                pageList.AlphaSplitEnd = exportModel.txtAlphaSplitEnd.Text.Trim().ToLower();
            }
            else
            {
                pageList.AlphaSplit = "All";
                pageList.AlphaSplitStart = string.Empty;
                pageList.AlphaSplitEnd = string.Empty;
            }

            pageList.FName = txtFName.Text.Trim();
            pageList.LName = txtLName.Text.Trim();

            // TreeView Filed
            pageList.GeoName = TreeStructure.MainTagName;
            pageList.GeoId = TreeStructure.Id;

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
                Response<WardRollBookReortModel> response = await GenericHttpClient.PostRequest<WardRollBookReortModel>("File/GetExportRecordsList", pageList);
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
                lstWardRoll.ForEach(x => x.MKAClanName = x.ClanName);
                dtExportReport = ToDataTable(lstWardRoll);
            }

            lstWardRoll.Clear();


        }

        #endregion

        #region Get Data Migration Report Data
        /// <summary>
        /// Gets the data migration report data.
        /// </summary>
        private async Task GetDataMigrationReportData()
        {
            #region pageList
            pageList = new PageList();

            // Page Name
            pageList.PageName = pageName;

            // Current Database Filed 
            pageList.DatabaseName = GlobalsModel.LoggedUser.Database.ToString();

            pageList.SortField = "ElectorId";
            pageList.SortOrder = "DESC";

            // Filter Filed
            pageList.FiledName = Convert.ToString(cmbFiled.SelectedValue);
            pageList.FiledValue = Convert.ToString(cmbStatus.SelectedValue);

            // TreeView Filed
            pageList.GeoName = TreeStructure.MainTagName;
            pageList.GeoId = TreeStructure.Id;

            // Filter User Details
            pageList.FName = txtFName.Text.Trim();
            pageList.LName = txtLName.Text.Trim();

            string gender = "All";

            if (cmbGender.Text.Trim() == "MALE")
            {
                gender = "M";
            }
            else if (cmbGender.Text.Trim() == "FEMALE")
            {
                gender = "F";
            }
            else if (cmbGender.Text.Trim() == "OTHER")
            {
                gender = "O";
            }

            pageList.Gender = gender;

            // Login User
            pageList.LoggedUserId = GlobalsModel.LoggedUser.User.UserId;

            // Page No
            int PageNo = 1;

            // Page Size
            pageList.PageSize = 1000;
            #endregion

            //lstWardRoll = new List<WardRollBookReortModel>();
            var dataMigrationList = new List<VoterModel>();
            bool DataFound = false;

            do
            {
                pageList.PageNo = PageNo;
                Response<VoterModel> response = await GenericHttpClient.PostRequest<VoterModel>("File/GetVoterList", pageList);
                if (response.Status && response.Data.ListItems.ToList() != null && response.Data.ListItems.ToList().Count > 0)
                {
                    dataMigrationList.AddRange(response.Data.ListItems.ToList());
                    DataFound = true;
                }
                else
                {
                    DataFound = false;
                }

                PageNo++;

            } while (DataFound);

            if (dataMigrationList != null && dataMigrationList.Count > 0)
            {
                dtExportReport = new DataTable();
                dtExportReport = ToDataTable(dataMigrationList);
            }

            dataMigrationList.Clear();
            dataMigrationList = null;
        }


        /// <summary>
        /// Datas the migration export report.
        /// </summary>
        /// <returns></returns>
        private byte[] DataMigrationExportReport()
        {
            byte[] bytest = null;

            if (dtExportReport != null && dtExportReport.Rows.Count > 0)
            {
                if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Staging)
                {
                    bytest = DataMigrationExportReportSource("Staging");
                }
                else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Preliminary)
                {
                    bytest = DataMigrationExportReportSource("Preliminary");
                }
                else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Final)
                {
                    bytest = DataMigrationExportReportSource("Final");
                }
            }

            return bytest;
        }

        /// <summary>
        /// Datas the migration export report source.
        /// </summary>
        /// <param name="report">The report.</param>
        /// <returns></returns>
        private byte[] DataMigrationExportReportSource(string report)
        {
            dsExportReport = new DataSet();
            byte[] bytes = null;

            this.dtExportReport.TableName = "tblDataMigration";
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

                #region RDLC parameters
                int paramCount = 2;


                //This is optional if you have parameter then you can add parameters as much as you want
                ReportParameter[] param = new ReportParameter[paramCount];
                param[0] = new ReportParameter("Status", Convert.ToString(cmbStatus.SelectedValue), true);

                //if (report == "Staging")
                //{
                //    param[1] = new ReportParameter("SubTitle", "From Working Roll to Preliminary Roll", true);
                //}
                //else if (report == "Preliminary")
                //{
                //    param[1] = new ReportParameter("SubTitle", "From Preliminary Roll to Final Roll", true);
                //}
                //else if (report == "Final")
                //{
                //    param[1] = new ReportParameter("SubTitle", "From Preliminary Roll to Final Roll", true);
                //}

                if (Convert.ToInt16(GlobalsModel.LoggedUser.Server) == 1)//Development
                {
                    param[1] = new ReportParameter("SubTitle", string.Format(" / DEVELOPMENT / {0}", GlobalsModel.LoggedUser.Database));
                }
                else if (Convert.ToInt16(GlobalsModel.LoggedUser.Server) == 2)//Training
                {
                    param[1] = new ReportParameter("SubTitle", string.Format(" / TRAINING / {0}", GlobalsModel.LoggedUser.Database));
                }
                else if (Convert.ToInt16(GlobalsModel.LoggedUser.Server) == 3)//Production
                {
                    param[1] = new ReportParameter("SubTitle", string.Format(" / PRODUCTION / {0}", GlobalsModel.LoggedUser.Database));
                }

                #endregion

                ReportDataSource dataSource = new ReportDataSource("ds_DataMigration", dsExportReport.Tables[0].Copy());

                ReportViewer viewer = new ReportViewer();
                viewer.LocalReport.Refresh();
                viewer.LocalReport.ReportPath = "Report/DataMigrationReport.rdlc"; //This is your rdlc name.
                viewer.LocalReport.SetParameters(param);

                viewer.LocalReport.DataSources.Add(dataSource); // Add  datasource here

                bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
            }
            return bytes;
        }
        #endregion

        #region Get Deletion Export Records List
        /// <summary>
        /// Gets the deletion export records data.
        /// </summary>
        private async Task GetDeletionExportRecordsData()
        {
            #region pageList
            PageList pageList = new PageList();

            // Current Database Filed 
            pageList.DatabaseName = GlobalsModel.LoggedUser.Database.ToString();

            // Filter Filed
            pageList.FiledName = Convert.ToString(cmbFiled.SelectedValue);
            pageList.FiledValue = "DELETED";

            if (exportModel.rdbVoterEnvelope.Checked != true)
            {
                // Gender
                if (exportModel.rdbGenderAll.Checked == true)
                {
                    pageList.Gender = "All";
                }
                else if (exportModel.rdbGenderMale.Checked == true)
                {
                    pageList.Gender = "M";
                }
                else if (exportModel.rdbGenderFemale.Checked == true)
                {
                    pageList.Gender = "F";
                }
                else if (exportModel.rdbGenderOther.Checked == true)
                {
                    pageList.Gender = "O";
                }

                // Disability
                if (exportModel.rdbDisabilityAll.Checked == true)
                {
                    pageList.Disability = "All";
                }
                else if (exportModel.rdbDisabilityWith.Checked == true)
                {
                    pageList.Disability = "With";
                }
            }
            else
            {
                pageList.Gender = "All";
                pageList.Disability = "All";
            }

            if (exportModel.rdbVoterPhotoCard.Checked == true)
            {
                // Photo
                if (exportModel.rdbPhotoAll.Checked == true)
                {
                    pageList.Photo = "All";
                }
                else if (exportModel.rdbPhotoWith.Checked == true)
                {
                    pageList.Photo = "WithPhoto";
                }
                else if (exportModel.rdbPhotoWithout.Checked == true)
                {
                    pageList.Photo = "WithoutPhoto";
                }

                // Date Range
                if (exportModel.rdbDateRange.Checked == true)
                {
                    pageList.DateRange = "Range";
                    TimeSpan FromTime = new TimeSpan(00, 00, 00);
                    var FromDate = exportModel.dtpFromDate.Value.Date + FromTime;
                    pageList.DateFrom = FromDate.ToUniversalTime();

                    TimeSpan ToTime = new TimeSpan(23, 59, 59);
                    var ToDate = exportModel.dtpToDate.Value.Date + ToTime;
                    pageList.DateTo = ToDate.ToUniversalTime();

                }
                else if (exportModel.rdbDateAll.Checked == true)
                {
                    pageList.DateRange = "All";
                    pageList.DateFrom = null;
                    pageList.DateTo = null;
                }

                // Elector By IDs
                if (exportModel.rdbElectorByIDsSelected.Checked == true)
                {
                    pageList.ElectorByIDs = string.Join(",", ElectorIDs);
                }
                else if (exportModel.rdbElectorByIDsAll.Checked == true)
                {
                    pageList.ElectorByIDs = "All";
                }
            }
            else if (exportModel.rdbExisitngVoterDetails.Checked == true)
            {
                pageList.Photo = "All";
                pageList.DateRange = "All";
                pageList.DateFrom = null;
                pageList.DateTo = null;
                // Elector By IDs
                if (exportModel.rdbElectorByIDsSelected.Checked == true)
                {
                    pageList.ElectorByIDs = string.Join(",", ElectorIDs);
                }
                else if (exportModel.rdbElectorByIDsAll.Checked == true)
                {
                    pageList.ElectorByIDs = "All";
                }
            }
            else
            {
                pageList.Photo = "All";
                pageList.DateRange = "All";
                pageList.DateFrom = null;
                pageList.DateTo = null;
                pageList.ElectorByIDs = "All";
            }

            pageList.FName = txtFName.Text.Trim();
            pageList.LName = txtLName.Text.Trim();

            // TreeView Filed
            pageList.GeoName = TreeStructure.MainTagName;
            pageList.GeoId = TreeStructure.Id;

            // Login User
            pageList.LoggedUserId = GlobalsModel.LoggedUser.User.UserId;

            // Page No
            int PageNo = 1;

            // Page Size
            pageList.PageSize = 1000;
            #endregion

            lstDeletionWardRoll = new List<WardRollBookReortModel>();

            bool DataFound = false;

            do
            {
                pageList.PageNo = PageNo;
                Response<WardRollBookReortModel> response = await GenericHttpClient.PostRequest<WardRollBookReortModel>("File/GetExportRecordsList", pageList);
                if (response.Status && response.Data.ListItems.ToList() != null && response.Data.ListItems.ToList().Count > 0)
                {
                    lstDeletionWardRoll.AddRange(response.Data.ListItems.ToList());
                    DataFound = true;
                }
                else
                {
                    DataFound = false;
                }

                PageNo++;

            } while (DataFound);

            if (dtDeletionExportReport == null)
            {
                dtDeletionExportReport = new DataTable();
            }
            else
            {
                dtDeletionExportReport.Clear();
            }

            if (lstDeletionWardRoll != null && lstDeletionWardRoll.Count > 0)
            {
                dtDeletionExportReport = ToDataTable(lstDeletionWardRoll);
            }

            lstDeletionWardRoll.Clear();
        }
        #endregion

        #region Get Export Csv Records Data
        /// <summary>
        /// Gets the export CSV records data.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private async Task GetExportCsvRecordsData()
        {
            #region pageList
            PageList pageList = new PageList();

            // Current Database Filed 
            pageList.DatabaseName = GlobalsModel.LoggedUser.Database.ToString();

            //pageList.KeyWord = txtSearch.Text;

            // Filter Filed
            pageList.FiledName = Convert.ToString(cmbFiled.SelectedValue);
            pageList.FiledValue = Convert.ToString(cmbStatus.SelectedValue);

            if (exportModel.rdbWardRollBook.Checked == true)
            {
                // Gender
                if (exportModel.rdbGenderAll.Checked == true)
                {
                    pageList.Gender = "All";
                }
                else if (exportModel.rdbGenderMale.Checked == true)
                {
                    pageList.Gender = "M";
                }
                else if (exportModel.rdbGenderFemale.Checked == true)
                {
                    pageList.Gender = "F";
                }
                else if (exportModel.rdbGenderOther.Checked == true)
                {
                    pageList.Gender = "O";
                }

                // Disability
                if (exportModel.rdbDisabilityAll.Checked == true)
                {
                    pageList.Disability = "All";
                }
                else if (exportModel.rdbDisabilityWith.Checked == true)
                {
                    pageList.Disability = "With";
                }
            }
            else
            {
                pageList.Gender = "All";
                pageList.Disability = "All";
            }

            if (exportModel.rdbVoterPhotoCard.Checked == true)
            {
                // Photo
                if (exportModel.rdbPhotoAll.Checked == true)
                {
                    pageList.Photo = "All";
                }
                else if (exportModel.rdbPhotoWith.Checked == true)
                {
                    pageList.Photo = "WithPhoto";
                }
                else if (exportModel.rdbPhotoWithout.Checked == true)
                {
                    pageList.Photo = "WithoutPhoto";
                }
            }
            else
            {
                pageList.Photo = "All";
            }

            pageList.FName = txtFName.Text.Trim();
            pageList.LName = txtLName.Text.Trim();

            // TreeView Filed
            pageList.GeoName = TreeStructure.MainTagName;
            pageList.GeoId = TreeStructure.Id;

            // Login User
            pageList.LoggedUserId = GlobalsModel.LoggedUser.User.UserId;

            // Page No
            int PageNo = 1;

            // Page Size
            pageList.PageSize = 1000;
            #endregion

            lstWardRollCsv = new List<WardRollBookCsvReportModel>();

            bool DataFound = false;

            do
            {
                pageList.PageNo = PageNo;
                Response<WardRollBookCsvReportModel> response = await GenericHttpClient.PostRequest<WardRollBookCsvReportModel>("File/GetExportCsvRecordsList", pageList);
                if (response.Status && response.Data.ListItems.ToList() != null && response.Data.ListItems.ToList().Count > 0)
                {
                    lstWardRollCsv.AddRange(response.Data.ListItems.ToList());
                    DataFound = true;
                }
                else
                {
                    DataFound = false;
                }

                PageNo++;

            } while (DataFound);

            if (lstWardRollCsv != null && lstWardRollCsv.Count > 0)
            {
                dtExportCsvReport = new DataTable();
                dtExportCsvReport = ToDataTable(lstWardRollCsv);
            }

            lstWardRollCsv.Clear();
        }
        #endregion

        #region Get Export Records List For Ward Photo
        /// <summary>
        /// Gets the export records list.
        /// </summary>
        private async Task GetExportRecordsDataForWardPhoto()
        {
            #region pageList
            PageList pageList = new PageList();

            // Current Database Filed 
            pageList.DatabaseName = GlobalsModel.LoggedUser.Database.ToString();

            //pageList.KeyWord = txtSearch.Text;

            // Filter Filed
            pageList.FiledName = Convert.ToString(cmbFiled.SelectedValue);
            if (exportModel.rdbWardRollDeletion.Checked)
            {
                pageList.FiledValue = "DELETED";
            }
            else
            {
                pageList.FiledValue = Convert.ToString(cmbStatus.SelectedValue);
            }

            if (exportModel.rdbWardPhotoRollBook.Checked == true)
            {
                // Gender
                if (exportModel.rdbGenderAll.Checked == true)
                {
                    pageList.Gender = "All";
                }
                else if (exportModel.rdbGenderMale.Checked == true)
                {
                    pageList.Gender = "M";
                }
                else if (exportModel.rdbGenderFemale.Checked == true)
                {
                    pageList.Gender = "F";
                }
                else if (exportModel.rdbGenderOther.Checked == true)
                {
                    pageList.Gender = "O";
                }

                // Disability
                if (exportModel.rdbDisabilityAll.Checked == true)
                {
                    pageList.Disability = "All";
                }
                else if (exportModel.rdbDisabilityWith.Checked == true)
                {
                    pageList.Disability = "With";
                }

                // Photo
                if (exportModel.rdbPhotoAll.Checked == true)
                {
                    pageList.Photo = "All";
                }
                else if (exportModel.rdbPhotoWith.Checked == true)
                {
                    pageList.Photo = "WithPhoto";
                }
                else if (exportModel.rdbPhotoWithout.Checked == true)
                {
                    pageList.Photo = "WithoutPhoto";
                }

                //pageList.DateRange = "All";
                //pageList.DateFrom = null;
                //pageList.DateTo = null;
                //pageList.ElectorByIDs = "All";

                // Date Range
                if (exportModel.rdbDateRange.Checked == true)
                {
                    pageList.DateRange = "Range";
                    TimeSpan FromTime = new TimeSpan(00, 00, 00);
                    var FromDate = exportModel.dtpFromDate.Value.Date + FromTime;
                    pageList.DateFrom = FromDate.ToUniversalTime();

                    TimeSpan ToTime = new TimeSpan(23, 59, 59);
                    var ToDate = exportModel.dtpToDate.Value.Date + ToTime;
                    pageList.DateTo = ToDate.ToUniversalTime();

                }
                else if (exportModel.rdbDateAll.Checked == true)
                {
                    pageList.DateRange = "All";
                    pageList.DateFrom = null;
                    pageList.DateTo = null;
                }

                // Elector By IDs
                if (exportModel.rdbElectorByIDsSelected.Checked == true)
                {
                    pageList.ElectorByIDs = string.Join(",", ElectorIDs);
                }
                else if (exportModel.rdbElectorByIDsAll.Checked == true)
                {
                    pageList.ElectorByIDs = "All";
                }

                if (exportModel.rdbAlphaSplit.Checked == true)
                {
                    pageList.AlphaSplit = "AlphaSplit";
                    pageList.AlphaSplitStart = exportModel.txtAlphaSplitStart.Text.Trim().ToLower();
                    pageList.AlphaSplitEnd = exportModel.txtAlphaSplitEnd.Text.Trim().ToLower();
                }
                else
                {
                    pageList.AlphaSplit = "All";
                    pageList.AlphaSplitStart = string.Empty;
                    pageList.AlphaSplitEnd = string.Empty;
                }
            }
            else
            {
                pageList.Gender = "All";
                pageList.Disability = "All";
                pageList.Photo = "All";
                pageList.DateRange = "All";
                pageList.DateFrom = null;
                pageList.DateTo = null;
                pageList.ElectorByIDs = "All";
                pageList.AlphaSplit = "All";
                pageList.AlphaSplitStart = string.Empty;
                pageList.AlphaSplitEnd = string.Empty;
            }

            pageList.FName = txtFName.Text.Trim();
            pageList.LName = txtLName.Text.Trim();

            // TreeView Filed
            pageList.GeoName = TreeStructure.MainTagName;
            pageList.GeoId = TreeStructure.Id;

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
                Response<WardRollBookReortModel> response = await GenericHttpClient.PostRequest<WardRollBookReortModel>("File/GetExportRecordsList", pageList);
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

        #region Reset Header CheckBox Location
        /// <summary>
        /// Resets the header CheckBox location.
        /// </summary>
        /// <param name="ColumnIndex">Index of the column.</param>
        /// <param name="RowIndex">Index of the row.</param>
        private void ResetHeaderCheckBoxLocation(int ColumnIndex, int RowIndex)
        {
            //Get the column header cell bounds
            Rectangle oRectangle = this.dgFile.GetCellDisplayRectangle(ColumnIndex, RowIndex, true);

            Point oPoint = new Point();

            oPoint.X = oRectangle.Location.X + (oRectangle.Width - headerCheckBox.Width) / 2 + 1;
            oPoint.Y = oRectangle.Location.Y + (oRectangle.Height - headerCheckBox.Height) / 2 + 1;

            //Change the location of the CheckBox to make it stay on the header
            headerCheckBox.Location = oPoint;
        }
        #endregion

        #region Get Ward Roll Status Count Export Csv Data
        /// <summary>
        /// Gets the ward roll status count export CSV data.
        /// </summary>
        private async Task GetWardRollStatusCountExportCsvData()
        {
            lstWardRoll = new List<WardRollBookReortModel>();

            try
            {
                #region pageList
                pageList = new PageList();

                // Page Name
                pageList.PageName = pageName;

                // WardRoll Export CSV
                pageList.isReportOrCSV = true;

                // Current Database Filed 
                pageList.DatabaseName = GlobalsModel.LoggedUser.Database.ToString();

                // Filter Filed
                pageList.FiledName = Convert.ToString(cmbFiled.SelectedValue);
                pageList.FiledValue = Convert.ToString(cmbStatus.SelectedValue);

                pageList.FName = txtFName.Text.Trim();
                pageList.LName = txtLName.Text.Trim();

                string gender = "All";

                if (cmbGender.Text.Trim() == "MALE")
                {
                    gender = "M";
                }
                else if (cmbGender.Text.Trim() == "FEMALE")
                {
                    gender = "F";
                }
                else if (cmbGender.Text.Trim() == "OTHER")
                {
                    gender = "O";
                }

                pageList.Gender = gender;
                pageList.Disability = "All";

                pageList.Photo = "All";
                pageList.DateRange = "All";
                pageList.DateFrom = null;
                pageList.DateTo = null;
                pageList.ElectorByIDs = "All";

                pageList.AlphaSplit = "All";
                pageList.AlphaSplitStart = string.Empty;
                pageList.AlphaSplitEnd = string.Empty;

                // TreeView Filed
                pageList.GeoName = TreeStructure.MainTagName;
                pageList.GeoId = TreeStructure.Id;

                // Login User
                pageList.LoggedUserId = GlobalsModel.LoggedUser.User.UserId;

                // Page No
                pageList.PageNo = 1;

                // Page Size
                pageList.PageSize = 1000;
                #endregion

                if (pageList != null)
                {
                    Response<WardRollBookReortModel> response = await GenericHttpClient.PostRequest<WardRollBookReortModel>("File/GetExportRecordsList", pageList);
                    if (response.Status && response?.Data?.ListItems?.ToList()?.Count > 0)
                    {
                        lstWardRoll.AddRange(response.Data.ListItems.ToList());
                        if (lstWardRoll?.Count > 0)
                        {
                            lstWardRoll.ForEach(x => x.Status = Convert.ToString(cmbStatus.SelectedValue));
                            dtExportCsvReportData = new DataTable();
                            dtExportCsvReportData = ToDataTable(lstWardRoll);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(Resources.ErrorMessage, "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ErrorLog.Write("File", "GetWardRollStatusCountExportCsvData", ex);
            }
            finally
            {
                lstWardRoll.Clear();
            }
        }
        #endregion

        #region GetWardClanSectionSummary
        /// <summary>
        /// Gets the ward clan section summary.
        /// </summary>
        private async Task GetWardClanSectionSummary()
        {
            #region pageList
            PageList pageList = new PageList();
            // Current Database Filed 
            pageList.DatabaseName = GlobalsModel.LoggedUser.Database.ToString();

            // TreeView Filed
            pageList.GeoName = TreeStructure.MainTagName;
            pageList.GeoId = TreeStructure.Id;
            #endregion

            var lstWardClanSectionSummary = new List<GetWardClanSectionSummaryModel>();

            Response<GetWardClanSectionSummaryModel> response = await GenericHttpClient.PostRequest<GetWardClanSectionSummaryModel>("File/GetWardClanSectionSummaryList", pageList);
            if (response.Status && response.Data.ListItems.ToList() != null && response.Data.ListItems.ToList().Count > 0)
            {
                lstWardClanSectionSummary.AddRange(response.Data.ListItems.ToList());
            }

            if (lstWardClanSectionSummary != null && lstWardClanSectionSummary.Count > 0)
            {
                dtWardClanSectionSummary = new DataTable();
                dtWardClanSectionSummary = ToDataTable(lstWardClanSectionSummary);
            }

            lstWardClanSectionSummary.Clear();
        }

        #region SevisPass Color Coding
        /// <summary>
        /// Visual formatting and color coding for SevisPass verification status (Point 10)
        /// </summary>
        private void dgFile_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.RowIndex >= this.dgFile.Rows.Count) return;

                if (this.dgFile.Columns.Contains("SevisPassStatus"))
                {
                    var row = this.dgFile.Rows[e.RowIndex];
                    var statusObj = row.Cells["SevisPassStatus"].Value;
                    if (statusObj != null && !Convert.IsDBNull(statusObj))
                    {
                        string statusVal = statusObj.ToString().Trim();
                        string colName = this.dgFile.Columns[e.ColumnIndex].Name;

                        if (colName == "SevisPassStatus" || colName == "SevisPassId")
                        {
                            if (string.Equals(statusVal, "Verified", StringComparison.OrdinalIgnoreCase))
                            {
                                e.CellStyle.BackColor = Color.FromArgb(220, 245, 220); // LightGreen
                                e.CellStyle.ForeColor = Color.DarkGreen;
                                e.CellStyle.SelectionBackColor = Color.FromArgb(180, 230, 180);
                                e.CellStyle.SelectionForeColor = Color.DarkGreen;
                            }
                            else if (string.Equals(statusVal, "Mismatch", StringComparison.OrdinalIgnoreCase))
                            {
                                e.CellStyle.BackColor = Color.FromArgb(255, 225, 225); // LightPink/Red
                                e.CellStyle.ForeColor = Color.DarkRed;
                                e.CellStyle.SelectionBackColor = Color.FromArgb(255, 190, 190);
                                e.CellStyle.SelectionForeColor = Color.DarkRed;
                            }
                            else if (string.Equals(statusVal, "NotFound", StringComparison.OrdinalIgnoreCase))
                            {
                                e.CellStyle.BackColor = Color.FromArgb(255, 250, 205); // LightYellow
                                e.CellStyle.ForeColor = Color.DarkGoldenrod;
                                e.CellStyle.SelectionBackColor = Color.FromArgb(255, 240, 170);
                                e.CellStyle.SelectionForeColor = Color.DarkGoldenrod;
                            }
                        }
                    }
                }
            }
            catch
            {
                // Suppress formatting errors
            }
        }
        #endregion

        #endregion

        #endregion
    }
}