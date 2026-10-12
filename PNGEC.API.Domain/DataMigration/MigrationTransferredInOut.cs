using System;

namespace PNGEC.API.Domain.DataMigration
{
    public class MigrationTransferredInOut
    {
        #region Voter Details
        /// <summary>
        /// Gets or sets the elector identifier.
        /// </summary>
        /// <value>
        /// The elector identifier.
        /// </value>
        public long ElectorId { get; set; }

        /// <summary>
        /// Gets or sets the old elector identifier.
        /// </summary>
        /// <value>
        /// The old elector identifier.
        /// </value>
        public long OldElectorID { get; set; }

        /// <summary>
        /// Gets or sets the name of the l.
        /// </summary>
        /// <value>
        /// The name of the l.
        /// </value>
        public string LName { get; set; }

        /// <summary>
        /// Gets or sets the name of the f.
        /// </summary>
        /// <value>
        /// The name of the f.
        /// </value>
        public string FName { get; set; }

        /// <summary>
        /// Gets or sets the dob.
        /// </summary>
        /// <value>
        /// The dob.
        /// </value>
        public DateTime DOB { get; set; }

        /// <summary>
        /// Gets or sets the province identifier.
        /// </summary>
        /// <value>
        /// The province identifier.
        /// </value>
        public int ProvinceId { get; set; }
        #endregion

        #region Em Details
        /// <summary>
        /// Gets or sets the em email address.
        /// </summary>
        /// <value>
        /// The em email address.
        /// </value>
        public string EmEmailAddress { get; set; }
        #endregion
    }
}
