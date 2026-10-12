using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using PNGEC.API.Domain.Mobile.General;
using PNGEC.API.Domain.Mobile.Voter;
using PNGEC.API.Helper;
using PNGEC.Desktop.Model;
using PNGEC.Desktop.Properties;

namespace PNGEC.Desktop.Forms
{
    public partial class ROVoterDetails : Form
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
        /// The BTN pager
        /// </summary>
        private Button btnPager = new Button();

        /// <summary>
        /// The status array
        /// </summary>
        private string[] StatusArray = { "Select Status", "OLD", "NEW", "UPDATED", "DECEASED", "GHOST", "DUPLICATE", "UNDER AGE" };

        /// <summary>
        /// The page size array
        /// </summary>
        private string[] pageSizeArray = { "15", "50", "100", "500" };

        /// <summary>
        /// The system format
        /// </summary>
        private string sysFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;

        /// <summary>
        /// The olist
        /// </summary>
        private List<OccupationModel> olist = null;
        /// <summary>
        /// The ward model
        /// </summary>
        //private API.Domain.Ward.WardModel wardModel = null;
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ROVoterDetails"/> class.
        /// </summary>
        public ROVoterDetails()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ROVoterDetails"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public ROVoterDetails(VoterModel model, string pageName)
        {
            InitializeComponent();
            this.model = model;
            this.pageName = pageName;
        }
        #endregion

