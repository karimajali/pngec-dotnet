using PNGEC.API.Domain.User;
using PNGEC.API.Helper;

namespace PNGEC.Desktop.Model
{
    public class LoggedUserModel
    {
        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public UserModel User { get; set; }

        /// <summary>
        /// Gets or sets the database.
        /// </summary>
        /// <value>
        /// The database.
        /// </value>
        public CommonEnum.Database Database { get; set; }

        /// <summary>
        /// Gets or sets the server.
        /// </summary>
        /// <value>
        /// The server.
        /// </value>
        public CommonEnum.Server Server { get; set; }
    }
}
