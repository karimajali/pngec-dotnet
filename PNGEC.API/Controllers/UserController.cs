using Microsoft.AspNetCore.Mvc;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Domain.User;
using PNGEC.API.Helper;
using PNGEC.API.Service.User;
using System.Threading.Tasks;

namespace PNGEC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        #region variable declaration
        /// <summary>
        /// The i user service
        /// </summary>
        private IUserService iUserService;

        /// <summary>
        /// The response
        /// </summary>
        private Response response = null;

        /// <summary>
        /// The response
        /// </summary>
        private Response<UserModel> lstresponse = null;
        #endregion

        #region constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="userService">The user service.</param>
        public UserController(IUserService userService)
        {
            iUserService = userService;
        }
        #endregion

        #region api

        #region add update user
        /// <summary>
        /// Adds the update user.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddUpdateUser")]
        public async Task<IActionResult> AddUpdateUser([FromForm]UserModel model)
        {
            response = new Response();

            if (model != null)
            {
                response = await iUserService.AddUpdateUser(model);
            }
            else
            {
                response.Status = false;
                response.Message = ResponseMessage.Invalid_Requests_Model;
            }
            return Ok(response);
        }
        #endregion

        #region get user list
        /// <summary>
        /// Gets the user list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetUserList")]
        public async Task<IActionResult> GetUserList([FromBody]PageList model)
        {
            lstresponse = new Response<UserModel>();

            if (model != null)
            {
                lstresponse = await iUserService.GetUserList(model);
            }
            else
            {
                lstresponse.Status = false;
                lstresponse.Message = ResponseMessage.Invalid_Requests_Model;
            }
            return Ok(lstresponse);
        }
        #endregion

        #region update user status
        /// <summary>
        /// Updates the user status.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateUserStatus")]
        public async Task<IActionResult> UpdateUserStatus([FromBody]CommonRequest model)
        {
            lstresponse = new Response<UserModel>();

            if (model != null)
            {
                lstresponse = await iUserService.UpdateUserStatus(model);
            }
            else
            {
                lstresponse.Status = false;
                lstresponse.Message = ResponseMessage.Invalid_Requests_Model;
            }
            return Ok(lstresponse);
        }
        #endregion

        #region user login
        /// <summary>
        /// Users the login.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("UserLogin")]
        public async Task<IActionResult> UserLogin([FromBody]CommonRequest model)
        {
            lstresponse = new Response<UserModel>();

            if (model != null)
            {
                lstresponse = await iUserService.UserLogin(model);
            }
            else
            {
                lstresponse.Status = false;
                lstresponse.Message = ResponseMessage.Invalid_Requests_Model;
            }
            return Ok(lstresponse);
        }
        #endregion

        #region forgot password
        /// <summary>
        /// Forgots the password.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody]CommonRequest model)
        {
            response = new Response();

            if (model != null)
            {
                response = await iUserService.ForgotPassword(model);
            }
            else
            {
                response.Status = false;
                response.Message = ResponseMessage.Invalid_Requests_Model;
            }
            return Ok(response);
        }
        #endregion

        #region reset password
        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody]UserModel model)
        {
            response = new Response();

            if (model != null)
            {
                response = await iUserService.ResetPassword(model);
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