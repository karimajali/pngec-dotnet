using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PNGEC.API.Domain.Base;

namespace PNGEC.API.Domain.MKAGeoStructure
{
    public class MKALocationModel : BaseModel
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
        private bool status { get; set; }

        /// <summary>
        /// Gets or sets the Location identifier.
        /// </summary>
        /// <value>
        /// The Location identifier.
        /// </value>
        public int LocationId { get; set; }

        /// <summary>
        /// Gets or sets the Location name.
        /// </summary>
        /// <value>
        /// The Location name.
        /// </value>
        public string LocationName { get; set; }

        
        /// <summary>
        /// Gets or sets the DistrictId.
        /// </summary>
        /// <value>
        /// The DistrictId.
        /// </value>
        public int DistrictId { get; set; }

        /// <summary>
        /// Gets or sets the LLGId.
        /// </summary>
        /// <value>
        /// The LLGId.
        /// </value>
        public int LLGId { get; set; }

        /// <summary>
        /// Gets or sets the WardId.
        /// </summary>
        /// <value>
        /// The WardId.
        /// </value>
        public int WardId { get; set; }

        /// <summary>
        /// Gets or sets the WardName.
        /// </summary>
        /// <value>
        /// The WardName.
        /// </value>
        public string WardName { get; set; }

        /// <summary>
        /// Gets or sets the Location status.
        /// </summary>
        /// <value>
        /// The Location status.
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
        /// Gets or sets the name of the status.
        /// </summary>
        /// <value>
        /// The name of the status.
        /// </value>
        public string StatusName { get; set; }

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


        /// <summary>
        /// Gets or sets the DistrictName.
        /// </summary>
        /// <value>
        /// The DistrictName.
        /// </value>
        public string DistrictName { get; set; }

        /// <summary>
        /// Gets or sets the LLGName.
        /// </summary>
        /// <value>
        /// The LLGName.
        /// </value>
        public string LLGName { get; set; }

        /// <summary>
        /// Gets or sets the name of the ward no ward.
        /// </summary>
        /// <value>
        /// The name of the ward no ward.
        /// </value>
        public string WardNoWardName { get; set; }

        private int wardnumber { get; set; }
        /// <summary>
        /// Gets or sets the Ward number.
        /// </summary>
        /// <value>
        /// The Ward number.
        /// </value>
        public int WardNumber
        {

            get { return wardnumber; }
            set
            {
                wardnumber = value;
                WardNoWardName = wardnumber + "-" + WardName;
            }
        }
    }
}
