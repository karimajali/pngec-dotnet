using PNGEC.API.Domain.GeoStructure;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Helper;
using System.Threading.Tasks;

namespace PNGEC.API.Repository.GeoStructure
{
    public interface IGeoStructureRepository
    {
        /// <summary>
        /// Add/update the GeoStructure
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<GeoStructureModel> AddUpdateGeoStructure(GeoStructureModel model);

        /// <summary>
        /// Gets the GeoStructure list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Result<GeoStructureModel>> GetGeoStructureList(PageList model);

        /// <summary>
        /// Gets the geo list.
        /// </summary>
        /// <returns>return Geo list</returns>
        Task<GeoListModel> GetGeoList(int userId);
    }
}
