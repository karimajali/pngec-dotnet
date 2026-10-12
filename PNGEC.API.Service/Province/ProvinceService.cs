using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PNGEC.API.Domain.Province;
using PNGEC.API.Helper;
using PNGEC.API.Repository.Province;

namespace PNGEC.API.Service.Province
{
    public class ProvinceService : IProvinceService
    {
        #region variable declaration
        /// <summary>
        /// The i Province repository
        /// </summary>
        private readonly IProvinceRepository iProvinceRepository;

        /// <summary>
        /// The response
        /// </summary>
        private Response response = null;

        /// <summary>
        /// The lstresponse
        /// </summary>
        private Response<ProvinceModel> lstresponse = null;

        /// <summary>
        /// The result
        /// </summary>
        private Result<ProvinceModel> result = null;

        /// <summary>
        /// The module name
        /// </summary>
        private const string ModuleName = "Province";
        #endregion

        #region constructor 
        /// <summary>
        /// Initializes a new instance of the <see cref="ProvinceService"/> class.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public ProvinceService(string connection)
        {
            iProvinceRepository = new ProvinceRepository(connection);
        }
        #endregion

        #region service

        #region add update province
        /// <summary>
        /// Adds/update the Province
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Response> AddUpdateProvince(ProvinceModel model)
        {
            try
            {
                response = new Response();
                ProvinceModel province = await iProvinceRepository.AddUpdateProvince(model);

                response.Status = province.ResponseCode > 0;
                if (province.ResponseCode == -1)
                {
                    response.Message = ResponseMessage.ProvinceError_ProvinceName_Exists;
                }
                else if (province.ResponseCode == -2)
                {
                    response.Message = ResponseMessage.ProvinceError_ProvinceIdUsed;
                }
                else
                {
                    if (model.ProvinceId <= 0)
                    {
                        response.Message = string.Format(ResponseMessage.RecordAddSuccess, "Province");
                    }
                    else
                    {
                        response.Message = string.Format(ResponseMessage.RecordUpdateSuccess, "Province");
                    }

                    response.Data = province;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("ProvinceService", "AddUpdateProvince", ex);
            }
            return response;
        }
        #endregion

        #region list province
        /// <summary>
        /// List the Province
        /// </summary>
        /// <returns></returns>
        public async Task<List<ProvinceModel>> ListProvince(int ProvinceId)
        {
            List<ProvinceModel> Provinces = new List<ProvinceModel>();
            try
            {
                Provinces = await iProvinceRepository.ListProvince(ProvinceId);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("ProvinceService", "ListProvince", ex);
            }
            return Provinces;
        }
        #endregion

        #region get province list
        /// <summary>
        /// Gets the province list.
        /// </summary>
        /// <returns></returns>
        public async Task<List<ProvinceModel>> GetProvinceList()
        {
            List<ProvinceModel> Provinces = new List<ProvinceModel>();
            try
            {
                Provinces = await iProvinceRepository.GetProvinceList();
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("ProvinceService", "GetProvinceList", ex);
            }
            return Provinces;
        }
        #endregion

        #endregion
    }
}
