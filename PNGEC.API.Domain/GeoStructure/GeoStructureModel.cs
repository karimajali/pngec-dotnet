using PNGEC.API.Domain.Base;

namespace PNGEC.API.Domain.GeoStructure
{
    public class GeoStructureModel : BaseModel
    {
        /// <summary>
        /// Gets or sets the GeoStructure identifier.
        /// </summary>
        /// <value>
        /// The GeoStructure identifier.
        /// </value>
        public int GeoStructureId { get; set; }

        /// <summary>
        /// Gets or sets the GeoStructure name.
        /// </summary>
        /// <value>
        /// The GeoStructure name.
        /// </value>
        public string GeoStructureName { get; set; }

        /// <summary>
        /// Gets or sets the Sequence.
        /// </summary>
        /// <value>
        /// The Sequence.
        /// </value>
        public string Sequence { get; set; }

        /// <summary>
        /// Gets or sets the GeoStructure status.
        /// </summary>
        /// <value>
        /// The GeoStructure status.
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
