using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Domain.Mobile.Voter;
using PNGEC.API.Helper;
using PNGEC.Desktop.Model;
using PNGEC.Desktop.Properties;

namespace PNGEC.Desktop.Forms
{
    public partial class VoterDetails : Form
    {
        #region variable declaration

        /// <summary>
        /// The model
        /// </summary>
        private VoterModel model;

        /// <summary>
        /// The page name
        /// </summary>
        private string pageName = string.Empty;

        /// <summary>
        /// The request model
        /// </summary>
        private CommonRequest requestModel = null;

        /// <summary>
        /// The response
        /// </summary>
        private Response response = null;

        /// <summary>
        /// The page list
        /// </summary>
        private PageList pageList = null;

        /// <summary>
        /// The BTN pager
        /// </summary>
        private Button btnPager = new Button();

        /// <summary>
        /// The page size value
        /// </summary>
        private int pageSizeValue;

        /// <summary>
        /// The page size array
        /// </summary>
        private string[] pageSizeArray = { "15", "50", "100", "500" };

        /// <summary>
        /// The system format
        /// </summary>
        private string sysFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;

        /// <summary>
        /// The rejected reason
        /// </summary>
        private RejectedReason rejectedReason = null;
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="VoterDetails"/> class.
        /// </summary>
        public VoterDetails()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VoterDetails"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public VoterDetails(VoterModel model, string pageName)
        {
            InitializeComponent();
            this.model = model;
            this.pageName = pageName;
        }
        #endregion

        #region Events
        /// <summary>
        /// Handles the Load event of the VoterDetails control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void VoterDetails_Load(object sender, EventArgs e)
        {
            if (this.model != null && this.model.ElectorId > 0)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    BindControlData(model);
                    GridViewSetting();
                }
                catch (Exception ex)
                {
                    this.Cursor = Cursors.Arrow;
                    MessageBox.Show(Resources.ResourceManager.GetString("ErrorMessage"), "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ErrorLog.Write("Data Migration", "VoterDetails_Load", ex);
                }
                finally
                {
                    this.Cursor = Cursors.Arrow;
                }
            }

            pnlShowMap.Visible = false;
        }

        /// <summary>
        /// Handles the Click event of the btnApprove control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnApprove_Click(object sender, EventArgs e)
        {
            ManageVoterStatus("Approve");
        }

        /// <summary>
        /// Handles the Click event of the btnReject control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnReject_Click(object sender, EventArgs e)
        {
            ManageVoterStatus("Reject");
        }

        /// <summary>
        /// Handles the Click event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Page_Click(object sender, EventArgs e)
        {
            btnPager = (sender as Button);
            GridViewSetting();
        }

