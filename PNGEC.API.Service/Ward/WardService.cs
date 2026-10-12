using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PNGEC.API.Domain.Ward;
using PNGEC.API.Helper;
using PNGEC.API.Repository.Ward;

namespace PNGEC.API.Service.Ward
{
    public class WardService : IWardService
    {
        #region variable declaration
        /// <summary>
        /// The i Ward repository
        /// </summary>
        private readonly IWardRepository iWardRepository;

        /// <summary>
        /// The response
        /// </summary>
        private Response response = null;

        /// <summary>
        /// The lstresponse
        /// </summary>
        private Response<WardModel> lstresponse = null;

        /// <summary>
        /// The result
        /// </summary>
        private Result<WardModel> result = null;

        /// <summary>
        /// The module name
        /// </summary>
        private const string ModuleName = "Ward";
        #endregion

        #region constructor 
        /// <summary>
        /// Initializes a new instance of the <see cref="WardService"/> class.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public WardService(string connection)
        {
            iWardRepository = new WardRepository(connection);
        }
        #endregion

        #region service

        #region add update ward
        /// <summary>
        /// Adds/update the Ward
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Response> AddUpdateWard(WardModel model)
        {
            try
            {
                response = new Response();
                WardModel ward = await iWardRepository.AddUpdateWard(model);

                response.Status = ward.ResponseCode > 0;
                if (ward.ResponseCode == -1)
                {
                    response.Message = ResponseMessage.WardError_WardNumber_Exists;
                }
                else if (ward.ResponseCode == -2)
                {
                    response.Message = ResponseMessage.WardError_WardName_Exists;
                }
                else if (ward.ResponseCode == -3)
                {
                    response.Message = ResponseMessage.WardError_WardIdUsed;
                }
                else
                {
                    if (model.WardId <= 0)
                    {
                        response.Message = string.Format(ResponseMessage.RecordAddSuccess, "Ward");
                    }
                    else
                    {
                        response.Message = string.Format(ResponseMessage.RecordUpdateSuccess, "Ward");
                    }

                    response.Data = ward;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("WardService", "AddUpdateWard", ex);
            }
            return response;
        }
        #endregion

        #region list ward
        /// <summary>
        /// List the Ward
        /// </summary>
        /// <returns></returns>
        public async Task<List<WardModel>> ListWard(int WardId)
        {
            List<WardModel> Wards = new List<WardModel>();
            try
            {
                Wards = await iWardRepository.ListWard(WardId);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("WardService", "ListWard", ex);
            }
            return Wards;
        }

        #endregion

        #region get ward list
        /// <summary>
        /// Gets the ward list.
        /// </summary>
        /// <returns></returns>
        public async Task<List<WardModel>> GetWardList()
        {
            List<WardModel> Wards = new List<WardModel>();
            try
            {
                Wards = await iWardRepository.GetWardList();
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("WardService", "GetWardList", ex);
            }
            return Wards;
        }
        #endregion

        #region Ward Transfer

        /// <summary>
        /// Wards the transfer.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Response> WardTransfer(WardModel model)
        {
            try
            {
                response = new Response();
                int result = await iWardRepository.WardTransfer(model);

                if (result > 0)
                {
                    response.Status = true;
                    response.Message = ResponseMessage.Ward_Transfer_Success;
                }
                else
                {
                    response.Message = ResponseMessage.Ward_Transfer_Failure;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("WardService", "WardTransfer", ex);
            }
            return response;
        }
        #endregion

        #endregion
    }
}
