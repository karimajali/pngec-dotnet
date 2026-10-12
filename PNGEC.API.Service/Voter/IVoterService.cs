using PNGEC.API.Domain.Mobile.Voter;
using PNGEC.API.Helper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PNGEC.API.Service.Voter
{
    public interface IVoterService
    {
        /// <summary>
        /// Gets the voter list mobile.
        /// </summary>
        /// <param name="voterReqModel">The voter req model.</param>
        /// <returns>return voter list</returns>
        Task<Response> GetVoterListMobile(VoterRequestModel voterReqModel);

        /// <summary>
        /// Downloads the voter list mobile.
        /// </summary>
        /// <param name="voterReqModel">The voter req model.</param>
        /// <returns></returns>
        Task<Response> DownloadVoterListMobile(VoterRequestModel voterReqModel);

        /// <summary>
        /// Gets the voter list by province mobile.
        /// </summary>
        /// <param name="voterReqModel">The voter req model.</param>
        /// <returns></returns>
        Task<Response> GetVoterListByProvinceMobile(VoterRequestModel voterReqModel);

        /// <summary>
        /// Gets the rejected voter list mobile.
        /// </summary>
        /// <param name="voterReqModel">The voter req model.</param>
        /// <returns>return rejected voter list</returns>
        Task<Response> GetRejectedVoterListMobile(VoterRequestModel voterReqModel);

        /// <summary>
        /// Gets the transfer in voter list.
        /// </summary>
        /// <param name="transferInVoterRequestModel">The transfer in voter request model.</param>
        /// <returns>return voter list</returns>
        Task<Response<VoterModel>> GetTransferInVoterList(TransferInVoterRequestModel transferInVoterRequestModel);

        /// <summary>
        /// Synchronizes the electors.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>return sync status</returns>
        Task<Response> SyncElectors(List<VoterModel> model);

        /// <summary>
        /// Synchronizes the electors images.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>return sync status</returns>
        Task<Response> SyncElectorsImages(VoterModel model);

        /// <summary>
        /// Synchronizes the electors and images.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Response> SyncElectorsAndImages(List<VoterModel> model);

        /// <summary>
        /// Imports the bio data details.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Response> ImportBioDataDetails(List<VoterModel> model);

        /// <summary>
        /// Gets the updated voter list.
        /// </summary>
        /// <param name="transferInVoterRequestModel">The transfer in voter request model.</param>
        /// <returns>return voter list</returns>
        Task<Response<VoterModel>> GetUpdatedVoterList(TransferInVoterRequestModel transferInVoterRequestModel);

        /// <summary>
        /// Transfers the voter by ro.
        /// </summary>
        /// <param name="lstvoter">The lstvoter.</param>
        /// <returns></returns>
        Task<Response> TransferVoterByRO(List<VoterModel> lstvoter);

        //Task<Response> GetFingerPrintByte();

        /// <summary>
        /// Gets the preliminary voter list mobile.
        /// </summary>
        /// <param name="voterReqModel">The voter req model.</param>
        /// <returns>return voter list</returns>
        Task<Response> GetPreliminaryVoterListMobile(VoterRequestModel voterReqModel);
    }
}
