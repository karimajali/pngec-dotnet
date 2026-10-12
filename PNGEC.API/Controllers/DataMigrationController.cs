using Microsoft.AspNetCore.Mvc;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Domain.Mobile.Voter;
using PNGEC.API.Helper;
using PNGEC.API.Service.DataMigration;
using System.Threading.Tasks;

namespace PNGEC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataMigrationController : ControllerBase
    {
        #region variable declaration
        /// <summary>
        /// The i data migration service
        /// </summary>
        private IDataMigrationService iDataMigrationService;

        /// <summary>
        /// The response
        /// </summary>
        private Response response = null;

        /// <summary>
        /// The response
        /// </summary>
        private Response<VoterAuditModel> lstresponse = null;
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="DataMigrationController"/> class.
        /// </summary>
        /// <param name="iDataMigrationService">The i data migration service.</param>
        public DataMigrationController(IDataMigrationService iDataMigrationService)
        {
            this.iDataMigrationService = iDataMigrationService;
        }
        #endregion

        #region api

        #region manage voter status
        /// <summary>
        /// Manages the voter status.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("ManageVoterStatus")]
        public async Task<IActionResult> ManageVoterStatus([FromBody]CommonRequest model)
        {
            response = new Response();

            if (model != null)
            {
                response = await iDataMigrationService.ManageVoterStatus(model);
            }
            else
            {
                response.Status = false;
                response.Message = ResponseMessage.Invalid_Requests_Model;
            }
            return Ok(response);
        }
        #endregion

        #region get voter audit list
        [HttpPost]
        [Route("GetVoterAuditList")]
        public async Task<IActionResult> GetVoterAuditList([FromBody]PageList model)
        {
            lstresponse = new Response<VoterAuditModel>();

            if (model != null)
            {
                lstresponse = await iDataMigrationService.GetVoterAuditList(model);
            }
            else
            {
                lstresponse.Status = false;
                lstresponse.Message = ResponseMessage.Invalid_Requests_Model;
            }
            return Ok(lstresponse);
        }
        #endregion

        #region UpdateVoter
        /// <summary>
        /// Updates the voter.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateVoter")]
        public async Task<IActionResult> UpdateVoter([FromForm]VoterModel model)
        {
            response = new Response();

            if (model != null)
            {
                response = await iDataMigrationService.UpdateVoter(model);
            }
            else
            {
                response.Status = false;
                response.Message = ResponseMessage.Invalid_Requests_Model;
            }

            return Ok(response);
        }
        #endregion

        #endregion
    }
}
