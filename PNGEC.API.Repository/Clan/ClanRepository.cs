using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Dapper;
using PNGEC.API.Domain.Clan;
using PNGEC.API.Helper;

namespace PNGEC.API.Repository.Clan
{
    public class ClanRepository : IClanRepository
    {
        #region variable declaration
        /// <summary>
        /// The connection string
        /// </summary>
        private readonly string connectionString;

        /// <summary>
        /// The Clan
        /// </summary>
        private ClanModel Clan = null;

        /// <summary>
        /// The lstClan
        /// </summary>
        private Result<ClanModel> result = null;
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ClanRepository"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public ClanRepository(string connection)
        {
            connectionString = connection;
        }
        #endregion

        #region repository

        #region add update Clan
        /// <summary>
        /// Add/update the Clan.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<ClanModel> AddUpdateClan(ClanModel model)
        {
            Clan = new ClanModel();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.Add_Update_Clan;

                DynamicParameters param = new DynamicParameters();
                param.Add("@ClanId", model.ClanId);
                param.Add("@ClanName", model.ClanName);
                param.Add("@RegionId", model.RegionId);
                param.Add("@ProvinceId", model.ProvinceId);
                param.Add("@DistrictId", model.DistrictId);
                param.Add("@LLGId", model.LLGId);
                param.Add("@WardId", model.WardId);
                param.Add("@Status", model.Status);
                param.Add("@ActionBy", model.ActionBy);
                param.Add("@Description", model.Description);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                Clan = await con.QueryFirstOrDefaultAsync<ClanModel>(sQuery, param, commandType: CommandType.StoredProcedure);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return Clan;
        }
        #endregion

        #region list Clan
        /// <summary>
        /// List the Clan.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<List<ClanModel>> ListClan(int ClanId)
        {
            List<ClanModel> ClanList;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.List_Clan;

                DynamicParameters param = new DynamicParameters();
                param.Add("@ClanId", ClanId);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                ClanList = con.Query<ClanModel>(sQuery, param, commandType: CommandType.StoredProcedure).ToList();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return ClanList;
        }
        #endregion

        #region Get Clan List
        /// <summary>
        /// Gets the Clan list.
        /// </summary>
        /// <returns></returns>
        public async Task<List<ClanModel>> GetClanList()
        {
            List<ClanModel> ClanList;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.GetClanList;
                DynamicParameters param = new DynamicParameters();
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                ClanList = con.Query<ClanModel>(sQuery, param, commandType: CommandType.StoredProcedure).ToList();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return ClanList;
        }
        #endregion

        #region ListClanByWardId
        /// <summary>
        /// Lists the Clan by ward identifier.
        /// </summary>
        /// <param name="wardId">The ward identifier.</param>
        /// <returns></returns>
        public async Task<List<ClanModel>> ListClanByWardId(int wardId)
        {
            List<ClanModel> ClanList;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.List_Clan_By_WardId;

                DynamicParameters param = new DynamicParameters();
                param.Add("@WardId", wardId);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                ClanList = con.Query<ClanModel>(sQuery, param, commandType: CommandType.StoredProcedure).ToList();
                
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return ClanList;
        }
        #endregion

        #region Clan transfer

        /// <summary>
        /// Clans the transfer.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<int> ClanTransfer(ClanModel model)
        {
            int result = 0;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.Clan_Transfer;

                DynamicParameters param = new DynamicParameters();
                param.Add("@ClanId", model.ClanId);
                param.Add("@WardId", model.WardId);
                param.Add("@LLGId", model.LLGId);
                param.Add("@DistrictId", model.DistrictId);
                param.Add("@ProvinceId", model.ProvinceId);
                param.Add("@RegionId", model.RegionId);
                param.Add("@ActionBy", model.ActionBy);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                result = await con.ExecuteScalarAsync<int>(sQuery, param, commandType: CommandType.StoredProcedure, commandTimeout:600);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return result;
        }

        public async Task<int> DeletebyClanId(int clanID)
        {
            int returnValue = 0;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.Delete_Clan;

                DynamicParameters param = new DynamicParameters();
                param.Add("@ClanId", clanID);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                returnValue = await con.QueryFirstOrDefaultAsync<int>(sQuery, param, commandType: CommandType.StoredProcedure);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return returnValue;
        }
        #endregion
        #endregion
    }
}
