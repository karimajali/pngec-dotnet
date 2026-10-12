using PNGEC.API.Domain.Listing;
using PNGEC.API.Domain.Mobile.Voter;
using PNGEC.API.Domain.Report;
using PNGEC.API.Helper;
using System.Threading.Tasks;

namespace PNGEC.API.Repository.File
{
    public interface IFileRepository
    {
        /// <summary>
        /// Gets the voter list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Result<VoterModel>> GetVoterList(PageList model);
        /// <summary>
        /// Gets the mka voter list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Result<MKAVoterModel>> GetMKAVoterList(PageList model);

        /// <summary>
        /// Gets the export records list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Result<WardRollBookReortModel>> GetExportRecordsList(PageList model);

        /// <summary>
        /// Gets the export CSV records list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Result<WardRollBookCsvReportModel>> GetExportCsvRecordsList(PageList model);

        /// <summary>
        /// Gets the duplicate voter list.
        /// </summary>
        /// <param name="voterReqModel">The voter req model.</param>
        /// <returns></returns>
        Task<Result<WardRollBookReortModel>> GetDuplicateVoterList(VoterRequestModel voterReqModel);

        /// <summary>
        /// Gets the transfor in voter list.
        /// </summary>
        /// <param name="voterReqModel">The voter req model.</param>
        /// <returns></returns>
        Task<Result<WardRollBookReortModel>> GetTranferinVoterWithCondition(PageList model);

        /// <summary>
        /// Gets the voter list by province mobile.
        /// </summary>
        /// <param name="voterReqModel">The voter req model.</param>
        /// <returns></returns>
        Task<Result<VoterModel>> GetVoterListByProvinceMobile(VoterRequestModel voterReqModel);

        /// <summary>
        /// Downloads the voters file by province.
        /// </summary>
        /// <param name="voterReqModel">The voter req model.</param>
        /// <returns></returns>
        Task<Result<VoterModelOffline>> DownloadVotersFileByProvince(VoterRequestModel voterReqModel);

        /// <summary>
        /// Gets the mka ward clan section summary list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Result<GetMKAWardClanSectionSummaryModel>> GetMKAWardClanSectionSummaryList(PageList model);

        /// <summary>
        /// Gets the ward clan section summary list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Result<GetWardClanSectionSummaryModel>> GetWardClanSectionSummaryList(PageList model);
    }
}
