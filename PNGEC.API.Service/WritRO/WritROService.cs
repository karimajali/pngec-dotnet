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
    public class WritROService : IWritROService
    {
        #region variable declaration
        /// <summary>
        /// The i Writ repository
        /// </summary>
        private readonly IWritRORepository iWritRORepository;

        /// <summary>
        /// The response
        /// </summary>
        private Response response = null;

        /// <summary>
        /// The lstresponse
        /// </summary>
        private Response<WritRO> lstresponse = null;

        /// <summary>
        /// The result
        /// </summary>
        private Result<WritRO> result = null;

        /// <summary>
        /// The module name
        /// </summary>
        private const string ModuleName = "WritRO";
        #endregion

        #region constructor 
        /// <summary>
        /// Initializes a new instance of the <see cref="WritService"/> class.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public WritROService(string connection)
        {
            iWritRORepository = new WritRORepository(connection);
        }
        #endregion

        #region service


        #region add update Writ
        /// <summary>
        /// Adds/update the Writ
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Response> AddUpdateWritRO(WritRO model)
        {
            try
            {
                response = new Response();
                WritRO Writ = await iWritRORepository.AddUpdateWritRO(model);
              
                if (response.Status)
                {                   
                    if (model.WritROId <= 0)
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

        #region get Writ RO list
        /// <summary>
        /// Gets the Writ list.
        /// </summary>
        /// <returns></returns>

        public async Task<Response> GetWritROList(PageList model)
        {
            Response response = new Response();

            try
            {
                var writslist = await iWritRORepository.GetWritROList(model);

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
                ErrorLog.Write("WritService", "GetWritROList", ex);
            }

            return response;
        }     

        #endregion

        #endregion
    }
}
