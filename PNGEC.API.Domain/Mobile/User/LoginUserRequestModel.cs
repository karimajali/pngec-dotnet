namespace PNGEC.API.Domain.Mobile.User
{
    public class LoginUserRequestModel
    {
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the device number.
        /// </summary>
        /// <value>
        /// The device number.
        /// </value>
        public string DeviceNumber { get; set; }
    }
}
