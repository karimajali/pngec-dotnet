using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PNGEC.API.Domain.MKAGeoStructure;
using PNGEC.API.Helper;

namespace PNGEC.API.Service.MKALocation
{
    public interface IMKALocationService
    {
        /// <summary>
        /// Adds/update the Location
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Response> AddUpdateMKALocation(MKALocationModel model);

        /// <summary>
        /// List the Location
        /// </summary>
        /// <returns></returns>
        Task<Response> ListMKALocation(int LocationId);

        /// <summary>
        /// Gets the location list.
        /// </summary>
        /// <returns></returns>
        Task<Response> GetMKALocationList();

        /// <summary>
        /// Lists the location by ward identifier.
        /// </summary>
        /// <param name="wardId">The ward identifier.</param>
        /// <returns></returns>
        Task<Response> ListMKALocationByWardId(int wardId);

        /// <summary>
        /// Locations the transfer.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Response> MKALocationTransfer(MKALocationModel model);
    }
}
