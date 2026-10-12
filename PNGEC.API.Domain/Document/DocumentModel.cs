using System;
using Microsoft.AspNetCore.Http;
using PNGEC.API.Domain.Base;

namespace PNGEC.API.Domain.Document
{
    public class DocumentModel : BaseModel
    {
        /// <summary>
        /// Gets or sets the document identifier.
        /// </summary>
        /// <value>
        /// The document identifier.
        /// </value>
        public int DocumentId { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public int Type { get; set; }

        /// <summary>
        /// Gets or sets the name of the document type.
        /// </summary>
        /// <value>
        /// The name of the document type.
        /// </value>
        public string DocumentTypeName { get; set; }

        /// <summary>
        /// Gets or sets the profile file.
        /// </summary>
        /// <value>
        /// The profile file.
        /// </value>
        public IFormFile FileImage { get; set; }

        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the file URL.
        /// </summary>
        /// <value>
        /// The file URL.
        /// </value>
        public string FileUrl { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the create date.
        /// </summary>
        /// <value>
        /// The create date.
        /// </value>
        public DateTime CreatedDate { get; set; }


        /// <summary>
        /// Gets or sets the change string date.
        /// </summary>
        /// <value>
        /// The change string date.
        /// </value>
        public string CreatedDateString { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>
        /// The created by.
        /// </value>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the modified date.
        /// </summary>
        /// <value>
        /// The modified date.
        /// </value>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the modified date string.
        /// </summary>
        /// <value>
        /// The modified date string.
        /// </value>
        public string ModifiedDateString { get; set; }

        /// <summary>
        /// Gets or sets the modified by.
        /// </summary>
        /// <value>
        /// The modified by.
        /// </value>
        public int ModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the response code.
        /// </summary>
        /// <value>
        /// The response code.
        /// </value>
        public int ResponseCode { get; set; }


    }
}
