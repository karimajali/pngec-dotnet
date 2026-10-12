using PNGEC.API.Domain.GeoStructure;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Helper;
using System.Threading.Tasks;

namespace PNGEC.API.Service.GeoStructure
{
    public interface IGeoStructureService
    {
        /// <summary>
        /// Adds/update the GeoStructure
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Response> AddUpdateGeoStructure(GeoStructureModel model);

        /// <summary>
        /// Gets the GeoStructure list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Response<GeoStructureModel>> GetGeoStructureList(PageList model);

        /// <summary>
        /// Gets the geo list.
        /// </summary>
        /// <returns>return geo list</returns>
        Task<Response> GetGeoList(int userId);

        /// <summary>
        /// Gets the geo list and create file.
        /// </summary>
        /// <returns></returns>
        Task GetGeoListAndCreateFile(int actionBy);
    }
}
