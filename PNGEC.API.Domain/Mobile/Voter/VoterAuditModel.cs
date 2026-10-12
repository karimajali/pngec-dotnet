using System;

namespace PNGEC.API.Domain.Mobile.Voter
{
    public class VoterAuditModel
    {
        private DateTime createdDate;

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the GPS.
        /// </summary>
        /// <value>
        /// The GPS.
        /// </value>
        public string GPS { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>
        /// The created date.
        /// </value>        
        public DateTime CreatedDate
        {
            get
            {
                return this.createdDate;
            }

            set
            {
                this.createdDate = value;
                if (value != null)
                {
                    //DateTime utcDate = DateTime.SpecifyKind(value, DateTimeKind.Utc);
                    //DateTime localDate = utcDate.ToLocalTime();
                    //DateAndTime = String.Format("{0:dd-MM-yyyy HH:mm}", localDate);
                    DateAndTime = this.createdDate.ToString("dd-MM-yyyy HH:mm");
                }
            }
        }

        /// <summary>
        /// Gets or sets the date and time.
        /// </summary>
        /// <value>
        /// The date and time.
        /// </value>
        public string DateAndTime { get; set; }

        /// <summary>
        /// Gets or sets the previous record identifier.
        /// </summary>
        /// <value>
        /// The previous record identifier.
        /// </value>
        public long PreviousRecordID { get; set; }

        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        /// <value>
        /// The comment.
        /// </value>
        public string Comment { get; set; }

        
        /// <summary>
        /// Gets or sets the name of the voter.
        /// </summary>
        /// <value>
        /// The name of the voter.
        /// </value>
        public string VoterName { get; set; }

        /// <summary>
        /// Gets or sets the elector identifier.
        /// </summary>
        /// <value>
        /// The elector identifier.
        /// </value>
        public long ElectorId { get; set; }

        /// <summary>
        /// Gets or sets the old elector identifier.
        /// </summary>
        /// <value>
        /// The old elector identifier.
        /// </value>
        public long OldElectorID { get; set; }

        /// <summary>
        /// Gets or sets the location identifier.
        /// </summary>
        /// <value>
        /// The location identifier.
        /// </value>
        public int LocationId { get; set; }

        /// <summary>
        /// Gets or sets the ward identifier.
        /// </summary>
        /// <value>
        /// The ward identifier.
        /// </value>
        public int WardId { get; set; }

        /// <summary>
        /// Gets or sets the LLG identifier.
        /// </summary>
        /// <value>
        /// The LLG identifier.
        /// </value>
        public int LLGId { get; set; }

        /// <summary>
        /// Gets or sets the district identifier.
        /// </summary>
        /// <value>
        /// The district identifier.
        /// </value>
        public int DistrictId { get; set; }

        /// <summary>
        /// Gets or sets the province identifier.
        /// </summary>
        /// <value>
        /// The province identifier.
        /// </value>
        public int ProvinceId { get; set; }

        /// <summary>
        /// Gets or sets the region identifier.
        /// </summary>
        /// <value>
        /// The region identifier.
        /// </value>
        public int RegionId { get; set; }

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
        /// Gets or sets the gender.
        /// </summary>
        /// <value>
        /// The gender.
        /// </value>
        public string Gender { get; set; }

        /// <summary>
        /// Gets or sets the dob.
        /// </summary>
        /// <value>
        /// The dob.
        /// </value>
        public DateTime DOB { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is yob.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is yob; otherwise, <c>false</c>.
        /// </value>
        public bool isYOB { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is password.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is password; otherwise, <c>false</c>.
        /// </value>
        public bool isPWD { get; set; }

        /// <summary>
        /// Gets or sets the name of the father l.
        /// </summary>
        /// <value>
        /// The name of the father l.
        /// </value>
        public string FatherLName { get; set; }

        /// <summary>
        /// Gets or sets the name of the father f.
        /// </summary>
        /// <value>
        /// The name of the father f.
        /// </value>
        public string FatherFName { get; set; }

        /// <summary>
        /// Gets or sets the name of the mother l.
        /// </summary>
        /// <value>
        /// The name of the mother l.
        /// </value>
        public string MotherLName { get; set; }

        /// <summary>
        /// Gets or sets the name of the mother f.
        /// </summary>
        /// <value>
        /// The name of the mother f.
        /// </value>
        public string MotherFName { get; set; }

        /// <summary>
        /// Gets or sets the prov of birth fk.
        /// </summary>
        /// <value>
        /// The prov of birth fk.
        /// </value>
        public int ProvOfBirthFK { get; set; }

        /// <summary>
        /// Gets or sets the province of birth.
        /// </summary>
        /// <value>
        /// The province of birth.
        /// </value>
        public string ProvinceOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the open electorate.
        /// </summary>
        /// <value>
        /// The open electorate.
        /// </value>
        public string OpenElectorate { get; set; }

        /// <summary>
        /// Gets or sets the place of birth.
        /// </summary>
        /// <value>
        /// The place of birth.
        /// </value>
        public string PlaceOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the occupation fk.
        /// </summary>
        /// <value>
        /// The occupation fk.
        /// </value>
        public int OccupationFK { get; set; }

        /// <summary>
        /// Gets or sets the occupation.
        /// </summary>
        /// <value>
        /// The occupation.
        /// </value>
        public string Occupation { get; set; }

        /// <summary>
        /// Gets or sets the clan fk.
        /// </summary>
        /// <value>
        /// The clan fk.
        /// </value>
        public int ClanFK { get; set; }

        /// <summary>
        /// Gets or sets the clan.
        /// </summary>
        /// <value>
        /// The clan.
        /// </value>
        public string Clan { get; set; }

        /// <summary>
        /// Gets or sets the sub clan.
        /// </summary>
        /// <value>
        /// The sub clan.
        /// </value>
        public string SubClan { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the nat identifier.
        /// </summary>
        /// <value>
        /// The nat identifier.
        /// </value>
        public string NatID { get; set; }

        /// <summary>
        /// Gets or sets the nat identifier record no.
        /// </summary>
        /// <value>
        /// The nat identifier record no.
        /// </value>
        public string NatIDRecordNo { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the name of the province.
        /// </summary>
        /// <value>
        /// The name of the province.
        /// </value>
        public string ProvinceName { get; set; }

        /// <summary>
        /// Gets or sets the name of the district.
        /// </summary>
        /// <value>
        /// The name of the district.
        /// </value>
        public string DistrictName { get; set; }

        /// <summary>
        /// Gets or sets the name of the LLG.
        /// </summary>
        /// <value>
        /// The name of the LLG.
        /// </value>
        public string LLGName { get; set; }

        /// <summary>
        /// Gets or sets the name of the ward.
        /// </summary>
        /// <value>
        /// The name of the ward.
        /// </value>
        public string WardName { get; set; }

        /// <summary>
        /// Gets or sets the name of the location.
        /// </summary>
        /// <value>
        /// The name of the location.
        /// </value>
        public string LocationName { get; set; }

        /// <summary>
        /// Gets or sets the house fk.
        /// </summary>
        /// <value>
        /// The house fk.
        /// </value>
        public int HouseFK { get; set; }

        /// <summary>
        /// Gets or sets the house sort order.
        /// </summary>
        /// <value>
        /// The house sort order.
        /// </value>
        public int HouseSortOrder { get; set; }

        /// <summary>
        /// Gets or sets the rest house.
        /// </summary>
        /// <value>
        /// The rest house.
        /// </value>
        public string RestHouse { get; set; }

        /// <summary>
        /// Gets or sets the section no.
        /// </summary>
        /// <value>
        /// The section no.
        /// </value>
        public string SectionNo { get; set; }

        /// <summary>
        /// Gets or sets the lot no.
        /// </summary>
        /// <value>
        /// The lot no.
        /// </value>
        public string LotNo { get; set; }

        /// <summary>
        /// Gets or sets the name of the street.
        /// </summary>
        /// <value>
        /// The name of the street.
        /// </value>
        public string StreetName { get; set; }

        /// <summary>
        /// Gets or sets the suburb town settlement.
        /// </summary>
        /// <value>
        /// The suburb town settlement.
        /// </value>
        public string SuburbTownSettlement { get; set; }

        /// <summary>
        /// Gets or sets the family fk.
        /// </summary>
        /// <value>
        /// The family fk.
        /// </value>
        public int FamilyFK { get; set; }

        /// <summary>
        /// Gets or sets the family sort order.
        /// </summary>
        /// <value>
        /// The family sort order.
        /// </value>
        public int FamilySortOrder { get; set; }

        /// <summary>
        /// Gets or sets the family number.
        /// </summary>
        /// <value>
        /// The family number.
        /// </value>
        public string FamilyNumber { get; set; }

        /// <summary>
        /// Gets or sets the name of the family.
        /// </summary>
        /// <value>
        /// The name of the family.
        /// </value>
        public string FamilyName { get; set; }

        /// <summary>
        /// Gets or sets the changed.
        /// </summary>
        /// <value>
        /// The changed.
        /// </value>
        //public TimeSpan changed { get; set; }

        /// <summary>
        /// Gets or sets the dpo.
        /// </summary>
        /// <value>
        /// The dpo.
        /// </value>
        public string dpo { get; set; }

        /// <summary>
        /// Gets or sets the lat.
        /// </summary>
        /// <value>
        /// The lat.
        /// </value>
        public decimal Lat { get; set; }

        /// <summary>
        /// Gets or sets the long.
        /// </summary>
        /// <value>
        /// The long.
        /// </value>
        public decimal Long { get; set; }

        /// <summary>
        /// Gets or sets the posted.
        /// </summary>
        /// <value>
        /// The posted.
        /// </value>
        public DateTime posted { get; set; }

        /// <summary>
        /// Gets or sets the GPS.
        /// </summary>
        /// <value>
        /// The GPS.
        /// </value>
        //public int gps { get; set; }s
        
        /// <summary>
        /// Gets or sets the elector profile image.
        /// </summary>
        /// <value>
        /// The elector profile image.
        /// </value>
        public string ElectorProfileImage { get; set; }

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
            }
        }

        /// <summary>
        /// Gets or sets the elector form image.
        /// </summary>
        /// <value>
        /// The elector form image.
        /// </value>
        public string ElectorFormImage { get; set; }

        /// <summary>
        /// Gets or sets the elector form image URL.
        /// </summary>
        /// <value>
        /// The elector form image URL.
        /// </value>
        public string ElectorFormImageUrl { get; set; }

        /// <summary>
        /// Sets the form image URL.
        /// </summary>
        /// <param name="apiURL">The API URL.</param>
        public void SetFormImageUrl(string apiURL)
        {
            if (!string.IsNullOrWhiteSpace(this.ElectorFormImage))
            {
                this.ElectorFormImageUrl = string.Format("{0}/{1}/{2}/{3}/{4}", apiURL, Constants.UploadFolderPath, Constants.ElectorFormImageFolderPath, this.ElectorId.ToString(), this.ElectorFormImage);
            }
        }
        
        /// <summary>
        /// Gets or sets the enrolment form number.
        /// </summary>
        /// <value>
        /// The enrolment form number.
        /// </value>
        public string EnrolmentFormNumber { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>
        /// The created by.
        /// </value>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the temporary elector identifier.
        /// </summary>
        /// <value>
        /// The temporary elector identifier.
        /// </value>
        public int TempElectorId { get; set; }

        /// <summary>
        /// Gets or sets the type of the record.
        /// </summary>
        /// <value>
        /// The type of the record.
        /// </value>
        public string RecordType { get; set; }

        /// <summary>
        /// Gets or sets the Religion.
        /// </summary>
        /// <value>
        /// The Religion identifier.
        /// </value>
        public string Religion { get; set; }

        /// <summary>
        /// Gets or sets the audit date.
        /// </summary>
        /// <value>
        /// The audit date.
        /// </value>        
        public DateTime AuditDatetime
        {
            get;set;
        }

        /// <summary>
        /// Gets or sets the approve-reject lat.
        /// </summary>
        /// <value>
        /// The approve-reject lat.
        /// </value>
        public decimal ARLat { get; set; }

        /// <summary>
        /// Gets or sets the approve-reject long.
        /// </summary>
        /// <value>ss
        /// The approve-reject long.
        /// </value>
        public decimal ARLong { get; set; }

        /// <summary>
        /// Gets or sets the rejected comment.
        /// </summary>
        /// <value>
        /// The rejected comment.
        /// </value>
        public string RejectedReason { get; set; }

        /// <summary>
        /// Gets or sets the is approve or reject.
        /// </summary>
        /// <value>
        /// The is approve or reject.
        /// </value>
        public string IsApproveOrReject { get; set; }

        /// <summary>
        /// Gets or sets the modified date.
        /// </summary>
        /// <value>
        /// The modified date.
        /// </value>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the name of the modified by.
        /// </summary>
        /// <value>
        /// The name of the modified by.
        /// </value>
        public string ModifiedByName { get; set; }
    }
}
