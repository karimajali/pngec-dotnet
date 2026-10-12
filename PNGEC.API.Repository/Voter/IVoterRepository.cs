using PNGEC.API.Domain.Mobile.Voter;
using PNGEC.API.Helper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PNGEC.API.Repository.Voter
{
    public interface IVoterRepository
    {

        /// <summary>
        /// Gets the voter list mobile.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>return voter list</returns>
        Task<Result<VoterModel>> GetVoterListMobile(VoterRequestModel voterReqModel);

        /// <summary>
        /// Downloads the voter list mobile.
        /// </summary>
        /// <param name="voterReqModel">The voter req model.</param>
        /// <returns></returns>
        Task<Result<VoterModel>> DownloadVoterListMobile(VoterRequestModel voterReqModel);

        /// <summary>
        /// Gets the voter list by province mobile.
        /// </summary>
        /// <param name="voterReqModel">The voter req model.</param>
        /// <returns></returns>
        Task<Result<VoterModel>> GetVoterListByProvinceMobile(VoterRequestModel voterReqModel);

        /// <summary>
        /// Gets the rejected voter list mobile.
        /// </summary>
        /// <param name="voterReqModel">The voter req model.</param>
        /// <returns>return rejected voter list</returns>
        Task<Result<VoterModel>> GetRejectedVoterListMobile(VoterRequestModel voterReqModel);

        /// <summary>
        /// Gets the transfer in voter list.
        /// </summary>
        /// <param name="transferInVoterRequestModel">The transfer in voter request model.</param>
        /// <returns>return voter list</returns>
        Task<Result<VoterModel>> GetTransferInVoterList(TransferInVoterRequestModel transferInVoterRequestModel);

        /// <summary>
        /// Synchronizes the electors.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>return elector id</returns>
        Task<long> SyncElectors(VoterModel model);

        /// <summary>
        /// Synchronizes the electors images.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>return elector id</returns>
        Task<long> SyncElectorsImages(VoterModel model);
        /// <summary>
        /// Imports the bio data details.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        Task<long> ImportBioDataDetails(VoterModel item);

        /// <summary>
        /// Gets the updated voter list.
        /// </summary>
        /// <param name="transferInVoterRequestModel">The transfer in voter request model.</param>
        /// <returns>return voter list</returns>
        Task<Result<VoterModel>> GetUpdatedVoterList(TransferInVoterRequestModel transferInVoterRequestModel);

        /// <summary>
        /// Transfers the voter by ro.
        /// </summary>
        /// <param name="lstvoter">The lstvoter.</param>
        /// <returns></returns>
        Task<int> TransferVoterByRO(VoterModel lstvoter);

        //Task<Result<FingerPrintModel>> GetFingerPrintByte();

        /// <summary>
        /// Gets the preliminary voter list mobile.
        /// </summary>
        /// <param name="voterReqModel">The voter req model.</param>
        /// <returns>return voter list</returns>
        Task<Result<VoterModel>> GetPreliminaryVoterListMobile(VoterRequestModel voterReqModel);
    }
}
