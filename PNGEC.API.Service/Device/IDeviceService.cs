using PNGEC.API.Domain.Device;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Helper;
using System.Threading.Tasks;

namespace PNGEC.API.Service.Device
{
    public interface IDeviceService
    {
        /// <summary>
        /// Adds/update the Device
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Response> AddUpdateDevice(DeviceModel model);

        /// <summary>
        /// Gets the Device list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Response<DeviceModel>> GetDeviceList(PageList model);

        /// <summary>
        /// Updates the device status.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Response<DeviceModel>> UpdateDeviceStatus(CommonRequest model);
    }
}
