namespace PNGEC.API.Domain.ScheduleLocSummary
{
    public class ScheduleLocSummaryRejectedByRDModel
    {
        /// <summary>
        /// Gets or sets the elector identifier.
        /// </summary>
        /// <value>
        /// The elector identifier.
        /// </value>
        public long ElectorId { get; set; }

        /// <summary>
        /// Gets or sets the name of the l.
        /// </summary>
        /// <value>
        /// The name of the l.
        /// </value>
        public string LName { get; set; }

        /// <summary>
        /// Gets or sets the name of the f.
        /// </summary>
        /// <value>
        /// The name of the f.
        /// </value>
        public string FName { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the name of the region.
        /// </summary>
        /// <value>
        /// The name of the region.
        /// </value>
        public string RegionName { get; set; }

        /// <summary>
        /// Gets or sets the name of the province.
        /// </summary>
        /// <value>
        /// The name of the province.
        /// </value>
        public string ProvinceName { get; set; }

        /// <summary>
        /// Gets or sets the province identifier.
        /// </summary>
        /// <value>
        /// The province identifier.
        /// </value>
        public int ProvinceId { get; set; }

        /// <summary>
        /// Gets or sets the user emails.
        /// </summary>
        /// <value>
        /// The user emails.
        /// </value>
        public string UserEmails { get; set; }
    }
}
