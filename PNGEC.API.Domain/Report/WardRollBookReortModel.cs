using System;
using System.Net;

namespace PNGEC.API.Domain.Report
{
    public class WardRollBookReortModel
    {
        /// <summary>
        /// Gets or sets the seq.
        /// </summary>
        /// <value>
        /// The seq.
        /// </value>
        public int Seq { get; set; }

        /// <summary>
        /// Gets or sets the elector identifier.
        /// </summary>
        /// <value>
        /// The elector identifier.
        /// </value>
        public long ElectorId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        private string gender { get; set; }
        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        /// <value>
        /// The gender.
        /// </value>
        public string Gender
        {

            get { return gender; }
            set
            {
                gender = value;
                if (value == "M")
                {
                    Gen = "Male";
                }
                else if(value == "F")
                {
                    Gen = "Female";
                }
                else
                {
                    Gen = "Other";
                }
            }
        }

        /// <summary>
        /// Gets or sets the occupation.
        /// </summary>
        /// <value>
        /// The occupation.
        /// </value>
        public string Occupation { get; set; }

        private DateTime dbo { get; set; }
        /// <summary>
        /// Gets or sets the dob.
        /// </summary>
        /// <value>
        /// The dob.
        /// </value>
        public DateTime DOB
        {
            get { return dbo; }
            set
            {
                dbo = value;
                if (value != null)
                {
                    DateOfBirth = String.Format("{0:dd MMM yyyy}", value);
                    BDate = String.Format("{0:dd MMMM yyyy}", value);
                }
                else
                {
                    DateOfBirth = string.Empty;
                    BDate = string.Empty;
                }
            }
        }

