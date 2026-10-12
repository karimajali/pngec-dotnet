using Microsoft.AspNetCore.Mvc;
using PNGEC.API.Domain.Device;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Helper;
using PNGEC.API.Service.Device;
using System.Threading.Tasks;

namespace PNGEC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        #region variable declaration

        /// <summary>
        /// The i device service
        /// </summary>
        private IDeviceService iDeviceService;

        /// <summary>
        /// The response
        /// </summary>
        private Response response = null;

        /// <summary>
        /// The response
        /// </summary>
        private Response<DeviceModel> lstresponse = null;
        #endregion

        #region constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceController"/> class.
        /// </summary>
        /// <param name="deviceService">The device service.</param>
        public DeviceController(IDeviceService deviceService)
        {
            iDeviceService = deviceService;
        }
        #endregion

        #region api

        #region add update device
        /// <summary>
        /// Add/Update Device.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddUpdateDevice")]
        public async Task<Response> AddUpdateDevice([FromBody] DeviceModel model)
        {
            response = new Response();

            if (model != null)
            {
                response = await iDeviceService.AddUpdateDevice(model);
            }
            else
            {
                response.Status = false;
                response.Message = ResponseMessage.Invalid_Requests_Model;
            }
            return response;
        }

        #endregion

        #region get device list
        /// <summary>
        /// Get Device List
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetDeviceList")]
        public async Task<IActionResult> GetDeviceList([FromBody] PageList model)
        {
            lstresponse = new Response<DeviceModel>();

            if (model != null)
            {
                lstresponse = await iDeviceService.GetDeviceList(model);
            }
            else
            {
                lstresponse.Status = false;
                lstresponse.Message = ResponseMessage.Invalid_Requests_Model;
            }
            return Ok(lstresponse);
        }
        #endregion

        #region update Device Status
        /// <summary>
        /// Update Device Status
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateDeviceStatus")]
        public async Task<IActionResult> UpdateDeviceStatus([FromBody]CommonRequest model)
        {
            lstresponse = new Response<DeviceModel>();

            if (model != null)
            {
                lstresponse = await iDeviceService.UpdateDeviceStatus(model);
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
