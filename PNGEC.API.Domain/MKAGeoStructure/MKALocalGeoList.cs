using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNGEC.API.Domain.MKAGeoStructure
{
    public static class MKALocalGeoList
    {
        /// <summary>
        /// Gets or sets the region list.
        /// </summary>
        /// <value>
        /// The region list.
        /// </value>
        //public static IEnumerable<RegionModel> RegionList { get; set; }

        /// <summary>
        /// Gets or sets the province list.
        /// </summary>
        /// <value>
        /// The province list.
        /// </value>
        //public static IEnumerable<Province.ProvinceModel> ProvinceList { get; set; }

        /// <summary>
        /// Gets or sets the district list.
        /// </summary>
        /// <value>
        /// The district list.
        /// </value>
        public static IEnumerable<MKADistrictModel> DistrictList { get; set; }

        /// <summary>
        /// Gets or sets the LLG list.
        /// </summary>
        /// <value>
        /// The LLG list.
        /// </value>
        public static IEnumerable<MKALLGModel> LLGList { get; set; }

        /// <summary>
        /// Gets or sets the ward list.
        /// </summary>
        /// <value>
        /// The ward list.
        /// </value>
        public static IEnumerable<MKAWardModel> WardList { get; set; }

        /// <summary>
        /// Gets or sets the location list.
        /// </summary>
        /// <value>
        /// The location list.
        /// </value>
        public static IEnumerable<MKALocationModel> LocationList { get; set; }
    }
}
