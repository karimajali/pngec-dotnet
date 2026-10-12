using System.Threading.Tasks;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Domain.Mobile.General;
using PNGEC.API.Helper;

namespace PNGEC.API.Service.Occupation
{
    /// <summary>
    /// occupation service interface
    /// </summary>
    public interface IOccupationService
    {
        /// <summary>
        /// Gets the occupation list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>return occupation list</returns>
        Task<Response> GetOccupationList(PageList model);

        /// <summary>
        /// Adds the update occupation.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>return occupation</returns>
        Task<Response> AddUpdateOccupation(OccupationModel model);
    }
}
