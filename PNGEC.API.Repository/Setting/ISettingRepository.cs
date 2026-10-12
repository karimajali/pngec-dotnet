using System.Threading.Tasks;
using PNGEC.API.Domain.Setting;

namespace PNGEC.API.Repository.Setting
{
    /// <summary>
    /// setting interface
    /// </summary>
    public interface ISettingRepository
    {
        /// <summary>
        /// Updates the setting.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<SettingModel> UpdateSetting(SettingModel model);

        
        /// <summary>
        /// Updates the setting mka geo version.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<SettingModel> UpdateSettingMKAGeoVersion(SettingModel model); 
        /// <summary>
        /// Updates the setting geo version.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<SettingModel> UpdateSettingGeoVersion(SettingModel model);

        /// <summary>
        /// Gets the setting.
        /// </summary>
        /// <returns></returns>
        Task<SettingModel> GetSetting();
    }
}
