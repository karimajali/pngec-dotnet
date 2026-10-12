using PNGEC.API.Domain.Listing;
using PNGEC.API.Domain.Writ;
using PNGEC.API.Helper;
using PNGEC.API.Repository.Occupation;
using PNGEC.API.Repository.Writ;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PNGEC.API.Service.Writ
{
    public class WritService : IWritService
    {
        #region variable declaration
        /// <summary>
        /// The i Writ repository
        /// </summary>
        private readonly IWritRepository iWritRepository;

        /// <summary>
        /// The response
        /// </summary>
        private Response response = null;

        /// <summary>
        /// The lstresponse
        /// </summary>
        private Response<WritModel> lstresponse = null;

        /// <summary>
        /// The result
        /// </summary>
        private Result<WritModel> result = null;

        /// <summary>
        /// The module name
        /// </summary>
        private const string ModuleName = "Writ";
        #endregion

        #region constructor 
        /// <summary>
        /// Initializes a new instance of the <see cref="WritService"/> class.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public WritService(string connection)
        {
            iWritRepository = new WritRepository(connection);
        }
        #endregion

        #region service

        #region add update Writ
        /// <summary>
        /// Adds/update the Writ
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Response> AddUpdateWrit(WritModel model)
        {
            try
            {
                response = new Response();
                WritModel Writ = await iWritRepository.AddUpdateWrit(model);

                response.Status = Writ.ResponseCode > 0;
                if (Writ.ResponseCode == -1)
                {
                    response.Message = ResponseMessage.WritError_WritName_Exists;
                }               
                else
                {
                    if (model.Id <= 0)
                    {
                        response.Message = string.Format(ResponseMessage.RecordAddSuccess, "Writ");
                    }
                    else
                    {
                        response.Message = string.Format(ResponseMessage.RecordUpdateSuccess, "Writ");
                    }

                    response.Data = Writ;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("WritService", "AddUpdateWrit", ex);
            }
            return response;
        }
        #endregion

        #region get Writ list
        /// <summary>
        /// Gets the Writ list.
        /// </summary>
        /// <returns></returns>
        
        public async Task<Response> GetWritList(PageList model)
        {
            Response response = new Response();

            try
            {
                var writslist = await iWritRepository.GetWritList(model);

                if (writslist != null)
                {
                    response.Data = writslist;
                    response.Status = true;
                }
                else
                {
                    response.Message = ResponseMessage.RecordNotExist;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("WritService", "GetWritList", ex);
            }

            return response;
        }

        #endregion


        #endregion
    }
}
