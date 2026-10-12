using Microsoft.AspNetCore.Mvc;
using PNGEC.API.Domain.Writ;
using PNGEC.API.Helper;
using PNGEC.API.Service.Writ;
using PNGEC.API.Service.GeoStructure;
using System.Collections.Generic;
using System.Threading.Tasks;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Service.Occupation;

namespace PNGEC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WritROController : ControllerBase
    {
        #region variable declaration

        /// <summary>
        /// The i Writ service
        /// </summary>
        private IWritROService iWritROService;

     
        /// <summary>
        /// The response
        /// </summary>
        private Response response = null;

        /// <summary>
        /// The response
        /// </summary>
        private Response<WritRO> lstresponse = null;
        #endregion

        #region constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="WritController"/> class.
        /// </summary>
        /// <param name="WritService">The Writ service.</param>
        public WritROController(IWritROService WritROService)
        {
            iWritROService = WritROService;
        }
        #endregion

        #region api

        #region add update Writ RO
        /// <summary>
        /// Add/Update Writ.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddUpdateWritRO")]
        public async Task<Response> AddUpdateWritRO([FromBody] WritRO model)
        {
            response = new Response();

            if (model != null)
            {
                response = await iWritROService.AddUpdateWritRO(model);
                response.Status = true;
            }
            else
            {
                response.Status = false;
                response.Message = ResponseMessage.Invalid_Requests_Model;
            }
            return response;
        }

        #endregion

        #region Get Writ RO List
        /// <summary>
        /// Gets the Writ RO list.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetWritROList")]
        public async Task<IActionResult> GetWritROList([FromBody] PageList model)
        {

            var response = new Response();

            if (model != null)
            {
                response = await iWritROService.GetWritROList(model);
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
