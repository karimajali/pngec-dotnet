using Microsoft.Reporting.WinForms;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Domain.Writ;
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
using PNGEC.Desktop.Report;

namespace PNGEC.Desktop.Forms
{
    public partial class WritList : Form
    {
        #region Variable declaration

        /// <summary>
        /// The Election Type array --  "BY-ELECTION" == 1, "NATIONAL" == 2, "PROVINCIAL" == 3
        /// </summary>
        private string[] ElectionTypeArray = { "ALL", "BY-ELECTION", "NATIONAL", "PROVINCIAL" };

        /// <summary>
        /// writList
        /// </summary>
        private List<WritModel> lstWrit = new List<WritModel>();

        /// <summary>
        /// The ds ElectionWrit
        /// </summary>
        private DataSet dsElectionWrit;

        /// <summary>
        /// The dt form11, dt SubForm11
        /// </summary>
        private DataTable dtElectionWrit = null, dtSubElectionWrit = null;


        private WritModel model = null;

        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="WritList"/> class.
        /// </summary>
        public WritList()
        {
            InitializeComponent();
        }
        #endregion

        #region Events

        /// <summary>
        /// Handles the Load event of the WritList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void WritList_Load(object sender, EventArgs e)
        {
            this.RefreshData();
            this.cmbElectionType.DataSource = ElectionTypeArray;
            this.cmbElectionType.SelectedIndexChanged -= CmbElectionType_SelectedIndexChanged;
            this.cmbElectionType.SelectedIndexChanged += CmbElectionType_SelectedIndexChanged;
        }

        private void CmbElectionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BindWritGrid();
        }

