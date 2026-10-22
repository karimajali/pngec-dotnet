using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using PNGEC.API.Domain.District;
using PNGEC.API.Domain.GeoStructure;
using PNGEC.API.Domain.MKAGeoStructure;
using PNGEC.API.Domain.LLG;
using PNGEC.API.Domain.Location;
using PNGEC.API.Domain.Ward;
using PNGEC.API.Helper;
using PNGEC.Desktop.Forms;
using PNGEC.Desktop.Helper;
using PNGEC.Desktop.Model;
using PNGEC.Desktop.Properties;

namespace PNGEC.Desktop
{
    /// <summary>
    /// main master form
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class MainMaster : Form
    {

        #region variable declaration
        /// <summary>
        /// Occurs when [TreeView selected changed].
        /// </summary>
        public event EventHandler TreeViewSelectedChanged = delegate { };

        ///// <summary>
        ///// Occurs when [TreeView selected changed augmented roll stat].
        ///// </summary>
        //public event EventHandler AugmentedRollStat_TreeViewSelectedChanged = delegate { };

        /// <summary>
        /// The get list
        /// </summary>
        private bool getList = false;

        /// <summary>
        /// The loading window
        /// </summary>
        private LoadingWindow loadingWindow = new LoadingWindow();

        /// <summary>
        /// root path of application
        /// </summary>
        private string exePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

        /// <summary>
        /// The menu name
        /// </summary>
        private string menuName = string.Empty;
        /// <summary>
        /// The bread crumb name
        /// </summary>
        private string breadCrumbName = string.Empty;

        protected StatusBarPanel statusPanel = new StatusBarPanel();
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="MainMaster"/> class.
        /// </summary>
        public MainMaster()
        {
            InitializeComponent();
            if (Convert.ToInt16(GlobalsModel.LoggedUser.Server) == 1)//Development
            {
                this.Text = "PNGEC Provincial - DEVELOPMENT env.";
                tvGeoStructure.BackColor = Color.Yellow;
                tvMKAGeoStructure.BackColor = Color.Yellow;
            }
            else if (Convert.ToInt16(GlobalsModel.LoggedUser.Server) == 2)//Training
            {
                this.Text = "PNGEC Provincial - TRAINING env.";
                tvGeoStructure.BackColor = Color.Pink;
                tvMKAGeoStructure.BackColor = Color.Pink;
            }
            else if (Convert.ToInt16(GlobalsModel.LoggedUser.Server) == 3)//Production
            {
                this.Text = "PNGEC Provincial - PRODUCTION env.";
            }

            ////ToolStripItem activeToolStripItem = null;
            ////menuStrip.Paint += (o, e) => activeToolStripItem?.Select();
            ////menuStrip.ItemClicked += (o, e) => activeToolStripItem = e.ClickedItem;
        }

        #endregion

        #region Events

        /// <summary>
        /// Handles the Load event of the MainMaster.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void MainMaster_Load(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            pnlLeft.Visible = false;
            menuStrip.Enabled = false;

            this.UserAllowMenu();
            this.UpdateTopBar();
            this.CreateStatusBar();

            try
            {
                for (int i = 0; i <= 2; i++)
                {
                    getList = await this.GetList();
                    if (getList)
                    {
                        break;
                    }
                    else
                    {
                        await Task.Delay(5000);
                    }
                }
            }
            catch
            {
            }

            if (!getList)
            {
                MessageBox.Show("Internet Connection Error, Please check connection!", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Environment.Exit(0);
            }
            else
            {
                bool isDownloadNewGeo = false;
                bool isDownloadNewMKAGeo = false;
                try
                {
                    if (Globals.GeoList.Version != GlobalsModel.LoggedUser.User.GeoVersion)
                    {
                        MessageBox.Show("Geo structure has been updated by admin. Please wait until downloading new geo structure from server.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (!string.IsNullOrWhiteSpace(GlobalsModel.LoggedUser.User.GeoFileUrl))
                        {
                            var destinationFilePath = $"{exePath}\\geolist.json";

                            using (var client = new HttpClientDownloadWithProgress(GlobalsModel.LoggedUser.User.GeoFileUrl, destinationFilePath))
                            {
                                lblTopMessage.Text = "Please wait until downloading new geo structure from server.";
                                client.ProgressChanged += (totalFileSize, totalBytesDownloaded, progressPercentage) =>
                                {
                                    //Console.WriteLine($"{progressPercentage}% ({totalBytesDownloaded}/{totalFileSize})");
                                    lblDownloading.Text = $" {(int)progressPercentage} %";

                                    if (progressPercentage == 100)
                                    {
                                        isDownloadNewGeo = true;
                                        lblTopMessage.Text = string.Empty;
                                        lblDownloading.Text = string.Empty;
                                    }
                                };

                                await client.StartDownload();
                            }
                        }
                        else
                        {
                            ////ToDo: Call API
                            lblTopMessage.Text = $"Please wait until downloading new geo structure from server...";
                            var geoList = await Globals.DataServiceManager.GetGeoListAsync(1);
                            JsonFileHelper.WriteJsonContent(Newtonsoft.Json.JsonConvert.SerializeObject(geoList.Data), "geolist");
                            AssignDataUserWise(geoList.Data);
                            lblTopMessage.Text = string.Empty;

                            //var mkaGeoList = await Globals.DataServiceManager.GetMKAGeoListAsync(1);
                            //JsonFileHelper.WriteJsonContent(Newtonsoft.Json.JsonConvert.SerializeObject(mkaGeoList.Data), "mkageolist");
                            //AssignMKADataUserWise(mkaGeoList.Data);
                        }
                    }

                    if (GlobalsModel.LoggedUser.User.Role == 9 || GlobalsModel.LoggedUser.User.Role == 0)
                    {
                        //MKA Geo Structure
                        if (Globals.MKAGeoList.Version != GlobalsModel.LoggedUser.User.GeoVersion)
                        {
                            //MessageBox.Show("MKA Geo structure has been updated by admin. Please wait until downloading new mka geo structure from server.", "MKA PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            if (!string.IsNullOrWhiteSpace(GlobalsModel.LoggedUser.User.MKAGeoFileUrl))
                            {
                                var destinationFilePath = $"{exePath}\\mkageolist.json";

                                using (var client = new HttpClientDownloadWithProgress(GlobalsModel.LoggedUser.User.MKAGeoFileUrl, destinationFilePath))
                                {
                                    lblTopMessage.Text = "Please wait until downloading new mka geo structure from server.";
                                    client.ProgressChanged += (totalFileSize, totalBytesDownloaded, progressPercentage) =>
                                    {
                                        //Console.WriteLine($"{progressPercentage}% ({totalBytesDownloaded}/{totalFileSize})");
                                        lblDownloading.Text = $" {(int)progressPercentage} %";

                                        if (progressPercentage == 100)
                                        {
                                            isDownloadNewMKAGeo = true;
                                            lblTopMessage.Text = string.Empty;
                                            lblDownloading.Text = string.Empty;
                                        }
                                    };
                                    await client.StartDownload();
                                }
                            }
                            else
                            {
                                var mkaGeoList = await Globals.DataServiceManager.GetMKAGeoListAsync(1);
                                JsonFileHelper.WriteJsonContent(Newtonsoft.Json.JsonConvert.SerializeObject(mkaGeoList.Data), "mkageolist");
                                AssignMKADataUserWise(mkaGeoList.Data);
                            }

                        }
                    }

                }
                catch (Exception ex)
                {
                    ErrorLog.Write("MainMaster", "MainMaster_Load", ex);
                }

                if (isDownloadNewGeo)
                {
                    //if (GlobalsModel.LoggedUser.User.Role != (int)CommonEnum.Role.MKA)
                    //{
                    var geoListResponse = JsonFileHelper.ReadJsonContent(System.IO.Path.Combine(exePath, "geolist.json"));
                    if (!string.IsNullOrEmpty(geoListResponse) && !string.IsNullOrWhiteSpace(geoListResponse))
                    {
                        var data = Newtonsoft.Json.JsonConvert.DeserializeObject<API.Domain.GeoStructure.GeoListModel>(geoListResponse);
                        AssignDataUserWise(data);
                    }
                    //}
                }
                //if (isDownloadNewMKAGeo)
                //{
                //    var geoListResponse = JsonFileHelper.ReadJsonContent(System.IO.Path.Combine(exePath, "mkageolist.json"));
                //    if (!string.IsNullOrEmpty(geoListResponse) && !string.IsNullOrWhiteSpace(geoListResponse))
                //    {
                //        var data = Newtonsoft.Json.JsonConvert.DeserializeObject<API.Domain.MKAGeoStructure.MKAGeoListModel>(geoListResponse);
                //        AssignMKADataUserWise(data);
                //    }
                //}

                this.InitTreeData(true);
                menuStrip.Enabled = true;
                //tvMKAGeoStructure.Hide();
                //tvGeoStructure.Dock = DockStyle.Fill;
                this.Cursor = Cursors.Arrow;
            }
        }

        /// <summary>
        /// Handles the Click event of the submenuUsers control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void submenuUsers_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            pnlLeft.Visible = true;
            ListUser mdiChildUser = new ListUser();
            mdiChildUser.MdiParent = this;
            mdiChildUser.ClientSize = new System.Drawing.Size(2000, 800);
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            mdiChildUser.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            mdiChildUser.Dock = DockStyle.Fill;
            mdiChildUser.AutoScroll = true;
            menuName = "Admin => " + sender.ToString();
            ManageBreadcums();
            this.SelectedMenuColorChange(this.menuAdmin);
            mdiChildUser.Show();
        }

        /// <summary>
        /// Handles the Click event of the menuFile control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void menuFile_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }
            tvGeoStructure.Show();
            tvMKAGeoStructure.Hide();
            tvGeoStructure.Dock = DockStyle.Fill;
            pnlLeft.Visible = true;
            //DB - MKA Form 11 Batch menu visible only for Admin and when MKA Geo structure is clicked
            //if (GlobalsModel.LoggedUser.User.Role == 0)
            //    submenuMKAForm11Batch.Visible = false;

            File mdiChildFile = new File("File");
            mdiChildFile.MdiParent = this;
            mdiChildFile.ClientSize = new System.Drawing.Size(2000, 800);
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            mdiChildFile.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            mdiChildFile.Dock = DockStyle.Fill;
            mdiChildFile.AutoScroll = true;
            menuName = "PNGECFile";
            breadCrumbName = tvGeoStructure.SelectedNode.FullPath.Replace("\\", " / ");
            ManageBreadcums();
            this.SelectedMenuColorChange(this.menuFile);
            mdiChildFile.Show();
        }

