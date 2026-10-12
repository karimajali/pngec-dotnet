using PNGEC.API.Domain.Base;
using PNGEC.API.Domain.LLG;
using System;
using System.Collections.Generic;

namespace PNGEC.API.Domain.District
{
    public class DistrictModel : BaseModel
    {
        /// <summary>
        /// Gets or sets the modifiedDate
        /// </summary>
        /// <value>
        /// The modifiedDate
        /// </value>
        private DateTime modifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the Created Date
        /// </summary>
        /// <value>
        /// The Created Date.
        /// </value>
        private DateTime createdDate { get; set; }

        /// <summary>
        /// Gets or sets the status
        /// </summary>
        /// <value>
        /// The status
        /// </value>
        private bool status { get; set; }

        /// <summary>
        /// Gets or sets the District identifier.
        /// </summary>
        /// <value>
        /// The District identifier.
        /// </value>
        public int DistrictId { get; set; }

        /// <summary>
        /// Gets or sets the District name.
        /// </summary>
        /// <value>
        /// The District name.
        /// </value>
        public string DistrictName { get; set; }

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
        /// Gets or sets the  batch identifier.
        /// </summary>
        /// <value>
        /// The  batch identifier.
        /// </value>
        public int BatchID { get; set; }

        /// <summary>
        /// Gets or sets the ProvinceName.
        /// </summary>
        /// <value>
        /// The ProvinceName.
        /// </value>
        public string ProvinceName { get; set; }

        /// <summary>
        /// Gets or sets the District status.
        /// </summary>
        /// <value>
        /// The District status.
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
        /// Gets or sets the Update By UserName
        /// </summary>
        /// <value>
        /// The Update By UserName
        /// </value>
        public string UpdateByUserName { get; set; }

        /// <summary>
        /// Gets or sets the RegionName
        /// </summary>
        /// <value>
        /// The RegionName
        /// </value>
        public string RegionName { get; set; }

        /// <summary>
        /// Gets or sets the Comment
        /// </summary>
        /// <value>
        /// The Comment
        /// </value>
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets the LLG list
        /// </summary>
        /// <value>
        /// The LLG list
        /// </value>
        public List<LLGModel> Childrens { get; set; }
    }
}
