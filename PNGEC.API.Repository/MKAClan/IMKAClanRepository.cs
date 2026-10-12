using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Domain.MKAGeoStructure;
using PNGEC.API.Helper;

namespace PNGEC.API.Repository.MKAClan
{
    public interface IMKAClanRepository
    {
        /// <summary>
        /// Add/update the MKAClan
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<MKAClanModel> AddUpdateMKAClan(MKAClanModel model);
        /// <summary>
        /// List the MKAClan
        /// </summary>
        /// <returns></returns>
        Task<Result<MKAClanModel>> ListMKAClan(int ClanId);
        /// <summary>
        /// Gets the clan list.
        /// </summary>
        /// <returns></returns>
        Task<List<MKAClanModel>> GetMKAClanList(PageList model);
    }
}
