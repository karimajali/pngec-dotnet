using PNGEC.API.Domain.Device;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Domain.User;
using PNGEC.API.Helper;
using System.Threading.Tasks;

namespace PNGEC.API.Repository.ActivityLog
{
    public interface IActivityLogRepository
    {
        /// <summary>
        /// Add the ActivityLog
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<ActivityLogModel> AddActivityLog(ActivityLogModel model);
        /// <summary>
        /// Gets the ActivityLog list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Result<ActivityLogModel>> GetActivityLogList(PageList model);
       
    }
}
