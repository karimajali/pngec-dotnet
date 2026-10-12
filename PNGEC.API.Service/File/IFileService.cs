using PNGEC.API.Domain.Listing;
using PNGEC.API.Domain.Mobile.Voter;
using PNGEC.API.Domain.Report;
using PNGEC.API.Helper;
using System.Threading.Tasks;

namespace PNGEC.API.Service.File
{
    public interface IFileService
    {
        /// <summary>
        /// Gets the voter list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Response<VoterModel>> GetVoterList(PageList model);
        /// <summary>
        /// Gets the mka voter list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Response<MKAVoterModel>> GetMKAVoterList(PageList model);

        /// <summary>
        /// Gets the export records list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Response<WardRollBookReortModel>> GetExportRecordsList(PageList model);

        /// <summary>
        /// Gets the export CSV records list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Response<WardRollBookCsvReportModel>> GetExportCsvRecordsList(PageList model);

        /// <summary>
        /// Gets the duplicate voter list.
        /// </summary>
        /// <param name="voterReqModel">The voter req model.</param>
        /// <returns></returns>
        Task<Response> GetDuplicateVoterList(VoterRequestModel voterReqModel);

        /// <summary>
        /// Gets the transfor in voter list.
        /// </summary>
        /// <param name="voterReqModel">The voter req model.</param>
        /// <returns></returns>
        Task<Response<WardRollBookReortModel>> GetTranferinVoterWithCondition(PageList model);

        /// <summary>
        /// Gets the voter list by province mobile.
        /// </summary>
        /// <param name="voterReqModel">The voter req model.</param>
        /// <returns></returns>
        Task<Response> GetVoterListByProvinceMobile(VoterRequestModel voterReqModel);

        /// <summary>
        /// Downloads the voters file by province.
        /// </summary>
        /// <param name="voterReqModel">The voter req model.</param>
        /// <returns></returns>
        Task<Response> DownloadVotersFileByProvince(VoterRequestModel voterReqModel);

        /// <summary>
        /// Gets the mka ward clan section summary list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Response<GetMKAWardClanSectionSummaryModel>> GetMKAWardClanSectionSummaryList(PageList model);

        /// <summary>
        /// Gets the ward clan section summary list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Response<GetWardClanSectionSummaryModel>> GetWardClanSectionSummaryList(PageList model);

    }
}
