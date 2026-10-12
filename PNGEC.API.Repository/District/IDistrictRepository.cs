using PNGEC.API.Domain.District;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PNGEC.API.Repository.District
{
    public interface IDistrictRepository
    {
        /// <summary>
        /// Add/update the District
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<DistrictModel> AddUpdateDistrict(DistrictModel model);

        /// <summary>
        /// List the District
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<List<DistrictModel>> ListDistrict(int DistrictId);

        /// <summary>
        /// Gets the district list.
        /// </summary>
        /// <returns></returns>
        Task<List<DistrictModel>> GetDistrictList();
    }
}