        #region Events
        /// <summary>
        /// Handles the Load event of the ROVoterDetails control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void ROVoterDetails_Load(object sender, EventArgs e)
        {
            dtpDateOfBirth.MaxDate = DateTime.Now.Date;

            if (this.model != null && this.model.ElectorId > 0)
            {
                try
                {
                    pnlShowMap.Visible = false;

                    //GlobalsModel.mainMaster.ShowLoader();
                    this.Cursor = Cursors.WaitCursor;

                    await BindCombobox();
                    BindControlData(model);
                }
                catch (Exception ex)
                {
                    this.Cursor = Cursors.Arrow;
                    MessageBox.Show(Resources.ResourceManager.GetString("ErrorMessage"), "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ErrorLog.Write("Data Migration", "ROVoterDetails_Load", ex);
                }
                finally
                {
                    //GlobalsModel.mainMaster.HideLoader();
                    this.Cursor = Cursors.Arrow;
                }
            }
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
        /// Handles the Click event of the btnUpdate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (isValidForm())
                {
                    VoterModel voterModel = new VoterModel();
                    #region BindModel
                    voterModel.ElectorId = Convert.ToInt64(txtElectorId.Text.Trim());
                    voterModel.LocationId = Convert.ToInt32(cmbLocation.SelectedValue);
                    voterModel.WardId = Convert.ToInt32(cmbWard.SelectedValue);
                    voterModel.LLGId = Convert.ToInt32(cmbLLG.SelectedValue);
                    voterModel.DistrictId = Convert.ToInt32(cmbOpenElectorate.SelectedValue);
                    //MKA Fields
                    voterModel.MKALocationId = Convert.ToInt32(cmbMKALocation.SelectedValue);
                    voterModel.MKAWardId = Convert.ToInt32(cmbMKAWard.SelectedValue);
                    voterModel.MKALLGId = Convert.ToInt32(cmbMKALLG.SelectedValue);
                    voterModel.MKAClanId = Convert.ToInt32(cmbMKAClan.SelectedValue);
                    voterModel.MKADistrictId = Convert.ToInt32(cmbMKADistrict.SelectedValue);

                    voterModel.ProvinceId = Convert.ToInt32(cmbProvince.SelectedValue);
                    voterModel.LName = txtLastName.Text.Trim();
                    voterModel.FName = txtGivenName.Text.Trim();
                    voterModel.Gender = rdbMale.Checked.Equals(true) ? "M" : "F";
                    voterModel.DOB = dtpDateOfBirth.Value.Date;
                    voterModel.isPWD = chkPersonWithDisability.Checked.Equals(true) ? true : false;
                    voterModel.FatherLName = txtFathersLastName.Text.Trim();
                    voterModel.FatherFName = txtFathersFirstName.Text.Trim();
                    voterModel.MotherLName = txtMothersLastName.Text.Trim();
                    voterModel.MotherFName = txtMothersFirstName.Text.Trim();
                    if (Convert.ToInt32(cmbProvinceOfBirth.SelectedValue) <= 0)
                    {
                        voterModel.ProvOfBirthFK = Convert.ToInt32(cmbProvince.SelectedValue);
                    }
                    else
                    {
                        voterModel.ProvOfBirthFK = Convert.ToInt32(cmbProvinceOfBirth.SelectedValue);
                    }
                    if (string.IsNullOrWhiteSpace(txtPlaceofBirth.Text))
                    {
                        voterModel.PlaceOfBirth = Convert.ToString(cmbLocation.Text);
                    }
                    else
                    {
                        voterModel.PlaceOfBirth = txtPlaceofBirth.Text.Trim();
                    }
                    voterModel.OccupationFK = olist.Where(c => c.OccupationName == "Not Specified").Select(x => x.OccupationPK).FirstOrDefault();
                    voterModel.SubClan = txtSubClan.Text.Trim();
                    voterModel.NatIDRecordNo = txtNationalIdCardNumber.Text.Trim();
                    voterModel.Status = Convert.ToString(cmbVoterStatus.Text);
                    voterModel.FamilySortOrder = Convert.ToInt32(txtFamilyOrderNo.Text.Trim());

                    if (!string.IsNullOrEmpty(txtlat.Text.Trim()) && txtlat.Text != "0.000000")
                    {
                        voterModel.Lat = Convert.ToDecimal(txtlat.Text.Trim());
                    }
                    else
                    {
                        voterModel.Lat = Convert.ToDecimal(GlobalsModel.LocationDetails.Lat);
                    }

                    if (!string.IsNullOrEmpty(txtlong.Text.Trim()) && txtlong.Text != "0.000000")
                    {
                        voterModel.Long = Convert.ToDecimal(txtlong.Text.Trim());
                    }
                    else
                    {
                        voterModel.Long = Convert.ToDecimal(GlobalsModel.LocationDetails.Lon);
                    }

                    voterModel.FamilyNumber = txtFamilyNumber.Text.Trim();
                    voterModel.FamilyName = txtFamilyName.Text.Trim();
                    voterModel.Clan = txtClan.Text.Trim();
                    voterModel.RestHouse = txtRestHouse.Text.Trim();
                    voterModel.SectionNo = txtSectionNo.Text.Trim();
                    voterModel.LotNo = txtLotNo.Text.Trim();
                    voterModel.StreetName = txtStreetName.Text.Trim();
                    voterModel.SuburbTownSettlement = txtSuburbTownSettlement.Text.Trim();
                    voterModel.Comment = txtComment.Text.Trim();
                    //voterModel.TransferOutRegionId = 0;
                    //voterModel.TransferOutProvinceId = 0;
                    //voterModel.TransferOutDistrictId = 0;
                    //voterModel.TransferOutLLGId = 0;
                    //voterModel.TransferOutWardId = 0;
                    //voterModel.TransferOutLocationId = 0;
                    voterModel.Religion = txtReligion.Text.Trim();
                    #endregion

                    if (voterModel != null && voterModel.ElectorId > 0)
                    {
                        DialogResult dialogResult = MessageBox.Show("Are you sure you want to update the record?", "PNGEC", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dialogResult == DialogResult.Yes)
                        {
                            GlobalsModel.mainMaster.ShowLoader();

                            Response responseData = new Response();

                            MultipartFormDataContent formContent = new MultipartFormDataContent
                            {
                                {new StringContent(Convert.ToString(voterModel.ElectorId)),"ElectorId"},
                                {new StringContent(Convert.ToString(voterModel.LocationId)),"LocationId"},
                                {new StringContent(Convert.ToString(voterModel.WardId)),"WardId"},
                                {new StringContent(Convert.ToString(voterModel.LLGId)),"LLGId"},
                                {new StringContent(Convert.ToString(voterModel.DistrictId)),"DistrictId"},
                                //MKA Fields
                                {new StringContent(Convert.ToString(voterModel.MKALocationId)),"MKALocationId"},
                                {new StringContent(Convert.ToString(voterModel.MKAWardId)),"MKAWardId"},
                                {new StringContent(Convert.ToString(voterModel.MKALLGId)),"MKALLGId"},
                                {new StringContent(Convert.ToString(voterModel.MKADistrictId)),"MKADistrictId"},
                                {new StringContent(Convert.ToString(voterModel.MKAClanId)),"MKAClanId"},

                                {new StringContent(Convert.ToString(voterModel.ProvinceId)),"ProvinceId"},
                                {new StringContent(voterModel.LName),"LName"},
                                {new StringContent(voterModel.FName),"FName"},
                                {new StringContent(voterModel.Gender),"Gender"},
                                {new StringContent(CommonFunction.ConvertCustomDateTimeFormat(voterModel.DOB)),"DOB"},
                                {new StringContent(Convert.ToString(voterModel.isPWD)),"isPWD"},
                                {new StringContent(voterModel.FatherLName),"FatherLName"},
                                {new StringContent(voterModel.FatherFName),"FatherFName"},
                                {new StringContent(voterModel.MotherLName),"MotherLName"},
                                {new StringContent(voterModel.MotherFName),"MotherFName"},
                                {new StringContent(Convert.ToString(voterModel.ProvOfBirthFK)),"ProvOfBirthFK"},
                                {new StringContent(voterModel.PlaceOfBirth),"PlaceOfBirth"},
                                {new StringContent(Convert.ToString(voterModel.OccupationFK)),"OccupationFK"},
                                {new StringContent(voterModel.SubClan),"SubClan"},
                                {new StringContent(voterModel.NatIDRecordNo),"NatIDRecordNo"},
                                {new StringContent(voterModel.Status),"Status"},
                                {new StringContent(Convert.ToString(voterModel.FamilySortOrder)),"FamilySortOrder"},
                                {new StringContent(Convert.ToString(voterModel.Lat)),"Lat"},
                                {new StringContent(Convert.ToString(voterModel.Long)),"Long"},
                                {new StringContent(voterModel.FamilyNumber),"FamilyNumber"},
                                {new StringContent(voterModel.FamilyName),"FamilyName"},
                                {new StringContent(voterModel.Clan),"Clan"},
                                {new StringContent(voterModel.RestHouse),"RestHouse"},
                                {new StringContent(voterModel.SectionNo),"SectionNo"},
                                {new StringContent(voterModel.LotNo),"LotNo"},
                                {new StringContent(voterModel.StreetName),"StreetName"},
                                {new StringContent(voterModel.SuburbTownSettlement),"SuburbTownSettlement"},
                                {new StringContent(voterModel.Comment),"Comment"},
                                {new StringContent(voterModel.Religion),"Religion"},
                                {new StringContent(Convert.ToString(GlobalsModel.LoggedUser.Database)), "Database"},
                            };

                            var jsonToSend = JsonConvert.SerializeObject(formContent, Formatting.None);
                            var multipart = new MultipartFormDataContent();
                            var body = new StringContent(jsonToSend, Encoding.UTF8, "application/json");

                            using (var client = new HttpClient())
                            {
                                client.BaseAddress = new Uri(Globals.serverURL);
                                using (HttpResponseMessage response = client.PostAsync("DataMigration/UpdateVoter", formContent).Result)
                                {
                                    if (response.StatusCode == HttpStatusCode.OK)
                                    {
                                        string responseBody = await response.Content.ReadAsStringAsync();
                                        responseData = JsonConvert.DeserializeObject<Response>(responseBody);
                                    }
                                }
                            }

                            GlobalsModel.mainMaster.HideLoader();

                            if (responseData.Status)
                            {
                                MessageBox.Show(responseData.Message, "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.DialogResult = DialogResult.OK;
                            }
                            else
                            {
                                MessageBox.Show(responseData.Message, "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                GlobalsModel.mainMaster.HideLoader();
                MessageBox.Show(Resources.ResourceManager.GetString("ErrorMessage"), "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorLog.Write("ROVoterDetails", "ROVoterDetails_Load", ex);
            }
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

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cmbProvince control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cmbProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProvince.SelectedIndex > 0)
            {
                var selectedValue = Convert.ToInt32(cmbProvince.SelectedValue);
                var list = new List<API.Domain.District.DistrictModel>();
                //if (GlobalsModel.LoggedUser.User.Role == 9)
                //{ list = API.Domain.GeoStructure.LocalGeoList.DistrictList.Where(x => x.Status).ToList(); }
                //else
                list = Globals.GeoList.DistrictList.Where(x => x.Status && x.ProvinceId == selectedValue).ToList();

                //var list = Globals.GeoList.DistrictList.Where(x => x.Status && x.ProvinceId == selectedValue).ToList();
                if (GlobalsModel.LoggedUser.User.Role == 9)
                    list.Insert(0, new API.Domain.District.DistrictModel { DistrictId = this.model.DistrictId, DistrictName = this.model.DistrictName });
                else
                    list.Insert(0, new API.Domain.District.DistrictModel { DistrictId = 0, DistrictName = "Select District" });
                cmbOpenElectorate.DataSource = list;
                cmbOpenElectorate.ValueMember = "DistrictId";
                cmbOpenElectorate.DisplayMember = "DistrictName";
            }
            else
            {
                cmbOpenElectorate.SelectedIndex = -1;
                cmbOpenElectorate.DataSource = null;
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cmbOpenElectorate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cmbOpenElectorate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbOpenElectorate.SelectedIndex > 0)
            {
                var selectedValue = Convert.ToInt32(cmbOpenElectorate.SelectedValue);
                var list = Globals.GeoList.LLGList.Where(x => x.Status && x.DistrictId == selectedValue).ToList();
                list.Insert(0, new API.Domain.LLG.LLGModel { LLGId = 0, LLGName = "Select LLG" });
                cmbLLG.DataSource = list;
                cmbLLG.ValueMember = "LLGId";
                cmbLLG.DisplayMember = "LLGName";
            }
            else
            {
                cmbLLG.SelectedIndex = -1;
                cmbLLG.DataSource = null;
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cmbLLG control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cmbLLG_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbLLG.SelectedIndex > 0)
            {
                //List<API.Domain.Ward.WardModel> wardList = new List<API.Domain.Ward.WardModel>();

                var selectedValue = Convert.ToInt32(cmbLLG.SelectedValue);
                var list = Globals.GeoList.WardList.Where(x => x.Status && x.LLGId == selectedValue).ToList();
                //foreach (var item in list)
                //{
                //    wardModel = new API.Domain.Ward.WardModel();
                //    wardModel.WardName = item.WardNumber + " - " + item.WardName;
                //    wardList.Add(wardModel);
                //}
                list.Insert(0, new API.Domain.Ward.WardModel { WardId = 0, WardNoWardName = "Select Ward" });
                cmbWard.DataSource = list;
                cmbWard.ValueMember = "WardId";
                cmbWard.DisplayMember = "WardNoWardName";
            }
            else
            {
                cmbWard.SelectedIndex = -1;
                cmbWard.DataSource = null;
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cmbWard control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cmbWard_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbWard.SelectedIndex > 0)
            {
                var selectedValue = Convert.ToInt32(cmbWard.SelectedValue);
                var list = Globals.GeoList.LocationList.Where(x => x.Status && x.WardId == selectedValue).ToList();
                list.Insert(0, new API.Domain.Location.LocationModel { LocationId = 0, LocationName = "Select Location" });
                cmbLocation.DataSource = list;
                cmbLocation.ValueMember = "LocationId";
                cmbLocation.DisplayMember = "LocationName";
            }
            else
            {
                cmbLocation.SelectedIndex = -1;
                cmbLocation.DataSource = null;
            }
        }


        private void cmbMKADistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMKADistrict.SelectedIndex > 0)
            {
                var selectedValue = Convert.ToInt32(cmbMKADistrict.SelectedValue);
                var list = Globals.MKAGeoList.MKALLGList.Where(x => x.DistrictId == selectedValue).ToList();
                list.Insert(0, new API.Domain.MKAGeoStructure.MKALLGModel { LLGId = 0, LLGName = "Select LLG" });
                cmbMKALLG.DataSource = list;
                cmbMKALLG.ValueMember = "LLGId";
                cmbMKALLG.DisplayMember = "LLGName";
            }
            else
            {
                cmbMKALLG.SelectedIndex = -1;
                cmbMKALLG.DataSource = null;
            }
        }

        private void cmbMKALLG_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMKALLG.SelectedIndex > 0)
            {
                var selectedValue = Convert.ToInt32(cmbMKALLG.SelectedValue);
                var list = Globals.MKAGeoList.MKAWardList.Where(x => x.LLGId == selectedValue).ToList();
                list.Insert(0, new API.Domain.MKAGeoStructure.MKAWardModel { WardId = 0, WardName = "Select Ward" });
                cmbMKAWard.DataSource = list;
                cmbMKAWard.ValueMember = "WardId";
                cmbMKAWard.DisplayMember = "WardName";
            }
            else
            {
                cmbMKAWard.SelectedIndex = -1;
                cmbMKAWard.DataSource = null;
            }
        }

        private void cmbMKAWard_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMKAWard.SelectedIndex > 0)
            {
                var selectedValue = Convert.ToInt32(cmbMKAWard.SelectedValue);
                //MKA Location
                var list = Globals.MKAGeoList.MKALocationList.Where(x => x.WardId == selectedValue).ToList();
                list.Insert(0, new API.Domain.MKAGeoStructure.MKALocationModel { LocationId = 0, LocationName = "Select Location" });
                cmbMKALocation.DataSource = list;
                cmbMKALocation.ValueMember = "LocationId";
                cmbMKALocation.DisplayMember = "LocationName";

                //MKA Clan                
                var listClan = Globals.MKAGeoList.MKAClanList.Where(x => x.WardId == selectedValue).ToList();
                listClan.Insert(0, new API.Domain.MKAGeoStructure.MKAClanModel { ClanId = 0, ClanName = "Select Clan" });
                cmbMKAClan.DataSource = listClan;
                cmbMKAClan.ValueMember = "ClanId";
                cmbMKAClan.DisplayMember = "ClanName";
            }
            else
            {
                cmbMKALocation.SelectedIndex = -1;
                cmbMKALocation.DataSource = null;
                cmbMKAClan.SelectedIndex = -1;
                cmbMKAClan.DataSource = null;
            }

        }
        #endregion

