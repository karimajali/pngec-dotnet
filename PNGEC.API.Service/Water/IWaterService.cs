using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PNGEC.API.Helper;

namespace PNGEC.API.Service.Water
{
    public interface IWaterService
    {
        /// <summary>
        /// Waters the list mobile.
        /// </summary>
        /// <param name="locationId">The location identifier.</param>
        /// <returns></returns>
        Task<Response> WaterListMobile(int locationId);
    }
}