        /// <summary>
        /// Gets or sets the date of birth.
        /// </summary>
        /// <value>
        /// The date of birth.
        /// </value>
        public string DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the status code.
        /// </summary>
        /// <value>
        /// The status code.
        /// </value>
        public string StatusCode { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        /// <value>
        /// The comments.
        /// </value>
        public string Comments { get; set; }

        /// <summary>
        /// Gets or sets the name of the ward number ward.
        /// </summary>
        /// <value>
        /// The name of the ward number ward.
        /// </value>
        public string WardNumberWardName { get; set; }

        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        public int Count { get; set; } = 0;

        /// <summary>
        /// Gets or sets the name of the l.
        /// </summary>
        /// <value>
        /// The name of the l.
        /// </value>
        public string LName { get; set; }

        /// <summary>
        /// Gets or sets the name of the f.
        /// </summary>
        /// <value>
        /// The name of the f.
        /// </value>
        public string FName { get; set; }

        /// <summary>
        /// Gets or sets the full string gender.
        /// </summary>
        /// <value>
        /// The full string gender.
        /// </value>
        public string Gen { get; set; }

        /// <summary>
        /// Gets or sets the b date.
        /// </summary>
        /// <value>
        /// The b date.
        /// </value>
        public string BDate { get; set; }

        /// <summary>
        /// Gets or sets the elector profile image.
        /// </summary>
        /// <value>
        /// The elector profile image.
        /// </value>
        public string ElectorProfileImage { get; set; }

        /// <summary>
        /// Gets or sets the nid.
        /// </summary>
        /// <value>
        /// The nid.
        /// </value>
        public string NID { get; set; }

        /// <summary>
        /// Gets or sets the nat identifier record no.
        /// </summary>
        /// <value>
        /// The nat identifier record no.
        /// </value>
        public string NatIDRecordNo { get; set; }

        private bool ispwd { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is password.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is password; otherwise, <c>false</c>.
        /// </value>
        public bool isPWD
        {
            get { return ispwd; }
            set
            {
                ispwd = value;
                if (value)
                {
                    WithDisability = "Yes";
                }
                else
                {
                    WithDisability = "No";
                }
            }
        }

        /// <summary>
        /// Gets or sets the with disability.
        /// </summary>
        /// <value>
        /// The with disability.
        /// </value>
        public string WithDisability { get; set; }

        /// <summary>
        /// Gets or sets the christianity.
        /// </summary>
        /// <value>
        /// The christianity.
        /// </value>
        public string Christianity { get; set; }

        /// <summary>
        /// Gets or sets the elector profile image URL.
        /// </summary>
        /// <value>
        /// The elector profile image URL.
        /// </value>
        public string ElectorProfileImageUrl { get; set; }

        /// <summary>
        /// Sets the image URL.
        /// </summary>
        /// <param name="apiURL">The API URL.</param>
        public void SetImageUrl(string apiURL)
        {
            if (!string.IsNullOrWhiteSpace(this.ElectorProfileImage))
            {
                this.ElectorProfileImageUrl = string.Format("{0}/{1}/{2}/{3}/{4}", apiURL, Constants.UploadFolderPath, Constants.ElectorProfileImageFolderPath, this.ElectorId.ToString(), this.ElectorProfileImage);

                #region image exit or not
                //HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(ElectorProfileImageUrl);
                //request.Method = "HEAD";
                //try
                //{
                //    request.GetResponse();
                //}
                //catch
                //{
                //    this.ElectorProfileImageUrl = string.Format("{0}/{1}/{2}", apiURL, Constants.UploadFolderPath, Constants.DefaultImage);
                //}
                #endregion
            }
            else
            {
               // this.ElectorProfileImageUrl = string.Format("{0}/{1}/{2}", apiURL, Constants.UploadFolderPath, Constants.DefaultImage);

                if(this.gender == "M")
                    this.ElectorProfileImageUrl = string.Format("{0}/{1}/{2}", apiURL, Constants.UploadFolderPath, Constants.DefaultMaleImage);
                else if(this.gender == "F")
                    this.ElectorProfileImageUrl = string.Format("{0}/{1}/{2}", apiURL, Constants.UploadFolderPath, Constants.DefaultFemaleImage);
                else
                    this.ElectorProfileImageUrl = string.Format("{0}/{1}/{2}", apiURL, Constants.UploadFolderPath, Constants.DefaultImage);
            }
        }

        /// <summary>
        /// Gets or sets the religion.
        /// </summary>
        /// <value>
        /// The religion.
        /// </value>
        public string Religion { get; set; }

        /// <summary>
        /// Gets or sets the region.
        /// </summary>
        /// <value>
        /// The region.
        /// </value>
        public string Region { get; set; }

        /// <summary>
        /// Gets or sets the province.
        /// </summary>
        /// <value>
        /// The province.
        /// </value>
        public string Province { get; set; }

        /// <summary>
        /// Gets or sets the district.
        /// </summary>
        /// <value>
        /// The district.
        /// </value>
        public string District { get; set; }

        /// <summary>
        /// Gets or sets the LLG.
        /// </summary>
        /// <value>
        /// The LLG.
        /// </value>
        public string LLG { get; set; }

        /// <summary>
        /// Gets or sets the ward.
        /// </summary>
        /// <value>
        /// The ward.
        /// </value>
        public string Ward { get; set; }
        /// <summary>
        /// Gets or sets the district.
        /// </summary>
        /// <value>
        /// The district.
        /// </value>
        public string MKADistrict { get; set; }

        /// <summary>
        /// Gets or sets the LLG.
        /// </summary>
        /// <value>
        /// The LLG.
        /// </value>
        public string MKALLG { get; set; }

        /// <summary>
        /// Gets or sets the ward.
        /// </summary>
        /// <value>
        /// The ward.
        /// </value>
        public string MKAWard { get; set; }

        /// <summary>
        /// Gets or sets the mka ward no.
        /// </summary>
        /// <value>
        /// The ward no.
        /// </value>
        public int MKAWardNo { get; set; }

        /// <summary>
        /// Gets or sets the mka clan id
        /// </summary>
        /// <value>
        /// The ward no.
        /// </value>
        public int MKAClanId { get; set; }

        /// <summary>
        /// Gets or sets the MKAClan.
        /// </summary>
        /// <value>
        /// The ward.
        /// </value>
        public string MKAClanName { get; set; }

        /// <summary>
        /// Gets or sets the name of the MKA ward number ward.
        /// </summary>
        /// <value>
        /// The name of the ward number ward.
        /// </value>
        public string MKAWardNumberWardName { get; set; }

        /// <summary>
        /// Gets or sets the ward no.
        /// </summary>
        /// <value>
        /// The ward no.
        /// </value>
        public int WardNo { get; set; }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        public string Location { get; set; }
        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        public string MKALocation { get; set; }

        /// <summary>
        /// Gets or sets the electors.
        /// </summary>
        /// <value>
        /// The electors.
        /// </value>
        public string Electors { get; set; }

        /// <summary>
        /// Gets or sets the date time.
        /// </summary>
        /// <value>
        /// The date time.
        /// </value>
        public string DateTime { get; set; }

        /// <summary>
        /// Gets or sets the duplicate identifier.
        /// </summary>
        /// <value>
        /// The duplicate identifier.
        /// </value>
        public long DuplicateId { get; set; }

        /// <summary>
        /// Gets or sets the province code.
        /// </summary>
        /// <value>
        /// The province code.
        /// </value>
        public string ProvinceCode { get; set; }

        /// <summary>
        /// Gets or sets the clan.
        /// </summary>
        /// <value>
        /// The clan.
        /// </value>
        public string Clan { get; set; }

        /// <summary>
        /// Gets or sets the place of birth.
        /// </summary>
        /// <value>
        /// The place of birth.
        /// </value>
        public string PlaceOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the name of the prov of birth.
        /// </summary>
        /// <value>
        /// The name of the prov of birth.
        /// </value>
        public string ProvOfBirthName { get; set; }

        #region WardStatus | Field Tally Summary Sheet

        /// <summary>
        /// Gets or sets the region identifier.
        /// </summary>
        /// <value>
        /// The region identifier.
        /// </value>
        public int RegionId { get; set; }

        /// <summary>
        /// Gets or sets the province identifier.
        /// </summary>
        /// <value>
        /// The province identifier.
        /// </value>
        public int ProvinceId { get; set; }

        /// <summary>
        /// Gets or sets the district identifier.
        /// </summary>
        /// <value>
        /// The district identifier.
        /// </value>
        public int DistrictId { get; set; }

        /// <summary>
        /// Gets or sets the LLG identifier.
        /// </summary>
        /// <value>
        /// The LLG identifier.
        /// </value>
        public int LLGId { get; set; }

        /// <summary>
        /// Gets or sets the ward identifier.
        /// </summary>
        /// <value>
        /// The ward identifier.
        /// </value>
        public int WardId { get; set; }

        /// <summary>
        /// Gets or sets the location identifier.
        /// </summary>
        /// <value>
        /// The location identifier.
        /// </value>
        public int LocationId { get; set; }

        /// <summary>
        /// Gets or sets the MKA district identifier.
        /// </summary>
        /// <value>
        /// The district identifier.
        /// </value>
        public int MKADistrictId { get; set; }

        /// <summary>
        /// Gets or sets the MKA LLG identifier.
        /// </summary>
        /// <value>
        /// The LLG identifier.
        /// </value>
        public int MKALLGId { get; set; }

        /// <summary>
        /// Gets or sets the MKA ward identifier.
        /// </summary>
        /// <value>
        /// The ward identifier.
        /// </value>
        public int MKAWardId { get; set; }

        /// <summary>
        /// Gets or sets the MKA location identifier.
        /// </summary>
        /// <value>
        /// The location identifier.
        /// </value>
        public int MKALocationId { get; set; }

        /// <summary>
        /// Gets or sets the transfer out region identifier.
        /// </summary>
        /// <value>
        /// The transfer out region identifier.
        /// </value>
        public int TransferOutRegionId { get; set; }

        /// <summary>
        /// Gets or sets the transfer out province identifier.
        /// </summary>
        /// <value>
        /// The transfer out province identifier.
        /// </value>
        public int TransferOutProvinceId { get; set; }

        /// <summary>
        /// Gets or sets the transfer out district identifier.
        /// </summary>
        /// <value>
        /// The transfer out district identifier.
        /// </value>
        public int TransferOutDistrictId { get; set; }

        /// <summary>
        /// Gets or sets the transfer out LLG identifier.
        /// </summary>
        /// <value>
        /// The transfer out LLG identifier.
        /// </value>
        public int TransferOutLLGId { get; set; }

        /// <summary>
        /// Gets or sets the transfer out ward identifier.
        /// </summary>
        /// <value>
        /// The transfer out ward identifier.
        /// </value>
        public int TransferOutWardId { get; set; }

        /// <summary>
        /// Gets or sets the transfer out location identifier.
        /// </summary>
        /// <value>
        /// The transfer out location identifier.
        /// </value>
        public int TransferOutLocationId { get; set; }

        /// <summary>
        /// Gets or sets the transfer in region identifier.
        /// </summary>
        /// <value>
        /// The transfer in region identifier.
        /// </value>
        public int TransferInRegionId { get; set; }

        /// <summary>
        /// Gets or sets the transfer in province identifier.
        /// </summary>
        /// <value>
        /// The transfer in province identifier.
        /// </value>
        public int TransferInProvinceId { get; set; }

        /// <summary>
        /// Gets or sets the transfer in district identifier.
        /// </summary>
        /// <value>
        /// The transfer in district identifier.
        /// </value>
        public int TransferInDistrictId { get; set; }

        /// <summary>
        /// Gets or sets the transfer in LLG identifier.
        /// </summary>
        /// <value>
        /// The transfer in LLG identifier.
        /// </value>
        public int TransferInLLGId { get; set; }

        /// <summary>
        /// Gets or sets the transfer in ward identifier.
        /// </summary>
        /// <value>
        /// The transfer in ward identifier.
        /// </value>
        public int TransferInWardId { get; set; }

        /// <summary>
        /// Gets or sets the transfer in location identifier.
        /// </summary>
        /// <value>
        /// The transfer in location identifier.
        /// </value>
        public int TransferInLocationId { get; set; }

        /// <summary>
        /// Gets or sets the transfer in district identifier.
        /// </summary>
        /// <value>
        /// The transfer in district identifier.
        /// </value>
        public int TransferInMKADistrictId { get; set; }

        /// <summary>
        /// Gets or sets the transfer in LLG identifier.
        /// </summary>
        /// <value>
        /// The transfer in LLG identifier.
        /// </value>
        public int TransferInMKALLGId { get; set; }

        /// <summary>
        /// Gets or sets the transfer in ward identifier.
        /// </summary>
        /// <value>
        /// The transfer in ward identifier.
        /// </value>
        public int TransferInMKAWardId { get; set; }

        /// <summary>
        /// Gets or sets the transfer in location identifier.
        /// </summary>
        /// <value>
        /// The transfer in location identifier.
        /// </value>
        public int TransferInMKALocationId { get; set; }

        /// <summary>
        /// Gets or sets the transfer out district identifier.
        /// </summary>
        /// <value>
        /// The transfer out district identifier.
        /// </value>
        public int TransferOutMKADistrictId { get; set; }

        /// <summary>
        /// Gets or sets the transfer out LLG identifier.
        /// </summary>
        /// <value>
        /// The transfer out LLG identifier.
        /// </value>
        public int TransferOutMKALLGId { get; set; }

        /// <summary>
        /// Gets or sets the transfer out ward identifier.
        /// </summary>
        /// <value>
        /// The transfer out ward identifier.
        /// </value>
        public int TransferOutMKAWardId { get; set; }

        /// <summary>
        /// Gets or sets the transfer out location identifier.
        /// </summary>
        /// <value>
        /// The transfer out location identifier.
        /// </value>
        public int TransferOutMKALocationId { get; set; }

        #endregion


        /// <summary>
        /// Get or Set ClanID
        /// </summary>
        public int ClanId { get; set; }


        /// <summary>
        /// Get or Set ClanID
        /// </summary>
        public string ClanName { get; set; }
    }
}
