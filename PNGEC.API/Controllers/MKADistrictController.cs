using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PNGEC.API.Domain.MKAGeoStructure;
using PNGEC.API.Helper;
using PNGEC.API.Service.GeoStructure;
using PNGEC.API.Service.MKADistrict;


namespace PNGEC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MKADistrictController : ControllerBase
    {
       
        #region variable declaration

        /// <summary>
        /// The i MKADistrict service
        /// </summary>
        private IMKADistrictService iMKADistrictService;

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
        private Response<MKADistrictModel> lstresponse = null;
        #endregion

        #region constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="MKADistrictController"/> class.
        /// </summary>
        /// <param name="MKADistrictService">The MKADistrict service.</param>
        public MKADistrictController(IMKADistrictService MKADistrictService, IMKAGeoStructureService MKAGeostructureService)
        {
            iMKADistrictService = MKADistrictService;
            iMKAGeoStructureService = MKAGeostructureService;
        }
        #endregion

        #region api
        #region add update MKADistrict
        /// <summary>
        /// Add/Update District.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddUpdateMKADistrict")]
        public async Task<Response> AddUpdateMKADistrict([FromBody] MKADistrictModel model)
        {
            response = new Response();

            if (model != null)
            {
                response = await iMKADistrictService.AddUpdateMKADistrict(model);

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
        #region list MKADistrict
        /// <summary>
        /// List MKADistrict.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("ListMKADistrict/{DistrictId}")]
        public async Task<IActionResult> ListMKADistrict(int DistrictId)
        {
            var lstresponse = new Response();
            if (DistrictId != null)
            {
                lstresponse = await iMKADistrictService.ListMKADistrict(DistrictId);
            }
            return Ok(lstresponse);
        }

        #endregion
        #region get district list
        /// <summary>
        /// Gets the district list.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetMKADistrictList")]
        public async Task<IActionResult> GetMKADistrictList()
        {
            var lstresponse = new Response();

            lstresponse = await iMKADistrictService.GetMKADistrictList();

            return Ok(lstresponse);
        }
        #endregion
        #endregion
    }
}
