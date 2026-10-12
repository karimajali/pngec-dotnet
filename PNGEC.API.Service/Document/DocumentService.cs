using System;
using System.Threading.Tasks;
using PNGEC.API.Domain;
using PNGEC.API.Domain.Document;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Helper;
using PNGEC.API.Repository.Document;

namespace PNGEC.API.Service.Document
{
    public class DocumentService : IDocumentService
    {
        #region variable declaration

        /// <summary>
        /// The i document repository
        /// </summary>
        private readonly IDocumentRepository iDocumentRepository;

        /// <summary>
        /// The response
        /// </summary>
        private Response response = null;

        /// <summary>
        /// The lstresponse
        /// </summary>
        private Response<DocumentModel> lstresponse = null;

        /// <summary>
        /// The result
        /// </summary>
        private Result<DocumentModel> result = null;

        /// <summary>
        /// The module name
        /// </summary>
        private const string ModuleName = "user";

        /// <summary>
        /// The API URL
        /// </summary>
        private string apiURL = string.Empty;
        #endregion

        #region constructor 

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentService"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="apiURL">The API URL.</param>
        public DocumentService(string connection, string apiURL)
        {
            iDocumentRepository = new DocumentRepository(connection);
            this.apiURL = apiURL;
        }
        #endregion

        #region service

        #region get document list
        /// <summary>
        /// Gets the document list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Response<DocumentModel>> GetDocumentList(PageList model)
        {
            lstresponse = new Response<DocumentModel>();

            try
            {
                result = await iDocumentRepository.GetDocumentList(model);

                if (result != null && result.ListItems != null)
                {
                    foreach (var item in result.ListItems)
                    {
                        if (item.Type != 5)
                        {
                            item.FileUrl = string.Format("{0}/{1}/{2}/{3}", apiURL, Constants.UploadFolderPath,
                            Constants.DocumentFolderPath, item.FileName);
                        }
                        else
                        {
                            item.FileUrl = string.Format("{0}/{1}/{2}/{3}", apiURL, Constants.UploadFolderPath,
                            Constants.ElectorAttachmentFolderPath, item.FileName);
                        }
                    }

                    lstresponse.Data = result;
                    lstresponse.Status = true;
                }
                else
                {
                    lstresponse.Message = ResponseMessage.RecordNotExist;
                }
            }
            catch (Exception ex)
            {
                lstresponse.Message = ex.Message;
                ErrorLog.Write("DocumentService", "GetDocumentList", ex);
            }

            return lstresponse;
        }
        #endregion

        #region add delete user

        /// <summary>
        /// Adds the delete document.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Response> AddDeleteDocument(DocumentModel model)
        {
            response = new Response();

            try
            {
                if (model.FileImage != null)
                {
                    model.FileName = model.FileImage.FileName;
                }

                DocumentModel document = await iDocumentRepository.AddDeleteDocument(model);

                response.Status = document.ResponseCode > 0;

                if (document.ResponseCode > 0)
                {
                    if (model.DocumentId > 0)
                    {
                        response.Message = string.Format(ResponseMessage.RecordDeleteSuccess, "Document");
                        response.Data = document;
                    }
                    else
                    {
                        // Save in folder
                        if (model.FileImage != null && model.FileImage.Length > 0)
                        {
                            if (model.Type == 3 || model.Type == 4)
                            {
                                model.FileName = await UploadFiles.UploadDocument(model.FileImage, Convert.ToString(document.DocumentId) + "_" + model.FileName);
                            }
                            else
                            {
                                model.FileName = await UploadFiles.UploadDocument(model.FileImage, model.FileName);
                                document.FileUrl = string.Format("{0}/{1}/{2}/{3}", apiURL, Constants.UploadFolderPath,
                                             Constants.DocumentFolderPath, model.FileName);
                            }
                        }
                        response.Message = string.Format(ResponseMessage.RecordAddSuccess, "Document");
                        response.Data = document;
                    }
                }
                else
                {
                    response.Message = string.Format("Something went wrong", "Document");
                }

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("DocumentService", "AddDeleteDocument", ex);
            }
            return response;
        }
        #endregion

        #endregion

    }
}
