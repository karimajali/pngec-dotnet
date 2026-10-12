using PNGEC.API.Domain.Mobile.Voter;
using System.Collections.Generic;

namespace PNGEC.API.Helper
{
    public class CommonRequest
    {
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public string username { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string password { get; set; }

        /// <summary>
        /// Gets or sets the database.
        /// </summary>
        /// <value>
        /// The database.
        /// </value>
        public string database { get; set; }

        /// <summary>
        /// Gets or sets the server.
        /// </summary>
        /// <value>
        /// The database.
        /// </value>
        public string server { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="CommonRequest"/> is status.
        /// </summary>
        /// <value>
        ///   <c>true</c> if status; otherwise, <c>false</c>.
        /// </value>
        public bool status { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int id { get; set; }

        /// <summary>
        /// Gets or sets the user ids.
        /// </summary>
        /// <value>
        /// The user ids.
        /// </value>
        public string ids { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public string recordType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is approved.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is approved; otherwise, <c>false</c>.
        /// </value>
        public bool isApproved { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is rejected.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is rejected; otherwise, <c>false</c>.
        /// </value>
        public bool isRejected { get; set; }

        /// <summary>
        /// Gets or sets the voter list.
        /// </summary>
        /// <value>
        /// The voter list.
        /// </value>
        public List<VoterModel> VoterList { get; set; }

    }
}
