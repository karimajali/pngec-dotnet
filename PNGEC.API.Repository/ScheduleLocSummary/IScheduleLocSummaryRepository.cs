using PNGEC.API.Domain.Listing;
using PNGEC.API.Domain.ScheduleLocSummary;
using PNGEC.API.Helper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace PNGEC.API.Repository.ScheduleLocSummary
{
    public interface IScheduleLocSummaryRepository
    {
        /// <summary>
        /// Gets the roll stat list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Result<ScheduleLocSummaryModel>> GetRollStatList(PageList model);

        /// <summary>
        /// Gets the mka roll stat list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Result<MKAScheduleLocSummaryModel>> GetMKARollStatList(PageList model);

        /// <summary>
        /// Gets the latest roll stat list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Result<ScheduleLocSummaryModel>> GetLatestRollStatList(PageList model);

        /// <summary>
        /// Schedules the notification staging.
        /// </summary>
        /// <returns></returns>
        Task<List<ScheduleLocSummaryModel>> ScheduleNotificationStaging();

        /// <summary>
        /// Schedules the notification preliminary.
        /// </summary>
        /// <returns></returns>
        Task<List<ScheduleLocSummaryModel>> ScheduleNotificationPreliminary();

        /// <summary>
        /// Schedules the notification final.
        /// </summary>
        /// <returns></returns>
        Task<List<ScheduleLocSummaryModel>> ScheduleNotificationFinal();

        /// <summary>
        /// Schedules the notification rejected.
        /// </summary>
        /// <returns></returns>
        Task<List<ScheduleLocSummaryRejectedModel>> ScheduleNotificationRejected();

        /// <summary>
        /// Schedules the notification rejected by rd.
        /// </summary>
        /// <returns></returns>
        Task<List<ScheduleLocSummaryRejectedByRDModel>> ScheduleNotificationRejectedByRD();

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
