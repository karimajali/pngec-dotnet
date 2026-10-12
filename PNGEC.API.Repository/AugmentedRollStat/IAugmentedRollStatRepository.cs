using PNGEC.API.Domain.AugmentedRollStat;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Helper;
using System.Threading.Tasks;

namespace PNGEC.API.Repository.AugmentedRollStat
{
    public interface IAugmentedRollStatRepository
    {
        /// <summary>
        /// Gets the augmented roll stat list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Result<AugmentedRollStatModel>> GetAugmentedRollStatList(PageList model);

        /// <summary>
        /// Adds the update augmented roll stat.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<AugmentedRollStatModel> AddUpdateAugmentedRollStat(AugmentedRollStatModel model);
    }
}
