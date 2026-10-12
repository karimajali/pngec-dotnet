using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNGEC.API.Domain.Report
{
    public class WardRollBookCsvReportModel
    {
        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        /// <value>
        /// The gender.
        /// </value>
        private string gender { get; set; }
        /// <summary>
        /// Gets or sets the dbo.
        /// </summary>
        /// <value>
        /// The dbo.
        /// </value>
        private DateTime dbo { get; set; }
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
        /// Gets or sets the name of the f.
        /// </summary>
        /// <value>
        /// The name of the f.
        /// </value>
        public string FName { get; set; }
        /// <summary>
        /// Gets or sets the name of the l.
        /// </summary>
        /// <value>
        /// The name of the l.
        /// </value>
        public string LName { get; set; }
        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        /// <value>
        /// The gender.
        /// </value>
        public string Gender {
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
        /// <summary>
        /// Gets or sets the dob.
        /// </summary>
        /// <value>
        /// The dob.
        /// </value>
        public DateTime DOB {
            get { return dbo; }
            set
            {
                dbo = value;
                if (value != null)
                {
                    BDate = String.Format("{0:dd-MMM-yy}", value);
                }
                else
                {
                    BDate = string.Empty;
                }
            }
        }
        /// <summary>
        /// Gets or sets the b date.
        /// </summary>
        /// <value>
        /// The b date.
        /// </value>
        public string BDate { get; set; }
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
        /// Gets or sets the place of birth.
        /// </summary>
        /// <value>
        /// The place of birth.
        /// </value>
        public string PlaceOfBirth { get; set; }
        /// <summary>
        /// Gets or sets the nat identifier.
        /// </summary>
        /// <value>
        /// The nat identifier.
        /// </value>
        public string NatID { get; set; }
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string Status { get; set; }
        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        public string Country { get; set; } = "Papua new Guinea";
        /// <summary>
        /// Gets or sets the name of the province.
        /// </summary>
        /// <value>
        /// The name of the province.
        /// </value>
        public string ProvinceName { get; set; }
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
        /// Gets or sets the name of the district.
        /// </summary>
        /// <value>
        /// The name of the district.
        /// </value>
        public string DistrictName { get; set; }

        /// <summary>
        /// Gets or sets the name of the MKA district.
        /// </summary>
        /// <value>
        /// The name of the MKA district.
        /// </value>
        public string MKADistrictName { get; set; }

        /// <summary>
        /// Gets or sets the name of the MKA LLG.
        /// </summary>
        /// <value>
        /// The name of the MKA LLG.
        /// </value>
        public string MKALLGName { get; set; }

        /// <summary>
        /// Gets or sets the name of the MKA Ward.
        /// </summary>
        /// <value>
        /// The name of the MKA Ward.
        /// </value>
        public string MKAWardName { get; set; }

        /// <summary>
        /// Gets or sets the name of the MKA Location.
        /// </summary>
        /// <value>
        /// The name of the MKA Ward.
        /// </value>
        public string MKALocationName { get; set; }

        /// <summary>
        /// Gets or sets the name of the MKA Clan.
        /// </summary>
        /// <value>
        /// The name of the MKA Ward.
        /// </value>
        public string MKAClanName { get; set; }

        /// <summary>
        /// MKA Clan Id
        /// </summary>
        public int MKAClanId { get; set; }
    }
}
