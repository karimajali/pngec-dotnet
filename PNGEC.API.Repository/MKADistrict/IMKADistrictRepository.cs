using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PNGEC.API.Domain.MKAGeoStructure;
using PNGEC.API.Helper;

namespace PNGEC.API.Repository.MKADistrict
{
    public interface IMKADistrictRepository
    {
        /// <summary>
        /// Add/update the MKADistrict
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<MKADistrictModel> AddUpdateMKADistrict(MKADistrictModel model);
        /// <summary>
        /// List the MKADistrict
        /// </summary>
        /// <returns></returns>
        Task<Result<MKADistrictModel>> ListMKADistrict(int DistrictId);
        /// <summary>
        /// Gets the district list.
        /// </summary>
        /// <returns></returns>
        Task<Result<MKADistrictModel>> GetMKADistrictList();
    }
}
