using Microsoft.AspNetCore.Mvc;
using PNGEC.API.Domain.GeoStructure;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Helper;
using PNGEC.API.Service.GeoStructure;
using System.Threading.Tasks;

namespace PNGEC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeoStructureController : ControllerBase
    {
        #region variable declaration

        /// <summary>
        /// The i GeoStructure service
        /// </summary>
        private IGeoStructureService iGeoStructureService;

        /// <summary>
        /// The response
        /// </summary>
        private Response response = null;

        /// <summary>
        /// The response
        /// </summary>
        private Response<GeoStructureModel> lstresponse = null;
        #endregion

        #region constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="GeoStructureController"/> class.
        /// </summary>
        /// <param name="geoStructureService">The GeoStructure service.</param>
        public GeoStructureController(IGeoStructureService geoStructureService)
        {
            iGeoStructureService = geoStructureService;
        }
        #endregion

        #region api

        #region add update GeoStructure
        /// <summary>
        /// Add/Update GeoStructure.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddUpdateGeoStructure")]
        public async Task<Response> AddUpdateGeoStructure([FromBody]GeoStructureModel model)
        {
            response = new Response();

            if (model != null)
            {
                response = await iGeoStructureService.AddUpdateGeoStructure(model);
            }
            else
            {
                response.Status = false;
                response.Message = ResponseMessage.Invalid_Requests_Model;
            }
            return response;
        }

        #endregion

        #region get GeoStructure list
        /// <summary>
        /// Add/Update GeoStructure.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetGeoStructureList")]
        public async Task<IActionResult> GetGeoStructureList([FromBody] PageList model)
        {
            lstresponse = new Response<GeoStructureModel>();

            if (model != null)
            {
                lstresponse = await iGeoStructureService.GetGeoStructureList(model);
            }
            else
            {
                lstresponse.Status = false;
                lstresponse.Message = ResponseMessage.Invalid_Requests_Model;
            }
            return Ok(lstresponse);
        }

        [HttpGet]
        [Route("GetGeoList/{userId}")]
        public async Task<IActionResult> GetGeoList([FromRoute]int userId)
        {
            Response response = new Response();
            response = await iGeoStructureService.GetGeoList(userId);
            return Ok(response);
        }
        #endregion

        #endregion
    }
}
