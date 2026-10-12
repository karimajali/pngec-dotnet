using System;

namespace PNGEC.API.Domain.Mobile.Water
{
    public class WaterModel
    {
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
        /// Gets or sets the clan fk.
        /// </summary>
        /// <value>
        /// The clan fk.
        /// </value>
        public int ClanFK { get; set; }

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
    }
}
