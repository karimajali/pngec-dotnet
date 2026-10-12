using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PNGEC.API.Domain.MKAGeoStructure;
using PNGEC.API.Helper;
using PNGEC.API.Service.GeoStructure;
using PNGEC.API.Service.LLG;
using PNGEC.API.Service.MKALLG;

namespace PNGEC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MKALLGController : ControllerBase
    {
        #region variable declaration

        /// <summary>
        /// The i LLG service
        /// </summary>
        private IMKALLGService iMKALLGService;

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
        private Response<MKALLGModel> lstresponse = null;
        #endregion
        #region constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="MKALLGController"/> class.
        /// </summary>
        /// <param name="MKALLGService">The LLG service.</param>
        public MKALLGController(IMKALLGService MKALLGService, IMKAGeoStructureService mkageoStructureService)
        {
            iMKALLGService = MKALLGService;
            iMKAGeoStructureService = mkageoStructureService;
        }
        #endregion
        #region api

        #region add update LLG
        /// <summary>
        /// Add/Update LLG.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddUpdateMKALLG")]
        public async Task<Response> AddUpdateMKALLG([FromBody] MKALLGModel model)
        {
            response = new Response();

            if (model != null)
            {
                response = await iMKALLGService.AddUpdateMKALLG(model);

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

        #region list LLG
        /// <summary>
        /// List LLG.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("ListMKALLG/{LLGId}")]
        public async Task<IActionResult> ListLLG(int LLGId)
        {
            var lstresponse = new Response();
            if (LLGId != null)
            {
                lstresponse = await iMKALLGService.ListMKALLG(LLGId);
            }

            return Ok(lstresponse);
        }

        #endregion

        #region get llg list
        [HttpGet]
        [Route("GetMKALLGList")]
        public async Task<IActionResult> GetLLGList()
        {
            var lstresponse = new Response();
             lstresponse = await iMKALLGService.GetMKALLGList();

            return Ok(lstresponse);
        }
        #endregion

        #region llg Transfer

        /// <summary>
        /// LLGs the transfer.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("MKALLGTransfer")]
        public async Task<Response> LLGTransfer([FromBody] MKALLGModel model)
        {
            response = new Response();

            if (model != null)
            {
                response = await iMKALLGService.MKALLGTransfer(model);

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
