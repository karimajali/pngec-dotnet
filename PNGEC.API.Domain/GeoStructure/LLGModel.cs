using PNGEC.API.Domain.Base;
using PNGEC.API.Domain.Ward;
using System;
using System.Collections.Generic;

namespace PNGEC.API.Domain.LLG
{
    public class LLGModel : BaseModel
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
        /// Gets or sets the LLG identifier.
        /// </summary>
        /// <value>
        /// The LLG identifier.
        /// </value>
        public int LLGId { get; set; }

        /// <summary>
        /// Gets or sets the LLG name.
        /// </summary>
        /// <value>
        /// The LLG name.
        /// </value>
        public string LLGName { get; set; }

        /// <summary>
        /// Gets or sets the RegionId.
        /// </summary>
        /// <value>
        /// The RegionId.
        /// </value>
        public int RegionId { get; set; }

        /// <summary>
        /// Gets or sets the ProvinceId.
        /// </summary>
        /// <value>
        /// The ProvinceId.
        /// </value>
        public int ProvinceId { get; set; }

        /// <summary>
        /// Gets or sets the DistrictId.
        /// </summary>
        /// <value>
        /// The DistrictId.
        /// </value>
        public int DistrictId { get; set; }

        /// <summary>
        /// Gets or sets the DistrictName.
        /// </summary>
        /// <value>
        /// The DistrictName.
        /// </value>
        public string DistrictName { get; set; }

        /// <summary>
        /// Gets or sets the LLG status.
        /// </summary>
        /// <value>
        /// The LLG status.
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
        /// Gets or sets the ward list
        /// </summary>
        /// <value>
        /// The wardlist
        /// </value>
        public List<WardModel> Childrens { get; set; }

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
        /// Gets or sets the RegionName
        /// </summary>
        /// <value>
        /// The RegionName
        /// </value>
        public string RegionName { get; set; }

        /// <summary>
        /// Gets or sets the ProvinceName.
        /// </summary>
        /// <value>
        /// The ProvinceName.
        /// </value>
        public string ProvinceName { get; set; }
    }
}
