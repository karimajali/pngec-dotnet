using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PNGEC.API.Domain.District;
using PNGEC.API.Helper;
using PNGEC.API.Repository.District;

namespace PNGEC.API.Service.District
{
    public class DistrictService : IDistrictService
    {
        #region variable declaration
        /// <summary>
        /// The i District repository
        /// </summary>
        private readonly IDistrictRepository iDistrictRepository;

        /// <summary>
        /// The response
        /// </summary>
        private Response response = null;

        /// <summary>
        /// The lstresponse
        /// </summary>
        private Response<DistrictModel> lstresponse = null;

        /// <summary>
        /// The result
        /// </summary>
        private Result<DistrictModel> result = null;

        /// <summary>
        /// The module name
        /// </summary>
        private const string ModuleName = "District";
        #endregion

        #region constructor 
        /// <summary>
        /// Initializes a new instance of the <see cref="DistrictService"/> class.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public DistrictService(string connection)
        {
            iDistrictRepository = new DistrictRepository(connection);
        }
        #endregion

        #region service

        #region add update district
        /// <summary>
        /// Adds/update the District
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Response> AddUpdateDistrict(DistrictModel model)
        {
            try
            {
                response = new Response();
                DistrictModel district = await iDistrictRepository.AddUpdateDistrict(model);

                response.Status = district.ResponseCode > 0;
                if (district.ResponseCode == -1)
                {
                    response.Message = ResponseMessage.DistrictError_BatchID_Exists;
                }
                else if (district.ResponseCode == -2)
                {
                    response.Message = ResponseMessage.DistrictError_DistrictName_Exists;
                }
                else if (district.ResponseCode == -3)
                {
                    response.Message = ResponseMessage.DistrictError_DistrictIdUsed;
                }
                else
                {
                    if (model.DistrictId <= 0)
                    {
                        response.Message = string.Format(ResponseMessage.RecordAddSuccess, "District");
                    }
                    else
                    {
                        response.Message = string.Format(ResponseMessage.RecordUpdateSuccess, "District");
                    }

                    response.Data = district;
                }

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("DistrictService", "AddUpdateDistrict", ex);
            }
            return response;
        }
        #endregion

        #region list district
        /// <summary>
        /// List the District
        /// </summary>
        /// <returns></returns>
        public async Task<List<DistrictModel>> ListDistrict(int DistrictId)
        {
            List<DistrictModel> Districts = new List<DistrictModel>();
            try
            {
                Districts = await iDistrictRepository.ListDistrict(DistrictId);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("DistrictService", "ListDistrict", ex);
            }
            return Districts;
        }

        #endregion

        #region get district list
        /// <summary>
        /// Gets the district list.
        /// </summary>
        /// <returns></returns>
        public async Task<List<DistrictModel>> GetDistrictList()
        {
            List<DistrictModel> Districts = new List<DistrictModel>();
            try
            {
                Districts = await iDistrictRepository.GetDistrictList();
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("DistrictService", "GetDistrictList", ex);
            }
            return Districts;
        }
        #endregion

        #endregion
    }
}
