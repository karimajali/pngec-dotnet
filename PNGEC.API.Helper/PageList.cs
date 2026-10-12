using PNGEC.API.Domain.Base;
using PNGEC.API.Helper;
using System;

namespace PNGEC.API.Domain.Listing
{
    public class PageList : BaseModel
    {
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
        /// Gets or sets the key word.
        /// </summary>
        /// <value>
        /// The key word.
        /// </value>
        public string KeyWord { get; set; }

        /// <summary>
        /// Gets or sets the sort field.
        /// </summary>
        /// <value>
        /// The sort field.
        /// </value>
        public string SortField { get; set; }

        /// <summary>
        /// Gets or sets the sort order.
        /// </summary>
        /// <value>
        /// The sort order.
        /// </value>
        public string SortOrder { get; set; }

        #region Writ RO 
        public int ElectionWritId { get; set; }
        #endregion

        #region File
        /// <summary>
        /// Gets or sets the name of the database.
        /// </summary>
        /// <value>
        /// The name of the database.
        /// </value>
        public string DatabaseName { get; set; }

        /// <summary>
        /// Gets or sets the name of the geo.
        /// </summary>
        /// <value>
        /// The name of the geo.
        /// </value>
        public string GeoName { get; set; }

        /// <summary>
        /// Gets or sets the geo identifier.
        /// </summary>
        /// <value>
        /// The geo identifier.
        /// </value>
        public int GeoId { get; set; }

        /// <summary>
        /// Gets or sets the geo identifier.
        /// </summary>
        /// <value>
        /// The geo identifier.
        /// </value>
        public int GeoClanId { get; set; }

        /// <summary>
        /// Gets or sets the name of the filed.
        /// </summary>
        /// <value>
        /// The name of the filed.
        /// </value>
        public string FiledName { get; set; }

        /// <summary>
        /// Gets or sets the filed value.
        /// </summary>
        /// <value>
        /// The filed value.
        /// </value>
        public string FiledValue { get; set; }

        /// <summary>
        /// Gets or sets the logged user identifier.
        /// </summary>
        /// <value>
        /// The logged user identifier.
        /// </value>
        public int LoggedUserId { get; set; }

        /// <summary>
        /// Gets or sets the clan user identifier.
        /// </summary>
        /// <value>
        /// The clan user identifier.
        /// </value>
        public int ClanId { get; set; }


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
        /// Gets or sets the disability.
        /// </summary>
        /// <value>
        /// The disability.
        /// </value>
        public string Disability { get; set; }

        /// <summary>
        /// Gets or sets the photo.
        /// </summary>
        /// <value>
        /// The photo.
        /// </value>
        public string Photo { get; set; }

        /// <summary>
        /// Gets or sets the date range.
        /// </summary>
        /// <value>
        /// The date range.
        /// </value>
        public string DateRange { get; set; }

        /// <summary>
        /// Gets or sets the elector by i ds.
        /// </summary>
        /// <value>
        /// The elector by i ds.
        /// </value>
        public string ElectorByIDs { get; set; }

        /// <summary>
        /// Gets or sets the date from.
        /// </summary>
        /// <value>
        /// The date from.
        /// </value>
        public DateTime? DateFrom { get; set; }

        /// <summary>
        /// Gets or sets the date to.
        /// </summary>
        /// <value>
        /// The date to.
        /// </value>
        public DateTime? DateTo { get; set; }

        /// <summary>
        /// Gets or sets the alpha split.
        /// </summary>
        /// <value>
        /// The alpha split.
        /// </value>
        public string AlphaSplit { get; set; }

        /// <summary>
        /// Gets or sets the alpha split start.
        /// </summary>
        /// <value>
        /// The alpha split start.
        /// </value>
        public string AlphaSplitStart { get; set; }

        /// <summary>
        /// Gets or sets the alpha split end.
        /// </summary>
        /// <value>
        /// The alpha split end.
        /// </value>
        public string AlphaSplitEnd { get; set; }

        /// <summary>
        /// Gets or sets the name of the page.
        /// </summary>
        /// <value>
        /// The name of the page.
        /// </value>
        public string PageName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is report or CSV.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is report or CSV; otherwise, <c>false</c>.
        /// </value>
        public bool isReportOrCSV { get; set; } = false;

        /// <summary>
        /// Gets or sets the role identifier.
        /// </summary>
        /// <value>
        /// The role identifier.
        /// </value>
        public CommonEnum.Role RoleId { get; set; }

        /// <summary>
        /// Gets or sets the approval status.
        /// </summary>
        /// <value>
        /// The approval status.
        /// </value>
        public string ApprovalStatus { get; set; }
        #endregion

        #region Audit
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public long Id { get; set; }
        #endregion

        #region RollStat
        /// <summary>
        /// Gets or sets from date.
        /// </summary>
        /// <value>
        /// From date.
        /// </value>
        public string FromDate { get; set; }

        /// <summary>
        /// Converts to date.
        /// </summary>
        /// <value>
        /// To date.
        /// </value>
        public string ToDate { get; set; }
        #endregion

        #region Document
        /// <summary>
        /// Gets or sets the type of the document.
        /// </summary>
        /// <value>
        /// The type of the document.
        /// </value>
        public int DocumentType { get; set; }
        #endregion

        #region Log Activity

        public int LogSource { get; set; }

        #endregion
    }
}
