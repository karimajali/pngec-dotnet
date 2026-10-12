using PNGEC.API.Domain.Region;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PNGEC.API.Repository.Region
{
    public interface IRegionRepository
    {
        /// <summary>
        /// Add/update the Region
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<RegionModel> AddUpdateRegion(RegionModel model);

        /// <summary>
        /// List the Region
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<List<RegionModel>> ListRegion(int RegionId);

        /// <summary>
        /// Gets the region list.
        /// </summary>
        /// <returns></returns>
        Task<List<RegionModel>> GetRegionList();
    }
}
