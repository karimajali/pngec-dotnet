using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using PNGEC.API.Domain.MKAGeoStructure;
using PNGEC.API.Helper;

namespace PNGEC.API.Repository.MKAWard
{
    public class MKAWardRepository : IMKAWardRepository
    {
        #region variable declaration
        /// <summary>
        /// The connection string
        /// </summary>
        private readonly string connectionString;

        /// <summary>
        /// The Ward
        /// </summary>
        private MKAWardModel MKAWard = null;

        /// <summary>
        /// The lstWard
        /// </summary>
        private Result<MKAWardModel> result = null;
        #endregion
        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="WardRepository"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public MKAWardRepository(string connection)
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
        public async Task<MKAWardModel> AddUpdateMKAWard(MKAWardModel model)
        {
            MKAWard = new MKAWardModel();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.Add_Update_MKAWard;

                DynamicParameters param = new DynamicParameters();
                param.Add("@WardId", model.WardId);
                param.Add("@WardNumber", model.WardNumber);
                param.Add("@WardName", model.WardName);
                param.Add("@DistrictId", model.DistrictId);
                param.Add("@LLGId", model.LLGId);
                param.Add("@Status", model.Status);
                param.Add("@ActionBy", model.ActionBy);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                MKAWard = await con.QueryFirstOrDefaultAsync<MKAWardModel>(sQuery, param, commandType: CommandType.StoredProcedure);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return MKAWard;
        }
        #endregion

        #region List Ward
        /// <summary>
        /// List the Ward.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Result<MKAWardModel>> ListMKAWard(int WardId)
        {
            List<MKAWardModel> WardList;
            Result<MKAWardModel> result = new Result<MKAWardModel>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.List_MKAWard;

                DynamicParameters param = new DynamicParameters();
                param.Add("@WardId", WardId);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                WardList = con.Query<MKAWardModel>(sQuery, param, commandType: CommandType.StoredProcedure).ToList();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                result.ListItems = WardList;
            }

            return result;
        }
        #endregion

        #region get ward list
        /// <summary>
        /// Gets the ward list.
        /// </summary>
        /// <returns></returns>
        public async Task<Result<MKAWardModel>> GetMKAWardList()
        {
            List<MKAWardModel> WardList;
            Result<MKAWardModel> result = new Result<MKAWardModel>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.GetMKAWardList;
                DynamicParameters param = new DynamicParameters();
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                WardList = con.Query<MKAWardModel>(sQuery, param, commandType: CommandType.StoredProcedure).ToList();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                result.ListItems = WardList;
            }
            return result;
        }
        #endregion

        #region ward transfer

        /// <summary>
        /// Wards the transfer.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<int> MKAWardTransfer(MKAWardModel model)
        {
            int result = 0;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.MKAWard_Transfer;

                DynamicParameters param = new DynamicParameters();
                param.Add("@WardId", model.WardId);
                param.Add("@LLGId", model.LLGId);
                param.Add("@DistrictId", model.DistrictId);
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
