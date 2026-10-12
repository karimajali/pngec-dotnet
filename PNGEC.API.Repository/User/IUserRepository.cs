using PNGEC.API.Domain.Listing;
using PNGEC.API.Domain.Mobile.User;
using PNGEC.API.Domain.User;
using PNGEC.API.Helper;
using System.Threading.Tasks;

namespace PNGEC.API.Repository.User
{
    public interface IUserRepository
    {
        /// <summary>
        /// Adds the update user.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<UserModel> AddUpdateUser(UserModel model);

        /// <summary>
        /// Gets the user list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Result<UserModel>> GetUserList(PageList model);

        /// <summary>
        /// Updates the user status.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Result<UserModel>> UpdateUserStatus(CommonRequest model);

        /// <summary>
        /// Users the login.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Result<UserModel>> UserLogin(CommonRequest model);

        /// <summary>
        /// Gets the super admin details.
        /// </summary>
        /// <returns></returns>
        Task<UserModel> GetSuperAdminDetails();

        /// <summary>
        /// Mobiles the user login.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<UserModel> MobileUserLogin(LoginUserRequestModel model);

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<UserModel> ResetPassword(UserModel model);
    }
}
