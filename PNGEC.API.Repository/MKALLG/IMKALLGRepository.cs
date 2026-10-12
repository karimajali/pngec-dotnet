using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PNGEC.API.Domain.MKAGeoStructure;
using PNGEC.API.Helper;

namespace PNGEC.API.Repository.MKALLG
{
    public interface IMKALLGRepository
    {
        /// <summary>
        /// Add/update the MKALLG
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<MKALLGModel> AddUpdateMKALLG(MKALLGModel model);

        /// <summary>
        /// List the LLG
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Result<MKALLGModel>> ListMKALLG(int LLGId);

        /// <summary>
        /// Gets the MKALLG list.
        /// </summary>
        /// <returns></returns>
        Task<Result<MKALLGModel>> GetMKALLGList();

        /// <summary>
        /// LLGs the transfer.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<int> MKALLGTransfer(MKALLGModel model);
    }
}
