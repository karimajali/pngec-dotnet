using Microsoft.Reporting.WinForms;
using Newtonsoft.Json;
using PNGEC.API.Domain;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Domain.Mobile.Voter;
using PNGEC.API.Domain.Report;
using PNGEC.API.Helper;
using PNGEC.Desktop.Model;
using PNGEC.Desktop.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace PNGEC.Desktop.Forms
{
    public partial class MKAFile : Form
    {
        #region variable declaration
        /// <summary>
        /// The header CheckBox
        /// </summary>
        private CheckBox headerCheckBox = new CheckBox();

        /// <summary>
        /// The MKAgeo tree structure
        /// </summary>
        private MKAGeoTreeStructure MKAGeoTreeStructure = null;

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
        private string[] filedArray = { "Status" };

        /// <summary>
        /// The status array
        /// </summary>
        /// "UNDER AGE"
        private string[] StatusArray = { "ALL", "MKA WARD ROLL", "OLD", "NEW", "UPDATED", "DECEASED", "TRANSFERRED OUT", "GHOST", "DELETED", "DUPLICATE", "TRANSFERRED IN", "UNDER AGE" };

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
        private MKAWardRollBookExportModel exportModel = null;

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
        public MKAFile()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="File"/> class.
        /// </summary>
        /// <param name="pageName">Name of the page.</param>
        public MKAFile(string pageName)
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
        private void MKAFile_Load(object sender, EventArgs e)
        {
            headerCheckBox.Click -= new EventHandler(HeaderCheckBox_Clicked);
            headerCheckBox.Click += new EventHandler(HeaderCheckBox_Clicked);

            dgFile.CellContentClick -= new DataGridViewCellEventHandler(dgFile_CellClick);
            dgFile.CellContentClick += new DataGridViewCellEventHandler(dgFile_CellClick);

            GlobalsModel.mainMaster.TreeViewSelectedChanged -= TreeViewSelectedChanged_AfterSelect;
            GlobalsModel.mainMaster.TreeViewSelectedChanged += TreeViewSelectedChanged_AfterSelect;

            FiledComboboxBind();

            //if (GlobalsModel.LoggedUser.Database != CommonEnum.Database.Final && this.pageName == "MKAWardRollBook")
            if (this.pageName == "MKAWardRollBook")
            {
                cmbStatus.SelectedIndex = cmbStatus.FindStringExact("MKA WARD ROLL");
            }

            ManagePanel();

            if (GlobalsModel.TreeViewStructure != null)
            {
                LoadGridViewSetting();
            }

            if (MKAGeoTreeStructure.MainTagName == "MKAWard" || MKAGeoTreeStructure.MainTagName == "MKALocation")
            {
                pnlClan.Visible = true;
                int selectedWard = MKAGeoTreeStructure.MainTagName == "MKAWard" ? MKAGeoTreeStructure.Id : MKAGeoTreeStructure.ParentId;
                BindMKAClan(selectedWard);
            }
            else
                pnlClan.Visible = false;
        }

        /// <summary>
        /// Handles the After Select event for tvMKAGeoStructure control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void tvMKAGeoStructure_AfterSelect(object sender, TreeViewEventArgs e)
        {
            MKAGeoTreeStructure = (MKAGeoTreeStructure)e.Node.Tag;
            await GridViewSetting();

        }

        /// <summary>
        /// Handles the After Collapse event for tvMKAGeoStructure control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvMKAGeoStructure_AfterCollapse(object sender, TreeViewEventArgs e)
        {

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
                if (this.pageName == "MKAWardRollBook")
                {
                    exportModel = new MKAWardRollBookExportModel();
                    if (exportModel.ShowDialog() == DialogResult.OK)
                    {
                        if (exportModel.IsExportToCsv)
                        {
                            if (exportModel.rdbMKAWardRollBook.Checked == true)
                            {
                                #region Ward Roll Book

                                string exportReportName = string.Empty;

                                #region filename
                                if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Staging && cmbStatus.Text != "DUPLICATE")
                                {
                                    exportReportName = GlobalsModel.reportName + "_Working_" + MKAGeoTreeStructure.MainTagName + "RollBook";
                                }
                                else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Preliminary && cmbStatus.Text != "DUPLICATE")
                                {
                                    exportReportName = GlobalsModel.reportName + "_Preliminary_" + MKAGeoTreeStructure.MainTagName + "RollBook";
                                }
                                else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Final && cmbStatus.Text != "DUPLICATE")
                                {
                                    exportReportName = GlobalsModel.reportName + "_Final_" + MKAGeoTreeStructure.MainTagName + "RollBook";
                                }
                                else if (cmbStatus.Text != "")
                                {
                                    exportReportName = MKAGeoTreeStructure.MainTagName + "RollBook_DuplicateVoters";
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
                                        string[] ColumnsToBeDeleted = { "Gender", "DOB", "MKAClanId" };
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
                                        dtExportCsvReport.Columns["MKALLGName"].ColumnName = "MKA Llg";
                                        dtExportCsvReport.Columns["MKAWardName"].ColumnName = "MKA Ward";
                                        dtExportCsvReport.Columns["MKADistrictName"].ColumnName = "MKA District";
                                        dtExportCsvReport.Columns["MKALocation"].ColumnName = "MKA Location";
                                        dtExportCsvReport.Columns["MKAClanName"].ColumnName = "MKA Clan Name";

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

                                        messageBoxText = string.Format("{0} Roll Book CSV is exported sucessfully at {1}", MKAGeoTreeStructure.MainTagName, saveFileDialog.FileName);
                                        MessageBox.Show(messageBoxText, "MKA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else
                                    {
                                        MessageBox.Show("No record founds.", "MKA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                }

                                #endregion
                            }
                        }
                        else
                        {
                            if (exportModel.rdbMKAWardRollBook.Checked == true)
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
                                    exportReportName = "Working_" + GlobalsModel.reportName + genderSplit + MKAGeoTreeStructure.MainTagName + "RollBook";
                                }
                                else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Preliminary && cmbStatus.Text != "DUPLICATE")
                                {
                                    exportReportName = "Preliminary_" + GlobalsModel.reportName + genderSplit + MKAGeoTreeStructure.MainTagName + "RollBook";
                                }
                                else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Final && cmbStatus.Text != "DUPLICATE")
                                {
                                    exportReportName = "Final_" + GlobalsModel.reportName + genderSplit + MKAGeoTreeStructure.MainTagName + "RollBook";
                                }
                                else if (cmbStatus.Text != "")
                                {
                                    exportReportName = MKAGeoTreeStructure.MainTagName + "RollBook_DuplicateVoters";
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
                                        await GetMKAWardClanSectionSummary();
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
                                        byte[] bytes = WardRollBookExportReport();
                                        //ExportGridToPDF(dgFile, "WardRollBook");

                                        using (FileStream stream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                                        {
                                            stream.Write(bytes, 0, bytes.Length);
                                        }

                                        WardRollMessage = string.Format("{0} Roll Book is exported sucessfully at {1}", MKAGeoTreeStructure.MainTagName, saveFileDialog.FileName);
                                        //messageBoxText = string.Format("{0} Roll Book is exported sucessfully at {1}", MKAGeoTreeStructure.MainTagName, saveFileDialog.FileName);
                                        //MessageBox.Show(messageBoxText, "MKA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else
                                    {
                                        WardRollMessage = string.Format("{0} Roll Book is No record founds.", MKAGeoTreeStructure.MainTagName);
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
                                            exportWardRollEnvelopeName = "Working_" + GlobalsModel.reportName + "_" + MKAGeoTreeStructure.MainTagName + "RollEnvelope.pdf";
                                        }
                                        else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Preliminary)
                                        {
                                            exportWardRollEnvelopeName = "Preliminary_" + GlobalsModel.reportName + "_" + MKAGeoTreeStructure.MainTagName + "RollEnvelope.pdf";
                                        }
                                        else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Final)
                                        {
                                            exportWardRollEnvelopeName = "Final_" + GlobalsModel.reportName + "_" + MKAGeoTreeStructure.MainTagName + "RollEnvelope.pdf";
                                        }

                                        exportWardRollEnvelopeName = Path.Combine(folderBasePath, exportWardRollEnvelopeName);

                                        if (MKAGeoTreeStructure.MainTagName == "Ward")
                                        {
                                            var Ward = Globals.GeoList.WardList.Where(x => x.WardId == MKAGeoTreeStructure.Id).FirstOrDefault();
                                            regionId = Ward.RegionId;
                                            provinceId = Ward.ProvinceId;
                                            districtId = Ward.DistrictId;
                                            llgId = Ward.LLGId;
                                            wardId = Ward.WardId;
                                            locationId = 0;
                                        }
                                        else
                                        {
                                            var Location = Globals.GeoList.LocationList.Where(x => x.LocationId == MKAGeoTreeStructure.Id).FirstOrDefault();
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

                                        WardRollEnvelopeMessage = string.Format("{0} RollEnvelope is exported sucessfully at {1}", MKAGeoTreeStructure.MainTagName, exportWardRollEnvelopeName);
                                        //messageBoxText = string.Format("{0} RollEnvelope is exported sucessfully at {1}", MKAGeoTreeStructure.MainTagName, exportWardRollEnvelopeName);
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
                                            exportWardPreFieldWorkSummaryName = "Working_" + GlobalsModel.reportName + "_" + MKAGeoTreeStructure.MainTagName + "PreFieldWorkSummary.pdf";
                                        }
                                        else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Preliminary)
                                        {
                                            exportWardPreFieldWorkSummaryName = "Preliminary_" + GlobalsModel.reportName + "_" + MKAGeoTreeStructure.MainTagName + "PreFieldWorkSummary.pdf";
                                        }
                                        else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Final)
                                        {
                                            exportWardPreFieldWorkSummaryName = "Final_" + GlobalsModel.reportName + "_" + MKAGeoTreeStructure.MainTagName + "PreFieldWorkSummary.pdf";
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

                                            WardWardPreFieldWorkSummary = string.Format("{0} Roll Pre Field Work Summary is exported sucessfully at {1}", MKAGeoTreeStructure.MainTagName, exportWardPreFieldWorkSummaryName);
                                            //messageBoxText = string.Format("{0} Roll Book is exported sucessfully at {1}", MKAGeoTreeStructure.MainTagName, exportWardPreFieldWorkSummaryName);
                                            //MessageBox.Show(messageBoxText, "MKA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }
                                        else
                                        {
                                            WardWardPreFieldWorkSummary = string.Format("{0} Roll Pre Field Work Summary is No record founds.", MKAGeoTreeStructure.MainTagName);
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
                                    exportReportName = "Working_" + GlobalsModel.reportName + genderSplit + MKAGeoTreeStructure.MainTagName + "PhotoRollBook";
                                }
                                else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Preliminary && cmbStatus.Text != "DUPLICATE")
                                {
                                    exportReportName = "Preliminary_" + GlobalsModel.reportName + genderSplit + MKAGeoTreeStructure.MainTagName + "PhotoRollBook";
                                }
                                else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Final && cmbStatus.Text != "DUPLICATE")
                                {
                                    exportReportName = "Final_" + GlobalsModel.reportName + genderSplit + MKAGeoTreeStructure.MainTagName + "PhotoRollBook";
                                }
                                else if (cmbStatus.Text != "")
                                {
                                    exportReportName = MKAGeoTreeStructure.MainTagName + "PhotoRollBook_DuplicateVoters";
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

                                        WardPhotoRollMessage = string.Format("{0} Photo Roll Book is exported sucessfully at {1}", MKAGeoTreeStructure.MainTagName, saveFileDialog.FileName);
                                        //messageBoxText = string.Format("{0} Photo Roll Book is exported sucessfully at {1}", MKAGeoTreeStructure.MainTagName, saveFileDialog.FileName);
                                        //MessageBox.Show(messageBoxText, "MKA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else
                                    {
                                        WardPhotoRollMessage = string.Format("{0} Photo Roll Book is No record founds.", MKAGeoTreeStructure.MainTagName);
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
                                            exportWardRollEnvelopeName = "Working_" + GlobalsModel.reportName + "_" + MKAGeoTreeStructure.MainTagName + "RollEnvelope.pdf";
                                        }
                                        else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Preliminary)
                                        {
                                            exportWardRollEnvelopeName = "Preliminary_" + GlobalsModel.reportName + "_" + MKAGeoTreeStructure.MainTagName + "RollEnvelope.pdf";
                                        }
                                        else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Final)
                                        {
                                            exportWardRollEnvelopeName = "Final_" + GlobalsModel.reportName + "_" + MKAGeoTreeStructure.MainTagName + "RollEnvelope.pdf";
                                        }

                                        exportWardRollEnvelopeName = Path.Combine(folderBasePath, exportWardRollEnvelopeName);

                                        if (MKAGeoTreeStructure.MainTagName == "Ward")
                                        {
                                            var Ward = Globals.GeoList.WardList.Where(x => x.WardId == MKAGeoTreeStructure.Id).FirstOrDefault();
                                            regionId = Ward.RegionId;
                                            provinceId = Ward.ProvinceId;
                                            districtId = Ward.DistrictId;
                                            llgId = Ward.LLGId;
                                            wardId = Ward.WardId;
                                            locationId = 0;
                                        }
                                        else
                                        {
                                            var Location = Globals.GeoList.LocationList.Where(x => x.LocationId == MKAGeoTreeStructure.Id).FirstOrDefault();
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

                                        WardRollEnvelopeMessage = string.Format("{0} RollEnvelope is exported sucessfully at {1}", MKAGeoTreeStructure.MainTagName, exportWardRollEnvelopeName);
                                        //messageBoxText = string.Format("{0} RollEnvelope is exported sucessfully at {1}", MKAGeoTreeStructure.MainTagName, exportWardRollEnvelopeName);
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
                                            exportWardPreFieldWorkSummaryName = "Working_" + GlobalsModel.reportName + "_" + MKAGeoTreeStructure.MainTagName + "PreFieldWorkSummary.pdf";
                                        }
                                        else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Preliminary)
                                        {
                                            exportWardPreFieldWorkSummaryName = "Preliminary_" + GlobalsModel.reportName + "_" + MKAGeoTreeStructure.MainTagName + "PreFieldWorkSummary.pdf";
                                        }
                                        else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Final)
                                        {
                                            exportWardPreFieldWorkSummaryName = "Final_" + GlobalsModel.reportName + "_" + MKAGeoTreeStructure.MainTagName + "PreFieldWorkSummary.pdf";
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

                                            WardWardPreFieldWorkSummary = string.Format("{0} Roll Pre Field Work Summary is exported sucessfully at {1}", MKAGeoTreeStructure.MainTagName, exportWardPreFieldWorkSummaryName);
                                            //messageBoxText = string.Format("{0} Roll Book is exported sucessfully at {1}", MKAGeoTreeStructure.MainTagName, exportWardPreFieldWorkSummaryName);
                                            //MessageBox.Show(messageBoxText, "MKA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }
                                        else
                                        {
                                            WardWardPreFieldWorkSummary = string.Format("{0} Roll Pre Field Work Summary is No record founds.", MKAGeoTreeStructure.MainTagName);
                                            //MessageBox.Show("No record founds.", "MKA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                                    MessageBox.Show(messageBoxText, "MKA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }

                                #endregion
                            }
                            else if (exportModel.rdbVoterIdCard.Checked == true)
                            {
                                #region VoterIdCard
                                var saveFileDialog = new SaveFileDialog();
                                saveFileDialog.FileName = GlobalsModel.reportName + "_" + MKAGeoTreeStructure.MainTagName + "VoterIdCards";
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
                                        "Clan",
                                        "MKADistrict",
                                        "MKALLG",
                                        "MKAWard",
                                        "MKALocation",
                                        "MKAClanName",
                                        "MKAWardNo");
                                        //dtSubVoterCard = view.ToTable("tblSubVoterIdCard", false, "ElectorId", "Seq", "LName", "FName", "Gen", "BDate", "WithDisability", "Province", "District", "LLG", "Ward", "Location");
                                        dsVoterCard.Tables.AddRange(new DataTable[] { dtVoterCard, dtSubVoterCard });

                                        if (dsVoterCard.Tables[0] != null && dsVoterCard.Tables[0].Rows.Count > 0)
                                        {
                                            byte[] bytes = VoterIdCardExportReportSource();

                                            using (FileStream stream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                                            {
                                                stream.Write(bytes, 0, bytes.Length);
                                            }

                                            messageBoxText = string.Format("{0} Voter Id Cards is exported sucessfully at {1}", MKAGeoTreeStructure.MainTagName, saveFileDialog.FileName);
                                            MessageBox.Show(messageBoxText, "MKA", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                            //VoterIdCardReport voterCard = new VoterIdCardReport(dsVoterCard);
                                            //voterCard.Show();
                                        }
                                        else
                                        {
                                            MessageBox.Show("No record founds.", "MKA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("No record founds.", "MKA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                }
                                #endregion
                            }
                            else if (exportModel.rdbVoterPhotoCard.Checked == true)
                            {
                                #region VoterPhotoCard
                                var saveFileDialog = new SaveFileDialog();
                                saveFileDialog.FileName = GlobalsModel.reportName + "_" + MKAGeoTreeStructure.MainTagName + "VoterPhotoCards";
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
                                            "Clan",
                                            "MKADistrict",
                                            "MKALLG",
                                            "MKAWard",
                                            "MKALocation",
                                            "MKAClanName",
                                            "MKAWardNo");
                                        dsVoterCard.Tables.AddRange(new DataTable[] { dtVoterCard, dtSubVoterCard });

                                        if (dsVoterCard.Tables[0] != null && dsVoterCard.Tables[0].Rows.Count > 0)
                                        {
                                            byte[] bytes = VoterPhotoCardExportReportSource();

                                            using (FileStream stream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                                            {
                                                stream.Write(bytes, 0, bytes.Length);
                                            }

                                            messageBoxText = string.Format("{0} Voter Photo Card is exported sucessfully at {1}", MKAGeoTreeStructure.MainTagName, saveFileDialog.FileName);
                                            MessageBox.Show(messageBoxText, "MKA", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                            //VoterPhotoCard voterPhotoCard = new VoterPhotoCard(dsVoterCard);
                                            //voterPhotoCard.Show();
                                        }
                                        else
                                        {
                                            MessageBox.Show("No record founds.", "MKA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("No record founds.", "MKA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                }
                                #endregion
                            }
                            else if (exportModel.rdbVoterEnvelope.Checked == true)
                            {
                                #region VoterEnvelope
                                GlobalsModel.mainMaster.ShowLoader();

                                var saveFileDialog = new SaveFileDialog();
                                saveFileDialog.FileName = GlobalsModel.reportName + "_" + MKAGeoTreeStructure.MainTagName + "RollEnvelope";
                                saveFileDialog.DefaultExt = ".pdf";
                                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                                {
                                    int regionId, provinceId, districtId, llgId, wardId, locationId;

                                    if (MKAGeoTreeStructure.MainTagName == "Ward")
                                    {
                                        var Ward = Globals.GeoList.WardList.Where(x => x.WardId == MKAGeoTreeStructure.Id).FirstOrDefault();
                                        regionId = Ward.RegionId;
                                        provinceId = Ward.ProvinceId;
                                        districtId = Ward.DistrictId;
                                        llgId = Ward.LLGId;
                                        wardId = Ward.WardId;
                                        locationId = 0;
                                    }
                                    else
                                    {
                                        var Location = Globals.GeoList.LocationList.Where(x => x.LocationId == MKAGeoTreeStructure.Id).FirstOrDefault();
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

                                    messageBoxText = string.Format("{0} RollEnvelope is exported sucessfully at {1}", MKAGeoTreeStructure.MainTagName, saveFileDialog.FileName);
                                    MessageBox.Show(messageBoxText, "MKA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                #endregion
                            }
                            else if (exportModel.rdbWardRollDeletion.Checked == true)
                            {
                                #region Ward Roll Deletion

                                string exportReportName = string.Empty;
                                if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Staging)
                                {
                                    exportReportName = GlobalsModel.reportName + "_Staging_" + MKAGeoTreeStructure.MainTagName + "Deletion";
                                }
                                else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Preliminary)
                                {
                                    exportReportName = GlobalsModel.reportName + "_Preliminary_" + MKAGeoTreeStructure.MainTagName + "Deletion";
                                }
                                else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Final)
                                {
                                    exportReportName = GlobalsModel.reportName + "_Final_" + MKAGeoTreeStructure.MainTagName + "Deletion";
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

                                        messageBoxText = string.Format("{0} Deletion is exported sucessfully at {1}", MKAGeoTreeStructure.MainTagName, saveFileDialog.FileName);
                                        MessageBox.Show(messageBoxText, "MKA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else
                                    {
                                        MessageBox.Show("No record founds.", "MKA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                                    exportReportName = GlobalsModel.reportName + "_Working_" + MKAGeoTreeStructure.MainTagName + "PreFieldWorkSummary";
                                }
                                else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Preliminary)
                                {
                                    exportReportName = GlobalsModel.reportName + "_Preliminary_" + MKAGeoTreeStructure.MainTagName + "PreFieldWorkSummary";
                                }
                                else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Final)
                                {
                                    exportReportName = GlobalsModel.reportName + "_Final_" + MKAGeoTreeStructure.MainTagName + "PreFieldWorkSummary";
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

                                        messageBoxText = string.Format("{0} Roll Book is exported sucessfully at {1}", MKAGeoTreeStructure.MainTagName, saveFileDialog.FileName);
                                        MessageBox.Show(messageBoxText, "MKA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else
                                    {
                                        MessageBox.Show("No record founds.", "MKA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                                    exportReportName = GlobalsModel.reportName + "_Working_" + MKAGeoTreeStructure.MainTagName + "ExisitngVoterDetails";
                                }
                                else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Preliminary)
                                {
                                    exportReportName = GlobalsModel.reportName + "_Preliminary_" + MKAGeoTreeStructure.MainTagName + "ExisitngVoterDetails";
                                }
                                else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Final)
                                {
                                    exportReportName = GlobalsModel.reportName + "_Final_" + MKAGeoTreeStructure.MainTagName + "ExisitngVoterDetails";
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

                                            messageBoxText = string.Format("{0} Exisitng Voter Details is exported sucessfully at {1}", MKAGeoTreeStructure.MainTagName, saveFileDialog.FileName);
                                            MessageBox.Show(messageBoxText, "MKA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }
                                        else
                                        {
                                            MessageBox.Show("No record founds.", "MKA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("No record founds.", "MKA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

                    if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Staging)
                    {
                        exportReportName = $"Working_{GlobalsModel.reportName}_{MKAGeoTreeStructure.MainTagName}_DataMigration";
                    }
                    else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Preliminary)
                    {
                        exportReportName = $"Preliminary_{GlobalsModel.reportName}_{MKAGeoTreeStructure.MainTagName}_DataMigration";
                    }
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

                            WardRollMessage = string.Format("{0} data migration is exported sucessfully at {1}", MKAGeoTreeStructure.MainTagName, saveFileDialog.FileName);
                        }
                        else
                        {
                            WardRollMessage = string.Format("{0} data migration is No record founds.", MKAGeoTreeStructure.MainTagName);
                        }

                        MessageBox.Show(WardRollMessage, "MKA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

            }
            catch (Exception ex)
            {
                messageBoxText = string.Empty;

                ErrorLog.Write("File", "btnExport_Click", ex);
                MessageBox.Show("Something went wrong", "MKA", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                string[] StatusArrayForAdminEdit;

                if(GlobalsModel.LoggedUser.Database != CommonEnum.Database.Final)
                {
                    StatusArrayForAdminEdit = new string[] { "OLD", "NEW", "UPDATED", "DECEASED", "GHOST", "DUPLICATE", "UNDER AGE" };
                }
                else
                {
                    StatusArrayForAdminEdit = new string[] { };
                }

                VoterModel model = (VoterModel)dgFile.Rows[e.RowIndex].DataBoundItem;
                if (GlobalsModel.LoggedUser.User.Role == 0 || GlobalsModel.LoggedUser.User.Role == 2)
                {
                    //if (model != null && (model.Status == "OLD" || model.Status == "UPDATED"))
                    //if (model != null && (model.Status != "TRANSFERRED IN" || model.Status != "TRANSFERRED OUT" || model.Status != "DELETED"))
                    if (model != null && (StatusArrayForAdminEdit.Contains(model.Status)))
                    {

                        ROVoterDetails roUser = new ROVoterDetails(model, this.pageName);
                        roUser.Width = (GlobalsModel.mainMaster.Width - 100);
                        roUser.Height = (GlobalsModel.mainMaster.Height - 100);
                        if (roUser.ShowDialog() == DialogResult.OK)
                        {
                            await GridViewSetting();
                        }
                    }
                    else
                    {
                        MessageBox.Show("You can not edit selected status data", "MKA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
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
                    if (GlobalsModel.LoggedUser.User.Role == 7)
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
                    MessageBox.Show("You can transfer only OLD and UPDATED status data", "MKA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            //Reset MKA button
            else if (e.RowIndex >= 0 && dgFile.Columns[e.ColumnIndex].Name == "NotMKA")
            {
                // Clear all checkbox
                //if (Convert.ToString(cmbStatus.SelectedValue) != "OLD")
                //{
                //    if ((this.pageName == "DataMigration"
                //        && (GlobalsModel.LoggedUser.User.Role == 2 || GlobalsModel.LoggedUser.User.Role == 7 && GlobalsModel.LoggedUser.Database == CommonEnum.Database.Staging)
                //        || (GlobalsModel.LoggedUser.User.Role == 3 && GlobalsModel.LoggedUser.Database == CommonEnum.Database.Preliminary)) || this.pageName == "WardRollBook")
                //    {
                //        this.ClearCheckbox();
                //    }
                //}

                var allowAllStatusForAdmin = Convert.ToInt32(ConfigurationManager.AppSettings["AllowAllStatusAdmin"]);
                string[] StatusArrayForAdminEdit;

                if (allowAllStatusForAdmin == 1)
                    StatusArrayForAdminEdit = new string[] { "OLD", "NEW", "UPDATED", "DECEASED", "GHOST", "DUPLICATE", "UNDER AGE" };
                else
                    StatusArrayForAdminEdit = new string[] { "OLD", "UPDATED" };



                VoterModel model = (VoterModel)dgFile.Rows[e.RowIndex].DataBoundItem;
                if (GlobalsModel.LoggedUser.User.Role == 0)
                {
                    //if (model != null && (model.Status == "OLD" || model.Status == "UPDATED"))
                    //if (model != null && (model.Status != "TRANSFERRED IN" || model.Status != "TRANSFERRED OUT" || model.Status != "DELETED"))
                    if (model != null && (StatusArrayForAdminEdit.Contains(model.Status)))
                    {

                        //ROVoterDetails roUser = new ROVoterDetails(model, this.pageName);
                        //roUser.Width = (GlobalsModel.mainMaster.Width - 100);
                        //roUser.Height = (GlobalsModel.mainMaster.Height - 100);
                        ResetMKAForVoter(model);
                        //if (roUser.ShowDialog() == DialogResult.OK)
                        //{
                        //    await GridViewSetting();
                        //}
                    }
                    else
                    {
                        MessageBox.Show("You can not edit selected status data", "MKA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }

        }

        /// <summary>
        /// Reset MKA values for Selected Voter
        /// </summary>
        /// <param name="voterModel"></param>
        private async void ResetMKAForVoter(VoterModel selectedVoter)
        {


            VoterModel voterModel = selectedVoter;

            //Reset MKA Fields for the Selected Voter
            voterModel.MKADistrictId = 0;
            voterModel.MKAWardId = 0;
            voterModel.MKALLGId = 0;
            voterModel.MKALocationId = 0;
            voterModel.MKAClanId = 0;
            voterModel.Status = "UPDATED";


            if (voterModel != null && voterModel.ElectorId > 0)
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to reset MKA for the record?", "MKA", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    GlobalsModel.mainMaster.ShowLoader();

                    Response responseData = new Response();

                    MultipartFormDataContent formContent = new MultipartFormDataContent
                            {
                                {new StringContent(Convert.ToString(voterModel.ElectorId)),"ElectorId"},
                                {new StringContent(Convert.ToString(voterModel.LocationId)),"LocationId"},
                                {new StringContent(Convert.ToString(voterModel.WardId)),"WardId"},
                                {new StringContent(Convert.ToString(voterModel.LLGId)),"LLGId"},
                                {new StringContent(Convert.ToString(voterModel.DistrictId)),"DistrictId"},
                                //MKA Fields
                                {new StringContent(Convert.ToString(voterModel.MKALocationId)),"MKALocationId"},
                                {new StringContent(Convert.ToString(voterModel.MKAWardId)),"MKAWardId"},
                                {new StringContent(Convert.ToString(voterModel.MKALLGId)),"MKALLGId"},
                                {new StringContent(Convert.ToString(voterModel.MKADistrictId)),"MKADistrictId"},
                                {new StringContent(Convert.ToString(voterModel.MKAClanId)),"MKAClanId"},


                                {new StringContent(Convert.ToString(voterModel.ProvinceId)),"ProvinceId"},
                                {new StringContent(voterModel.LName == null ? String.Empty : voterModel.LName),"LName"},
                                {new StringContent(voterModel.FName == null ? String.Empty : voterModel.FName),"FName"},
                                {new StringContent(voterModel.Gender == null ? String.Empty : voterModel.Gender),"Gender"},
                                {new StringContent(Convert.ToString(voterModel.DOB)),"DOB"},
                                {new StringContent(Convert.ToString(voterModel.isPWD)),"isPWD"},
                                {new StringContent(voterModel.FatherLName == null ? "" : voterModel.FatherLName),"FatherLName"},
                                {new StringContent(voterModel.FatherFName == null ? String.Empty : voterModel.FatherFName),"FatherFName"},
                                {new StringContent(voterModel.MotherLName == null ? String.Empty : voterModel.MotherLName),"MotherLName"},
                                {new StringContent(voterModel.MotherFName == null ? String.Empty : voterModel.MotherFName),"MotherFName"},
                                {new StringContent(Convert.ToString(voterModel.ProvOfBirthFK)),"ProvOfBirthFK"},
                                {new StringContent(voterModel.PlaceOfBirth == null ? String.Empty : voterModel.PlaceOfBirth),"PlaceOfBirth"},
                                {new StringContent(Convert.ToString(voterModel.OccupationFK)),"OccupationFK"},
                                {new StringContent(voterModel.SubClan == null ? String.Empty : voterModel.SubClan),"SubClan"},
                                {new StringContent(voterModel.NatIDRecordNo == null ? String.Empty : voterModel.NatIDRecordNo ),"NatIDRecordNo"},
                                {new StringContent(voterModel.Status == null ? String.Empty : voterModel.Status ),"Status"},
                                {new StringContent(Convert.ToString(voterModel.FamilySortOrder)),"FamilySortOrder"},
                                {new StringContent(Convert.ToString(voterModel.Lat)),"Lat"},
                                {new StringContent(Convert.ToString(voterModel.Long)),"Long"},
                                {new StringContent(voterModel.FamilyNumber == null ? String.Empty : voterModel.FamilyNumber),"FamilyNumber"},
                                {new StringContent(voterModel.FamilyName == null ? String.Empty : voterModel.FamilyName),"FamilyName"},
                                {new StringContent(voterModel.Clan == null? String.Empty:voterModel.Clan),"Clan"},
                                {new StringContent(voterModel.RestHouse == null ? String.Empty : voterModel.RestHouse),"RestHouse"},
                                {new StringContent(voterModel.SectionNo == null ? String.Empty: voterModel.SectionNo),"SectionNo"},
                                {new StringContent(voterModel.LotNo == null ? String.Empty : voterModel.LotNo),"LotNo"},
                                {new StringContent(voterModel.StreetName == null ? String.Empty : voterModel.StreetName),"StreetName"},
                                {new StringContent(voterModel.SuburbTownSettlement == null ? String.Empty : voterModel.SuburbTownSettlement ),"SuburbTownSettlement"},
                                {new StringContent(voterModel.Comment == null? String.Empty:voterModel.Comment),"Comment"},
                                {new StringContent(voterModel.Religion == null ? String.Empty : voterModel.Religion),"Religion"},
                            };

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(Globals.serverURL);
                        using (HttpResponseMessage response = client.PostAsync("DataMigration/UpdateVoter", formContent).Result)
                        {
                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                string responseBody = await response.Content.ReadAsStringAsync();
                                responseData = JsonConvert.DeserializeObject<Response>(responseBody);
                            }
                        }
                    }

                    GlobalsModel.mainMaster.HideLoader();

                    if (responseData.Status)
                    {
                        MessageBox.Show(responseData.Message, "MKA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        await GridViewSetting();
                    }
                    else
                    {
                        MessageBox.Show(responseData.Message, "MKA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
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
            if (MKAGeoTreeStructure.MainTagName == "MKAWard" || MKAGeoTreeStructure.MainTagName == "MKALocation")
            {
                pnlClan.Visible = true;
                int selectedWard = MKAGeoTreeStructure.MainTagName == "MKAWard" ? MKAGeoTreeStructure.Id : MKAGeoTreeStructure.ParentId;
                BindMKAClan(selectedWard);
            }
            else
                pnlClan.Visible = false;

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
                        exportReportName = GlobalsModel.reportName + "_Working_" + MKAGeoTreeStructure.MainTagName + "_TransferOut";
                    }
                    else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Preliminary)
                    {
                        exportReportName = GlobalsModel.reportName + "_Preliminary_" + MKAGeoTreeStructure.MainTagName + "_TransferOut";
                    }
                    else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Final)
                    {
                        exportReportName = GlobalsModel.reportName + "_Final_" + MKAGeoTreeStructure.MainTagName + "_TransferOut";
                    }
                    #endregion
                }
                else if (this.pageName == "Duplicate")
                {
                    #region filename
                    if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Staging)
                    {
                        exportReportName = GlobalsModel.reportName + "_Working_" + MKAGeoTreeStructure.MainTagName + "_Duplicate";
                    }
                    else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Preliminary)
                    {
                        exportReportName = GlobalsModel.reportName + "_Preliminary_" + MKAGeoTreeStructure.MainTagName + "_Duplicate";
                    }
                    else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Final)
                    {
                        exportReportName = GlobalsModel.reportName + "_Final_" + MKAGeoTreeStructure.MainTagName + "_Duplicate";
                    }
                    #endregion
                }
                else if (this.pageName == "MKAWardRollBook" && btnExportToCSV.Visible == true)
                {
                    //#region filename
                    //if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Staging)
                    //{
                    //    exportReportName = GlobalsModel.reportName + "_Working_" + MKAGeoTreeStructure.MainTagName + "_WardRollBook";
                    //}
                    //else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Preliminary)
                    //{
                    //    exportReportName = GlobalsModel.reportName + "_Preliminary_" + MKAGeoTreeStructure.MainTagName + "_WardRollBook";
                    //}
                    //else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Final)
                    //{
                    //    exportReportName = GlobalsModel.reportName + "_Final_" + MKAGeoTreeStructure.MainTagName + "_WardRollBook";
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

                    if (this.pageName == "MKAWardRollBook" && btnExportToCSV.Visible == true)
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
                            dtTransferOut.Columns["RowNo"].ColumnName = "No";
                            dtTransferOut.Columns["VoterName"].ColumnName = "Voter";
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
                        else if (this.pageName == "MKAWardRollBook")
                        {
                            #region MKAWardRollBook
                            int provinceId = 0;

                            if (MKAGeoTreeStructure.MainTagName == "MKALLG")
                            {
                                // provinceId = Globals.MKAGeoList.MKALLGList.Where(x => x.LLGId == MKAGeoTreeStructure.Id).Select(s => s.ProvinceId).FirstOrDefault();
                            }
                            else if (MKAGeoTreeStructure.MainTagName == "MKADistrict")
                            {
                                //provinceId = Globals.MKAGeoList.MKADistrictList.Where(x => x.DistrictId == MKAGeoTreeStructure.Id).Select(s => s.ProvinceId).FirstOrDefault();

                            }
                            //else if (MKAGeoTreeStructure.MainTagName == "Province")
                            //{
                            //    provinceId = MKAGeoTreeStructure.Id;
                            //}

                            if (provinceId > 0 && provinceId == 13) //13 is NCD Province so display location
                            {
                                DataView view = new DataView(dtExportCsvReportData);
                                dtTransferOut = view.ToTable(false,
                                    "MKADistrict",
                                    "MKALLG",
                                    "MKAWardNo",
                                    "MKAWard",
                                    "Location",
                                    "Status",
                                    "Count");

                                // rename column
                                dtTransferOut.Columns["Province"].ColumnName = "Province Name";
                                dtTransferOut.Columns["District"].ColumnName = "MKA District Name";
                                dtTransferOut.Columns["LLG"].ColumnName = "MKA LLG Name";
                                dtTransferOut.Columns["WardNo"].ColumnName = "MKA Ward Number";
                                dtTransferOut.Columns["Ward"].ColumnName = "MKA Ward Name";
                                dtTransferOut.Columns["Location"].ColumnName = "MKA Location Name";
                                dtTransferOut.Columns["Status"].ColumnName = "Status";
                                dtTransferOut.Columns["Count"].ColumnName = "Voters";
                            }
                            else
                            {
                                DataView view = new DataView(dtExportCsvReportData);
                                dtTransferOut = view.ToTable(false,
                                    "District",
                                    "LLG",
                                    "WardNo",
                                    "Ward",
                                    "Status",
                                    "Count");

                                // rename column
                                //dtTransferOut.Columns["Province"].ColumnName = "Province Name";
                                dtTransferOut.Columns["District"].ColumnName = "MKA District Name";
                                dtTransferOut.Columns["LLG"].ColumnName = "MKA LLG Name";
                                dtTransferOut.Columns["WardNo"].ColumnName = "MKA Ward Number";
                                dtTransferOut.Columns["Ward"].ColumnName = "MKA Ward Name";
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

                        messageBoxText = $"{MKAGeoTreeStructure.MainTagName} {this.pageName} CSV is exported sucessfully at {saveFileDialog.FileName}";
                        //messageBoxText = string.Format("{0} {1} CSV is exported sucessfully at {2}", MKAGeoTreeStructure.MainTagName, this.pageName, saveFileDialog.FileName);
                        MessageBox.Show(messageBoxText, "MKA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No record founds.", "MKA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    GlobalsModel.mainMaster.HideLoader();
                }
            }
            catch (Exception ex)
            {
                GlobalsModel.mainMaster.HideLoader();
                MessageBox.Show(Resources.ErrorMessage, "MKA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                        if (model != null && (model.Status == "OLD" || model.Status == "UPDATED") && GlobalsModel.LoggedUser.User.Role == 7)
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
                MessageBox.Show("At least one OLD and UPDATED status records checkbox must be selected.", "MKA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //MessageBox.Show("You can transfer only OLD and UPDATED status data.", "MKA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //MessageBox.Show("At least one checkbox must be selected.", "MKA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            MKAGeoTreeStructure = GlobalsModel.MKATreeViewStructure;
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
                (GlobalsModel.LoggedUser.User.Role == 2 && GlobalsModel.LoggedUser.Database == CommonEnum.Database.Staging) ||
                (GlobalsModel.LoggedUser.User.Role == 3 && GlobalsModel.LoggedUser.Database == CommonEnum.Database.Preliminary)) &&
                Convert.ToString(cmbStatus.SelectedValue) != "OLD" &&
                Convert.ToString(cmbStatus.SelectedValue) != "ALL" &&
                Convert.ToString(cmbStatus.SelectedValue) != "MKA WARD ROLL")
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
            else if (this.pageName == "MKAWardRollBook" || this.pageName == "DataMigrationReport")
            {
                pnlApproveReject.Visible = false;
                pnlApproval.Visible = false;

                //if (GlobalsModel.TreeViewStructure.MainTagName == "Ward" || GlobalsModel.TreeViewStructure.MainTagName == "LLG" || GlobalsModel.TreeViewStructure.MainTagName == "District")
                if (GlobalsModel.MKATreeViewStructure.MainTagName == "MKAWard" || GlobalsModel.MKATreeViewStructure.MainTagName == "MKALocation")
                {
                    #region This condition set for only MKA ward wise generate report
                    if (GlobalsModel.MKATreeViewStructure.MainTagName == "MKAWard")
                    {
                        pnlExport.Visible = true;
                    }
                    else
                    {
                        pnlExport.Visible = false;
                    }
                    #endregion

                }
                else
                {
                    pnlExport.Visible = false;
                }

                btnExportToCSV.Visible = false;

                if (this.pageName == "MKAWardRollBook")
                {
                    #region Export CSV report for Geo(Country), Region, Province, District and LLG 
                    if (GlobalsModel.MKATreeViewStructure.MainTagName == "MKAGeo" ||
                        GlobalsModel.MKATreeViewStructure.MainTagName == "MKADistrict" ||
                        GlobalsModel.MKATreeViewStructure.MainTagName == "MKALLG"
                        //GlobalsModel.MKATreeViewStructure.MainTagName == "MKAWard" ||
                        //GlobalsModel.MKATreeViewStructure.MainTagName == "LLG"
                        )
                    {
                        if (GlobalsModel.LoggedUser.Database != CommonEnum.Database.Final)
                        {
                            var selectedIndex = cmbStatus.SelectedIndex;
                            StatusArray = new string[] { "ALL", "MKA WARD ROLL", "OLD", "NEW", "UPDATED", "DECEASED", "TRANSFERRED OUT", "GHOST", "DELETED", "DUPLICATE", "TRANSFERRED IN", "UNDER AGE", "TRANSFER OUT with FORM 11", "TRANSFER OUT without FORM 11" };
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
                            StatusArray = new string[] { "ALL", "MKA WARD ROLL", "OLD", "NEW", "UPDATED", "DECEASED", "TRANSFERRED OUT", "GHOST", "DELETED", "DUPLICATE", "TRANSFERRED IN", "UNDER AGE" };
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
                (GlobalsModel.LoggedUser.User.Role == (int)CommonEnum.Role.RD || GlobalsModel.LoggedUser.User.Role == (int)CommonEnum.Role.EM
                 || GlobalsModel.LoggedUser.User.Role == (int)CommonEnum.Role.Admin || GlobalsModel.LoggedUser.User.Role == (int)CommonEnum.Role.SelectRole))
            {
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
            else if ((GlobalsModel.LoggedUser.User.Role == (int)CommonEnum.Role.EM || GlobalsModel.LoggedUser.User.Role == (int)CommonEnum.Role.RD || GlobalsModel.LoggedUser.User.Role == 0) && this.pageName == "TransferInApproval")
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
            var mkaLocs = Globals.MKAGeoList.MKALocationList.Select(l => new MKAGeoTreeStructure()
            {
                Id = l.LocationId,
                Name = l.LocationName,
                MainTagName = "Location",
                ParentId = l.WardId,
                Children = null
            });
            var mkaWards = Globals.MKAGeoList.MKAWardList.Select(w => new MKAGeoTreeStructure()
            {
                Id = w.WardId,
                Name = w.WardName,
                MainTagName = "Ward",
                ParentId = w.LLGId,
                Children = mkaLocs.Where(l => l.ParentId == w.WardId)
            });
            var mkaLLGs = Globals.MKAGeoList.MKALLGList.Select(w => new MKAGeoTreeStructure()
            {
                Id = w.LLGId,
                Name = w.LLGName,
                MainTagName = "LLG",
                ParentId = w.DistrictId,
                Children = mkaWards.Where(l => l.ParentId == w.LLGId)
            });
            var mkaDists = Globals.MKAGeoList.MKADistrictList.Select(w => new MKAGeoTreeStructure()
            {
                Id = w.DistrictId,
                Name = w.DistrictName,
                MainTagName = "District",
                ParentId = default(int),
                Children = mkaLLGs.Where(l => l.ParentId == w.DistrictId)
            });

            InitializeMKAActualTreeLogic(mkaDists);
        }
        #endregion

        #region InitializeActualTreeLogic
        /// <summary>
        /// Initializes the actual tree logic.
        /// </summary>
        /// <param name="region">The region.</param>
        private async void InitializeMKAActualTreeLogic(IEnumerable<MKAGeoTreeStructure> mkaDistricts)
        {
            //this.Cursor = Cursors.WaitCursor;
            GeoTreeStructure mkaGeoTreeStructure = new GeoTreeStructure();
            mkaGeoTreeStructure.Id = 0;
            mkaGeoTreeStructure.MainTagName = "MKAGeo";
            await Task.Delay(50);
            tvMKAGeoStructure.Invoke((Action)delegate ()
            {
                this.Cursor = Cursors.WaitCursor;
                TreeNode t = new TreeNode();
                t.Text = "MKA";
                t.Tag = mkaDistricts;
                t.Name = "District";
                t.ImageKey = "MKA";
                t.Tag = mkaGeoTreeStructure;
                tvMKAGeoStructure.BeginUpdate(); // No visual updates until we say 
                tvMKAGeoStructure.Nodes.Clear(); // Remove existing nodes
                tvMKAGeoStructure.Nodes.Add(t);
                PopulateTreeView(mkaDistricts, t);
                tvMKAGeoStructure.SelectedNode = t;
                tvMKAGeoStructure.SelectedNode.Expand();
                tvMKAGeoStructure.EndUpdate(); // Allow the treeview to update visually
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
        private void PopulateTreeView(IEnumerable<MKAGeoTreeStructure> data, TreeNode parentNode)
        {
            TreeNode childNode;
            foreach (MKAGeoTreeStructure row in data)
            {
                TreeNode t = new TreeNode();
                t.Text = row.Name.ToString();
                t.Tag = row;
                t.Name = row.MainTagName;
                t.ImageKey = Convert.ToString(row.Id);
                if (parentNode == null)
                {
                    tvMKAGeoStructure.Nodes.Add(t);
                    childNode = t;
                }
                else
                {
                    parentNode.Nodes.Add(t);
                    childNode = t;
                }

                //if (row.Children != null)
                //    PopulateTreeView(row.Children, childNode);

                if (row.Children != null && row.Children.Any())
                {
                    childNode.Nodes.Add("Loading...");
                }
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
                pageList.GeoName = MKAGeoTreeStructure.MainTagName;
                //pageList.GeoName = "District";
                pageList.GeoId = MKAGeoTreeStructure.Id;

                // Filter User Details
                pageList.FName = txtFName.Text.Trim();
                pageList.LName = txtLName.Text.Trim();

                pageList.ClanId = Convert.ToInt32(cmbClan.SelectedValue);

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
                    if (chkLookUp.Checked == false && (pageList.GeoName == "MKAGeo" || pageList.GeoName == "MKADistrict" || pageList.GeoName == "MKALLG"))
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
                MessageBox.Show(Resources.ErrorMessage, "MKA", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            GlobalsModel.mainMaster.ShowLoader();
            Response<VoterModel> response = await GenericHttpClient.PostRequest<VoterModel>("File/GetVoterList", model);
            GlobalsModel.mainMaster.HideLoader();
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
                else {
                    this.dgFile.BackgroundColor = Color.DarkGray;
                }
            }
            else
            {
                MessageBox.Show(response.Message, "MKA", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    //MKA Columns
                    this.dgFile.Columns["MKALocationId"].Visible = false;
                    this.dgFile.Columns["MKAWardId"].Visible = false;
                    this.dgFile.Columns["MKALLGId"].Visible = false;
                    this.dgFile.Columns["MKADistrictId"].Visible = false;
                    this.dgFile.Columns["MKADistrictName"].Visible = false;
                    this.dgFile.Columns["MKAClanId"].Visible = false;
                    this.dgFile.Columns["ClanId"].Visible = false;
                    //Hide Geo Columns
                    this.dgFile.Columns["ProvinceName"].Visible = false;
                    this.dgFile.Columns["DistrictName"].Visible = false;
                    this.dgFile.Columns["LLGName"].Visible = false;
                    this.dgFile.Columns["WardName"].Visible = false;
                    this.dgFile.Columns["LocationName"].Visible = false;

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

                    // Display Column
                    this.dgFile.Columns["RowNo"].HeaderText = "No";
                    this.dgFile.Columns["RowNo"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    this.dgFile.Columns["VoterName"].HeaderText = "Voter";
                    this.dgFile.Columns["VoterName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    //this.dgFile.Columns["VoterName"].DisplayIndex = 1;
                    this.dgFile.Columns["ElectorId"].HeaderText = "Elector";
                    this.dgFile.Columns["ElectorId"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    this.dgFile.Columns["Status"].HeaderText = "Status";
                    this.dgFile.Columns["MKALLGName"].HeaderText = "MKA LLG";
                    this.dgFile.Columns["MKALLGName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    this.dgFile.Columns["MKAWardName"].HeaderText = "MKA Ward";
                    this.dgFile.Columns["MKAWardName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    this.dgFile.Columns["MKALocationName"].HeaderText = "MKA Location";
                    this.dgFile.Columns["MKALocationName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    this.dgFile.Columns["MKAClanName"].HeaderText = "MKA Clan";
                    this.dgFile.Columns["MKAClanName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

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
                            this.dgFile.Columns["transferOutProvinceName"].Visible = false;
                            this.dgFile.Columns["transferOutDistrictName"].Visible = false;
                            this.dgFile.Columns["transferOutLLGName"].Visible = false;
                            this.dgFile.Columns["transferOutWardName"].Visible = false;
                            this.dgFile.Columns["transferOutLocationName"].Visible = false;

                            this.dgFile.Columns["transferInMKADistrictName"].Visible = false;
                            this.dgFile.Columns["transferInMKALLGName"].Visible = false;
                            this.dgFile.Columns["transferInMKAWardName"].Visible = false;
                            this.dgFile.Columns["transferInMKALocationName"].Visible = false;

                            this.dgFile.Columns["transferOutMKADistrictName"].HeaderText = "TransferOut MKADistrict Name";
                            this.dgFile.Columns["transferOutMKALLGName"].HeaderText = "TransferOut MKALLG Name";
                            this.dgFile.Columns["transferOutMKAWardName"].HeaderText = "TransferOut MKAWard Name";
                            this.dgFile.Columns["transferOutMKALocationName"].HeaderText = "TransferOut MKALocation Name";
                        }
                        else
                        {
                            this.dgFile.Columns["transferOutProvinceName"].Visible = false;
                            this.dgFile.Columns["transferOutDistrictName"].Visible = false;
                            this.dgFile.Columns["transferOutLLGName"].Visible = false;
                            this.dgFile.Columns["transferOutWardName"].Visible = false;
                            this.dgFile.Columns["transferOutLocationName"].Visible = false;
                            this.dgFile.Columns["transferOutMKADistrictName"].Visible = false;
                            this.dgFile.Columns["transferOutMKALLGName"].Visible = false;
                            this.dgFile.Columns["transferOutMKAWardName"].Visible = false;
                            this.dgFile.Columns["transferOutMKALocationName"].Visible = false;

                            this.dgFile.Columns["transferInMKADistrictName"].Visible = false;
                            this.dgFile.Columns["transferInMKALLGName"].Visible = false;
                            this.dgFile.Columns["transferInMKAWardName"].Visible = false;
                            this.dgFile.Columns["transferInMKALocationName"].Visible = false;

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

                        this.dgFile.Columns["transferInMKADistrictName"].Visible = false;
                        this.dgFile.Columns["transferInMKALLGName"].Visible = false;
                        this.dgFile.Columns["transferInMKAWardName"].Visible = false;
                        this.dgFile.Columns["transferInMKALocationName"].Visible = false;


                        this.dgFile.Columns["transferOutMKADistrictName"].Visible = false;
                        this.dgFile.Columns["transferOutMKALLGName"].Visible = false;
                        this.dgFile.Columns["transferOutMKAWardName"].Visible = false;
                        this.dgFile.Columns["transferOutMKALocationName"].Visible = false;
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
             

                    this.dgFile.Columns["ClanId"].Visible = false;

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


                    this.dgFile.Columns["transferInMKADistrictName"].Visible = false;
                    this.dgFile.Columns["transferInMKALLGName"].Visible = false;
                    this.dgFile.Columns["transferInMKAWardName"].Visible = false;
                    this.dgFile.Columns["transferInMKALocationName"].Visible = false;


                    this.dgFile.Columns["transferOutMKADistrictName"].Visible = false;
                    this.dgFile.Columns["transferOutMKALLGName"].Visible = false;
                    this.dgFile.Columns["transferOutMKAWardName"].Visible = false;
                    this.dgFile.Columns["transferOutMKALocationName"].Visible = false;

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
                    (GlobalsModel.LoggedUser.User.Role == 2 && this.pageName == "DataMigration" && (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Staging || GlobalsModel.LoggedUser.Database == CommonEnum.Database.Preliminary) && cmbStatus.Text == "OLD"))
                {
                    if (((this.pageName == "DataMigration" || this.pageName == "TransferOut" || this.pageName == "Duplicate" || this.pageName == "TransferInApproval") &&
                        ((GlobalsModel.LoggedUser.User.Role == 2 && GlobalsModel.LoggedUser.Database == CommonEnum.Database.Staging) ||
                        (GlobalsModel.LoggedUser.User.Role == 3 && GlobalsModel.LoggedUser.Database == CommonEnum.Database.Preliminary))) || this.pageName == "MKAWardRollBook")
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
                        };

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
                if (this.pageName == "MKAFile" || this.pageName == "DataMigration" || this.pageName == "TransferOut" || this.pageName == "Duplicate" || this.pageName == "TransferInApproval")
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
                        }
                    }
                }
                #endregion

                if ((GlobalsModel.LoggedUser.User.Role == 7 && this.pageName == "DataMigration" && GlobalsModel.LoggedUser.Database == CommonEnum.Database.Staging) &&
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
                    //DB - Enable Edit button for Admin or EM user
                    if ((GlobalsModel.LoggedUser.User.Role == 0 || GlobalsModel.LoggedUser.User.Role == 2) && this.pageName == "DataMigration" && (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Staging ||
                        GlobalsModel.LoggedUser.Database == CommonEnum.Database.Preliminary))
                    {
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
                            //if (GlobalsModel.LoggedUser.User.Role == 9 && this.pageName == "DataMigration")
                            //{
                            dgFile.Columns.Insert(dgFile.ColumnCount, editButton);
                            //}
                            //else
                            //{
                            //    dgFile.Columns.Insert(dgFile.ColumnCount - 1, editButton);
                            //}
                        }

                        //Add Not a MKA button for MKA Screen
                        DataGridViewButtonColumn notMKAButton = new DataGridViewButtonColumn();

                        notMKAButton.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        notMKAButton.FlatStyle = FlatStyle.Standard;
                        notMKAButton.HeaderText = "Not MKA";
                        notMKAButton.Name = "NotMKA";
                        notMKAButton.Text = "Not MKA";
                        notMKAButton.UseColumnTextForButtonValue = true;
                        notMKAButton.Width = 70;
                        //if (dgFile.Columns["Not a MKA"] == null)
                        //{
                        dgFile.Columns.Insert(dgFile.ColumnCount, notMKAButton);
                        //}
                    }
                    btnTransfer.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Resources.ResourceManager.GetString("ErrorMessage"), "MKA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorLog.Write("File", "BindVoterGird", ex);
            }
        }
        #endregion

        #region Bind MKA Clan
        /// <summary>
        /// Bind Clan Dropdown based on the MKA Ward selected
        /// </summary>
        /// <param name="WardId"></param>
        private void BindMKAClan(int WardId)
        {
            List<API.Domain.MKAGeoStructure.MKAClanModel> clans = Globals.MKAGeoList.MKAClanList.Where(c => c.WardId == WardId).ToList();
            clans.Insert(0, new API.Domain.MKAGeoStructure.MKAClanModel { ClanId = 0, ClanName = "ALL" });
            cmbClan.DataSource = clans;
            cmbClan.DisplayMember = "ClanName";
            cmbClan.ValueMember = "ClanId";
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
                    dialogResult = MessageBox.Show("Are you sure you want to " + isRequest + " the record?", "MKA", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
                                DialogResult result = MessageBox.Show("Status changed successfully", "MKA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                if (result == DialogResult.OK)
                                {
                                    await GridViewSetting();
                                }
                            }
                            else
                            {
                                this.Cursor = Cursors.Arrow;
                                MessageBox.Show("Something went wrong, try again later", "MKA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            this.Cursor = Cursors.Arrow;

                            MessageBox.Show("At least one NORMAL Record must be selected.", "MKA", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            #region unsed
                            //if (this.pageName == "TransferOut")
                            //{
                            //    MessageBox.Show("At least one NORMAL Record checkbox must be selected.", "MKA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            //}
                            //else
                            //{
                            //    MessageBox.Show("At least one checkbox must be selected.", "MKA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            //}
                            #endregion
                        }
                    }
                    catch (Exception ex)
                    {
                        this.Cursor = Cursors.Arrow;
                        MessageBox.Show(Resources.ResourceManager.GetString("ErrorMessage"), "MKA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ErrorLog.Write("File", "ManageVoterStatus", ex);
                    }
                }
                //else
                //{
                //    MessageBox.Show("Something went wrong, try again later", "MKA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //}

                // Clear all checkbox
                this.ClearCheckbox();
            }
            else
            {
                MessageBox.Show("At least one checkbox must be selected.", "MKA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        private byte[] WardRollBookExportReportSource(string report)
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
                if (MKAGeoTreeStructure.MainTagName == "MKALocation")
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

                    if (MKAGeoTreeStructure.MainTagName == "MKAWard")
                    {
                        wardId = MKAGeoTreeStructure.Id;
                    }
                    else
                    {
                        wardId = MKAGeoTreeStructure.ParentId;
                    }

                    var clans = Globals.MKAGeoList.MKAClanList.Where(c => c.WardId == wardId).Select(x => x.ClanName).OrderBy(name => name).ToList();

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

                    rdlcReport = "MKAWardRollBook.rdlc";
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

                    if (exportModel.rdbLocation.Checked)
                    {
                        rdlcReport = "MKAWardRollBookPreliminaryLocation.rdlc";
                    }
                    else
                    {
                        rdlcReport = "MKAWardRollBookPreliminary.rdlc";
                    }
                }
                else if (report == "Final")
                {
                    if (exportModel.rdbMKAWardRollBook.Checked == true && GlobalsModel.LoggedUser.Database == CommonEnum.Database.Final)
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
                   

                    if (exportModel.rdbLocation.Checked)
                    {
                        rdlcReport = "MKAWardRollBookFinalLocation.rdlc";
                    }
                    else
                    {
                        rdlcReport = "MKAWardRollBookFinal.rdlc";
                    }
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
                param[2] = new ReportParameter("District", dsExportReport.Tables[0].Rows[0]["MKADistrict"].ToString(), true);
                param[3] = new ReportParameter("LLG", dsExportReport.Tables[0].Rows[0]["MKALLG"].ToString(), true);
                param[4] = new ReportParameter("Ward", string.Format("({0}) - {1}", dsExportReport.Tables[0].Rows[0]["MKAWardNo"].ToString(), dsExportReport.Tables[0].Rows[0]["MKAWard"].ToString()), true);
                //param[4] = new ReportParameter("Ward", dsExportReport.Tables[0].Rows[0]["Ward"].ToString(), true);
                if (IsLocation)
                {
                    param[5] = new ReportParameter("Location", dsExportReport.Tables[0].Rows[0]["MKALocation"].ToString(), true);
                }
                else
                {
                    IsLocation = true;
                    var locations = (from DataRow dr in dsExportReport.Tables[0].Rows where dr["MKALocation"] != DBNull.Value select (string)dr["MKALocation"]).Distinct().OrderBy(name => name);
                    //var locations = dsExportReport.Tables[0].AsEnumerable().Where(r => r.Field<string>("MKALocation") != null).Select(x => x.Field<string>("MKALocation")).Distinct().OrderBy(name => name);

                    param[5] = new ReportParameter("Location", string.Join(", ", locations), true);
                }
                param[6] = new ReportParameter("Clan", ClanName, true);
                param[7] = new ReportParameter("Electors", dsExportReport.Tables[0].Rows.Count.ToString(), true);
                param[8] = new ReportParameter("DateTime", DateTime.Now.ToString("dddd, MMMM dd, yyyy"), true);
                param[9] = new ReportParameter("ReportTitle", ReportTitle, true);
                param[10] = new ReportParameter("Version", Version, true);
                param[11] = new ReportParameter("WardNumberWardName", dsExportReport.Tables[0].Rows[0]["MKAWardNumberWardName"].ToString(), true);
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

        #region MKAWardPhotoRollBook Export ReportSource
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

                //dtSubWardPhotoRollBook = dsExportReport.Tables[0].Copy();               
                dtSubWardPhotoRollBook = dsExportReport.Tables[0].AsEnumerable().OrderBy(r => r.Field<string>("MKAClanName")).CopyToDataTable();


                //Add New Column for Serial Number
                DataColumn dc = new DataColumn("SrNo");
                dc.AutoIncrement = true;
                dc.AutoIncrementSeed = 1;
                dc.AutoIncrementStep = 1;
                dc.DataType = typeof(Int32);
                dtSubWardPhotoRollBook.Columns.Add(dc);
                int srno = 1;
                dtSubWardPhotoRollBook.AsEnumerable().ToList().ForEach(r => { r["SrNo"] = srno++; });

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

                string ReportTitle = string.Empty, Version = string.Empty, rdlcReport = string.Empty, GenderSplit = string.Empty, AlphaSplitStart = string.Empty, AlphaSplitEnd = string.Empty, ClanName = string.Empty;
                bool IsLocation = false, IsGenderSplit = false, IsAlphaSplit = false;

                // isLocation
                if (MKAGeoTreeStructure.MainTagName == "MKALocation")
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

                //get Clan Name
                if (Convert.ToInt32(cmbClan.SelectedValue) == 0)
                {
                    int wardId = 0;

                    if (MKAGeoTreeStructure.MainTagName == "MKAWard")
                    {
                        wardId = MKAGeoTreeStructure.Id;
                    }
                    else
                    {
                        wardId = MKAGeoTreeStructure.ParentId;
                    }

                    var clans = Globals.MKAGeoList.MKAClanList.Where(c => c.WardId == wardId).Select(x => x.ClanName).OrderBy(name => name).ToList();

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

                if (report == "Staging")
                {
                    if (!string.IsNullOrWhiteSpace(exportModel.txtReportTitle.Text.Trim()))
                    {
                        ReportTitle = exportModel.txtReportTitle.Text.Trim();
                    }
                    else
                    {
                        //ReportTitle = $"WORKING ROLL {DateTime.Now.ToString("yyyy")}";
                        ReportTitle = $"WORKING ROLL {dsExportReport.Tables[0].Rows[0]["MKADistrict"].ToString().ToUpper()} ASSEMBLY ELECTION {DateTime.Now.ToString("yyyy")}";
                    }

                    Version = "V1";

                    rdlcReport = "MKAWardPhotoRollBook.rdlc";
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

                    rdlcReport = "MKAWardPhotoRollBookPreliminary.rdlc";
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
                            //ReportTitle = $"ELECTORAL ROLL {DateTime.Now.ToString("yyyy")}";
                            ReportTitle = $"ELECTORAL ROLL {dsExportReport.Tables[0].Rows[0]["MKADistrict"].ToString().ToUpper()} ASSEMBLY ELECTION {DateTime.Now.ToString("yyyy")}";
                        }
                    }

                    //ReportTitle = "ELECTORAL ROLL 2022";
                    Version = "version 1";

                    rdlcReport = "MKAWardPhotoRollBookFinal.rdlc";
                }

                Warning[] warnings;
                string[] streamIds;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;

                int paramCount = 19;
                if (report == "Final")
                {
                    paramCount = 21;
                }

                //This is optional if you have parameter then you can add parameters as much as you want
                ReportParameter[] param = new ReportParameter[paramCount];
                param[0] = new ReportParameter("Region", dsExportReport.Tables[0].Rows[0]["Region"].ToString(), true);
                param[1] = new ReportParameter("Province", dsExportReport.Tables[0].Rows[0]["Province"].ToString(), true);
                param[2] = new ReportParameter("District", dsExportReport.Tables[0].Rows[0]["MKADistrict"].ToString(), true);
                param[3] = new ReportParameter("LLG", dsExportReport.Tables[0].Rows[0]["MKALLG"].ToString(), true);
                param[4] = new ReportParameter("Ward", string.Format("({0}) - {1}", dsExportReport.Tables[0].Rows[0]["MKAWardNo"].ToString(), dsExportReport.Tables[0].Rows[0]["MKAWard"].ToString()), true);
                if (IsLocation)
                {
                    param[5] = new ReportParameter("Location", dsExportReport.Tables[0].Rows[0]["MKALocation"].ToString(), true);
                }
                else
                {
                    IsLocation = true;
                    var locations = (from DataRow dr in dsExportReport.Tables[0].Rows select (string)dr["MKALocation"]).Distinct().OrderBy(name => name);
                    param[5] = new ReportParameter("Location", string.Join(", ", locations), true);
                }
                param[6] = new ReportParameter("Clan", ClanName, true);
                param[7] = new ReportParameter("Electors", dsExportReport.Tables[0].Rows.Count.ToString(), true);
                param[8] = new ReportParameter("DateTime", DateTime.Now.ToString("dddd, MMMM dd, yyyy"), true);
                param[9] = new ReportParameter("ReportTitle", ReportTitle, true);
                param[10] = new ReportParameter("Version", Version, true);
                param[11] = new ReportParameter("WardNumberWardName", dsExportReport.Tables[0].Rows[0]["MKAWardNumberWardName"].ToString(), true);
                param[12] = new ReportParameter("IsDeletionReport", Convert.ToString(exportModel.chkIncludePossibleDeletionReport.Checked), true);
                param[13] = new ReportParameter("IsLocation", Convert.ToString(IsLocation), true);
                param[14] = new ReportParameter("IsGenderSplit", Convert.ToString(IsGenderSplit), true);
                param[15] = new ReportParameter("GenderSplit", GenderSplit, true);
                param[16] = new ReportParameter("IsAlphaSplit", Convert.ToString(IsAlphaSplit), true);
                param[17] = new ReportParameter("AlphaSplitStart", AlphaSplitStart, true);
                param[18] = new ReportParameter("AlphaSplitEnd", AlphaSplitEnd, true);
                if (report == "Final")
                {
                    param[19] = new ReportParameter("GeoNameAndYear", "Electorate Election 2023 ", true);
                    param[20] = new ReportParameter("FooterDate", DateTime.Now.ToString("dd-MMM-yyyy"), true);
                }

                ReportDataSource dataSource = new ReportDataSource("ds_MKAWardPhotoRollBook", dsExportReport.Tables[0].Copy());

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
                tblWardRollDeletion.Clear();
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
                tblWardRollDeletionTrnsOutStatus.Clear();
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
                tblWardRollDeletionTrnsOutForm11.Clear();
                #endregion

                #endregion

                if (dtWardRollFrontCover == null)
                {
                    dtWardRollFrontCover = new DataTable("tblWardRollFrontCover");
                }
                dtWardRollFrontCover = new DataTable("tblWardRollFrontCover");
                ReportDataSource datasourceWardRollFrontCover = new ReportDataSource("ds_MKAWardPhotoRollFrontCover", dtWardRollFrontCover.Copy());

                ReportDataSource dataSourceWardRollDeletionReport = new ReportDataSource("ds_MKAWardPhotoRollDeletion", tblWardRollDeletion);
                ReportDataSource datasourceWardRollDeletionTrnsOutStatus = new ReportDataSource("ds_MKAWardPhotoRollDeletionTrnsOutStatus", tblWardRollDeletionTrnsOutStatus);
                ReportDataSource datasourceWardRollDeletionTrnsOutForm11 = new ReportDataSource("ds_MKAWardPhotoRollDeletionTrnsOutForm11", tblWardRollDeletionTrnsOutForm11);

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
                ReportDataSource datasource = new ReportDataSource("ds_SubMKAWardPhotoRollBook", tblSubRCardFiltered);
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

        #region MKA Voter Id Card Export Report Source
        /// <summary>
        /// Voters the identifier card export report source.
        /// </summary>
        /// <returns></returns>
        private byte[] VoterIdCardExportReportSource()
        {
            byte[] bytes = null;
            try
            {
                if (dsVoterCard.Tables[0].Rows != null && dsVoterCard.Tables[0].Rows.Count > 0)
                {
                    Warning[] warnings;
                    string[] streamIds;
                    string mimeType = string.Empty;
                    string encoding = string.Empty;
                    string extension = string.Empty;

                    dsVoterCard.Tables[0].TableName = "tblVoterIdCard";
                    ReportDataSource dataSource = new ReportDataSource("ds_MKAVoterIdCard", dsVoterCard.Tables[0].Copy());
                    ReportViewer viewer = new ReportViewer();
                    viewer.LocalReport.Refresh();

                    viewer.LocalReport.ReportPath = "Report/MKAVoterIdCardNew.rdlc";
                    viewer.LocalReport.DataSources.Add(dataSource);
                    viewer.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SetVoterIdCardSubDataSource);
                    bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
                }
            }
            catch (Exception ex)
            {
                throw;
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

            if (MKAGeoTreeStructure.MainTagName == "Ward")
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

            param[11] = new ReportParameter("ReportTitle", $"MKA - ELECTORAL ROLL UPDATE {DateTime.Now.ToString("yyyy")}", true);

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

                if (MKAGeoTreeStructure.MainTagName == "Location")
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

        //        if (MKAGeoTreeStructure.MainTagName == "Location")
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

            if (MKAGeoTreeStructure.MainTagName == "Location")
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

                if (MKAGeoTreeStructure.MainTagName == "Location")
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
                ReportDataSource datasource = new ReportDataSource("ds_SubMKAVoterIdCard", tblSubRCardFiltered);
                e.DataSources.Add(datasource);
            }
        }
        #endregion

        #region MKA Voter Photo Card Export Report Source
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
                ReportDataSource dataSource = new ReportDataSource("ds_MKAVoterPhotoCard", dsVoterCard.Tables[0].Copy());
                ReportViewer viewer = new ReportViewer();
                viewer.LocalReport.Refresh();
                viewer.LocalReport.ReportPath = "Report/MKAVoterPhotoCard.rdlc";
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
                ReportDataSource datasource = new ReportDataSource("ds_SubMKAVoterPhotoCard", tblSubRCardFiltered);
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
            pageList.ClanId = Convert.ToInt32(cmbClan.SelectedValue);
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

            if (exportModel.rdbAlphaSplit.Checked == true && exportModel.rdbMKAWardRollBook.Checked == true)
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
            pageList.GeoName = MKAGeoTreeStructure.MainTagName;
            pageList.GeoId = MKAGeoTreeStructure.Id;

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
                //Response<WardRollBookReortModel> response = await GenericHttpClient.PostRequest<WardRollBookReortModel>("File/GetExportRecordsList", pageList);
                Response<WardRollBookReortModel> response = await GenericHttpClient.PostRequest<WardRollBookReortModel>("File/GetMKAExportRecordsList", pageList);
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
            pageList.GeoName = MKAGeoTreeStructure.MainTagName;
            pageList.GeoId = MKAGeoTreeStructure.Id;

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

                if (report == "Staging")
                {
                    param[1] = new ReportParameter("SubTitle", "From Working Roll to Preliminary Roll", true);
                }
                else if (report == "Preliminary")
                {
                    param[1] = new ReportParameter("SubTitle", "From Preliminary Roll to Final Roll", true);
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
            pageList.GeoName = MKAGeoTreeStructure.MainTagName;
            pageList.GeoId = MKAGeoTreeStructure.Id;

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
                Response<WardRollBookReortModel> response = await GenericHttpClient.PostRequest<WardRollBookReortModel>("File/GetMKAExportRecordsList", pageList);
                //Response<WardRollBookReortModel> response = await GenericHttpClient.PostRequest<WardRollBookReortModel>("File/GetExportRecordsList", pageList);
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
            pageList.ClanId = Convert.ToInt32(cmbClan.SelectedValue);

            if (exportModel.rdbMKAWardRollBook.Checked == true)
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
            pageList.GeoName = MKAGeoTreeStructure.MainTagName;
            pageList.GeoId = MKAGeoTreeStructure.Id;

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
            pageList.ClanId = Convert.ToInt32(cmbClan.SelectedValue);
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
            pageList.GeoName = MKAGeoTreeStructure.MainTagName;
            pageList.GeoId = MKAGeoTreeStructure.Id;

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

                pageList.ClanId = Convert.ToInt32(cmbClan.SelectedValue);

                // TreeView Filed
                pageList.GeoName = MKAGeoTreeStructure.MainTagName;
                pageList.GeoId = MKAGeoTreeStructure.Id;

                // Login User
                pageList.LoggedUserId = GlobalsModel.LoggedUser.User.UserId;

                // Page No
                pageList.PageNo = 1;

                // Page Size
                pageList.PageSize = 1000;
                #endregion

                if (pageList != null)
                {
                    //Response<WardRollBookReortModel> response = await GenericHttpClient.PostRequest<WardRollBookReortModel>("File/GetExportRecordsList", pageList);
                    Response<WardRollBookReortModel> response = await GenericHttpClient.PostRequest<WardRollBookReortModel>("File/GetMKAExportRecordsList", pageList);

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
                //MessageBox.Show(Resources.ErrorMessage, "MKA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ErrorLog.Write("MKAFile", "GetWardRollStatusCountExportCsvData", ex);
            }
            finally
            {
                lstWardRoll.Clear();
            }
        }
        #endregion

        #region GetMKAWardClanSectionSummary
        /// <summary>
        /// Gets the mka ward clan section summary.
        /// </summary>
        private async Task GetMKAWardClanSectionSummary()
        {
            #region pageList
            PageList pageList = new PageList();
            // Current Database Filed 
            pageList.DatabaseName = GlobalsModel.LoggedUser.Database.ToString();

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


            // TreeView Filed
            pageList.GeoName = MKAGeoTreeStructure.MainTagName;
            pageList.GeoId = MKAGeoTreeStructure.Id;
            #endregion

            var lstWardClanSectionSummary = new List<GetMKAWardClanSectionSummaryModel>();

            Response<GetMKAWardClanSectionSummaryModel> response = await GenericHttpClient.PostRequest<GetMKAWardClanSectionSummaryModel>("File/GetMKAWardClanSectionSummaryList", pageList);
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

        #endregion

        #endregion
    }
}