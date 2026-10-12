using PNGEC.API.Domain.Device;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Domain.User;
using PNGEC.API.Helper;
using PNGEC.API.Repository.ActivityLog;
using PNGEC.API.Repository.Device;
using System;
using System.Threading.Tasks;

namespace PNGEC.API.Service.ActivityLog
{
    public class ActivityLogService : IActivityLogService
    {
        #region variable declaration
        /// <summary>
        /// The i ActivityLog repository
        /// </summary>
        private readonly IActivityLogRepository iActivityLogRepository;

        /// <summary>
        /// The response
        /// </summary>
        private Response response = null;

        /// <summary>
        /// The lstresponse
        /// </summary>
        private Response<ActivityLogModel> lstresponse = null;

        /// <summary>
        /// The result
        /// </summary>
        private Result<ActivityLogModel> result = null;

        /// <summary>
        /// The module name
        /// </summary>
        private const string ModuleName = "ActivityLog";
        #endregion

        #region constructor 
        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityLogService"/> class.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public ActivityLogService(string connection)
        {
            iActivityLogRepository = new ActivityLogRepository(connection);
        }
        #endregion

        #region service

        #region add update ActivityLog
        /// <summary>
        /// Adds/update the ActivityLog
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Response> AddActivityLog(ActivityLogModel model)
        {
            try
            {
                response = new Response();
                ActivityLogModel activityLog = await iActivityLogRepository.AddActivityLog(model);

                response.Status = activityLog.LogId > 0;

                if (activityLog != null)
                {
                    response.Message = ResponseMessage.Record_Add_Update_Success;
                    response.Data = activityLog;
                }
                else
                {
                    response.Status = false;
                    response.Message = "something went wrong, try again later";
                }


            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("ActivityLogService", "AddActivityLog", ex);
            }
            return response;
        }
        #endregion

        #region get ActivityLog list
        /// <summary>
        /// Gets the ActivityLog list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Response<ActivityLogModel>> GetActivityLogList(PageList model)
        {
            lstresponse = new Response<ActivityLogModel>();

            try
            {
                result = await iActivityLogRepository.GetActivityLogList(model);

                if (result != null && result.ListItems != null)
                {
                    lstresponse.Data = result;
                    lstresponse.Status = true;
                }
                else
                {
                    lstresponse.Message = ResponseMessage.RecordNotExist;
                }
            }
            catch (Exception ex)
            {
                lstresponse.Message = ex.Message;
                ErrorLog.Write("ActivityLogService", "GetActivityLogList", ex);
            }

            return lstresponse;
        }
        #endregion



        #endregion
    }
}
