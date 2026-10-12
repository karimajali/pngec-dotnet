using PNGEC.API.Domain.Region;
using System.Collections.Generic;

namespace PNGEC.API.Domain.GeoStructure
{
    /// <summary>
    /// LocalGeoList
    /// </summary>
    public static class LocalGeoList
    {
        /// <summary>
        /// Gets or sets the region list.
        /// </summary>
        /// <value>
        /// The region list.
        /// </value>
        public static IEnumerable<RegionModel> RegionList { get; set; }

        /// <summary>
        /// Gets or sets the province list.
        /// </summary>
        /// <value>
        /// The province list.
        /// </value>
        public static IEnumerable<Province.ProvinceModel> ProvinceList { get; set; }

        /// <summary>
        /// Gets or sets the district list.
        /// </summary>
        /// <value>
        /// The district list.
        /// </value>
        public static IEnumerable<District.DistrictModel> DistrictList { get; set; }

        /// <summary>
        /// Gets or sets the LLG list.
        /// </summary>
        /// <value>
        /// The LLG list.
        /// </value>
        public static IEnumerable<LLG.LLGModel> LLGList { get; set; }

        /// <summary>
        /// Gets or sets the ward list.
        /// </summary>
        /// <value>
        /// The ward list.
        /// </value>
        public static IEnumerable<Ward.WardModel> WardList { get; set; }

        /// <summary>
        /// Gets or sets the location list.
        /// </summary>
        /// <value>
        /// The location list.
        /// </value>
        public static IEnumerable<Location.LocationModel> LocationList { get; set; }
    }
}
