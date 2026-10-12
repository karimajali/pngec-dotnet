using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Domain.MKAGeoStructure;
using PNGEC.API.Helper;
using PNGEC.API.Repository.MKAClan;
using PNGEC.API.Repository.MKADistrict;

namespace PNGEC.API.Service.MKAClan
{
    public class MKAClanService : IMKAClanService
    {
        #region variable declaration
        /// <summary>
        /// The i MKADistrict repository
        /// </summary>
        private readonly IMKAClanRepository iMKAClanRepository;

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
        private const string ModuleName = "MKAClan";
        #endregion
        #region constructor 
        /// <summary>
        /// Initializes a new instance of the <see cref="MKAClanService"/> class.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public MKAClanService(string connection)
        {
            iMKAClanRepository = new MKAClanRepository(connection);
        }
        #endregion
        #region service

        #region add update clan
        /// <summary>
        /// Adds/update the Clan
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Response> AddUpdateMKAClan(MKAClanModel model)
        {
            try
            {
                response = new Response();
                MKAClanModel mkaClan = await iMKAClanRepository.AddUpdateMKAClan(model);

                response.Status = mkaClan.ResponseCode > 0;
                if (mkaClan.ResponseCode == -1)
                {
                    response.Message = ResponseMessage.MKAClanError_ClanName_Exists;
                }
                else if (mkaClan.ResponseCode == -2)
                {
                    response.Message = ResponseMessage.MKAClanError_ClanIdUsed;
                }
                else
                {
                    if (model.ClanId <= 0)
                    {
                        response.Message = string.Format(ResponseMessage.RecordAddSuccess, "MKA Clan");
                    }
                    else
                    {
                        response.Message = string.Format(ResponseMessage.RecordUpdateSuccess, "MKA Clan");
                    }

                    response.Data = mkaClan;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("MKAClanService", "AddUpdateMKAClan", ex);
            }
            return response;
        }
        #endregion
        #region list mkaclan
        /// <summary>
        /// List the District
        /// </summary>
        /// <returns></returns>
        public async Task<Response> ListMKAClan(int ClanId)
        {
            var res = new Response();
            try
            {
                var clans = await iMKAClanRepository.ListMKAClan(ClanId);
                if(clans != null && clans.ListItems != null && clans.ListItems.Count() > 0)
                {
                    res.Data = clans.ListItems;
                    res.Message = "Data Get Successfully";
                    res.Status = true;
                }
                else
                {

                    res.Data = clans.ListItems;
                    res.Message = "Data Get Successfully";
                    res.Status = true;
                }
            
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("ClanService", "ListClan", ex);
            }
            return res;
        }

        #endregion
        #region get clan list
        /// <summary>
        /// Gets the clan list.
        /// </summary>
        /// <returns></returns>
        public async Task<Response> GetMKAClanList(PageList model)
        {
            var res = new Response();
            try
            {
               var clans = await iMKAClanRepository.GetMKAClanList(model);
                if (clans != null && clans.Count() > 0)
                {
                    res.Data = clans;
                    res.Message = "Data Get Successfully";
                    res.Status = true;
                }
                else
                {

                    res.Data = clans;
                    res.Message = "Data Get Successfully";
                    res.Status = true;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("ClanService", "GetClanList", ex);
            }
            return res;
        }
        #endregion
        #endregion
    }
}
