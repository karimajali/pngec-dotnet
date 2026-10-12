using PNGEC.API.Domain.Listing;
using PNGEC.API.Domain.Mobile;
using PNGEC.API.Domain.Mobile.User;
using PNGEC.API.Domain.User;
using PNGEC.API.Helper;
using System.Threading.Tasks;

namespace PNGEC.API.Service.User
{
    public interface IUserService
    {
        /// <summary>
        /// Adds the update user.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Response> AddUpdateUser(UserModel model);

        /// <summary>
        /// Gets the user list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Response<UserModel>> GetUserList(PageList model);

        /// <summary>
        /// Updates the user status.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Response<UserModel>> UpdateUserStatus(CommonRequest model);

        /// <summary>
        /// Users the login.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Response<UserModel>> UserLogin(CommonRequest model);

        /// <summary>
        /// Forgots the password.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Response> ForgotPassword(CommonRequest model);

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Response> ResetPassword(UserModel model);

        /// <summary>
        /// Mobiles the user login.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Response> MobileUserLogin(LoginUserRequestModel model);
    }
}
