using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Windows.Forms;
using Newtonsoft.Json;
using PNGEC.API.Domain.Mobile.Voter;
using PNGEC.API.Helper;
using PNGEC.Desktop.Helper;
using PNGEC.Desktop.Model;

namespace PNGEC.Desktop.Forms
{
    public partial class DownloadProvincialFile : Form
    {

        #region variable declaration

        /// <summary>
        /// The check box column
        /// </summary>
        private DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();

        /// <summary>
        /// The voter request
        /// </summary>
        private VoterRequestModel voterRequest = null;

        /// <summary>
        /// The LST province
        /// </summary>
        private List<VoterModel> lstVoter = null;

        /// <summary>
        /// The path
        /// </summary>
        private string path = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);

        /// <summary>
        /// The json response
        /// </summary>
        private Response<VoterModel> jsonResponse = null;

        /// <summary>
        /// The is load first time
        /// </summary>
        private bool isLoadFirstTime = true;

        /// <summary>
        /// Occurs when [download clicked].
        /// </summary>
        public event EventHandler<string> DownloadClicked = delegate { };

        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="DownloadProvincialFile"/> class.
        /// </summary>
        public DownloadProvincialFile()
        {
            InitializeComponent();
            InitProvinceList();
        }
        #endregion

        #region Events

