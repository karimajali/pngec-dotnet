using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNGEC.API.Domain.Mobile.General
{
    public class MKALLGModel
    {
        /// <summary>
        /// Gets or sets the LLG identifier.
        /// </summary>
        /// <value>
        /// The LLG identifier.
        /// </value>
        public int LLGId { get; set; }

        /// <summary>
        /// Gets or sets the name of the LLG.
        /// </summary>
        /// <value>
        /// The name of the LLG.
        /// </value>
        public string LLGName { get; set; }
        /// <summary>
        /// Gets or sets the list of the Ward.
        /// </summary>
        /// <value>
        /// The list of the Ward.
        /// </value>
        public List<MKAWardModel>  Wards{ get; set; }
    }
}
