using System.Threading.Tasks;
using PNGEC.API.Domain.Setting;
using PNGEC.API.Helper;

namespace PNGEC.API.Service.Setting
{
    /// <summary>
    /// setting service interface
    /// </summary>
    public interface ISettingService
    {
        /// <summary>
        /// Gets the setting.
        /// </summary>
        /// <returns></returns>
        Task<Response> GetSetting();

        /// <summary>
        /// Updates the setting.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Response> UpdateSetting(SettingModel model);
    }
}
