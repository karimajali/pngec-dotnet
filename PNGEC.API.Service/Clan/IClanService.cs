using PNGEC.API.Domain.Clan;
using PNGEC.API.Helper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PNGEC.API.Service.Clan
{
    public interface IClanService
    {

        /// <summary>
        /// Delete the clan
        /// </summary>
        /// <param name="clanID">ClanID</param>
        /// <returns></returns>
        Task<Response> DeleteClan(int ClanId);

        /// <summary>
        /// Adds/update the clan
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Response> AddUpdateClan(ClanModel model);

        /// <summary>
        /// List the clan
        /// </summary>
        /// <returns></returns>
        Task<List<ClanModel>> Listclan(int ClanId);

        /// <summary>
        /// Gets the clan list.
        /// </summary>
        /// <returns></returns>
        Task<List<ClanModel>> GetClanList();

        /// <summary>
        /// Gets the clan list.
        /// </summary>
        /// <returns></returns>
        Task<Response> GetGeoClanList();

        /// <summary>
        /// Lists the clan by ward identifier.
        /// </summary>
        /// <param name="wardId">The ward identifier.</param>
        /// <returns></returns>
        Task<List<ClanModel>> ListclanByWardId(int wardId);

        /// <summary>
        /// clans the transfer.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Response> clanTransfer(ClanModel model);
    }
}
