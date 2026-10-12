using PNGEC.API.Helper;
using System;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PNGEC.Desktop.Forms
{
    public partial class ProductionToTraining : Form
    {

        #region variable declaration

        private string[] DatabaseOperations = { "SELECT OPERATION", "ROLL DATA ONLY", "GEO DATA ONLY", "ROLL AND GEO DATA ONLY" };

        /// <summary>
        ///  is backup in progress 
        /// </summary>
        private bool isBackupInProgress = false;

        private int progressBarValue = 0;

        private string GlobleServer = string.Empty;

        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductionToTraining"/> class.
        /// </summary>
        public ProductionToTraining()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        /// <summary>
        /// Form Load Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProductionToTraining_Load(object sender, EventArgs e)
        {
            cmbDatabaseOperations.DataSource = DatabaseOperations;
        }

        /// <summary>
        /// Handles the Tick event of the tmrProductionToTraining control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void TmrProductionToTraining_Tick(object sender, EventArgs e)
        {
            //if (progressBarValue >= 90 && progressBarValue <= 99)
            //{
            //    progressBarValue = 20;
            //}
            //progressBarValue = progressBarValue + 1;
            //this.progressBar1.Value = progressBarValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnStart_Click(object sender, EventArgs e)
        {
            if (isBackupInProgress)
            {
                return;
            }

            if (cmbDatabaseOperations.SelectedIndex == 0)
            {
                MessageBox.Show("Select Operation First.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                lblStatusMessage.Text = cmbDatabaseOperations.SelectedText + " Production to Training is in progress...";
                isBackupInProgress = true;
                this.progressBar1.Value = 10;
                this.tmrProductionToTraining.Enabled = true;
                lblStatusMessage.Invalidate();
                this.progressBar1.Invalidate();

                int operationType = cmbDatabaseOperations.SelectedIndex;
                /// TODO: call API for migrate data
                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = false;
                    ExecuteTransfer(operationType);

                }).Start();

            }
        }

        private async void ExecuteTransfer(int databaseOperation)
        {

            GlobleServer = Globals.serverURL;
            try
            {
                if (databaseOperation == 1) //ROLL DATA ONLY
                {
                    lblStatusMessage.Invoke(new System.Action(() =>
                    {
                        lblStatusMessage.Text = "Staging backup Production to Training is in progress...";
                        lblStatusMessage.Invalidate();
                    }));
                    progressBar1.Invoke(new System.Action(() =>
                    {
                        progressBar1.Value = 20;
                        progressBar1.Invalidate();
                    }));

                    if (await MoveStaginDatabase())
                    {
                        if (await MoveTrainingDatabase())
                        {
                            if (await MoveFinalDatabase())
                            {
                                this.tmrProductionToTraining.Enabled = false;

                                progressBar1.Invoke(new System.Action(() =>
                                {
                                    progressBar1.Value = 100;
                                    progressBar1.Invalidate();
                                }));

                                Invoke(new System.Action(() =>
                                {
                                    MessageBox.Show("Roll Data Production to Training completed. ", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    Globals.serverURL = GlobleServer;
                                    isBackupInProgress = false;
                                    this.Close();
                                }));
                            }
                        }
                    }
                }
                else if (databaseOperation == 2) //GEO DATA ONLY
                {

                    lblStatusMessage.Invoke(new System.Action(() =>
                    {
                        lblStatusMessage.Text = "Geo backup Production to Training is in progress...";
                        lblStatusMessage.Invalidate();
                    }));

                    if (await MoveGeoDatabase())
                    {
                        this.tmrProductionToTraining.Enabled = false;
                        progressBar1.Invoke(new System.Action(() =>
                        {
                            progressBar1.Value = 100;
                            progressBar1.Invalidate();
                        }));

                        Invoke(new System.Action(() =>
                        {
                            MessageBox.Show("Geo Data Production to Training completed. ", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Globals.serverURL = GlobleServer;
                            isBackupInProgress = false;
                            this.Close();
                        }));
                    }
                }
                else if (databaseOperation == 3) //ROLL AND GEO DATA
                {
                    lblStatusMessage.Invoke(new System.Action(() =>
                    {
                        lblStatusMessage.Text = "Staging backup Production to Training is in progress...";
                        lblStatusMessage.Invalidate();
                    }));

                    if (await MoveStaginDatabase())
                    {
                        if (await MoveTrainingDatabase())
                        {
                            if (await MoveFinalDatabase())
                            {
                                if (await MoveGeoDatabase())
                                {
                                    this.tmrProductionToTraining.Enabled = false;

                                    progressBar1.Invoke(new System.Action(() =>
                                    {
                                        progressBar1.Value = 100;
                                        progressBar1.Invalidate();
                                    }));

                                    Invoke(new System.Action(() =>
                                    {
                                        MessageBox.Show("Geo and Roll Data Production to Training completed. ", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        Globals.serverURL = GlobleServer;
                                        isBackupInProgress = false;
                                        this.Close();
                                    }));
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Invoke(new System.Action(() =>
                {
                    MessageBox.Show(ex.Message, "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }));

                //this.progressBar1.Value = 0;
                //this.progressBar1.Invalidate();
                Globals.serverURL = GlobleServer;
                isBackupInProgress = false;
                this.Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (!isBackupInProgress)
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("The operation will continue in background.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
            }
        }

        #endregion

        #region Methods


        private async Task<bool> MoveStaginDatabase()
        {
            Globals.serverURL = ConfigurationManager.AppSettings["ProductionApiURL"].ToString();
            Response<Response> response = await GenericHttpClient.PostRequest<Response>("ProductionToTraining/BackupPngecStaging");
            if (response.Status)
            {
                lblStatusMessage.Invoke(new System.Action(() =>
                {
                    lblStatusMessage.Text = "Restore Staging DB from Production to Training is in progress...";
                    lblStatusMessage.Invalidate();
                }));

                Globals.serverURL = ConfigurationManager.AppSettings["TrainingApiURL"].ToString();
                try
                {
                    Response<Response> response1 = await GenericHttpClient.PostRequest<Response>("ProductionToTraining/RestorePngecStaging");
                    if (response1.Status)
                    {
                        lblStatusMessage.Invoke(new System.Action(() =>
                        {
                            lblStatusMessage.Text = "Restore Stating from Production to Training is completed...";
                            lblStatusMessage.Invalidate();
                        }));

                        progressBar1.Invoke(new System.Action(() =>
                        {
                            progressBar1.Value = 40;
                            progressBar1.Invalidate();
                        }));
                        return true;
                    }
                }
                catch (Exception ex)
                {

                }

            }
            return false;

        }

        private async Task<bool> MoveTrainingDatabase()
        {
            Globals.serverURL = ConfigurationManager.AppSettings["ProductionApiURL"].ToString();
            Response<Response> response = await GenericHttpClient.PostRequest<Response>("ProductionToTraining/BackupPngecPreliminary");
            if (response.Status)
            {

                lblStatusMessage.Invoke(new System.Action(() =>
                {
                    lblStatusMessage.Text = "Restore Preliminary DB from Production to Training is in progress...";
                    lblStatusMessage.Invalidate();
                }));

                progressBar1.Invoke(new System.Action(() =>
                {
                    progressBar1.Value = 70;
                    progressBar1.Invalidate();
                }));

                Globals.serverURL = ConfigurationManager.AppSettings["TrainingApiURL"].ToString();
                Response<Response> response1 = await GenericHttpClient.PostRequest<Response>("ProductionToTraining/RestorePngecPreliminary");
                if (response1.Status)
                {
                    lblStatusMessage.Invoke(new System.Action(() =>
                    {
                        lblStatusMessage.Text = "Restore Preliminary from Production to Training is completed...";
                        lblStatusMessage.Invalidate();
                    }));

                    return true;
                }

            }
            return false;

        }

        private async Task<bool> MoveFinalDatabase()
        {
            Globals.serverURL = ConfigurationManager.AppSettings["ProductionApiURL"].ToString();
            Response<Response> response = await GenericHttpClient.PostRequest<Response>("ProductionToTraining/BackupPngecFinal");
            if (response.Status)
            {
                lblStatusMessage.Invoke(new System.Action(() =>
                {
                    lblStatusMessage.Text = "Restore Final DB from Production to Training is in progress...";
                    lblStatusMessage.Invalidate();
                }));

                progressBar1.Invoke(new System.Action(() =>
                {
                    progressBar1.Value = 85;
                    progressBar1.Invalidate();
                }));


                Globals.serverURL = ConfigurationManager.AppSettings["TrainingApiURL"].ToString();
                Response<Response> response1 = await GenericHttpClient.PostRequest<Response>("ProductionToTraining/RestorePngecFinal");
                if (response1.Status)
                {
                    lblStatusMessage.Invoke(new System.Action(() =>
                    {
                        lblStatusMessage.Text = "Restore Final from Production to Training is completed...";
                        lblStatusMessage.Invalidate();
                    }));

                    return true;
                }

            }
            return false;

        }

        private async Task<bool> MoveGeoDatabase()
        {
            Globals.serverURL = ConfigurationManager.AppSettings["ProductionApiURL"].ToString();
            Response<Response> response = await GenericHttpClient.PostRequest<Response>("ProductionToTraining/BackupPngecCore");
            if (response.Status)
            {

                lblStatusMessage.Invoke(new System.Action(() =>
                {
                    lblStatusMessage.Text = "Restore Geo DB from Production to Training is in progress...";
                    lblStatusMessage.Invalidate();
                }));

                progressBar1.Invoke(new System.Action(() =>
                {
                    progressBar1.Value = 50;
                    progressBar1.Invalidate();
                }));

                Globals.serverURL = ConfigurationManager.AppSettings["TrainingApiURL"].ToString();
                Response<Response> response1 = await GenericHttpClient.PostRequest<Response>("ProductionToTraining/RestorePngecCore");
                if (response1.Status)
                {
                    lblStatusMessage.Invoke(new System.Action(() =>
                    {
                        lblStatusMessage.Text = "Restore Geo from Production to Training is completed...";
                        lblStatusMessage.Invalidate();
                    }));

                    progressBar1.Invoke(new System.Action(() =>
                    {
                        progressBar1.Value = 90;
                        progressBar1.Invalidate();
                    }));

                    return true;
                }

            }
            return false;

        }
        #endregion
    }
}