        #region Methods

        #region BindCombobox
        /// <summary>
        /// Binds the combobox.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        private async Task BindCombobox()
        {
            await BindOccupation();
            BindProvinceOfBirth();
            BindProvince();
            BindStatus();

            //BindMKADistrict();
            //MKA user fields changes
            //if (GlobalsModel.LoggedUser.User.Role == 0 || GlobalsModel.LoggedUser.User.Role == 9)
            //{
            //BindMKADistrict();
            //BindResidenceFields();
            //}
        }
        #endregion

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
            if (model.DOB != null && model.DOB != Convert.ToDateTime("01-01-0001 00:00:00"))
            {
                dtpDateOfBirth.Value = Convert.ToDateTime(model.DOB).Date;
            }
            else
            {
                dtpDateOfBirth.Value = Convert.ToDateTime("01/01/2000").Date;
            }
            txtPlaceofBirth.Text = model.PlaceOfBirth;
            cmbProvinceOfBirth.SelectedValue = model.ProvOfBirthFK;

            if (!string.IsNullOrWhiteSpace(model.Gender))
            {
                if (model.Gender == "M")
                {
                    rdbMale.Checked = true;
                }
                else if (model.Gender == "F")
                {
                    rdbFemale.Checked = true;
                }
            }

            cmbOccupation.SelectedValue = model.OccupationFK;
            chkPersonWithDisability.Checked = model.isPWD.Equals(true) ? true : false;
            txtFathersFirstName.Text = model.FatherFName;
            txtFathersLastName.Text = model.FatherLName;
            txtMothersFirstName.Text = model.MotherFName;
            txtMothersLastName.Text = model.MotherLName;
            txtReligion.Text = model.Religion;
            if (GlobalsModel.LoggedUser.User.Role == 0 ||
                GlobalsModel.LoggedUser.User.Role == 2 ||
                GlobalsModel.LoggedUser.User.Role == 3 ||
                GlobalsModel.LoggedUser.User.Role == 7)
            {
                pnlImage.Visible = true;
                pbProfile.Image = GetImageFromUrl(model.ElectorProfileImageUrl);
                pbForm11.Image = GetImageFromUrl(model.ElectorFormImageUrl);
            }
            else
            {
                pnlImage.Visible = false;
            }

