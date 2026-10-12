namespace PNGEC.API.Domain.ScheduleLocSummary
{
    public class ScheduleLocSummaryRejectedModel
    {
        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>
        /// The created by.
        /// </value>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the rejected records.
        /// </summary>
        /// <value>
        /// The rejected records.
        /// </value>
        public int RejectedRecords { get; set; }

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        /// <value>
        /// The email address.
        /// </value>
        public string EmailAddress { get; set; }
    }
}
