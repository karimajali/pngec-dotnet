using System;

namespace PNGEC.API.Domain.ScheduleLocSummary
{
    public class MKAScheduleLocSummaryModel
    {
        public int MKADistrictId { get; set; }

        public int MKALLGId { get; set; }

        public int MKAWardId { get; set; }

        public int MKALocationId { get; set; }

        public int MKAClanId { get; set; }

        private DateTime scheduledate { get; set; }
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

        public string MKADistrictName { get; set; }

        public string MKALLGName { get; set; }

        public string MKAWardName { get; set; }

        public string MKALocationName { get; set; }

        public string MKAClanName { get; set; }

        public string ScheduleStringDate { get; set; }

        public int INIT { get; set; }

        public int OLD { get; set; }

        public int NEW { get; set; }

        public int UPDATED { get; set; }

        public int UNDERAGE { get; set; }

        public int GHOST { get; set; }

        public int DUPLICATE { get; set; }

        public int DECEASED { get; set; }

        public int DELETED { get; set; }

        public int MKAWARDROLL { get; set; }
    }
}