        /// <summary>
        /// Handles the Click event of the MKAFile Menu control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void menuMKAFile_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            tvGeoStructure.Hide();
            tvMKAGeoStructure.Show();
            tvMKAGeoStructure.Dock = DockStyle.Fill;
            pnlLeft.Visible = true;
            //DB - MKA Form 11 Batch menu visible only for Admin and when MKA Geo structure is clicked
            //if(GlobalsModel.LoggedUser.User.Role == 0 || GlobalsModel.LoggedUser.User.Role == 6)
            //    submenuMKAForm11Batch.Visible = true;
            MKAFile mdiChildFile = new MKAFile("MKAFile");
            mdiChildFile.MdiParent = this;
            mdiChildFile.ClientSize = new System.Drawing.Size(2000, 800);
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            mdiChildFile.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            mdiChildFile.Dock = DockStyle.Fill;
            mdiChildFile.AutoScroll = true;
            menuName = "MKAFile";
            breadCrumbName = tvMKAGeoStructure.SelectedNode.FullPath.Replace("\\", " / ");
            ManageBreadcums();
            this.SelectedMenuColorChange(this.mkaToolStripMenuItem);
            mdiChildFile.Show();
        }

        /// <summary>
        /// Handles the Click event of the submenuDataMigration control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void submenuDataMigration_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            pnlLeft.Visible = true;
            tvGeoStructure.Show();
            tvMKAGeoStructure.Hide();
            tvGeoStructure.Dock = DockStyle.Fill;
            File mdiChildFile = new File("DataMigration");
            mdiChildFile.MdiParent = this;
            mdiChildFile.ClientSize = new System.Drawing.Size(2000, 800);
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            mdiChildFile.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            mdiChildFile.Dock = DockStyle.Fill;
            mdiChildFile.AutoScroll = true;
            menuName = "Data => " + sender.ToString();
            ManageBreadcums();
            this.SelectedMenuColorChange(this.menuData);
            mdiChildFile.Show();
        }

        /// <summary>
        /// Handles the Click event of the submenuTransferOut control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void submenuTransferOut_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            pnlLeft.Visible = true;
            File mdiChildFile = new File("TransferOut");
            mdiChildFile.MdiParent = this;
            mdiChildFile.ClientSize = new System.Drawing.Size(2000, 800);
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            mdiChildFile.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            mdiChildFile.Dock = DockStyle.Fill;
            mdiChildFile.AutoScroll = true;
            menuName = "Data => " + sender.ToString();
            ManageBreadcums();
            this.SelectedMenuColorChange(this.menuData);
            mdiChildFile.Show();
        }

        /// <summary>
        /// Handles the Click event of the submenuDuplicate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void submenuDuplicate_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            pnlLeft.Visible = true;
            File mdiChildFile = new File("Duplicate");
            mdiChildFile.MdiParent = this;
            mdiChildFile.ClientSize = new System.Drawing.Size(2000, 800);
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            mdiChildFile.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            mdiChildFile.Dock = DockStyle.Fill;
            mdiChildFile.AutoScroll = true;
            menuName = "Data => " + sender.ToString();
            ManageBreadcums();
            this.SelectedMenuColorChange(this.menuData);
            mdiChildFile.Show();
        }

        /// <summary>
        /// Handles the Click event of the submenuWardRollBook control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void submenuWardRollBook_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            pnlLeft.Visible = true;
            tvMKAGeoStructure.Hide();
            tvGeoStructure.Dock = DockStyle.Fill;
            tvGeoStructure.Show();
            File mdiChildFile = new File("WardRollBook");
            mdiChildFile.MdiParent = this;
            mdiChildFile.ClientSize = new System.Drawing.Size(2000, 800);
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            mdiChildFile.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            mdiChildFile.Dock = DockStyle.Fill;
            mdiChildFile.AutoScroll = true;
            menuName = "Report => " + sender.ToString();
            breadCrumbName = tvGeoStructure.SelectedNode.FullPath.Replace("\\", " / ");
            ManageBreadcums();
            UpdateReportName();
            this.SelectedMenuColorChange(this.menuReport);
            mdiChildFile.Show();
        }

        /// <summary>
        /// Handles the Click event of the submenuMKAWardRollBook control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void submenuMKAWardRollBook_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            pnlLeft.Visible = true;
            tvGeoStructure.Hide();
            tvMKAGeoStructure.Dock = DockStyle.Fill;
            tvMKAGeoStructure.Show();
            MKAFile mdiChildFile = new MKAFile("MKAWardRollBook");
            mdiChildFile.MdiParent = this;
            mdiChildFile.ClientSize = new System.Drawing.Size(2000, 800);
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            mdiChildFile.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            mdiChildFile.Dock = DockStyle.Fill;
            mdiChildFile.AutoScroll = true;
            menuName = "MKA => Data => " + sender.ToString();
            breadCrumbName = tvMKAGeoStructure.SelectedNode.FullPath.Replace("\\", " / ");
            ManageBreadcums();
            UpdateReportName();
            this.SelectedMenuColorChange(this.mkaToolStripMenuItem);
            mdiChildFile.Show();
        }

        /// <summary>
        /// Handles the Click event of the submenuDevice control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void submenuDevice_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }
            pnlLeft.Visible = false;
            ListDevice mdiChildUser = new ListDevice();
            mdiChildUser.MdiParent = this;
            mdiChildUser.ClientSize = new System.Drawing.Size(2000, 800);
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            mdiChildUser.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            mdiChildUser.Dock = DockStyle.Fill;
            mdiChildUser.AutoScroll = true;
            menuName = "Admin => " + sender.ToString();
            ManageBreadcums();
            this.SelectedMenuColorChange(this.menuAdmin);
            mdiChildUser.Show();
        }

        /// <summary>
        /// Handles the Click event of the submenuAugmentedRollStat control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void submenuAugmentedRollStat_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            pnlLeft.Visible = true;
            AugmentedRollStat mdiChildAugmentedRollStat = new AugmentedRollStat();
            mdiChildAugmentedRollStat.MdiParent = this;
            mdiChildAugmentedRollStat.ClientSize = new System.Drawing.Size(2000, 800);
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            mdiChildAugmentedRollStat.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            mdiChildAugmentedRollStat.Dock = DockStyle.Fill;
            mdiChildAugmentedRollStat.AutoScroll = true;
            menuName = "Report => " + sender.ToString();
            ManageBreadcums();
            menuName = "AugmentedRollStat";
            this.SelectedMenuColorChange(this.menuAdmin);
            mdiChildAugmentedRollStat.Show();
        }

        /// <summary>
        /// Handles the Click event of the submenuDownloadProvincialFile control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void submenuDownloadProvincialFile_Click(object sender, EventArgs e)
        {
            if (GlobalsModel.downloadProvincial == null)
            {
                GlobalsModel.downloadProvincial = new DownloadProvincialFile();
            }
            //this.SelectedMenuColorChange(this.menuAdmin);
            //GlobalsModel.downloadProvincial.Width = (GlobalsModel.mainMaster.Width - 100);
            //GlobalsModel.downloadProvincial.Height = (GlobalsModel.mainMaster.Height - 100);
            GlobalsModel.downloadProvincial.ShowDialog();

        }

        /// <summary>
        /// Handles the Click event of the submenuGeoStructure control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void submenuGeoStructure_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            pnlLeft.Visible = true;
            tvMKAGeoStructure.Hide();
            tvGeoStructure.Dock = DockStyle.Fill;
            tvGeoStructure.Show();
            tvGeoStructureList mdiChildUser = new tvGeoStructureList();
            mdiChildUser.MdiParent = this;
            mdiChildUser.ClientSize = new System.Drawing.Size(2000, 800);
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            mdiChildUser.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            mdiChildUser.Dock = DockStyle.Fill;
            mdiChildUser.AutoScroll = true;
            menuName = "Admin => " + sender.ToString();
            breadCrumbName = tvGeoStructure.SelectedNode.FullPath.Replace("\\", " / ");
            ManageBreadcums();
            this.SelectedMenuColorChange(this.menuAdmin);
            mdiChildUser.Show();
        }

        private void submenuMKAGeoStructure_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            pnlLeft.Visible = true;
            tvGeoStructure.Hide();
            tvMKAGeoStructure.Dock = DockStyle.Fill;
            tvMKAGeoStructure.Show();
            tvMKAGeoStructureList mdiChildUser = new tvMKAGeoStructureList();
            mdiChildUser.MdiParent = this;
            mdiChildUser.ClientSize = new System.Drawing.Size(2000, 800);
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            mdiChildUser.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            mdiChildUser.Dock = DockStyle.Fill;
            mdiChildUser.AutoScroll = true;
            menuName = "MKA => Data => " + sender.ToString();
            breadCrumbName = tvMKAGeoStructure.SelectedNode.FullPath.Replace("\\", " / ");
            ManageBreadcums();
            this.SelectedMenuColorChange(this.mkaToolStripMenuItem);
            mdiChildUser.Show();
        }

        /// <summary>
        /// Handles the Click event of the submenuDBConnect control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void submenuDBConnect_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }
            pnlLeft.Visible = false;
            DatabaseConnect mdiChildUser = new DatabaseConnect();
            mdiChildUser.DatabaseChanged += DatabaseConnect_DatabaseChanged;
            mdiChildUser.MdiParent = this;
            mdiChildUser.ClientSize = new System.Drawing.Size(2000, 800);
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            mdiChildUser.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            mdiChildUser.Dock = DockStyle.Fill;
            mdiChildUser.AutoScroll = true;
            menuName = "Data => " + sender.ToString();
            ManageBreadcums();
            this.SelectedMenuColorChange(this.menuData);
            mdiChildUser.Show();
        }

        /// <summary>
        /// Handles the Click event of the submenuForm11Batch control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void submenuForm11Batch_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            pnlLeft.Visible = true;
            tvMKAGeoStructure.Hide();
            tvGeoStructure.Dock = DockStyle.Fill;
            tvGeoStructure.Show();
            EnrolmentForm mdiChildUser = new EnrolmentForm();
            mdiChildUser.MdiParent = this;
            mdiChildUser.ClientSize = new System.Drawing.Size(2000, 800);
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            mdiChildUser.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            mdiChildUser.Dock = DockStyle.Fill;
            mdiChildUser.AutoScroll = true;
            menuName = "Report => " + sender.ToString();
            breadCrumbName = tvGeoStructure.SelectedNode.FullPath.Replace("\\", " / ");
            ManageBreadcums();
            this.SelectedMenuColorChange(this.menuReport);
            mdiChildUser.Show();
        }

        /// <summary>
        /// Handles MKA Form 11 Batch click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void submenuMKAForm11Batch_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            pnlLeft.Visible = true;
            tvGeoStructure.Hide();
            tvMKAGeoStructure.Dock = DockStyle.Fill;
            tvMKAGeoStructure.Show();
            MKAEnrolmentForm mdiChildUser = new MKAEnrolmentForm();
            mdiChildUser.MdiParent = this;
            mdiChildUser.ClientSize = new System.Drawing.Size(2000, 800);
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            mdiChildUser.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            mdiChildUser.Dock = DockStyle.Fill;
            mdiChildUser.AutoScroll = true;
            menuName = "MKA => Data => " + sender.ToString();
            breadCrumbName = tvMKAGeoStructure.SelectedNode.FullPath.Replace("\\", " / ");
            ManageBreadcums();
            this.SelectedMenuColorChange(this.mkaToolStripMenuItem);
            mdiChildUser.Show();
        }

        /// <summary>
        /// Handles the Click event of the mKARollStatToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void mKARollStatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            pnlLeft.Visible = true;
            tvGeoStructure.Hide();
            tvMKAGeoStructure.Dock = DockStyle.Fill;
            tvMKAGeoStructure.Show();
            MKARollStat mdiChildUser = new MKARollStat("MKARollState");
            mdiChildUser.MdiParent = this;
            mdiChildUser.ClientSize = new System.Drawing.Size(2000, 800);
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            mdiChildUser.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            mdiChildUser.Dock = DockStyle.Fill;
            mdiChildUser.AutoScroll = true;
            menuName = "MKA => Report => " + sender.ToString();
            breadCrumbName = tvMKAGeoStructure.SelectedNode.FullPath.Replace("\\", " / ");
            ManageBreadcums();
            this.SelectedMenuColorChange(this.mkaToolStripMenuItem);
            mdiChildUser.Show();
        }

        /// <summary>
        /// Handles the Click event of the submenuRollStart control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void submenuRollStart_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            pnlLeft.Visible = true;
            tvMKAGeoStructure.Hide();
            tvGeoStructure.Dock = DockStyle.Fill;
            tvGeoStructure.Show();
            ScheduleLocSummary mdiChildScheduleLocSummary = new ScheduleLocSummary("RollState");
            mdiChildScheduleLocSummary.MdiParent = this;
            mdiChildScheduleLocSummary.ClientSize = new System.Drawing.Size(2000, 800);
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            mdiChildScheduleLocSummary.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            mdiChildScheduleLocSummary.Dock = DockStyle.Fill;
            mdiChildScheduleLocSummary.AutoScroll = true;
            menuName = "Report => " + sender.ToString();
            ManageBreadcums();
            this.SelectedMenuColorChange(this.menuReport);
            mdiChildScheduleLocSummary.Show();
        }

        /// <summary>
        /// Handles the Click event of the submenuLatestRollStat control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void submenuLatestRollStat_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            pnlLeft.Visible = true;
            ScheduleLocSummary mdiChildScheduleLocSummary = new ScheduleLocSummary("LatestRollState");
            mdiChildScheduleLocSummary.MdiParent = this;
            mdiChildScheduleLocSummary.ClientSize = new System.Drawing.Size(2000, 800);
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            mdiChildScheduleLocSummary.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            mdiChildScheduleLocSummary.Dock = DockStyle.Fill;
            mdiChildScheduleLocSummary.AutoScroll = true;
            menuName = "Report => " + sender.ToString();
            ManageBreadcums();
            this.SelectedMenuColorChange(this.menuReport);
            mdiChildScheduleLocSummary.Show();
        }

        private void submenuWardStatus_Click(object sender, EventArgs e)
        {
            if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Staging)
            {
                if (ActiveMdiChild != null)
                {
                    ActiveMdiChild.Close();
                }

                pnlLeft.Visible = true;
                WardStatus mdiChildWardStatus = new WardStatus("WardStatus");
                mdiChildWardStatus.MdiParent = this;
                mdiChildWardStatus.ClientSize = new System.Drawing.Size(2000, 800);
                this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                mdiChildWardStatus.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                mdiChildWardStatus.Dock = DockStyle.Fill;
                mdiChildWardStatus.AutoScroll = true;
                menuName = "Report => " + sender.ToString();
                ManageBreadcums();
                this.SelectedMenuColorChange(this.menuReport);
                mdiChildWardStatus.Show();
            }
            else
            {
                MessageBox.Show("This Database in this menu is not allowed.", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void submenuFieldTallySummarySheet_Click(object sender, EventArgs e)
        {
            //if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Staging)
            {
                if (ActiveMdiChild != null)
                {
                    ActiveMdiChild.Close();
                }

                pnlLeft.Visible = true;
                tvMKAGeoStructure.Hide();
                tvGeoStructure.Dock = DockStyle.Fill;
                tvGeoStructure.Show();
                WardStatus mdiChildFieldTallySummarySheet = new WardStatus("FieldTallySummarySheet");
                mdiChildFieldTallySummarySheet.MdiParent = this;
                mdiChildFieldTallySummarySheet.ClientSize = new System.Drawing.Size(2000, 800);
                this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                mdiChildFieldTallySummarySheet.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                mdiChildFieldTallySummarySheet.Dock = DockStyle.Fill;
                mdiChildFieldTallySummarySheet.AutoScroll = true;
                menuName = "Report => " + sender.ToString();
                breadCrumbName = tvGeoStructure.SelectedNode.FullPath.Replace("\\", " / ");
                ManageBreadcums();
                this.SelectedMenuColorChange(this.menuReport);
                mdiChildFieldTallySummarySheet.Show();
            }
            //else
            //{
            //    MessageBox.Show("This Database in this menu is not allowed.", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
        }

        /// <summary>
        /// Handles Click event of MKA Field Tally Summary
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mkaFieldTallySummaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Staging)
            {
                if (ActiveMdiChild != null)
                {
                    ActiveMdiChild.Close();
                }

                pnlLeft.Visible = true;
                tvGeoStructure.Hide();
                tvMKAGeoStructure.Dock = DockStyle.Fill;
                tvMKAGeoStructure.Show();
                MKAWardStatus mdiChildFieldTallySummarySheet = new MKAWardStatus("FieldTallySummarySheet");
                mdiChildFieldTallySummarySheet.MdiParent = this;
                mdiChildFieldTallySummarySheet.ClientSize = new System.Drawing.Size(2000, 800);
                this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                mdiChildFieldTallySummarySheet.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                mdiChildFieldTallySummarySheet.Dock = DockStyle.Fill;
                mdiChildFieldTallySummarySheet.AutoScroll = true;
                menuName = "MKA => Report => " + sender.ToString();
                breadCrumbName = tvMKAGeoStructure.SelectedNode.FullPath.Replace("\\", " / ");
                ManageBreadcums();
                this.SelectedMenuColorChange(this.mkaToolStripMenuItem);
                mdiChildFieldTallySummarySheet.Show();
            }
            else
            {
                MessageBox.Show("This Database in this menu is not allowed.", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void submenuStagingWardQualityCheck_Click(object sender, EventArgs e)
        {
            if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Staging)
            {
                if (ActiveMdiChild != null)
                {
                    ActiveMdiChild.Close();
                }

                pnlLeft.Visible = true;
                WardQualityCheck mdiWardQualityCheck = new WardQualityCheck("WardQualityCheck");
                mdiWardQualityCheck.MdiParent = this;
                mdiWardQualityCheck.ClientSize = new System.Drawing.Size(2000, 800);
                this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                mdiWardQualityCheck.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                mdiWardQualityCheck.Dock = DockStyle.Fill;
                mdiWardQualityCheck.AutoScroll = true;
                menuName = "Report => " + sender.ToString();
                ManageBreadcums();
                this.SelectedMenuColorChange(this.menuReport);
                mdiWardQualityCheck.Show();
            }
            else
            {
                MessageBox.Show("This Database in this menu is not allowed.", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Handles the Click event of the menuHelp control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void menuHelp_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the DatabaseChanged event of the DatabaseConnect control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void DatabaseConnect_DatabaseChanged(object sender, EventArgs e)
        {
            this.UpdateTopBar();

            //set dynamic statusbar text
            setPanelStatusBar();

            //set database wise menu
            UserAllowMenu();

        }

        /// <summary>
        /// Handles the AfterSelect event of the tvGeoStructure control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="TreeViewEventArgs"/> instance containing the event data.</param>
        private void tvGeoStructure_AfterSelect(object sender, TreeViewEventArgs e)
        {
            GlobalsModel.TreeViewStructure = (GeoTreeStructure)e.Node.Tag;
            GlobalsModel.SelectedNode = tvGeoStructure.SelectedNode;


            // ManageBreadcums(e.Node.FullPath);
            breadCrumbName = e.Node.FullPath.Replace("\\", " / ");

            string reportName = breadCrumbName.Replace(" / ", "_").Replace("PNGEC_", string.Empty);

            if (GlobalsModel.TreeViewStructure.MainTagName == "Ward")
            {
                string WardName = Globals.GeoList.WardList.Where(x => x.Status && x.WardId == GlobalsModel.TreeViewStructure.Id).Select(x => x.WardName).Distinct().SingleOrDefault();
                int WardNumber = Globals.GeoList.WardList.Where(x => x.Status && x.WardId == GlobalsModel.TreeViewStructure.Id).Select(x => x.WardNumber).Distinct().SingleOrDefault();
                GlobalsModel.reportName = reportName.Substring(reportName.IndexOf('_') + 1).Replace("/", "_").Replace(WardName, string.Format("{0}", WardName));
                //GlobalsModel.reportName = reportName.Substring(reportName.IndexOf('_') + 1).Replace("/", "_").Replace(WardName, string.Format("{0}_{1}", WardNumber, WardName));
            }
            else if (GlobalsModel.TreeViewStructure.MainTagName == "Location")
            {
                string WardName = Globals.GeoList.WardList.Where(x => x.Status && x.WardId == GlobalsModel.TreeViewStructure.ParentId).Select(x => x.WardName).Distinct().SingleOrDefault();
                int WardNumber = Globals.GeoList.WardList.Where(x => x.Status && x.WardId == GlobalsModel.TreeViewStructure.ParentId).Select(x => x.WardNumber).Distinct().SingleOrDefault();
                GlobalsModel.reportName = reportName.Substring(reportName.IndexOf('_') + 1).Replace("/", "_").Replace(WardName, string.Format("{0}", WardName));
                //GlobalsModel.reportName = reportName.Substring(reportName.IndexOf('_') + 1).Replace("/", "_").Replace(WardName, string.Format("{0}_{1}", WardNumber, WardName));
            }
            else
            {
                GlobalsModel.reportName = reportName.Substring(reportName.IndexOf('_') + 1).Replace("/", "_");
            }

            GlobalsModel.reportName = String.Concat(GlobalsModel.reportName.Where(c => !Char.IsWhiteSpace(c)));

            ManageBreadcums();

            TreeViewSelectedChanged(this, new EventArgs());
        }

        /// <summary>
        /// MKA Geo Structure event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvMKAGeoStructure_AfterSelect(object sender, TreeViewEventArgs e)
        {
            GlobalsModel.MKATreeViewStructure = (MKAGeoTreeStructure)e.Node.Tag;
            // ManageBreadcums(e.Node.FullPath);
            breadCrumbName = e.Node.FullPath.Replace("\\", " / ");

            string reportName = breadCrumbName.Replace(" / ", "_").Replace("MKA_", string.Empty);

            if (GlobalsModel.MKATreeViewStructure.MainTagName == "MKAWard")
            {
                string WardName = Globals.MKAGeoList.MKAWardList.Where(x => x.Status && x.WardId == GlobalsModel.MKATreeViewStructure.Id).Select(x => x.WardName).Distinct().SingleOrDefault();
                int WardNumber = Globals.MKAGeoList.MKAWardList.Where(x => x.Status && x.WardId == GlobalsModel.MKATreeViewStructure.Id).Select(x => x.WardNumber).Distinct().SingleOrDefault();
                GlobalsModel.reportName = reportName.Substring(reportName.IndexOf('_') + 1).Replace("/", "_").Replace(WardName, string.Format("{0}", WardName));
                //GlobalsModel.reportName = reportName.Substring(reportName.IndexOf('_') + 1).Replace("/", "_").Replace(WardName, string.Format("{0}_{1}", WardNumber, WardName));
            }
            else if (GlobalsModel.MKATreeViewStructure.MainTagName == "MKALocation")
            {
                string WardName = Globals.MKAGeoList.MKAWardList.Where(x => x.Status && x.WardId == GlobalsModel.MKATreeViewStructure.ParentId).Select(x => x.WardName).Distinct().SingleOrDefault();
                int WardNumber = Globals.MKAGeoList.MKAWardList.Where(x => x.Status && x.WardId == GlobalsModel.MKATreeViewStructure.ParentId).Select(x => x.WardNumber).Distinct().SingleOrDefault();
                GlobalsModel.reportName = reportName.Substring(reportName.IndexOf('_') + 1).Replace("/", "_").Replace(WardName, string.Format("{0}", WardName));
                //GlobalsModel.reportName = reportName.Substring(reportName.IndexOf('_') + 1).Replace("/", "_").Replace(WardName, string.Format("{0}_{1}", WardNumber, WardName));
            }
            else
            {
                GlobalsModel.reportName = reportName.Substring(reportName.IndexOf('_') + 1).Replace("/", "_");
            }

            GlobalsModel.reportName = String.Concat(GlobalsModel.reportName.Where(c => !Char.IsWhiteSpace(c)));

            ManageBreadcums();

            TreeViewSelectedChanged(this, new EventArgs());
        }

        //private string getControlName(object sender)
        //{
        //   return   menuName = sender.ToString();
        //}

        /// <summary>
        /// Handles the AfterExpand event of the tvGeoStructure control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="TreeViewEventArgs"/> instance containing the event data.</param>
        private void tvGeoStructure_AfterExpand(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag != null)
            {
                var value = (GeoTreeStructure)e.Node.Tag;
                if (value.Id != default(int) && value.Children.Any())
                {
                    e.Node.Nodes.Clear();
                    PopulateTreeView(value.Children, e.Node);
                }

            }
        }

        /// <summary>
        /// Handles the AfterExpand event of the tvMKAGeoStructure control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvMKAGeoStructure_AfterExpand(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag != null)
            {
                var value = (MKAGeoTreeStructure)e.Node.Tag;
                if (value.Id != default(int) && value.Children.Any())
                {
                    e.Node.Nodes.Clear();
                    PopulateMKATreeView(value.Children, e.Node);
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the logoutToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GlobalsModel.downloadProvincial != null && GlobalsModel.downloadProvincial.IsProvincialFileDownloading())
            {
                DialogResult dialogResult = MessageBox.Show("Download is in progress. are you sure you want to logout PNGEC Application anyway?", "PNGEC", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    //if (MessageBox.Show("Are you sure you want to logout?", "PNGEC", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    //{
                    Login login = new Login();
                    login.Show();
                    this.Hide();

                   await CommonFunction.AddActivityLogForDesktop(GlobalsModel.LoggedUser.User.UserId, "Login", "Login Out " + GlobalsModel.LoggedUser.User.EmailAddress);
            
                    //try
                    //{
                    //    foreach (Form fm in Application.OpenForms)
                    //    {
                    //        if (fm.Name != "Login" && fm.Name != "SplashScreen")
                    //        {
                    //            fm.Close();
                    //        }
                    //        else if (fm.Name == "Login")
                    //        {
                    //            fm.Show();
                    //        }
                    //    }
                    //}
                    //catch (Exception ex)
                    //{
                    //}                
                    //}
                }
            }
            else
            {
                if (MessageBox.Show("Are you sure you want to logout?", "PNGEC", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Login login = new Login();
                    login.Show();
                    this.Hide();
                    await CommonFunction.AddActivityLogForDesktop(GlobalsModel.LoggedUser.User.UserId, "Login", "Login Out " + GlobalsModel.LoggedUser.User.EmailAddress);
                    //try
                    //{
                    //    foreach (Form fm in Application.OpenForms)
                    //    {
                    //        if (fm.Name != "Login" && fm.Name != "SplashScreen")
                    //        {
                    //            fm.Close();
                    //        }
                    //        else if (fm.Name == "Login")
                    //        {
                    //            fm.Show();
                    //        }
                    //    }
                    //}
                    //catch (Exception ex)
                    //{
                    //}                
                }
            }
        }

        #region unused code
        /// <summary>
        /// Handles the Click event of the transferInVoterToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void transferInVoterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            pnlLeft.Visible = false;
            TransferInVoter transferInVoter = new TransferInVoter();
            transferInVoter.MdiParent = this;
            transferInVoter.ClientSize = new System.Drawing.Size(2000, 800);
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            transferInVoter.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            transferInVoter.Dock = DockStyle.Fill;
            transferInVoter.AutoScroll = true;
            menuName = "Report => " + sender.ToString();
            ManageBreadcums();
            this.SelectedMenuColorChange(this.menuReport);
            transferInVoter.Show();
        }
        #endregion

        /// <summary>
        /// Handles the FormClosing event of the MainMaster control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
        private void MainMaster_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (GlobalsModel.downloadProvincial != null && GlobalsModel.downloadProvincial.IsProvincialFileDownloading())
            {
                DialogResult dialogResult = MessageBox.Show("Download is in progress. Exit PNGEC application anyway?", "PNGEC", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.No)
                {                   
                    e.Cancel = true;
                }
            }
            else
            {
                if (MessageBox.Show("Are you sure you want to close the Provincial App?", "PNGEC", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {                  
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// Handles the Tick event of the tmrDownloadProvincialFile control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void tmrDownloadProvincialFile_Tick(object sender, EventArgs e)
        {
            if (GlobalsModel.DownloadProvinceList != null && GlobalsModel.DownloadProvinceList.Count > 0)
            {
                this.lblDownloading.Text = string.Empty;
                var isFileDownloading = GlobalsModel.DownloadProvinceList.FirstOrDefault(dp => dp.FileDownloadStatus == DownloadStatus.Downloading);
                if (isFileDownloading != null)
                {
                    this.lblDownloading.Text = "Provincial files downloading...";
                }
                else
                {
                    var isAllFileDownloaded = GlobalsModel.DownloadProvinceList.FirstOrDefault(dp => dp.FileDownloadStatus != DownloadStatus.Downloading && dp.FileDownloadStatus == DownloadStatus.Downloaded);
                    if (isAllFileDownloaded != null)
                    {
                        this.lblDownloading.Text = "Provincial files downloaded";
                    }
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the importBioDataDeailsToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void importBioDataDeailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            pnlLeft.Visible = false;
            ImportBioDataDetails mdiChildEnrolledUsers = new ImportBioDataDetails();
            //ImportFromExcel mdiChildEnrolledUsers = new ImportFromExcel();
            mdiChildEnrolledUsers.MdiParent = this;
            mdiChildEnrolledUsers.ClientSize = new System.Drawing.Size(2000, 800);
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            mdiChildEnrolledUsers.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            mdiChildEnrolledUsers.Dock = DockStyle.Fill;
            mdiChildEnrolledUsers.AutoScroll = true;
            menuName = "Admin => " + sender.ToString();
            ManageBreadcums();
            menuName = "ImportBioDataDeails";
            this.SelectedMenuColorChange(this.menuAdmin);
            mdiChildEnrolledUsers.Show();
        }

        /// <summary>
        /// Handles the Click event of the menuDocumentManagement control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void menuDocumentManagement_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }
            pnlLeft.Visible = false;
            DocumentList mdiChildFile = new DocumentList();
            mdiChildFile.MdiParent = this;
            mdiChildFile.ClientSize = new System.Drawing.Size(2000, 800);
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            mdiChildFile.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            mdiChildFile.Dock = DockStyle.Fill;
            mdiChildFile.AutoScroll = true;
            menuName = "Document Management";
            ManageBreadcums();
            this.SelectedMenuColorChange(this.menuDocumentManagement);
            mdiChildFile.Show();
        }

        /// <summary>
        /// Handles the Click event of the submenuTransferInApproval control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void submenuTransferInApproval_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            pnlLeft.Visible = true;
            File mdiChildFile = new File("TransferInApproval");
            mdiChildFile.MdiParent = this;
            mdiChildFile.ClientSize = new System.Drawing.Size(2000, 800);
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            mdiChildFile.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            mdiChildFile.Dock = DockStyle.Fill;
            mdiChildFile.AutoScroll = true;
            menuName = "Data => " + sender.ToString();
            ManageBreadcums();
            this.SelectedMenuColorChange(this.menuData);
            mdiChildFile.Show();
        }

        /// <summary>
        /// Handles the Click event of the settingToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void settingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            pnlLeft.Visible = false;
            Setting mdiChildUser = new Setting();
            mdiChildUser.MdiParent = this;
            mdiChildUser.ClientSize = new System.Drawing.Size(2000, 800);
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            mdiChildUser.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            mdiChildUser.Dock = DockStyle.Fill;
            mdiChildUser.AutoScroll = true;
            menuName = "Admin => " + sender.ToString();
            ManageBreadcums();
            this.SelectedMenuColorChange(this.menuAdmin);
            mdiChildUser.Show();
        }

        /// <summary>
        /// Handles the Click event of the submenuDataMigrationReport control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void submenuDataMigrationReport_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            pnlLeft.Visible = true;
            File mdiChildFile = new File("DataMigrationReport");
            mdiChildFile.MdiParent = this;
            mdiChildFile.ClientSize = new System.Drawing.Size(2000, 800);
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            mdiChildFile.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            mdiChildFile.Dock = DockStyle.Fill;
            mdiChildFile.AutoScroll = true;
            menuName = "Report => " + sender.ToString();
            ManageBreadcums();
            this.SelectedMenuColorChange(this.menuReport);
            mdiChildFile.Show();
        }

        /// <summary>
        /// Handles the Click event of the manageOccupationToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void manageOccupationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }
            pnlLeft.Visible = false;
            OccupationList mdiChildUser = new OccupationList();
            mdiChildUser.MdiParent = this;
            mdiChildUser.ClientSize = new System.Drawing.Size(2000, 800);
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            mdiChildUser.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            mdiChildUser.Dock = DockStyle.Fill;
            mdiChildUser.AutoScroll = true;
            menuName = "Admin => " + sender.ToString();
            ManageBreadcums();
            this.SelectedMenuColorChange(this.menuAdmin);
            mdiChildUser.Show();
        }

        /// <summary>
        /// Handles the Click event of the mkaClansToolStripMenuItem control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mkaClansToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }
            pnlLeft.Visible = false;
            MKAClanList mdiChildUser = new MKAClanList();
            mdiChildUser.MdiParent = this;
            mdiChildUser.ClientSize = new System.Drawing.Size(2000, 800);
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            mdiChildUser.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            mdiChildUser.Dock = DockStyle.Fill;
            mdiChildUser.AutoScroll = true;
            menuName = "MKA => Data => " + sender.ToString();
            ManageBreadcums();
            this.SelectedMenuColorChange(this.mkaToolStripMenuItem);
            mdiChildUser.Show();

        }

        /// <summary>
        /// Handles MKA Migration Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mkaMigrationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            pnlLeft.Visible = true;
            tvGeoStructure.Hide();
            tvMKAGeoStructure.Dock = DockStyle.Fill;
            tvMKAGeoStructure.Show();
            MKAFile mdiChildFile = new MKAFile("DataMigration");
            mdiChildFile.MdiParent = this;
            mdiChildFile.ClientSize = new System.Drawing.Size(2000, 800);
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            mdiChildFile.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            mdiChildFile.Dock = DockStyle.Fill;
            mdiChildFile.AutoScroll = true;
            menuName = "MKA => Data => " + sender.ToString();
            ManageBreadcums();
            this.SelectedMenuColorChange(this.mkaToolStripMenuItem);
            mdiChildFile.Show();
        }

        /// <summary>
        /// Handles MKA Transfer In Approval Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mkaTransferInApprovalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            pnlLeft.Visible = true;
            tvGeoStructure.Hide();
            tvMKAGeoStructure.Dock = DockStyle.Fill;
            tvMKAGeoStructure.Show();
            MKAFile mdiChildFile = new MKAFile("TransferInApproval");
            mdiChildFile.MdiParent = this;
            mdiChildFile.ClientSize = new System.Drawing.Size(2000, 800);
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            mdiChildFile.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            mdiChildFile.Dock = DockStyle.Fill;
            mdiChildFile.AutoScroll = true;
            menuName = "MKA => Data => " + sender.ToString();
            ManageBreadcums();
            this.SelectedMenuColorChange(this.mkaToolStripMenuItem);
            mdiChildFile.Show();
        }

        #endregion

        #region Methods
        /// <summary>
        /// Users the allow menu.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void UserAllowMenu()
        {
            if (GlobalsModel.LoggedUser != null && GlobalsModel.LoggedUser.User.UserId > 0)
            {
                //if (GlobalsModel.LoggedUser.User.Role == 10)
                if (GlobalsModel.LoggedUser.User.Role == (int)CommonEnum.Role.ManageClan)
                {
                    #region Data
                    menuData.Visible = false;                 
                    #endregion

                    #region Report
                    menuReport.Visible = false;
                    mkaReportToolStripMenuItem.Visible = false;
                    //submenuForm11Batch.Visible = false;
                    #endregion

                    #region Admin
                    menuAdmin.Visible = true;                   
                    submenuClansToolStripMenuItem.Visible = true;
                    // subMnuUserActivity.Visible = true;                    
                    submenuDevice.Visible = false;
                    submenuUsers.Visible = false;
                    submenuGeoStructure.Visible = false;
                    submenuAugmentedRollStat.Visible = false;
                    submenuDownloadProvincialFile.Visible = false;
                    importBioDataDeailsToolStripMenuItem.Visible = false;
                    settingToolStripMenuItem.Visible = false;
                    manageOccupationToolStripMenuItem.Visible = false;
                    #endregion

                    #region Document management
                    menuDocumentManagement.Visible = false;
                    #endregion

                    #region File
                    menuFile.Visible = false;
                    #endregion
                }
                //if (GlobalsModel.LoggedUser.User.Role == 1)
                else if (GlobalsModel.LoggedUser.User.Role == (int)CommonEnum.Role.DPO || GlobalsModel.LoggedUser.User.Role == (int)CommonEnum.Role.Guest)
                {
                    #region Data
                    menuData.Visible = false;
                    //submenuDataMigration.Visible = false;
                    //submenuTransferOut.Visible = false;
                    //submenuDuplicate.Visible = false;
                    //submenuTransferInApproval.Visible = false;
                    #endregion

                    #region Report
                    menuReport.Visible = false;
                    mkaReportToolStripMenuItem.Visible = false;
                    //submenuForm11Batch.Visible = false;
                    #endregion

                    #region Admin
                    menuAdmin.Visible = false;
                    #endregion

                    #region Document management
                    menuDocumentManagement.Visible = false;
                    #endregion
                }
                //else if (GlobalsModel.LoggedUser.User.Role == 2 || GlobalsModel.LoggedUser.User.Role == 3)
                else if (GlobalsModel.LoggedUser.User.Role == (int)CommonEnum.Role.EM || GlobalsModel.LoggedUser.User.Role == (int)CommonEnum.Role.RD)
                {
                    #region Data
                    if (GlobalsModel.LoggedUser.User.Role == (int)CommonEnum.Role.EM)
                    {
                        submenuTransferOut.Visible = false;
                        submenuDuplicate.Visible = false;
                        submenuTransferInApproval.Visible = false;
                    }
                    #endregion

                    #region Report
                    submenuForm11Batch.Visible = false;
                    #endregion

                    #region Admin
                    menuAdmin.Visible = false;
                    if (GlobalsModel.LoggedUser.User.Role == (int)CommonEnum.Role.EM)
                    {
                        menuAdmin.Visible = true;
                        submenuDevice.Visible = true;
                        submenuUsers.Visible = false;
                        submenuGeoStructure.Visible = false;
                        submenuAugmentedRollStat.Visible = false;
                        submenuDownloadProvincialFile.Visible = false;
                        importBioDataDeailsToolStripMenuItem.Visible = false;
                        settingToolStripMenuItem.Visible = false;
                    }
                    #endregion

                    #region MKA
                    mkaAdminToolStripMenuItem.Visible = false;
                    mkaForm11ToolStripMenuItem.Visible = false;
                    mkaReportToolStripMenuItem.Visible = true;
                    if (GlobalsModel.LoggedUser.User.Role == (int)CommonEnum.Role.EM)
                    {
                        //Allow EM to access MKA Menus only if assigned Southern region and NCD province
                        if (GlobalsModel.LoggedUser.User.Region == 4 && GlobalsModel.LoggedUser.User.Province == 13)
                        {
                            //menuMKAFile.Visible = true;
                            mkaFileToolStripMenuItem.Visible = true;
                            mkaToolStripMenuItem.Visible = true;
                            mkaAdminToolStripMenuItem.Visible = true;
                            mkaGeoStructureToolStripMenuItem.Visible = false;
                        }
                    }
                    else //RD user
                    {
                        //Allow RD to access MKA Menus only if assigned Southern region
                        if (GlobalsModel.LoggedUser.User.Region == 4)
                        {
                            //menuMKAFile.Visible = true;
                            mkaFileToolStripMenuItem.Visible = true;
                            mkaToolStripMenuItem.Visible = true;
                        }
                    }
                    #endregion
                }
                //else if (GlobalsModel.LoggedUser.User.Role == 4)
                ////else if (GlobalsModel.LoggedUser.User.Role == (int)CommonEnum.Role.Enrolment)
                ////{
                ////    #region Data
                ////    //submenuDataMigration.Visible = false;
                ////    submenuTransferOut.Visible = false;
                ////    submenuDuplicate.Visible = false;
                ////    submenuTransferInApproval.Visible = false;
                ////    #endregion

                ////    #region Report
                ////    //submenuForm11Batch.Visible = true;
                ////    #endregion

                ////    #region Admin
                ////    //menuAdmin.Visible = false;
                ////    #endregion

                ////    if (!string.IsNullOrWhiteSpace(GlobalsModel.LoggedUser.User.Permission))
                ////    {
                ////        AllMenuDisable();
                ////        AllowPermissionMenu(GlobalsModel.LoggedUser.User.Permission);
                ////    }
                ////}
                ////else if (GlobalsModel.LoggedUser.User.Role == 5)
                //else if (GlobalsModel.LoggedUser.User.Role == (int)CommonEnum.Role.Guest)
                //{
                //    #region Data
                //    submenuDataMigration.Visible = false;
                //    submenuTransferOut.Visible = false;
                //    submenuDuplicate.Visible = false;
                //    submenuTransferInApproval.Visible = false;
                //    #endregion

                //    #region Report
                //    submenuForm11Batch.Visible = false;
                //    #endregion

                //    #region Admin
                //    menuAdmin.Visible = false;
                //    #endregion
                //}
                //else if (GlobalsModel.LoggedUser.User.Role == 6 || GlobalsModel.LoggedUser.User.Role == 8)
                else if (GlobalsModel.LoggedUser.User.Role == (int)CommonEnum.Role.Admin || GlobalsModel.LoggedUser.User.Role == (int)CommonEnum.Role.Management)
                {
                    #region Data
                    submenuTransferOut.Visible = false;
                    submenuDuplicate.Visible = false;
                    submenuTransferInApproval.Visible = false;
                    #endregion

                    #region Report
                    //submenuForm11Batch.Visible = false;
                    #endregion

                    #region Admin
                    submenuClansToolStripMenuItem.Visible = true;
                    subMnuUserActivity.Visible = true;
                    subMenMuanageWrit.Visible = true;
                    subMenuManagePollingSchedules.Visible = true;
                    if (Convert.ToInt16(GlobalsModel.LoggedUser.Server) == 3)
                    {
                        subMenuPushProductionDataToTraining.Visible = true;
                    }
                    else
                    {
                        subMenuPushProductionDataToTraining.Visible = false;
                    }
                    #endregion

                    if (!string.IsNullOrWhiteSpace(GlobalsModel.LoggedUser.User.Permission))
                    {
                        AllMenuDisable();
                        AllowPermissionMenu(GlobalsModel.LoggedUser.User.Permission);
                    }
                }
                //else if (GlobalsModel.LoggedUser.User.Role == 7)
                else if (GlobalsModel.LoggedUser.User.Role == (int)CommonEnum.Role.RO)
                {
                    #region Report
                    submenuForm11Batch.Visible = false;
                    mkaForm11ToolStripMenuItem.Visible = false;
                    #endregion

                    #region Admin
                    menuAdmin.Visible = false;
                    #endregion
                }
                else if (GlobalsModel.LoggedUser.User.Role == (int)CommonEnum.Role.MKA)
                {
                    #region File/MKA File
                    menuFile.Visible = true;
                    tvGeoStructure.Visible = true;

                    //menuMKAFile.Visible = true;
                    mkaToolStripMenuItem.Visible = true;
                    mkaFileToolStripMenuItem.Visible = true;
                    //mkaClansToolStripMenuItem.Visible = true;
                    #endregion

                    #region Data
                    menuData.Visible = false;
                    mkaDataToolStripMenuItem.Visible = false;
                    #endregion

                    #region Report
                    menuReport.Visible = false;
                    mkaReportToolStripMenuItem.Visible = false;
                    #endregion

                    #region Admin
                    menuAdmin.Visible = false;
                    mkaAdminToolStripMenuItem.Visible = false;
                    #endregion

                    #region Document management
                    menuDocumentManagement.Visible = false;
                    #endregion
                }
            }

            //hide Transfer In menu
            //transferInVoterToolStripMenuItem.Visible = false;

            if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Final)
            {
                #region Data
                submenuDataMigration.Visible = true;
                submenuTransferOut.Visible = false;
                submenuDuplicate.Visible = false;
                submenuTransferInApproval.Visible = false;
                submenuDataMigrationReport.Visible = true;
                if (GlobalsModel.LoggedUser.User.Role == 0 || GlobalsModel.LoggedUser.User.Role == (int)CommonEnum.Role.RD)
                {
                    mkaToolStripMenuItem.Visible = true;
                    mkaReportToolStripMenuItem.Visible = true;
                    mkaDataToolStripMenuItem.Visible = false;
                }
                #endregion

                //#region Admin
                if (GlobalsModel.LoggedUser.User.Role == 0)
                {
                    submenuClansToolStripMenuItem.Visible = true;
                    subMnuUserActivity.Visible = true;  
                    subMenMuanageWrit.Visible = true;
                    subMenuManagePollingSchedules.Visible = true;
                    if (Convert.ToInt16(GlobalsModel.LoggedUser.Server) == 3)
                    {
                        subMenuPushProductionDataToTraining.Visible = true;
                    }
                    else
                    {
                        subMenuPushProductionDataToTraining.Visible = false;
                    }
                }
                //#endregion
            }
            else if (GlobalsModel.LoggedUser.User.Role == 0 || GlobalsModel.LoggedUser.User.Role == (int)CommonEnum.Role.RD)
            {
                #region Data
                submenuDataMigration.Visible = true;
                submenuTransferOut.Visible = true;
                submenuDuplicate.Visible = true;
                submenuTransferInApproval.Visible = true;
                submenuDataMigrationReport.Visible = true;
                //MKA Menus
                //menuMKAFile.Visible = true;
                mkaFileToolStripMenuItem.Visible = true;
                mkaToolStripMenuItem.Visible = true;
                if (GlobalsModel.LoggedUser.User.Role == 0)
                    mkaAdminToolStripMenuItem.Visible = true;
                mkaReportToolStripMenuItem.Visible = true;
                mkaDataToolStripMenuItem.Visible = true;
                //submenuMKAGeoStructure.Visible = true;
                //mkaClansToolStripMenuItem.Visible = true;
                #endregion

                #region Admin
                if (GlobalsModel.LoggedUser.User.Role == 0)
                {
                    submenuClansToolStripMenuItem.Visible = true;
                    subMnuUserActivity.Visible = true;  
                    subMenMuanageWrit.Visible = true;
                    subMenuManagePollingSchedules.Visible = true;
                    if (Convert.ToInt16(GlobalsModel.LoggedUser.Server) == 3)
                    {
                        subMenuPushProductionDataToTraining.Visible = true;
                    }
                    else
                    {
                        subMenuPushProductionDataToTraining.Visible = false;
                    }
                }
                #endregion
            }

            submenuWardStatus.Visible = false;
            submenuFieldTallySummarySheet.Visible = true;
            mkaFieldTallySummaryToolStripMenuItem.Visible = false;
            submenuStagingWardQualityCheck.Visible = false;
            if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Staging)
            {
                submenuWardStatus.Visible = true;
                submenuFieldTallySummarySheet.Visible = true;
                mkaFieldTallySummaryToolStripMenuItem.Visible = true;
                submenuStagingWardQualityCheck.Visible = true;
            }
        }

        /// <summary>
        /// Alls the menu disable.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void AllMenuDisable()
        {
            menuFile.Visible = false;

            menuData.Visible = false;
            submenuDBConnect.Visible = false;
            submenuDataMigration.Visible = false;
            submenuForm11Batch.Visible = false;

            menuReport.Visible = false;
            submenuWardRollBook.Visible = false;
            submenuRollStart.Visible = false;

            menuAdmin.Visible = false;
            submenuUsers.Visible = false;
            submenuGeoStructure.Visible = false;
            submenuDevice.Visible = false;

            //menuHelp.Visible = false;
        }

        /// <summary>
        /// Allows the permission menu.
        /// </summary>
        /// <param name="permission">The permission.</param>
        /// <exception cref="NotImplementedException"></exception>
        private void AllowPermissionMenu(string permission)
        {
            List<int> permissionIds = permission.Split(',').Select(int.Parse).ToList();

            foreach (var item in permissionIds)
            {
                if (item == 1)
                {
                    //File
                    menuFile.Visible = true;
                }
                else if (item == 2)
                {
                    //Geo Structure
                    menuAdmin.Visible = true;
                    submenuGeoStructure.Visible = true;
                }
                else if (item == 3)
                {
                    //Database Connect
                    menuData.Visible = true;
                    submenuDBConnect.Visible = true;
                }
                else if (item == 4)
                {
                    //Ward Roll Book 
                    menuReport.Visible = true;
                    submenuWardRollBook.Visible = true;
                }
                else if (item == 5)
                {
                    //Role Stat
                    menuReport.Visible = true;
                    submenuRollStart.Visible = true;
                }
                else if (item == 6)
                {
                    //Form 11 Batch
                    menuReport.Visible = true;
                    submenuForm11Batch.Visible = true;
                }
                else if (item == 7)
                {
                    //Users
                    menuAdmin.Visible = true;
                    submenuUsers.Visible = true;
                }
                else if (item == 8)
                {
                    //Devices
                    menuAdmin.Visible = true;
                    submenuDevice.Visible = true;
                }
                else if (item == 9)
                {
                    //MKAFile
                    //menuMKAFile.Visible = true;
                    mkaFileToolStripMenuItem.Visible = true;
                }
                else if (item == 10)
                {
                    //MKA Geo Structure
                    menuAdmin.Visible = true;
                    mkaAdminToolStripMenuItem.Visible = true;
                    mkaGeoStructureToolStripMenuItem.Visible = true;
                    //submenuMKAGeoStructure.Visible = true;
                }
                else if (item == 11)
                {
                    //MKA Ward Roll Book 
                    //menuReport.Visible = true;
                    mkaReportToolStripMenuItem.Visible = true;
                    mkaWardRollToolStripMenuItem.Visible = true;
                    //submenuMKAWardRollBook.Visible = true;
                }
                else if (item == 12)
                {
                    //MKA Form 11 Batch
                    menuReport.Visible = true;
                    mkaReportToolStripMenuItem.Visible = true;
                    mkaForm11ToolStripMenuItem.Visible = true;
                    //submenuMKAForm11Batch.Visible = true;
                }
            }
        }

        /// <summary>
        /// Creates the status bar.
        /// </summary>
        private void CreateStatusBar()
        {
            StatusBar mainStatusBar = new StatusBar();
            //StatusBarPanel statusPanel = new StatusBarPanel();
            StatusBarPanel datetimePanel = new StatusBarPanel();
            StatusBarPanel versionPanel = new StatusBarPanel();
            StatusBarPanel rolePanel = new StatusBarPanel();

            statusPanel.BorderStyle = StatusBarPanelBorderStyle.Sunken;
            //statusPanel.Text = string.Format("Hi {0}, you are currently connect to the {1} database. You can change data view to staging, preliminary and final from Data menu.",
            // String.Concat(GlobalsModel.LoggedUser.User.FirstName, " ", GlobalsModel.LoggedUser.User.LastName), GlobalsModel.LoggedUser.Database);
            setPanelStatusBar();

            statusPanel.AutoSize = StatusBarPanelAutoSize.Contents;
            statusPanel.Alignment = HorizontalAlignment.Left;
            mainStatusBar.Panels.Add(statusPanel);

            rolePanel.BorderStyle = StatusBarPanelBorderStyle.Raised;

            if (GlobalsModel.LoggedUser.User.Role == 0 || GlobalsModel.LoggedUser.User.Role == 4 ||
                GlobalsModel.LoggedUser.User.Role == 5 || GlobalsModel.LoggedUser.User.Role == 6 ||
                GlobalsModel.LoggedUser.User.Role == 8)
            {
                rolePanel.Text = string.Format("Role:{0}  Data: All  ", GlobalsModel.LoggedUser.User.RoleName);
            }
            else if (GlobalsModel.LoggedUser.User.Role == 2)
            {
                rolePanel.Text = string.Format("Role:{0}  Data:{1} ", GlobalsModel.LoggedUser.User.RoleName, GlobalsModel.LoggedUser.User.ProvinceName);
            }
            else if (GlobalsModel.LoggedUser.User.Role == 3)
            {
                rolePanel.Text = string.Format("Role:{0}  Data:{1} ", GlobalsModel.LoggedUser.User.RoleName, GlobalsModel.LoggedUser.User.RegionName);
            }
            else if (GlobalsModel.LoggedUser.User.Role == 1)
            {
                string roleText = string.Empty;
                if (GlobalsModel.LoggedUser.User.Location > 0)
                {
                    roleText = string.Format("Role:{0}  Data:{1}  ", GlobalsModel.LoggedUser.User.RoleName, GlobalsModel.LoggedUser.User.LocationName);
                }
                else if (GlobalsModel.LoggedUser.User.Ward > 0)
                {
                    roleText = string.Format("Role:{0}  Data:{1}  ", GlobalsModel.LoggedUser.User.RoleName, GlobalsModel.LoggedUser.User.WardName);
                }
                else if (GlobalsModel.LoggedUser.User.LLG > 0)
                {
                    roleText = string.Format("Role:{0}  Data:{1}  ", GlobalsModel.LoggedUser.User.RoleName, GlobalsModel.LoggedUser.User.LLGName);
                }
                else if (GlobalsModel.LoggedUser.User.District > 0)
                {
                    roleText = string.Format("Role:{0}  Data:{1}  ", GlobalsModel.LoggedUser.User.RoleName, GlobalsModel.LoggedUser.User.DistrictName);
                }

                rolePanel.Text = roleText;
            }
            else if (GlobalsModel.LoggedUser.User.Role == 7)
            {
                string roleText = string.Empty;
                if (GlobalsModel.LoggedUser.User.Location > 0)
                {
                    roleText = string.Format("Role:{0}  Data:{1}  ", GlobalsModel.LoggedUser.User.RoleName, GlobalsModel.LoggedUser.User.LocationName);
                }
                else if (GlobalsModel.LoggedUser.User.Ward > 0)
                {
                    roleText = string.Format("Role:{0}  Data:{1}  ", GlobalsModel.LoggedUser.User.RoleName, GlobalsModel.LoggedUser.User.WardName);
                }
                else if (GlobalsModel.LoggedUser.User.LLG > 0)
                {
                    roleText = string.Format("Role:{0}  Data:{1}  ", GlobalsModel.LoggedUser.User.RoleName, GlobalsModel.LoggedUser.User.LLGName);
                }
                else if (GlobalsModel.LoggedUser.User.District > 0)
                {
                    roleText = string.Format("Role:{0}  Data:{1}  ", GlobalsModel.LoggedUser.User.RoleName, GlobalsModel.LoggedUser.User.DistrictName);
                }
                else if (GlobalsModel.LoggedUser.User.Province > 0)
                {
                    roleText = string.Format("Role:{0}  Data:{1}  ", GlobalsModel.LoggedUser.User.RoleName, GlobalsModel.LoggedUser.User.ProvinceName);
                }

                rolePanel.Text = roleText;
            }

            rolePanel.AutoSize = StatusBarPanelAutoSize.Spring;
            rolePanel.Alignment = HorizontalAlignment.Right;
            mainStatusBar.Panels.Add(rolePanel);

            datetimePanel.BorderStyle = StatusBarPanelBorderStyle.Raised;
            datetimePanel.Text = $"{DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss")}  ";
            datetimePanel.AutoSize = StatusBarPanelAutoSize.Contents;
            datetimePanel.Alignment = HorizontalAlignment.Right;
            mainStatusBar.Panels.Add(datetimePanel);

            versionPanel.BorderStyle = StatusBarPanelBorderStyle.Raised;
            //versionPanel.Text = $"v {Convert.ToString(System.Reflection.Assembly.GetExecutingAssembly().GetName().Version)}  ";
            versionPanel.Text = $"v {API.Helper.Globals.DesktopVersionYear}.{System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Major}.{System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Minor}  ";
            versionPanel.AutoSize = StatusBarPanelAutoSize.Contents;
            versionPanel.Alignment = HorizontalAlignment.Right;
            mainStatusBar.Panels.Add(versionPanel);

            mainStatusBar.ShowPanels = true;
            Controls.Add(mainStatusBar);
        }

        /// <summary>
        /// Creates the top bar.
        /// </summary>
        public void UpdateTopBar()
        {
            if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Staging)
                pnlTop.BackColor = Color.Red;
            else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Preliminary)
                pnlTop.BackColor = Color.Orange;
            else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Final)
                pnlTop.BackColor = Color.Green;
            else
                pnlTop.BackColor = Color.Gray;

            //lblTopMessage.Text = string.Format("Hi {0}, you are currently connect to {1} database. You can change data view to staging, preliminary and final from Data menu.", string.Concat(GlobalsModel.LoggedUser.User.FirstName, " ", GlobalsModel.LoggedUser.User.LastName), GlobalsModel.LoggedUser.Database);

        }

        /// <summary>
        /// Initializes the tree data.
        /// </summary>
        public void InitTreeData(bool isMainScreen)
        {
            SetData(isMainScreen);
        }

        /// <summary>
        /// Sets the data.
        /// </summary>
        private void SetData(bool isMainScreen)
        {
            if (GlobalsModel.LoggedUser.User.Role == 0 || GlobalsModel.LoggedUser.User.Role == 9
                || (GlobalsModel.LoggedUser.User.Role == 2 && (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Staging || GlobalsModel.LoggedUser.Database == CommonEnum.Database.Preliminary))
                || (GlobalsModel.LoggedUser.User.Role == 3))
            {
                //MKAGeoStructure
                var mkaLocs = Globals.MKAGeoList.MKALocationList.Select(l => new MKAGeoTreeStructure()
                {
                    Id = l.LocationId,
                    Name = l.LocationName,
                    MainTagName = "MKALocation",
                    ParentId = l.WardId,
                    Children = null
                });
                var mkaWards = Globals.MKAGeoList.MKAWardList.OrderBy(o => o.WardNumber).Select(w => new MKAGeoTreeStructure()
                {
                    Id = w.WardId,
                    Name = $"{w.WardNumber} - {w.WardName}",
                    MainTagName = "MKAWard",
                    ParentId = w.LLGId,
                    Children = mkaLocs.Where(l => l.ParentId == w.WardId)
                });
                var mkaLLGs = Globals.MKAGeoList.MKALLGList.Select(w => new MKAGeoTreeStructure()
                {
                    Id = w.LLGId,
                    Name = w.LLGName,
                    MainTagName = "MKALLG",
                    ParentId = w.DistrictId,
                    Children = mkaWards.Where(l => l.ParentId == w.LLGId)
                });
                var mkaDists = Globals.MKAGeoList.MKADistrictList.Select(w => new MKAGeoTreeStructure()
                {
                    Id = w.DistrictId,
                    Name = w.DistrictName,
                    //MainTagName = "MKADistrict",
                    //For Testing
                    MainTagName = "MKADistrict",
                    ParentId = default(int),
                    Children = mkaLLGs.Where(l => l.ParentId == w.DistrictId)
                });
                InitializeMKAActualTreeLogic(mkaDists, isMainScreen);

            }
            //else
            //if (GlobalsModel.LoggedUser.User.Role != (int)CommonEnum.Role.MKA)
            //{
            if (Globals.GeoList.ClanList != null)
            {
                var cla = Globals.GeoList.ClanList.Select(l => new GeoTreeStructure()
                {
                    Id = l.ClanId,
                    Name = l.ClanName,
                    MainTagName = "Clan",
                    ParentId = l.WardId,
                    Children = null
                });
            }
            var loc = Globals.GeoList.LocationList.Select(l => new GeoTreeStructure()
            {
                Id = l.LocationId,
                Name = l.LocationName,
                MainTagName = "Location",
                ParentId = l.WardId,
                Children = null
            });
            var ward = Globals.GeoList.WardList.OrderBy(o => o.WardNumber).Select(w => new GeoTreeStructure()
            {
                Id = w.WardId,
                Name = $"{w.WardNumber} - {w.WardName}",
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

            InitializeActualTreeLogic(reg, isMainScreen);
            //}

        }

        /// <summary>
        /// Initializes the actual tree logic.
        /// </summary>
        /// <param name="region">The region.</param>
        private async void InitializeActualTreeLogic(IEnumerable<GeoTreeStructure> region, bool isMainScreen)
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

                this.tvGeoStructure.AfterSelect -= tvGeoStructure_AfterSelect;

                tvGeoStructure.BeginUpdate(); // No visual updates until we say 
                tvGeoStructure.Nodes.Clear(); // Remove existing nodes

                tvGeoStructure.Nodes.Add(t);
                PopulateTreeView(region, t);
                tvGeoStructure.SelectedNode = t;
                tvGeoStructure.SelectedNode.Expand();
                tvGeoStructure.EndUpdate(); // Allow the treeview to update visually

                this.tvGeoStructure.AfterSelect += tvGeoStructure_AfterSelect;
                if (t.Nodes != null && t.Nodes.Count > 0)
                {
                    tvGeoStructure.SelectedNode = t.Nodes[0];
                }

                if (isMainScreen)
                {
                    if (GlobalsModel.LoggedUser.User.Role == (int)CommonEnum.Role.ManageClan)
                    {
                        this.submenuClansToolStripMenuItem_Click(this, new EventArgs());
                    }
                    else
                    {
                        this.menuFile_Click(this, new EventArgs());
                    }
                }

                this.Cursor = Cursors.Default;
                //this.pnlGeoButtons.Enabled = true;
                //GetgeoDataInBackground();
            });
        }

        /// <summary>
        /// Initialize MKA Tree
        /// </summary>
        /// <param name="MKAdistrict"></param>
        /// <param name="isMainScreen"></param>
        private async void InitializeMKAActualTreeLogic(IEnumerable<MKAGeoTreeStructure> MKAdistrict, bool isMainScreen)
        {
            //this.Cursor = Cursors.WaitCursor;
            MKAGeoTreeStructure mkaGeoTreeStructure = new MKAGeoTreeStructure();
            mkaGeoTreeStructure.Id = 0;
            mkaGeoTreeStructure.MainTagName = "MKAGeo";
            await Task.Delay(50);

            tvMKAGeoStructure.Invoke((Action)delegate ()
            {
                this.Cursor = Cursors.WaitCursor;

                TreeNode t = new TreeNode();
                t.Text = "MKA";
                t.Tag = MKAdistrict;
                t.Name = "MKADistrict";
                t.ImageKey = "MKA";
                t.Tag = mkaGeoTreeStructure;

                this.tvMKAGeoStructure.AfterSelect -= tvMKAGeoStructure_AfterSelect;

                tvMKAGeoStructure.BeginUpdate(); // No visual updates until we say 
                tvMKAGeoStructure.Nodes.Clear(); // Remove existing nodes

                tvMKAGeoStructure.Nodes.Add(t);
                PopulateMKATreeView(MKAdistrict, t);
                tvMKAGeoStructure.SelectedNode = t;
                tvMKAGeoStructure.SelectedNode.Expand();
                tvMKAGeoStructure.EndUpdate(); // Allow the treeview to update visually

                this.tvMKAGeoStructure.AfterSelect += tvMKAGeoStructure_AfterSelect;
                if (t.Nodes != null && t.Nodes.Count > 0)
                {
                    tvMKAGeoStructure.SelectedNode = t.Nodes[0];
                }

                if (isMainScreen)
                {
                    //DB Menu
                    if (GlobalsModel.LoggedUser.User.Role == (int)CommonEnum.Role.MKA || GlobalsModel.LoggedUser.User.Role == (int)CommonEnum.Role.Admin)
                        this.menuMKAFile_Click(this, new EventArgs());
                }

                this.Cursor = Cursors.Default;
                //this.pnlGeoButtons.Enabled = true;
                //GetgeoDataInBackground();
            });

            //Second Menu

        }

        /// <summary>
        /// 
        /// </summary>
        private async void GetgeoDataInBackground()
        {
            await GetgeoDataInBackgroundAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task GetgeoDataInBackgroundAsync()
        {
            try
            {
                var geoListResponse = await Globals.DataServiceManager.GetGeoListAsync(1);
                if (geoListResponse.Status)
                {
                    JsonFileHelper.WriteJsonContent(Newtonsoft.Json.JsonConvert.SerializeObject(geoListResponse.Data), "geolist");
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Write("MainMaster", "AsyncGeo", ex);
            }
        }

        /// <summary>
        /// Populates the TreeView.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="parentNode">The parent node.</param>
        private void PopulateTreeView(IEnumerable<GeoTreeStructure> data, TreeNode parentNode)
        {

            if (parentNode.Name == "Ward")
            { 
                data = data.OrderBy(x => x.Name);
            }

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

                //if (row.Children != null)
                //    PopulateTreeView(row.Children, childNode);

                if (row.Children != null && row.Children.Any())
                {
                    childNode.Nodes.Add("Loading...");
                }
            }
        }

        private void PopulateMKATreeView(IEnumerable<MKAGeoTreeStructure> data, TreeNode parentNode)
        {
            if (parentNode.Name == "MKAWard")
            {
                data = data.OrderBy(x => x.Name);
            }


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
                    tvGeoStructure.Nodes.Add(t);
                    childNode = t;
                }
                else
                {
                    parentNode.Nodes.Add(t);
                    childNode = t;
                }

                if (row.Children != null && row.Children.Any())
                {
                    childNode.Nodes.Add("Loading...");
                }
            }
        }

        //OLD
        /// <summary>
        /// Gets the list.
        /// </summary>
        //private async Task<bool> GetList()
        //{
        //    try
        //    {
        //        ////Globals.dtRegion = await GenericHttpClient.GetList("Region/ListRegion");
        //        ////Globals.dtProvince = await GenericHttpClient.GetList("Province/ListProvince?regionId=" + -1);
        //        ////Globals.dtDistrict = await GenericHttpClient.GetList("District/ListDistrict?provinceId=" + -1);
        //        ////Globals.dtLLG = await GenericHttpClient.GetList("LLG/ListLLG?districtId=" + -1);
        //        ////Globals.dtWard = await GenericHttpClient.GetList("Ward/ListWard?LLGId=" + -1);
        //        ////Globals.dtLocation = await GenericHttpClient.GetList("Location/ListLocation?wardId=" + -1);


        //        var geoListResponse = await Globals.DataServiceManager.GetGeoListAsync(GlobalsModel.LoggedUser.User.UserId);


        //        if (geoListResponse.Status)
        //        {
        //            Globals.GeoList = geoListResponse.Data;
        //            Globals.dtRegion = CommonFunction.ToDataTable(Globals.GeoList.RegionList.ToList());
        //            Globals.dtProvince = CommonFunction.ToDataTable(Globals.GeoList.ProvinceList.ToList());
        //            Globals.dtDistrict = CommonFunction.ToDataTable(Globals.GeoList.DistrictList.ToList());
        //            Globals.dtLLG = CommonFunction.ToDataTable(Globals.GeoList.LLGList.ToList());
        //            Globals.dtWard = CommonFunction.ToDataTable(Globals.GeoList.WardList.ToList());
        //            Globals.dtLocation = CommonFunction.ToDataTable(Globals.GeoList.LocationList.ToList());
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //  MessageBox.Show(Resources.ResourceManager.GetString("ErrorMessage"), "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        ErrorLog.Write("Main", "GetList", ex);
        //        return false;
        //    }

        //    return true;
        //}


        /// <summary>
        /// NEW
        /// </summary>
        /// <returns></returns>
        private async Task<bool> GetList()
        {
            try
            {
                //Geo List
                if (!System.IO.File.Exists(System.IO.Path.Combine(exePath, "geolist.json")))
                {
                    //To get all data passign 0 
                    var geoListResponse = await Globals.DataServiceManager.GetGeoListAsync(1);
                    if (geoListResponse.Status)
                    {
                        JsonFileHelper.WriteJsonContent(Newtonsoft.Json.JsonConvert.SerializeObject(geoListResponse.Data), "geolist");
                        AssignDataUserWise(geoListResponse.Data);
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    var geoListResponse = JsonFileHelper.ReadJsonContent(System.IO.Path.Combine(exePath, "geolist.json"));
                    if (!string.IsNullOrEmpty(geoListResponse) && !string.IsNullOrWhiteSpace(geoListResponse))
                    {
                        var data = Newtonsoft.Json.JsonConvert.DeserializeObject<API.Domain.GeoStructure.GeoListModel>(geoListResponse);
                        AssignDataUserWise(data);
                    }
                    else
                    {
                        //var geoList = await Globals.DataServiceManager.GetGeoListAsync(GlobalsModel.LoggedUser.User.UserId);
                        var geoList = await Globals.DataServiceManager.GetGeoListAsync(1);
                        JsonFileHelper.WriteJsonContent(Newtonsoft.Json.JsonConvert.SerializeObject(geoList.Data), "geolist");
                        AssignDataUserWise(geoList.Data);
                    }
                }

                if (!System.IO.File.Exists(System.IO.Path.Combine(exePath, "mkageolist.json")))
                {
                    //To get all data passing 0 
                    var geoListResponse = await Globals.DataServiceManager.GetMKAGeoListAsync(1);
                    if (geoListResponse.Status)
                    {
                        JsonFileHelper.WriteJsonContent(Newtonsoft.Json.JsonConvert.SerializeObject(geoListResponse.Data), "mkageolist");
                        AssignMKADataUserWise(geoListResponse.Data);
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    var geoListResponse = JsonFileHelper.ReadJsonContent(System.IO.Path.Combine(exePath, "mkageolist.json"));
                    if (!string.IsNullOrEmpty(geoListResponse) && !string.IsNullOrWhiteSpace(geoListResponse))
                    {
                        var data = Newtonsoft.Json.JsonConvert.DeserializeObject<API.Domain.MKAGeoStructure.MKAGeoListModel>(geoListResponse);
                        AssignMKADataUserWise(data);
                    }
                    else
                    {
                        var geoList = await Globals.DataServiceManager.GetMKAGeoListAsync(1);
                        JsonFileHelper.WriteJsonContent(Newtonsoft.Json.JsonConvert.SerializeObject(geoList.Data), "mkageolist");
                        AssignMKADataUserWise(geoList.Data);
                    }
                }

            }
            catch (Exception ex)
            {
                //  MessageBox.Show(Resources.ResourceManager.GetString("ErrorMessage"), "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorLog.Write("Main", "GetList", ex);
                JsonFileHelper.RemoveGeoFile();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Assigning Data to global var
        /// </summary>
        /// <param name="geoList"></param>
        private void AssignData(API.Domain.GeoStructure.GeoListModel geoList)
        {
            Globals.GeoList = geoList;
            Globals.dtRegion = CommonFunction.ToDataTable(Globals.GeoList.RegionList.ToList());
            Globals.dtProvince = CommonFunction.ToDataTable(Globals.GeoList.ProvinceList.ToList());
            Globals.dtDistrict = CommonFunction.ToDataTable(Globals.GeoList.DistrictList.ToList());
            Globals.dtLLG = CommonFunction.ToDataTable(Globals.GeoList.LLGList.ToList());
            Globals.dtWard = CommonFunction.ToDataTable(Globals.GeoList.WardList.ToList());
            Globals.dtLocation = CommonFunction.ToDataTable(Globals.GeoList.LocationList.ToList());
            if (Globals.GeoList.ClanList != null)
                Globals.dtClan = CommonFunction.ToDataTable(Globals.GeoList.ClanList.ToList());
        }

        /// <summary>
        /// Assigning MKA Data to global variables
        /// </summary>
        /// <param name="mkaGeoList"></param>
        private void MKAAssignData(API.Domain.MKAGeoStructure.MKAGeoListModel mkaGeoList)
        {
            Globals.MKAGeoList = mkaGeoList;
            Globals.dtMKADistricts = CommonFunction.ToDataTable(Globals.MKAGeoList.MKADistrictList.ToList());
            Globals.dtMKALLGs = CommonFunction.ToDataTable(Globals.MKAGeoList.MKALLGList.ToList());
            Globals.dtMKAWards = CommonFunction.ToDataTable(Globals.MKAGeoList.MKAWardList.ToList());
            Globals.dtMKALocations = CommonFunction.ToDataTable(Globals.MKAGeoList.MKALocationList.ToList());

        }

        /// <summary>
        /// Assigning Data User wise to globle var
        /// </summary>
        /// <param name="geoList"></param>
        private void AssignDataUserWise(API.Domain.GeoStructure.GeoListModel geoList)
        {
            //LocalGeoList.RegionList = geoList.RegionList;
            LocalGeoList.ProvinceList = geoList.ProvinceList;
            //LocalGeoList.DistrictList = geoList.DistrictList;
            //LocalGeoList.LLGList = geoList.LLGList;
            //LocalGeoList.WardList = geoList.WardList;
            //LocalGeoList.LocationList = geoList.LocationList;

            //Enrolment || Admin || Super Admin || Guest || Management
            //if (GlobalsModel.LoggedUser.User.Role == 6 || GlobalsModel.LoggedUser.User.Role == 0 ||
            //    GlobalsModel.LoggedUser.User.Role == 4 || GlobalsModel.LoggedUser.User.Role == 5 ||
            //    GlobalsModel.LoggedUser.User.Role == 8)
            if (GlobalsModel.LoggedUser.User.Role == (int)CommonEnum.Role.Admin || GlobalsModel.LoggedUser.User.Role == 0 ||
               GlobalsModel.LoggedUser.User.Role == (int)CommonEnum.Role.ManageClan || GlobalsModel.LoggedUser.User.Role == (int)CommonEnum.Role.Guest ||
                GlobalsModel.LoggedUser.User.Role == (int)CommonEnum.Role.Management || GlobalsModel.LoggedUser.User.Role == (int)CommonEnum.Role.DPO)
            {
                if (GlobalsModel.LoggedUser.User.Role == (int)CommonEnum.Role.ManageClan)
                {
                    var provinceId = GlobalsModel.LoggedUser.User.Province;
                    var regionList = geoList.RegionList.Where(r => r.RegionId == GlobalsModel.LoggedUser.User.Region).ToList();
                    var provinceList = geoList.ProvinceList.Where(p => p.ProvinceId == GlobalsModel.LoggedUser.User.Province).ToList();
                    var districtList = geoList.DistrictList.Where(d => d.ProvinceId == provinceId).ToList();
                    var llgList = geoList.LLGList.Where(l => l.ProvinceId == provinceId).ToList();
                    var wardList = geoList.WardList.Where(w => w.ProvinceId == provinceId).ToList();
                    var locationList = geoList.LocationList.Where(l => l.ProvinceId == provinceId).ToList();
                    var clanList = geoList.ClanList.Where(l => l.ProvinceId == provinceId).ToList();
                    var geo = new API.Domain.GeoStructure.GeoListModel()
                    {
                        RegionList = regionList,
                        ProvinceList = provinceList,
                        DistrictList = districtList,
                        LLGList = llgList,
                        WardList = wardList,
                        LocationList = locationList,
                        ClanList = clanList,
                        Version = geoList.Version
                    };
                    AssignData(geo);
                }
                else
                {
                    AssignData(geoList);
                }
            }
            else if (GlobalsModel.LoggedUser.User.Role == 2 ) //EM
            {
                var provinceId = GlobalsModel.LoggedUser.User.Province;
                var regionList = geoList.RegionList.Where(r => r.RegionId == GlobalsModel.LoggedUser.User.Region).ToList();
                var provinceList = geoList.ProvinceList.Where(p => p.ProvinceId == GlobalsModel.LoggedUser.User.Province).ToList();
                var districtList = geoList.DistrictList.Where(d => d.ProvinceId == provinceId).ToList();
                var llgList = geoList.LLGList.Where(l => l.ProvinceId == provinceId).ToList();
                var wardList = geoList.WardList.Where(w => w.ProvinceId == provinceId).ToList();
                var locationList = geoList.LocationList.Where(l => l.ProvinceId == provinceId).ToList();              
                var geo = new API.Domain.GeoStructure.GeoListModel()
                {
                    RegionList = regionList,
                    ProvinceList = provinceList,
                    DistrictList = districtList,
                    LLGList = llgList,
                    WardList = wardList,
                    LocationList = locationList,                   
                    Version = geoList.Version
                };
                AssignData(geo);
            }
            else if (GlobalsModel.LoggedUser.User.Role == 3) //RD
            {
                var regionId = GlobalsModel.LoggedUser.User.Region;
                var regionList = geoList.RegionList.Where(r => r.RegionId == GlobalsModel.LoggedUser.User.Region).ToList();
                var provinceList = geoList.ProvinceList.Where(p => p.RegionId == regionId).ToList();
                var districtList = geoList.DistrictList.Where(d => d.RegionId == regionId).ToList();
                var llgList = geoList.LLGList.Where(l => l.RegionId == regionId).ToList();
                var wardList = geoList.WardList.Where(w => w.RegionId == regionId).ToList();
                var locationList = geoList.LocationList.Where(l => l.RegionId == regionId).ToList();
                var geo = new API.Domain.GeoStructure.GeoListModel()
                {
                    RegionList = regionList,
                    ProvinceList = provinceList,
                    DistrictList = districtList,
                    LLGList = llgList,
                    WardList = wardList,
                    LocationList = locationList,
                    Version = geoList.Version
                }; AssignData(geo);
            }
            else if (GlobalsModel.LoggedUser.User.Role == 7) //RO
            {
                var regionId = GlobalsModel.LoggedUser.User.Region;
                var provinceId = GlobalsModel.LoggedUser.User.Province;
                var districtId = GlobalsModel.LoggedUser.User.District;
                var llgId = GlobalsModel.LoggedUser.User.LLG;
                var wardId = GlobalsModel.LoggedUser.User.Ward;
                var locationId = GlobalsModel.LoggedUser.User.Location;
                var regionList = geoList.RegionList.Where(r => r.RegionId == GlobalsModel.LoggedUser.User.Region).ToList();
                var provinceList = geoList.ProvinceList.Where(p => p.ProvinceId == GlobalsModel.LoggedUser.User.Province).ToList();
                //var districtList = geoList.DistrictList.Where(d => d.DistrictId == GlobalsModel.LoggedUser.User.District).ToList();

                var districtList = new List<DistrictModel>();
                List<LLGModel> llgList = new List<LLGModel>();
                List<WardModel> wardList = new List<WardModel>();
                List<LocationModel> locationList = new List<LocationModel>();

                if (provinceId > 0)
                {
                    provinceList = geoList.ProvinceList.Where(p => p.ProvinceId == provinceId).ToList();
                    if (districtId > 0)
                    {
                        districtList = geoList.DistrictList.Where(d => d.DistrictId == districtId).ToList();
                        if (llgId > 0)
                        {
                            llgList = geoList.LLGList.Where(l => l.LLGId == llgId).ToList();
                            if (wardId > 0)
                            {
                                wardList = geoList.WardList.Where(w => w.WardId == wardId).ToList();
                                if (locationId > 0)
                                {
                                    locationList = geoList.LocationList.Where(l => l.LocationId == locationId).ToList();
                                }
                                else
                                {
                                    locationList = geoList.LocationList.Where(l => l.WardId == wardId).ToList();
                                }
                            }
                            else
                            {
                                wardList = geoList.WardList.Where(w => w.LLGId == llgId).ToList();
                                locationList = geoList.LocationList.Where(l => l.LLGId == llgId).ToList();
                            }
                        }
                        else
                        {
                            llgList = geoList.LLGList.Where(l => l.DistrictId == districtId).ToList();
                            wardList = geoList.WardList.Where(w => w.DistrictId == districtId).ToList();
                            locationList = geoList.LocationList.Where(l => l.DistrictId == districtId).ToList();
                        }
                    }
                    else
                    {
                        districtList = geoList.DistrictList.Where(d => d.ProvinceId == provinceId).ToList();
                        llgList = geoList.LLGList.Where(l => l.ProvinceId == provinceId).ToList();
                        wardList = geoList.WardList.Where(w => w.ProvinceId == provinceId).ToList();
                        locationList = geoList.LocationList.Where(l => l.ProvinceId == provinceId).ToList();
                    }
                }
                else
                {
                    provinceList = geoList.ProvinceList.Where(d => d.RegionId == regionId).ToList();
                    districtList = geoList.DistrictList.Where(d => d.ProvinceId == provinceId).ToList();
                    llgList = geoList.LLGList.Where(l => l.ProvinceId == provinceId).ToList();
                    wardList = geoList.WardList.Where(w => w.ProvinceId == provinceId).ToList();
                    locationList = geoList.LocationList.Where(l => l.ProvinceId == provinceId).ToList();
                }

                var geo = new API.Domain.GeoStructure.GeoListModel()
                {
                    RegionList = regionList,
                    ProvinceList = provinceList,
                    DistrictList = districtList,
                    LLGList = llgList,
                    WardList = wardList,
                    LocationList = locationList,
                    Version = geoList.Version
                }; AssignData(geo);
            }
            else //DPO
            {
                var provinceId = GlobalsModel.LoggedUser.User.Province;
                var districtId = GlobalsModel.LoggedUser.User.District;
                var llgId = GlobalsModel.LoggedUser.User.LLG;
                var wardId = GlobalsModel.LoggedUser.User.Ward;
                var locationId = GlobalsModel.LoggedUser.User.Location;
                var regionList = geoList.RegionList.Where(r => r.RegionId == GlobalsModel.LoggedUser.User.Region).ToList();
                var provinceList = geoList.ProvinceList.Where(p => p.ProvinceId == GlobalsModel.LoggedUser.User.Province).ToList();
                //var districtList = geoList.DistrictList.Where(d => d.DistrictId == GlobalsModel.LoggedUser.User.District).ToList();

                var districtList = new List<DistrictModel>();
                List<LLGModel> llgList = new List<LLGModel>();
                List<WardModel> wardList = new List<WardModel>();
                List<LocationModel> locationList = new List<LocationModel>();

                if (districtId > 0)
                {
                    districtList = geoList.DistrictList.Where(d => d.DistrictId == districtId).ToList();
                    if (llgId > 0)
                    {
                        llgList = geoList.LLGList.Where(l => l.LLGId == llgId).ToList();
                        if (wardId > 0)
                        {
                            wardList = geoList.WardList.Where(w => w.WardId == wardId).ToList();
                            if (locationId > 0)
                            {
                                locationList = geoList.LocationList.Where(l => l.LocationId == locationId).ToList();
                            }
                            else
                            {
                                locationList = geoList.LocationList.Where(l => l.WardId == wardId).ToList();
                            }
                        }
                        else
                        {
                            wardList = geoList.WardList.Where(w => w.LLGId == llgId).ToList();
                            locationList = geoList.LocationList.Where(l => l.LLGId == llgId).ToList();
                        }
                    }
                    else
                    {
                        llgList = geoList.LLGList.Where(l => l.DistrictId == districtId).ToList();
                        wardList = geoList.WardList.Where(w => w.DistrictId == districtId).ToList();
                        locationList = geoList.LocationList.Where(l => l.DistrictId == districtId).ToList();
                    }
                }
                else
                {
                    districtList = geoList.DistrictList.Where(d => d.ProvinceId == provinceId).ToList();
                    llgList = geoList.LLGList.Where(l => l.ProvinceId == provinceId).ToList();
                    wardList = geoList.WardList.Where(w => w.ProvinceId == provinceId).ToList();
                    locationList = geoList.LocationList.Where(l => l.ProvinceId == provinceId).ToList();
                }

                var geo = new API.Domain.GeoStructure.GeoListModel()
                {
                    RegionList = regionList,
                    ProvinceList = provinceList,
                    DistrictList = districtList,
                    LLGList = llgList,
                    WardList = wardList,
                    LocationList = locationList,
                    Version = geoList.Version
                }; AssignData(geo);
            }
        }

        /// <summary>
        /// Assign MKA Data based on user logged in
        /// </summary>
        /// <param name="mkaGeoList"></param>
        private void AssignMKADataUserWise(API.Domain.MKAGeoStructure.MKAGeoListModel mkaGeoList)
        {
            if (mkaGeoList == null)
                return;

            //MKALocalGeoList.DistrictList = mkaGeoList.MKADistrictList;
            //MKALocalGeoList.LLGList = mkaGeoList.MKALLGList;
            //MKALocalGeoList.WardList = mkaGeoList.MKAWardList;
            //MKALocalGeoList.LocationList = mkaGeoList.MKALocationList;


            //Enrolment || Admin || Super Admin || Guest || Management
            //if (GlobalsModel.LoggedUser.User.Role == 6 || GlobalsModel.LoggedUser.User.Role == 0 ||
            //    GlobalsModel.LoggedUser.User.Role == 4 || GlobalsModel.LoggedUser.User.Role == 5 ||
            //    GlobalsModel.LoggedUser.User.Role == 8)
            if (GlobalsModel.LoggedUser.User.Role == (int)CommonEnum.Role.Admin || GlobalsModel.LoggedUser.User.Role == 0 ||
                GlobalsModel.LoggedUser.User.Role == (int)CommonEnum.Role.ManageClan || GlobalsModel.LoggedUser.User.Role == (int)CommonEnum.Role.Guest ||
                GlobalsModel.LoggedUser.User.Role == (int)CommonEnum.Role.Management || GlobalsModel.LoggedUser.User.Role == (int)CommonEnum.Role.EM || GlobalsModel.LoggedUser.User.Role == (int)CommonEnum.Role.RD)
            {
                MKAAssignData(mkaGeoList);
            }
            //MKAAssignData(mkaGeoList);
            else if (GlobalsModel.LoggedUser.User.Role == (int)CommonEnum.Role.MKA)
            {
                //#region test
                var mkaLLGId = GlobalsModel.LoggedUser.User.MKALLG;
                var mkaDistrictId = GlobalsModel.LoggedUser.User.MKADistrict;
                var mkawardId = GlobalsModel.LoggedUser.User.MKAWard;
                var mkalocationId = GlobalsModel.LoggedUser.User.MKALocation;

                var mkadistrictList = mkaGeoList.MKADistrictList.Where(r => r.DistrictId == GlobalsModel.LoggedUser.User.MKADistrict).ToList();
                var mkaLLGList = mkaGeoList.MKALLGList.Where(p => p.LLGId == GlobalsModel.LoggedUser.User.MKALLG).ToList();
                //var districtList = geoList.DistrictList.Where(d => d.DistrictId == GlobalsModel.LoggedUser.User.District).ToList();

                //var districtList = new List<DistrictModel>();
                var wardList = new List<MKAWardModel>();
                List<MKALocationModel> locationList = new List<MKALocationModel>();

                if (mkawardId > 0)
                {
                    wardList = mkaGeoList.MKAWardList.Where(d => d.WardId == mkawardId).ToList();
                    if (mkalocationId > 0)
                    {
                        locationList = mkaGeoList.MKALocationList.Where(l => l.LocationId == mkalocationId).ToList();
                        //if (wardId > 0)
                        //{
                        //    wardList = geoList.WardList.Where(w => w.WardId == wardId).ToList();
                        //    if (locationId > 0)
                        //    {
                        //        locationList = geoList.LocationList.Where(l => l.LocationId == locationId).ToList();
                        //    }
                        //    else
                        //    {
                        //        locationList = geoList.LocationList.Where(l => l.WardId == wardId).ToList();
                        //    }
                        //}
                        //else
                        //{
                        //    wardList = mkaGeoList.MKAWardList.Where(w => w.LLGId == mkaLLGId).ToList();
                        //    locationList = geoList.LocationList.Where(l => l.LLGId == mkaLLGId).ToList();
                        //}
                    }
                    else
                    {
                        //wardList = mkaGeoList.MKAWardList.Where(w => w.LLGId == mka).ToList();
                        //locationList = geoList.LocationList.Where(l => l.DistrictId == districtId).ToList();
                        locationList = mkaGeoList.MKALocationList.Where(l => l.WardId == mkawardId).ToList();
                    }
                }
                else
                {
                    wardList = mkaGeoList.MKAWardList.Where(w => w.LLGId == mkaLLGId).ToList();
                    locationList = mkaGeoList.MKALocationList.Where(l => l.LLGId == mkaLLGId).ToList();
                }

                var clanList = mkaGeoList.MKAClanList.ToList();
                var mkageo = new API.Domain.MKAGeoStructure.MKAGeoListModel()
                {
                    MKADistrictList = mkadistrictList,
                    MKALLGList = mkaLLGList,
                    MKAWardList = wardList,
                    MKALocationList = locationList,
                    MKAClanList = clanList,
                    Version = mkaGeoList.Version
                }; MKAAssignData(mkageo);
            }
            //#endregion



            //DB - We are not filtering MKA data based on the user logged in, hence it will show all data
            #region Filter data user wise
            //else if (GlobalsModel.LoggedUser.User.Role == 2) //EM
            //{
            //    var provinceId = GlobalsModel.LoggedUser.User.Province;

            //    //var regionList = geoList.RegionList.Where(r => r.RegionId == GlobalsModel.LoggedUser.User.Region).ToList();
            //    //var provinceList = geoList.ProvinceList.Where(p => p.ProvinceId == GlobalsModel.LoggedUser.User.Province).ToList();
            //    var districtList = mkaGeoList.MKADistrictList.ToList();
            //    var llgList = mkaGeoList.MKALLGList.ToList();
            //    //var llgList = mkaGeoList.MKALLGList.Where(l => l.DistrictId == provinceId).ToList();
            //    var wardList = mkaGeoList.MKAWardList.ToList();
            //    //var wardList = mkaGeoList.MKAWardList.Where(w => w.ProvinceId == provinceId).ToList();
            //    var locationList = mkaGeoList.MKALocationList.ToList();
            //    //var locationList = mkaGeoList.MKALocationList.Where(l => l.ProvinceId == provinceId).ToList();
            //    var mkaGeo = new API.Domain.MKAGeoStructure.MKAGeoListModel()
            //    {
            //        MKADistrictList = districtList,
            //        MKALLGList = llgList,
            //        MKAWardList = wardList,
            //        MKALocationList = locationList,
            //        Version = mkaGeoList.Version
            //    };
            //    MKAAssignData(mkaGeo);
            //}
            //else if (GlobalsModel.LoggedUser.User.Role == 3) //RD
            //{
            //    var districtList = mkaGeoList.MKADistrictList.ToList();
            //    var llgList = mkaGeoList.MKALLGList.ToList();
            //    var wardList = mkaGeoList.MKAWardList.ToList();
            //    var locationList = mkaGeoList.MKALocationList.ToList();
            //    var mkaGeo = new API.Domain.MKAGeoStructure.MKAGeoListModel()
            //    {
            //        MKADistrictList = districtList,
            //        MKALLGList = llgList,
            //        MKAWardList = wardList,
            //        MKALocationList = locationList,
            //        Version = mkaGeoList.Version
            //    };
            //    MKAAssignData(mkaGeo);
            //}
            //else if (GlobalsModel.LoggedUser.User.Role == 7) //RO
            //{
            //    //var regionId = GlobalsModel.LoggedUser.User.Region;
            //    //var provinceId = GlobalsModel.LoggedUser.User.Province;
            //    var districtId = GlobalsModel.LoggedUser.User.District;
            //    var llgId = GlobalsModel.LoggedUser.User.LLG;
            //    var wardId = GlobalsModel.LoggedUser.User.Ward;
            //    var locationId = GlobalsModel.LoggedUser.User.Location;
            //    //var regionList = geoList.RegionList.Where(r => r.RegionId == GlobalsModel.LoggedUser.User.Region).ToList();
            //    //var provinceList = geoList.ProvinceList.Where(p => p.ProvinceId == GlobalsModel.LoggedUser.User.Province).ToList();
            //    //var districtList = geoList.DistrictList.Where(d => d.DistrictId == GlobalsModel.LoggedUser.User.District).ToList();

            //    var districtList = new List<MKADistrictModel>();
            //    List<MKALLGModel> llgList = new List<MKALLGModel>();
            //    List<MKAWardModel> wardList = new List<MKAWardModel>();
            //    List<MKALocationModel> locationList = new List<MKALocationModel>();

            //    //if (provinceId > 0)
            //    //{
            //    //  provinceList = geoList.ProvinceList.Where(p => p.ProvinceId == provinceId).ToList();
            //    if (districtId > 0)
            //    {
            //        districtList = mkaGeoList.MKADistrictList.Where(d => d.DistrictId == districtId).ToList();
            //        if (llgId > 0)
            //        {
            //            llgList = mkaGeoList.MKALLGList.Where(l => l.LLGId == llgId).ToList();
            //            if (wardId > 0)
            //            {
            //                wardList = mkaGeoList.MKAWardList.Where(w => w.WardId == wardId).ToList();
            //                if (locationId > 0)
            //                {
            //                    locationList = mkaGeoList.MKALocationList.Where(l => l.LocationId == locationId).ToList();
            //                }
            //                else
            //                {
            //                    locationList = mkaGeoList.MKALocationList.Where(l => l.WardId == wardId).ToList();
            //                }
            //            }
            //            else
            //            {
            //                wardList = mkaGeoList.MKAWardList.Where(w => w.LLGId == llgId).ToList();
            //                locationList = mkaGeoList.MKALocationList.Where(l => l.LLGId == llgId).ToList();
            //            }
            //        }
            //        else
            //        {
            //            llgList = mkaGeoList.MKALLGList.Where(l => l.DistrictId == districtId).ToList();
            //            wardList = mkaGeoList.MKAWardList.Where(w => w.DistrictId == districtId).ToList();
            //            locationList = mkaGeoList.MKALocationList.Where(l => l.DistrictId == districtId).ToList();
            //        }
            //    }
            //    else
            //    {
            //        districtList = mkaGeoList.MKADistrictList.Where(d => d.ProvinceId == provinceId).ToList();
            //        llgList = mkaGeoList.MKALLGList.Where(l => l.ProvinceId == provinceId).ToList();
            //        wardList = mkaGeoList.MKAWardList.Where(w => w.ProvinceId == provinceId).ToList();
            //        locationList = mkaGeoList.MKALocationList.Where(l => l.ProvinceId == provinceId).ToList();
            //    }
            //    //}
            //    //else
            //    //{
            //    //    provinceList = geoList.ProvinceList.Where(d => d.RegionId == regionId).ToList();
            //    //    districtList = geoList.DistrictList.Where(d => d.ProvinceId == provinceId).ToList();
            //    //    llgList = geoList.LLGList.Where(l => l.ProvinceId == provinceId).ToList();
            //    //    wardList = geoList.WardList.Where(w => w.ProvinceId == provinceId).ToList();
            //    //    locationList = geoList.LocationList.Where(l => l.ProvinceId == provinceId).ToList();
            //    //}

            //    var geo = new API.Domain.GeoStructure.GeoListModel()
            //    {
            //        RegionList = regionList,
            //        ProvinceList = provinceList,
            //        DistrictList = districtList,
            //        LLGList = llgList,
            //        WardList = wardList,
            //        LocationList = locationList,
            //        Version = geoList.Version
            //    };
            //    MKAAssignData(geo);
            //}
            //else //DPO
            //{
            //    var provinceId = GlobalsModel.LoggedUser.User.Province;
            //    var districtId = GlobalsModel.LoggedUser.User.District;
            //    var llgId = GlobalsModel.LoggedUser.User.LLG;
            //    var wardId = GlobalsModel.LoggedUser.User.Ward;
            //    var locationId = GlobalsModel.LoggedUser.User.Location;
            //    var regionList = geoList.RegionList.Where(r => r.RegionId == GlobalsModel.LoggedUser.User.Region).ToList();
            //    var provinceList = geoList.ProvinceList.Where(p => p.ProvinceId == GlobalsModel.LoggedUser.User.Province).ToList();
            //    //var districtList = geoList.DistrictList.Where(d => d.DistrictId == GlobalsModel.LoggedUser.User.District).ToList();

            //    var districtList = new List<DistrictModel>();
            //    List<LLGModel> llgList = new List<LLGModel>();
            //    List<WardModel> wardList = new List<WardModel>();
            //    List<LocationModel> locationList = new List<LocationModel>();

            //    if (districtId > 0)
            //    {
            //        districtList = geoList.DistrictList.Where(d => d.DistrictId == districtId).ToList();
            //        if (llgId > 0)
            //        {
            //            llgList = geoList.LLGList.Where(l => l.LLGId == llgId).ToList();
            //            if (wardId > 0)
            //            {
            //                wardList = geoList.WardList.Where(w => w.WardId == wardId).ToList();
            //                if (locationId > 0)
            //                {
            //                    locationList = geoList.LocationList.Where(l => l.LocationId == locationId).ToList();
            //                }
            //                else
            //                {
            //                    locationList = geoList.LocationList.Where(l => l.WardId == wardId).ToList();
            //                }
            //            }
            //            else
            //            {
            //                wardList = geoList.WardList.Where(w => w.LLGId == llgId).ToList();
            //                locationList = geoList.LocationList.Where(l => l.LLGId == llgId).ToList();
            //            }
            //        }
            //        else
            //        {
            //            llgList = geoList.LLGList.Where(l => l.DistrictId == districtId).ToList();
            //            wardList = geoList.WardList.Where(w => w.DistrictId == districtId).ToList();
            //            locationList = geoList.LocationList.Where(l => l.DistrictId == districtId).ToList();
            //        }
            //    }
            //    else
            //    {
            //        districtList = geoList.DistrictList.Where(d => d.ProvinceId == provinceId).ToList();
            //        llgList = geoList.LLGList.Where(l => l.ProvinceId == provinceId).ToList();
            //        wardList = geoList.WardList.Where(w => w.ProvinceId == provinceId).ToList();
            //        locationList = geoList.LocationList.Where(l => l.ProvinceId == provinceId).ToList();
            //    }

            //    var geo = new API.Domain.GeoStructure.GeoListModel()
            //    {
            //        RegionList = regionList,
            //        ProvinceList = provinceList,
            //        DistrictList = districtList,
            //        LLGList = llgList,
            //        WardList = wardList,
            //        LocationList = locationList,
            //        Version = geoList.Version
            //    };
            //    MKAAssignData(geo);
            //}
            #endregion
        }

        /// <summary>
        /// Shows the loader.
        /// </summary>
        public void ShowLoader()
        {
            this.UseWaitCursor = true;
            this.loadingWindow.Show();
        }

        /// <summary>
        /// Hides the loader.
        /// </summary>
        public void HideLoader()
        {
            this.UseWaitCursor = false;
            this.loadingWindow.Hide();
        }

        /// <summary>
        /// Manages the breadcums.
        /// </summary>
        private void ManageBreadcums()
        {
            lblTopMessage.Text = menuName + " : " + breadCrumbName;
        }

        /// <summary>
        /// Selecteds the menu color change.
        /// </summary>
        /// <param name="selectedMenuItem">The selected menu item.</param>
        private void SelectedMenuColorChange(ToolStripItem selectedMenuItem)
        {
            this.menuFile.BackColor = ColorTranslator.FromHtml("#F2F2F2");
            this.menuMKAFile.BackColor = ColorTranslator.FromHtml("#F2F2F2");
            this.mkaFileToolStripMenuItem.BackColor = ColorTranslator.FromHtml("#F2F2F2");
            this.menuData.BackColor = ColorTranslator.FromHtml("#F2F2F2");
            this.menuAdmin.BackColor = ColorTranslator.FromHtml("#F2F2F2");
            this.menuReport.BackColor = ColorTranslator.FromHtml("#F2F2F2");
            this.mkaToolStripMenuItem.BackColor = ColorTranslator.FromHtml("#F2F2F2");
            this.menuDocumentManagement.BackColor = ColorTranslator.FromHtml("#F2F2F2");

            selectedMenuItem.BackColor = ColorTranslator.FromHtml("#C2E0FF");
        }

        /// <summary>
        /// Sets the panel status bar.
        /// </summary>
        private void setPanelStatusBar()
        {
            if (Convert.ToInt16(GlobalsModel.LoggedUser.Server) == 1)//Development
            {
                statusPanel.Text = string.Format("Hi {0}, you are currently connected to DEVELOPMENT env. on the {1} database. You can change data view to staging, preliminary and final from Data menu.",
                String.Concat(GlobalsModel.LoggedUser.User.FirstName, " ", GlobalsModel.LoggedUser.User.LastName), GlobalsModel.LoggedUser.Database);
            }
            else if (Convert.ToInt16(GlobalsModel.LoggedUser.Server) == 2)//Training
            {
                statusPanel.Text = string.Format("Hi {0}, you are currently connected to TRAINING env. on the {1} database. You can change data view to staging, preliminary and final from Data menu.",
                String.Concat(GlobalsModel.LoggedUser.User.FirstName, " ", GlobalsModel.LoggedUser.User.LastName), GlobalsModel.LoggedUser.Database);
            }
            else if (Convert.ToInt16(GlobalsModel.LoggedUser.Server) == 3)//Production
            {
                statusPanel.Text = string.Format("Hi {0}, you are currently connected to PRODUCTION env. on the {1} database. You can change data view to staging, preliminary and final from Data menu.",
                String.Concat(GlobalsModel.LoggedUser.User.FirstName, " ", GlobalsModel.LoggedUser.User.LastName), GlobalsModel.LoggedUser.Database);

                //statusPanel.Text = string.Format("Hi {0}, you are currently connected to the {1} database. You can change data view to staging, preliminary and final from Data menu.",
                // String.Concat(GlobalsModel.LoggedUser.User.FirstName, " ", GlobalsModel.LoggedUser.User.LastName), GlobalsModel.LoggedUser.Database);
            }
        }

        /// <summary>
        /// Update Global Report name 
        /// </summary>
        private void UpdateReportName()
        {
            string reportName = breadCrumbName.Replace(" / ", "_").Replace("MKA_", string.Empty);

            if (GlobalsModel.MKATreeViewStructure.MainTagName == "MKAWard")
            {
                string WardName = Globals.MKAGeoList.MKAWardList.Where(x => x.Status && x.WardId == GlobalsModel.MKATreeViewStructure.Id).Select(x => x.WardName).Distinct().SingleOrDefault();
                int WardNumber = Globals.MKAGeoList.MKAWardList.Where(x => x.Status && x.WardId == GlobalsModel.MKATreeViewStructure.Id).Select(x => x.WardNumber).Distinct().SingleOrDefault();
                GlobalsModel.reportName = reportName.Substring(reportName.IndexOf('_') + 1).Replace("/", "_").Replace(WardName, string.Format("{0}", WardName));
                //GlobalsModel.reportName = reportName.Substring(reportName.IndexOf('_') + 1).Replace("/", "_").Replace(WardName, string.Format("{0}_{1}", WardNumber, WardName));
            }
            else if (GlobalsModel.MKATreeViewStructure.MainTagName == "MKALocation")
            {
                string WardName = Globals.MKAGeoList.MKAWardList.Where(x => x.Status && x.WardId == GlobalsModel.MKATreeViewStructure.ParentId).Select(x => x.WardName).Distinct().SingleOrDefault();
                int WardNumber = Globals.MKAGeoList.MKAWardList.Where(x => x.Status && x.WardId == GlobalsModel.MKATreeViewStructure.ParentId).Select(x => x.WardNumber).Distinct().SingleOrDefault();
                GlobalsModel.reportName = reportName.Substring(reportName.IndexOf('_') + 1).Replace("/", "_").Replace(WardName, string.Format("{0}", WardName));
                //GlobalsModel.reportName = reportName.Substring(reportName.IndexOf('_') + 1).Replace("/", "_").Replace(WardName, string.Format("{0}_{1}", WardNumber, WardName));
            }
            else
            {
                GlobalsModel.reportName = reportName.Substring(reportName.IndexOf('_') + 1).Replace("/", "_");
            }

            GlobalsModel.reportName = String.Concat(GlobalsModel.reportName.Where(c => !Char.IsWhiteSpace(c)));
        }
        #endregion

        private void submenuClansToolStripMenuItem_Click(object sender, EventArgs e)
        {            

            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            pnlLeft.Visible = true;
            tvMKAGeoStructure.Hide();
            tvGeoStructure.Dock = DockStyle.Fill;
            tvGeoStructure.Show();
            tvGeoClanList mdiChildUser = new tvGeoClanList();
            mdiChildUser.MdiParent = this;
            mdiChildUser.ClientSize = new System.Drawing.Size(2000, 800);
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            mdiChildUser.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            mdiChildUser.Dock = DockStyle.Fill;
            mdiChildUser.AutoScroll = true;
            menuName = "Admin => " + sender.ToString();
            breadCrumbName = tvGeoStructure.SelectedNode.FullPath.Replace("\\", " / ");
            ManageBreadcums();
            this.SelectedMenuColorChange(this.menuAdmin);
            mdiChildUser.Show();
        }

        private void subMenMuanageWrit_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }
            pnlLeft.Visible = false;
            WritList mdiChildUser = new WritList();
            mdiChildUser.MdiParent = this;
            mdiChildUser.ClientSize = new System.Drawing.Size(2000, 800);
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            mdiChildUser.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            mdiChildUser.Dock = DockStyle.Fill;
            mdiChildUser.AutoScroll = true;
            menuName = "Admin => " + sender.ToString();
            ManageBreadcums();
            this.SelectedMenuColorChange(this.menuAdmin);
            mdiChildUser.Show();
        }

        private void subMnuUserActivity_Click(object sender, EventArgs e)
        {

            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }
            pnlLeft.Visible = false;
            UserActivityList mdiChildUser = new UserActivityList();
            mdiChildUser.MdiParent = this;
            mdiChildUser.ClientSize = new System.Drawing.Size(2000, 800);
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            mdiChildUser.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            mdiChildUser.Dock = DockStyle.Fill;
            mdiChildUser.AutoScroll = true;
            menuName = "Admin => " + sender.ToString();
            ManageBreadcums();
            this.SelectedMenuColorChange(this.menuAdmin);
            mdiChildUser.Show();
        }

        private void subMenuPushProductionDataToTraining_Click(object sender, EventArgs e)
        {
            ProductionToTraining mdiProductionToTraining = new ProductionToTraining();
            //mdiProductionToTraining.MdiParent = this;

            mdiProductionToTraining.ShowDialog();
        }
    }
}
