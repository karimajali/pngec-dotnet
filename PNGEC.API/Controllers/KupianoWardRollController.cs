using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PNGEC.API.Domain.KupianoWardRoll;
using PNGEC.API.Helper;
using PNGEC.API.Service.KupianoWardRoll;

namespace PNGEC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KupianoWardRollController : ControllerBase
    {
        #region variable declaration

        /// <summary>
        /// The i kupiano ward roll service
        /// </summary>
        private IKupianoWardRollService iKupianoWardRollService;

        /// <summary>
        /// The response
        /// </summary>
        private Response<KupianoWardRollModel> lstresponse = null;
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="KupianoWardRollController"/> class.
        /// </summary>
        public KupianoWardRollController(IKupianoWardRollService kupianoWardRollService)
        {
            iKupianoWardRollService = kupianoWardRollService;
        }
        #endregion

        #region api

        #region get kupiano ward roll list
        /// <summary>
        /// Gets the kupiano ward roll list.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetKupianoWardRollList")]
        public async Task<IActionResult> GetKupianoWardRollList()
        {
            lstresponse = new Response<KupianoWardRollModel>();
            lstresponse = await iKupianoWardRollService.GetKupianoWardRollList();
            return Ok(lstresponse);
        }
        #endregion

        #endregion
    }
}