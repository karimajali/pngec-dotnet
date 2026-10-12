using PNGEC.API.Domain.Location;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PNGEC.API.Repository.Location
{
    public interface ILocationRepository
    {
        /// <summary>
        /// Add/update the Location
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<LocationModel> AddUpdateLocation(LocationModel model);

        /// <summary>
        /// List the Location
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<List<LocationModel>> ListLocation(int LocationId);

        /// <summary>
        /// Gets the location list.
        /// </summary>
        /// <returns></returns>
        Task<List<LocationModel>> GetLocationList();

        /// <summary>
        /// Lists the location by ward identifier.
        /// </summary>
        /// <param name="wardId">The ward identifier.</param>
        /// <returns></returns>
        Task<List<LocationModel>> ListLocationByWardId(int wardId);

        /// <summary>
        /// Locations the transfer.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<int> LocationTransfer(LocationModel model);
    }
}