        /// <summary>
        /// Handles the Click event of the btnAddNew control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnAddNew_Click(object sender, EventArgs e)
        {
            Writ Writ = new Writ();
            Writ.AutoScroll = true;
            if (Writ.ShowDialog() == DialogResult.OK)
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

        private async void dgWrit_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string messageBoxText = string.Empty;

            if (e.RowIndex >= 0 && this.dgWrit.Columns[e.ColumnIndex].Name == "view")
            {
                model = new WritModel();
                model.Id = Convert.ToInt32(this.dgWrit.Rows[e.RowIndex].Cells["Id"].Value);
                model.RegionId = Convert.ToInt32(this.dgWrit.Rows[e.RowIndex].Cells["RegionId"].Value);
                model.ProvinceId = Convert.ToInt32(this.dgWrit.Rows[e.RowIndex].Cells["ProvinceId"].Value);
                model.DistrictId = Convert.ToInt32(this.dgWrit.Rows[e.RowIndex].Cells["DistrictId"].Value);
                model.LLGId = Convert.ToInt32(this.dgWrit.Rows[e.RowIndex].Cells["LLGId"].Value);
                model.WardId = Convert.ToInt32(this.dgWrit.Rows[e.RowIndex].Cells["WardId"].Value);
                model.WritName = Convert.ToString(this.dgWrit.Rows[e.RowIndex].Cells["WritName"].Value);
                model.Type = Convert.ToString(this.dgWrit.Rows[e.RowIndex].Cells["Type"].Value);
                model.Scop = Convert.ToString(this.dgWrit.Rows[e.RowIndex].Cells["Scop"].Value);
                model.IsLocked = Convert.ToBoolean(this.dgWrit.Rows[e.RowIndex].Cells["IsLocked"].Value);
                model.NominationStartDate = Convert.ToDateTime(this.dgWrit.Rows[e.RowIndex].Cells["NominationStartDate"].Value);
                model.NominationEndDate = Convert.ToDateTime(this.dgWrit.Rows[e.RowIndex].Cells["NominationEndDate"].Value);
                model.FirstDayOfPolling = Convert.ToDateTime(this.dgWrit.Rows[e.RowIndex].Cells["FirstDayOfPolling"].Value);
                model.LastDayOfPolling = Convert.ToDateTime(this.dgWrit.Rows[e.RowIndex].Cells["LastDayOfPolling"].Value);
                model.ReturnOfWritDate = Convert.ToDateTime(this.dgWrit.Rows[e.RowIndex].Cells["ReturnOfWritDate"].Value);
                model.IssueDate = Convert.ToDateTime(this.dgWrit.Rows[e.RowIndex].Cells["IssueDate"].Value);
                model.IssuerName = Convert.ToString(this.dgWrit.Rows[e.RowIndex].Cells["IssuerName"].Value);
                model.IssuerTitle = Convert.ToString(this.dgWrit.Rows[e.RowIndex].Cells["IssuerTitle"].Value);
                model.CreatedBy = GlobalsModel.LoggedUser.User.UserId;
                model.ModifiedBy = GlobalsModel.LoggedUser.User.UserId;
                model.RegionName = Convert.ToString(this.dgWrit.Rows[e.RowIndex].Cells["RegionName"].Value);
                model.ProvinceName = Convert.ToString(this.dgWrit.Rows[e.RowIndex].Cells["ProvinceName"].Value);
                model.DistrictName = Convert.ToString(this.dgWrit.Rows[e.RowIndex].Cells["DistrictName"].Value);
                model.LLGName = Convert.ToString(this.dgWrit.Rows[e.RowIndex].Cells["LLGName"].Value);
                model.WardName = Convert.ToString(this.dgWrit.Rows[e.RowIndex].Cells["WardName"].Value);


                if (model != null)
                {
                    Writ Writ = new Writ(model);
                    if (Writ.ShowDialog() == DialogResult.OK)
                    {
                        this.RefreshData();
                    }
                }
            }
            else if (e.RowIndex >= 0 && dgWrit.Columns[e.ColumnIndex].Name == "print")
            {
                try
                {

                    model = new WritModel();

                    if (Convert.ToString(this.dgWrit.Rows[e.RowIndex].Cells["Type"].Value).ToUpper() == "BY-ELECTION")
                    {
                        model.Id = Convert.ToInt32(this.dgWrit.Rows[e.RowIndex].Cells["Id"].Value);
                        model.RegionId = Convert.ToInt32(this.dgWrit.Rows[e.RowIndex].Cells["RegionId"].Value);
                        model.ProvinceId = Convert.ToInt32(this.dgWrit.Rows[e.RowIndex].Cells["ProvinceId"].Value);
                        model.DistrictId = Convert.ToInt32(this.dgWrit.Rows[e.RowIndex].Cells["DistrictId"].Value);
                        model.LLGId = Convert.ToInt32(this.dgWrit.Rows[e.RowIndex].Cells["LLGId"].Value);
                        model.WardId = Convert.ToInt32(this.dgWrit.Rows[e.RowIndex].Cells["WardId"].Value);
                        model.RegionName = Convert.ToString(this.dgWrit.Rows[e.RowIndex].Cells["RegionName"].Value);
                        model.ProvinceName = Convert.ToString(this.dgWrit.Rows[e.RowIndex].Cells["ProvinceName"].Value);
                        model.DistrictName = Convert.ToString(this.dgWrit.Rows[e.RowIndex].Cells["DistrictName"].Value);
                        model.LLGName = Convert.ToString(this.dgWrit.Rows[e.RowIndex].Cells["LLGName"].Value);
                        model.WardName = Convert.ToString(this.dgWrit.Rows[e.RowIndex].Cells["WardName"].Value);
                    }

                    if (model != null)
                    {
                        WritPrintOptions Writ = new WritPrintOptions(model);
                        Writ.FormClosing += Writ_FormClosing;
                        Writ.ShowDialog();
                    }

                    if (model != null && !string.IsNullOrEmpty(model.WardName) && model.WardId > 0) // make sure user select the ward in print option screen
                    {
                        #region Election Writ

                        var saveFileDialog = new SaveFileDialog();
                        saveFileDialog.FileName = GetFileNameStr(e, "Writ").Replace("/", "_");
                        saveFileDialog.DefaultExt = ".pdf";
                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            GlobalsModel.mainMaster.ShowLoader();

                            if (Path.GetExtension(saveFileDialog.FileName) != ".pdf")
                            {
                                MessageBox.Show("Only .pdf format is supported.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                            {

                                PageList model = new PageList();
                                model.ElectionWritId = Convert.ToInt32(dgWrit.Rows[e.RowIndex].Cells["Id"].Value);
                                List<WritRO> lstWritRO = new List<WritRO>();
                                string ROName = string.Empty;
                                int WritROId = 0;
                                var response = await GenericHttpClient.PostRequestCommon<List<WritRO>>("WritRO/GetWritROList", model);
                                if (response.Status)
                                {
                                    lstWritRO = response.Data;

                                    if (lstWritRO.Count == 1) // only one RO == "BY-ELECTION"?
                                    {
                                        ROName = lstWritRO.FirstOrDefault().ROName;
                                        WritROId = lstWritRO.FirstOrDefault().WritROId;
                                    }
                                    else
                                    {                                        
                                        ROName = lstWritRO.Where(x => x.ProvinceId == this.model.ProvinceId).First().ROName;
                                        WritROId = lstWritRO.Where(x => x.ProvinceId == this.model.ProvinceId).First().WritROId;
                                    }
                                }
                                // string NominationStartDate, string ,string , string ,string  , string )

                                byte[] bytes = null;
                                try
                                {

                                    bytes = await WritReportSource(
                                    Convert.ToInt32(dgWrit.Rows[e.RowIndex].Cells["RegionId"].Value),
                                    Convert.ToInt32(dgWrit.Rows[e.RowIndex].Cells["ProvinceId"].Value),
                                    Convert.ToInt32(dgWrit.Rows[e.RowIndex].Cells["DistrictId"].Value),
                                    Convert.ToInt32(dgWrit.Rows[e.RowIndex].Cells["LLGId"].Value),
                                    Convert.ToInt32(dgWrit.Rows[e.RowIndex].Cells["WardId"].Value),
                                    Convert.ToString(dgWrit.Rows[e.RowIndex].Cells["Batch"].Value), ROName, WritROId,
                                    Convert.ToString(dgWrit.Rows[e.RowIndex].Cells["NominationStartDate"].Value),
                                    Convert.ToString(dgWrit.Rows[e.RowIndex].Cells["NominationEndDate"].Value),
                                    Convert.ToString(dgWrit.Rows[e.RowIndex].Cells["FirstDayOfPolling"].Value),
                                    Convert.ToString(dgWrit.Rows[e.RowIndex].Cells["LastDayOfPolling"].Value),
                                    Convert.ToString(dgWrit.Rows[e.RowIndex].Cells["ReturnOfWritDate"].Value),
                                    Convert.ToString(dgWrit.Rows[e.RowIndex].Cells["IssueDate"].Value),
                                    Convert.ToString(dgWrit.Rows[e.RowIndex].Cells["IssuerName"].Value),
                                     Convert.ToString(dgWrit.Rows[e.RowIndex].Cells["IssuerTitle"].Value));
                                }
                                catch (Exception ex)
                                {

                                }


                                if (bytes != null)
                                {
                                    using (FileStream stream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                                    {
                                        stream.Write(bytes, 0, bytes.Length);
                                    }

                                    messageBoxText = string.Format("Form 11 is saved at {0}", saveFileDialog.FileName);
                                    MessageBox.Show(messageBoxText, "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    //MessageBox.Show("Form11 generated successfully ", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show("Error in saving the PDF file(s).", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                        }
                        else
                        {
                            /*Can not delete records on print button so below line comment */
                            //this.DeleteEnrolmentForm(Convert.ToInt32(dgEnrolmentForms.Rows[e.RowIndex].Cells["EnrolmentFormId"].Value));
                        }

                        #endregion
                    }
                }
                catch (Exception ex)
                {
                    ErrorLog.Write("EnrolmentForm", "dgEnrolmentForms_CellContentClick - Print", ex);
                    MessageBox.Show("Something went wrong", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (dtElectionWrit != null)
                    {
                        dtElectionWrit.Clear();
                    }

                    if (dtSubElectionWrit != null)
                    {
                        dtSubElectionWrit.Clear();
                    }

                    if (dsElectionWrit != null)
                    {
                        dsElectionWrit.Clear();
                    }

                    GlobalsModel.mainMaster.HideLoader();
                }
            }
        }

        private void Writ_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.model = ((WritPrintOptions)sender).writModel;
        }


        /// <summary>
        /// Handles the Click event of the btnSearch control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.BindWritGrid();
        }


        #endregion

        #region Methods

        #region Get File Name Str
        /// <summary>
        /// Gets the file name string.
        /// </summary>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
        /// <param name="appendStr">The append string.</param>
        /// <returns></returns>
        private string GetFileNameStr(DataGridViewCellEventArgs e, string appendStr)
        {
            //var province = Convert.ToString(dgWrit.Rows[e.RowIndex].Cells["ProvinceName"].Value);
            //var district = Convert.ToString(dgWrit.Rows[e.RowIndex].Cells["DistrictName"].Value);
            //var llg = Convert.ToString(dgWrit.Rows[e.RowIndex].Cells["LLGName"].Value);
            //var ward = Convert.ToString(dgWrit.Rows[e.RowIndex].Cells["WardName"].Value);


            var province = model.ProvinceName;
            var district = model.DistrictName;
            var llg = model.LLGName;
            var ward = model.WardName;

            var batchName = Convert.ToString(dgWrit.Rows[e.RowIndex].Cells["Batch"].Value);
            var str = string.Empty;

            if (!string.IsNullOrEmpty(district))
            {
                str = province + "_" + district;
            }
            if (!string.IsNullOrEmpty(llg))
            {
                str += "_" + llg;
            }
            if (!string.IsNullOrEmpty(ward))
            {
                str += "_" + ward;
            }
            if (!string.IsNullOrEmpty(batchName))
            {
                str += "_" + batchName;
            }
            return str + "_" + appendStr;
        }
        #endregion

        #region EnrolmentFormsExportReportSource
        /// <summary>
        /// Enrolments the forms export report source.
        /// </summary>
        /// <param name="startRange">The start range.</param>
        /// <param name="endRange">The end range.</param>
        /// <param name="provinceId">The province identifier.</param>
        /// <param name="wardId">The ward identifier.</param>
        /// <param name="regionId">The region identifier.</param>
        /// <param name="districtId">The district identifier.</param>
        /// <param name="llgId">The LLG identifier.</param>
        /// <param name="batchName">Name of the batch.</param>
        /// <returns></returns>
        private async Task<byte[]> WritReportSource(int regionId, int provinceId, int districtId, int llgId, int wardId, string batchName, string ROName, int wWritROId,
           string NominationStartDate, string NominationEndDate, string FirstDayOfPolling, string LastDayOfPolling, string ReturnOfWritDate, string IssueDate,string IssuerName, string IssuerTitle)
        {
            byte[] bytes = null;

            await Task.Delay(10);

            var numberOfForms = 1; // count number of Writes need to generate
            string[] PDFfilepaths = new string[numberOfForms + 1];
            //string FormNo, firstFormNo = "", lastFormNo = "";

            //var RegionName = Globals.GeoList.RegionList.Where(x => x.RegionId == regionId).Select(y => y.RegionName).FirstOrDefault();
            //var ProvinceName = Globals.GeoList.ProvinceList.Where(x => x.ProvinceId == provinceId).Select(y => y.ProvinceName).FirstOrDefault();
            //var DistrictName = Globals.GeoList.DistrictList.Where(x => x.DistrictId == districtId).Select(y => y.DistrictName).FirstOrDefault();
            //var LLGName = Globals.GeoList.LLGList.Where(x => x.LLGId == llgId).Select(y => y.LLGName).FirstOrDefault();
            //var WardName = Globals.GeoList.WardList.Where(x => x.WardId == wardId).Select(y => y.WardName).FirstOrDefault();
            //var WardNumber = Globals.GeoList.WardList.Where(x => x.WardId == wardId).Select(y => y.WardNumber).FirstOrDefault();


            var RegionName = Globals.GeoList.RegionList.Where(x => x.RegionId == model.RegionId).Select(y => y.RegionName).FirstOrDefault();
            var ProvinceName = Globals.GeoList.ProvinceList.Where(x => x.ProvinceId == model.ProvinceId).Select(y => y.ProvinceName).FirstOrDefault();
            var DistrictName = Globals.GeoList.DistrictList.Where(x => x.DistrictId == model.DistrictId).Select(y => y.DistrictName).FirstOrDefault();
            var LLGName = Globals.GeoList.LLGList.Where(x => x.LLGId == model.LLGId).Select(y => y.LLGName).FirstOrDefault();
            var WardName = Globals.GeoList.WardList.Where(x => x.WardId == model.WardId).Select(y => y.WardName).FirstOrDefault();
            var WardNumber = Globals.GeoList.WardList.Where(x => x.WardId == model.WardId).Select(y => y.WardNumber).FirstOrDefault();


            dtElectionWrit = new DataTable("tblSubWritRo");
            dtElectionWrit.Clear();
            dtElectionWrit.Columns.Add("WritROId", typeof(Int32));
            dtElectionWrit.Columns.Add("ElectionWritId", typeof(Int32));
            dtElectionWrit.Columns.Add("ProvinceId", typeof(Int32));
            dtElectionWrit.Columns.Add("ROName");
            dtElectionWrit.Columns.Add("drElectionWrit");
            dtElectionWrit.Columns.Add("WritName");
            dtElectionWrit.Columns.Add("Type");
            dtElectionWrit.Columns.Add("Scop");
            dtElectionWrit.Columns.Add("NominationStartDate");
            dtElectionWrit.Columns.Add("NominationEndDate");
            dtElectionWrit.Columns.Add("FirstDayOfPolling");
            dtElectionWrit.Columns.Add("LastDayOfPolling");
            dtElectionWrit.Columns.Add("ReturnOfWritDate");
            dtElectionWrit.Columns.Add("IssueDate");
            dtElectionWrit.Columns.Add("IssuerName");
            dtElectionWrit.Columns.Add("IssuerTitle");
            dtElectionWrit.Columns.Add("Batch");
            dtElectionWrit.Columns.Add("WardName");
            dtElectionWrit.Columns.Add("WardNoWardName");
            dtElectionWrit.Columns.Add("ProvinceName");
            dtElectionWrit.Columns.Add("DistrictName");
            dtElectionWrit.Columns.Add("LLGName");


            dtSubElectionWrit = new DataTable("tblElectionWrit");
            dtSubElectionWrit.Clear();
            dtSubElectionWrit.Columns.Add("Id", typeof(Int32));
            dtSubElectionWrit.Columns.Add("ROName");
            dtSubElectionWrit.Columns.Add("WritName");
            dtSubElectionWrit.Columns.Add("Type");
            dtSubElectionWrit.Columns.Add("Scop");
            dtSubElectionWrit.Columns.Add("NominationStartDate");
            dtSubElectionWrit.Columns.Add("NominationEndDate");
            dtSubElectionWrit.Columns.Add("FirstDayOfPolling");
            dtSubElectionWrit.Columns.Add("LastDayOfPolling");
            dtSubElectionWrit.Columns.Add("ReturnOfWritDate");
            dtSubElectionWrit.Columns.Add("IssueDate");
            dtSubElectionWrit.Columns.Add("IssuerName");
            dtSubElectionWrit.Columns.Add("IssuerTitle");
            dtSubElectionWrit.Columns.Add("Batch");
            dtSubElectionWrit.Columns.Add("WardName");
            dtSubElectionWrit.Columns.Add("WardNoWardName");
            dtSubElectionWrit.Columns.Add("ProvinceName");
            dtSubElectionWrit.Columns.Add("DistrictName");
            dtSubElectionWrit.Columns.Add("LLGName");


            for (int i = 1; i <= numberOfForms; i++)
            {
                DataRow drElectionWrit = dtElectionWrit.NewRow();
                DataRow drdtSubElectionWrit = dtSubElectionWrit.NewRow();

                drElectionWrit["WritROId"] = (i);
                drElectionWrit["drElectionWrit"] = ROName;
                drdtSubElectionWrit["Id"] = (i);


                drElectionWrit["ROName"] = ROName;
                drElectionWrit["NominationStartDate"] = DateTime.Parse(NominationStartDate).ToString("dddd, dd MMM, yyyy") + " (4:00 pm)";
                drElectionWrit["NominationEndDate"] = DateTime.Parse(NominationEndDate).ToString("dddd, dd MMM, yyyy")  + " (4:00 pm)";  
                drElectionWrit["FirstDayOfPolling"] = DateTime.Parse(FirstDayOfPolling).ToString("dddd, dd MMM, yyyy");  
                drElectionWrit["LastDayOfPolling"] = DateTime.Parse(LastDayOfPolling).ToString("dddd, dd MMM, yyyy");  
                drElectionWrit["ReturnOfWritDate"] = DateTime.Parse(ReturnOfWritDate).ToString("dddd, dd MMM, yyyy") + " (4:00 pm)"; 
                drElectionWrit["IssueDate"] = DateTime.Parse(IssueDate).ToString("dddd, dd MMM, yyyy") ;
                drElectionWrit["ProvinceName"] = ProvinceName;
                drElectionWrit["DistrictName"] = DistrictName;
                drElectionWrit["LLGName"] = LLGName;
                drElectionWrit["WardName"] = WardName;
                drElectionWrit["WardNoWardName"] = WardNumber + " - " + WardName;
                drElectionWrit["IssuerName"] = IssuerName;
                drElectionWrit["IssuerTitle"] = IssuerTitle;


                drdtSubElectionWrit["ROName"] = ROName;
                drElectionWrit["NominationStartDate"] = DateTime.Parse(NominationStartDate).ToString("dddd, dd MMM, yyyy") + " (4:00 pm)";
                drElectionWrit["NominationEndDate"] = DateTime.Parse(NominationEndDate).ToString("dddd, dd MMM, yyyy") + " (4:00 pm)";
                drElectionWrit["FirstDayOfPolling"] = DateTime.Parse(FirstDayOfPolling).ToString("dddd, dd MMM, yyyy");
                drElectionWrit["LastDayOfPolling"] = DateTime.Parse(LastDayOfPolling).ToString("dddd, dd MMM, yyyy");
                drElectionWrit["ReturnOfWritDate"] = DateTime.Parse(ReturnOfWritDate).ToString("dddd, dd MMM, yyyy") + " (4:00 pm)";
                drElectionWrit["IssueDate"] = DateTime.Parse(IssueDate).ToString("dddd, dd MMM, yyyy");
                drdtSubElectionWrit["ProvinceName"] = ProvinceName;
                drdtSubElectionWrit["DistrictName"] = DistrictName;
                drdtSubElectionWrit["LLGName"] = LLGName;
                drdtSubElectionWrit["WardName"] = WardName;
                drdtSubElectionWrit["WardNoWardName"] = WardNumber + " - " + WardName;
                drdtSubElectionWrit["IssuerName"] = IssuerName;
                drdtSubElectionWrit["IssuerTitle"] = IssuerTitle;

                dtElectionWrit.Rows.Add(drElectionWrit);
                dtSubElectionWrit.Rows.Add(drdtSubElectionWrit);

            }

            if ((dtElectionWrit != null && dtSubElectionWrit != null) && (dtElectionWrit.Rows.Count > 0 && dtSubElectionWrit.Rows.Count > 0))
            {
                dsElectionWrit = new DataSet();
                dsElectionWrit.Tables.AddRange(new DataTable[] { dtElectionWrit, dtSubElectionWrit });

                if (dsElectionWrit.Tables[0].Rows != null && dsElectionWrit.Tables[0].Rows.Count > 0)
                {
                    Warning[] warnings;
                    string[] streamIds;
                    string mimeType = string.Empty;
                    string encoding = string.Empty;
                    string extension = string.Empty;
                    dsElectionWrit.Tables[0].TableName = "tblSubWritRo";


                    ReportParameter[] param = new ReportParameter[7];
                    param[0] = new ReportParameter("Region", RegionName, true);
                    param[1] = new ReportParameter("Province", ProvinceName, true);
                    param[2] = new ReportParameter("District", DistrictName, true);
                    param[3] = new ReportParameter("LLG", LLGName, true);
                    param[4] = new ReportParameter("Ward", WardName, true);
                    param[5] = new ReportParameter("TYPE", "BY-ELECTION", true);
                    param[6] = new ReportParameter("ROName", ROName, true);


                    ReportDataSource dataSource = new ReportDataSource("ElectionWrit", dsElectionWrit.Tables[0].Copy());
                    ReportViewer viewer = new ReportViewer();
                    viewer.LocalReport.Refresh();
                    viewer.LocalReport.ReportPath = "Report/ElectionWrit.rdlc";
                    viewer.LocalReport.EnableExternalImages = true;
                    viewer.LocalReport.SetParameters(param);
                    viewer.LocalReport.DataSources.Add(dataSource);
                    viewer.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SetEnrolmentFormsSubDataSource);
                    bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
                }
            }

            return bytes;
        }
        #endregion

        #region SetEnrolmentFormsSubDataSource
        /// <summary>
        /// Sets the enrolment forms sub data source.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="SubreportProcessingEventArgs"/> instance containing the event data.</param>
        private void SetEnrolmentFormsSubDataSource(object sender, SubreportProcessingEventArgs e)
        {
            long FormId = Convert.ToInt32(e.Parameters[0].Values[0]);

            if (FormId > 0)
            {
                DataTable tblSubRCardFiltered = dsElectionWrit.Tables[1].AsEnumerable().Where(row => row.Field<int>("Id") == FormId).CopyToDataTable();
                tblSubRCardFiltered.TableName = "tblSubWritRo";
                ReportDataSource datasource = new ReportDataSource("ElectionWritDataSet", tblSubRCardFiltered);
                e.DataSources.Add(datasource);
            }
        }
        #endregion


        /// <summary>
        /// Binds the Writ grid.
        /// </summary>
        private async void BindWritGrid()
        {
            try
            {
                GlobalsModel.mainMaster.ShowLoader();

                PageList model = new PageList();
                model.KeyWord = this.txtSearch.Text;
                lstWrit = new List<WritModel>();
                var response = await GenericHttpClient.PostRequestCommon<List<WritModel>>("Writ/GetWritList", model);
                if (response.Status)
                {
                    lstWrit = response.Data;
                }

                if (cmbElectionType.SelectedIndex > 0)
                {
                    this.dgWrit.DataSource = lstWrit.Where(x => x.Type.ToLower() == this.cmbElectionType.Text.ToLower()).ToList();
                }
                else
                {
                    this.dgWrit.DataSource = lstWrit;
                }

                if (lstWrit != null)
                {
                    this.dgWrit.Columns["Id"].Visible = false;
                    this.dgWrit.Columns["RegionId"].Visible = false;
                    this.dgWrit.Columns["ProvinceId"].Visible = false;
                    this.dgWrit.Columns["DistrictId"].Visible = false;
                    this.dgWrit.Columns["LLGId"].Visible = false;
                    this.dgWrit.Columns["WardId"].Visible = false;
                    this.dgWrit.Columns["NominationStartDate"].Visible = false;
                    this.dgWrit.Columns["NominationEndDate"].Visible = false;
                    this.dgWrit.Columns["FirstDayOfPolling"].Visible = false;
                    this.dgWrit.Columns["LastDayOfPolling"].Visible = false;
                    this.dgWrit.Columns["ReturnOfWritDate"].Visible = false;
                    this.dgWrit.Columns["IssueDate"].Visible = false;
                    this.dgWrit.Columns["IssuerName"].Visible = false;
                    this.dgWrit.Columns["IssuerTitle"].Visible = false;
                    this.dgWrit.Columns["CreatedBy"].Visible = false;
                    this.dgWrit.Columns["ModifiedBy"].Visible = false;
                    this.dgWrit.Columns["ModifiedDate"].Visible = false;
                    this.dgWrit.Columns["ResponseCode"].Visible = false;
                    this.dgWrit.Columns["RegionName"].Visible = false;
                    this.dgWrit.Columns["ProvinceName"].Visible = false;
                    this.dgWrit.Columns["DistrictName"].Visible = false;
                    this.dgWrit.Columns["LLGName"].Visible = false;
                    this.dgWrit.Columns["WardName"].Visible = false;
                    this.dgWrit.Columns["ActionBy"].Visible = false;

                    this.dgWrit.Columns["WritName"].HeaderText = "Election Name";
                    this.dgWrit.Columns["Type"].HeaderText = "Type";
                    this.dgWrit.Columns["Scop"].HeaderText = "Scope";
                    this.dgWrit.Columns["IsLocked"].HeaderText = "Locked?";
                    this.dgWrit.Columns["Batch"].HeaderText = "Batch";
                    this.dgWrit.Columns["CreatedDate"].HeaderText = "Created";

                    #region View Button
                    DataGridViewButtonColumn viewButton = new DataGridViewButtonColumn();
                    viewButton.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    viewButton.FlatStyle = FlatStyle.Standard;
                    viewButton.HeaderText = "Action";
                    viewButton.Name = "view";
                    viewButton.Text = "view";
                    viewButton.UseColumnTextForButtonValue = true;
                    viewButton.Width = 80;
                    if (this.dgWrit.Columns["view"] == null)
                    {
                        this.dgWrit.Columns.Insert(this.dgWrit.ColumnCount, viewButton);
                    }

                    this.dgWrit.Columns["view"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    this.dgWrit.Columns["view"].CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    // col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    #endregion

                    #region Print Button
                    DataGridViewButtonColumn printButton = new DataGridViewButtonColumn();
                    printButton.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    printButton.FlatStyle = FlatStyle.Standard;
                    printButton.HeaderText = "Action";
                    printButton.Name = "print";
                    printButton.Text = "print";
                    printButton.UseColumnTextForButtonValue = true;
                    printButton.Width = 80;
                    if (this.dgWrit.Columns["print"] == null)
                    {
                        this.dgWrit.Columns.Insert(this.dgWrit.ColumnCount, printButton);
                    }

                    this.dgWrit.Columns["print"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    this.dgWrit.Columns["print"].CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    // col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    #endregion

                }

                GlobalsModel.mainMaster.HideLoader();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Resources.ResourceManager.GetString("ErrorMessage"), "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorLog.Write("WritList", "BindWritGrid", ex);
            }

            this.Cursor = Cursors.Arrow;
        }

        /// <summary>
        /// Refresh Data
        /// </summary>
        private void RefreshData()
        {
            this.txtSearch.Text = string.Empty;

            if (cmbElectionType.SelectedIndex > 0)
            {
                cmbElectionType.SelectedIndex = 0;
            }
            else
            {
                this.BindWritGrid();
            }
        }

        #endregion       
    }
}
