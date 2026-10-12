using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace PNGEC.Desktop.Report
{
    public partial class VoterPhotoCardReport : Form
    {
        #region variable
        /// <summary>
        /// The ds
        /// </summary>
        private DataSet ds = null;
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="VoterPhotoCardReport"/> class.
        /// </summary>
        /// <param name="ds">The ds.</param>
        public VoterPhotoCardReport(DataSet ds)
        {
            InitializeComponent();
            this.ds = ds;
        }
        #endregion

        #region Events
        /// <summary>
        /// Handles the Load event of the VoterPhotoCardReport control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void VoterPhotoCardReport_Load(object sender, EventArgs e)
        {
            this.BindVoterPhotoCardReport();
            this.rvVoterPhotoCard.RefreshReport();
        }
        #endregion

        #region Methods

        #region Bind Voter Photo Card Report
        /// <summary>
        /// Binds the voter photo card report.
        /// </summary>
        private void BindVoterPhotoCardReport()
        {
            ds.Tables[0].TableName = "tblVoterPhotoCard";
            ReportDataSource datasource = new ReportDataSource("ds_VoterPhotoCard", ds.Tables[0]);
            this.rvVoterPhotoCard.LocalReport.DataSources.Clear();
            this.rvVoterPhotoCard.LocalReport.EnableExternalImages = true;
            this.rvVoterPhotoCard.LocalReport.DataSources.Add(datasource);
            this.rvVoterPhotoCard.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SetSubDataSource);
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
                tblSubRCardFiltered.TableName = "tblSubVoterPhotoCard";
                ReportDataSource datasource = new ReportDataSource("ds_SubVoterPhotoCard", tblSubRCardFiltered);
                e.DataSources.Add(datasource);
            }
        }
        #endregion

        #endregion
    }
}
