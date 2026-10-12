using PNGEC.API.Domain.Province;
using PNGEC.API.Helper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PNGEC.API.Service.Province
{
    public interface IProvinceService
    {
        /// <summary>
        /// Adds/update the Province
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Response> AddUpdateProvince(ProvinceModel model);

        /// <summary>
        /// List the Province
        /// </summary>
        /// <returns></returns>
        Task<List<ProvinceModel>> ListProvince(int ProvinceId);

        /// <summary>
        /// Gets the province list.
        /// </summary>
        /// <returns></returns>
        Task<List<ProvinceModel>> GetProvinceList();
    }
}
