using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PNGEC.API.Domain.LLG;
using PNGEC.API.Helper;
using PNGEC.API.Repository.LLG;

namespace PNGEC.API.Service.LLG
{
    public class LLGService : ILLGService
    {
        #region variable declaration
        /// <summary>
        /// The i LLG repository
        /// </summary>
        private readonly ILLGRepository iLLGRepository;

        /// <summary>
        /// The response
        /// </summary>
        private Response response = null;

        /// <summary>
        /// The lstresponse
        /// </summary>
        private Response<LLGModel> lstresponse = null;

        /// <summary>
        /// The result
        /// </summary>
        private Result<LLGModel> result = null;

        /// <summary>
        /// The module name
        /// </summary>
        private const string ModuleName = "LLG";
        #endregion

        #region constructor 
        /// <summary>
        /// Initializes a new instance of the <see cref="LLGService"/> class.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public LLGService(string connection)
        {
            iLLGRepository = new LLGRepository(connection);
        }
        #endregion

        #region service

        #region add update llg
        /// <summary>
        /// Adds/update the LLG
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Response> AddUpdateLLG(LLGModel model)
        {
            try
            {
                response = new Response();
                LLGModel LLG = await iLLGRepository.AddUpdateLLG(model);

                response.Status = LLG.ResponseCode > 0;
                if (LLG.ResponseCode == -1)
                {
                    response.Message = ResponseMessage.LLGError_LLGName_Exists;
                }
                else if (LLG.ResponseCode == -2)
                {
                    response.Message = ResponseMessage.LLGError_LLGId_Used;
                }
                else
                {
                    if (model.LLGId <= 0)
                    {
                        response.Message = string.Format(ResponseMessage.RecordAddSuccess, "LLG");
                    }
                    else
                    {
                        response.Message = string.Format(ResponseMessage.RecordUpdateSuccess, "LLG");
                    }

                    response.Data = LLG;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("LLGService", "AddUpdateLLG", ex);
            }
            return response;
        }
        #endregion

        #region list llg
        /// <summary>
        /// List the LLG
        /// </summary>
        /// <returns></returns>
        public async Task<List<LLGModel>> ListLLG(int LLGId)
        {
            List<LLGModel> LLGs = new List<LLGModel>();
            try
            {
                LLGs = await iLLGRepository.ListLLG(LLGId);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("LLGService", "ListLLG", ex);
            }
            return LLGs;
        }

        #endregion

        #region get llg list
        /// <summary>
        /// Gets the LLG list.
        /// </summary>
        /// <returns></returns>
        public async Task<List<LLGModel>> GetLLGList()
        {
            List<LLGModel> LLGs = new List<LLGModel>();
            try
            {
                LLGs = await iLLGRepository.GetLLGList();
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("LLGService", "GetLLGList", ex);
            }
            return LLGs;
        }
        #endregion

        #region llg Transfer

        /// <summary>
        /// LLGs the transfer.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Response> LLGTransfer(LLGModel model)
        {
            try
            {
                response = new Response();
                int result = await iLLGRepository.LLGTransfer(model);

                if (result > 0)
                {
                    response.Status = true;
                    response.Message = ResponseMessage.LLG_Transfer_Success;
                }
                else
                {
                    response.Message = ResponseMessage.LLG_Transfer_Failure;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("LLGService", "LLGTransfer", ex);
            }
            return response;
        }
        #endregion

        #endregion
    }
}
