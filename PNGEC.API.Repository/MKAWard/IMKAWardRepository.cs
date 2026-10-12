using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PNGEC.API.Domain.MKAGeoStructure;
using PNGEC.API.Helper;

namespace PNGEC.API.Repository.MKAWard
{
    public interface IMKAWardRepository
    {
        /// <summary>
        /// Add/update the Ward
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<MKAWardModel> AddUpdateMKAWard(MKAWardModel model);

        /// <summary>
        /// List the Ward
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Result<MKAWardModel>> ListMKAWard(int WardId);

        /// <summary>
        /// Gets the ward list.
        /// </summary>
        /// <returns></returns>
        Task<Result<MKAWardModel>> GetMKAWardList();

        /// <summary>
        /// Wards the transfer.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<int> MKAWardTransfer(MKAWardModel model);
    }
}
