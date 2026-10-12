using System.Collections.Generic;
using System.Threading.Tasks;
using PNGEC.API.Domain.Ward;
using PNGEC.API.Helper;

namespace PNGEC.API.Service.Ward
{
    public interface IWardService
    {
        /// <summary>
        /// Adds/update the Ward
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Response> AddUpdateWard(WardModel model);

        /// <summary>
        /// List the Ward
        /// </summary>
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
        Task<Response> WardTransfer(WardModel model);
    }
}