        /// <summary>
        /// Handles the Click event of the btnSearch control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            GridViewSetting("Search");
        }

        /// <summary>
        /// Handles the Click event of the btnRefresh control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtAuditSearch.Text = string.Empty;
            GridViewSetting("Refresh");
        }

        /// Handles the SelectedIndexChanged event of the CmbPageSize control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void CmbPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            pageSizeValue = Convert.ToInt32(cmbPageSize.SelectedValue);
            GridViewSetting("PageSize");
        }

        /// <summary>
        /// Handles the Click event of the pbProfile control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void pbProfile_Click(object sender, EventArgs e)
        {
            ZomeImage zomeImage = new ZomeImage(((PictureBox)sender).Image);
            zomeImage.Width = (GlobalsModel.mainMaster.Width - 100);
            zomeImage.Height = (GlobalsModel.mainMaster.Height - 100);
            //zomeImage.StartPosition = FormStartPosition.CenterScreen;
            zomeImage.Show();
        }

        /// <summary>
        /// Handles the Click event of the pbForm11 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void pbForm11_Click(object sender, EventArgs e)
        {
            ZomeImage zomeImage = new ZomeImage(((PictureBox)sender).Image);
            zomeImage.Width = (GlobalsModel.mainMaster.Width - 100);
            zomeImage.Height = (GlobalsModel.mainMaster.Height - 100);
            //zomeImage.StartPosition = FormStartPosition.CenterScreen;
            zomeImage.Show();
        }

        /// <summary>
        /// Handles the Click event of the btnProfileDownload control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnProfileDownload_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = string.Format(@"{0}", txtElectorId.Text);
            saveFileDialog.DefaultExt = GetExtension(model.ElectorProfileImage);
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                {
                    GetImageFromUrl(model.ElectorProfileImageUrl);
                    stream.Close();
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the btnForm11Download control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnForm11Download_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = string.Format(@"{0}", txtElectorId.Text);
            saveFileDialog.DefaultExt = GetExtension(model.ElectorFormImage);
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                {
                    GetImageFromUrl(model.ElectorFormImageUrl);
                    stream.Close();
                }
            }
        }

        /// <summary>
        /// Handles the CellClick event of the dgAuditTrail control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void dgAuditTrail_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgAuditTrail.Columns[e.ColumnIndex].Name == "View")
            {
                VoterModel model = GetGridCellData(e.RowIndex);
                if (model != null)
                {
                    AuditTrialviewDetail audit = new AuditTrialviewDetail(model);
                    audit.Width = (GlobalsModel.mainMaster.Width - 100);
                    audit.Height = (GlobalsModel.mainMaster.Height - 100);
                    if (audit.ShowDialog() == DialogResult.OK)
                    {
                        //await GridViewSetting();
                    }
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the btnmap control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnmap_Click(object sender, EventArgs e)
        {
            if (pnlShowMap.Visible == false)
            {
                if ((!string.IsNullOrWhiteSpace(txtlat.Text.Trim()) && !string.IsNullOrWhiteSpace(txtlong.Text.Trim())) &&
                    (txtlat.Text.Trim() != "0.0" && txtlong.Text.Trim() != "0.0"))
                {
                    pnlShowMap.Visible = true;

                    Assembly assembly = Assembly.GetExecutingAssembly();
                    StreamReader reader = new StreamReader("Maps.htm");
                    string docText = reader.ReadToEnd();

                    docText = Regex.Replace(docText, "YOUR_API_KEY", Helper.AppSettings.MapKey)
                        .Replace("LATITUDE", txtlat.Text.Trim())
                        .Replace("LONGITUDE", txtlong.Text.Trim());
                    wbMap.DocumentText = docText;

                    lbllatlong.Text = txtlat.Text.Trim() + ", " + txtlong.Text.Trim();
                }
                else
                {
                    MessageBox.Show("There is not a proper geo location available for this record.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        #endregion

        #region Methods

        #region BindControlData
        /// <summary>
        /// Binds the control data.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <exception cref="NotImplementedException"></exception>
        private void BindControlData(VoterModel model)
        {
            // Personal Particulars
            txtElectorId.Text = model.ElectorId.ToString();
            txtFamilyNumber.Text = model.FamilyNumber;
            txtFamilyName.Text = model.FamilyName;
            txtFamilyOrderNo.Text = Convert.ToString(model.FamilySortOrder);
            txtLastName.Text = model.LName;
            txtGivenName.Text = model.FName;
            txtNationalIdCardNumber.Text = model.NatIDRecordNo;
            if (model.DOB != null)
            {
                txtDateOfBirth.Text = model.DOB.Value.ToString("dd-MM-yyyy");
                //DateTime dateOfBirth = Convert.ToDateTime(DateTime.ParseExact(Convert.ToString((model.DOB).ToShortDateString()), sysFormat, CultureInfo.InvariantCulture));
                //txtDateOfBirth.Text = dateOfBirth.ToString("dd/MM/yyyy");
            }
            //txtDateOfBirth.Text = Convert.ToDateTime(model.DOB).ToShortDateString();
            txtPlaceOfBirth.Text = model.PlaceOfBirth;
            txtProvinceOfBirth.Text = model.ProvinceOfBirth;

            if (!string.IsNullOrWhiteSpace(model.Gender))
            {
                txtGender.Text = model.Gender.Equals("M") ? "Male" : "Female";
            }

            txtOccupation.Text = model.Occupation;
            chkPersonWithDisability.Checked = model.isPWD.Equals(true) ? true : false;
            txtFathersFirstName.Text = model.FatherFName;
            txtFathersLastName.Text = model.FatherLName;
            txtMothersFirstName.Text = model.MotherFName;
            txtMothersLastName.Text = model.MotherLName;
            txtReligion.Text = model.Religion;
            if (GlobalsModel.LoggedUser.User.Role == 0 ||
                GlobalsModel.LoggedUser.User.Role == 2 ||
                GlobalsModel.LoggedUser.User.Role == 3)
            {
                pnlImage.Visible = true;
                pbProfile.Image = GetImageFromUrl(model.ElectorProfileImageUrl);
                pbForm11.Image = GetImageFromUrl(model.ElectorFormImageUrl);
            }
            else
            {
                pnlImage.Visible = false;
            }

            //if (GlobalsModel.LoggedUser.User.Role == 9 || GlobalsModel.LoggedUser.User.Role == 0 )
            //{
                pnlMKADetails.Visible = true;
            //}
            

            // Place of Residence
            txtProvince.Text = model.ProvinceName;
            txtOpenElectorate.Text = model.DistrictName;
            //txtOpenElectorate.Text = model.OpenElectorate;
            txtLLG.Text = model.LLGName;
            txtWardNoAndName.Text = model.WardName;

            // Rural
            txtLocationVillage.Text = model.LocationName;
            txtClan.Text = model.ClanName;
            txtSubClan.Text = model.SubClan;
            txtRestHouse.Text = model.RestHouse;

            // Urban
            txtSectionNo.Text = model.SectionNo;
            txtLotNo.Text = model.LotNo;
            txtStreetName.Text = model.StreetName;
            txtSuburbTownSettlement.Text = model.SuburbTownSettlement;

            //Status
            txtStatus.Text = model.Status;
            txtComment.Text = model.Comment;

            txtlat.Text = Convert.ToString(model.Lat);
            txtlong.Text = Convert.ToString(model.Long);

            //Approve/Reject Data
            txtApproveReject.Text = model.IsApproveOrReject;

            if (model.ModifiedDate != null && Convert.ToString(model.ModifiedDate) != "01-01-0001 00:00:00")
            {
                DateTime utcDate = DateTime.SpecifyKind(Convert.ToDateTime(model.ModifiedDate), DateTimeKind.Utc);
                DateTime localDate = utcDate.ToLocalTime();
                txtModifiedDatetime.Text = String.Format("{0:dd-MM-yyyy HH:mm}", localDate);
            }

            txtModifiedByName.Text = model.ModifiedByName;
            txtARLatLong.Text = model.ARLat + " , " + model.ARLong;
            txtRejectedReason.Text = model.RejectedReason;
            //lat long wise location

            //set Enrollment Number
            txtForm11Number.Text = model.EnrolmentFormNumber;
            //if (!string.IsNullOrWhiteSpace(model.EnrolmentFormNumber))
            //{
            //    string[] EnrolmentFormArray = model.EnrolmentFormNumber.Split('#');

            //    if (EnrolmentFormArray.Length == 7)
            //    {
            //        txtForm11Number.Text = EnrolmentFormArray[1] + EnrolmentFormArray[4].ToString().PadLeft(4, '0') + EnrolmentFormArray[6];
            //    }
            //}

            //MKA Fields Binding
            txtMKAClan.Text = model.MKAClanName;
            txtMKALLG.Text = model.MKALLGName;
            txtMKALocation.Text = model.MKALocationName;
            txtMKAWard.Text = model.MKAWardName;


            if (((this.pageName == "DataMigration" || this.pageName == "TransferOut" || this.pageName == "Duplicate" || this.pageName == "TransferInApproval") &&
                (GlobalsModel.LoggedUser.User.Role == 7 && GlobalsModel.LoggedUser.Database == CommonEnum.Database.Staging) ||
                (GlobalsModel.LoggedUser.User.Role == 3 && GlobalsModel.LoggedUser.Database == CommonEnum.Database.Preliminary)) &&
                txtStatus.Text != "OLD" && txtStatus.Text != "WARD ROLL")
            {
                if ((this.pageName == "DataMigration" || this.pageName == "TransferOut" || this.pageName == "Duplicate" || this.pageName == "TransferInApproval") && model.RecordType == "NORMAL")
                {
                    pnlbutton.Visible = true;
                    pnlApproveRejectInfo.Visible = true;
                }
                else
                {
                    pnlbutton.Visible = false;
                }
            }
            else
            {
                pnlbutton.Visible = false;
                pnlApproveRejectInfo.Visible = true;
            }
        }
        #endregion

        #region GridViewSetting
        /// <summary>
        /// Grids the view setting.
        /// </summary>
        private void GridViewSetting(string eventName = "")
        {
            pageList = new PageList();

            // Current Database Filed 
            pageList.DatabaseName = GlobalsModel.LoggedUser.Database.ToString();

            // Pagination Filed
            string pageIndex = string.Empty;
            if (eventName == "Refresh" || eventName == "Search" || eventName == "PageSize")
            {
                pageIndex = "1";
            }
            else
            {
                pageIndex = btnPager.Name.Equals("") ? "1" : btnPager.Name;
            }
            pageList.PageNo = int.Parse(pageIndex);
            pageList.PageSize = pageSizeValue.Equals(0) ? Convert.ToInt32(pageSizeArray.First()) : pageSizeValue;
            pageList.KeyWord = txtAuditSearch.Text;
            pageList.SortField = "ElectorId";
            pageList.SortOrder = "DESC";
            pageList.Id = Convert.ToInt64(txtElectorId.Text);

            if (pageList != null)
            {
                this.BindGrid(pageList);
            }
        }
        #endregion

        #region Bind Grid
        /// <summary>
        /// Binds the grid.
        /// </summary>
        /// <param name="model">The model.</param>
        private async void BindGrid(PageList model)
        {
            Response<VoterAuditModel> response = await GenericHttpClient.PostRequest<VoterAuditModel>("DataMigration/GetVoterAuditList", model);
            if (response.Status)
            {
                if (response.Data.ListItems.ToList() != null && response.Data.ListItems.ToList().Count > 0)
                {
                    foreach (var item in response.Data.ListItems.ToList())
                    {
                        DateTime utcDate = DateTime.SpecifyKind(item.AuditDatetime, DateTimeKind.Utc);
                        DateTime localDate = utcDate.ToLocalTime();
                        item.DateAndTime = String.Format("{0:dd-MM-yyyy HH:mm}", localDate);
                    }

                    BindVoterAuditGird(response.Data.ListItems.ToList());
                    PopulatePager(response.Data.TotalRecordCount, model.PageNo, model.PageSize);
                }
            }
        }
        #endregion

        #region Bind Voter Audit Gird
        /// <summary>
        /// Binds the voter audit gird.
        /// </summary>
        /// <param name="lstVoters">The LST voters.</param>
        private void BindVoterAuditGird(List<VoterAuditModel> lstVoters)
        {
            try
            {
                //if(lstVoters != null && lstVoters.Count > 0)
                //{
                //    foreach (var item in lstVoters)
                //    {
                //        if (!string.IsNullOrWhiteSpace(item.DateAndTime))
                //        {
                //            item.DateAndTime = Convert.ToDateTime(item.DateAndTime).ToString("dd-MM-yyyy HH:mm");
                //            //DateTime dateOfBirth = Convert.ToDateTime(DateTime.ParseExact(Convert.ToString(item.DateAndTime), sysFormat, CultureInfo.InvariantCulture));
                //            //item.DateAndTime = dateOfBirth.ToString("dd/MM/yyyy hh:mm:ss");
                //        }
                //    }
                //}

                if (this.dgAuditTrail.Columns.Count > 0)
                {
                    this.dgAuditTrail.Columns.Clear();
                }

                // Unvisible Column
                this.dgAuditTrail.DataSource = null;
                this.dgAuditTrail.DataSource = lstVoters;

                // Display Column
                this.dgAuditTrail.Columns["UserName"].HeaderText = "User Name";
                this.dgAuditTrail.Columns["GPS"].HeaderText = "GPS";
                this.dgAuditTrail.Columns["DateAndTime"].HeaderText = "Date And Time";
                this.dgAuditTrail.Columns["PreviousRecordID"].HeaderText = "Previous Record ID";
                this.dgAuditTrail.Columns["Comment"].HeaderText = "Comment";

                this.dgAuditTrail.Columns["ProvinceName"].HeaderText = "Province";
                this.dgAuditTrail.Columns["DistrictName"].HeaderText = "District";
                this.dgAuditTrail.Columns["LLGName"].HeaderText = "LLG";
                this.dgAuditTrail.Columns["WardName"].HeaderText = "Ward";
                this.dgAuditTrail.Columns["LocationName"].HeaderText = "Location";

                this.dgAuditTrail.Columns["CreatedDate"].Visible = false;

                this.dgAuditTrail.Columns["VoterName"].Visible = false;
                this.dgAuditTrail.Columns["ElectorId"].Visible = false;
                this.dgAuditTrail.Columns["OldElectorID"].Visible = false;
                this.dgAuditTrail.Columns["LocationId"].Visible = false;
                this.dgAuditTrail.Columns["WardId"].Visible = false;
                this.dgAuditTrail.Columns["LLGId"].Visible = false;
                this.dgAuditTrail.Columns["DistrictId"].Visible = false;
                this.dgAuditTrail.Columns["ProvinceId"].Visible = false;
                this.dgAuditTrail.Columns["RegionId"].Visible = false;
                this.dgAuditTrail.Columns["LName"].Visible = false;


                this.dgAuditTrail.Columns["FName"].Visible = false;
                this.dgAuditTrail.Columns["Gender"].Visible = false;
                this.dgAuditTrail.Columns["DOB"].Visible = false;
                this.dgAuditTrail.Columns["isYOB"].Visible = false;
                this.dgAuditTrail.Columns["isPWD"].Visible = false;
                this.dgAuditTrail.Columns["FatherLName"].Visible = false;
                this.dgAuditTrail.Columns["FatherFName"].Visible = false;
                this.dgAuditTrail.Columns["MotherLName"].Visible = false;
                this.dgAuditTrail.Columns["MotherFName"].Visible = false;
                this.dgAuditTrail.Columns["ProvOfBirthFK"].Visible = false;


                this.dgAuditTrail.Columns["ProvinceOfBirth"].Visible = false;
                this.dgAuditTrail.Columns["OpenElectorate"].Visible = false;
                this.dgAuditTrail.Columns["PlaceOfBirth"].Visible = false;
                this.dgAuditTrail.Columns["OccupationFK"].Visible = false;
                this.dgAuditTrail.Columns["Occupation"].Visible = false;
                this.dgAuditTrail.Columns["ClanFK"].Visible = false;
                this.dgAuditTrail.Columns["Clan"].Visible = false;
                this.dgAuditTrail.Columns["SubClan"].Visible = false;
                this.dgAuditTrail.Columns["Address"].Visible = false;
                this.dgAuditTrail.Columns["NatID"].Visible = false;


                this.dgAuditTrail.Columns["NatIDRecordNo"].Visible = false;
                this.dgAuditTrail.Columns["Status"].Visible = false;
                this.dgAuditTrail.Columns["HouseFK"].Visible = false;
                this.dgAuditTrail.Columns["HouseSortOrder"].Visible = false;
                this.dgAuditTrail.Columns["RestHouse"].Visible = false;
                this.dgAuditTrail.Columns["SectionNo"].Visible = false;
                this.dgAuditTrail.Columns["LotNo"].Visible = false;
                this.dgAuditTrail.Columns["StreetName"].Visible = false;
                this.dgAuditTrail.Columns["SuburbTownSettlement"].Visible = false;
                this.dgAuditTrail.Columns["FamilyFK"].Visible = false;

                this.dgAuditTrail.Columns["FamilySortOrder"].Visible = false;
                this.dgAuditTrail.Columns["FamilyNumber"].Visible = false;
                this.dgAuditTrail.Columns["FamilyName"].Visible = false;
                this.dgAuditTrail.Columns["dpo"].Visible = false;
                this.dgAuditTrail.Columns["Lat"].Visible = false;
                this.dgAuditTrail.Columns["Long"].Visible = false;
                this.dgAuditTrail.Columns["posted"].Visible = false;
                this.dgAuditTrail.Columns["ElectorProfileImage"].Visible = false;
                this.dgAuditTrail.Columns["ElectorProfileImageUrl"].Visible = false;


                this.dgAuditTrail.Columns["ElectorFormImage"].Visible = false;
                this.dgAuditTrail.Columns["ElectorFormImageUrl"].Visible = false;
                this.dgAuditTrail.Columns["EnrolmentFormNumber"].Visible = false;
                this.dgAuditTrail.Columns["CreatedBy"].Visible = false;
                this.dgAuditTrail.Columns["TempElectorId"].Visible = false;
                this.dgAuditTrail.Columns["RecordType"].Visible = false;
                this.dgAuditTrail.Columns["Religion"].Visible = false;
                //this.dgAuditTrail.Columns["ErrorMessage"].Visible = false;
                this.dgAuditTrail.Columns["CreatedDate"].Visible = false;
                this.dgAuditTrail.Columns["AuditDatetime"].Visible = false;
                this.dgAuditTrail.Columns["ARLat"].Visible = false;
                this.dgAuditTrail.Columns["ARLong"].Visible = false;
                this.dgAuditTrail.Columns["RejectedReason"].Visible = false;
                this.dgAuditTrail.Columns["IsApproveOrReject"].Visible = false;
                this.dgAuditTrail.Columns["ModifiedDate"].Visible = false;
                this.dgAuditTrail.Columns["ModifiedByName"].Visible = false;

                DataGridViewButtonColumn viewButton = new DataGridViewButtonColumn();
                viewButton.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                viewButton.FlatStyle = FlatStyle.Standard;
                viewButton.HeaderText = "Action";
                viewButton.Name = "View";
                viewButton.Text = "View";
                viewButton.UseColumnTextForButtonValue = true;
                viewButton.Width = 60;
                if (dgAuditTrail.Columns["View"] == null)
                {
                    dgAuditTrail.Columns.Insert(dgAuditTrail.ColumnCount - 1, viewButton);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(Resources.ResourceManager.GetString("ErrorMessage"), "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorLog.Write("List Voter", "BindVoterGird", ex);
            }
        }
        #endregion

        #region Manage Voter Status
        /// <summary>
        /// Manages the voter status.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private async void ManageVoterStatus(string isRequest)
        {
            DialogResult dialogResult = DialogResult.None;
            if (isRequest == "Approve")
            {
                dialogResult = MessageBox.Show("Are you sure you want to " + isRequest + " the record?", "PNGEC", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    dialogResult = DialogResult.OK;
                }
            }
            else
            {
                rejectedReason = new RejectedReason();
                dialogResult = rejectedReason.ShowDialog();
            }

            //DialogResult dialogResult = MessageBox.Show("Are you sure you want to " + isRequest + " the record?", "PNGEC", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //if (dialogResult == DialogResult.Yes)
            if (dialogResult == DialogResult.OK)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;

                    if (rejectedReason != null && !string.IsNullOrWhiteSpace(rejectedReason.txtRejectedReason.Text))
                    {
                        model.RejectedReason = rejectedReason.txtRejectedReason.Text;
                    }

                    model.ARLat = Convert.ToDecimal(GlobalsModel.LocationDetails.Lat);
                    model.ARLong = Convert.ToDecimal(GlobalsModel.LocationDetails.Lon);

                    requestModel = new CommonRequest();
                    requestModel.database = Convert.ToString(GlobalsModel.LoggedUser.Database);
                    requestModel.ids = txtElectorId.Text;
                    requestModel.recordType = Convert.ToString(txtStatus.Text);
                    requestModel.id = GlobalsModel.LoggedUser.User.UserId;
                    requestModel.isApproved = isRequest.Equals("Approve") ? true : false;
                    requestModel.isRejected = isRequest.Equals("Reject") ? true : false;
                    requestModel.VoterList = new List<VoterModel> { this.model };

                    response = await GenericHttpClient.PostRequest("DataMigration/ManageVoterStatus", requestModel);
                    if (response.Status)
                    {
                        this.Cursor = Cursors.Arrow;
                        MessageBox.Show("Status changed successfully", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        this.Cursor = Cursors.Arrow;
                        MessageBox.Show("Something went wrong, try again later", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    this.Cursor = Cursors.Arrow;
                    MessageBox.Show(Resources.ResourceManager.GetString("ErrorMessage"), "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ErrorLog.Write("File Form", "ManageVoterStatus", ex);
                }
            }
            //else
            //{
            //    MessageBox.Show("Something went wrong, try again later", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
        }
        #endregion

        #region Populate Pager
        /// <summary>
        /// Populates the pager.
        /// </summary>
        /// <param name="recordCount">The record count.</param>
        /// <param name="currentPage">The current page.</param>
        private void PopulatePager(int recordCount, int currentPage, int pageSize)
        {
            List<Page> pages = new List<Page>();
            int startIndex, endIndex;
            int pagerSpan = 5;

            //Calculate the Start and End Index of pages to be displayed.
            double dblPageCount = (double)(recordCount / Convert.ToDecimal(pageList.PageSize));
            int pageCount = (int)Math.Ceiling(dblPageCount);
            startIndex = currentPage > 1 && currentPage + pagerSpan - 1 < pagerSpan ? currentPage : 1;
            endIndex = pageCount > pagerSpan ? pagerSpan : pageCount;
            if (currentPage > pagerSpan % 2)
            {
                if (currentPage == 2)
                {
                    endIndex = 5;
                }
                else
                {
                    endIndex = currentPage + 2;
                }
            }
            else
            {
                endIndex = (pagerSpan - currentPage) + 1;
            }

            if (endIndex - (pagerSpan - 1) > startIndex)
            {
                startIndex = endIndex - (pagerSpan - 1);
            }

            if (endIndex > pageCount)
            {
                endIndex = pageCount;
                startIndex = ((endIndex - pagerSpan) + 1) > 0 ? (endIndex - pagerSpan) + 1 : 1;
            }

            //Add the First Page Button.
            if (currentPage > 1)
            {
                //var bitmap = new Bitmap(global::PNGEC.Desktop.Properties.Resources.first);
                pages.Add(new Page { Text = "First", Value = "1" });
            }

            //Add the Previous Button.
            if (currentPage > 5)
            {
                pages.Add(new Page { Text = "<<", Value = (currentPage - 5).ToString() });
            }

            if (currentPage > 1)
            {
                pages.Add(new Page { Text = "<", Value = (currentPage - 1).ToString() });
            }

            for (int i = startIndex; i <= endIndex; i++)
            {
                pages.Add(new Page { Text = i.ToString(), Value = i.ToString(), Selected = i == currentPage });
            }

            //Add the Next Button.
            if (currentPage < pageCount)
            {
                pages.Add(new Page { Text = ">", Value = (currentPage + 1).ToString() });
            }

            if (currentPage < pageCount)
            {
                pages.Add(new Page { Text = ">>", Value = (currentPage + 5).ToString() });
            }

            //Add the Last Button.
            if (currentPage != pageCount)
            {
                pages.Add(new Page { Text = "Last", Value = pageCount.ToString() });
            }

            //Clear existing Pager Buttons.
            pnlPaginationLeft.Controls.Clear();

            if (dgAuditTrail != null && dgAuditTrail.Rows.Count > 0)
            {
                //Loop and add Buttons for Pager.
                int count = 0;
                foreach (Page page in pages)
                {
                    Button btnPage = new Button();
                    btnPage.Location = new System.Drawing.Point(65 * count, 5);
                    btnPage.Size = new System.Drawing.Size(65, 30);
                    btnPage.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
                    btnPage.Name = page.Value;
                    btnPage.Text = page.Text;
                    btnPage.Enabled = !page.Selected;
                    btnPage.Click += new System.EventHandler(this.Page_Click);
                    pnlPaginationLeft.Controls.Add(btnPage);
                    count++;
                }
                this.cmbPageSize.SelectedIndexChanged -= this.CmbPageSize_SelectedIndexChanged;
                cmbPageSize.DataSource = pageSizeArray;
                pnlPaginationRight.Visible = true;
                this.cmbPageSize.SelectedIndexChanged += this.CmbPageSize_SelectedIndexChanged;
            }
        }
        #endregion

        #region Get Image From Url
        /// <summary>
        /// Gets the image from URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        private static Image GetImageFromUrl(string url)
        {
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
                using (HttpWebResponse httpWebReponse = (HttpWebResponse)httpWebRequest.GetResponse())
                {
                    using (Stream stream = httpWebReponse.GetResponseStream())
                    {
                        return Image.FromStream(stream);
                    }
                }
            }
            catch
            {
                return Properties.Resources.noimage;
            }
        }
        #endregion

        #region Get Extension
        /// <summary>
        /// Gets the extension.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        private string GetExtension(string filename)
        {
            if (!string.IsNullOrWhiteSpace(filename))
            {
                return ("." + Path.GetExtension(filename));
            }
            return (".png");
        }
        #endregion

        #region GetGridCellData        
        /// <summary>
        /// Gets the grid cell data.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        /// <returns></returns>
        private VoterModel GetGridCellData(int rowIndex)
        {
            VoterModel model = new VoterModel();

            model.ElectorId = Convert.ToInt64(dgAuditTrail.Rows[rowIndex].Cells["ElectorId"].Value);

            // Personal Particulars
            model.FamilyNumber = Convert.ToString(dgAuditTrail.Rows[rowIndex].Cells["FamilyNumber"].Value);
            model.FamilyName = Convert.ToString(dgAuditTrail.Rows[rowIndex].Cells["FamilyName"].Value);
            model.FamilySortOrder = Convert.ToInt32(dgAuditTrail.Rows[rowIndex].Cells["FamilySortOrder"].Value);
            model.LName = Convert.ToString(dgAuditTrail.Rows[rowIndex].Cells["LName"].Value);
            model.FName = Convert.ToString(dgAuditTrail.Rows[rowIndex].Cells["FName"].Value);
            model.NatIDRecordNo = Convert.ToString(dgAuditTrail.Rows[rowIndex].Cells["NatIDRecordNo"].Value);
            model.DOB = Convert.ToDateTime(dgAuditTrail.Rows[rowIndex].Cells["DOB"].Value);
            model.PlaceOfBirth = Convert.ToString(dgAuditTrail.Rows[rowIndex].Cells["PlaceOfBirth"].Value);
            model.ProvinceOfBirth = Convert.ToString(dgAuditTrail.Rows[rowIndex].Cells["ProvinceOfBirth"].Value);
            model.Gender = Convert.ToString(dgAuditTrail.Rows[rowIndex].Cells["Gender"].Value);
            model.Occupation = Convert.ToString(dgAuditTrail.Rows[rowIndex].Cells["Occupation"].Value);
            model.isPWD = Convert.ToBoolean(dgAuditTrail.Rows[rowIndex].Cells["isPWD"].Value);
            model.FatherFName = Convert.ToString(dgAuditTrail.Rows[rowIndex].Cells["FatherFName"].Value);
            model.FatherLName = Convert.ToString(dgAuditTrail.Rows[rowIndex].Cells["FatherLName"].Value);
            model.MotherFName = Convert.ToString(dgAuditTrail.Rows[rowIndex].Cells["MotherFName"].Value);
            model.MotherLName = Convert.ToString(dgAuditTrail.Rows[rowIndex].Cells["MotherLName"].Value);
            model.Religion = Convert.ToString(dgAuditTrail.Rows[rowIndex].Cells["Religion"].Value);
            // Place of Residence
            model.ProvinceName = Convert.ToString(dgAuditTrail.Rows[rowIndex].Cells["ProvinceName"].Value);
            model.OpenElectorate = Convert.ToString(dgAuditTrail.Rows[rowIndex].Cells["OpenElectorate"].Value);
            model.LLGName = Convert.ToString(dgAuditTrail.Rows[rowIndex].Cells["LLGName"].Value);
            model.WardName = Convert.ToString(dgAuditTrail.Rows[rowIndex].Cells["WardName"].Value);

            // Rural
            model.LocationName = Convert.ToString(dgAuditTrail.Rows[rowIndex].Cells["LocationName"].Value);
            model.Clan = Convert.ToString(dgAuditTrail.Rows[rowIndex].Cells["Clan"].Value);
            model.SubClan = Convert.ToString(dgAuditTrail.Rows[rowIndex].Cells["SubClan"].Value);
            model.RestHouse = Convert.ToString(dgAuditTrail.Rows[rowIndex].Cells["RestHouse"].Value);

            // Urban
            model.SectionNo = Convert.ToString(dgAuditTrail.Rows[rowIndex].Cells["SectionNo"].Value);
            model.LotNo = Convert.ToString(dgAuditTrail.Rows[rowIndex].Cells["LotNo"].Value);
            model.StreetName = Convert.ToString(dgAuditTrail.Rows[rowIndex].Cells["StreetName"].Value);
            model.SuburbTownSettlement = Convert.ToString(dgAuditTrail.Rows[rowIndex].Cells["SuburbTownSettlement"].Value);

            // Status
            model.Status = Convert.ToString(dgAuditTrail.Rows[rowIndex].Cells["Status"].Value);
            model.Comment = Convert.ToString(dgAuditTrail.Rows[rowIndex].Cells["Comment"].Value);

            //Approve/Reject Data
            model.IsApproveOrReject = Convert.ToString(dgAuditTrail.Rows[rowIndex].Cells["IsApproveOrReject"].Value);
            model.ModifiedDate = Convert.ToDateTime(dgAuditTrail.Rows[rowIndex].Cells["ModifiedDate"].Value);
            model.ModifiedByName = Convert.ToString(dgAuditTrail.Rows[rowIndex].Cells["ModifiedByName"].Value);
            model.ARLat = Convert.ToDecimal(dgAuditTrail.Rows[rowIndex].Cells["ARLat"].Value);
            model.ARLong = Convert.ToDecimal(dgAuditTrail.Rows[rowIndex].Cells["ARLong"].Value);
            model.RejectedReason = Convert.ToString(dgAuditTrail.Rows[rowIndex].Cells["RejectedReason"].Value);
            //model.IsApproveOrReject = Convert.ToString(dgAuditTrail.Rows[rowIndex].Cells[""].Value);

            // 
            model.ElectorProfileImage = Convert.ToString(dgAuditTrail.Rows[rowIndex].Cells["ElectorProfileImage"].Value);
            model.ElectorProfileImageUrl = Convert.ToString(dgAuditTrail.Rows[rowIndex].Cells["ElectorProfileImageUrl"].Value);

            // Elector Form Image
            model.ElectorFormImage = Convert.ToString(dgAuditTrail.Rows[rowIndex].Cells["ElectorFormImage"].Value);
            model.ElectorFormImageUrl = Convert.ToString(dgAuditTrail.Rows[rowIndex].Cells["ElectorFormImageUrl"].Value);

            model.EnrolmentFormNumber = Convert.ToString(dgAuditTrail.Rows[rowIndex].Cells["EnrolmentFormNumber"].Value);

            return model;
        }
        #endregion

        #endregion

    }
}
