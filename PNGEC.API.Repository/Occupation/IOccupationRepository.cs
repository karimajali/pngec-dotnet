using System.Collections.Generic;
using System.Threading.Tasks;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Domain.Mobile.General;

namespace PNGEC.API.Repository.Occupation
{
    public interface IOccupationRepository
    {
        /// <summary>
        /// Gets the occupation list.
        /// </summary>
        /// <returns>return list of occupation</returns>
        Task<List<OccupationModel>> GetOccupationList(PageList model);

        /// <summary>
        /// Adds the update occupation.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>return added or updated occupation</returns>
        Task<OccupationModel> AddUpdateOccupation(OccupationModel model);

    }
}
