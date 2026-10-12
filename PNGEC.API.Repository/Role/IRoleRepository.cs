using PNGEC.API.Domain.Listing;
using PNGEC.API.Domain.Role;
using PNGEC.API.Helper;
using System.Threading.Tasks;

namespace PNGEC.API.Repository.Role
{
    public interface IRoleRepository
    {
        /// <summary>
        /// Add/update the Role
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<RoleModel> AddUpdateRole(RoleModel model);

        /// <summary>
        /// Gets the Role list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Result<RoleModel>> GetRoleList(PageList model);
    }
}
