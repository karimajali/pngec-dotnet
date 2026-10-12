using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PNGEC.API.Domain.MKAGeoStructure;
using PNGEC.API.Helper;
using PNGEC.API.Repository.LLG;
using PNGEC.API.Repository.MKALLG;

namespace PNGEC.API.Service.MKALLG
{
    public class MKALLGService : IMKALLGService
    {
        #region variable declaration
        /// <summary>
        /// The i LLG repository
        /// </summary>
        private readonly IMKALLGRepository iMKALLGRepository;

        /// <summary>
        /// The response
        /// </summary>
        private Response response = null;

        /// <summary>
        /// The lstresponse
        /// </summary>
        private Response<MKALLGModel> lstresponse = null;

        /// <summary>
        /// The result
        /// </summary>
        private Result<MKALLGModel> result = null;

        /// <summary>
        /// The module name
        /// </summary>
        private const string ModuleName = "MKALLG";
        #endregion
        #region constructor 
        /// <summary>
        /// Initializes a new instance of the <see cref="LLGService"/> class.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public MKALLGService(string connection)
        {
            iMKALLGRepository = new MKALLGRepository(connection);
        }
        #endregion
        #region service

        #region add update llg
        /// <summary>
        /// Adds/update the LLG
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Response> AddUpdateMKALLG(MKALLGModel model)
        {
            try
            {
                response = new Response();
                MKALLGModel MKALLG = await iMKALLGRepository.AddUpdateMKALLG(model);

                response.Status = MKALLG.ResponseCode > 0;
                if (MKALLG.ResponseCode == -1)
                {
                    response.Message = ResponseMessage.LLGError_MKALLGName_Exists;
                }
                else if (MKALLG.ResponseCode == -2)
                {
                    response.Message = ResponseMessage.LLGError_MKALLGId_Used;
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

                    response.Data = MKALLG;
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
        public async Task<Response> ListMKALLG(int LLGId)
        {
            List<MKALLGModel> MKALLGs = new List<MKALLGModel>();
            var res = new Response();
            try
            {
                var llgs = await iMKALLGRepository.ListMKALLG(LLGId);
                if (llgs != null && llgs.ListItems != null && llgs.ListItems.Count() > 0)
                {
                    res.Data = llgs.ListItems;
                    res.Message = "Data Get Successfully";
                    res.Status = true;
                }
                else
                {
                    res.Data = null;
                    res.Message = "Data Does Not Exist";
                    res.Status = false;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("LLGService", "ListLLG", ex);
            }
            return res;
        }

        #endregion

        #region get llg list
        /// <summary>
        /// Gets the LLG list.
        /// </summary>
        /// <returns></returns>
        public async Task<Response> GetMKALLGList()
        {
            var res = new Response();
           
            try
            {
                var llgs = await iMKALLGRepository.GetMKALLGList();
                if (llgs != null && llgs.ListItems != null && llgs.ListItems.Count() > 0)
                {
                    res.Data = llgs.ListItems;
                    res.Message = "Data Get Successfully";
                    res.Status = true;
                }
                else
                {
                    res.Data = null;
                    res.Message = "Data Does Not Exist";
                    res.Status = false;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("LLGService", "GetLLGList", ex);
            }
            return res;
        }
        #endregion

        #region llg Transfer

        /// <summary>
        /// LLGs the transfer.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Response> MKALLGTransfer(MKALLGModel model)
        {
            try
            {
                response = new Response();
                int result = await iMKALLGRepository.MKALLGTransfer(model);

                if (result > 0)
                {
                    response.Status = true;
                    response.Message = ResponseMessage.MKALLG_Transfer_Success;
                }
                else
                {
                    response.Message = ResponseMessage.MKALLG_Transfer_Failure;
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
