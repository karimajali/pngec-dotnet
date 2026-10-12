using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PNGEC.API.Domain.LLG;
using PNGEC.API.Helper;
using PNGEC.API.Service.GeoStructure;
using PNGEC.API.Service.LLG;

namespace PNGEC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LLGController : ControllerBase
    {
        #region variable declaration

        /// <summary>
        /// The i LLG service
        /// </summary>
        private ILLGService iLLGService;

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
        private Response<LLGModel> lstresponse = null;
        #endregion

        #region constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="LLGController"/> class.
        /// </summary>
        /// <param name="LLGService">The LLG service.</param>
        public LLGController(ILLGService LLGService, IGeoStructureService geoStructureService)
        {
            iLLGService = LLGService;
            iGeoStructureService = geoStructureService;
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
        [Route("AddUpdateLLG")]
        public async Task<Response> AddUpdateLLG([FromBody] LLGModel model)
        {
            response = new Response();

            if (model != null)
            {
                response = await iLLGService.AddUpdateLLG(model);

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

        #region list LLG
        /// <summary>
        /// List LLG.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("ListLLG/{LLGId}")]
        public async Task<List<LLGModel>> ListLLG(int LLGId)
        {
            var response = await iLLGService.ListLLG(LLGId);

            return response;
        }

        #endregion

        #region get llg list
        [HttpGet]
        [Route("GetLLGList")]
        public async Task<List<LLGModel>> GetLLGList()
        {
            var response = await iLLGService.GetLLGList();

            return response;
        }
        #endregion

        #region llg Transfer

        /// <summary>
        /// LLGs the transfer.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("LLGTransfer")]
        public async Task<Response> LLGTransfer([FromBody] LLGModel model)
        {
            response = new Response();

            if (model != null)
            {
                response = await iLLGService.LLGTransfer(model);

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
