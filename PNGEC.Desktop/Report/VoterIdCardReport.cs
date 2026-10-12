using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace PNGEC.Desktop.Report
{
    public partial class VoterIdCardReport : Form
    {
        #region variable
        /// <summary>
        /// The ds
        /// </summary>
        private DataSet ds = null;
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="VoterIdCardReport"/> class.
        /// </summary>
        /// <param name="ds">The ds.</param>
        public VoterIdCardReport(DataSet ds)
        {
            InitializeComponent();
            this.ds = ds;
        }
        #endregion

        #region Events
        /// <summary>
        /// Handles the Load event of the VoterIdCardReport control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void VoterIdCardReport_Load(object sender, EventArgs e)
        {
            this.BindVoterIdCardReport();
            this.rvVoterIdCard.RefreshReport();
        }
        #endregion

        #region Methods

        #region Bind VoterId Card Report
        /// <summary>
        /// Binds the voter identifier card report.
        /// </summary>
        private void BindVoterIdCardReport()
        {
            ds.Tables[0].TableName = "tblVoterIdCard";
            ReportDataSource datasource = new ReportDataSource("ds_VoterIdCard", ds.Tables[0]);
            this.rvVoterIdCard.LocalReport.DataSources.Clear();
            this.rvVoterIdCard.LocalReport.EnableExternalImages = true;
            this.rvVoterIdCard.LocalReport.DataSources.Add(datasource);
            this.rvVoterIdCard.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SetSubDataSource);
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
            long ElectorId = Convert.ToInt64(e.Parameters[0].Values[0]);

            if (ElectorId > 0)
            {
                DataTable tblSubRCardFiltered = ds.Tables[1].AsEnumerable().Where(row => row.Field<int>("ElectorId") == ElectorId).CopyToDataTable();
                tblSubRCardFiltered.TableName = "tblSubVoterIdCard";
                ReportDataSource datasource = new ReportDataSource("ds_SubVoterIdCard", tblSubRCardFiltered);
                e.DataSources.Add(datasource);
            }
        }
        #endregion

        #endregion
    }
}
