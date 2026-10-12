using PNGEC.API.Domain.Region;
using PNGEC.API.Helper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PNGEC.API.Service.Region
{
    public interface IRegionService
    {
        /// <summary>
        /// Adds/update the Region
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Response> AddUpdateRegion(RegionModel model);

        /// <summary>
        /// List the Region
        /// </summary>
        /// <returns></returns>
        Task<List<RegionModel>> ListRegion(int RegionId);

        /// <summary>
        /// Gets the region list.
        /// </summary>
        /// <returns></returns>
        Task<List<RegionModel>> GetRegionList();
    }
}
