using PNGEC.API.Domain.Listing;
using PNGEC.API.Domain.Writ;
using PNGEC.API.Helper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PNGEC.API.Service.Writ
{
    public interface IWritService
    {    
        /// <summary>
        /// Adds/update the Writ
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Response> AddUpdateWrit(WritModel model);

        /// <summary>
        /// Gets the Writ list.
        /// </summary>
        /// <returns></returns>      
        Task<Response> GetWritList(PageList model);
        
    }
}
