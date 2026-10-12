using PNGEC.API.Domain.Clan;
using System.Collections.Generic;

namespace PNGEC.API.Domain.Mobile.General
{
    /// <summary>
    /// geo list model
    /// </summary>
    public class GeoListModel
    {// <summary>
        /// Gets or sets the region list.
        /// </summary>
        /// <value>
        /// The region list.
        /// </value>
        public IEnumerable<RegionModel> RegionList { get; set; }

        /// <summary>
        /// Gets or sets the province list.
        /// </summary>
        /// <value>
        /// The province list.
        /// </value>
        public IEnumerable<ProvinceModel> ProvinceList { get; set; }

        /// <summary>
        /// Gets or sets the district list.
        /// </summary>
        /// <value>
        /// The district list.
        /// </value>
        public IEnumerable<DistrictModel> DistrictList { get; set; }

        /// <summary>
        /// Gets or sets the LLG list.
        /// </summary>
        /// <value>
        /// The LLG list.
        /// </value>
        public IEnumerable<LLGModel> LLGList { get; set; }

        /// <summary>
        /// Gets or sets the ward list.
        /// </summary>
        /// <value>
        /// The ward list.
        /// </value>
        public IEnumerable<WardModel> WardList { get; set; }

        /// <summary>
        /// Gets or sets the location list.
        /// </summary>
        /// <value>
        /// The location list.
        /// </value>
        public IEnumerable<LocationModel> LocationList { get; set; }


        /// <summary>
        /// Gets or sets the Clan list.
        /// </summary>
        /// <value>
        /// The location list.
        /// </value>
        public IEnumerable<ClanModel> ClanList { get; set; }
    }
}
