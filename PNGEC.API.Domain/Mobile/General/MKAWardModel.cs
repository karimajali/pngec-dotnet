using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNGEC.API.Domain.Mobile.General
{
    public class MKAWardModel
    {
        /// <summary>
        /// Gets or sets the ward identifier.
        /// </summary>
        /// <value>
        /// The ward identifier.
        /// </value>
        public int WardId { get; set; }

        /// <summary>
        /// Gets or sets the name of the ward.
        /// </summary>
        /// <value>
        /// The name of the ward.
        /// </value>
        public string WardName { get; set; }

        /// <summary>
        /// Gets or sets the ward number.
        /// </summary>
        /// <value>
        /// The ward number.
        /// </value>
        public int WardNumber { get; set; }
        /// <summary>
        /// Gets or sets the list of the Ward.
        /// </summary>
        /// <value>
        /// The list of the Ward.
        /// </value>
        public List<MKALocationModel> Locations { get; set; }

        /// <summary>
        /// Gets or sets the list of the Clans.
        /// </summary>
        /// <value>
        /// The list of the Clans.
        /// </value>
        public List<MKAClanModel> Clans { get; set; }
    }
}
