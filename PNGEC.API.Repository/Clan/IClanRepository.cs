using PNGEC.API.Domain.Clan;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PNGEC.API.Repository.Clan
{
    public interface IClanRepository
    {
        /// <summary>
        /// Add/update the Clan
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<ClanModel> AddUpdateClan(ClanModel model);

        /// <summary>
        /// List the Clan
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<List<ClanModel>> ListClan(int Clanid);

        /// <summary>
        /// Gets the Clan list.
        /// </summary>
        /// <returns></returns>
        Task<List<ClanModel>> GetClanList();

        /// <summary>
        /// Lists the Clan by ward identifier.
        /// </summary>
        /// <param name="wardId">The ward identifier.</param>
        /// <returns></returns>
        Task<List<ClanModel>> ListClanByWardId(int wardId);

        /// <summary>
        /// Clans the transfer.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<int> ClanTransfer(ClanModel model);


        /// <summary>
        /// delete clan by ClanID
        /// </summary>
        /// <param name="wardId">The ward identifier.</param>
        /// <returns></returns>
        Task<int> DeletebyClanId(int clanID);

    }
}
