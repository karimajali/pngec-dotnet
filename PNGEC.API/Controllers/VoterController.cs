using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PNGEC.API.Domain.Mobile.Voter;
using PNGEC.API.Helper;
using PNGEC.API.Service.Voter;

namespace PNGEC.API.Controllers
{
    [Route("api/mobile/[controller]")]
    [ApiController]
    public class VoterController : Controller
    {
        #region variable declaration
        /// <summary>
        /// The voter service
        /// </summary>
        private IVoterService iVoterService;
        #endregion

        #region constructor        
        /// <summary>
        /// Initializes a new instance of the <see cref="VoterController"/> class.
        /// </summary>
        /// <param name="voterService">The voter service.</param>
        public VoterController(IVoterService voterService)
        {
            iVoterService = voterService;
        }
        #endregion

        #region api

        #region get voter list mobile
        /// <summary>
        /// Gets the water.
        /// </summary>
        /// <param name="userId">The userId identifier.</param>
        /// <returns>return voter list</returns>
        [HttpPost]
        [Route("GetVoterList")]
        public async Task<IActionResult> GetVoterListMobile(VoterRequestModel voterReqModel)
        {
            var lstresponse = new Response();
            if (voterReqModel.UserId != default(int))
            {
                lstresponse = await iVoterService.GetVoterListMobile(voterReqModel);
            }
            else
            {
                lstresponse.Status = false;
                lstresponse.Message = ResponseMessage.Invalid_Requests_Model;
            }

            return Ok(lstresponse);
        }

        /// <summary>
        /// Downloads the voter list mobile.
        /// </summary>
        /// <param name="voterReqModel">The voter req model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("DownloadVoterList")]
        public async Task<IActionResult> DownloadVoterListMobile(VoterRequestModel voterReqModel)
        {
            var lstresponse = new Response();
            if (voterReqModel.UserId != default(int))
            {
                lstresponse = await iVoterService.DownloadVoterListMobile(voterReqModel);
            }
            else
            {
                lstresponse.Status = false;
                lstresponse.Message = ResponseMessage.Invalid_Requests_Model;
            }

            return Ok(lstresponse);
        }
        #endregion

        #region get voter list by province mobile
        /// <summary>
        /// Gets the voter list by province.
        /// </summary>
        /// <param name="voterReqModel">The voter req model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetVoterListByProvince")]
        public async Task<IActionResult> GetVoterListByProvinceMobile(VoterRequestModel voterReqModel)
        {
            var lstresponse = new Response();
            if (voterReqModel.ProvinceId != default(int))
            {
                lstresponse = await iVoterService.GetVoterListByProvinceMobile(voterReqModel);
            }
            else
            {
                lstresponse.Status = false;
                lstresponse.Message = ResponseMessage.Invalid_Requests_Model;
            }

            return Ok(lstresponse);
        }
        #endregion

        #region get rejected voter list mobile
        /// <summary>
        /// Gets the rejected voter list mobile.
        /// </summary>
        /// <param name="voterReqModel">The voter req model.</param>
        /// <returns>return rejected voter list</returns>
        [HttpPost]
        [Route("GetRejectedVoterList")]
        public async Task<IActionResult> GetRejectedVoterListMobile(VoterRequestModel voterReqModel)
        {
            var lstresponse = new Response();
            if (voterReqModel.UserId != default(int))
            {
                lstresponse = await iVoterService.GetRejectedVoterListMobile(voterReqModel);
            }
            else
            {
                lstresponse.Status = false;
                lstresponse.Message = ResponseMessage.Invalid_Requests_Model;
            }

            return Ok(lstresponse);
        }
        #endregion

        #region get transfer in voter list
        /// <summary>
        /// Gets the transfer in voter list.
        /// </summary>
        /// <param name="transferInVoterRequestModel">The transfer in voter request model.</param>
        /// <returns>return voter list</returns>
        [HttpPost]
        [Route("GetTransferInVoterList")]
        public async Task<IActionResult> GetTransferInVoterList(TransferInVoterRequestModel transferInVoterRequestModel)
        {
            var lstresponse = new Response<VoterModel>();
            if (transferInVoterRequestModel != null)
            {
                lstresponse = await iVoterService.GetTransferInVoterList(transferInVoterRequestModel);
            }
            else
            {
                lstresponse.Status = false;
                lstresponse.Message = ResponseMessage.Invalid_Requests_Model;
            }

            return Ok(lstresponse);
        }
        #endregion

