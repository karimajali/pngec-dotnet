using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PNGEC.API.Domain.Base;

namespace PNGEC.API.Domain.Mobile.General
{
    public class MKAGeoListModel : BaseModel
    {

        /// <summary>
        /// Gets or sets the district identifier.
        /// </summary>
        /// <value>
        /// The district identifier.
        /// </value>
        public int DistrictId { get; set; }

        /// <summary>
        /// Gets or sets the name of the district.
        /// </summary>
        /// <value>
        /// The name of the district.
        /// </value>
        public string DistrictName { get; set; }

        /// <summary>
        /// Gets or sets the list of the LLG.
        /// </summary>
        /// <value>
        /// The list of the LLG.
        /// </value>
        public List<MKALLGModel> LLGs { get; set; }
    }
}
