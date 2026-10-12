using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PNGEC.API.Domain.MKAGeoStructure;
using PNGEC.API.Helper;
using PNGEC.API.Service.GeoStructure;
using PNGEC.API.Service.MKAWard;
using PNGEC.API.Service.Ward;

namespace PNGEC.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class MKAWardController : ControllerBase
    {
        #region variable declaration

        /// <summary>
        /// The i Ward service
        /// </summary>
        private IMKAWardService iMKAWardService;

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
        private Response<MKAWardModel> lstresponse = null;
        #endregion

        #region constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="MKAWardController"/> class.
        /// </summary>
        /// <param name="WardService">The Ward service.</param>
        public MKAWardController(IMKAWardService WardService, IMKAGeoStructureService geoStructureService)
        {
            iMKAWardService = WardService;
            iMKAGeoStructureService = geoStructureService;
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
        [Route("AddUpdateMKAWard")]
        public async Task<Response> AddUpdateMKAWard([FromBody] MKAWardModel model)
        {
            response = new Response();

            if (model != null)
            {
                response = await iMKAWardService.AddUpdateMKAWard(model);

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

        #region list Ward
        /// <summary>
        /// List Ward.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("ListMKAWard/{WardId}")]
        public async Task<IActionResult> ListMKAWard(int WardId)
        {
            var lstresponse = new Response();
            if (WardId != null)
            {
                lstresponse = await iMKAWardService.ListMKAWard(WardId);
            }
            return Ok(lstresponse);
        }

        #endregion

        #region get ward list
        /// <summary>
        /// Gets the ward list.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetMKAWardList")]
        public async Task<IActionResult> GetMKAWardList()
        {
            var lstresponse = new Response();
            lstresponse = await iMKAWardService.GetMKAWardList();

            return Ok(lstresponse);
        }
        #endregion

        #region ward Transfer


        /// <summary>
        /// Wards the transfer.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("MKAWardTransfer")]
        public async Task<Response> MKAWardTransfer([FromBody] MKAWardModel model)
        {
            response = new Response();

            if (model != null)
            {
                response = await iMKAWardService.MKAWardTransfer(model);

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
