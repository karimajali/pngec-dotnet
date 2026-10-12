using System;

namespace PNGEC.API.Domain.Setting
{
    /// <summary>
    /// setting model
    /// </summary>
    public class SettingModel
    {
        /// <summary>
        /// Gets or sets the dc password.
        /// </summary>
        /// <value>
        /// The dc password.
        /// </value>
        public string DCPassword { get; set; }

        /// <summary>
        /// Gets or sets the dc version.
        /// </summary>
        /// <value>
        /// The dc version.
        /// </value>
        public int DCVersion { get; set; }

        /// <summary>
        /// Gets or sets the geo version.
        /// </summary>
        /// <value>
        /// The geo version.
        /// </value>
        public int GeoVersion { get; set; }

        /// <summary>
        /// Gets or sets the occupation version.
        /// </summary>
        /// <value>
        /// The occupation version.
        /// </value>
        public int OccupationVersion { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is dc change.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is dc change; otherwise, <c>false</c>.
        /// </value>
        public bool IsDCChange { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is geo change.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is geo change; otherwise, <c>false</c>.
        /// </value>
        public bool IsGeoChange { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is occupation change.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is occupation change; otherwise, <c>false</c>.
        /// </value>
        public bool IsOccupationChange { get; set; }

        /// <summary>
        /// Gets or sets the dc date reset.
        /// </summary>
        /// <value>
        /// The dc date reset.
        /// </value>
        public DateTime DCDateReset { get; set; }

        /// <summary>
        /// Gets or sets the geo refresh date.
        /// </summary>
        /// <value>
        /// The geo refresh date.
        /// </value>
        public DateTime GeoRefreshDate { get; set; }

        /// <summary>
        /// Gets or sets the occupation refresh date.
        /// </summary>
        /// <value>
        /// The occupation refresh date.
        /// </value>
        public DateTime OccupationRefreshDate { get; set; }

        /// <summary>
        /// Gets or sets the action by.
        /// </summary>
        /// <value>
        /// The action by.
        /// </value>
        public int ActionBy { get; set; }
    }
}