        #region sync electors
        /// <summary>
        /// Synchronizes the electors.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("SyncElectors")]
        public async Task<IActionResult> SyncElectors([FromBody] List<VoterModel> model)
        {
            var lstresponse = new Response();
            if (model != null)
            {
                lstresponse = await iVoterService.SyncElectors(model);
            }
            else
            {
                lstresponse.Status = false;
                lstresponse.Message = ResponseMessage.Invalid_Requests_Model;
            }

            return Ok(lstresponse);
        }
        #endregion

        #region sync electors images
        /// <summary>
        /// Synchronizes the electors images.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("SyncElectorsImages")]
        public async Task<IActionResult> SyncElectorsImages([FromForm] VoterModel model)
        {
            var lstresponse = new Response();
            if (model != null)
            {
                lstresponse = await iVoterService.SyncElectorsImages(model);
            }
            else
            {
                lstresponse.Status = false;
                lstresponse.Message = ResponseMessage.Invalid_Requests_Model;
            }

            return Ok(lstresponse);
        }
        #endregion

        #region sync electors and images
        /// <summary>
        /// Synchronizes the electors and images.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("SyncElectorsAndImages")]
        public async Task<IActionResult> SyncElectorsAndImages([FromForm] VoterModel model)
        {
            var lstresponse = new Response();
            if (model != null)
            {
                List<VoterModel> voterModels = new List<VoterModel>();
                voterModels.Add(model);
                lstresponse = await iVoterService.SyncElectorsAndImages(voterModels);
            }
            else
            {
                lstresponse.Status = false;
                lstresponse.Message = ResponseMessage.Invalid_Requests_Model;
            }

            return Ok(lstresponse);
        }
        #endregion

        #region import biodata details
        /// <summary>
        /// Imports the bio data details.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("ImportBioDataDetails")]
        public async Task<IActionResult> ImportBioDataDetails([FromBody] List<VoterModel> model)
        {
            var lstresponse = new Response();
            if (model != null)
            {
                lstresponse = await iVoterService.ImportBioDataDetails(model);
            }
            else
            {
                lstresponse.Status = false;
                lstresponse.Message = ResponseMessage.Invalid_Requests_Model;
            }

            return Ok(lstresponse);
        }
        #endregion

        #region get updated voter list
        /// <summary>
        /// Gets the updated in voter list.
        /// </summary>
        /// <param name="transferInVoterRequestModel">The transfer in voter request model.</param>
        /// <returns>return voter list</returns>
        [HttpPost]
        [Route("GetUpdatedVoterList")]
        public async Task<IActionResult> GetUpdatedVoterList(TransferInVoterRequestModel transferInVoterRequestModel)
        {
            var lstresponse = new Response<VoterModel>();
            if (transferInVoterRequestModel != null)
            {
                lstresponse = await iVoterService.GetUpdatedVoterList(transferInVoterRequestModel);
            }
            else
            {
                lstresponse.Status = false;
                lstresponse.Message = ResponseMessage.Invalid_Requests_Model;
            }

            return Ok(lstresponse);
        }
        #endregion

        #region TransferVoterByRO
        /// <summary>
        /// Transfers the voter by ro.
        /// </summary>
        /// <param name="lstvoter">The lstvoter.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("TransferVoterByRO")]
        public async Task<IActionResult> TransferVoterByRO(List<VoterModel> lstvoter)
        {
            var response = new Response();
            if (lstvoter != null)
            {
                response = await iVoterService.TransferVoterByRO(lstvoter);
            }
            else
            {
                response.Status = false;
                response.Message = ResponseMessage.Invalid_Requests_Model;
            }

            return Ok(response);
        }
        #endregion

        #region Unused code for finger print
        /// <summary>
        /// Gets the water.
        /// </summary>
        /// <param name="userId">The userId identifier.</param>
        /// <returns>return voter list</returns>
        //[HttpPost]
        //[Route("GetFingerPrintByte")]
        //public async Task<IActionResult> GetFingerPrintByte()
        //{
        //    var lstresponse = new Response();

        //    lstresponse = await iVoterService.GetFingerPrintByte();
        //    return Ok(lstresponse);
        //}
        #endregion

        #region get voter list mobile
        /// <summary>
        /// Gets the water.
        /// </summary>
        /// <param name="userId">The userId identifier.</param>
        /// <returns>return voter list</returns>
        [HttpPost]
        [Route("GetPreliminaryVoterList")]
        public async Task<IActionResult> GetPreliminaryVoterListMobile(VoterRequestModel voterReqModel)
        {
            return Ok(await iVoterService.GetPreliminaryVoterListMobile(voterReqModel));
        }
        #endregion
        #endregion
    }
}