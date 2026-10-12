namespace PNGEC.API.Domain.Mobile.General
{
    /// <summary>
    /// User Model class
    /// </summary>
    public class UserModel
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the region.
        /// </summary>
        /// <value>
        /// The region.
        /// </value>
        public int Region { get; set; }

        /// <summary>
        /// Gets or sets the province.
        /// </summary>
        /// <value>
        /// The province.
        /// </value>
        public int Province { get; set; }

        /// <summary>
        /// Gets or sets the district.
        /// </summary>
        /// <value>
        /// The district.
        /// </value>
        public int District { get; set; }

        /// <summary>
        /// Gets or sets the LLG.
        /// </summary>
        /// <value>
        /// The LLG.
        /// </value>
        public int LLG { get; set; }

        /// <summary>
        /// Gets or sets the ward.
        /// </summary>
        /// <value>
        /// The ward.
        /// </value>
        public int Ward { get; set; }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        public int Location { get; set; }
    }
}
