using System.Collections.Generic;


namespace PNGEC.API.Domain.MKAGeoStructure
{
    /// <summary>
    /// geo list model
    /// </summary>
    public class MKAGeoListModel
    {
       

        /// <summary>
        /// Gets or sets the district list.
        /// </summary>
        /// <value>
        /// The district list.
        /// </value>
        public IEnumerable<MKADistrictModel> MKADistrictList { get; set; }
        /// <summary>
        /// Gets or sets the LLG list.
        /// </summary>
        /// <value>
        /// The LLG list.
        /// </value>
        public IEnumerable<MKALLGModel> MKALLGList { get; set; }
        /// <summary>
        /// Gets or sets the Ward list.
        /// </summary>
        /// <value>
        /// The WARD list.
        /// </value>
        public IEnumerable<MKAWardModel> MKAWardList { get; set; }
        /// <summary>
        /// Gets or sets the Location list.
        /// </summary>
        /// <value>
        /// The LOCATION list.
        /// </value>
        public IEnumerable<MKALocationModel> MKALocationList { get; set; }

        //DB Added to load all MKAClans
        /// <summary>
        /// Gets or Sets the Clan List
        /// </summary>
        public IEnumerable<MKAClanModel> MKAClanList { get; set; }

        public int Version { get; set; }
    }
}
