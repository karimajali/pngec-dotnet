using PNGEC.API.Domain.Listing;
using PNGEC.API.Domain.Mobile.Voter;
using PNGEC.API.Helper;
using System.Threading.Tasks;

namespace PNGEC.API.Service.DataMigration
{
    public interface IDataMigrationService
    {
        /// <summary>
        /// Manages the voter status.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Response> ManageVoterStatus(CommonRequest model);

        /// <summary>
        /// Gets the voter audit list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Response<VoterAuditModel>> GetVoterAuditList(PageList model);

        /// <summary>
        /// Updates the voter.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Response> UpdateVoter(VoterModel model);
    }
}
