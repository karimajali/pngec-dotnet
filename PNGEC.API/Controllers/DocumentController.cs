using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PNGEC.API.Domain.Document;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Helper;
using PNGEC.API.Service.Document;

namespace PNGEC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        #region variable declaration

        /// <summary>
        /// The i document service
        /// </summary>
        private IDocumentService iDocumentService;

        /// <summary>
        /// The response
        /// </summary>
        private Response response = null;

        /// <summary>
        /// The response
        /// </summary>
        private Response<DocumentModel> lstresponse = null;
        #endregion

        #region constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentController"/> class.
        /// </summary>
        /// <param name="documentService">The document service.</param>
        public DocumentController(IDocumentService documentService)
        {
            iDocumentService = documentService;
        }
        #endregion

        #region api
        #region get user list
        /// <summary>
        /// Gets the user list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetDocumentList")]
        public async Task<IActionResult> GetDocumentList([FromBody]PageList model)
        {
            lstresponse = new Response<DocumentModel>();

            if (model != null)
            {
                lstresponse = await iDocumentService.GetDocumentList(model);
            }
            else
            {
                lstresponse.Status = false;
                lstresponse.Message = ResponseMessage.Invalid_Requests_Model;
            }
            return Ok(lstresponse);
        }
        #endregion

        #region add document

        /// <summary>
        /// Adds the document.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddDeleteDocument")]
        public async Task<IActionResult> AddDeleteDocument([FromForm]DocumentModel model)
        {
            response = new Response();

            if (model != null)
            {
                response = await iDocumentService.AddDeleteDocument(model);
            }
            else
            {
                response.Status = false;
                response.Message = ResponseMessage.Invalid_Requests_Model;
            }
            return Ok(response);
        }
        #endregion

        #endregion
    }
}