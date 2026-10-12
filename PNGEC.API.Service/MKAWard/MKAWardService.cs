using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PNGEC.API.Domain.MKAGeoStructure;
using PNGEC.API.Helper;
using PNGEC.API.Repository.MKAWard;
using PNGEC.API.Repository.Ward;

namespace PNGEC.API.Service.MKAWard
{
    public class MKAWardService : IMKAWardService
    {
        #region variable declaration
        /// <summary>
        /// The i Ward repository
        /// </summary>
        private readonly IMKAWardRepository iMKAWardRepository;

        /// <summary>
        /// The response
        /// </summary>
        private Response response = null;

        /// <summary>
        /// The lstresponse
        /// </summary>
        private Response<MKAWardModel> lstresponse = null;

        /// <summary>
        /// The result
        /// </summary>
        private Result<MKAWardModel> result = null;

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
        public MKAWardService(string connection)
        {
            iMKAWardRepository = new MKAWardRepository(connection);
        }
        #endregion

        #region service

        #region add update ward
        /// <summary>
        /// Adds/update the Ward
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Response> AddUpdateMKAWard(MKAWardModel model)
        {
            try
            {
                response = new Response();
                MKAWardModel ward = await iMKAWardRepository.AddUpdateMKAWard(model);

                response.Status = ward.ResponseCode > 0;
                if (ward.ResponseCode == -1)
                {
                    response.Message = ResponseMessage.WardError_MKAWardNumber_Exists;
                }
                else if (ward.ResponseCode == -2)
                {
                    response.Message = ResponseMessage.WardError_MKAWardName_Exists;
                }
                else if (ward.ResponseCode == -3)
                {
                    response.Message = ResponseMessage.WardError_MKAWardIdUsed;
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
        public async Task<Response> ListMKAWard(int WardId)
        {
            var res = new Response();

            try
            {

                var wards = await iMKAWardRepository.ListMKAWard(WardId);
                if (wards != null && wards.ListItems != null && wards.ListItems.Count() > 0)
                {

                    res.Data = wards.ListItems;
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
                ErrorLog.Write("WardService", "ListWard", ex);
            }
            return res;
        }

        #endregion

        #region get ward list
        /// <summary>
        /// Gets the ward list.
        /// </summary>
        /// <returns></returns>
        public async Task<Response> GetMKAWardList()
        {
            var res = new Response(); try
            {
                var wards = await iMKAWardRepository.GetMKAWardList();
                if (wards != null && wards.ListItems != null && wards.ListItems.Count() > 0)
                {

                    res.Data = wards.ListItems;
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
                ErrorLog.Write("WardService", "GetWardList", ex);
            }
            return res;
        }
        #endregion

        #region Ward Transfer

        /// <summary>
        /// Wards the transfer.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Response> MKAWardTransfer(MKAWardModel model)
        {
            try
            {
                response = new Response();
                int result = await iMKAWardRepository.MKAWardTransfer(model);

                if (result > 0)
                {
                    response.Status = true;
                    response.Message = ResponseMessage.MKAWard_Transfer_Success;
                }
                else
                {
                    response.Message = ResponseMessage.MKAWard_Transfer_Failure;
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
