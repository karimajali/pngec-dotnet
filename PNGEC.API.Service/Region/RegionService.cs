using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PNGEC.API.Domain.Region;
using PNGEC.API.Helper;
using PNGEC.API.Repository.Region;

namespace PNGEC.API.Service.Region
{
    public class RegionService : IRegionService
    {
        #region variable declaration
        /// <summary>
        /// The i Region repository
        /// </summary>
        private readonly IRegionRepository iRegionRepository;

        /// <summary>
        /// The response
        /// </summary>
        private Response response = null;

        /// <summary>
        /// The lstresponse
        /// </summary>
        private Response<RegionModel> lstresponse = null;

        /// <summary>
        /// The result
        /// </summary>
        private Result<RegionModel> result = null;

        /// <summary>
        /// The module name
        /// </summary>
        private const string ModuleName = "Region";
        #endregion

        #region constructor 
        /// <summary>
        /// Initializes a new instance of the <see cref="RegionService"/> class.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public RegionService(string connection)
        {
            iRegionRepository = new RegionRepository(connection);
        }
        #endregion

        #region service

        #region add update region
        /// <summary>
        /// Adds/update the Region
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Response> AddUpdateRegion(RegionModel model)
        {
            try
            {
                response = new Response();
                RegionModel region = await iRegionRepository.AddUpdateRegion(model);

                response.Status = region.ResponseCode > 0;
                if (region.ResponseCode == -1)
                {
                    response.Message = ResponseMessage.RegionError_RegionName_Exists;
                }
                else if (region.ResponseCode == -2)
                {
                    response.Message = ResponseMessage.RegionError_RegionIdUsed;
                }
                else
                {
                    if (model.RegionId <= 0)
                    {
                        response.Message = string.Format(ResponseMessage.RecordAddSuccess, "Region");
                    }
                    else
                    {
                        response.Message = string.Format(ResponseMessage.RecordUpdateSuccess, "Region");
                    }

                    response.Data = region;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("RegionService", "AddUpdateRegion", ex);
            }
            return response;
        }
        #endregion

        #region list region
        /// <summary>
        /// List the Region
        /// </summary>
        /// <returns></returns>
        public async Task<List<RegionModel>> ListRegion(int RegionId)
        {
            List<RegionModel> regions = new List<RegionModel>();
            try
            {
                regions = await iRegionRepository.ListRegion(RegionId);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("RegionService", "ListRegion", ex);
            }
            return regions;
        }
        #endregion

        #region get region list
        /// <summary>
        /// Gets the region list.
        /// </summary>
        /// <returns></returns>
        public async Task<List<RegionModel>> GetRegionList()
        {
            List<RegionModel> regions = new List<RegionModel>();
            try
            {
                regions = await iRegionRepository.GetRegionList();
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("RegionService", "GetRegionList", ex);
            }
            return regions;
        }

        #endregion

        #endregion
    }
}
