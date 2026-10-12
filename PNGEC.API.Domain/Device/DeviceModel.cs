using PNGEC.API.Domain.Base;

namespace PNGEC.API.Domain.Device
{
    public class DeviceModel : BaseModel
    {
        /// <summary>
        /// Gets or sets the device identifier.
        /// </summary>
        /// <value>
        /// The device identifier.
        /// </value>
        public int DeviceId { get; set; }

        /// <summary>
        /// Gets or sets the device number.
        /// </summary>
        /// <value>
        /// The device number.
        /// </value>
        public string DeviceNumber { get; set; }

        /// <summary>
        /// Gets or sets the device name.
        /// </summary>
        /// <value>
        /// The device name.
        /// </value>
        public string DeviceName { get; set; }

        /// <summary>
        /// Gets or sets the device type.
        /// </summary>
        /// <value>
        /// The device type.
        /// </value>
        public string DeviceType { get; set; }

        /// <summary>
        /// Gets or sets the device assigned to.
        /// </summary>
        /// <value>
        /// The device AssignedTo.
        /// </value>
        public int AssignedTo { get; set; }

        /// <summary>
        /// Gets or sets the device assigned to Email Address.
        /// </summary>
        /// <value>
        /// The device EmailAddress.
        /// </value>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the device name changed or not.
        /// </summary>
        /// <value>
        /// The device name changed.
        /// </value>
        public bool DeviceNameChanged { get; set; }

        /// <summary>
        /// Gets or sets the assigned to user changed or not.
        /// </summary>
        /// <value>
        /// The assigned to user changed.
        /// </value>
        public bool AssignedToChanged { get; set; }

        /// <summary>
        /// Gets or sets the device status.
        /// </summary>
        /// <value>
        /// The device status.
        /// </value>
        public bool Status { get; set; }

        /// <summary>
        /// Gets or sets the name of the status.
        /// </summary>
        /// <value>
        /// The name of the status.
        /// </value>
        public string StatusName { get; set; }

        /// <summary>
        /// Gets or sets the device OS.
        /// </summary>
        /// <value>
        /// The device OS.
        /// </value>
        public string DeviceOS { get; set; }

        /// <summary>
        /// Gets or sets the response code.
        /// </summary>
        /// <value>
        /// The response code.
        /// </value>
        public int ResponseCode { get; set; }
    }
}
