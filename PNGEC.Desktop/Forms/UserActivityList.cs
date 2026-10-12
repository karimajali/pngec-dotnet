using PNGEC.API.Domain.Device;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Domain.User;
using PNGEC.API.Helper;
using PNGEC.Desktop.Model;
using PNGEC.Desktop.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PNGEC.Desktop.Forms
{
    public partial class UserActivityList : Form
    {
        #region Variable declaration   
        /// <summary>
        /// The request model
        /// </summary>
        private CommonRequest requestModel = null;

        /// <summary>
        /// 
        /// </summary>
        private string[] LogSourceArray = { "ALL", "Report Server", "Desktop" };
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="UserActivityList"/> class.
        /// </summary>
        public UserActivityList()
        {
            InitializeComponent();
        }
        #endregion

        #region Events

        /// <summary>
        /// Handles the Load event of the UserActivityList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ListDevice_Load(object sender, EventArgs e)
        {
            cmbLogSystem.DataSource = LogSourceArray;
            RefreshData();
            dgUserActivity.CellContentClick -= new DataGridViewCellEventHandler(dgUserActivity_CellContentClick);
            dgUserActivity.CellContentClick += new DataGridViewCellEventHandler(dgUserActivity_CellContentClick);
        }

        /// <summary>
        /// Handles the Click event of the btnAddNew control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnAddNew_Click(object sender, EventArgs e)
        {
            Device device = new Device();
            device.AutoScroll = true;
            if (device.ShowDialog() == DialogResult.OK)
            {
                RefreshData();
            }
        }       

        /// <summary>
        /// Handles the Click event of the btnRefresh control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
            cmbLogSystem.SelectedIndex = 0;
            RefreshData();                 
        }

        /// <summary>
        /// Handles the CellContentClick event of the dgUserActivity control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void dgUserActivity_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
          
        }

        /// <summary>
        /// Handles the Click event of the btnSearch control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void btnSearch_Click(object sender, EventArgs e)
        {
            PageList model = new PageList();
            model.PageNo = 1;
            model.PageSize = 50;
            model.KeyWord = txtSearch.Text;
            model.SortField = "LogId";
            model.SortOrder = "DESC";
            model.LogSource = cmbLogSystem.Text == "ALL" ? 0: cmbLogSystem.Text == "Report Server" ? 1 : 2;

            Response <ActivityLogModel> response = await GenericHttpClient.PostRequest<ActivityLogModel>("ActivityLog/GetActivityLogList", model);
            if (response.Status)
            {
                BindDeviceGrid(response.Data.ListItems.ToList());
            }
        }     

       
        /// <summary>
        /// Handles the CellPainting event of the dgUsers control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellPaintingEventArgs"/> instance containing the event data.</param>
        private void dgUserActivity_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
           
        }     
        
        #endregion

        #region Methods

        #region BindGrid
        /// <summary>
        /// Binds the device gird.
        /// </summary>
        /// <param name="model">The model.</param>
        private void BindDeviceGrid(List<ActivityLogModel> lstDevices)
        {           
           
            dgUserActivity.DataSource = lstDevices;

            dgUserActivity.Columns["LogId"].Visible = false;
            dgUserActivity.Columns["UserID"].Visible = false;
            dgUserActivity.Columns["ResponseCode"].Visible = false;
            dgUserActivity.Columns["ActionBy"].Visible = false;
            dgUserActivity.Columns["LogSource"].Visible = false;
            dgUserActivity.Columns["LogSourceName"].HeaderText = "Log Source";
            dgUserActivity.Columns["LogDescription"].HeaderText = "Description";
            dgUserActivity.Columns["LogTime"].HeaderText = "Log Time";
            dgUserActivity.Columns["UserName"].HeaderText = "User Name";
            dgUserActivity.Columns["EmailAddress"].HeaderText = "User Email Address";
           
        }
        #endregion

        #region RefreshData
        /// <summary>
        /// Refresh Data
        /// </summary>
        private async void RefreshData()
        {
            txtSearch.Text = "";
            PageList model = new PageList();
            model.PageNo = 1;
            model.PageSize = (int.MaxValue - 5);
            model.KeyWord = "";
            model.SortField = "LogId";
            model.SortOrder = "DESC";
            model.LoggedUserId = GlobalsModel.LoggedUser.User.UserId;
            model.LogSource = cmbLogSystem.Text == "ALL" ? 0 : cmbLogSystem.Text == "Report Server" ? 1 : 2;

            Response<ActivityLogModel> response = await GenericHttpClient.PostRequest<ActivityLogModel>("ActivityLog/GetActivityLogList", model);
            if (response.Status)
            {
                BindDeviceGrid(response.Data.ListItems.ToList());
            }
        }
        #endregion

        #region Export CSV

        private void btnExportToCSV_Click(object sender, EventArgs e)
        {
            try
            {
                string exportReportName = string.Empty;
                string messageBoxText = string.Empty;

                exportReportName = $"{GlobalsModel.LoggedUser.Server}-{GlobalsModel.LoggedUser.Database}-{"User Activity Log"}-{DateTime.Now.ToString("yyyyMMddHHmmss")}";


                var saveFileDialog = new SaveFileDialog();
                saveFileDialog.FileName = exportReportName;
                saveFileDialog.Filter = "CSV (*.csv)|*.csv";
                saveFileDialog.DefaultExt = ".csv";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    GlobalsModel.mainMaster.ShowLoader();

                    #region write
                    StreamWriter sw = new StreamWriter(saveFileDialog.FileName, false);
                    //headers    
                    sw.Write(string.Format("{0},{1},{2},{3},{4},{5}", "Log Source", "User Name", "Event", "Log Description", "User Email Address", "Log Time"));
                    sw.Write(sw.NewLine);
                    //data
                    foreach (ActivityLogModel objModel in dgUserActivity.DataSource as List<ActivityLogModel>)
                    {
                        sw.Write(string.Format("{0},{1},{2},{3},{4},{5}",                          
                           objModel.LogSourceName,
                            objModel.UserName,
                            objModel.Event,
                            objModel.LogDescription,
                            objModel.EmailAddress,
                            objModel.LogTime
                           ));

                        sw.Write(sw.NewLine);
                    }
                    sw.Close();
                    #endregion

                    messageBoxText = $"User Activity Log CSV is exported sucessfully at {saveFileDialog.FileName}";
                    MessageBox.Show(messageBoxText, "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }               

                GlobalsModel.mainMaster.HideLoader();
            }
            catch (Exception ex)
            {
                GlobalsModel.mainMaster.HideLoader();
                MessageBox.Show(Resources.ErrorMessage, "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ErrorLog.Write("File", "btnExportToCSV_Click", ex);
            }
            finally
            {
            }
        }


        #endregion


        #endregion
    }
}
