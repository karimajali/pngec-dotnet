using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PNGEC.API.Domain.Region;
using PNGEC.API.Helper;
using PNGEC.API.Service.GeoStructure;
using PNGEC.API.Service.Region;

namespace PNGEC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        #region variable declaration

        /// <summary>
        /// The i Region service
        /// </summary>
        private IRegionService iRegionService;

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
        private Response<RegionModel> lstresponse = null;
        #endregion

        #region constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="RegionController"/> class.
        /// </summary>
        /// <param name="RegionService">The Region service.</param>
        public RegionController(IRegionService RegionService, IGeoStructureService geoStructureService)
        {
            iRegionService = RegionService;
            iGeoStructureService = geoStructureService;
        }
        #endregion

        #region api

        #region add update Region
        /// <summary>
        /// Add/Update Region.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddUpdateRegion")]
        public async Task<Response> AddUpdateRegion([FromBody] RegionModel model)
        {
            response = new Response();

            if (model != null)
            {
                response = await iRegionService.AddUpdateRegion(model);

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

        #region list Region
        /// <summary>
        /// List Region.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("ListRegion/{RegionId}")]
        public async Task<List<RegionModel>> ListRegion(int RegionId)
        {
            var response = await iRegionService.ListRegion(RegionId);

            return response;
        }

        #endregion

        #region get region list
        /// <summary>
        /// Gets the region list.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetRegionList")]
        public async Task<List<RegionModel>> GetRegionList()
        {
            var response = await iRegionService.GetRegionList();

            return response;
        }
        #endregion

        #endregion
    }
}
