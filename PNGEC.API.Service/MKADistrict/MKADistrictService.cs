using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PNGEC.API.Domain.MKAGeoStructure;
using PNGEC.API.Helper;
using PNGEC.API.Repository.District;
using PNGEC.API.Repository.MKADistrict;
using PNGEC.API.Repository.Region;
using PNGEC.API.Repository.Voter;

namespace PNGEC.API.Service.MKADistrict
{
    public class MKADistrictService : IMKADistrictService
    {
        #region variable declaration
        /// <summary>
        /// The i MKADistrict repository
        /// </summary>
        private readonly IMKADistrictRepository iMKADistrictRepository;

        /// <summary>
        /// The response
        /// </summary>
        private Response response = null;

        /// <summary>
        /// The lstresponse
        /// </summary>
        private Response<MKADistrictModel> lstresponse = null;

        /// <summary>
        /// The result
        /// </summary>
        private Result<MKADistrictModel> result = null;

        /// <summary>
        /// The module name
        /// </summary>
        private const string ModuleName = "MKADistrict";
        #endregion
        #region constructor 
        /// <summary>
        /// Initializes a new instance of the <see cref="MKADistrictService"/> class.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public MKADistrictService(string connection)
        {
            iMKADistrictRepository = new MKADistrictRepository(connection);
        }
        #endregion
        #region service

        #region add update region
        /// <summary>
        /// Adds/update the Region
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Response> AddUpdateMKADistrict(MKADistrictModel model)
        {
            try
            {
                response = new Response();
                MKADistrictModel mkaDistrict = await iMKADistrictRepository.AddUpdateMKADistrict(model);

                response.Status = mkaDistrict.ResponseCode > 0;
                if (mkaDistrict.ResponseCode == -1)
                {
                    response.Message = ResponseMessage.MKADistrictError_DistrictName_Exists;
                }
                else if (mkaDistrict.ResponseCode == -2)
                {
                    response.Message = ResponseMessage.MKADistrictError_DistrictIdUsed;
                }
                else
                {
                    if (model.DistrictId <= 0)
                    {
                        response.Message = string.Format(ResponseMessage.RecordAddSuccess, "MKADistrict");
                    }
                    else
                    {
                        response.Message = string.Format(ResponseMessage.RecordUpdateSuccess, "MKADistrict");
                    }

                    response.Data = mkaDistrict;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("MKADistrictService", "AddUpdateMKADistrict", ex);
            }
            return response;
        }
        #endregion
        #region list mkadistrict
        /// <summary>
        /// List the District
        /// </summary>
        /// <returns></returns>
        public async Task<Response> ListMKADistrict(int DistrictId)
        {
            var res = new Response();
            try
            {
                var districts = await iMKADistrictRepository.ListMKADistrict(DistrictId);
                if (districts != null && districts.ListItems != null && districts.ListItems.Count() > 0)
                {
              
                    res.Data = districts.ListItems;
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
                ErrorLog.Write("DistrictService", "ListDistrict", ex);
            }
            return res;
        }

        #endregion
        #region get district list
        /// <summary>
        /// Gets the district list.
        /// </summary>
        /// <returns></returns>
        public async Task<Response> GetMKADistrictList()
        {
            var res = new Response();
            try
            {
                var districts = await iMKADistrictRepository.GetMKADistrictList();
                if (districts != null && districts.ListItems != null && districts.ListItems.Count() > 0)
                {

                    res.Data = districts.ListItems;
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
                ErrorLog.Write("DistrictService", "GetDistrictList", ex);
            }
            return res;
        }
        #endregion
        #endregion
    }
}
