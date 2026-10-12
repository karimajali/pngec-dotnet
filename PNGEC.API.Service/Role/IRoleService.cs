using PNGEC.API.Domain.Listing;
using PNGEC.API.Domain.Role;
using PNGEC.API.Helper;
using System.Threading.Tasks;

namespace PNGEC.API.Service.Role
{
    public interface IRoleService
    {
        /// <summary>
        /// Adds/update the Role
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Response> AddUpdateRole(RoleModel model);

        /// <summary>
        /// Gets the Role list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Response<RoleModel>> GetRoleList(PageList model);
    }
}
