using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PNGEC.API.Helper;

namespace PNGEC.API.Service.General
{
    public interface IMKAGeneralService
    {
        /// <summary>
        /// Gets the mkageo list mobile.
        /// </summary>
        /// <returns>return mkageo list</returns>
        Task<Response> GetMKAGeoListMobile();
        /// <summary>
        /// Gets the Clan list.
        /// </summary>
        /// <returns></returns>
        Task<Response> GetMKAClanList();
    }
}
