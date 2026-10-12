using PNGEC.API.Domain.AugmentedRollStat;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Helper;
using System.Threading.Tasks;

namespace PNGEC.API.Service.AugmentedRollStat
{
    public interface IAugmentedRollStatService
    {
        /// <summary>
        /// Gets the augmented roll stat list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Response<AugmentedRollStatModel>> GetAugmentedRollStatList(PageList model);

        /// <summary>
        /// Adds the update augmented roll stat.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Response> AddUpdateAugmentedRollStat(AugmentedRollStatModel model);
    }
}
