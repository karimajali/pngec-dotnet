using PNGEC.API.Domain.Listing;
using PNGEC.API.Domain.Writ;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PNGEC.API.Repository.Writ
{
    public interface IWritRORepository
    {
        /// <summary>
        /// Gets the Writ RO Name list.
        /// </summary>
        /// <returns></returns>      
        Task<List<WritRO>> GetWritROList(PageList model);

        /// <summary>
        /// Add update Write RO
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<WritRO> AddUpdateWritRO(WritRO model);

    }
}
