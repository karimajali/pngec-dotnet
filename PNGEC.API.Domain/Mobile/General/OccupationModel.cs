using System;

namespace PNGEC.API.Domain.Mobile.General
{
    /// <summary>
    /// Occupation model class
    /// </summary>
    public class OccupationModel
    {
        /// <summary>
        /// Gets or sets the occupation pk.
        /// </summary>
        /// <value>
        /// The occupation pk.
        /// </value>
        public int OccupationPK { get; set; }

        /// <summary>
        /// Gets or sets the name of the occupation.
        /// </summary>
        /// <value>
        /// The name of the occupation.
        /// </value>
        public string OccupationName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is deleted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is deleted; otherwise, <c>false</c>.
        /// </value>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the action by.
        /// </summary>
        /// <value>
        /// The action by.
        /// </value>
        public int ActionBy { get; set; }

        /// <summary>
        /// Gets or sets the response code.
        /// </summary>
        /// <value>
        /// The response code.
        /// </value>
        public int ResponseCode { get; set; }
    }
}
