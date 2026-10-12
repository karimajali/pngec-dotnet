namespace PNGEC.API.Domain.Report
{
    /// <summary>
    /// get wards number of form11 response model
    /// </summary>
    public class GetWardsNumberOfForm11Response
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
        /// Gets or sets the total ward voters.
        /// </summary>
        /// <value>
        /// The total ward voters.
        /// </value>
        /// 
        public int TotalWardVoters { get; set; }
        /// <summary>
        /// Gets or sets the district identifier.
        /// </summary>
        /// <value>
        /// The district identifier.
        /// </value>
        public int ClanId { get; set; }

        /// <summary>
        /// Gets or sets the number of forms.
        /// </summary>
        /// <value>
        /// The number of forms.
        /// </value>
        public int NumberOfForms { get; set; }
    }
}
