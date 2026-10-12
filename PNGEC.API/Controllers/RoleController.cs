using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PNGEC.API.Domain.Role;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Helper;
using PNGEC.API.Service.Role;

namespace PNGEC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        #region variable declaration

        /// <summary>
        /// The i Role service
        /// </summary>
        private IRoleService iRoleService;

        /// <summary>
        /// The response
        /// </summary>
        private Response response = null;

        /// <summary>
        /// The response
        /// </summary>
        private Response<RoleModel> lstresponse = null;
        #endregion

        #region constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleController"/> class.
        /// </summary>
        /// <param name="RoleService">The Role service.</param>
        public RoleController(IRoleService RoleService)
        {
            iRoleService = RoleService;
        }
        #endregion

        #region api

        #region add update Role
        /// <summary>
        /// Add/Update Role.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddUpdateRole")]
        public async Task<Response> AddUpdateRole([FromBody]RoleModel model)
        {
            response = new Response();

            if (model != null)
            {
                response = await iRoleService.AddUpdateRole(model);
            }
            else
            {
                response.Status = false;
                response.Message = ResponseMessage.Invalid_Requests_Model;
            }
            return response;
        }

        #endregion

        #region get Role list
        [HttpGet]
        [Route("GetRoleList")]
        public async Task<IActionResult> GetRoleList([FromBody] PageList model)
        {
            lstresponse = new Response<RoleModel>();

            if (model != null)
            {
                lstresponse = await iRoleService.GetRoleList(model);
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
