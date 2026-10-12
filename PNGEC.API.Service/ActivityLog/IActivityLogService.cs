using PNGEC.API.Domain.Device;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Domain.User;
using PNGEC.API.Helper;
using System.Threading.Tasks;

namespace PNGEC.API.Service.ActivityLog
{
    public interface IActivityLogService
    {
        /// <summary>
        /// Adds the ActivityLog
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Response> AddActivityLog(ActivityLogModel model);

        /// <summary>
        /// Gets the ActivityLog list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Response<ActivityLogModel>> GetActivityLogList(PageList model);

        
    }
}
