namespace PNGEC.API.Domain.EnrolmentForm
{
    /// <summary>
    /// ward quality check response model
    /// </summary>
    public class WardQualityCheckResponseModel
    {
        /// <summary>
        /// Gets or sets the form number.
        /// </summary>
        /// <value>
        /// The form number.
        /// </value>
        public string FormNumber { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is used.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is used; otherwise, <c>false</c>.
        /// </value>
        public bool IsUsed { get; set; }
    }
}
