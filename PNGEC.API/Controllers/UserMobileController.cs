using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PNGEC.API.Domain.Mobile;
using PNGEC.API.Domain.Mobile.User;
using PNGEC.API.Domain.User;
using PNGEC.API.Helper;
using PNGEC.API.Service.User;

namespace PNGEC.API.Controllers
{
    [Route("api/mobile/[controller]")]
    [ApiController]
    public class UserMobileController : Controller
    {
        #region variable        
        /// <summary>
        /// The i user service
        /// </summary>
        private IUserService iUserService;
        #endregion

        #region constructor
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="userService"></param>
        public UserMobileController(IUserService userService)
        {
            iUserService = userService;
        }
        #endregion

        #region api

        #region MobileUserLogin
        /// <summary>
        /// Users the login.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("UserLogin")]
        public async Task<IActionResult> UserLogin([FromBody]LoginUserRequestModel model)
        {
            var lstresponse = new Response();
            if (model != null)
            {
                lstresponse = await iUserService.MobileUserLogin(model);
            }
            else
            {
                lstresponse.Status = false;
                lstresponse.Message = ResponseMessage.Invalid_Requests_Model;
            }

            return Ok(lstresponse);
        }
        #endregion

        #endregion

    }
}