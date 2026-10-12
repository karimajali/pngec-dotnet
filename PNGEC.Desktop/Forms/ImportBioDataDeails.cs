using Newtonsoft.Json;
using PNGEC.API.Domain.ImportBioDataDeails;
using PNGEC.API.Domain.Mobile.Voter;
using PNGEC.API.Helper;
using PNGEC.Desktop.Model;
using PNGEC.Desktop.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace PNGEC.Desktop.Forms
{
    public partial class ImportBioDataDetails : Form
    {
        #region Fields & Properties

        private int CurrentPage { set; get; } = 1;
        private int PagesCount { get; set; }
        private int LastPage { get; set; }
        private int searchcount { get; set; }
        public int LastSearchPage { get; private set; }
        public bool searchclicked { get; private set; }
        public int BioSearchCount { get; private set; }
        public int totalRecords { get; set; }
        public int firstRecord { get; private set; }
        public int lastRecord { get; set; }
        private const int exportmaxrecord = 50;
        public List<Staticfield> exportDataList { get; private set; }
        public bool IsDataEncrypted { get; private set; }
        public int filecount { get; private set; }
        public string dresult { get; private set; }
        public string plainOrEncyptedText { get; private set; }
        private const int pageRows = 50;
        private BindingList<Staticfield> UsersTemplist;
        private BindingList<Staticfield> UsersBindingBaselist;
        private BindingList<Staticfield> SearchBindinglist;
        public string encrypteddata { get; private set; }
        public static byte[] encrypted { get; private set; }
        public byte[] OutEncrypted { get; private set; }

        private List<Staticfield> UpdatedUserBaseList = new List<Staticfield>();
        private List<Staticfield> ExportTotalUserList = new List<Staticfield>();
        private static int pswdIterations = 10;
        private static string IV = "8119745113154120";
        private static string PASSWORD = "sampleText";
        private static string SALT = "exampleSalt";
        private Stream myStream;
        public int counter { get; private set; }
        public string exportFileName { get; private set; }
        public int filecounter { get; private set; }
        public List<string> ZipFileList { get; private set; }
        public string ZipFileName { get; private set; }
        private List<VoterModel> voters = null;
        private CheckBox headerCheckBox = new CheckBox();

        /// <summary>
        /// The import bio details status type
        /// </summary>
        private ImportBioDetailsStatusType importBioDetailsStatusType = null;
        #endregion

        #region Constructor
        public ImportBioDataDetails()
        {
            InitializeComponent();
        }
        #endregion

        #region Event

        private void ImportBioDataDetails_Load(object sender, EventArgs e)
        {
            this.BindRegionName();

            CurrentPage = 1;
            MaximizeBox = true;
            dgImportBioDataDetails.ColumnHeadersDefaultCellStyle.Font = new Font(dgImportBioDataDetails.ColumnHeadersDefaultCellStyle.Font, FontStyle.Bold);
            dgImportBioDataDetails.AllowUserToAddRows = false;
            Searchbtn.Enabled = false;
            tbRead.Enabled = false;
            exportButton.Enabled = false;
            btnLoadToStagingServer.Enabled = false;

            RefreshPagination();
            lblPage.Text = string.Format("Records {0}-{1} of {2}", 0, lastRecord, totalRecords);

            headerCheckBox.Click -= new EventHandler(HeaderCheckBox_Clicked);
            headerCheckBox.Click += new EventHandler(HeaderCheckBox_Clicked);
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void Browse_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog od = new OpenFileDialog();
                od.Multiselect = true;
                if (od.ShowDialog() == DialogResult.OK)
                {
                    tbRead.Enabled = true;
                    foreach (String file in od.FileNames)
                    {
                        try
                        {
                            if ((myStream = od.OpenFile()) != null)
                            {
                                using (myStream)
                                {
                                    fileListBox.Items.Add(file);
                                }
                            }
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Some Error Occurred", "Error Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Some Error Occurred", "Error Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tbRead_Click(object sender, EventArgs e)
        {
            importBioDetailsStatusType = new ImportBioDetailsStatusType();
            UpdatedUserBaseList = new List<Staticfield>();
            DialogResult dialogResult = importBioDetailsStatusType.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {

                ReadData();

                if (dgImportBioDataDetails != null && dgImportBioDataDetails.Rows != null && dgImportBioDataDetails.Rows.Count > 0)
                {
                    headerCheckBox.Visible = true;
                    exportButton.Enabled = true;
                    btnLoadToStagingServer.Enabled = true;
                }
                else
                {
                    exportButton.Enabled = false;
                    btnLoadToStagingServer.Enabled = false;
                }
            }
        }

        private void Nextbtn_Click(object sender, EventArgs e)
        {
            headerCheckBox.Checked = false;

            CurrentPage++;
            RefreshPagination();
            RebindGridForPageChange();
        }

        private void Backbtn_Click(object sender, EventArgs e)
        {
            headerCheckBox.Checked = false;

            if (CurrentPage > 1)
            {
                CurrentPage--;
                RefreshPagination();
                RebindGridForPageChange();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                RefreshPagination();
                RebindGridForPageChange();
            }
            catch (Exception)
            {
                if (dgImportBioDataDetails.DataSource == null)
                    MessageBox.Show("No Data to loaded", "Error Occurred", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                else
                    MessageBox.Show("Some Error Occurred", "Error Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Searchbox_TextChanged(object sender, EventArgs e)
        {
            RebindGridForPageChange();
            RefreshPagination();
        }

        private void Searchbtn_Click(object sender, EventArgs e)
        {
            try
            {
                searchclicked = true;
                if (!String.IsNullOrEmpty(Searchbox.Text))
                {
                    RebindGridForPageChange();
                }
                else
                {
                    MessageBox.Show("Type to search", "Search", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                RefreshPagination();
            }
            catch (Exception)
            {
                MessageBox.Show("Error Occurred", "Invalid Search", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {
        }

        private void Resetbtn_Click(object sender, EventArgs e)
        {
            try
            {
                Searchbox.Clear();
                biocheckBox1.Checked = false;
                // fileListBox.Items.Clear();
                RefreshPagination();
                CurrentPage = 1;
                RebindGridForPageChange();
                ColumnCellSize();
                dgImportBioDataDetails.ReadOnly = true;
            }
            catch (Exception)
            {
                if (dgImportBioDataDetails.DataSource == null)
                    MessageBox.Show("No Data to reset", "Error Occurred", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                else
                    MessageBox.Show("Some Error Occurred", "Error Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            ZipFileList = GetExportData();
            SaveFileDialog SaveFileDialog1 = new SaveFileDialog();
            SaveFileDialog1.ShowDialog();
            SaveFileDialog1.Title = "Save Zip File As";

            SaveFileDialog1.DefaultExt = ".zip";
            SaveFileDialog1.CheckFileExists = true;
            SaveFileDialog1.CheckPathExists = true;
            SaveFileDialog1.RestoreDirectory = true;

            string savefilename = SaveFileDialog1.FileName;

            bool emptyfilename = String.IsNullOrEmpty(savefilename);
            if (emptyfilename)
            {
                //no action
            }
            else
            {
                ZipFileName = savefilename;

                try
                {
                    bool IsZipFileExist = System.IO.File.Exists(ZipFileName);
                    if (IsZipFileExist)
                    {
                        var existzip = ZipFile.Open(ZipFileName, ZipArchiveMode.Update);
                        foreach (var file in ZipFileList)
                        {
                            // Add the entry for each file
                            existzip.CreateEntryFromFile(file, Path.GetFileName(file), CompressionLevel.Optimal);
                        }
                        //* Dispose of the object when we are done
                        existzip.Dispose();
                    }
                    else
                    {
                        var zip = ZipFile.Open(ZipFileName, ZipArchiveMode.Create);
                        foreach (var file in ZipFileList)
                        {
                            // Add the entry for each file
                            zip.CreateEntryFromFile(file, Path.GetFileName(file), CompressionLevel.Optimal);
                        }
                        //* Dispose of the object when we are done
                        zip.Dispose();
                    }
                    //Delete files on system once files are added to zip folder

                    foreach (var file in ZipFileList)
                    {
                        System.IO.File.Delete(file);
                    }
                    MessageBox.Show(Show(filecounter, "File(s) have been exported at", ZipFileName), "File Exported", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (Exception)
                {
                    MessageBox.Show("Some Error Occurred", "Error Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void cmbRegionName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbRegionName.SelectedIndex > 0)
            {
                var selectedValue = Convert.ToInt32(cmbRegionName.SelectedValue);
                var list = Globals.GeoList.ProvinceList.Where(x => x.Status && x.RegionId == selectedValue).ToList();
                list.Insert(0, new API.Domain.Province.ProvinceModel { ProvinceId = 0, ProvinceName = "Select Province" });
                cmbProvinceName.DataSource = list;
                cmbProvinceName.ValueMember = "ProvinceId";
                cmbProvinceName.DisplayMember = "ProvinceName";
            }
            else
            {
                cmbProvinceName.SelectedIndex = -1;
                cmbProvinceName.DataSource = null;
            }
        }

        private void cmbProvinceName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProvinceName.SelectedIndex > 0)
            {
                var selectedValue = Convert.ToInt32(cmbProvinceName.SelectedValue);
                var list = Globals.GeoList.DistrictList.Where(x => x.Status && x.ProvinceId == selectedValue).ToList();
                list.Insert(0, new API.Domain.District.DistrictModel { DistrictId = 0, DistrictName = "Select District" });
                cmbDistrictName.DataSource = list;
                cmbDistrictName.ValueMember = "DistrictId";
                cmbDistrictName.DisplayMember = "DistrictName";
            }
            else
            {
                cmbDistrictName.SelectedIndex = -1;
                cmbDistrictName.DataSource = null;
            }
        }

        private void cmbDistrictName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDistrictName.SelectedIndex > 0)
            {
                var selectedValue = Convert.ToInt32(cmbDistrictName.SelectedValue);
                var list = Globals.GeoList.LLGList.Where(x => x.Status && x.DistrictId == selectedValue).ToList();
                list.Insert(0, new API.Domain.LLG.LLGModel { LLGId = 0, LLGName = "Select LLG" });
                cmbLLGName.DataSource = list;
                cmbLLGName.ValueMember = "LLGId";
                cmbLLGName.DisplayMember = "LLGName";
            }
            else
            {
                cmbLLGName.SelectedIndex = -1;
                cmbLLGName.DataSource = null;
            }
        }

        private void cmbLLGName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbLLGName.SelectedIndex > 0)
            {
                var selectedValue = Convert.ToInt32(cmbLLGName.SelectedValue);
                var list = Globals.GeoList.WardList.Where(x => x.Status && x.LLGId == selectedValue).ToList();
                list.Insert(0, new API.Domain.Ward.WardModel { WardId = 0, WardNoWardName = "Select Ward" });
                cmbWardName.DataSource = list;
                cmbWardName.ValueMember = "WardId";
                cmbWardName.DisplayMember = "WardNoWardName";
            }
            else
            {
                cmbWardName.SelectedIndex = -1;
                cmbWardName.DataSource = null;
            }
        }

        private void cmbWardName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbWardName.SelectedIndex > 0)
            {
                var selectedValue = Convert.ToInt32(cmbWardName.SelectedValue);
                var list = Globals.GeoList.LocationList.Where(x => x.Status && x.WardId == selectedValue).ToList();
                list.Insert(0, new API.Domain.Location.LocationModel { LocationId = 0, LocationName = "Select Location" });
                cmbLocationName.DataSource = list;
                cmbLocationName.ValueMember = "LocationId";
                cmbLocationName.DisplayMember = "LocationName";
            }
            else
            {
                cmbLocationName.SelectedIndex = -1;
                cmbLocationName.DataSource = null;
            }
        }

        private void btnLoadToStagingServer_Click(object sender, EventArgs e)
        {
            GlobalsModel.mainMaster.ShowLoader();

            try
            {
                if (cmbLocationName.SelectedValue != null && (int)cmbLocationName.SelectedValue > 0)
                {
                    voters = new List<VoterModel>();
                    VoterModel model = null;

                    int CheckedRowsCount = 0;
                    for (int rows = 0; rows < dgImportBioDataDetails.Rows.Count; rows++)
                    {
                        DataGridViewCheckBoxCell isChecked = dgImportBioDataDetails.Rows[rows].Cells["checkBoxColumn"] as DataGridViewCheckBoxCell;
                        if (Convert.ToBoolean(isChecked.Value))
                        {
                            CheckedRowsCount += 1;
                        }
                    }

                    if (CheckedRowsCount > 0)
                    {
                        for (int rows = 0; rows < dgImportBioDataDetails.Rows.Count; rows++)
                        {
                            DataGridViewCheckBoxCell cell = dgImportBioDataDetails.Rows[rows].Cells["checkBoxColumn"] as DataGridViewCheckBoxCell;
                            if (cell.Value != null)
                            {
                                if (Convert.ToBoolean(cell.Value))
                                {
                                    //It's checked!
                                    model = new VoterModel();

                                    model.FName = Convert.ToString(dgImportBioDataDetails.Rows[rows].Cells["mFirstName"].Value);
                                    model.LName = Convert.ToString(dgImportBioDataDetails.Rows[rows].Cells["mLastName"].Value);
                                    model.NatID = Convert.ToString(dgImportBioDataDetails.Rows[rows].Cells["mNationalyID"].Value);

                                    // mLocation
                                    string str = Convert.ToString(dgImportBioDataDetails.Rows[rows].Cells["mLocationFK"].Value);
                                    int intStr; bool intResultTryParse = int.TryParse(str, out intStr);

                                    if (intResultTryParse == true)
                                    {
                                        var Location = Globals.GeoList.LocationList.Where(x => x.Status && x.LocationId == intStr).FirstOrDefault();
                                        model.RegionId = Location.RegionId;
                                        model.ProvinceId = Location.ProvinceId;
                                        model.LLGId = Location.LLGId;
                                        model.WardId = Location.WardId;
                                        model.DistrictId = Location.DistrictId;
                                        model.LocationId = Location.LocationId;
                                    }
                                    else
                                    {
                                        if (!string.IsNullOrWhiteSpace(str))
                                        {
                                            var Location = Globals.GeoList.LocationList.Where(x => x.Status && x.LocationName == str).FirstOrDefault();
                                            model.RegionId = Location.RegionId;
                                            model.ProvinceId = Location.ProvinceId;
                                            model.LLGId = Location.LLGId;
                                            model.WardId = Location.WardId;
                                            model.DistrictId = Location.DistrictId;
                                            model.LocationId = Location.LocationId;
                                        }
                                        else
                                        {
                                            int LocationId = (int)cmbLocationName.SelectedValue;
                                            var Location = Globals.GeoList.LocationList.Where(x => x.Status && x.LocationId == LocationId).FirstOrDefault();
                                            model.RegionId = Location.RegionId;
                                            model.ProvinceId = Location.ProvinceId;
                                            model.LLGId = Location.LLGId;
                                            model.WardId = Location.WardId;
                                            model.DistrictId = Location.DistrictId;
                                            model.LocationId = Location.LocationId;
                                        }
                                    }

                                    model.OccupationFK = Convert.ToInt32(dgImportBioDataDetails.Rows[rows].Cells["mOccupationFK"].Value);
                                    model.ProvOfBirthFK = Convert.ToInt32(dgImportBioDataDetails.Rows[rows].Cells["mBirthProvinceFK"].Value);
                                    if (!string.IsNullOrWhiteSpace(Convert.ToString(dgImportBioDataDetails.Rows[rows].Cells["mdisability"].Value)))
                                    {
                                        model.isPWD = Convert.ToBoolean(dgImportBioDataDetails.Rows[rows].Cells["mdisability"].Value);
                                    }
                                    else
                                    {
                                        model.isPWD = false;
                                    }

                                    #region get lat-long from GPS
                                    string GPS = Convert.ToString(dgImportBioDataDetails.Rows[rows].Cells["mGPS"].Value);
                                    if (!string.IsNullOrWhiteSpace(GPS))
                                    {
                                        string tempGPS = GPS.Replace("°", string.Empty).Replace("´", string.Empty)
                                            .Replace("″", string.Empty).Replace("'", string.Empty);

                                        string[] splitGPS = Regex.Split(tempGPS, @"\s+");

                                        var lat = ConvertDMSToDD(Convert.ToDecimal(splitGPS[0]), Convert.ToDecimal(splitGPS[1]), Convert.ToDecimal(splitGPS[2]), splitGPS[3]);
                                        var lng = ConvertDMSToDD(Convert.ToDecimal(splitGPS[4]), Convert.ToDecimal(splitGPS[5]), Convert.ToDecimal(splitGPS[6]), splitGPS[7]);

                                        model.Lat = lat;
                                        model.Long = lng;
                                    }
                                    #endregion

                                    model.EnrolmentFormNumber = Convert.ToString(dgImportBioDataDetails.Rows[rows].Cells["mEnrolmentFormNumber"].Value);
                                    //model.gps = Convert.ToString(dgImportBioDataDetails.Rows[rows].Cells["mGPS"].Value);
                                    model.ElectorProfileImage = Convert.ToString(dgImportBioDataDetails.Rows[rows].Cells["mFaceImage"].Value);
                                    model.ElectorProfileImageBinary = Convert.ToString(dgImportBioDataDetails.Rows[rows].Cells["mFaceImage"].Value);

                                    //model.ElectorFormImage = Convert.ToString(dgImportBioDataDetails.Rows[rows].Cells["mFaceTemplate"].Value);
                                    //model.SignatureImage = Convert.ToString(dgImportBioDataDetails.Rows[rows].Cells["mSignatureImage"].Value)
                                    model.ElectorSignatureImage = Convert.ToString(dgImportBioDataDetails.Rows[rows].Cells["mSignatureImage"].Value);
                                    model.ElectorSignatureImageBinary = Convert.ToString(dgImportBioDataDetails.Rows[rows].Cells["mSignatureImage"].Value);
                                    model.ElectorFingerPrintImage = Convert.ToString(dgImportBioDataDetails.Rows[rows].Cells["mFingerprintsFmdTemplate"].Value);
                                    model.Address = Convert.ToString(dgImportBioDataDetails.Rows[rows].Cells["streetAddressOne"].Value) + " " + Convert.ToString(dgImportBioDataDetails.Rows[rows].Cells["streetAddressTWo"].Value);

                                    string DOB = string.Empty;

                                    if (!string.IsNullOrWhiteSpace(Convert.ToString(dgImportBioDataDetails.Rows[rows].Cells["mDateOfBirth"].Value)))
                                    {
                                        DOB = Convert.ToDateTime(dgImportBioDataDetails.Rows[rows].Cells["mDateOfBirth"].Value).ToString("d-MMM-yy");
                                    }
                                    else
                                    {
                                        DOB = "1-JAN-2000";
                                    }

                                    model.DOB = Convert.ToDateTime(DOB);

                                    string Gen = Convert.ToString(dgImportBioDataDetails.Rows[rows].Cells["mGender"].Value);
                                    if (!string.IsNullOrWhiteSpace(Gen))
                                    {
                                        model.Gender = Gen.Equals("Male") ? "M" : "F";
                                    }
                                    else
                                    {
                                        model.Gender = Gen;
                                    }

                                    model.PlaceOfBirth = Convert.ToString(dgImportBioDataDetails.Rows[rows].Cells["mPlaceOfBirth"].Value);
                                    model.FatherFName = Convert.ToString(dgImportBioDataDetails.Rows[rows].Cells["mFatherFirstName"].Value);
                                    model.FatherLName = Convert.ToString(dgImportBioDataDetails.Rows[rows].Cells["mFatherLastName"].Value);
                                    model.MotherFName = Convert.ToString(dgImportBioDataDetails.Rows[rows].Cells["mMotherFirstName"].Value);
                                    model.MotherLName = Convert.ToString(dgImportBioDataDetails.Rows[rows].Cells["mMotherLastName"].Value);
                                    model.Clan = Convert.ToString(dgImportBioDataDetails.Rows[rows].Cells["mClan"].Value);
                                    model.Religion = Convert.ToString(dgImportBioDataDetails.Rows[rows].Cells["mReligion"].Value);

                                    string strElectorID = Convert.ToString(dgImportBioDataDetails.Rows[rows].Cells["mElectoralId"].Value);
                                    long intStrElectorID;
                                    bool int64ResultTryParse = long.TryParse(strElectorID, out intStrElectorID);

                                    if (int64ResultTryParse == true)
                                    {
                                        model.ElectorId = intStrElectorID;
                                    }
                                    else
                                    {
                                        model.ElectorId = 0;
                                    }

                                    //model.ElectorId = Convert.ToInt64(dgImportBioDataDetails.Rows[rows].Cells["mElectoralId"].Value);
                                    model.Status = Convert.ToString(dgImportBioDataDetails.Rows[rows].Cells["mEnrollmentType"].Value).ToUpper();
                                    if (!string.IsNullOrWhiteSpace(model.Status))
                                    {
                                        if (model.Status.ToUpper() == "New Enrollment".ToUpper() ||
                                            model.Status.ToUpper() == "Provisional".ToUpper() ||
                                            model.Status.ToUpper() == "New".ToUpper())
                                        {
                                            if (model.ElectorId > 0)
                                            {
                                                model.Status = "OLD";
                                            }
                                            else
                                            {
                                                model.Status = "NEW";
                                            }
                                        }
                                        else if (model.Status.ToUpper() == "Ghost".ToUpper())
                                        {
                                            model.Status = "GHOST";
                                        }
                                        else if (model.Status.ToUpper() == "Duplicate".ToUpper())
                                        {
                                            model.Status = "DUPLICATE";
                                        }
                                        else if (model.Status.ToUpper() == "Deceased".ToUpper())
                                        {
                                            model.Status = "DECEASED";
                                        }
                                        else if (model.Status.ToUpper() == "Update".ToUpper() || model.Status.ToUpper() == "Updated".ToUpper())
                                        {
                                            model.Status = "UPDATED";
                                        }
                                        else if (model.Status.ToUpper() == "Transfer IN".ToUpper() || model.Status.ToUpper() == "Transferred In".ToUpper())
                                        {
                                            model.Status = "TRANSFERRED IN";
                                        }
                                        else if (model.Status.ToUpper() == "Transfer OUT".ToUpper() || model.Status.ToUpper() == "Transferred Out".ToUpper())
                                        {
                                            model.Status = "TRANSFERRED OUT";
                                        }
                                        else if (model.Status.ToUpper() == "Under Age".ToUpper())
                                        {
                                            model.Status = "UNDER AGE";
                                        }
                                        else
                                        {
                                            //model.Status = "";
                                        }
                                    }

                                    model.CreatedBy = GlobalsModel.LoggedUser.User.UserId;

                                    if ((!string.IsNullOrWhiteSpace(model.Status) && model.Status != "OLD") && model.LocationId > 0)
                                    {
                                        if (!string.IsNullOrWhiteSpace(model.Status) && model.Status == "NEW")
                                        {
                                            if (model.ElectorId == 0)
                                            {
                                                voters.Add(model);
                                            }
                                        }
                                        else
                                        {
                                            if (model.ElectorId > 0)
                                            {
                                                voters.Add(model);
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        if (voters != null && voters.Count > 0)
                        {
                            var response = GenericHttpClient.PostRequest("mobile/Voter/ImportBioDataDetails", voters);
                            if (response.Result != null && response.Result.Status)
                            {
                                this.DialogResult = DialogResult.OK;
                                MessageBox.Show(response.Result.Message, "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show(Resources.ErrorMessage, "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("At least one checkbox must be selected.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Please select Location Name", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Write("ImportBioDataDetails", "btnLoadToStagingServer_Click", ex);
                MessageBox.Show(Resources.ResourceManager.GetString("ErrorMessage"), "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            GlobalsModel.mainMaster.HideLoader();
        }

        public decimal ConvertDMSToDD(decimal degrees, decimal minutes, decimal seconds, string direction)
        {
            if (degrees < 0)
            {
                degrees = Math.Abs(degrees);
            }
            decimal dd = degrees + minutes / 60 + seconds / (60 * 60);

            if (direction == "S" || direction == "W")
            {
                dd = dd * -1;
            } // Don't do anything for N or E
            return dd;
        }

        private void HeaderCheckBox_Clicked(object sender, EventArgs e)
        {
            //Necessary to end the edit mode of the Cell.
            dgImportBioDataDetails.EndEdit();

            //Loop and check and uncheck all row CheckBoxes based on Header Cell CheckBox.
            foreach (DataGridViewRow row in dgImportBioDataDetails.Rows)
            {
                DataGridViewCheckBoxCell checkBox = (row.Cells["checkBoxColumn"] as DataGridViewCheckBoxCell);
                checkBox.Value = headerCheckBox.Checked;
            }
        }

        private void dgImportBioDataDetails_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex == 0)
            {
                ResetHeaderCheckBoxLocation(e.ColumnIndex, e.RowIndex);
            }
        }

        private void dgImportBioDataDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgImportBioDataDetails.Columns["checkBoxColumn"].Index && e.RowIndex >= 0)
            {

                int one = UpdatedUserBaseList.Count();

                DataGridViewCheckBoxCell crntCheckBoxCell = (DataGridViewCheckBoxCell)this.dgImportBioDataDetails.Rows[e.RowIndex].Cells["checkBoxColumn"];
                if (crntCheckBoxCell.Value == null)
                {
                    crntCheckBoxCell.Value = true;
                }
                else
                {
                    crntCheckBoxCell.Value = !(bool)crntCheckBoxCell.Value;
                }

                //Loop to verify whether all row CheckBoxes are checked or not.
                bool isChecked = true;
                foreach (DataGridViewRow row in dgImportBioDataDetails.Rows)
                {
                    if (Convert.ToBoolean(row.Cells["checkBoxColumn"].EditedFormattedValue) == false)
                    {
                        isChecked = false;
                        break;
                    }
                }
                headerCheckBox.Checked = isChecked;
            }
        }

        private void dgImportBioDataDetails_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation.Equals(ScrollOrientation.HorizontalScroll))
            {
                headerCheckBox.Location = new Point(headerCheckBox.Location.X - (e.NewValue - e.OldValue), headerCheckBox.Location.Y);
            }
            if (headerCheckBox.Location.X < dgImportBioDataDetails.Location.X + 40)
            {
                headerCheckBox.Visible = false;
            }
            else
            {
                headerCheckBox.Visible = true;
            }
        }

        private void btnCLear_Click(object sender, EventArgs e)
        {
            fileListBox.Items.Clear();
            dgImportBioDataDetails.DataSource = null;
            dgImportBioDataDetails.Rows.Clear();
            if (dgImportBioDataDetails.Columns.Count > 0)
            {
                dgImportBioDataDetails.Columns.RemoveAt(dgImportBioDataDetails.Columns.Count - 1);
            }
            headerCheckBox.Visible = false;
            dgImportBioDataDetails.Refresh();
        }

        #endregion

        #region Methods

        private void RefreshPagination()
        {
            lblPage.Text = string.Format("Records {0}-{1} of {2}", firstRecord, lastRecord, totalRecords);
            Nextbtn.Enabled = Backbtn.Enabled = true;
            Resetbtn.Enabled = true;
            biocheckBox1.Enabled = true;
            Searchbtn.Enabled = true;
            //disables the next and previous buttons at start when data is not loaded
            if (CurrentPage == 1 && dgImportBioDataDetails.DataSource == null)
            {
                Backbtn.Enabled = false;
                Nextbtn.Enabled = false;
                Resetbtn.Enabled = false;
                biocheckBox1.Enabled = false;
                Searchbtn.Enabled = false;
            }
            else if (totalRecords <= pageRows)
            {
                Backbtn.Enabled = false;
                Nextbtn.Enabled = false;
            }
            else if (firstRecord.Equals(1) || CurrentPage == 1)
            {
                Backbtn.Enabled = false;
            }
            else if (lastRecord == totalRecords || CurrentPage == LastPage)
            {
                Nextbtn.Enabled = false;
            }
            else
                Nextbtn.Enabled = Backbtn.Enabled = true;
        }

        private List<Staticfield> RebindGridForPageChange()
        {
            try
            {
                SetLoading(true);
                int datasourcestartIndex = (CurrentPage - 1) * pageRows;
                firstRecord = datasourcestartIndex + 1;
                UsersTemplist = new BindingList<Staticfield>();
                ExportTotalUserList = new List<Staticfield>();

                //both filter(search and biometeric users checkbox)
                if ((biocheckBox1.Checked == true && searchclicked == true && Searchbox.Text.Length > 0))
                {
                    string UName = Searchbox.Text.ToString();
                    var SearchedBioUsers = UsersBindingBaselist.Where(user => (user.biometricID > 0) && ((user.mFirstName.ToLower().Contains(UName.ToLower())) || (user.mLastName.ToLower().Contains(UName.ToLower())) || (user.mElectoralId != null ? (user.mElectoralId.ToString()) : ("")).Contains(UName)) || (user.biometricID.ToString().Contains(UName)) || (user.mNationalyID != null ? (user.mNationalyID.ToString()) : ("")).Contains(UName));
                    SearchBindinglist = new BindingList<Staticfield>(SearchedBioUsers.ToList<Staticfield>());
                    BioSearchCount = SearchedBioUsers.Count();
                    totalRecords = BioSearchCount;
                    LastPage = Convert.ToInt32(Math.Ceiling(SearchedBioUsers.Count() * 1.0 / pageRows));

                    if (totalRecords <= pageRows || firstRecord > BioSearchCount)
                    {
                        datasourcestartIndex = 0;
                        firstRecord = 1;
                    }
                    for (int i = datasourcestartIndex; i < datasourcestartIndex + pageRows; i++)
                    {
                        if (i >= BioSearchCount)
                            break;
                        UsersTemplist.Add(SearchBindinglist[i]);
                        lastRecord = ((BioSearchCount < pageRows) || (BioSearchCount < (datasourcestartIndex + pageRows)) ? BioSearchCount : (firstRecord + (pageRows - 1)));
                    }

                    //var Templist = UsersTemplist.OrderBy(x => x.mEnrollmentType);
                    dgImportBioDataDetails.DataSource = UsersTemplist;

                    #region CheckBox
                    DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
                    checkBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    checkBoxColumn.FlatStyle = FlatStyle.Standard;
                    checkBoxColumn.ReadOnly = false;
                    checkBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    checkBoxColumn.HeaderText = string.Empty;
                    checkBoxColumn.Width = 40;
                    checkBoxColumn.Name = "checkBoxColumn";
                    if (dgImportBioDataDetails.Columns["checkBoxColumn"] == null)
                    {
                        dgImportBioDataDetails.Columns.Insert(0, checkBoxColumn);
                    };

                    Rectangle oRectangle = this.dgImportBioDataDetails.GetCellDisplayRectangle(0, -1, true);
                    Point oPoint = new Point();
                    headerCheckBox.Size = new Size(15, 15);
                    oPoint.X = oRectangle.Location.X + (oRectangle.Width - headerCheckBox.Width) / 2 + 1;
                    oPoint.Y = oRectangle.Location.Y + (oRectangle.Height - headerCheckBox.Height) / 2 + 1;
                    headerCheckBox.BackColor = Color.FromArgb(180, 180, 180);
                    //Change the location of the CheckBox to make it stay on the header
                    headerCheckBox.Location = oPoint;
                    dgImportBioDataDetails.Controls.Add(headerCheckBox);
                    #endregion

                    RefreshPagination();
                    dgImportBioDataDetails.Refresh();
                    ExportTotalUserList.AddRange(SearchBindinglist);
                    SetLoading(false);
                    return ExportTotalUserList;
                }
                //search filter
                else if (searchclicked == true && Searchbox.Text.Length > 0)
                {
                    string UName = Searchbox.Text.ToString();
                    var FilteredSearchList = UsersBindingBaselist.Where(n => (n.mFirstName.ToLower().Contains(UName.ToLower())) || (n.mLastName.ToLower().Contains(UName.ToLower())) || (n.biometricID.ToString().Contains(UName)) || (n.mElectoralId != null ? (n.mElectoralId) : ("")).Contains(UName) || (n.mNationalyID != null ? (n.mNationalyID.ToString()) : ("")).Contains(UName));

                    SearchBindinglist = new BindingList<Staticfield>(FilteredSearchList.ToList<Staticfield>());
                    searchcount = FilteredSearchList.Count();
                    totalRecords = searchcount;
                    LastSearchPage = Convert.ToInt32(Math.Ceiling(FilteredSearchList.Count() * 1.0 / pageRows));
                    if (CurrentPage >= 1 && searchcount != 0)
                    {
                        if (totalRecords <= pageRows || firstRecord > searchcount)
                        {
                            datasourcestartIndex = 0;
                            firstRecord = 1;
                        }
                        for (int i = datasourcestartIndex; i < datasourcestartIndex + pageRows; i++)
                        {
                            if (i >= searchcount)
                                break;
                            UsersTemplist.Add(SearchBindinglist[i]);

                            lastRecord = (searchcount < pageRows) || (searchcount < (firstRecord + pageRows - 1)) ? searchcount : (datasourcestartIndex + pageRows);
                        }

                        //var Templist = UsersTemplist.OrderBy(x => x.mEnrollmentType);
                        dgImportBioDataDetails.DataSource = UsersTemplist;

                        #region CheckBox
                        DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
                        checkBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        checkBoxColumn.FlatStyle = FlatStyle.Standard;
                        checkBoxColumn.ReadOnly = false;
                        checkBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        checkBoxColumn.HeaderText = string.Empty;
                        checkBoxColumn.Width = 40;
                        checkBoxColumn.Name = "checkBoxColumn";
                        if (dgImportBioDataDetails.Columns["checkBoxColumn"] == null)
                        {
                            dgImportBioDataDetails.Columns.Insert(0, checkBoxColumn);
                        };

                        Rectangle oRectangle = this.dgImportBioDataDetails.GetCellDisplayRectangle(0, -1, true);
                        Point oPoint = new Point();
                        headerCheckBox.Size = new Size(15, 15);
                        oPoint.X = oRectangle.Location.X + (oRectangle.Width - headerCheckBox.Width) / 2 + 1;
                        oPoint.Y = oRectangle.Location.Y + (oRectangle.Height - headerCheckBox.Height) / 2 + 1;
                        headerCheckBox.BackColor = Color.FromArgb(180, 180, 180);
                        //Change the location of the CheckBox to make it stay on the header
                        headerCheckBox.Location = oPoint;
                        dgImportBioDataDetails.Controls.Add(headerCheckBox);
                        #endregion

                        RefreshPagination();
                        ExportTotalUserList.AddRange(SearchBindinglist);
                        SetLoading(false);
                        return ExportTotalUserList;
                    }
                    else
                    {
                        MessageBox.Show("No Such Record Found!");
                    }
                }
                //only checkbox
                else if (biocheckBox1.Checked == true && Searchbox.Text.Length == 0 || biocheckBox1.Checked)
                {
                    var BiofilteredUsers = UsersBindingBaselist.Where(user => user.biometricID > 0);
                    SearchBindinglist = new BindingList<Staticfield>(BiofilteredUsers.ToList<Staticfield>());
                    BioSearchCount = BiofilteredUsers.Count();
                    totalRecords = BioSearchCount;
                    LastPage = Convert.ToInt32(Math.Ceiling(BiofilteredUsers.Count() * 1.0 / pageRows));
                    if (totalRecords <= pageRows || firstRecord > BioSearchCount)
                    {
                        datasourcestartIndex = 0;
                        firstRecord = 1;
                    }
                    for (int i = datasourcestartIndex; i < datasourcestartIndex + pageRows; i++)
                    {
                        if (i >= BioSearchCount)
                            break;
                        UsersTemplist.Add(SearchBindinglist[i]);
                        lastRecord = BioSearchCount < pageRows ? BioSearchCount : (firstRecord + pageRows - 1);
                    }

                    //var Templist = UsersTemplist.OrderBy(x => x.mEnrollmentType);
                    dgImportBioDataDetails.DataSource = UsersTemplist;

                    #region CheckBox
                    DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
                    checkBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    checkBoxColumn.FlatStyle = FlatStyle.Standard;
                    checkBoxColumn.ReadOnly = false;
                    checkBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    checkBoxColumn.HeaderText = string.Empty;
                    checkBoxColumn.Width = 40;
                    checkBoxColumn.Name = "checkBoxColumn";
                    if (dgImportBioDataDetails.Columns["checkBoxColumn"] == null)
                    {
                        dgImportBioDataDetails.Columns.Insert(0, checkBoxColumn);
                    };

                    Rectangle oRectangle = this.dgImportBioDataDetails.GetCellDisplayRectangle(0, -1, true);
                    Point oPoint = new Point();
                    headerCheckBox.Size = new Size(15, 15);
                    oPoint.X = oRectangle.Location.X + (oRectangle.Width - headerCheckBox.Width) / 2 + 1;
                    oPoint.Y = oRectangle.Location.Y + (oRectangle.Height - headerCheckBox.Height) / 2 + 1;
                    headerCheckBox.BackColor = Color.FromArgb(180, 180, 180);
                    ////Change the location of the CheckBox to make it stay on the header
                    headerCheckBox.Location = oPoint;

                    //DataGridViewHeaderCell header = dgImportBioDataDetails.Columns["checkBoxColumn"].HeaderCell;
                    //headerCheckBox.Location = new Point(
                    //    header.ContentBounds.Left + (header.ContentBounds.Right - header.ContentBounds.Left + headerCheckBox.Size.Width) / 2,
                    //    header.ContentBounds.Top + (header.ContentBounds.Bottom - header.ContentBounds.Top + headerCheckBox.Size.Height) / 2
                    //);
                    dgImportBioDataDetails.Controls.Add(headerCheckBox);

                    #endregion

                    RefreshPagination();
                    dgImportBioDataDetails.Refresh();

                    ExportTotalUserList.AddRange(SearchBindinglist);
                    SetLoading(false);
                    return ExportTotalUserList;
                }
                else
                {
                    for (int i = datasourcestartIndex; i < datasourcestartIndex + pageRows; i++)
                    {
                        if (i >= UsersBindingBaselist.Count)
                            break;
                        UsersTemplist.Add(UsersBindingBaselist[i]);
                        lastRecord = UsersBindingBaselist.Count < pageRows ? UsersBindingBaselist.Count : (firstRecord + pageRows - 1);
                    }
                    totalRecords = UsersBindingBaselist.Count;
                    LastPage = Convert.ToInt32(Math.Ceiling(totalRecords * 1.0 / pageRows));

                    //var Templist = UsersTemplist.OrderBy(x => x.mEnrollmentType);
                    dgImportBioDataDetails.DataSource = UsersTemplist;

                    #region CheckBox
                    DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
                    checkBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    checkBoxColumn.FlatStyle = FlatStyle.Standard;
                    checkBoxColumn.ReadOnly = false;
                    checkBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    checkBoxColumn.HeaderText = string.Empty;
                    checkBoxColumn.Width = 40;
                    checkBoxColumn.Name = "checkBoxColumn";
                    if (dgImportBioDataDetails.Columns["checkBoxColumn"] == null)
                    {
                        dgImportBioDataDetails.Columns.Insert(0, checkBoxColumn);
                    };

                    Rectangle oRectangle = this.dgImportBioDataDetails.GetCellDisplayRectangle(0, -1, true);
                    Point oPoint = new Point();
                    headerCheckBox.Size = new Size(15, 15);
                    oPoint.X = oRectangle.Location.X + (oRectangle.Width - headerCheckBox.Width) / 2 + 1;
                    oPoint.Y = oRectangle.Location.Y + (oRectangle.Height - headerCheckBox.Height) / 2 + 1;
                    headerCheckBox.BackColor = Color.FromArgb(180, 180, 180);
                    //Change the location of the CheckBox to make it stay on the header
                    headerCheckBox.Location = oPoint;
                    dgImportBioDataDetails.Controls.Add(headerCheckBox);
                    #endregion

                    ColumnCellSize();
                    RefreshPagination();
                    dgImportBioDataDetails.Refresh();
                    SearchBindinglist = UsersBindingBaselist;
                    ExportTotalUserList.AddRange(SearchBindinglist);
                    SetLoading(false);
                    return ExportTotalUserList;
                }

                SetLoading(false);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                MessageBox.Show("Some Error Occurred", "Error Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            SetLoading(false);
            return ExportTotalUserList;
        }

        private void ColumnCellSize()
        {
            dgImportBioDataDetails.Columns[0].Width = 100;
            dgImportBioDataDetails.Columns[1].Width = 120;
            dgImportBioDataDetails.Columns[3].Width = 120;
            dgImportBioDataDetails.Columns[8].Width = 120;
            dgImportBioDataDetails.Columns[16].Width = 150;
            dgImportBioDataDetails.Columns[17].Width = 150;
            dgImportBioDataDetails.Columns[18].Width = 120;
            dgImportBioDataDetails.Columns[21].Width = 150;
            dgImportBioDataDetails.Columns[23].Width = 120;
            dgImportBioDataDetails.Columns[24].Width = 150;
            dgImportBioDataDetails.Columns[25].Width = 120;
        }

        private byte[] EncodeAndEncrypt(byte[] bytearraytoencrypt)
        {
            AesCryptoServiceProvider dataencrypt = new AesCryptoServiceProvider();
            var d = GetCryptoTransform(dataencrypt, true);
            dataencrypt.BlockSize = 128;
            dataencrypt.KeySize = 128;
            dataencrypt.Padding = PaddingMode.PKCS7;
            dataencrypt.Mode = CipherMode.CBC;
            byte[] encrypteddata = d.TransformFinalBlock(bytearraytoencrypt, 0, bytearraytoencrypt.Length);
            d.Dispose();
            return encrypteddata;
        }

        private static string DecodeAndDecrypt(string encrypted)
        {
            try
            {
                using (var csp = new AesCryptoServiceProvider())
                {
                    var d = GetCryptoTransform(csp, false);
                    byte[] output = Convert.FromBase64String(encrypted);
                    byte[] decryptedOutput = d.TransformFinalBlock(output, 0, output.Length);
                    string decrypted = Encoding.UTF8.GetString(decryptedOutput);
                    return decrypted;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        private static ICryptoTransform GetCryptoTransform(AesCryptoServiceProvider csp, bool encrypting)
        {
            csp.Mode = CipherMode.CBC;
            csp.Padding = PaddingMode.PKCS7;
            var spec = new Rfc2898DeriveBytes(Encoding.UTF8.GetBytes(PASSWORD), Encoding.UTF8.GetBytes(SALT), pswdIterations);
            byte[] key = spec.GetBytes(16);
            csp.IV = Encoding.UTF8.GetBytes(IV);
            csp.Key = key;
            if (encrypting)
            {
                return csp.CreateEncryptor();
            }
            return csp.CreateDecryptor();
        }

        private void SetLoading(bool displayLoader)
        {
            if (displayLoader)
            {
                Invoke((MethodInvoker)delegate
                {
                    Cursor = System.Windows.Forms.Cursors.WaitCursor;
                });
            }
            else
            {
                Invoke((MethodInvoker)delegate
                {
                    Cursor = System.Windows.Forms.Cursors.Default;
                });
            }
        }

        private List<string> GetExportData()
        {
            exportDataList = RebindGridForPageChange();
            Details Sobject = new Details();
            List<string> FilenamesList = new List<string>();
            counter = 1;
            //string ZipFileName = string.Format(@"D:\ExportFilesZip-{0:yyyy-MM-dd_hh-mm-ss-tt}",
            //                                         DateTime.Now);

            do
            {
                int dataindex = (counter - 1) * exportmaxrecord;
                Sobject.staticfield = new List<Staticfield>();
                for (int i = dataindex; i < dataindex + exportmaxrecord; i++)
                {
                    if (i >= exportDataList.Count)
                    {
                        break;
                    }
                    Sobject.staticfield.Add(exportDataList[i]);
                }
                string strserialize = JsonConvert.SerializeObject(Sobject);
                exportFileName = string.Format("Exported_Result_File{0}-{1:yyyy-MM-dd_hh-mm-ss-tt}.json", counter,
                                                          DateTime.Now);
                byte[] encryptedbytes = Encoding.UTF8.GetBytes(strserialize);
                OutEncrypted = EncodeAndEncrypt(encryptedbytes);
                string enryptedstring = System.Convert.ToBase64String(OutEncrypted);
                System.IO.File.WriteAllText(exportFileName, enryptedstring);
                counter++;
                FilenamesList.Add(exportFileName);
                filecounter = (exportDataList.Count % exportmaxrecord) == 0 ? ((exportDataList.Count / exportmaxrecord)) : ((exportDataList.Count / exportmaxrecord) + 1);

            } while (counter <= (filecounter));

            return FilenamesList;
        }

        private void ReadData()
        {
            try
            {
                SetLoading(true);
                foreach (var item in fileListBox.Items)
                {
                    FileInfo fi1 = new FileInfo(item.ToString());
                    var stream = System.IO.File.OpenText(item.ToString());
                    if (stream.BaseStream.Length > 0 && fi1.Extension == ".json")
                    {
                        plainOrEncyptedText = stream.ReadToEnd();

                        if (plainOrEncyptedText.StartsWith("{")) //json file format always starts with "{"
                        {
                            Details rootobj = JsonConvert.DeserializeObject<Details>(plainOrEncyptedText);
                            List<Staticfield> UsersBaselist = rootobj.staticfield;
                            UpdatedUserBaseList.AddRange(UsersBaselist);
                            IsDataEncrypted = false;
                        }
                        else
                        {
                            dresult = DecodeAndDecrypt(plainOrEncyptedText);
                            Details rootobj = JsonConvert.DeserializeObject<Details>(dresult);
                            List<Staticfield> UsersBaselist = rootobj.staticfield;
                            UpdatedUserBaseList.AddRange(UsersBaselist);
                            IsDataEncrypted = true;
                        }
                    }

                    if (UpdatedUserBaseList != null && UpdatedUserBaseList.Count > 0)
                    {
                        foreach (Staticfield field in UpdatedUserBaseList)
                        {
                            if (field.mCustomFieldValues != null && field.mCustomFieldValues.Count > 0)
                            {
                                foreach (CustomField custField in field.mCustomFieldValues)
                                {
                                    if (custField.headerName == "Form 11")
                                    {
                                        field.mEnrolmentFormNumber = custField.headerValues;
                                    }
                                }
                            }
                        }
                    }

                    if (importBioDetailsStatusType.rdbNew.Checked == true)
                    {
                        UpdatedUserBaseList = UpdatedUserBaseList.Where(c => c.mEnrollmentType != null && c.mEnrollmentType.ToUpper() == "New Enrollment".ToUpper()).ToList();
                        UsersBindingBaselist = new BindingList<Staticfield>(UpdatedUserBaseList);
                    }
                    else
                    {
                        UsersBindingBaselist = new BindingList<Staticfield>(UpdatedUserBaseList);
                    }


                    CurrentPage = 1;
                    RebindGridForPageChange();
                    PagesCount = Convert.ToInt32(Math.Ceiling(UsersBindingBaselist.Count * 1.0 / pageRows));
                    LastPage = PagesCount;
                    ColumnCellSize();
                    dgImportBioDataDetails.ReadOnly = true;
                    RefreshPagination();
                }
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("File invalid or No File Selected", "Invalid File", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (ArgumentException)
            {
                MessageBox.Show("File invalid or No File Selected", "Invalid File", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show("Some Error Occurred", "Error Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            exportButton.Enabled = true;
            SetLoading(false);
        }

        private static string Show(int counter, string message, string ZipFileName)
        {
            return String.Format("{0} {1}-{2}", counter, message, ZipFileName);
        }

        private void BindRegionName()
        {
            var list = Globals.GeoList.RegionList.Where(x => x.Status).ToList();
            list.Insert(0, new API.Domain.Region.RegionModel { RegionId = 0, RegionName = "Select Region" });
            cmbRegionName.DataSource = list;
            cmbRegionName.ValueMember = "RegionId";
            cmbRegionName.DisplayMember = "RegionName";
        }

        private void ClearCheckbox()
        {
            headerCheckBox.Checked = false;

            //Loop and check and uncheck all row CheckBoxes based on Header Cell CheckBox.
            foreach (DataGridViewRow row in dgImportBioDataDetails.Rows)
            {
                DataGridViewCheckBoxCell checkBox = (row.Cells["checkBoxColumn"] as DataGridViewCheckBoxCell);
                checkBox.Value = headerCheckBox.Checked;
            }
        }

        private void ResetHeaderCheckBoxLocation(int ColumnIndex, int RowIndex)
        {
            //Get the column header cell bounds
            Rectangle oRectangle = this.dgImportBioDataDetails.GetCellDisplayRectangle(ColumnIndex, RowIndex, true);

            Point oPoint = new Point();

            oPoint.X = oRectangle.Location.X + (oRectangle.Width - headerCheckBox.Width) / 2 + 1;
            oPoint.Y = oRectangle.Location.Y + (oRectangle.Height - headerCheckBox.Height) / 2 + 1;

            //Change the location of the CheckBox to make it stay on the header
            headerCheckBox.Location = oPoint;
        }
        #endregion
    }
}
