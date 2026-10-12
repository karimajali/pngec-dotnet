namespace PNGEC.API.Domain.Mobile.Voter
{
    /// <summary>
    /// transfer in voter request model class
    /// </summary>
    public class TransferInVoterRequestModel
    {
        /// <summary>
        /// Gets or sets the region identifier.
        /// </summary>
        /// <value>
        /// The region identifier.
        /// </value>
        public int RegionId { get; set; }

        /// <summary>
        /// Gets or sets the province identifier.
        /// </summary>
        /// <value>
        /// The province identifier.
        /// </value>
        public int ProvinceId { get; set; }

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
        /// Gets or sets the key word.
        /// </summary>
        /// <value>
        /// The key word.
        /// </value>
        public string KeyWord { get; set; }

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
        /// Gets or sets the name of the f.
        /// </summary>
        /// <value>
        /// The name of the f.
        /// </value>
        public string FName { get; set; }

        /// <summary>
        /// Gets or sets the name of the l.
        /// </summary>
        /// <value>
        /// The name of the l.
        /// </value>
        public string LName { get; set; }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        /// <value>
        /// The gender.
        /// </value>
        public string Gender { get; set; }

        /// <summary>
        /// Gets or sets the name of the database.
        /// </summary>
        /// <value>
        /// The name of the database.
        /// </value>
        public string DatabaseName { get; set; }

        /// <summary>
        /// Gets or sets the MKA district identifier.
        /// </summary>
        /// <value>
        /// The district identifier.
        /// </value>
        public int MKADistrictId { get; set; }

        /// <summary>
        /// Gets or sets the MKA LLG identifier.
        /// </summary>
        /// <value>
        /// The LLG identifier.
        /// </value>
        public int MKALLGId { get; set; }

        /// <summary>
        /// Gets or sets the MKA ward identifier.
        /// </summary>
        /// <value>
        /// The ward identifier.
        /// </value>
        public int MKAWardId { get; set; }
    }
}
