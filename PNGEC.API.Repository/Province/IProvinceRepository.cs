using PNGEC.API.Domain.Province;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PNGEC.API.Repository.Province
{
    public interface IProvinceRepository
    {
        /// <summary>
        /// Add/update the Province
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<ProvinceModel> AddUpdateProvince(ProvinceModel model);

        /// <summary>
        /// List the Province
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<List<ProvinceModel>> ListProvince(int ProvinceId);

        /// <summary>
        /// Gets the province list.
        /// </summary>
        /// <returns></returns>
        Task<List<ProvinceModel>> GetProvinceList();
    }
}
