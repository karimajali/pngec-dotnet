using Microsoft.AspNetCore.Mvc;
using PNGEC.API.Domain.EnrolmentForm;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Domain.MKAEnrolmentForm;
using PNGEC.API.Helper;
using PNGEC.API.Service.EnrolmentForm;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PNGEC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrolmentFormController : ControllerBase
    {
        #region variable declaration

        /// <summary>
        /// The i EnrolmentForm service
        /// </summary>
        private IEnrolmentFormService iEnrolmentFormService;

        /// <summary>
        /// The response
        /// </summary>
        private Response response = null;

        /// <summary>
        /// The response
        /// </summary>
        private Response<EnrolmentFormModel> lstresponse = null;
        /// <summary>
        /// The response
        /// </summary>
        private Response<MKAEnrolmentFormModel> lstmkaresponse = null;
        #endregion

        #region constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="EnrolmentFormController"/> class.
        /// </summary>
        /// <param name="EnrolmentFormService">The EnrolmentForm service.</param>
        public EnrolmentFormController(IEnrolmentFormService EnrolmentFormService)
        {
            iEnrolmentFormService = EnrolmentFormService;
        }
        #endregion

        #region api

        #region add update EnrolmentForm
        /// <summary>
        /// Add/Update EnrolmentForm.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddEnrolmentForm")]
        public async Task<Response> AddEnrolmentForm([FromForm]EnrolmentFormModel model)
        {
            response = new Response();

            if (model != null)
            {
                response = await iEnrolmentFormService.AddEnrolmentForm(model);            }
            else
            {
                response.Status = false;
                response.Message = ResponseMessage.Invalid_Requests_Model;
            }
            return response;
        }

        #endregion

        #region add update EnrolmentForm
        /// <summary>
        /// Add/Update EnrolmentForm.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddMKAEnrolmentForm")]
        public async Task<Response> AddMKAEnrolmentForm([FromForm] MKAEnrolmentFormModel model)
        {
            response = new Response();

            if (model != null)
            {
                response = await iEnrolmentFormService.AddMKAEnrolmentForm(model);
            }
            else
            {
                response.Status = false;
                response.Message = ResponseMessage.Invalid_Requests_Model;
            }
            return response;
        }

        #endregion

        #region get EnrolmentForm list
        /// <summary>
        /// Get EnrolmentForm List
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetEnrolmentFormList")]
        public async Task<IActionResult> GetEnrolmentFormList([FromBody] PageList model)
        {
            lstresponse = new Response<EnrolmentFormModel>();

            if (model != null)
            {
                lstresponse = await iEnrolmentFormService.GetEnrolmentFormList(model);
            }
            else
            {
                lstresponse.Status = false;
                lstresponse.Message = ResponseMessage.Invalid_Requests_Model;
            }
            return Ok(lstresponse);
        }
        #endregion

        #region get EnrolmentForm list
        /// <summary>
        /// Get EnrolmentForm List
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetMKAEnrolmentFormList")]
        public async Task<IActionResult> GetMKAEnrolmentFormList([FromBody] PageList model)
        {
            lstmkaresponse = new Response<MKAEnrolmentFormModel>();

            if (model != null)
            {
                lstmkaresponse = await iEnrolmentFormService.GetMKAEnrolmentFormList(model);
            }
            else
            {
                lstmkaresponse.Status = false;
                lstmkaresponse.Message = ResponseMessage.Invalid_Requests_Model;
            }
            return Ok(lstmkaresponse);
        }
        #endregion

        #region get ward quality check data

        /// <summary>
        /// Gets the ward quality check data.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetWardQualityCheckData")]
        public async Task<IActionResult> GetWardQualityCheckData([FromBody] EnrolmentFormModel model)
        {
            var response = new CommonResponse<List<WardQualityCheckResponseModel>>();

            if (model != null)
            {
                response = await iEnrolmentFormService.GetWardQualityCheckData(model);
            }
            else
            {
                response.Status = false;
                response.Message = ResponseMessage.Invalid_Requests_Model;
            }
            return Ok(response);
        }
        #endregion

        #region get ward quality check data

        /// <summary>
        /// Gets the ward quality check data.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetMKAWardQualityCheckData")]
        public async Task<IActionResult> GetMKAWardQualityCheckData([FromBody] MKAEnrolmentFormModel model)
        {
            var response = new CommonResponse<List<WardQualityCheckResponseModel>>();

            if (model != null)
            {
                response = await iEnrolmentFormService.GetMKAWardQualityCheckData(model);
            }
            else
            {
                response.Status = false;
                response.Message = ResponseMessage.Invalid_Requests_Model;
            }
            return Ok(response);
        }
        #endregion

        #region Delete EnrolmentForm
        /// <summary>
        /// Delete Enrolment Form
        /// </summary>
        /// <param name="enrolmentFormId">The enrolmentFormId.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("DeleteEnrolmentForm/{EnrolmentFormId}")]
        public async Task<IActionResult> DeleteEnrolmentForm(int enrolmentFormId)
        {
            var response = await iEnrolmentFormService.DeleteEnrolmentForm(enrolmentFormId);
            return Ok(response);
        }
        #endregion

        #region Delete EnrolmentForm
        /// <summary>
        /// Delete Enrolment Form
        /// </summary>
        /// <param name="enrolmentFormId">The enrolmentFormId.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("DeleteMKAEnrolmentForm/{EnrolmentFormId}")]
        public async Task<IActionResult> DeleteMKAEnrolmentForm(int enrolmentFormId)
        {
            var response = await iEnrolmentFormService.DeleteMKAEnrolmentForm(enrolmentFormId);
            return Ok(response);
        }
        #endregion
        #endregion
    }
}
