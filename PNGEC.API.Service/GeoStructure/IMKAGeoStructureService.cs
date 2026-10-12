using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PNGEC.API.Domain.GeoStructure;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Domain.MKAGeoStructure;
using PNGEC.API.Helper;

namespace PNGEC.API.Service.GeoStructure
{
    public interface IMKAGeoStructureService
    {
      
        /// <summary>
        /// Gets the GeoStructure list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Response<MKAGeoStructureModel>> GetMKAGeoStructureList(PageList model);

        /// <summary>
        /// Gets the geo list.
        /// </summary>
        /// <returns>return geo list</returns>
        Task<Response> GetMKAGeoList(int userId);
        /// <summary>
        /// Gets the geo list and create file.
        /// </summary>
        /// <returns></returns>
        Task GetMKAGeoListAndCreateFile(int actionBy);
    }
}
