using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PNGEC.API.Domain.District;
using PNGEC.API.Helper;
using PNGEC.API.Service.District;
using PNGEC.API.Service.GeoStructure;

namespace PNGEC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistrictController : ControllerBase
    {
        #region variable declaration

        /// <summary>
        /// The i District service
        /// </summary>
        private IDistrictService iDistrictService;

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
        private Response<DistrictModel> lstresponse = null;
        #endregion

        #region constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="DistrictController"/> class.
        /// </summary>
        /// <param name="DistrictService">The District service.</param>
        public DistrictController(IDistrictService DistrictService, IGeoStructureService geoStructureService)
        {
            iDistrictService = DistrictService;
            iGeoStructureService = geoStructureService;
        }
        #endregion

        #region api

        #region add update District
        /// <summary>
        /// Add/Update District.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddUpdateDistrict")]
        public async Task<Response> AddUpdateDistrict([FromBody] DistrictModel model)
        {
            response = new Response();

            if (model != null)
            {
                response = await iDistrictService.AddUpdateDistrict(model);

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

        #region list District
        /// <summary>
        /// List District.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("ListDistrict/{DistrictId}")]
        public async Task<List<DistrictModel>> ListDistrict(int DistrictId)
        {
            var response = await iDistrictService.ListDistrict(DistrictId);

            return response;
        }

        #endregion

        #region get district list
        /// <summary>
        /// Gets the district list.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetDistrictList")]
        public async Task<List<DistrictModel>> GetDistrictList()
        {
            var response = await iDistrictService.GetDistrictList();

            return response;
        }
        #endregion

        #endregion
    }
}
