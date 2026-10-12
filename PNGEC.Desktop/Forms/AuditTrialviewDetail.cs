using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;
using PNGEC.API.Domain.Mobile.Voter;
using PNGEC.Desktop.Model;

namespace PNGEC.Desktop.Forms
{
    public partial class AuditTrialviewDetail : Form
    {
        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="AuditTrialviewDetail"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public AuditTrialviewDetail(VoterModel model)
        {
            InitializeComponent();
            InitControls(model);
        }
        #endregion

        #region InitControls        
        /// <summary>
        /// Initializes the controls.
        /// </summary>
        /// <param name="model">The model.</param>
        private void InitControls(VoterModel model)
        {
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
            txtGender.Text = model.Gender.Equals("M") ? "Male" : "Female";
            txtOccupation.Text = model.Occupation;
            chkPersonWithDisability.Checked = model.isPWD.Equals(true) ? true : false;
            txtFathersFirstName.Text = model.FatherFName;
            txtFathersLastName.Text = model.FatherLName;
            txtMothersFirstName.Text = model.MotherFName;
            txtMothersLastName.Text = model.MotherLName;

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

            //Place of Residence
            txtProvince.Text = model.ProvinceName;
            txtOpenElectorate.Text = model.OpenElectorate;
            txtLLG.Text = model.LLGName;
            txtWardNoAndName.Text = model.WardName;

            // Rural
            txtLocationVillage.Text = model.LocationName;
            txtClan.Text = model.Clan;
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
            txtReligion.Text = model.Religion;

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
            if (!string.IsNullOrWhiteSpace(model.EnrolmentFormNumber))
            {
                string[] EnrolmentFormArray = model.EnrolmentFormNumber.Split('#');

                if (EnrolmentFormArray.Length == 7)
                {
                    txtForm11Number.Text = EnrolmentFormArray[1] + EnrolmentFormArray[4].ToString().PadLeft(4, '0') + EnrolmentFormArray[6];
                }
            }

            //if ((this.pageName == "DataMigration"
            //    && (GlobalsModel.LoggedUser.User.Role == 2 && GlobalsModel.LoggedUser.Database == CommonEnum.Database.Staging)
            //    || (GlobalsModel.LoggedUser.User.Role == 3 && GlobalsModel.LoggedUser.Database == CommonEnum.Database.Preliminary)) && txtStatus.Text != "OLD")
            //{
            //    pnlbutton.Visible = true;
            //}
            //else
            //{
            //    pnlbutton.Visible = false;
            //}
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
    }
}
