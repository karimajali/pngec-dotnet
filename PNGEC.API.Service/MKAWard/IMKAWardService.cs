using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PNGEC.API.Domain.MKAGeoStructure;
using PNGEC.API.Helper;

namespace PNGEC.API.Service.MKAWard
{
    public interface IMKAWardService
    {
        /// <summary>
        /// Adds/update the Ward
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Response> AddUpdateMKAWard(MKAWardModel model);

        /// <summary>
        /// List the Ward
        /// </summary>
        /// <returns></returns>
        Task<Response> ListMKAWard(int WardId);

        /// <summary>
        /// Gets the ward list.
        /// </summary>
        /// <returns></returns>
        Task<Response> GetMKAWardList();

        /// <summary>
        /// Wards the transfer.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Response> MKAWardTransfer(MKAWardModel model);
    }
}
