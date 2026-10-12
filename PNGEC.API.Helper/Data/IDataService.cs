using PNGEC.API.Domain.District;
using PNGEC.API.Domain.LLG;
using PNGEC.API.Domain.Location;
using PNGEC.API.Domain.Province;
using PNGEC.API.Domain.Region;
using PNGEC.API.Domain.Ward;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PNGEC.API.Helper.Data
{
    /// <summary>
    /// data service interface
    /// </summary>
    public interface IDataService
    {
        /// <summary>
        /// Gets the geo list asynchronous.
        /// </summary>
        /// <returns>return geo list</returns>
        Task<CommonResponse<Domain.GeoStructure.GeoListModel>> GetGeoListAsync(int userId);
        /// <summary>
        /// Gets the mka geo list asynchronous.
        /// </summary>
        /// <returns>return mka geo list</returns>
        Task<CommonResponse<Domain.MKAGeoStructure.MKAGeoListModel>> GetMKAGeoListAsync(int userId);

        /// <summary>
        /// Gets the region list asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<List<RegionModel>> GetRegionListAsync();

        /// <summary>
        /// Gets the province list asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<List<ProvinceModel>> GetProvinceListAsync();

        /// <summary>
        /// Gets the district list asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<List<DistrictModel>> GetDistrictListAsync();

        /// <summary>
        /// Gets the LLG list asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<List<LLGModel>> GetLLGListAsync();

        /// <summary>
        /// Gets the ward list asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<List<WardModel>> GetWardListAsync();

        /// <summary>
        /// Gets the location list asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<List<LocationModel>> GetLocationListAsync();
    }
}
