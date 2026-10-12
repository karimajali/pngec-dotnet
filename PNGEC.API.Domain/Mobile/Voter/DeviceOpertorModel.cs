using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNGEC.API.Domain.Mobile.Voter
{
    public class DeviceOpertorModel
    {
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
        /// Gets or sets the seq.
        /// </summary>
        /// <value>
        /// The seq.
        /// </value>
        public int Seq { get; set; }

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
        public string Gen { get; set; }

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
        public string DateOfBirth { get; set; }
        public string BDate { get; set; }

        /// <summary>
        /// Gets or sets the occupation.
        /// </summary>
        /// <value>
        /// The occupation.
        /// </value>
        public string Occupation { get; set; }

        /// <summary>
        /// Gets or sets the nid.
        /// </summary>
        /// <value>
        /// The nid.
        /// </value>
        public string NatID { get; set; }

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
        /// Gets or sets the location.
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        public string LocationName { get; set; }

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
        public string ProvinceOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string Status { get; set; }
    }
}
