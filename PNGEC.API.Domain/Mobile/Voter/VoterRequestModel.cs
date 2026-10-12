namespace PNGEC.API.Domain.Mobile.Voter
{
    public class VoterRequestModel
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the page no.
        /// </summary>
        /// <value>
        /// The page no.
        /// </value>
        public int PageNo { get; set; }

        /// <summary>
        /// Gets or sets the size of the page.
        /// </summary>
        /// <value>
        /// The size of the page.
        /// </value>
        public int PageSize { get; set; }

        /// <summary>
        /// Gets or sets the province identifier.
        /// </summary>
        /// <value>
        /// The province identifier.
        /// </value>
        public int? ProvinceId { get; set; }

        /// <summary>
        /// Gets or sets the name of the database.
        /// </summary>
        /// <value>
        /// The name of the database.
        /// </value>
        public string DatabaseName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the district identifier.
        /// </summary>
        /// <value>
        /// The district identifier.
        /// </value>
        public int DistrictId { get; set; }

        /// <summary>
        /// Gets or sets the LLG identifier.
        /// </summary>
        /// <value>
        /// The LLG identifier.
        /// </value>
        public int LLGId { get; set; }

        /// <summary>
        /// Gets or sets the ward identifier.
        /// </summary>
        /// <value>
        /// The ward identifier.
        /// </value>
        public int WardId { get; set; }

        /// <summary>
        /// Gets or sets the location identifier.
        /// </summary>
        /// <value>
        /// The location identifier.
        /// </value>
        public int LocationId { get; set; }

        /// <summary>
        /// Gets ir Sets the MKA District identifier
        /// </summary>
        public int MKADistrictId { get; set; }

        /// <summary>
        /// Gets or sets the MKA LLG identifier.
        /// </summary>
        /// <value>
        /// The MKA LLG identifier.
        /// </value>
        public int MKALLGId { get; set; }

        /// <summary>
        /// Gets or sets the MKA ward identifier.
        /// </summary>
        /// <value>
        /// The MKA ward identifier.
        /// </value>
        public int MKAWardId { get; set; }

        /// <summary>
        /// Gets or sets the MKA location identifier.
        /// </summary>
        /// <value>
        /// The MKA location identifier.
        /// </value>
        public int MKALocationId { get; set; }
    }
}
