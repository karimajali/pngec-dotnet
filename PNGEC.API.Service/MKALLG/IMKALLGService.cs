using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PNGEC.API.Domain.MKAGeoStructure;
using PNGEC.API.Helper;

namespace PNGEC.API.Service.MKALLG
{
    public interface IMKALLGService
    {
        /// <summary>
        /// Adds/update the LLG
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Response> AddUpdateMKALLG(MKALLGModel model);

        /// <summary>
        /// List the LLG
        /// </summary>
        /// <returns></returns>
        Task<Response> ListMKALLG(int LLGId);

        /// <summary>
        /// Gets the LLG list.
        /// </summary>
        /// <returns></returns>
        Task<Response> GetMKALLGList();

        /// <summary>
        /// LLGs the transfer.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Response> MKALLGTransfer(MKALLGModel model);
    }
}
