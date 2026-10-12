using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PNGEC.API.Domain.Mobile.Water;

namespace PNGEC.API.Repository.Water
{
    public interface IWaterRepository
    {

        /// <summary>
        /// Gets the water list mobile.
        /// </summary>
        /// <param name="locationId">The location identifier.</param>
        /// <returns></returns>
        Task<List<WaterModel>> GetWaterListMobile(int locationId);
    }
}
