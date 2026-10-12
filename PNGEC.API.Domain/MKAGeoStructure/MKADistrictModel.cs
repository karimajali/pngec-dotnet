using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PNGEC.API.Domain.Base;

namespace PNGEC.API.Domain.MKAGeoStructure
{
    public class MKADistrictModel : BaseModel
    {
        /// <summary>
        /// Gets or sets the modifiedDate
        /// </summary>
        /// <value>
        /// The modifiedDate
        /// </value>
        private DateTime modifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the status
        /// </summary>
        /// <value>
        /// The status
        /// </value>
        private bool status;

        /// <summary>
        /// Gets or sets the MKADistrict identifier.
        /// </summary>
        /// <value>
        /// The MKADistrict identifier.
        /// </value>
        public int DistrictId { get; set; }

        /// <summary>
        /// Gets or sets the MKADistrict name.
        /// </summary>
        /// <value>
        /// The MKADistrict name.
        /// </value>
        public string DistrictName { get; set; }

        /// <summary>
        /// Gets or sets the MKADistrict status.
        /// </summary>
        /// <value>
        /// The Ward status.
        /// </value>
        public bool Status
        {
            get { return status; }
            set
            {
                status = value;
                if (value)
                {
                    StatusName = "Active";
                }
                else
                {
                    StatusName = "InActive";
                }
            }
        }

        /// <summary>
        /// Gets or sets the response code.
        /// </summary>
        /// <value>
        /// The response code.
        /// </value>
        public int ResponseCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the status.
        /// </summary>
        /// <value>
        /// The name of the status.
        /// </value>
        public string StatusName { get; set; }

        /// <summary>
        /// Gets or sets the Created Date
        /// </summary>
        /// <value>
        /// The Created Date.
        /// </value>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the ModifiedDate
        /// </summary>
        /// <value>
        /// The Modified date.
        /// </value>
        public DateTime ModifiedDate
        {
            get { return modifiedDate; }
            set
            {
                modifiedDate = value;
                LastChangeDate = String.Format("{0:dd-MM-yyyy}", value);
            }
        }

        /// <summary>
        /// Gets or sets the LastChangeDate
        /// </summary>
        /// <value>
        /// The LastChangeDate
        /// </value>
        public string LastChangeDate { get; set; }
        /// <summary>
        /// Gets or sets the district list
        /// </summary>
        /// <value>
        /// The wardlist
        /// </value>
        public List<MKALLGModel> Childrens { get; set; }

        /// <summary>
        /// Gets or sets the Update By UserName
        /// </summary>
        /// <value>
        /// The Update By UserName
        /// </value>
        public string UpdateByUserName { get; set; }

        /// <summary>
        /// Gets or sets the Comment
        /// </summary>
        /// <value>
        /// The Comment
        /// </value>
        public string Comment { get; set; }
    }
}

