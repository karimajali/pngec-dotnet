using PNGEC.API.Domain.Listing;
using PNGEC.API.Domain.Writ;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PNGEC.API.Repository.Writ
{
    public interface IWritRepository
    {
        /// <summary>
        /// Add/update the Writ
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<WritModel> AddUpdateWrit(WritModel model);


        /// <summary>
        /// Gets the Writ list.
        /// </summary>
        /// <returns></returns>      
        Task<List<WritModel>> GetWritList(PageList model);
       

    }
}
