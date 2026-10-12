using PNGEC.API.Domain.Listing;
using PNGEC.API.Domain.ScheduleLocSummary;
using PNGEC.API.Helper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PNGEC.API.Service.ScheduleLocSummary
{
    public interface IScheduleLocSummaryService
    {
        /// <summary>
        /// Gets the roll stat list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Response<ScheduleLocSummaryModel>> GetRollStatList(PageList model);

        /// <summary>
        /// Gets the mka roll stat list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Response<MKAScheduleLocSummaryModel>> GetMKARollStatList(PageList model);

        /// <summary>
        /// Gets the latest roll stat list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Response<ScheduleLocSummaryModel>> GetLatestRollStatList(PageList model);

        /// <summary>
        /// Schedules the notification.
        /// </summary>
        Task<Response> ScheduleNotification();

        /// <summary>
        /// Stagings the schedule notification.
        /// </summary>
        /// <returns></returns>
        Task<List<ScheduleLocSummaryModel>> StagingScheduleNotification();

        /// <summary>
        /// Preliminaries the schedule notification.
        /// </summary>
        /// <returns></returns>
        Task<List<ScheduleLocSummaryModel>> PreliminaryScheduleNotification();

        /// <summary>
        /// Finals the schedule notification.
        /// </summary>
        /// <returns></returns>
        Task<List<ScheduleLocSummaryModel>> FinalScheduleNotification();

        /// <summary>
        /// Rejecteds the schedule notification.
        /// </summary>
        /// <returns></returns>
        Task<List<ScheduleLocSummaryRejectedModel>> RejectedScheduleNotification();

        /// <summary>
        /// Rejecteds the by rd schedule notification.
        /// </summary>
        /// <returns></returns>
        Task<List<ScheduleLocSummaryRejectedByRDModel>> RejectedByRDScheduleNotification();
    }
}
