using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Domain.Mobile.General;
using PNGEC.API.Helper;
using PNGEC.API.Service.Occupation;

namespace PNGEC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OccupationController : ControllerBase
    {
        #region variable declaration

        /// <summary>
        /// The i device service
        /// </summary>
        private IOccupationService iOccupationService;

        #endregion

        #region constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="OccupationController"/> class.
        /// </summary>
        /// <param name="occupationService">The occupation service.</param>
        public OccupationController(IOccupationService occupationService)
        {
            iOccupationService = occupationService;
        }
        #endregion

        #region api

        /// <summary>
        /// Adds the update occupation.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddUpdateOccupation")]
        public async Task<Response> AddUpdateOccupation([FromBody] OccupationModel model)
        {
            var response = new Response();

            if (model != null)
            {
                response = await iOccupationService.AddUpdateOccupation(model);
            }
            else
            {
                response.Status = false;
                response.Message = ResponseMessage.Invalid_Requests_Model;
            }
            return response;
        }


        /// <summary>
        /// Gets the occupation list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetOccupationList")]
        public async Task<IActionResult> GetOccupationList([FromBody] PageList model)
        {
            var response = new Response();

            if (model != null)
            {
                response = await iOccupationService.GetOccupationList(model);
            }
            else
            {
                response.Status = false;
                response.Message = ResponseMessage.Invalid_Requests_Model;
            }
            return Ok(response);
        }

        #endregion
    }
}
