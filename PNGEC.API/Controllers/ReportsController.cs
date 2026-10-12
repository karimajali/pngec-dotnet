using Microsoft.AspNetCore.Mvc;
using PNGEC.API.Domain.Mobile.Voter;
using PNGEC.API.Domain.Report;
using PNGEC.API.Helper;
using PNGEC.API.Service.Report;
using System.Threading.Tasks;

namespace PNGEC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        #region variable declaration

        /// <summary>
        /// The i reports service
        /// </summary>
        private IReportsService iReportsService;

        /// <summary>
        /// The response
        /// </summary>
        private Response response = null;

        /// <summary>
        /// The voterResponse
        /// </summary>
        private Response<WardRollBookReortModel> voterResponse = null;
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ReportsController"/> class.
        /// </summary>
        /// <param name="reportsService">The reports service.</param>
        public ReportsController(IReportsService reportsService)
        {
            iReportsService = reportsService;
        }
        #endregion

        #region api

        #region Get Ward Status Report
        /// <summary>
        /// Gets the ward status report.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetWardStatusReport")]
        public async Task<IActionResult> GetWardStatusReport(WardStatusReportRequestModel model)
        {
            voterResponse = new Response<WardRollBookReortModel>();
            if (model != null)
            {
                voterResponse = await iReportsService.GetWardStatusReport(model);
            }
            else
            {
                voterResponse.Status = false;
                voterResponse.Message = ResponseMessage.Invalid_Requests_Model;
            }

            return Ok(voterResponse);
        }
        #endregion

        #region Get wards number of form11
        /// <summary>
        /// Gets the wards number of form11.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetWardsNumberOfForm11")]
        public async Task<IActionResult> GetWardsNumberOfForm11(GetWardsNumberOfForm11Request model)
        {
            var response = new Response();

            if (model != null)
            {
                if (model.MajorVersionNumber < 2 && model.MinorVersionNumber < 8)
                {
                    response.Data = null;
                    response.Status = false;
                    response.Message = "Update to latest software to generate reports.";
                }
                else
                {
                    response = await iReportsService.GetWardsNumberOfForm11(model);
                }
            }
            else
            {
                response.Status = false;
                response.Message = ResponseMessage.Invalid_Requests_Model;
            }

            return Ok(response);
        }
        #endregion
        #region Get wards number of form11
        /// <summary>
        /// Gets the wards number of form11.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetWardsNumberOfMKAForm11")]
        public async Task<IActionResult> GetWardsNumberOfMKAForm11(GetWardsNumberOfForm11Request model)
        {
            var response = new Response();
            if (model != null)
            {
                response = await iReportsService.GetWardsNumberOfMKAForm11(model);
            }
            else
            {
                response.Status = false;
                response.Message = ResponseMessage.Invalid_Requests_Model;
            }

            return Ok(response);
        }
        #endregion
        #endregion
    }
}
