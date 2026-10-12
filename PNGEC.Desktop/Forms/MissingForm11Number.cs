using Microsoft.Reporting.WinForms;
using PNGEC.API.Domain.EnrolmentForm;
using PNGEC.API.Helper;
using PNGEC.Desktop.Model;
using PNGEC.Desktop.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PNGEC.Desktop.Forms
{
    public partial class MissingForm11Number : Form
    {
        #region variable declaration
        /// <summary>
        /// The enrolment form
        /// </summary>
        private EnrolmentFormModel enrolmentForm;

        /// <summary>
        /// The LST ward quality check response
        /// </summary>
        private List<WardQualityCheckResponseModel> lstWardQualityCheckResponse;

        /// <summary>
        /// Gets or sets the ward.
        /// </summary>
        /// <value>
        /// The ward.
        /// </value>
        private API.Domain.Ward.WardModel Ward { get; set; }

        /// <summary>
        /// The viewer
        /// </summary>
        private ReportViewer viewer;

        /// <summary>
        /// The geo tree structure
        /// </summary>
        private GeoTreeStructure treeStructure = null;

        /// <summary>
        /// The data table
        /// </summary>
        private DataTable dataTable;

        /// <summary>
        /// The report datatable
        /// </summary>
        private DataTable reportDatatable;

        /// <summary>
        /// The message box text
        /// </summary>
        private string messageBoxText;
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="MissingForm11Number"/> class.
        /// </summary>
        public MissingForm11Number(EnrolmentFormModel enrolmentForm, List<WardQualityCheckResponseModel> lstWardQualityCheckResponse, GeoTreeStructure treeStructure)
        {
            InitializeComponent();
            this.enrolmentForm = enrolmentForm;
            this.lstWardQualityCheckResponse = lstWardQualityCheckResponse;
            this.treeStructure = treeStructure;
        }
        #endregion

        #region Events

        /// <summary>
        /// Handles the Load event of the MissingForm11Number control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void MissingForm11Number_Load(object sender, EventArgs e)
        {
            dataTable = new DataTable();
            dataTable.Clear();
            dataTable.Columns.Add("MissingNumber", typeof(string));

            for (int i = 0; i < lstWardQualityCheckResponse.Count; i++)
            {
                if(lstWardQualityCheckResponse[i].IsUsed == false)
                {
                    dataTable.Rows.Add(new object[] { lstWardQualityCheckResponse[i].FormNumber });
                }
            }

            if(dataTable?.Rows?.Count > 0)
            {
                this.lblTotalNumber.Text = $"Total: {dataTable.Rows.Count}";
                BindGird();
            }
        }

        /// <summary>
        /// Handles the Click event of the btnExport control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnExport_Click(object sender, EventArgs e)
        {
            #region ExisitngVoterDetails
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                string exportReportName = GlobalsModel.reportName + "_Working_" + this.treeStructure.MainTagName + "_UnUsedForm11Batch_" + enrolmentForm.EnrolmentFormId;

                var saveFileDialog = new SaveFileDialog();
                saveFileDialog.FileName = exportReportName;
                saveFileDialog.DefaultExt = ".pdf";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    GlobalsModel.mainMaster.ShowLoader();

                    byte[] bytes = WardQualityCheck();

                    using (FileStream stream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                    {
                        stream.Write(bytes, 0, bytes.Length);
                    }

                    messageBoxText = string.Format("{0} Roll Book is exported sucessfully at {1}", treeStructure.MainTagName, saveFileDialog.FileName);
                    MessageBox.Show(messageBoxText, "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    GlobalsModel.mainMaster.HideLoader();
                }
            }
            else
            {
                MessageBox.Show("No record founds.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            #endregion
        }
        #endregion

        #region Methods

        #region Bind Gird
        /// <summary>
        /// Binds the gird.
        /// </summary>
        private void BindGird()
        {
            try
            {
                if (this.dgMissingFrom11Number.Columns.Count > 0)
                {
                    this.dgMissingFrom11Number.Columns.Clear();
                }

                this.dgMissingFrom11Number.DataSource = null;
                this.dgMissingFrom11Number.DataSource = dataTable;
                this.dgMissingFrom11Number.Columns["MissingNumber"].HeaderText = "Un Used/Missing Form11";
                this.dgMissingFrom11Number.Columns["MissingNumber"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Resources.ResourceManager.GetString("ErrorMessage"), "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorLog.Write("MissingForm11Number", "BindGird", ex);
            }
        }
        #endregion

        #region WardQualityCheck
        /// <summary>
        /// Wards the quality check.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        private byte[] WardQualityCheck()
        {
            byte[] bytes = null;

            if (dataTable.Rows != null && dataTable.Rows.Count > 0)
            {
                #region Report Datatable
                reportDatatable = new DataTable("tblWardQualityCheckReport");
                reportDatatable.Clear();
                reportDatatable.Columns.Add("DataColumn1", typeof(string));
                reportDatatable.Columns.Add("DataColumn2", typeof(string));
                reportDatatable.Columns.Add("DataColumn3", typeof(string));
                reportDatatable.Columns.Add("DataColumn4", typeof(string));
                reportDatatable.Columns.Add("DataColumn5", typeof(string));

                DataRow dr = reportDatatable.NewRow();
                for (int i = 0, j = 0; i < dataTable.Rows.Count; i++, j++)
                {
                    if (j <= 4)
                    {
                        dr[j] = dataTable.Rows[i]["MissingNumber"].ToString();
                    }

                    if(j == 4 || i == (dataTable.Rows.Count -1))
                    {
                        reportDatatable.Rows.Add(dr);
                        dr = reportDatatable.NewRow();
                        j = -1;
                    }
                }
                #endregion

                string rdlcReport = string.Empty;
                this.Ward = Globals.GeoList.WardList.Where(x => x.Status && x.WardId == this.enrolmentForm.WardId).SingleOrDefault();

                Warning[] warnings;
                string[] streamIds;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;

                int paramCount = 9;

                ReportParameter[] param = new ReportParameter[paramCount];
                param[0] = new ReportParameter("ReportTitle", "Un Used Form 11 Batch", true);
                param[1] = new ReportParameter("ProvinceName", this.enrolmentForm.ProvinceName, true);
                param[2] = new ReportParameter("DistrictName", this.enrolmentForm.DistrictName, true);
                param[3] = new ReportParameter("LLGName", this.enrolmentForm.LLGName, true);
                param[4] = new ReportParameter("WardNumberWardName", this.Ward.WardNoWardName, true);
                param[5] = new ReportParameter("BatchName", this.enrolmentForm.BatchName, true);
                param[6] = new ReportParameter("NumberOfGeneratedForm11", Convert.ToString(lstWardQualityCheckResponse.Count), true);
                param[7] = new ReportParameter("NumberOfUsedForm11", Convert.ToString(lstWardQualityCheckResponse.Where(x => x.IsUsed == true).Count()), true);
                param[8] = new ReportParameter("NumberOfUnusedOrMissingForm11", Convert.ToString(lstWardQualityCheckResponse.Where(x => x.IsUsed == false).Count()), true);

                ReportDataSource dataWardQualityCheckReport = new ReportDataSource("ds_WardQualityCheckReport", reportDatatable);

                viewer = new ReportViewer();
                viewer.LocalReport.Refresh();
                viewer.LocalReport.ReportPath = "Report/WardQualityCheckReport.rdlc";
                viewer.LocalReport.SetParameters(param);

                viewer.LocalReport.DataSources.Add(dataWardQualityCheckReport); // Add  datasource here
                bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
            }

            return bytes;
        }
        #endregion

        #endregion
    }
}
