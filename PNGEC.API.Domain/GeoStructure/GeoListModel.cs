using System.Collections.Generic;

namespace PNGEC.API.Domain.GeoStructure
{
    /// <summary>
    /// geo list model
    /// </summary>
    public class GeoListModel
    {
        /// <summary>
        /// Gets or sets the region list.
        /// </summary>
        /// <value>
        /// The region list.
        /// </value>
        public IEnumerable<Region.RegionModel> RegionList { get; set; }

        /// <summary>
        /// Gets or sets the province list.
        /// </summary>
        /// <value>
        /// The province list.
        /// </value>
        public IEnumerable<Province.ProvinceModel> ProvinceList { get; set; }

        /// <summary>
        /// Gets or sets the district list.
        /// </summary>
        /// <value>
        /// The district list.
        /// </value>
        public IEnumerable<District.DistrictModel> DistrictList { get; set; }

        /// <summary>
        /// Gets or sets the LLG list.
        /// </summary>
        /// <value>
        /// The LLG list.
        /// </value>
        public IEnumerable<LLG.LLGModel> LLGList { get; set; }

        /// <summary>
        /// Gets or sets the ward list.
        /// </summary>
        /// <value>
        /// The ward list.
        /// </value>
        public IEnumerable<Ward.WardModel> WardList { get; set; }

        /// <summary>
        /// Gets or sets the location list.
        /// </summary>
        /// <value>
        /// The location list.
        /// </value>
        public IEnumerable<Location.LocationModel> LocationList { get; set; }

        /// <summary>
        /// Gets or sets the location list.
        /// </summary>
        /// <value>
        /// The location list.
        /// </value>
        public IEnumerable<Clan.ClanModel> ClanList { get; set; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        public int Version { get; set; }
    }
}