        /// <summary>
        /// Handles the Load event of the DownloadProvincialFile control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void DownloadProvincialFile_Load(object sender, EventArgs e)
        {
            if (this.IsProvincialFileDownloading())
            {
                this.tmrDownLoadStatusUpdate.Enabled = true;
            }
            else
            {
                if (this.isLoadFirstTime)
                {
                    this.isLoadFirstTime = false;
                    this.BindDownloadProvincialFile();
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the btnDownloadAll control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnDownloadAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalsModel.DownloadProvinceList.Any(dp => dp.IsChecked && (dp.FileDownloadStatus != DownloadStatus.Downloading || dp.FileDownloadStatus != DownloadStatus.InQueue)))
                {
                    MessageBox.Show("Please select any non downloading province for download.", "PNGEC");
                }
                else
                {
                    using (var fbd = new FolderBrowserDialog())
                    {
                        DialogResult result = fbd.ShowDialog();
                        if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                        {
                            path = fbd.SelectedPath;

                            var downloadProvince = GlobalsModel.DownloadProvinceList.Where(x => x.IsChecked == true && (x.FileDownloadStatus != DownloadStatus.Downloading)).ToList();

                            if (GlobalsModel.DownloadProvinceList.Where(x => x.FileDownloadStatus == DownloadStatus.Downloading).Count<DownloadProvince>() > 0)
                            {
                                foreach (var item in downloadProvince)
                                {
                                    if (item.FileDownloadStatus != DownloadStatus.Downloading)
                                    {
                                        item.FileDownloadStatus = DownloadStatus.InQueue;
                                        item.FileDownloadStatusText = "In Queue";
                                        item.Path = fbd.SelectedPath;
                                        item.Percentage = "0 %";

                                        if (this.tmrDownLoadStatusUpdate.Enabled == false)
                                        {
                                            this.tmrDownLoadStatusUpdate.Enabled = true;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                foreach (var item in downloadProvince)
                                {
                                    if (downloadProvince.First() == item)
                                    {
                                        if (item.IsChecked && item.FileDownloadStatus != DownloadStatus.Downloading)
                                        {
                                            item.FileDownloadStatus = DownloadStatus.Downloading;
                                            item.FileDownloadStatusText = DownloadStatus.Downloading.ToString();
                                            item.Path = fbd.SelectedPath;
                                            item.Percentage = "0 %";

                                            if (this.tmrDownLoadStatusUpdate.Enabled == false)
                                            {
                                                this.tmrDownLoadStatusUpdate.Enabled = true;
                                            }

                                            new Thread(() =>
                                            {
                                                Thread.CurrentThread.IsBackground = true;
                                                GetVoterListByProvince(item.ProvinceId, item.ProvinceName, $"{path}\\{item.ProvinceName}.json");

                                            }).Start();
                                        }
                                    }
                                    else
                                    {
                                        item.FileDownloadStatus = DownloadStatus.InQueue;
                                        item.FileDownloadStatusText = "In Queue";
                                        item.Path = fbd.SelectedPath;
                                        item.Percentage = "0 %";
                                        if (this.tmrDownLoadStatusUpdate.Enabled == false)
                                        {
                                            this.tmrDownLoadStatusUpdate.Enabled = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                //messageBoxText = string.Empty;
                //MessageBox.Show(Resources.ResourceManager.GetString("ErrorMessage"), "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorLog.Write("DownloadProvincialFile", "btnDownloadAll_Click - Download", ex);
            }
        }

        /// <summary>
        /// Handles the CellClick event of the dgDownloadProvincialFile control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void dgDownloadProvincialFile_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgDownloadProvincialFile.Columns[e.ColumnIndex].Name == "IsChecked")
            {
                var model = (DownloadProvince)dgDownloadProvincialFile.Rows[e.RowIndex].DataBoundItem;

                if (GlobalsModel.DownloadProvinceList.Any(dp => dp.FileDownloadStatus == DownloadStatus.Downloading))
                {
                    if (!model.IsChecked && model.FileDownloadStatus != DownloadStatus.Downloading && model.FileDownloadStatus != DownloadStatus.Downloaded)
                    {
                        model.FileDownloadStatus = DownloadStatus.InQueue;
                        model.FileDownloadStatusText = "In Queue";
                    }
                    else if (model.IsChecked && model.FileDownloadStatus != DownloadStatus.Downloading && model.FileDownloadStatus != DownloadStatus.Downloaded)
                    {
                        model.FileDownloadStatus = DownloadStatus.NotStarted;
                        model.FileDownloadStatusText = "Not started";
                        model.Path = string.Empty;
                    }
                }

                model.IsChecked = !model.IsChecked;

                if (this.tmrDownLoadStatusUpdate.Enabled == false)
                {
                    this.tmrDownLoadStatusUpdate.Enabled = true;
                }
            }
        }

        /// <summary>
        /// Handles the FormClosing event of the DownloadProvincialFile control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
        private void DownloadProvincialFile_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.tmrDownLoadStatusUpdate.Enabled = false;
        }

        /// <summary>
        /// Handles the Tick event of the tmrDownLoadStatusUpdate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void tmrDownLoadStatusUpdate_Tick(object sender, EventArgs e)
        {
            try
            {
                this.Invoke(new MethodInvoker(delegate { BindDownloadProvincialFile(); }));
                if (!this.IsProvincialFileDownloading())
                {
                    this.tmrDownLoadStatusUpdate.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Write("DownloadProvincialFile", "tmrDownLoadStatusUpdate_Tick", ex);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnCheckedAll control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnCheckedAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgDownloadProvincialFile.Rows)
            {
                var model = (DownloadProvince)row.DataBoundItem;

                if (model.FileDownloadStatus != DownloadStatus.Downloading && model.FileDownloadStatus != DownloadStatus.InQueue)
                {
                    model.FileDownloadStatus = DownloadStatus.InQueue;
                    model.FileDownloadStatusText = "In Queue";
                    model.IsChecked = true;
                    if (this.tmrDownLoadStatusUpdate.Enabled == false)
                    {
                        this.tmrDownLoadStatusUpdate.Enabled = true;
                    }
                }
                if (model.FileDownloadStatus == DownloadStatus.Downloaded)
                {
                    model.IsChecked = true;

                    if (this.tmrDownLoadStatusUpdate.Enabled == false)
                    {
                        this.tmrDownLoadStatusUpdate.Enabled = true;
                    }
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the btnUncheckedAll control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnUncheckedAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgDownloadProvincialFile.Rows)
            {
                var model = (DownloadProvince)row.DataBoundItem;

                if (model.FileDownloadStatus != DownloadStatus.Downloading && model.FileDownloadStatus != DownloadStatus.Downloaded)
                {
                    model.FileDownloadStatus = DownloadStatus.NotStarted;
                    model.FileDownloadStatusText = "Not started";
                    model.IsChecked = false;
                    model.Path = string.Empty;
                    model.Percentage = "0 %";

                    if (this.tmrDownLoadStatusUpdate.Enabled == false)
                    {
                        this.tmrDownLoadStatusUpdate.Enabled = true;
                    }
                }
                else if (model.FileDownloadStatus == DownloadStatus.Downloaded)
                {
                    model.IsChecked = false;

                    if (this.tmrDownLoadStatusUpdate.Enabled == false)
                    {
                        this.tmrDownLoadStatusUpdate.Enabled = true;
                    }
                }
            }
        }

        #endregion

        #region Methods

        #region init province list
        /// <summary>
        /// Initializes the province list.
        /// </summary>
        private void InitProvinceList()
        {
            if (Globals.GeoList.ProvinceList != null && Globals.GeoList.ProvinceList.Count() > 0)
            {
                GlobalsModel.DownloadProvinceList = new List<DownloadProvince>();
                var activeProvinces = Globals.GeoList.ProvinceList.Where(p => p.Status == true).ToList();
                for (int i = 0; i < activeProvinces.Count; i++)
                {
                    GlobalsModel.DownloadProvinceList.Add(new DownloadProvince { ProvinceId = activeProvinces[i].ProvinceId, ProvinceName = activeProvinces[i].ProvinceName, FileDownloadStatus = DownloadStatus.NotStarted, Percentage = "0 %" });
                }
            }
        }
        #endregion

        #region bind download provincial file
        /// <summary>
        /// Binds the download provincial file.
        /// </summary>
        /// <param name="provinceList">The province list.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        private void BindDownloadProvincialFile()
        {
            if (this.dgDownloadProvincialFile.Columns.Count > 0)
            {
                this.dgDownloadProvincialFile.Columns.Clear();
            }

            // Unvisible Column
            this.dgDownloadProvincialFile.DataSource = null;
            this.dgDownloadProvincialFile.DataSource = GlobalsModel.DownloadProvinceList;

            // Display Column
            this.dgDownloadProvincialFile.Columns["ProvinceName"].HeaderText = "Available province";
            this.dgDownloadProvincialFile.Columns["ProvinceName"].Width = 150;
            this.dgDownloadProvincialFile.Columns["ProvinceName"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.dgDownloadProvincialFile.Columns["FileDownloadStatus"].Visible = false;

            this.dgDownloadProvincialFile.Columns["ProvinceId"].HeaderText = "Province Id";
            this.dgDownloadProvincialFile.Columns["ProvinceId"].Width = 105;
            this.dgDownloadProvincialFile.Columns["ProvinceId"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.dgDownloadProvincialFile.Columns["IsChecked"].HeaderText = "";
            this.dgDownloadProvincialFile.Columns["IsChecked"].Width = 60;

            this.dgDownloadProvincialFile.Columns["Percentage"].HeaderText = "Percentage completed";
            this.dgDownloadProvincialFile.Columns["Percentage"].Width = 85;
            this.dgDownloadProvincialFile.Columns["Percentage"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.dgDownloadProvincialFile.Columns["FileDownloadStatusText"].HeaderText = "File download status";
            this.dgDownloadProvincialFile.Columns["FileDownloadStatusText"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgDownloadProvincialFile.Columns["FileDownloadStatusText"].Width = 120;

            this.dgDownloadProvincialFile.Columns["Path"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }
        #endregion

        #region get voter list by province

        /// <summary>Gets the voter list by province.</summary>
        /// <param name="provinceId">The province identifier.</param>
        /// <param name="ProvinceName">Name of the province.</param>
        /// <param name="filePath">The file path.</param>
        //private async void GetVoterListByProvince(int provinceId, string ProvinceName, string filePath)
        //{
        //    try
        //    {
        //        lstVoter = new List<VoterModel>();
        //        int PageNo = 1;
        //        int maxFileRecords = 25000;
        //        int apiCallCount = 1;
        //        int voterListCount = 0;
        //        string NewfileName = string.Empty;
        //        string NewfilePath = string.Empty;

        //        voterRequest = new VoterRequestModel();
        //        voterRequest.UserId = GlobalsModel.LoggedUser.User.UserId;
        //        voterRequest.PageSize = 1000;
        //        voterRequest.ProvinceId = provinceId;
        //        voterRequest.DatabaseName = GlobalsModel.LoggedUser.Database.ToString();

        //        Response<VoterModel> response = new Response<VoterModel>();

        //        if (voterRequest != null)
        //        {
        //            bool DataFound = false;
        //            do
        //            {
        //                voterRequest.PageNo = PageNo;
        //                response = await GenericHttpClient.PostRequest<VoterModel>("File/GetVoterListByProvince", voterRequest);
        //                if (response.Status && response.Data.ListItems.ToList() != null && response.Data.ListItems.ToList().Count > 0)
        //                {
        //                    foreach (var item in response.Data.ListItems)
        //                    {
        //                        item.VoterName = (!string.IsNullOrWhiteSpace(item.VoterName)) ? item.VoterName.Replace("'", "''") : null;
        //                        item.LName = (!string.IsNullOrWhiteSpace(item.LName)) ? item.LName.Replace("'", "''") : null;
        //                        item.FName = (!string.IsNullOrWhiteSpace(item.FName)) ? item.FName.Replace("'", "''") : null;
        //                        item.Gender = (!string.IsNullOrWhiteSpace(item.Gender)) ? item.Gender.Replace("'", "''") : null;
        //                        item.Gen = (!string.IsNullOrWhiteSpace(item.Gen)) ? item.Gen.Replace("'", "''") : null;
        //                        item.DateOfBirth = (!string.IsNullOrWhiteSpace(item.DateOfBirth)) ? item.DateOfBirth.Replace("'", "''") : null;
        //                        item.FatherLName = (!string.IsNullOrWhiteSpace(item.FatherLName)) ? item.FatherLName.Replace("'", "''") : null;
        //                        item.FatherFName = (!string.IsNullOrWhiteSpace(item.FatherFName)) ? item.FatherFName.Replace("'", "''") : null;
        //                        item.MotherLName = (!string.IsNullOrWhiteSpace(item.MotherLName)) ? item.MotherLName.Replace("'", "''") : null;
        //                        item.MotherFName = (!string.IsNullOrWhiteSpace(item.MotherFName)) ? item.MotherFName.Replace("'", "''") : null;
        //                        item.ProvinceOfBirth = (!string.IsNullOrWhiteSpace(item.ProvinceOfBirth)) ? item.ProvinceOfBirth.Replace("'", "''") : null;
        //                        item.OpenElectorate = (!string.IsNullOrWhiteSpace(item.OpenElectorate)) ? item.OpenElectorate.Replace("'", "''") : null;
        //                        item.PlaceOfBirth = (!string.IsNullOrWhiteSpace(item.PlaceOfBirth)) ? item.PlaceOfBirth.Replace("'", "''") : null;
        //                        item.Occupation = (!string.IsNullOrWhiteSpace(item.Occupation)) ? item.Occupation.Replace("'", "''") : null;
        //                        item.Religion = (!string.IsNullOrWhiteSpace(item.Religion)) ? item.Religion.Replace("'", "''") : null;
        //                        item.Clan = (!string.IsNullOrWhiteSpace(item.Clan)) ? item.Clan.Replace("'", "''") : null;
        //                        item.SubClan = (!string.IsNullOrWhiteSpace(item.SubClan)) ? item.SubClan.Replace("'", "''") : null;
        //                        item.Address = (!string.IsNullOrWhiteSpace(item.Address)) ? item.Address.Replace("'", "''") : null;
        //                        item.NatID = (!string.IsNullOrWhiteSpace(item.NatID)) ? item.NatID.Replace("'", "''") : null;
        //                        item.NatIDRecordNo = (!string.IsNullOrWhiteSpace(item.NatIDRecordNo)) ? item.NatIDRecordNo.Replace("'", "''") : null;
        //                        item.Status = (!string.IsNullOrWhiteSpace(item.Status)) ? item.Status.Replace("'", "''") : null;
        //                        item.ProvinceName = (!string.IsNullOrWhiteSpace(item.ProvinceName)) ? item.ProvinceName.Replace("'", "''") : null;
        //                        item.DistrictName = (!string.IsNullOrWhiteSpace(item.DistrictName)) ? item.DistrictName.Replace("'", "''") : null;
        //                        item.LLGName = (!string.IsNullOrWhiteSpace(item.LLGName)) ? item.LLGName.Replace("'", "''") : null;
        //                        item.WardName = (!string.IsNullOrWhiteSpace(item.WardName)) ? item.WardName.Replace("'", "''") : null;
        //                        item.LocationName = (!string.IsNullOrWhiteSpace(item.LocationName)) ? item.LocationName.Replace("'", "''") : null;
        //                        item.RestHouse = (!string.IsNullOrWhiteSpace(item.RestHouse)) ? item.RestHouse.Replace("'", "''") : null;
        //                        item.SectionNo = (!string.IsNullOrWhiteSpace(item.SectionNo)) ? item.SectionNo.Replace("'", "''") : null;
        //                        item.LotNo = (!string.IsNullOrWhiteSpace(item.LotNo)) ? item.LotNo.Replace("'", "''") : null;
        //                        item.StreetName = (!string.IsNullOrWhiteSpace(item.StreetName)) ? item.StreetName.Replace("'", "''") : null;
        //                        item.SuburbTownSettlement = (!string.IsNullOrWhiteSpace(item.SuburbTownSettlement)) ? item.SuburbTownSettlement.Replace("'", "''") : null;
        //                        item.FamilyNumber = (!string.IsNullOrWhiteSpace(item.FamilyNumber)) ? item.FamilyNumber.Replace("'", "''") : null;
        //                        item.FamilyName = (!string.IsNullOrWhiteSpace(item.FamilyName)) ? item.FamilyName.Replace("'", "''") : null;
        //                        item.dpo = (!string.IsNullOrWhiteSpace(item.dpo)) ? item.dpo.Replace("'", "''") : null;
        //                        item.Comment = (!string.IsNullOrWhiteSpace(item.Comment)) ? item.Comment.Replace("'", "''") : null;
        //                        item.ElectorProfileImage = (!string.IsNullOrWhiteSpace(item.ElectorProfileImage)) ? item.ElectorProfileImage.Replace("'", "''") : null;
        //                        item.ElectorProfileImageUrl = (!string.IsNullOrWhiteSpace(item.ElectorProfileImageUrl)) ? item.ElectorProfileImageUrl.Replace("'", "''") : null;
        //                        item.ElectorFormImage = (!string.IsNullOrWhiteSpace(item.ElectorFormImage)) ? item.ElectorFormImage.Replace("'", "''") : null;
        //                        item.ElectorFormImageUrl = (!string.IsNullOrWhiteSpace(item.ElectorFormImageUrl)) ? item.ElectorFormImageUrl.Replace("'", "''") : null;
        //                        item.EnrolmentFormNumber = (!string.IsNullOrWhiteSpace(item.EnrolmentFormNumber)) ? item.EnrolmentFormNumber.Replace("'", "''") : null;
        //                        item.AuditDescription = (!string.IsNullOrWhiteSpace(item.AuditDescription)) ? item.AuditDescription.Replace("'", "''") : null;

        //                        item.transferInProvinceName = (!string.IsNullOrWhiteSpace(item.transferInProvinceName)) ? item.transferInProvinceName.Replace("'", "''") : null;
        //                        item.transferInDistrictName = (!string.IsNullOrWhiteSpace(item.transferInDistrictName)) ? item.transferInDistrictName.Replace("'", "''") : null;
        //                        item.transferInLLGName = (!string.IsNullOrWhiteSpace(item.transferInLLGName)) ? item.transferInLLGName.Replace("'", "''") : null;
        //                        item.transferInWardName = (!string.IsNullOrWhiteSpace(item.transferInWardName)) ? item.transferInWardName.Replace("'", "''") : null;
        //                        item.transferInLocationName = (!string.IsNullOrWhiteSpace(item.transferInLocationName)) ? item.transferInLocationName.Replace("'", "''") : null;

        //                        item.transferOutProvinceName = (!string.IsNullOrWhiteSpace(item.transferOutProvinceName)) ? item.transferOutProvinceName.Replace("'", "''") : null;
        //                        item.transferOutDistrictName = (!string.IsNullOrWhiteSpace(item.transferOutDistrictName)) ? item.transferOutDistrictName.Replace("'", "''") : null;
        //                        item.transferOutLLGName = (!string.IsNullOrWhiteSpace(item.transferOutLLGName)) ? item.transferOutLLGName.Replace("'", "''") : null;
        //                        item.transferOutWardName = (!string.IsNullOrWhiteSpace(item.transferOutWardName)) ? item.transferOutWardName.Replace("'", "''") : null;
        //                        item.transferOutLocationName = (!string.IsNullOrWhiteSpace(item.transferOutLocationName)) ? item.transferOutLocationName.Replace("'", "''") : null;

        //                        item.CreatedDate = (!string.IsNullOrWhiteSpace(item.CreatedDate)) ? item.CreatedDate.Replace("'", "''") : null;
        //                        item.ErrorMessage = (!string.IsNullOrWhiteSpace(item.ErrorMessage)) ? item.ErrorMessage.Replace("'", "''") : null;
        //                        item.RejectedReason = (!string.IsNullOrWhiteSpace(item.RejectedReason)) ? item.RejectedReason.Replace("'", "''") : null;
        //                        item.IsApproveOrReject = (!string.IsNullOrWhiteSpace(item.IsApproveOrReject)) ? item.IsApproveOrReject.Replace("'", "''") : null;
        //                        item.ModifiedByName = (!string.IsNullOrWhiteSpace(item.ModifiedByName)) ? item.ModifiedByName.Replace("'", "''") : null;
        //                        item.DuplicateLName = (!string.IsNullOrWhiteSpace(item.DuplicateLName)) ? item.DuplicateLName.Replace("'", "''") : null;
        //                        item.DuplicateFName = (!string.IsNullOrWhiteSpace(item.DuplicateFName)) ? item.DuplicateFName.Replace("'", "''") : null;
        //                        item.DuplicateVoterName = (!string.IsNullOrWhiteSpace(item.DuplicateVoterName)) ? item.DuplicateVoterName.Replace("'", "''") : null;
        //                        item.DuplicateGender = (!string.IsNullOrWhiteSpace(item.DuplicateGender)) ? item.DuplicateGender.Replace("'", "''") : null;
        //                        item.DuplicateGen = (!string.IsNullOrWhiteSpace(item.DuplicateGen)) ? item.DuplicateGen.Replace("'", "''") : null;
        //                        item.DuplicateDateOfBirth = (!string.IsNullOrWhiteSpace(item.DuplicateDateOfBirth)) ? item.DuplicateDateOfBirth.Replace("'", "''") : null;
        //                        item.DuplicateOccupation = (!string.IsNullOrWhiteSpace(item.DuplicateOccupation)) ? item.DuplicateOccupation.Replace("'", "''") : null;
        //                        item.DuplicateProvinceName = (!string.IsNullOrWhiteSpace(item.DuplicateProvinceName)) ? item.DuplicateProvinceName.Replace("'", "''") : null;
        //                        item.DuplicateDistrictName = (!string.IsNullOrWhiteSpace(item.DuplicateDistrictName)) ? item.DuplicateDistrictName.Replace("'", "''") : null;
        //                        item.DuplicateLLGName = (!string.IsNullOrWhiteSpace(item.DuplicateLLGName)) ? item.DuplicateLLGName.Replace("'", "''") : null;
        //                        item.DuplicateWardName = (!string.IsNullOrWhiteSpace(item.DuplicateWardName)) ? item.DuplicateWardName.Replace("'", "''") : null;
        //                        item.DuplicateLocationName = (!string.IsNullOrWhiteSpace(item.DuplicateLocationName)) ? item.DuplicateLocationName.Replace("'", "''") : null;
        //                        item.RecordType = (!string.IsNullOrWhiteSpace(item.RecordType)) ? item.RecordType.Replace("'", "''") : null;
        //                        item.DuplicateRecordType = (!string.IsNullOrWhiteSpace(item.DuplicateRecordType)) ? item.DuplicateRecordType.Replace("'", "''") : null;
        //                    }

        //                    lstVoter.AddRange(response.Data.ListItems.ToList());
        //                    DataFound = true;
        //                    var downloadProvince = GlobalsModel.DownloadProvinceList.FirstOrDefault(dp => dp.ProvinceId == provinceId);
        //                    if (downloadProvince != null)
        //                    {
        //                        if (response.Data.TotalRecordCount > voterRequest.PageSize)
        //                        {
        //                            var percentage = (PageNo * 100) / (response.Data.TotalRecordCount / voterRequest.PageSize);
        //                            if (percentage < 100)
        //                            {
        //                                downloadProvince.Percentage = $"{percentage} %";
        //                            }
        //                        }
        //                        else
        //                        {
        //                            var percentage = 99;
        //                            if (percentage < 100)
        //                            {
        //                                downloadProvince.Percentage = $"{percentage} %";
        //                            }
        //                        }
        //                    }

        //                    voterListCount = (voterListCount + response.Data.ListItems.Count());
        //                    if (((maxFileRecords * apiCallCount) == voterListCount) || (response.Data.TotalRecordCount == voterListCount))
        //                    {
        //                        if (lstVoter != null && lstVoter.Count > 0)
        //                        {
        //                            jsonResponse = new Response<VoterModel>();
        //                            jsonResponse.Status = true;
        //                            jsonResponse.Data = new Result<VoterModel>();
        //                            jsonResponse.Data.ListItems = lstVoter;

        //                            NewfileName = $"{Path.GetFileNameWithoutExtension(filePath)}_{apiCallCount}";
        //                            NewfilePath = filePath.Replace(Path.GetFileNameWithoutExtension(filePath), NewfileName);

        //                            using (TextWriter writer = System.IO.File.CreateText(NewfilePath))
        //                            {
        //                                var serializer = new JsonSerializer();
        //                                serializer.Serialize(writer, jsonResponse);
        //                            }

        //                            jsonResponse = null;
        //                        }

        //                        apiCallCount++;

        //                        lstVoter.Clear();
        //                        //lstVoter = null;
        //                    }

        //                    PageNo++;
        //                    response = null;
        //                }
        //                else
        //                {
        //                    DataFound = false;
        //                }

        //                #region unused
        //                //PageNo++;
        //                //response = null;
        //                #endregion

        //            } while (DataFound);
        //        }

        //        if(apiCallCount > 0 && maxFileRecords > 0)
        //        {
        //            var downloadProvince = GlobalsModel.DownloadProvinceList.FirstOrDefault(dp => dp.ProvinceId == provinceId);
        //            if (downloadProvince != null)
        //            {
        //                if(response.Status == true)
        //                {
        //                    downloadProvince.Path = filePath;
        //                    downloadProvince.Percentage = "100 %";
        //                    downloadProvince.FileDownloadStatus = DownloadStatus.Downloaded;
        //                }
        //                else
        //                {
        //                    downloadProvince.Path = string.Empty;
        //                    downloadProvince.Percentage = "0 %";
        //                    downloadProvince.FileDownloadStatus = DownloadStatus.Fail;
        //                }

        //                downloadProvince.IsChecked = false;
        //                if (this.tmrDownLoadStatusUpdate.Enabled == false)
        //                {
        //                    this.tmrDownLoadStatusUpdate.Enabled = true;
        //                }
        //            }
        //        }

        //        #region unused
        //        //if (lstVoter != null && lstVoter.Count > 0)
        //        //{
        //        //    jsonResponse = new Response<VoterModel>();
        //        //    jsonResponse.Data = new Result<VoterModel>();
        //        //    jsonResponse.Data.ListItems = lstVoter;

        //        //    using (TextWriter writer = System.IO.File.CreateText(filePath))
        //        //    {
        //        //        var serializer = new JsonSerializer();
        //        //        serializer.Serialize(writer, jsonResponse);
        //        //    }

        //        //    var downloadProvince = GlobalsModel.DownloadProvinceList.FirstOrDefault(dp => dp.ProvinceId == provinceId);
        //        //    if (downloadProvince != null)
        //        //    {
        //        //        downloadProvince.Path = filePath;
        //        //        downloadProvince.Percentage = "100 %";
        //        //        downloadProvince.FileDownloadStatus = DownloadStatus.Downloaded;
        //        //        downloadProvince.IsChecked = false;
        //        //        if (this.tmrDownLoadStatusUpdate.Enabled == false)
        //        //        {
        //        //            this.tmrDownLoadStatusUpdate.Enabled = true;
        //        //        }
        //        //    }

        //        //    jsonResponse = null;
        //        //}
        //        #endregion
        //        GCSettings.LargeObjectHeapCompactionMode = GCLargeObjectHeapCompactionMode.CompactOnce;
        //        GC.Collect();
        //    }
        //    catch (Exception ex)
        //    {
        //        var downloadProvince = GlobalsModel.DownloadProvinceList.FirstOrDefault(dp => dp.ProvinceId == provinceId);
        //        if (downloadProvince != null)
        //        {
        //            downloadProvince.FileDownloadStatus = DownloadStatus.Fail;
        //        }
        //        ErrorLog.Write("DownloadProvincialFile", "GetVoterListByProvince", ex);
        //    }
        //    finally
        //    {
        //        var downloadProvince = GlobalsModel.DownloadProvinceList.FirstOrDefault(x => x.IsChecked == true && x.FileDownloadStatus == DownloadStatus.InQueue);
        //        if (downloadProvince != null)
        //        {
        //            downloadProvince.FileDownloadStatus = DownloadStatus.Downloading;
        //            //GetVoterListByProvince(downloadProvince.ProvinceId, downloadProvince.ProvinceName, $"{path}");
        //            GetVoterListByProvince(downloadProvince.ProvinceId, downloadProvince.ProvinceName, $"{path}\\{downloadProvince.ProvinceName}.json");
        //        }

        //        lstVoter = null;
        //    }
        //}

        private async void GetVoterListByProvince(int provinceId, string ProvinceName, string filePath)
        {
            try
            {
                voterRequest = new VoterRequestModel();
                voterRequest.ProvinceId = provinceId;

                var downloadProvince = GlobalsModel.DownloadProvinceList.FirstOrDefault(dp => dp.ProvinceId == provinceId);
                if (downloadProvince != null)
                {
                    downloadProvince.Percentage = "1 %";
                }

                Response response2;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Globals.serverURL);
                    client.Timeout = new TimeSpan(0, 30, 0);
                    var response = await client.PostAsJsonAsync($"File/CreateVotersFileByProvince", voterRequest);
                    string responseBody = await response.Content.ReadAsStringAsync();
                    response2 = JsonConvert.DeserializeObject<Response>(responseBody);
                }

                //var response = await GenericHttpClient.PostRequest<Response>("File/CreateVotersFileByProvince", voterRequest);

                if (response2 != null && response2.Status)
                {
                    var downloadFileUrl = response2.Message;
                    var destinationFilePath = Path.GetFullPath(filePath.Replace(".json", ".zip"));

                    using (var client = new HttpClientDownloadWithProgress(downloadFileUrl, destinationFilePath))
                    {
                        client.ProgressChanged += (totalFileSize, totalBytesDownloaded, progressPercentage) =>
                        {
                            //Console.WriteLine($"{progressPercentage}% ({totalBytesDownloaded}/{totalFileSize})");
                            if (downloadProvince != null)
                            {
                                downloadProvince.Percentage = $"{(int)progressPercentage} %";
                            }

                            if (progressPercentage == 100)
                            {
                                downloadProvince.FileDownloadStatus = DownloadStatus.Downloaded;
                                downloadProvince.Path = filePath.Replace(".json", ".zip");
                                if (this.tmrDownLoadStatusUpdate.Enabled == false)
                                {
                                    this.tmrDownLoadStatusUpdate.Enabled = true;
                                }
                            }
                        };

                        await client.StartDownload();
                    }
                }
                else
                {
                    //downloadProvince = GlobalsModel.DownloadProvinceList.FirstOrDefault(dp => dp.ProvinceId == provinceId);
                    if (downloadProvince != null)
                    {
                        downloadProvince.Percentage = "0 %";
                        downloadProvince.FileDownloadStatus = DownloadStatus.Fail;
                    }
                }
            }
            catch (Exception ex)
            {
                var downloadProvince = GlobalsModel.DownloadProvinceList.FirstOrDefault(dp => dp.ProvinceId == provinceId);
                if (downloadProvince != null)
                {
                    downloadProvince.FileDownloadStatus = DownloadStatus.Fail;
                }
                ErrorLog.Write("DownloadProvincialFile", "GetVoterListByProvince", ex);
            }
            finally
            {
                var downloadProvince = GlobalsModel.DownloadProvinceList.FirstOrDefault(x => x.IsChecked == true && x.FileDownloadStatus == DownloadStatus.InQueue);
                if (downloadProvince != null)
                {
                    downloadProvince.FileDownloadStatus = DownloadStatus.Downloading;
                    //GetVoterListByProvince(downloadProvince.ProvinceId, downloadProvince.ProvinceName, $"{path}");
                    GetVoterListByProvince(downloadProvince.ProvinceId, downloadProvince.ProvinceName, $"{path}\\{downloadProvince.ProvinceName}.json");
                }

                lstVoter = null;
            }
        }

        #endregion

        #region is provincial file downloading
        /// <summary>
        /// Determines whether [is provincial file downloading].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is provincial file downloading]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsProvincialFileDownloading()
        {
            return GlobalsModel.DownloadProvinceList.FirstOrDefault(dp => dp.FileDownloadStatus == DownloadStatus.Downloading) == null ? false : true;
        }
        #endregion

        #endregion
    }
}
