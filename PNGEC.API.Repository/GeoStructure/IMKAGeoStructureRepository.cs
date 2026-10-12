using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PNGEC.API.Domain.GeoStructure;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Domain.MKAGeoStructure;
using PNGEC.API.Helper;

namespace PNGEC.API.Repository.MKAGeoStructure
{
    public interface IMKAGeoStructureRepository
    {
       
        /// <summary>
        /// Gets the geo list.
        /// </summary>
        /// <returns>return Geo list</returns>
        Task<MKAGeoListModel> GetMKAGeoList(int userId);
        /// <summary>
        /// Gets the GeoStructure list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Result<MKAGeoStructureModel>> GetMKAGeoStructureList(PageList model);
    }
}
