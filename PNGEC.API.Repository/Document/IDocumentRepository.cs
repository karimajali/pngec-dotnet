using System.Threading.Tasks;
using PNGEC.API.Domain.Document;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Helper;

namespace PNGEC.API.Repository.Document
{
    public interface IDocumentRepository
    {

        /// <summary>
        /// Gets the document list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Result<DocumentModel>> GetDocumentList(PageList model);

        /// <summary>
        /// Adds the delete document.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<DocumentModel> AddDeleteDocument(DocumentModel model);
    }
}
