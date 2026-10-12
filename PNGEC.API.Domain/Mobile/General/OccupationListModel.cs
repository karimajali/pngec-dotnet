using System.Collections.Generic;

namespace PNGEC.API.Domain.Mobile.General
{
    /// <summary>
    /// Occupation list model class
    /// </summary>
    internal class OccupationListModel
    {
        /// <summary>
        /// Gets or sets the occupation list.
        /// </summary>
        /// <value>
        /// The occupation list.
        /// </value>
        public IEnumerable<OccupationModel> OccupationList { get; set; }
    }
}
