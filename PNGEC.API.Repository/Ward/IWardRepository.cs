using System.Collections.Generic;
using System.Threading.Tasks;
using PNGEC.API.Domain.Ward;

namespace PNGEC.API.Repository.Ward
{
    public interface IWardRepository
    {
        /// <summary>
        /// Add/update the Ward
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<WardModel> AddUpdateWard(WardModel model);

        /// <summary>
        /// List the Ward
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<List<WardModel>> ListWard(int WardId);

        /// <summary>
        /// Gets the ward list.
        /// </summary>
        /// <returns></returns>
        Task<List<WardModel>> GetWardList();

        /// <summary>
        /// Wards the transfer.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<int> WardTransfer(WardModel model);
    }
}
