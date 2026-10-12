using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PNGEC.API.Domain.Mobile.General;
namespace PNGEC.API.Repository.General
{
    public interface IMKAGeneralRepository
    {
        /// <summary>
        /// Gets the mkageo list.
        /// </summary>
        /// <returns>return MKAGeo list</returns>
        Task<List<MKAGeoListModel>> GetMKAGeoListMobile();
        /// <summary>
        /// Gets the clan list.
        /// </summary>
        /// <returns></returns>
        Task<List<MKAClanModel>> GetMKAClanList();
    }
}
