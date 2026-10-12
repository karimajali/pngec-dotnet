using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Domain.MKAGeoStructure;
using PNGEC.API.Helper;

namespace PNGEC.API.Service.MKAClan
{
    public interface IMKAClanService
    {
        /// <summary>
        /// Adds/update the MKAClan
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Response> AddUpdateMKAClan(MKAClanModel model);
        /// <summary>
        /// List the Clan
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Response> ListMKAClan(int ClanId);
        /// <summary>
        /// Gets the Clan list.
        /// </summary>
        /// <returns></returns>
        Task<Response> GetMKAClanList(PageList model);

    }
}
