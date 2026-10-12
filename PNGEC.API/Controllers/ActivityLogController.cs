using Microsoft.AspNetCore.Mvc;
using PNGEC.API.Domain.Device;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Domain.User;
using PNGEC.API.Helper;
using PNGEC.API.Service.ActivityLog;
using System.Threading.Tasks;

namespace PNGEC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityLogController : ControllerBase
    {
        #region variable declaration

        /// <summary>
        /// The i ActivityLog service
        /// </summary>
        private IActivityLogService iActivityLogService;

        /// <summary>
        /// The response
        /// </summary>
        private Response response = null;

        /// <summary>
        /// The response
        /// </summary>
        private Response<ActivityLogModel> lstresponse = null;
        #endregion

        #region constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityLogController"/> class.
        /// </summary>
        /// <param name="ActivityLogService">The ActivityLog service.</param>
        public ActivityLogController(IActivityLogService activityLogService)
        {
            iActivityLogService = activityLogService;
        }
        #endregion

        #region api

        #region add ActivityLog
        /// <summary>
        /// Add ActivityLog.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddActivityLog")]
        public async Task<Response> AddActivityLog([FromBody] ActivityLogModel model)
        {
            response = new Response();

            if (model != null)
            {
                response = await iActivityLogService.AddActivityLog(model);
            }
            else
            {
                response.Status = false;
                response.Message = ResponseMessage.Invalid_Requests_Model;
            }
            return response;
        }

        #endregion

        #region get ActivityLog list
        /// <summary>
        /// Get ActivityLog List
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetActivityLogList")]
        public async Task<IActionResult> GetActivityLogList([FromBody] PageList model)
        {
            lstresponse = new Response<ActivityLogModel>();

            if (model != null)
            {
                lstresponse = await iActivityLogService.GetActivityLogList(model);
            }
            else
            {
                lstresponse.Status = false;
                lstresponse.Message = ResponseMessage.Invalid_Requests_Model;
            }
            return Ok(lstresponse);
        }
        #endregion

      

        #endregion
    }
}
