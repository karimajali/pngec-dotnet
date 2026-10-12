using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PNGEC.API.Domain.KupianoWardRoll;
using PNGEC.API.Helper;

namespace PNGEC.API.Service.KupianoWardRoll
{
    public interface IKupianoWardRollService
    {
        /// <summary>
        /// Gets the kupiano ward roll list.
        /// </summary>
        /// <returns></returns>
        Task<Response<KupianoWardRollModel>> GetKupianoWardRollList();
    }
}
