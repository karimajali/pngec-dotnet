using PNGEC.API.Domain.Listing;
using PNGEC.API.Domain.Writ;
using PNGEC.API.Helper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PNGEC.API.Service.Writ
{
    public interface IWritROService
    {   
        /// <summary>
        /// Gets the Writ RO Name list.
        /// </summary>
        /// <returns></returns>      
        Task<Response> GetWritROList(PageList model);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Response> AddUpdateWritRO(WritRO model);

    }
}
