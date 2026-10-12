using PNGEC.API.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNGEC.API.Domain.Writ
{
    public class WritModel : BaseModel
    {
        public int Id { get; set; }
        public int RegionId { get; set; }
        public int ProvinceId { get; set; }
        public int DistrictId { get; set; }
        public int LLGId { get; set; }
        public int WardId { get; set; }
        public string WritName { get; set; }
        public string Type { get; set; }       
        public string Scop { get; set; }
        public bool IsLocked { get; set; }
        public DateTime NominationStartDate { get; set; }
        public DateTime NominationEndDate { get; set; }
        public DateTime FirstDayOfPolling { get; set; }
        public DateTime LastDayOfPolling { get; set; }
        public DateTime ReturnOfWritDate { get; set; }
        public DateTime IssueDate { get; set; }
        public string IssuerName { get; set; }
        public string IssuerTitle { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

        public int Batch { get; set; }

        public int ResponseCode { get; set; }

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
        public string WardName { get; set; }

        /// <summary>
        /// RO Name List
        /// </summary>
        public List<WritRO> RONameList { get; set; }

    }

    public class WritRO : BaseModel
    {
        public int WritROId { get; set; }
        public int ElectionWritId { get; set; }
        public int ProvinceId { get; set; }      
        public string ROName { get; set; }
    }
}
