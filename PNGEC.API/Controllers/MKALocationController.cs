using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PNGEC.API.Domain.MKAGeoStructure;
using PNGEC.API.Helper;
using PNGEC.API.Service.GeoStructure;
using PNGEC.API.Service.Location;
using PNGEC.API.Service.MKALocation;

namespace PNGEC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MKALocationController : ControllerBase
    {
        #region variable declaration

        /// <summary>
        /// The i Location service
        /// </summary>
        private IMKALocationService iMKALocationService;

        /// <summary>
        /// The i geo structure service
        /// </summary>
        private IMKAGeoStructureService iMKAGeoStructureService;

        /// <summary>
        /// The response
        /// </summary>
        private Response response = null;

        /// <summary>
        /// The response
        /// </summary>
        private Response<MKALocationModel> lstresponse = null;
        #endregion

        #region constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationController"/> class.
        /// </summary>
        /// <param name="MKALocationService">The Location service.</param>
        public MKALocationController(IMKALocationService LocationService, IMKAGeoStructureService geoStructureService)
        {
            iMKALocationService = LocationService;
            iMKAGeoStructureService = geoStructureService;
        }
        #endregion

        #region api

        #region add update Location
        /// <summary>
        /// Add/Update Location.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddUpdateMKALocation")]
        public async Task<Response> AddUpdateMKALocation([FromBody] MKALocationModel model)
        {
            response = new Response();

            if (model != null)
            {
                response = await iMKALocationService.AddUpdateMKALocation(model);

                if (response.Status)
                {
                    await iMKAGeoStructureService.GetMKAGeoListAndCreateFile(model.ActionBy);
                }
            }
            else
            {
                response.Status = false;
                response.Message = ResponseMessage.Invalid_Requests_Model;
            }
            return response;
        }

        #endregion

        #region list Location
        /// <summary>
        /// List Location.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("ListMKALocation/{LocationId}")]
        public async Task<IActionResult> ListMKALocation(int LocationId)
        {
            var lstresponse = new Response();
            if (LocationId != null)
            {
            lstresponse = await iMKALocationService.ListMKALocation(LocationId);
            }

            return Ok(lstresponse);
        }

        #endregion

        #region Get Location List
        /// <summary>
        /// Gets the location list.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetMKALocationList")]
        public async Task<IActionResult> GetMKALocationList()
        {
            var lstresponse = new Response();
            lstresponse = await iMKALocationService.GetMKALocationList();

            return Ok(lstresponse);
        }
        #endregion

        #region ListLocationByWardId
        /// <summary>
        /// Lists the location by ward identifier.
        /// </summary>
        /// <param name="wardId">The ward identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("ListMKALocationByWardId/{wardId}")]
        public async Task<IActionResult> ListMKALocationByWardId(int wardId)
        {
            var lstresponse = new Response();
            lstresponse = await iMKALocationService.ListMKALocationByWardId(wardId);
            return Ok(lstresponse);
        }
        #endregion

        #region Location Transfer

        /// <summary>
        /// Locations the transfer.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("MKALocationTransfer")]
        public async Task<Response> MKALocationTransfer([FromBody] MKALocationModel model)
        {
            response = new Response();

            if (model != null)
            {
                response = await iMKALocationService.MKALocationTransfer(model);

                if (response.Status)
                {
                    await iMKAGeoStructureService.GetMKAGeoListAndCreateFile(model.ActionBy);
                }
            }
            else
            {
                response.Status = false;
                response.Message = ResponseMessage.Invalid_Requests_Model;
            }
            return response;
        }

        #endregion

        #endregion
    }
}
