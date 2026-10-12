using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PNGEC.API.Domain.MKAGeoStructure;
using PNGEC.API.Helper;

namespace PNGEC.API.Repository.MKALocation
{
    public interface IMKALocationRepository
    {
        /// <summary>
        /// Add/update the Location
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<MKALocationModel> AddUpdateMKALocation(MKALocationModel model);

        /// <summary>
        /// List the Location
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Result<MKALocationModel>> ListMKALocation(int LocationId);

        /// <summary>
        /// Gets the location list.
        /// </summary>
        /// <returns></returns>
        Task<Result<MKALocationModel>> GetMKALocationList();

        /// <summary>
        /// Lists the location by ward identifier.
        /// </summary>
        /// <param name="wardId">The ward identifier.</param>
        /// <returns></returns>
        Task<Result<MKALocationModel>> ListMKALocationByWardId(int wardId);

        /// <summary>
        /// Locations the transfer.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<int> MKALocationTransfer(MKALocationModel model);
    }
}
