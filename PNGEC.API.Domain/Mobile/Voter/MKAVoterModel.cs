using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PNGEC.API.Domain.Mobile.Voter
{
    /// <summary>
    /// mka voter model class
    /// </summary>
    public class MKAVoterModel 
    {
   
        /// <summary>
        /// voter model class
        /// </summary>
       
            /// <summary>
            /// Gets or sets the row no.
            /// </summary>
            /// <value>
            /// The row no.
            /// </value>
            public long RowNo { get; set; }

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
            /// Gets or sets the mkalocation identifier.
            /// </summary>
            /// <value>
            /// The location identifier.
            /// </value>
            public int MKALocationId { get; set; }

            /// <summary>
            /// Gets or sets the ward identifier.
            /// </summary>
            /// <value>
            /// The ward identifier.
            /// </value>
            public int WardId { get; set; }
            /// <summary>
            /// Gets or sets the mkaward identifier.
            /// </summary>
            /// <value>
            /// The ward identifier.
            /// </value>
            public int MKAWardId { get; set; }

            /// <summary>
            /// Gets or sets the LLG identifier.
            /// </summary>
            /// <value>
            /// The LLG identifier.
            /// </value>
            public int LLGId { get; set; }
            /// <summary>
            /// Gets or sets the MKALLG identifier.
            /// </summary>
            /// <value>
            /// The MKALLG identifier.
            /// </value>
            public int MKALLGId { get; set; }

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
                    else if (value == "F")
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
            /// Gets or sets the gen.
            /// </summary>
            /// <value>
            /// The gen.
            /// </value>
            public string Gen { get; set; }

            private DateTime? dbo { get; set; }
            /// <summary>
            /// Gets or sets the dob.
            /// </summary>
            /// <value>
            /// The dob.
            /// </value>
            public DateTime? DOB
            {
                get { return dbo; }
                set
                {
                    dbo = value;
                    if (value != null)
                    {
                        DateOfBirth = String.Format("{0:dd MMM yyyy}", value);
                    }
                    else
                    {
                        DateOfBirth = string.Empty;
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
            /// Gets or sets the Religion.
            /// </summary>
            /// <value>
            /// The Religion identifier.
            /// </value>
            public string Religion { get; set; }

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
            /// Gets or sets the mkaclan.
            /// </summary>
            /// <value>
            /// The clan.
            /// </value>
            public string MKAClanId { get; set; }
            /// <summary>
            /// Gets or sets the mkaclan.
            /// </summary>
            /// <value>
            /// The clan.
            /// </value>
            public string MKAClanName { get; set; }

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
            // <summary>
            /// Gets or sets the name of the MKALLG.
            /// </summary>
            /// <value>
            /// The name of the MKALLG.
            /// </value>
            public string MKALLGName { get; set; }
            /// <summary>
            /// Gets or sets the name of the ward.
            /// </summary>
            /// <value>
            /// The name of the ward.
            /// </value>
            public string WardName { get; set; }
            /// <summary>
            /// Gets or sets the name of the mkaward.
            /// </summary>
            /// <value>
            /// The name of the mkaward.
            /// </value>
            public string MKAWardName { get; set; }

            /// <summary>
            /// Gets or sets the name of the location.
            /// </summary>
            /// <value>
            /// The name of the location.
            /// </value>
            public string LocationName { get; set; }
            /// <summary>
            /// Gets or sets the name of the mkalocation.
            /// </summary>
            /// <value>
            /// The name of the mkalocation.
            /// </value>
            public string MKALocationName { get; set; }

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
            //public string gps { get; set; }

            /// <summary>
            /// Gets or sets the comment.
            /// </summary>
            /// <value>
            /// The comment.
            /// </value>
            public string Comment { get; set; }

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
            /// Gets or sets the elector profile image file.
            /// </summary>
            /// <value>
            /// The elector profile image file.
            /// </value>
            public IFormFile ElectorProfileImageFile { get; set; }

            /// <summary>
            /// Gets or sets the elector form image file.
            /// </summary>
            /// <value>
            /// The elector form image file.
            /// </value>
            public IFormFile ElectorFormImageFile { get; set; }

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
            /// Gets or sets the duplicate identifier.
            /// </summary>
            /// <value>
            /// The duplicate identifier.
            /// </value>
            public long DuplicateId { get; set; }

            /// <summary>
            /// Gets or sets the audit description.
            /// </summary>
            /// <value>
            /// The audit description.
            /// </value>
            public string AuditDescription { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether this instance has transfer in form11.
            /// </summary>
            /// <value>
            ///   <c>true</c> if this instance has transfer in form11; otherwise, <c>false</c>.
            /// </value>
            public bool HasTransferInForm11 { get; set; }

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
            /// Gets or sets the name of the transfer in province.
            /// </summary>
            /// <value>
            /// The name of the transfer in province.
            /// </value>
            public string transferInProvinceName { get; set; }

            /// <summary>
            /// Gets or sets the name of the transfer in district.
            /// </summary>
            /// <value>
            /// The name of the transfer in district.
            /// </value>
            public string transferInDistrictName { get; set; }

            /// <summary>
            /// Gets or sets the name of the transfer in LLG.
            /// </summary>
            /// <value>
            /// The name of the transfer in LLG.
            /// </value>
            public string transferInLLGName { get; set; }

            /// <summary>
            /// Gets or sets the name of the transfer in ward.
            /// </summary>
            /// <value>
            /// The name of the transfer in ward.
            /// </value>
            public string transferInWardName { get; set; }

            /// <summary>
            /// Gets or sets the name of the transfer in location.
            /// </summary>
            /// <value>
            /// The name of the transfer in location.
            /// </value>
            public string transferInLocationName { get; set; }

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
            /// Gets or sets the transfer in ward identifier.
            /// </summary>
            /// <value>
            /// The transfer in ward identifier.
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
            /// Gets or sets the name of the transfer out province.
            /// </summary>
            /// <value>
            /// The name of the transfer out province.
            /// </value>
            public string transferOutProvinceName { get; set; }

            /// <summary>
            /// Gets or sets the name of the transfer out district.
            /// </summary>
            /// <value>
            /// The name of the transfer out district.
            /// </value>
            public string transferOutDistrictName { get; set; }

            /// <summary>
            /// Gets or sets the name of the transfer out LLG.
            /// </summary>
            /// <value>
            /// The name of the transfer out LLG.
            /// </value>
            public string transferOutLLGName { get; set; }

            /// <summary>
            /// Gets or sets the name of the transfer out ward.
            /// </summary>
            /// <value>
            /// The name of the transfer out ward.
            /// </value>
            public string transferOutWardName { get; set; }

            /// <summary>
            /// Gets or sets the name of the transfer out location.
            /// </summary>
            /// <value>
            /// The name of the transfer out location.
            /// </value>
            public string transferOutLocationName { get; set; }

            /// <summary>
            /// Gets or sets the is rejected by rd.
            /// </summary>
            /// <value>
            /// The is rejected by rd.
            /// </value>
            public int IsRejectedByRD { get; set; }

            /// <summary>
            /// Gets or sets the created date.
            /// </summary>
            /// <value>
            /// The created date identifier.
            /// </value>
            public string CreatedDate { get; set; }

            /// <summary>
            /// Gets or sets the Error Message.
            /// </summary>
            /// <value>
            /// The Error Message identifier.
            /// </value>
            public string ErrorMessage { get; set; }

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

            /// <summary>
            /// Gets or sets a value indicating whether this instance is rejected.
            /// </summary>
            /// <value>
            ///   <c>true</c> if this instance is rejected; otherwise, <c>false</c>.
            /// </value>
            public bool IsRejected { get; set; }

            /// <summary>
            /// Gets or sets the duplicate elector identifier.
            /// </summary>
            /// <value>
            /// The duplicate elector identifier.
            /// </value>
            public long DuplicateElectorId { get; set; }

            /// <summary>
            /// Gets or sets the name of the duplicate l.
            /// </summary>
            /// <value>
            /// The name of the duplicate l.
            /// </value>
            public string DuplicateLName { get; set; }

            /// <summary>
            /// Gets or sets the name of the duplicate f.
            /// </summary>
            /// <value>
            /// The name of the duplicate f.
            /// </value>
            public string DuplicateFName { get; set; }

            /// <summary>
            /// Gets or sets the name of the duplicate voter.
            /// </summary>
            /// <value>
            /// The name of the duplicate voter.
            /// </value>
            public string DuplicateVoterName { get; set; }

            private string duplicategender { get; set; }
            /// <summary>
            /// Gets or sets the gender.
            /// </summary>
            /// <value>
            /// The gender.
            /// </value>
            public string DuplicateGender
            {

                get { return duplicategender; }
                set
                {
                    duplicategender = value;
                    if (value == "M")
                    {
                        DuplicateGen = "Male";
                    }
                    else if (value == "F")
                    {
                        DuplicateGen = "Female";
                    }
                    else
                    {
                        DuplicateGen = "Other";
                    }
                }
            }

            /// <summary>
            /// Gets or sets the duplicate gen.
            /// </summary>
            /// <value>
            /// The duplicate gen.
            /// </value>
            public string DuplicateGen { get; set; }

            private DateTime duplicatedbo { get; set; }
            /// <summary>
            /// Gets or sets the dob.
            /// </summary>
            /// <value>
            /// The dob.
            /// </value>
            public DateTime DuplicateDOB
            {
                get { return duplicatedbo; }
                set
                {
                    duplicatedbo = value;
                    if (value != null)
                    {
                        DuplicateDateOfBirth = String.Format("{0:dd MMM yyyy}", value);
                    }
                    else
                    {
                        DuplicateDateOfBirth = string.Empty;
                    }
                }
            }

            /// <summary>
            /// Gets or sets the duplicate date of birth.
            /// </summary>
            /// <value>
            /// The duplicate date of birth.
            /// </value>
            public string DuplicateDateOfBirth { get; set; }

            /// <summary>
            /// Gets or sets the duplicate occupation.
            /// </summary>
            /// <value>
            /// The duplicate occupation.
            /// </value>
            public string DuplicateOccupation { get; set; }

            /// <summary>
            /// Gets or sets the name of the duplicate province.
            /// </summary>
            /// <value>
            /// The name of the duplicate province.
            /// </value>
            public string DuplicateProvinceName { get; set; }

            /// <summary>
            /// Gets or sets the name of the duplicate district.
            /// </summary>
            /// <value>
            /// The name of the duplicate district.
            /// </value>
            public string DuplicateDistrictName { get; set; }

            /// <summary>
            /// Gets or sets the name of the duplicate LLG.
            /// </summary>
            /// <value>
            /// The name of the duplicate LLG.
            /// </value>
            public string DuplicateLLGName { get; set; }

            /// <summary>
            /// Gets or sets the name of the duplicate ward.
            /// </summary>
            /// <value>
            /// The name of the duplicate ward.
            /// </value>
            public string DuplicateWardName { get; set; }

            /// <summary>
            /// Gets or sets the name of the duplicate location.
            /// </summary>
            /// <value>
            /// The name of the duplicate location.
            /// </value>
            public string DuplicateLocationName { get; set; }

            /// <summary>
            /// Gets or sets the type of the record.
            /// </summary>
            /// <value>
            /// The type of the record.
            /// </value>
            public string RecordType { get; set; }

            /// <summary>
            /// Gets or sets the type of the duplicate record.
            /// </summary>
            /// <value>
            /// The type of the duplicate record.
            /// </value>
            public string DuplicateRecordType { get; set; }

            /// <summary>
            /// Gets or sets the elector signature image.
            /// </summary>
            /// <value>
            /// The elector signature image.
            /// </value>
            public string ElectorSignatureImage { get; set; }

            /// <summary>
            /// Gets or sets the elector finger print image.
            /// </summary>
            /// <value>
            /// The elector finger print image.
            /// </value>
            public string ElectorFingerPrintImage { get; set; }

            /// <summary>
            /// Gets or sets the elector profile image binary.
            /// </summary>
            /// <value>
            /// The elector profile image binary.
            /// </value>
            public string ElectorProfileImageBinary { get; set; }

            /// <summary>
            /// Gets or sets the elector signature image binary.
            /// </summary>
            /// <value>
            /// The elector signature image binary.
            /// </value>
            public string ElectorSignatureImageBinary { get; set; }

            /// <summary>Gets or sets the old location identifier.</summary>
            /// <value>The old location identifier.</value>
            public int OldLocationId { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether this instance is manual form number.
            /// </summary>
            /// <value>
            ///   <c>true</c> if this instance is manual form number; otherwise, <c>false</c>.
            /// </value>
            public bool IsManualFormNumber { get; set; }
        }
    }



