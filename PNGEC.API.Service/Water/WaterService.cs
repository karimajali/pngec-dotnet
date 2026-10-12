using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PNGEC.API.Domain.Mobile.Water;
using PNGEC.API.Helper;
using PNGEC.API.Repository.Water;

namespace PNGEC.API.Service.Water
{
    public class WaterService : IWaterService
    {
        #region variable declaration        
        /// <summary>
        /// The i water repository
        /// </summary>
        private readonly IWaterRepository iWaterRepository;
        #endregion

        #region constructor         
        /// <summary>
        /// Initializes a new instance of the <see cref="WaterService"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public WaterService(string connection)
        {
            iWaterRepository = new WaterRepository(connection);
        }
        #endregion

        #region Methods

        #region GetWaterListMobile        
        /// <summary>
        /// Waters the list mobile.
        /// </summary>
        /// <param name="locationId">The location identifier.</param>
        /// <returns></returns>
        public async Task<Response> WaterListMobile(int locationId)
        {
            var res = new Response();
            try
            {
                res.Data = await iWaterRepository.GetWaterListMobile(locationId);
                res.Status = true;
                res.Message = "Success";
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                res.Data = null;
                res.Status = false;
            }
            return res;
        }
        #endregion

        #endregion
    }
}
