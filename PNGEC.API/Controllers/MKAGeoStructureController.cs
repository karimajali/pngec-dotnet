using Microsoft.AspNetCore.Mvc;
using PNGEC.API.Domain.GeoStructure;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Domain.MKAGeoStructure;
using PNGEC.API.Helper;
using PNGEC.API.Service.GeoStructure;
using System.Threading.Tasks;

namespace PNGEC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MKAGeoStructureController : ControllerBase
    {
        #region variable declaration

        /// <summary>
        /// The i GeoStructure service
        /// </summary>
        private IMKAGeoStructureService iMKAGeoStructureService;

        /// <summary>
        /// The response
        /// </summary>
        private Response response = null;

        /// <summary>
        /// The response
        /// </summary>
        private Response<MKAGeoStructureModel> lstresponse = null;
        #endregion

        #region constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="GeoStructureController"/> class.
        /// </summary>
        /// <param name="geoStructureService">The GeoStructure service.</param>
        public MKAGeoStructureController(IMKAGeoStructureService mkageoStructureService)
        {
            iMKAGeoStructureService = mkageoStructureService;
        }
        #endregion

        #region api

       

        #region get GeoStructure list
        /// <summary>
        /// Add/Update GeoStructure.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetMKAGeoStructureList")]
        public async Task<IActionResult> GetGeoStructureList([FromBody] PageList model)
        {
            lstresponse = new Response<MKAGeoStructureModel>();

            if (model != null)
            {
                lstresponse = await iMKAGeoStructureService.GetMKAGeoStructureList(model);
            }
            else
            {
                lstresponse.Status = false;
                lstresponse.Message = ResponseMessage.Invalid_Requests_Model;
            }
            return Ok(lstresponse);
        }

        [HttpGet]
        [Route("GetMKAGeoList/{userId}")]
        public async Task<IActionResult> GetMKAGeoList([FromRoute] int userId)
        {
            Response response = new Response();
            response = await iMKAGeoStructureService.GetMKAGeoList(userId);
            return Ok(response);
        }
        #endregion

        #endregion
    }
}
