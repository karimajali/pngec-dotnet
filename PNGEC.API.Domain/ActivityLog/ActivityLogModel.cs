using Microsoft.AspNetCore.Http;
using PNGEC.API.Domain.Base;
using System;

namespace PNGEC.API.Domain.User
{
    public class ActivityLogModel : BaseModel
    {
        public int LogId { get; set; }
        public int UserID { get; set; }
        public int LogSource { get; set; }
        public string LogSourceName
        {
            get
            {
                return LogSource == 1 ? "ReportServer" : LogSource == 2 ? "Desktop" : "Mobile";
            }
        }
        public string UserName { get; set; }
        public string Event { get; set; }
        public string LogDescription { get; set; }
        public DateTime LogTime { get; set; }       
        public string EmailAddress { get; set; }
        public int ResponseCode { get; set; }



      

    }
}
