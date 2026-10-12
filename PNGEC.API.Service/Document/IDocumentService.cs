using System.Threading.Tasks;
using PNGEC.API.Domain.Document;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Helper;

namespace PNGEC.API.Service.Document
{
    public interface IDocumentService
    {
        /// <summary>
        /// Gets the document list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Response<DocumentModel>> GetDocumentList(PageList model);

        /// <summary>
        /// Adds the delete document.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Response> AddDeleteDocument(DocumentModel model);
    }
}
