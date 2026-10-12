using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PNGEC.API.Domain.Ward;
using PNGEC.API.Helper;
using PNGEC.API.Service.GeoStructure;
using PNGEC.API.Service.Ward;

namespace PNGEC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WardController : ControllerBase
    {
        #region variable declaration

        /// <summary>
        /// The i Ward service
        /// </summary>
        private IWardService iWardService;

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
        private Response<WardModel> lstresponse = null;
        #endregion

        #region constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="WardController"/> class.
        /// </summary>
        /// <param name="WardService">The Ward service.</param>
        public WardController(IWardService WardService, IGeoStructureService geoStructureService)
        {
            iWardService = WardService;
            iGeoStructureService = geoStructureService;
        }
        #endregion

        #region api

        #region add update Ward
        /// <summary>
        /// Add/Update Ward.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddUpdateWard")]
        public async Task<Response> AddUpdateWard([FromBody]WardModel model)
        {
            response = new Response();

            if (model != null)
            {
                response = await iWardService.AddUpdateWard(model);

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

        #region list Ward
        /// <summary>
        /// List Ward.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("ListWard/{WardId}")]
        public async Task<List<WardModel>> ListWard(int WardId)
        {
            var response = await iWardService.ListWard(WardId);

            return response;
        }

        #endregion

        #region get ward list
        /// <summary>
        /// Gets the ward list.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetWardList")]
        public async Task<List<WardModel>> GetWardList()
        {
            var response = await iWardService.GetWardList();

            return response;
        }
        #endregion

        #region ward Transfer


        /// <summary>
        /// Wards the transfer.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("WardTransfer")]
        public async Task<Response> WardTransfer([FromBody] WardModel model)
        {
            response = new Response();

            if (model != null)
            {
                response = await iWardService.WardTransfer(model);

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
