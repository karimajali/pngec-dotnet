using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNGEC.Desktop.Model
{
    public class MKAGeoTreeStructure
    {/// <summary>
     /// Gets or sets the identifier.
     /// </summary>
     /// <value>
     /// The identifier.
     /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the name of the main tag.
        /// </summary>
        /// <value>
        /// The name of the main tag.
        /// </value>
        public string MainTagName { get; set; }

        /// <summary>
        /// Gets or sets the name of the child node.
        /// </summary>
        /// <value>
        /// The name of the child node.
        /// </value>
        public string ChildNodeName { get; set; }

        /// <summary>
        /// Gets or sets the parent identifier.
        /// </summary>
        /// <value>
        /// The parent identifier.
        /// </value>
        public int ParentId { get; set; }

        /// <summary>
        /// Gets or sets the children.
        /// </summary>
        /// <value>
        /// The children.
        /// </value>
        public IEnumerable<MKAGeoTreeStructure> Children { get; set; }
    }
}
