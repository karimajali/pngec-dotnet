using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using PNGEC.API.Domain.MKAGeoStructure;
using PNGEC.API.Helper;
using System.Reflection;
using PNGEC.API.Domain.Listing;

namespace PNGEC.API.Repository.MKAClan
{
    public class MKAClanRepository : IMKAClanRepository
    {
        #region variable declaration
        /// <summary>
        /// The connection string
        /// </summary>
        private readonly string connectionString;

        /// <summary>
        /// The Clan
        /// </summary>
        private MKAClanModel MKAClan = null;

        /// <summary>
        /// The lstClan
        /// </summary>
        private Result<MKAClanModel> result = null;
        #endregion
        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ClanRepository"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public MKAClanRepository(string connection)
        {
            connectionString = connection;
        }
        #endregion

        #region repository

        #region Add Update Clan
        /// <summary>
        /// Add/update the Clan.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<MKAClanModel> AddUpdateMKAClan(MKAClanModel model)
        {
            MKAClan = new MKAClanModel();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.Add_Update_MKAClan;

                DynamicParameters param = new DynamicParameters();
                param.Add("@ClanId", model.ClanId);
                param.Add("@ClanName", model.ClanName);
                param.Add("@Description", model.Description);
                param.Add("@DistrictId", model.DistrictId);
                param.Add("@LLGId", model.LLGId);
                param.Add("@WardId", model.WardId);
                param.Add("@ActionBy", model.ActionBy);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                MKAClan = await con.QueryFirstOrDefaultAsync<MKAClanModel>(sQuery, param, commandType: CommandType.StoredProcedure);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return MKAClan;
        }
        #endregion
        #region List MKAClan
        /// <summary>
        /// List the MKAClan.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Result<MKAClanModel>> ListMKAClan(int ClanId)
        {
            List<MKAClanModel> ClanList;
            Result<MKAClanModel> result = new Result<MKAClanModel>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.List_MKAClan;

                DynamicParameters param = new DynamicParameters();
                param.Add("@ClanId", ClanId);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                ClanList = con.Query<MKAClanModel>(sQuery, param, commandType: CommandType.StoredProcedure).ToList();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                result.ListItems = ClanList;
            }

            return result;
        }
        #endregion
        #region get Clan list
        /// <summary>
        /// Gets the Clan list.
        /// </summary>
        /// <returns></returns>
        public async Task<List<MKAClanModel>> GetMKAClanList(PageList model)
        {
            List<MKAClanModel> ClanList;
            
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.GetMKAClanList;
                DynamicParameters param = new DynamicParameters();
                param.Add("@KeyWord", model.KeyWord);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                ClanList = con.Query<MKAClanModel>(sQuery, param, commandType: CommandType.StoredProcedure).ToList();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
               
            }
            return ClanList.ToList();
        }
        #endregion

        #endregion
    }
}
