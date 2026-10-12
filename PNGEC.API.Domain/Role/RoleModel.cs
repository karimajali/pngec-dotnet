using PNGEC.API.Domain.Base;

namespace PNGEC.API.Domain.Role
{
    public class RoleModel : BaseModel
    {
        /// <summary>
        /// Gets or sets the Role identifier.
        /// </summary>
        /// <value>
        /// The Role identifier.
        /// </value>
        public int RoleId { get; set; }

        /// <summary>
        /// Gets or sets the Role name.
        /// </summary>
        /// <value>
        /// The Role name.
        /// </value>
        public string RoleName { get; set; }

        /// <summary>
        /// Gets or sets the role about.
        /// </summary>
        /// <value>
        /// The role about.
        /// </value>
        public string About { get; set; }

        /// <summary>
        /// Gets or sets the device status.
        /// </summary>
        /// <value>
        /// The device status.
        /// </value>
        public bool Status { get; set; }

        /// <summary>
        /// Gets or sets the response code.
        /// </summary>
        /// <value>
        /// The response code.
        /// </value>
        public int ResponseCode { get; set; }
    }
}
