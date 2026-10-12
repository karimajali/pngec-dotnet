using System;

namespace PNGEC.API.Domain.ScheduleLocSummary
{
    public class ScheduleLocSummaryModel
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
        /// Gets or sets the location identifier.
        /// </summary>
        /// <value>
        /// The location identifier.
        /// </value>
        public int LocationId { get; set; }

        /// <summary>
        /// Gets or sets the schhedule.
        /// </summary>
        /// <value>
        /// The schhedule.
        /// </value>
        private DateTime scheduledate { get; set; }
        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        public DateTime Scheduledate
        {
            get { return scheduledate; }
            set
            {
                scheduledate = value;

                if (value != null)
                {
                    //DateTime utcDate = DateTime.SpecifyKind(value, DateTimeKind.Utc);
                    //DateTime localDate = utcDate.ToLocalTime();
                    //ScheduleStringDate = String.Format("{0:dd-MM-yyyy}", localDate);
                    ScheduleStringDate = String.Format("{0:dd-MM-yyyy}", value);
                }
                else
                {
                    ScheduleStringDate = string.Empty;
                }
            }
        }

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
        /// Gets or sets the name of the district.
        /// </summary>
        /// <value>
        /// The name of the district.
        /// </value>
        public string DistrictName { get; set; }

        /// <summary>
        /// Gets or sets the name of the LLG.
        /// </summary>
        /// <value>
        /// The name of the LLG.
        /// </value>
        public string LLGName { get; set; }

        /// <summary>
        /// Gets or sets the name of the ward.
        /// </summary>
        /// <value>
        /// The name of the ward.
        /// </value>
        public string WardName { get; set; }

        /// <summary>
        /// Gets or sets the name of the location.
        /// </summary>
        /// <value>
        /// The name of the location.
        /// </value>
        public string LocationName { get; set; }

        /// <summary>
        /// Gets or sets the updated.
        /// </summary>
        /// <value>
        /// The updated.
        /// The Scheduledate.
        /// </value>
        public string ScheduleStringDate { get; set; }

        /// <summary>
        /// Gets or sets the previous total.
        /// </summary>
        /// <value>
        /// The previous total.
        /// </value>
        public int PreviousTotal { get; set; }

        /// <summary>
        /// Gets or sets the old.
        /// </summary>
        /// <value>
        /// The old.
        /// </value>
        public int OLD { get; set; }

        /// <summary>
        /// Gets or sets the new.
        /// </summary>
        /// <value>
        /// The new.
        /// </value>
        public int NEW { get; set; }

        /// <summary>
        /// Gets or sets the updated.
        /// </summary>
        /// <value>
        /// The updated.
        /// </value>
        public int UPDATED { get; set; }

        /// <summary>
        /// Gets or sets the transferin.
        /// </summary>
        /// <value>
        /// The transferin.
        /// </value>
        public int TRANSFERIN { get; set; }

        /// <summary>
        /// Gets or sets the transferout.
        /// </summary>
        /// <value>
        /// The transferout.
        /// </value>
        public int TRANSFEROUT { get; set; }

        /// <summary>
        /// Gets or sets the deceased.
        /// </summary>
        /// <value>
        /// The deceased.
        /// </value>
        public int DECEASED { get; set; }

        /// <summary>
        /// Gets or sets the duplicate.
        /// </summary>
        /// <value>
        /// The duplicate.
        /// </value>
        public int DUPLICATE { get; set; }

        /// <summary>
        /// Gets or sets the ghost.
        /// </summary>
        /// <value>
        /// The ghost.
        /// </value>
        public int GHOST { get; set; }

        /// <summary>
        /// Gets or sets the underage.
        /// </summary>
        /// <value>
        /// The underage.
        /// </value>
        public int UNDERAGE { get; set; }

        /// <summary>
        /// Gets or sets the total.
        /// </summary>
        /// <value>
        /// The total.
        /// </value>
        public int TOTAL { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string UserEmails { get; set; }

        /// <summary>
        /// Gets or sets the user names.
        /// </summary>
        /// <value>
        /// The user names.
        /// </value>
        public string UserNames { get; set; }

        /// <summary>
        /// Gets or sets the management user emails.
        /// </summary>
        /// <value>
        /// The management user emails.
        /// </value>
        public string ManagementUserEmails { get; set; }

        /// <summary>
        /// Gets or sets the management user names.
        /// </summary>
        /// <value>
        /// The management user names.
        /// </value>
        public string ManagementUserNames { get; set; }
    }
}