            // Place of Residence
            cmbProvince.SelectedValue = model.ProvinceId;
            cmbProvince.Enabled = false;

            //MKA Fields
            var list = Globals.MKAGeoList.MKADistrictList.Where(x => x.Status).ToList();
            list.Insert(0, new API.Domain.MKAGeoStructure.MKADistrictModel { DistrictId = 0, DistrictName = "Select MKA District" });

            cmbMKADistrict.DataSource = list;
            cmbMKADistrict.ValueMember = "DistrictId";
            cmbMKADistrict.DisplayMember = "DistrictName";

            cmbMKADistrict.SelectedValue = model.MKADistrictId;   
            
            cmbMKALLG.SelectedValue = model.MKALLGId;           
            cmbMKAWard.SelectedValue = model.MKAWardId;           
            cmbMKALocation.SelectedValue = model.MKALocationId;           
            cmbMKAClan.SelectedValue = model.MKAClanId;

            if(GlobalsModel.LoggedUser.User.Role != 0)
            {
                cmbMKADistrict.Enabled = false;
                cmbMKALLG.Enabled = false;
                cmbMKAWard.Enabled = false;
                cmbMKALocation.Enabled = false;
                cmbMKAClan.Enabled = false;
            }

            cmbOpenElectorate.SelectedValue = model.DistrictId;
            cmbOpenElectorate.Enabled = false;
            cmbLLG.SelectedValue = model.LLGId;
            cmbLLG.Enabled = false;
            cmbWard.SelectedValue = model.WardId;
            cmbWard.Enabled = false;
            cmbLocation.SelectedValue = model.LocationId;
            cmbLocation.Enabled = false;

