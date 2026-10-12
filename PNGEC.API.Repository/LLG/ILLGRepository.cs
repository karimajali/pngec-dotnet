using System.Collections.Generic;
using System.Threading.Tasks;
using PNGEC.API.Domain.LLG;

namespace PNGEC.API.Repository.LLG
{
    public interface ILLGRepository
    {
        /// <summary>
        /// Add/update the LLG
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<LLGModel> AddUpdateLLG(LLGModel model);

        /// <summary>
        /// List the LLG
        /// </summary>
        /// <param name="model">The model.</param>
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
        Task<int> LLGTransfer(LLGModel model);
    }
}
