using System.Collections.Generic;
using System.Threading.Tasks;
using PNGEC.API.Domain.LLG;
using PNGEC.API.Helper;

namespace PNGEC.API.Service.LLG
{
    public interface ILLGService
    {
        /// <summary>
        /// Adds/update the LLG
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Response> AddUpdateLLG(LLGModel model);

        /// <summary>
        /// List the LLG
        /// </summary>
        /// <returns></returns>
        Task<List<LLGModel>> ListLLG(int LLGId);

        /// <summary>
        /// Gets the LLG list.
        /// </summary>
        /// <returns></returns>
        Task<List<LLGModel>> GetLLGList();

        /// <summary>
        /// LLGs the transfer.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Response> LLGTransfer(LLGModel model);
    }
}
