using Microsoft.AspNetCore.Mvc;
using PNGEC.API.Domain.AugmentedRollStat;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Helper;
using PNGEC.API.Service.AugmentedRollStat;
using System.Threading.Tasks;

namespace PNGEC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AugmentedRollStatController : ControllerBase
    {
        #region variable declaration

        /// <summary>
        /// The i augmented roll stat service
        /// </summary>
        private IAugmentedRollStatService iAugmentedRollStatService;

        /// <summary>
        /// The response
        /// </summary>
        private Response<AugmentedRollStatModel> lstresponse = null;

        private Response response = null;
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="AugmentedRollStatController"/> class.
        /// </summary>
        /// <param name="augmentedRollStatService">The augmented roll stat service.</param>
        public AugmentedRollStatController(IAugmentedRollStatService augmentedRollStatService)
        {
            iAugmentedRollStatService = augmentedRollStatService;
        }
        #endregion

        #region api

        #region get roll stat list
        /// <summary>
        /// Gets the augmented roll stat list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetAugmentedRollStatList")]
        public async Task<IActionResult> GetAugmentedRollStatList([FromBody]PageList model)
        {
            lstresponse = new Response<AugmentedRollStatModel>();

            if (model != null)
            {
                lstresponse = await iAugmentedRollStatService.GetAugmentedRollStatList(model);
            }
            else
            {
                lstresponse.Status = false;
                lstresponse.Message = ResponseMessage.Invalid_Requests_Model;
            }
            return Ok(lstresponse);
        }
        #endregion

        #region add update augmented roll stat
        /// <summary>
        /// Adds the update augmented roll stat.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddUpdateAugmentedRollStat")]
        public async Task<Response> AddUpdateAugmentedRollStat([FromBody] AugmentedRollStatModel model)
        {
            response = new Response();

            if (model != null)
            {
                response = await iAugmentedRollStatService.AddUpdateAugmentedRollStat(model);
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