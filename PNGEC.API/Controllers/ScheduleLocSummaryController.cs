using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Domain.ScheduleLocSummary;
using PNGEC.API.Helper;
using PNGEC.API.Service.ScheduleLocSummary;

namespace PNGEC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleLocSummaryController : ControllerBase
    {
        #region variable declaration

        /// <summary>
        /// The i schedule loc summary service
        /// </summary>
        private IScheduleLocSummaryService iScheduleLocSummaryService;

        /// <summary>
        /// The response
        /// </summary>
        private Response<ScheduleLocSummaryModel> lstresponse = null;

        /// <summary>
        /// The lstmkaresponse
        /// </summary>
        private Response<MKAScheduleLocSummaryModel> lstmkaresponse = null;

        /// <summary>
        /// The response
        /// </summary>
        private Response response = null;
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ScheduleLocSummaryController"/> class.
        /// </summary>
        /// <param name="scheduleLocSummaryService">The schedule loc summary service.</param>
        public ScheduleLocSummaryController(IScheduleLocSummaryService scheduleLocSummaryService)
        {
            iScheduleLocSummaryService = scheduleLocSummaryService;
        }
        #endregion

        #region api

        #region get roll stat list
        /// <summary>
        /// Gets the roll stat list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetRollStatList")]
        public async Task<IActionResult> GetRollStatList([FromBody]PageList model)
        {
            lstresponse = new Response<ScheduleLocSummaryModel>();

            if (model != null)
            {
                lstresponse = await iScheduleLocSummaryService.GetRollStatList(model);
            }
            else
            {
                lstresponse.Status = false;
                lstresponse.Message = ResponseMessage.Invalid_Requests_Model;
            }
            return Ok(lstresponse);
        }
        #endregion

        #region get roll stat list
        /// <summary>
        /// Gets the roll stat list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetMKARollStatList")]
        public async Task<IActionResult> GetMKARollStatList([FromBody] PageList model)
        {
            lstmkaresponse = new Response<MKAScheduleLocSummaryModel>();

            if (model != null)
            {
                lstmkaresponse = await iScheduleLocSummaryService.GetMKARollStatList(model);
            }
            else
            {
                lstmkaresponse.Status = false;
                lstmkaresponse.Message = ResponseMessage.Invalid_Requests_Model;
            }
            return Ok(lstmkaresponse);
        }
        #endregion

        #region get latest roll stat list

        /// <summary>
        /// Gets the latest roll stat list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetLatestRollStatList")]
        public async Task<IActionResult> GetLatestRollStatList([FromBody] PageList model)
        {
            lstresponse = new Response<ScheduleLocSummaryModel>();

            if (model != null)
            {
                lstresponse = await iScheduleLocSummaryService.GetLatestRollStatList(model);
            }
            else
            {
                lstresponse.Status = false;
                lstresponse.Message = ResponseMessage.Invalid_Requests_Model;
            }
            return Ok(lstresponse);
        }
        #endregion

        #region schedule notification
        /// <summary>
        /// Schedules the notification.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("ScheduleNotification")]
        public async Task<IActionResult> ScheduleNotification()
        {
            try
            {
                response = await iScheduleLocSummaryService.ScheduleNotification();

            }
            catch (Exception)
            {

            }
            return Ok(response);
        }
        #endregion

        #region staging schedule notification
        /// <summary>
        /// Stagings the schedule notification.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("StagingScheduleNotification")]
        public async Task<List<ScheduleLocSummaryModel>> StagingScheduleNotification()
        {
            var response = await iScheduleLocSummaryService.StagingScheduleNotification();

            return response;
        }
        #endregion

        #region preliminary schedule notification
        /// <summary>
        /// Preliminaries the schedule notification.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("PreliminaryScheduleNotification")]
        public async Task<List<ScheduleLocSummaryModel>> PreliminaryScheduleNotification()
        {
            var response = await iScheduleLocSummaryService.PreliminaryScheduleNotification();

            return response;
        }
        #endregion

        #region final schedule notification
        /// <summary>
        /// Finals the schedule notification.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("FinalScheduleNotification")]
        public async Task<List<ScheduleLocSummaryModel>> FinalScheduleNotification()
        {
            var response = await iScheduleLocSummaryService.FinalScheduleNotification();

            return response;
        }
        #endregion

        #region rejected schedule notification
        /// <summary>
        /// Rejecteds the schedule notification.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("RejectedScheduleNotification")]
        public async Task<List<ScheduleLocSummaryRejectedModel>> RejectedScheduleNotification()
        {
            var response = await iScheduleLocSummaryService.RejectedScheduleNotification();

            return response;
        }
        #endregion

        #region rejected by rd schedule notification
        /// <summary>
        /// Rejecteds the by rd schedule notification.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("RejectedByRDScheduleNotification")]
        public async Task<List<ScheduleLocSummaryRejectedByRDModel>> RejectedByRDScheduleNotification()
        {
            var response = await iScheduleLocSummaryService.RejectedByRDScheduleNotification();

            return response;
        }
        #endregion

        #endregion
    }
}