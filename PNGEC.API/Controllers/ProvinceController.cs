using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PNGEC.API.Domain.Province;
using PNGEC.API.Helper;
using PNGEC.API.Service.GeoStructure;
using PNGEC.API.Service.Province;

namespace PNGEC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvinceController : ControllerBase
    {
        #region variable declaration

        /// <summary>
        /// The i Province service
        /// </summary>
        private IProvinceService iProvinceService;

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
        private Response<ProvinceModel> lstresponse = null;
        #endregion

        #region constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ProvinceController"/> class.
        /// </summary>
        /// <param name="ProvinceService">The Province service.</param>
        public ProvinceController(IProvinceService ProvinceService, IGeoStructureService geoStructureService)
        {
            iProvinceService = ProvinceService;
            iGeoStructureService = geoStructureService;
        }
        #endregion

        #region api

        #region add update Province
        /// <summary>
        /// Add/Update Province.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddUpdateProvince")]
        public async Task<Response> AddUpdateProvince([FromBody] ProvinceModel model)
        {
            response = new Response();

            if (model != null)
            {
                response = await iProvinceService.AddUpdateProvince(model);

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

        #region list Province
        /// <summary>
        /// List Province.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("ListProvince/{ProvinceId}")]
        public async Task<List<ProvinceModel>> ListProvince(int ProvinceId)
        {
            var response = await iProvinceService.ListProvince(ProvinceId);

            return response;
        }

        #endregion

        #region get province list
        /// <summary>
        /// Gets the province list.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetProvinceList")]
        public async Task<List<ProvinceModel>> GetProvinceList()
        {
            var response = await iProvinceService.GetProvinceList();

            return response;
        }
        #endregion

        #endregion
    }
}
