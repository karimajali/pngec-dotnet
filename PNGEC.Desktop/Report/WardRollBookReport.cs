using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.Windows.Forms;

namespace PNGEC.Desktop.Report
{
    public partial class WardRollBookReport : Form
    {
        #region variable declaration

        /// <summary>
        /// The ds
        /// </summary>
        private DataSet ds = null;

        /// <summary>
        /// The data table
        /// </summary>
        private DataTable DataTable = null;
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="WardRollBookReport"/> class.
        /// </summary>
        public WardRollBookReport(DataTable dataTable)
        {
            InitializeComponent();
            this.DataTable = dataTable;
        }
        #endregion

        #region Events

        /// <summary>
        /// Handles the Load event of the WardRollBookReport control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void WardRollBookReport_Load(object sender, EventArgs e)
        {
            ds = new DataSet();

            this.DataTable.TableName = "tblWardRollBook";
            if (!ds.Tables.Contains(this.DataTable.TableName))
            {
                ds.Tables.Add(this.DataTable);
            }
        }

        #endregion

        #region Methods

        #region bind report source
        /// <summary>
        /// Binds the report source.
        /// </summary>
        /// <returns></returns>
        private void BindReportSource()
        {
            ReportDataSource dataSource = new ReportDataSource("ds_WardRollBook", ds.Tables[0]);

            if (ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
            {
                ReportParameter[] rParams = new ReportParameter[]
                {
                    new ReportParameter("Region", ds.Tables[0].Rows[0]["Region"].ToString()),
                    new ReportParameter("Province", ds.Tables[0].Rows[0]["Province"].ToString()),
                    new ReportParameter("District", ds.Tables[0].Rows[0]["District"].ToString()),
                    new ReportParameter("Electors",  ds.Tables[0].Rows.Count.ToString()),
                    new ReportParameter("DateTime", DateTime.Now.ToString("dddd, MMMM dd, yyyy")),
                    new ReportParameter("ReportTitle", "WORKING ROLL 2021"),
                    new ReportParameter("Version", "V1"),
                    new ReportParameter("WardNumberWardName", ds.Tables[0].Rows[0]["WardNumberWardName"].ToString()),
                };
                this.rvWardRollBook.LocalReport.SetParameters(rParams);
            }
            this.rvWardRollBook.LocalReport.DataSources.Clear();
            this.rvWardRollBook.LocalReport.DataSources.Add(dataSource);
            this.rvWardRollBook.RefreshReport();
        }
        #endregion

        #endregion
    }
}
