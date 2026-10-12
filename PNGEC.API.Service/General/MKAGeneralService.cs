using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PNGEC.API.Domain.Mobile.Voter;
using PNGEC.API.Helper;
using PNGEC.API.Repository.General;
using PNGEC.API.Repository.MKAClan;


namespace PNGEC.API.Service.General
{
    public class MKAGeneralService : IMKAGeneralService
    {
        #region variable declaration
        /// <summary>
        /// The i general repository
        /// </summary>
        private readonly IMKAGeneralRepository iMKAGeneralRepository;

        /// <summary>
        /// The voters
        /// </summary>
        private List<DeviceOpertorModel> deviceOpertor = null;
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="MKAGeneralService"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public MKAGeneralService(string connection)
        {
            iMKAGeneralRepository = new MKAGeneralRepository(connection);
        }
        #endregion

        #region service

        #region get geo list mobile
        /// <summary>
        /// Gets the geo list mobile.
        /// </summary>
        /// <returns>
        /// return geo list
        /// </returns>
        public async Task<Response> GetMKAGeoListMobile()
        {
            Response response = new Response();

            try
            {
                var geolist = await iMKAGeneralRepository.GetMKAGeoListMobile();

                if (geolist != null)
                {
                    response.Data = geolist;
                    response.Status = true;
                }
                else
                {
                    response.Message = ResponseMessage.RecordNotExist;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("MKAGeneralService", "GetMKAGeoListMobile", ex);
            }

            return response;
        }
        #endregion
        #region get clan list
        /// <summary>
        /// Gets the clan list.
        /// </summary>
        /// <returns></returns>
        public async Task<Response> GetMKAClanList()
        {
            var res = new Response();
            try
            {
                var clans = await iMKAGeneralRepository.GetMKAClanList();
                if (clans != null && clans != null && clans.Count() > 0)
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
                res.Message = ex.Message;
                ErrorLog.Write("ClanService", "GetClanList", ex);
            }
            return res;
        }
        #endregion
        #endregion

    }
}
