using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PNGEC.API.Domain.Location;
using PNGEC.API.Helper;
using PNGEC.API.Service.GeoStructure;
using PNGEC.API.Service.Location;

namespace PNGEC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        #region variable declaration

        /// <summary>
        /// The i Location service
        /// </summary>
        private ILocationService iLocationService;

        /// <summary>
        /// The i geo structure service
        /// </summary>
        private IGeoStructureService iGeoStructureService;

        /// <summary>
        /// The response
        /// </summary>
        private Response response = null;

        /// <summary>
        /// The response
        /// </summary>
        private Response<LocationModel> lstresponse = null;
        #endregion

        #region constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationController"/> class.
        /// </summary>
        /// <param name="LocationService">The Location service.</param>
        public LocationController(ILocationService LocationService, IGeoStructureService geoStructureService)
        {
            iLocationService = LocationService;
            iGeoStructureService = geoStructureService;
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
        [Route("AddUpdateLocation")]
        public async Task<Response> AddUpdateLocation([FromBody] LocationModel model)
        {
            response = new Response();

            if (model != null)
            {
                response = await iLocationService.AddUpdateLocation(model);

                if (response.Status)
                {
                    await iGeoStructureService.GetGeoListAndCreateFile(model.ActionBy);
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
        [Route("ListLocation/{LocationId}")]
        public async Task<List<LocationModel>> ListLocation(int LocationId)
        {
            var response = await iLocationService.ListLocation(LocationId);

            return response;
        }

        #endregion

        #region Get Location List
        /// <summary>
        /// Gets the location list.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetLocationList")]
        public async Task<List<LocationModel>> GetLocationList()
        {
            var response = await iLocationService.GetLocationList();

            return response;
        }
        #endregion

        #region ListLocationByWardId
        /// <summary>
        /// Lists the location by ward identifier.
        /// </summary>
        /// <param name="wardId">The ward identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("ListLocationByWardId/{wardId}")]
        public async Task<List<LocationModel>> ListLocationByWardId(int wardId)
        {
            var response = await iLocationService.ListLocationByWardId(wardId);
            return response;
        }
        #endregion

        #region Location Transfer

        /// <summary>
        /// Locations the transfer.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("LocationTransfer")]
        public async Task<Response> LocationTransfer([FromBody] LocationModel model)
        {
            response = new Response();

            if (model != null)
            {
                response = await iLocationService.LocationTransfer(model);

                if (response.Status)
                {
                    await iGeoStructureService.GetGeoListAndCreateFile(model.ActionBy);
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
