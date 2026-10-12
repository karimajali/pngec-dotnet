using Microsoft.AspNetCore.Mvc;
using PNGEC.API.Domain.Clan;
using PNGEC.API.Helper;
using PNGEC.API.Service.GeoStructure;
using PNGEC.API.Service.Clan;
using System.Collections.Generic;
using System.Threading.Tasks;
using PNGEC.API.Service.Location;


namespace PNGEC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClanController : ControllerBase
    {

        #region variable declaration

        /// <summary>
        /// The i Clan service
        /// </summary>
        private IClanService iClanService;

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
        private Response<ClanModel> lstresponse = null;
        #endregion


        #region constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ClanController"/> class.
        /// </summary>
        /// <param name="ClanService">The Clan service.</param>
        public ClanController(IClanService ClanService, IGeoStructureService geoStructureService)
        {
            iClanService = ClanService;
            iGeoStructureService = geoStructureService;
        }
        #endregion

        #region api

        #region delete Clan
        /// <summary>
        /// delete Clan.
        /// </summary>
        /// <param >Clan ID</param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteClan")]
        public async Task<Response> DeleteClan([FromBody] int ClanID)
        {
            var response = await iClanService.DeleteClan(ClanID);
            return response;
        }

        #endregion

        #region add update Clan
        /// <summary>
        /// Add/Update Clan.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddUpdateClan")]
        public async Task<Response> AddUpdateClan([FromBody] ClanModel model)
        {
            response = new Response();

            if (model != null)
            {
                response = await iClanService.AddUpdateClan(model);

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

        #region list Clan
        /// <summary>
        /// List Clan.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("ListClan/{ClanId}")]
        public async Task<List<ClanModel>> ListClan(int ClanId)
        {
            var response = await iClanService.Listclan(ClanId);

            return response;
        }

        #endregion

        #region Get Clan List
        /// <summary>
        /// Gets the Clan list.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetClanList")]
        public async Task<List<ClanModel>> GetClanList()
        {
            var response = await iClanService.GetClanList();

            return response;
        }
        #endregion

        #region ListClanByWardId
        /// <summary>
        /// Lists the Clan by ward identifier.
        /// </summary>
        /// <param name="wardId">The ward identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("ListClanByWardId/{wardId}")]
        public async Task<List<ClanModel>> ListClanByWardId(int wardId)
        {
            var response = await iClanService.ListclanByWardId(wardId);
            return response;
        }
        #endregion

        #region Clan Transfer

        /// <summary>
        /// Clans the transfer.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("ClanTransfer")]
        public async Task<Response> ClanTransfer([FromBody] ClanModel model)
        {
            response = new Response();

            if (model != null)
            {
                response = await iClanService.clanTransfer(model);

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
