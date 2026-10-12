using PNGEC.API.Domain.Clan;
using PNGEC.API.Helper;
using PNGEC.API.Repository.Clan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PNGEC.API.Service.Clan
{
    public class ClanService : IClanService
    {
        #region variable declaration
        /// <summary>
        /// The i Clan repository
        /// </summary>
        private readonly IClanRepository iClanRepository;

        /// <summary>
        /// The response
        /// </summary>
        private Response response = null;

        /// <summary>
        /// The lstresponse
        /// </summary>
        private Response<ClanModel> lstresponse = null;

        /// <summary>
        /// The result
        /// </summary>
        private Result<ClanModel> result = null;

        /// <summary>
        /// The module name
        /// </summary>
        private const string ModuleName = "Clan";
        #endregion

        #region constructor 
        /// <summary>
        /// Initializes a new instance of the <see cref="ClanService"/> class.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public ClanService(string connection)
        {
            iClanRepository = new ClanRepository(connection);
        }
        #endregion

        #region service

        #region add update Clan
        /// <summary>
        /// Adds/update the Clan
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Response> AddUpdateClan(ClanModel model)
        {
            try
            {
                response = new Response();
                ClanModel Clan = await iClanRepository.AddUpdateClan(model);

                response.Status = Clan.ResponseCode > 0;
                if (Clan.ResponseCode == -1)
                {
                    response.Message = ResponseMessage.ClanError_ClanName_Exists;
                }
                else if (Clan.ResponseCode == -2)
                {
                    response.Message = ResponseMessage.ClanError_ClanIdUsed;
                }
                else
                {
                    if (model.ClanId <= 0)
                    {
                        response.Message = string.Format(ResponseMessage.RecordAddSuccess, "Clan");
                    }
                    else
                    {
                        response.Message = string.Format(ResponseMessage.RecordUpdateSuccess, "Clan");
                    }

                    response.Data = Clan;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("ClanService", "AddUpdateClan", ex);
            }
            return response;
        }
        #endregion

        #region list Clan
        /// <summary>
        /// List the Clan
        /// </summary>
        /// <returns></returns>
        public async Task<List<ClanModel>> Listclan(int ClanId)
        {
            List<ClanModel> Clans = new List<ClanModel>();
            try
            {
                Clans = await iClanRepository.ListClan(ClanId);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("ClanService", "ListClan", ex);
            }
            return Clans;
        }

        #endregion

        #region get Clan list
        /// <summary>
        /// Gets the Clan list.
        /// </summary>
        /// <returns></returns>
        public async Task<List<ClanModel>> GetClanList()
        {
            List<ClanModel> Clans = new List<ClanModel>();
            try
            {
                Clans = await iClanRepository.GetClanList();
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("ClanService", "GetClanList", ex);
            }
            return Clans;
        }
        #endregion

        #region ListClanByWardId
        /// <summary>
        /// Lists the Clan by ward identifier.
        /// </summary>
        /// <param name="wardId">The ward identifier.</param>
        /// <returns></returns>
        public async Task<List<ClanModel>> ListclanByWardId(int wardId)
        {
            List<ClanModel> Clans = new List<ClanModel>();
            try
            {
                Clans = await iClanRepository.ListClanByWardId(wardId);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("ClanService", "ListClanByWardId", ex);
            }
            return Clans;
        }
        #endregion

        #region Clan Transfer

        /// <summary>
        /// Clans the transfer.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Response> clanTransfer(ClanModel model)
        {
            try
            {
                response = new Response();
                int result = await iClanRepository.ClanTransfer(model);

                if (result > 0)
                {
                    response.Status = true;
                    response.Message = ResponseMessage.Clan_Transfer_Success;
                }
                else
                {
                    response.Message = ResponseMessage.Clan_Transfer_Failure;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("ClanService", "ClanTransfer", ex);
            }
            return response;
        }

        public async Task<Response> DeleteClan(int ClanId)
        {
            int deleteClan = 0;
            response = new Response();
            try
            {
                deleteClan = await iClanRepository.DeletebyClanId(ClanId);

                if(deleteClan > 0)
                {
                    response.Message = "Clan deleted successfully.";
                    response.Status = true;
                    response.Data = deleteClan;
                }
                else
                {
                    response.Message = "Somting went Wrong.";
                    response.Status = false;
                    response.Data = 0;

                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("ClanService", "ListClanByWardId", ex);
            }
            return response;
        }

        public async Task<Response> GetGeoClanList()
        {
            var res = new Response();
            try
            {
                var clans = await iClanRepository.GetClanList();
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
