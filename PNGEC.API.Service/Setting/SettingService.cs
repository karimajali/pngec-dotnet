using System;
using System.Threading.Tasks;
using PNGEC.API.Domain.Setting;
using PNGEC.API.Helper;
using PNGEC.API.Repository.Setting;

namespace PNGEC.API.Service.Setting
{
    /// <summary>
    /// setting service
    /// </summary>
    /// <seealso cref="PNGEC.API.Service.Setting.ISettingService" />
    public class SettingService : ISettingService
    {
        #region variable declaration

        private readonly ISettingRepository iSettingRepository;

        #endregion

        #region constructor 

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingService"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public SettingService(string connection)
        {
            iSettingRepository = new SettingRepository(connection);
        }
        #endregion

        #region Service

        /// <summary>
        /// Updates the setting.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Response> UpdateSetting(SettingModel model)
        {
            Response response = new Response();

            try
            {                
                response.Data = await iSettingRepository.UpdateSetting(model);
                response.Status = true;
                response.Message = string.Format(ResponseMessage.RecordUpdateSuccess, "Setting");
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("SettingService", "UpdateSetting", ex);
            }

            return response;
        }
       
        public async Task<Response> GetSetting()
        {
            Response response = new Response();

            try
            {
                response.Data = await iSettingRepository.GetSetting();
                response.Status = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("SettingService", "GetSetting", ex);
            }

            return response;
        }
        #endregion
    }
}
