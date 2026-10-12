using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using PNGEC.API.Domain.Ward;
using PNGEC.API.Helper;

namespace PNGEC.API.Repository.Ward
{
    public class WardRepository : IWardRepository
    {
        #region variable declaration
        /// <summary>
        /// The connection string
        /// </summary>
        private readonly string connectionString;

        /// <summary>
        /// The Ward
        /// </summary>
        private WardModel Ward = null;

        /// <summary>
        /// The lstWard
        /// </summary>
        private Result<WardModel> result = null;
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="WardRepository"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public WardRepository(string connection)
        {
            connectionString = connection;
        }
        #endregion

        #region repository

        #region Add Update Ward
        /// <summary>
        /// Add/update the Ward.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<WardModel> AddUpdateWard(WardModel model)
        {
            Ward = new WardModel();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.Add_Update_Ward;

                DynamicParameters param = new DynamicParameters();
                param.Add("@WardId", model.WardId);
                param.Add("@WardNumber", model.WardNumber);
                param.Add("@WardName", model.WardName);
                param.Add("@RegionId", model.RegionId);
                param.Add("@ProvinceId", model.ProvinceId);
                param.Add("@DistrictId", model.DistrictId);
                param.Add("@LLGId", model.LLGId);
                param.Add("@Status", model.Status);
                param.Add("@ActionBy", model.ActionBy);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                Ward = await con.QueryFirstOrDefaultAsync<WardModel>(sQuery, param, commandType: CommandType.StoredProcedure);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return Ward;
        }
        #endregion

        #region List Ward
        /// <summary>
        /// List the Ward.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<List<WardModel>> ListWard(int WardId)
        {
            List<WardModel> WardList;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.List_Ward;

                DynamicParameters param = new DynamicParameters();
                param.Add("@WardId", WardId);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                WardList = con.Query<WardModel>(sQuery, param, commandType: CommandType.StoredProcedure).ToList();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return WardList;
        }
        #endregion

        #region get ward list
        /// <summary>
        /// Gets the ward list.
        /// </summary>
        /// <returns></returns>
        public async Task<List<WardModel>> GetWardList()
        {
            List<WardModel> WardList;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.GetWardList;
                DynamicParameters param = new DynamicParameters();
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                WardList = con.Query<WardModel>(sQuery, param, commandType: CommandType.StoredProcedure).ToList();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return WardList;
        }
        #endregion

        #region ward transfer

        /// <summary>
        /// Wards the transfer.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<int> WardTransfer(WardModel model)
        {
            int result = 0;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.Ward_Transfer;

                DynamicParameters param = new DynamicParameters();
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

                result = await con.ExecuteScalarAsync<int>(sQuery, param, commandType: CommandType.StoredProcedure, commandTimeout: 600);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return result;
        }
        #endregion
        #endregion
    }
}
