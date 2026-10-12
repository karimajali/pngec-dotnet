using PNGEC.API.Domain.District;
using PNGEC.API.Helper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PNGEC.API.Service.District
{
    public interface IDistrictService
    {
        /// <summary>
        /// Adds/update the District
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Response> AddUpdateDistrict(DistrictModel model);

        /// <summary>
        /// List the District
        /// </summary>
        /// <returns></returns>
        Task<List<DistrictModel>> ListDistrict(int DistrictId);

        /// <summary>
        /// Gets the district list.
        /// </summary>
        /// <returns></returns>
        Task<List<DistrictModel>> GetDistrictList();
    }
}
