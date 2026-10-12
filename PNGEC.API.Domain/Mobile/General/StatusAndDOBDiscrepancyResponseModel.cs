using System;

namespace PNGEC.API.Domain.Mobile.General
{
    public class StatusAndDOBDiscrepancyResponseModel
    {
        /// <summary>
        /// Gets or sets the row no.
        /// </summary>
        /// <value>
        /// The row no.
        /// </value>
        public long RowNo { get; set; }

        /// <summary>
        /// Gets or sets the elector identifier.
        /// </summary>
        /// <value>
        /// The elector identifier.
        /// </value>
        public long ElectorId { get; set; }

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

        private DateTime dbo;
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
                    DateOfBirth = String.Format("{0:dd-MM-yyyy}", value);
                }
                else
                {
                    DateOfBirth = string.Empty;
                }
            }
        }
        public string DateOfBirth { get; set; }

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

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        public string LocationName { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string Status { get; set; }
    }
}
