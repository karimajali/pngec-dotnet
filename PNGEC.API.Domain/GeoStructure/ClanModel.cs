using PNGEC.API.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNGEC.API.Domain.Clan
{
    public class ClanModel : BaseModel
    {
        public int ClanId { get; set; }
        public int RegionId { get; set; }
        public int ProvinceId { get; set; }
        public int DistrictId { get; set; }
        public int LLGId { get; set; }
        public int WardId { get; set; }
        public string ClanName { get; set; }
        public bool Status { get; set; }
        public string Description { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }       
        public int ResponseCode { get; set; }


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

    }
}
