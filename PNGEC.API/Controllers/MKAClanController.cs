using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Domain.MKAGeoStructure;
using PNGEC.API.Helper;
using PNGEC.API.Service.GeoStructure;
using PNGEC.API.Service.MKAClan;
using PNGEC.API.Service.MKADistrict;

namespace PNGEC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MKAClanController : ControllerBase
    {
        #region variable declaration

        /// <summary>
        /// The i MKADistrict service
        /// </summary>
        private IMKAClanService iMKAClanService;

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
        private Response<MKAClanModel> lstresponse = null;
        #endregion

        #region constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="MKAClanController"/> class.
        /// </summary>
        /// <param name="MKADistrictService">The MKADistrict service.</param>
        public MKAClanController(IMKAClanService MKAClanService, IMKAGeoStructureService MKAGeostructureService)
        {
            iMKAClanService = MKAClanService;
            iMKAGeoStructureService = MKAGeostructureService;
        }
        #endregion

        #region api

        #region add update MKAClan
        /// <summary>
        /// Add/Update Clan.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddUpdateMKAClan")]
        public async Task<Response> AddUpdateMKAClan([FromBody] MKAClanModel model)
        {
            response = new Response();

            if (model != null)
            {
                response = await iMKAClanService.AddUpdateMKAClan(model);

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

        #region list MKAClan
        /// <summary>
        /// List MKADistrict.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("ListMKAClan/{ClanId}")]
        public async Task<IActionResult> ListMKAClan(int ClanId)
        {
            var lstresponse = new Response();
            lstresponse = await iMKAClanService.ListMKAClan(ClanId);

            return Ok(lstresponse);
        }

        #endregion

        #region get clan list
        /// <summary>
        /// Gets the clan list.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetMKAClanList")]
        public async Task<IActionResult> GetMKAClanList([FromBody] PageList model)
        {
            var lstresponse = new Response();
            lstresponse = await iMKAClanService.GetMKAClanList(model);

            return Ok(lstresponse);
        }
        #endregion

        #endregion
    }
}
