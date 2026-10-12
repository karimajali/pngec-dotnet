using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PNGEC.API.Domain.MKAGeoStructure;
using PNGEC.API.Helper;

namespace PNGEC.API.Service.MKADistrict
{
    public interface IMKADistrictService
    {
        /// <summary>
        /// Adds/update the MKADistrict
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Response> AddUpdateMKADistrict(MKADistrictModel model);
        /// <summary>
        /// List the District
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Response> ListMKADistrict(int DistrictId);
        /// <summary>
        /// Gets the district list.
        /// </summary>
        /// <returns></returns>
        Task<Response> GetMKADistrictList();

    }
}
