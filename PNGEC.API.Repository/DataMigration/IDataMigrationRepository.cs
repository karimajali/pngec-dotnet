using PNGEC.API.Domain.DataMigration;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Domain.Mobile.Voter;
using PNGEC.API.Helper;
using System.Threading.Tasks;

namespace PNGEC.API.Repository.DataMigration
{
    public interface IDataMigrationRepository
    {
        /// <summary>
        /// Manages the voter status.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<bool> ManageVoterStatus(CommonRequest model, VoterModel voterModel);

        /// <summary>
        /// Manages the voter status transferred in out.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<MigrationTransferredInOut> ManageVoterStatusTransferredInOut(CommonRequest model, VoterModel voterModel);

        /// <summary>
        /// Gets the voter audit list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Result<VoterAuditModel>> GetVoterAuditList(PageList model);

        /// <summary>
        /// Updates the voter.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<int> UpdateVoter(VoterModel model);
    }
}