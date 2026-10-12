using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Domain.Mobile.Voter;
using PNGEC.API.Domain.Report;
using PNGEC.API.Helper;
using PNGEC.API.Service.File;

namespace PNGEC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : Controller
    {
        #region variable declaration
        /// <summary>
        /// The i file service
        /// </summary>
        private IFileService iFileService;

        /// <summary>
        /// The response
        /// </summary>
        private Response response = null;

        /// <summary>
        /// The response
        /// </summary>
        private Response<VoterModel> lstresponse = null;
        /// <summary>
        /// The response
        /// </summary>
        private Response<MKAVoterModel> lstvoterresponse = null;

        /// <summary>
        /// The lstresponse ward roll report
        /// </summary>
        private Response<WardRollBookReortModel> lstresponseWardRollReport = null;

        /// <summary>
        /// The lstresponse ward roll report
        /// </summary>
        private Response<WardRollBookCsvReportModel> lstresponseWardRollCsvReport = null;

        /// <summary>
        /// The lstresponse mka ward clan section summary report
        /// </summary>
        private Response<GetMKAWardClanSectionSummaryModel> lstresponseMKAWardClanSectionSummaryReport = null;

        /// <summary>
        /// The lstresponse ward clan section summary report
        /// </summary>
        private Response<GetWardClanSectionSummaryModel> lstresponseWardClanSectionSummaryReport = null;


        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="FileController"/> class.
        /// </summary>
        /// <param name="fileService">The file service.</param>
        public FileController(IFileService fileService)
        {
            iFileService = fileService;
        }
        #endregion

        #region api

        #region get voter list
        /// <summary>
        /// Gets the voter list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetVoterList")]
        public async Task<IActionResult> GetVoterList([FromBody] PageList model)
        {
            lstresponse = new Response<VoterModel>();

            if (model != null)
            {
                lstresponse = await iFileService.GetVoterList(model);
            }
            else
            {
                lstresponse.Status = false;
                lstresponse.Message = ResponseMessage.Invalid_Requests_Model;
            }
            return Ok(lstresponse);
        }
        #endregion

        #region get mka voter list
        /// <summary>
        /// Gets the voter list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetMKAVoterList")]
        public async Task<IActionResult> GetMKAVoterList([FromBody] PageList model)
        {
            lstvoterresponse = new Response<MKAVoterModel>();

            if (model != null)
            {
                lstvoterresponse = await iFileService.GetMKAVoterList(model);
            }
            else
            {
                lstvoterresponse.Status = false;
                lstvoterresponse.Message = ResponseMessage.Invalid_Requests_Model;
            }
            return Ok(lstvoterresponse);
        }
        #endregion

        #region get export records list
        /// <summary>
        /// Gets the export records list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetExportRecordsList")]
        public async Task<IActionResult> GetExportRecordsList([FromBody] PageList model)
        {
            lstresponseWardRollReport = new Response<WardRollBookReortModel>();

            if (model != null)
            {
                lstresponseWardRollReport = await iFileService.GetExportRecordsList(model);
            }
            else
            {
                lstresponseWardRollReport.Status = false;
                lstresponseWardRollReport.Message = ResponseMessage.Invalid_Requests_Model;
            }
            return Ok(lstresponseWardRollReport);
        }
        #endregion

        #region get MKA export records list
        /// <summary>
        /// Gets the export records list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetMKAExportRecordsList")]
        public async Task<IActionResult> GetMKAExportRecordsList([FromBody] PageList model)
        {
            lstresponseWardRollReport = new Response<WardRollBookReortModel>();

            if (model != null)
            {
                lstresponseWardRollReport = await iFileService.GetExportRecordsList(model);
            }
            else
            {
                lstresponseWardRollReport.Status = false;
                lstresponseWardRollReport.Message = ResponseMessage.Invalid_Requests_Model;
            }
            return Ok(lstresponseWardRollReport);
        }
        #endregion

        #region get export csv records list
        /// <summary>
        /// Gets the export CSV records list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetExportCsvRecordsList")]
        public async Task<IActionResult> GetExportCsvRecordsList([FromBody] PageList model)
        {
            lstresponseWardRollCsvReport = new Response<WardRollBookCsvReportModel>();

            if (model != null)
            {
                lstresponseWardRollCsvReport = await iFileService.GetExportCsvRecordsList(model);
            }
            else
            {
                lstresponseWardRollCsvReport.Status = false;
                lstresponseWardRollCsvReport.Message = ResponseMessage.Invalid_Requests_Model;
            }
            return Ok(lstresponseWardRollCsvReport);
        }
        #endregion

        #region get MKA export csv records list        
        /// <summary>
        /// Gets the export CSV records list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetMKAExportCsvRecordsList")]
        public async Task<IActionResult> GetMKAExportCsvRecordsList([FromBody] PageList model)
        {
            lstresponseWardRollCsvReport = new Response<WardRollBookCsvReportModel>();

            if (model != null)
            {
                lstresponseWardRollCsvReport = await iFileService.GetExportCsvRecordsList(model);
            }
            else
            {
                lstresponseWardRollCsvReport.Status = false;
                lstresponseWardRollCsvReport.Message = ResponseMessage.Invalid_Requests_Model;
            }
            return Ok(lstresponseWardRollCsvReport);
        }     
        #endregion

        #region get duplicate voter list
        /// <summary>
        /// Gets the duplicate voter list.
        /// </summary>
        /// <param name="voterReqModel">The voter req model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetDuplicateVoterList")]
        public async Task<IActionResult> GetDuplicateVoterList(VoterRequestModel voterReqModel)
        {
            var lstresponse = new Response();
            if (voterReqModel.DatabaseName != null && !string.IsNullOrEmpty(voterReqModel.DatabaseName))
            {
                lstresponse = await iFileService.GetDuplicateVoterList(voterReqModel);
            }
            {
                lstresponse.Status = false;
                lstresponse.Message = ResponseMessage.Invalid_Requests_Model;
            }

            return Ok(lstresponse);
        }
        #endregion

        #region get tranferin voter with condition
        /// <summary>
        /// GetTranferinVoterWithCondition
        /// </summary>
        /// <param name="voterReqModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetTransferInVoterList")]
        public async Task<IActionResult> GetTranferinVoterWithCondition(PageList model)
        {
            lstresponseWardRollReport = new Response<WardRollBookReortModel>();

            if (model != null)
            {
                lstresponseWardRollReport = await iFileService.GetTranferinVoterWithCondition(model);
            }
            else
            {
                lstresponseWardRollReport.Status = false;
                lstresponseWardRollReport.Message = ResponseMessage.Invalid_Requests_Model;
            }
            return Ok(lstresponseWardRollReport);
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
                lstresponse = await iFileService.GetVoterListByProvinceMobile(voterReqModel);
            }
            else
            {
                lstresponse.Status = false;
                lstresponse.Message = ResponseMessage.Invalid_Requests_Model;
            }

            return Ok(lstresponse);
        }
        #endregion

        #region create voters file by province

        /// <summary>Creates the voters file by province.</summary>
        /// <param name="voterReqModel">The voter req model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateVotersFileByProvince")]
        public async Task<IActionResult> CreateVotersFileByProvince(VoterRequestModel voterReqModel)
        {
            var lstresponse = new Response();
            if (voterReqModel.ProvinceId != default(int))
            {
                lstresponse = await iFileService.DownloadVotersFileByProvince(voterReqModel);
            }
            else
            {
                lstresponse.Status = false;
                lstresponse.Message = ResponseMessage.Invalid_Requests_Model;
            }

            return Ok(lstresponse);
        }

        #endregion

        #region get mka ward clan section summary list
        /// <summary>
        /// Gets the mka ward clan section summary list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetMKAWardClanSectionSummaryList")]
        public async Task<IActionResult> GetMKAWardClanSectionSummaryList([FromBody] PageList model)
        {
            lstresponseMKAWardClanSectionSummaryReport = new Response<GetMKAWardClanSectionSummaryModel>();

            if (model != null)
            {
                lstresponseMKAWardClanSectionSummaryReport = await iFileService.GetMKAWardClanSectionSummaryList(model);
            }
            else
            {
                lstresponseMKAWardClanSectionSummaryReport.Status = false;
                lstresponseMKAWardClanSectionSummaryReport.Message = ResponseMessage.Invalid_Requests_Model;
            }
            return Ok(lstresponseMKAWardClanSectionSummaryReport);
        }
        #endregion

        #region get ward clan section summary list
        /// <summary>
        /// Gets the ward clan section summary list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetWardClanSectionSummaryList")]
        public async Task<IActionResult> GetWardClanSectionSummaryList([FromBody] PageList model)
        {
            lstresponseWardClanSectionSummaryReport = new Response<GetWardClanSectionSummaryModel>();

            if (model != null)
            {
                lstresponseWardClanSectionSummaryReport = await iFileService.GetWardClanSectionSummaryList(model);
            }
            else
            {
                lstresponseWardClanSectionSummaryReport.Status = false;
                lstresponseWardClanSectionSummaryReport.Message = ResponseMessage.Invalid_Requests_Model;
            }
            return Ok(lstresponseWardClanSectionSummaryReport);
        }
        #endregion

        #endregion
    }
}