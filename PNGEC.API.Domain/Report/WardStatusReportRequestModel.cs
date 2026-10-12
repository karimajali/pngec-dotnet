using System;

namespace PNGEC.API.Domain.Report
{
    public class WardStatusReportRequestModel
    {
        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>
        /// The start date.
        /// </value>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>
        /// The end date.
        /// </value>
        public DateTime? EndDate { get; set; }

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
        /// Gets or sets the location identifier.
        /// </summary>
        /// <value>
        /// The location identifier.
        /// </value>
        public int LocationId { get; set; }

        /// <summary>
        /// Gets or sets the district identifier.
        /// </summary>
        /// <value>
        /// The MKA district identifier.
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

        /// <summary>
        /// Gets or sets the MKA location identifier.
        /// </summary>
        /// <value>
        /// The location identifier.
        /// </value>
        public int MKALocationId { get; set; }

        /// <summary>
        /// Gets or sets the Request Type - PNG or MKA
        /// </summary>
        /// <value>
        /// The device run.
        /// </value>
        public string RequestType { get; set; }

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
        /// Gets or sets the device run.
        /// </summary>
        /// <value>
        /// The device run.
        /// </value>
        public string DeviceRun { get; set; }

        /// <summary>
        /// Gets or sets the device identifier.
        /// </summary>
        /// <value>
        /// The device identifier.
        /// </value>
        public string DeviceID { get; set; }

        /// <summary>
        /// Gets or sets the dpo.
        /// </summary>
        /// <value>
        /// The dpo.
        /// </value>
        public string DPO { get; set; }
    }
}
