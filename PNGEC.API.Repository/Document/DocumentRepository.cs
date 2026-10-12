using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using PNGEC.API.Domain.Document;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Helper;

namespace PNGEC.API.Repository.Document
{
    public class DocumentRepository : IDocumentRepository
    {
        #region variable declaration
        /// <summary>
        /// The connection string
        /// </summary>
        private readonly string connectionString;

        /// <summary>
        /// The document
        /// </summary>
        private DocumentModel document = null;

        /// <summary>
        /// The result
        /// </summary>
        private Result<DocumentModel> result = null;
        #endregion

        #region constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentRepository"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public DocumentRepository(string connection)
        {
            connectionString = connection;
        }

        #endregion

        #region repository

        #region get document list
        /// <summary>
        /// Gets the document list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Result<DocumentModel>> GetDocumentList(PageList model)
        {
            result = new Result<DocumentModel>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.Get_All_Document_List;

                DynamicParameters param = new DynamicParameters();
                param.Add("@PageNo", model.PageNo);
                param.Add("@PageSize", model.PageSize);
                param.Add("@KeyWord", model.KeyWord);
                param.Add("@DocumentType", model.DocumentType);
                //param.Add("@SortField", string.IsNullOrWhiteSpace(model.SortField) ? "DocumentId" : model.SortField);
                //param.Add("@SortOrder", string.IsNullOrWhiteSpace(model.SortOrder) ? "DESC" : model.SortOrder);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                var lstdocument = await con.QueryMultipleAsync(sQuery, param, commandType: CommandType.StoredProcedure);
                if (lstdocument != null)
                {
                    result.ListItems = await lstdocument.ReadAsync<DocumentModel>();
                    result.TotalRecordCount = await lstdocument.ReadFirstOrDefaultAsync<int>();
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                return result;
            }
        }
        #endregion

        #region add delete document

        /// <summary>
        /// Adds the delete document.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<DocumentModel> AddDeleteDocument(DocumentModel model)
        {
            document = new DocumentModel();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.Add_Delete_Document;

                DynamicParameters param = new DynamicParameters();
                param.Add("@DocumentId", model.DocumentId);
                param.Add("@Type", model.Type);
                param.Add("@FileName", model.FileName);
                param.Add("@Description", model.Description);
                param.Add("@CreatedBy", model.CreatedBy);
                param.Add("@ModifiedBy", model.ModifiedBy);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                document = await con.QueryFirstOrDefaultAsync<DocumentModel>(sQuery, param, commandType: CommandType.StoredProcedure);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return document;
        }
        #endregion

        #endregion
    }
}
