using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PNGEC.API.Domain.Setting;
using PNGEC.API.Helper;
using PNGEC.API.Service.Setting;

namespace PNGEC.API.Controllers
{
    /// <summary>
    /// setting controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class SettingController : ControllerBase
    {
        #region variable declaration

        private ISettingService iSettingService;

        #endregion

        #region constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingController"/> class.
        /// </summary>
        /// <param name="settingService">The setting service.</param>
        public SettingController(ISettingService settingService)
        {
            iSettingService = settingService;
        }
        #endregion

        #region api

        /// <summary>
        /// Updates the setting.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateSetting")]
        public async Task<Response> UpdateSetting([FromBody] SettingModel model)
        {
            var response = new Response();

            if (model != null)
            {
                response = await iSettingService.UpdateSetting(model);
            }
            else
            {
                response.Status = false;
                response.Message = ResponseMessage.Invalid_Requests_Model;
            }
            return response;
        }

        /// <summary>
        /// Gets the setting.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetSetting")]
        public async Task<IActionResult> GetSetting()
        {
            return Ok(await iSettingService.GetSetting());
        }

        #endregion
    }
}
