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
    public class WritController : ControllerBase
    {
        #region variable declaration

        /// <summary>
        /// The i Writ service
        /// </summary>
        private IWritService iWritService;

     
        /// <summary>
        /// The response
        /// </summary>
        private Response response = null;

        /// <summary>
        /// The response
        /// </summary>
        private Response<WritModel> lstresponse = null;
        #endregion

        #region constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="WritController"/> class.
        /// </summary>
        /// <param name="WritService">The Writ service.</param>
        public WritController(IWritService WritService)
        {
            iWritService = WritService;
        }
        #endregion

        #region api

        #region add update Writ
        /// <summary>
        /// Add/Update Writ.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddUpdateWrit")]
        public async Task<Response> AddUpdateWrit([FromBody] WritModel model)
        {
            response = new Response();

            if (model != null)
            {
                response = await iWritService.AddUpdateWrit(model);
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

        #region Get Writ List
        /// <summary>
        /// Gets the Writ list.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetWritList")]
        public async Task<IActionResult> GetWritList([FromBody] PageList model)
        {

            var response = new Response();

            if (model != null)
            {
                 response = await iWritService.GetWritList(model);
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
