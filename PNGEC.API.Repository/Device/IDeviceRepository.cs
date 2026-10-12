using PNGEC.API.Domain.Device;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Helper;
using System.Threading.Tasks;

namespace PNGEC.API.Repository.Device
{
    public interface IDeviceRepository
    {
        /// <summary>
        /// Add/update the Device
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<DeviceModel> AddUpdateDevice(DeviceModel model);
        /// <summary>
        /// Gets the Device list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Result<DeviceModel>> GetDeviceList(PageList model);
        /// <summary>
        /// Updates the device status.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Result<DeviceModel>> UpdateDeviceStatus(CommonRequest model);
    }
}