            // Rural
            txtClan.Text = model.Clan;
            txtSubClan.Text = model.SubClan;
            txtRestHouse.Text = model.RestHouse;

            // Urban
            txtSectionNo.Text = model.SectionNo;
            txtLotNo.Text = model.LotNo;
            txtStreetName.Text = model.StreetName;
            txtSuburbTownSettlement.Text = model.SuburbTownSettlement;

            //Status
            cmbVoterStatus.Text = model.Status;
            //cmbVoterStatus.Enabled = false;
            txtComment.Text = model.Comment;

            txtlat.Text = Convert.ToString(model.Lat);
            txtlong.Text = Convert.ToString(model.Long);


            //set Enrollment Number
            if (!string.IsNullOrWhiteSpace(model.EnrolmentFormNumber))
            {
                string[] EnrolmentFormArray = model.EnrolmentFormNumber.Split('#');

                if (EnrolmentFormArray.Length == 7)
                {
                    txtForm11Number.Text = EnrolmentFormArray[1] + EnrolmentFormArray[4] + EnrolmentFormArray[6];
                }
            }

            if (this.pageName == "DataMigration")
            {
                if ((GlobalsModel.LoggedUser.User.Role == 2 || GlobalsModel.LoggedUser.User.Role == 7 && GlobalsModel.LoggedUser.Database == CommonEnum.Database.Staging)
                || (GlobalsModel.LoggedUser.User.Role == 3 && GlobalsModel.LoggedUser.Database == CommonEnum.Database.Preliminary)
                || ((GlobalsModel.LoggedUser.User.Role == 0 || GlobalsModel.LoggedUser.User.Role == 6) && GlobalsModel.LoggedUser.Database == CommonEnum.Database.Final))
                {
                    pnlbutton.Visible = true;
                }
                else
                {
                    pnlbutton.Visible = false;
                }

                if ((GlobalsModel.LoggedUser.User.Role == 0 || GlobalsModel.LoggedUser.User.Role == 2) && (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Staging || GlobalsModel.LoggedUser.Database == CommonEnum.Database.Preliminary))
                {
                    cmbMKALLG.SelectedValue = model.MKALLGId;
                    cmbMKAWard.SelectedValue = model.MKAWardId;
                    cmbMKALocation.SelectedValue = model.MKALocationId;
                    pnlMKADetails.Visible = true;
                    pnlbutton.Visible = true;
                }
            }
            else if (this.pageName == "MKAFile" || this.pageName == "File")
            {
                if ((GlobalsModel.LoggedUser.User.Role == 9))// GlobalsModel.LoggedUser.Database == CommonEnum.Database.Staging)
                //|| (GlobalsModel.LoggedUser.User.Role == 3 && GlobalsModel.LoggedUser.Database == CommonEnum.Database.Preliminary))
                {
                    cmbMKALLG.SelectedValue = model.MKALLGId;
                    cmbMKAWard.SelectedValue = model.MKAWardId;
                    cmbMKALocation.SelectedValue = model.MKALocationId;
                    pnlMKADetails.Visible = true;
                    pnlbutton.Visible = true;
                }
            }
            else
            {
                pnlbutton.Visible = false;
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
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to " + isRequest + " the record?", "PNGEC", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;

                    requestModel = new CommonRequest();
                    requestModel.database = Convert.ToString(GlobalsModel.LoggedUser.Database);
                    requestModel.ids = txtElectorId.Text;
                    //requestModel.recordType = Convert.ToString(txtStatus.Text);
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

        #region BindOccupation
        /// <summary>
        /// Binds the occupation.
        /// </summary>
        private async Task BindOccupation()
        {
            olist = new List<OccupationModel>();

            Response response = await GenericHttpClient.GetResponse("Mobile/General/GetOccupationList");
            if (response.Status && response.Data != null)
            {
                olist = JsonConvert.DeserializeObject<List<OccupationModel>>(Convert.ToString(response.Data));
            }

            olist.Insert(0, new OccupationModel { OccupationPK = 0, OccupationName = "Select Occupation" });
            cmbOccupation.DataSource = olist;
            cmbOccupation.ValueMember = "OccupationPK";
            cmbOccupation.DisplayMember = "OccupationName";
        }
        #endregion

        #region ProvinceOfBirth
        /// <summary>
        /// Binds the province of birth.
        /// </summary>
        private void BindProvinceOfBirth()
        {
            var plist = API.Domain.GeoStructure.LocalGeoList.ProvinceList.Where(x => x.Status).ToList();
            plist.Insert(0, new API.Domain.Province.ProvinceModel { ProvinceId = 0, ProvinceName = "Select Province" });
            cmbProvinceOfBirth.DataSource = plist;
            cmbProvinceOfBirth.ValueMember = "ProvinceId";
            cmbProvinceOfBirth.DisplayMember = "ProvinceName";
        }
        #endregion

        #region BindProvince
        /// <summary>
        /// Binds the province.
        /// </summary>
        private void BindProvince()
        {
            var list = new List<API.Domain.Province.ProvinceModel>();
            //if(GlobalsModel.LoggedUser.User.Role == 9)
            //{ list = API.Domain.GeoStructure.LocalGeoList.ProvinceList.Where(x => x.Status).ToList(); }
            //else                 
            list = Globals.GeoList.ProvinceList.Where(x => x.Status).ToList();
            if (GlobalsModel.LoggedUser.User.Role == 9)
                list.Insert(0, new API.Domain.Province.ProvinceModel { ProvinceId = this.model.ProvinceId, ProvinceName = this.model.ProvinceName });
            //BindResidenceFields();
            else
                list.Insert(0, new API.Domain.Province.ProvinceModel { ProvinceId = 0, ProvinceName = "Select Province" });
            cmbProvince.DataSource = list;
            cmbProvince.ValueMember = "ProvinceId";
            cmbProvince.DisplayMember = "ProvinceName";
        }
        #endregion

        #region BindResidenceFields
        private void BindResidenceFields()
        {

            var listDistrict = new List<API.Domain.District.DistrictModel>();
            listDistrict.Insert(0, new API.Domain.District.DistrictModel { DistrictId = this.model.DistrictId, DistrictName = this.model.DistrictName });
            cmbOpenElectorate.DataSource = listDistrict;
            cmbOpenElectorate.ValueMember = "DistrictId";
            cmbOpenElectorate.DisplayMember = "DistrictName";

            var listLLG = new List<API.Domain.LLG.LLGModel>();
            listLLG.Insert(0, new API.Domain.LLG.LLGModel { LLGId = this.model.LLGId, LLGName = this.model.LLGName });
            cmbLLG.DataSource = listLLG;
            cmbLLG.ValueMember = "LLGId";
            cmbLLG.DisplayMember = "LLGName";


            var listWard = new List<API.Domain.Ward.WardModel>();
            listWard.Insert(0, new API.Domain.Ward.WardModel { WardId = this.model.WardId, WardName = this.model.WardName });
            cmbWard.DataSource = listWard;
            cmbWard.ValueMember = "WardId";
            cmbWard.DisplayMember = "WardName";

            var listLocation = new List<API.Domain.Location.LocationModel>();
            listLocation.Insert(0, new API.Domain.Location.LocationModel { LocationId = this.model.LocationId, LocationName = this.model.LocationName });
            cmbLocation.DataSource = listLocation;
            cmbLocation.ValueMember = "LocationId";
            cmbLocation.DisplayMember = "LocationName";


        }
        #endregion

        #region BindMKADistrict
        private void BindMKADistrict()
        {
            if (GlobalsModel.LoggedUser.User.Role == 0)
            {
                var list = Globals.MKAGeoList.MKADistrictList.Where(x => x.Status).ToList();
                list.Insert(0, new API.Domain.MKAGeoStructure.MKADistrictModel { DistrictId = 0, DistrictName = "Select MKA District" });

                cmbMKADistrict.DataSource = list;
                cmbMKADistrict.ValueMember = "DistrictId";
                cmbMKADistrict.DisplayMember = "DistrictName";
            }
            else
            {
                var listDistrict = new List<API.Domain.MKAGeoStructure.MKADistrictModel>();
                listDistrict.Insert(0, new API.Domain.MKAGeoStructure.MKADistrictModel { DistrictId = this.model.MKADistrictId, DistrictName = this.model.MKADistrictName });
                cmbMKADistrict.DataSource = listDistrict;
                cmbMKADistrict.ValueMember = "DistrictId";
                cmbMKADistrict.DisplayMember = "DistrictName";                
            }

            var listLLG = new List<API.Domain.MKAGeoStructure.MKALLGModel>();
            listLLG.Insert(0, new API.Domain.MKAGeoStructure.MKALLGModel { LLGId = this.model.MKALLGId, LLGName = this.model.MKALLGName });
            cmbMKALLG.DataSource = listLLG;
            cmbMKALLG.ValueMember = "LLGId";
            cmbMKALLG.DisplayMember = "LLGName";

            var listWard = new List<API.Domain.MKAGeoStructure.MKAWardModel>();
            listWard.Insert(0, new API.Domain.MKAGeoStructure.MKAWardModel { WardId = this.model.MKAWardId, WardName = this.model.MKAWardName });
            cmbMKAWard.DataSource = listWard;
            cmbMKAWard.ValueMember = "WardId";
            cmbMKAWard.DisplayMember = "WardName";

            var listLocation = new List<API.Domain.MKAGeoStructure.MKALocationModel>();
            listLocation.Insert(0, new API.Domain.MKAGeoStructure.MKALocationModel { LocationId = this.model.MKALocationId, LocationName = this.model.MKALocationName });
            cmbMKALocation.DataSource = listLocation;
            cmbMKALocation.ValueMember = "LocationId";
            cmbMKALocation.DisplayMember = "LocationName";

            var listClan = new List<API.Domain.MKAGeoStructure.MKAClanModel>();
            listClan.Insert(0, new API.Domain.MKAGeoStructure.MKAClanModel { ClanId = this.model.MKAClanId, ClanName = this.model.MKAClanName });
            cmbMKAClan.DataSource = listClan;
            cmbMKAClan.ValueMember = "ClanId";
            cmbMKAClan.DisplayMember = "ClanName";
        }
        #endregion


        #region BindStatus
        /// <summary>
        /// Binds the status.
        /// </summary>
        private void BindStatus()
        {
            cmbVoterStatus.DataSource = StatusArray;
        }
        #endregion

        #region isValidForm
        /// <summary>
        /// Determines whether [is valid form].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is valid form]; otherwise, <c>false</c>.
        /// </returns>
        private bool isValidForm()
        {
            bool result = true;

            if (string.IsNullOrWhiteSpace(txtGivenName.Text))
            {
                result = false;
                MessageBox.Show("Please enter valid given name.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtGivenName.Focus();
            }
            else if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                result = false;
                MessageBox.Show("Please enter valid last name.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtLastName.Focus();
            }
            else if (rdbMale.Checked == false && rdbFemale.Checked == false)
            {
                result = false;
                MessageBox.Show("Please select valid gender.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblGender.Focus();
            }
            else if (dtpDateOfBirth.Value.Date >= DateTime.Now.Date)
            {
                result = false;
                MessageBox.Show("please select less than date of current date in date of birth.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dtpDateOfBirth.Focus();
            }
            else if (txtPlaceofBirth.Text.Length > 250)
            {
                result = false;
                MessageBox.Show("please enter less than 250 characters place of birth.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPlaceofBirth.Focus();
            }
            else if (Convert.ToInt32(cmbProvince.SelectedValue) <= 0)
            {
                result = false;
                MessageBox.Show("Please select valid province.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbProvince.Enabled = false;
                cmbProvince.Focus();
            }
            else if (Convert.ToInt32(cmbOpenElectorate.SelectedValue) <= 0)
            {
                result = false;
                MessageBox.Show("Please select valid open electorate.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbOpenElectorate.Enabled = false;
                cmbOpenElectorate.Focus();
            }
            else if (Convert.ToInt32(cmbLLG.SelectedValue) <= 0)
            {
                result = false;
                MessageBox.Show("Please select valid llg.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbLLG.Enabled = false;
                cmbLLG.Focus();
            }
            else if (Convert.ToInt32(cmbWard.SelectedValue) <= 0)
            {
                result = false;
                MessageBox.Show("Please select valid ward.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbWard.Enabled = false;
                cmbWard.Focus();
            }
            else if (Convert.ToInt32(cmbLocation.SelectedValue) <= 0)
            {
                result = false;
                MessageBox.Show("Please select valid location/village.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbLocation.Enabled = false;
                cmbLocation.Focus();
            }
            else if (Convert.ToString(cmbVoterStatus.Text) == "Select Status")
            {
                result = false;
                MessageBox.Show("Please select valid status.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbVoterStatus.Focus();
            }
            //else if (string.IsNullOrWhiteSpace(txtClan.Text))
            //{
            //    result = false;
            //    MessageBox.Show("Please enter valid clan.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtClan.Focus();
            //}
            //else if (string.IsNullOrWhiteSpace(txtReligion.Text))
            //{
            //    result = false;
            //    MessageBox.Show("Please enter valid religion.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtReligion.Focus();
            //}
            return result;
        }
        #endregion

        #endregion       
    }

}
