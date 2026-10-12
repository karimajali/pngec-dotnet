namespace PNGEC.API.Domain.Mobile.General
{
    /// <summary>
    /// Sync status response model
    /// </summary>
    public class SyncStatusResponseModel
    {
        /// <summary>
        /// Gets or sets the elector identifier.
        /// </summary>
        /// <value>
        /// The elector identifier.
        /// </value>
        public long ElectorId { get; set; }

        /// <summary>
        /// Gets or sets the location identifier.
        /// </summary>
        /// <value>
        /// The location identifier.
        /// </value>
        public int LocationId { get; set; }

        /// <summary>
        /// Gets or sets the ward identifier.
        /// </summary>
        /// <value>
        /// The ward identifier.
        /// </value>
        public int WardId { get; set; }

        /// <summary>
        /// Gets or sets the LLG identifier.
        /// </summary>
        /// <value>
        /// The LLG identifier.
        /// </value>
        public int LLGId { get; set; }

        /// <summary>
        /// Gets or sets the district identifier.
        /// </summary>
        /// <value>
        /// The district identifier.
        /// </value>
        public int DistrictId { get; set; }

        /// <summary>
        /// Gets or sets the province identifier.
        /// </summary>
        /// <value>
        /// The province identifier.
        /// </value>
        public int ProvinceId { get; set; }

        /// <summary>
        /// Gets or sets the region identifier.
        /// </summary>
        /// <value>
        /// The region identifier.
        /// </value>
        public int RegionId { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is synced.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is synced; otherwise, <c>false</c>.
        /// </value>
        public bool IsSynced { get; set; }

        /// <summary>
        /// Gets or sets the temporary elector identifier.
        /// </summary>
        /// <value>
        /// The temporary elector identifier.
        /// </value>
        public int TempElectorId { get; set; }
    }
}
