using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace PNGEC.Desktop.Report
{
    public partial class RegistrationCardReport : Form
    {
        #region variable declaration
        /// <summary>
        /// The ds
        /// </summary>
        private DataSet ds = null;
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationCardReport"/> class.
        /// </summary>
        /// <param name="ds">The ds.</param>
        public RegistrationCardReport(DataSet ds)
        {
            InitializeComponent();
            this.ds = ds;
        }
        #endregion

        #region Events
        /// <summary>
        /// Handles the Load event of the RegistrationCardReport control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void RegistrationCardReport_Load(object sender, EventArgs e)
        {
            this.BindRegistrationCardReport();
            this.rvRegistrationCard.RefreshReport();
        }
        #endregion

        #region Methods

        #region Bind Registration Card Report
        /// <summary>
        /// Binds the registration card report.
        /// </summary>
        private void BindRegistrationCardReport()
        {
            ds.Tables[0].TableName = "tblRegistrationCard";
            ReportDataSource datasource = new ReportDataSource("ds_RegistrationCard", ds.Tables[0]);
            this.rvRegistrationCard.LocalReport.DataSources.Clear();
            this.rvRegistrationCard.LocalReport.DataSources.Add(datasource);
            this.rvRegistrationCard.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SetSubDataSource);
        }
        #endregion

        #region Set Sub DataSource
        /// <summary>
        /// Sets the sub data source.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="SubreportProcessingEventArgs"/> instance containing the event data.</param>
        private void SetSubDataSource(object sender, SubreportProcessingEventArgs e)
        {
            string RegistrationCardNo = Convert.ToString(e.Parameters[0].Values[0]);

            if (!string.IsNullOrWhiteSpace(RegistrationCardNo))
            {
                DataTable tblSubRCardFiltered = ds.Tables[1].AsEnumerable().Where(row => row.Field<String>("RegistrationCardNo") == RegistrationCardNo).CopyToDataTable();
                tblSubRCardFiltered.TableName = "tblSubRegistrationCard";
                ReportDataSource datasource = new ReportDataSource("ds_SubRegistrationCard", tblSubRCardFiltered);
                e.DataSources.Add(datasource);
            }
        }
        #endregion

        #endregion
    }
}