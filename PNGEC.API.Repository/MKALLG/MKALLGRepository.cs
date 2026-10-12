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

namespace PNGEC.API.Repository.MKALLG
{
    public class MKALLGRepository : IMKALLGRepository
    {
        #region variable declaration
        /// <summary>
        /// The connection string
        /// </summary>
        private readonly string connectionString;

        /// <summary>
        /// The LLG
        /// </summary>
        private MKALLGModel MKALLG = null;

        /// <summary>
        /// The lstLLG
        /// </summary>
        private Result<MKALLGModel> result = null;
        #endregion
        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="LLGRepository"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public MKALLGRepository(string connection)
        {
            connectionString = connection;
        }
        #endregion
        #region repository
        #region add update llg
        /// <summary>
        /// Add/update the LLG.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<MKALLGModel> AddUpdateMKALLG(MKALLGModel model)
        {
            MKALLG = new MKALLGModel();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.Add_Update_MKALLG;

                DynamicParameters param = new DynamicParameters();
                param.Add("@LLGId", model.LLGId);
                param.Add("@LLGName", model.LLGName);
                param.Add("@DistrictId", model.DistrictId);
                param.Add("@Status", model.Status);
                param.Add("@ActionBy", model.ActionBy);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                MKALLG = await con.QueryFirstOrDefaultAsync<MKALLGModel>(sQuery, param, commandType: CommandType.StoredProcedure);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return MKALLG;
        }
        #endregion
        #region list llg
        /// <summary>
        /// List the LLG.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Result<MKALLGModel>> ListMKALLG(int LLGId)
        {
            List<MKALLGModel> LLGList;
            Result<MKALLGModel> result = new Result<MKALLGModel>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.List_MKALLG;

                DynamicParameters param = new DynamicParameters();
                param.Add("@LLGId", LLGId);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                LLGList = con.Query<MKALLGModel>(sQuery, param, commandType: CommandType.StoredProcedure).ToList();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                result.ListItems = LLGList;
            }

            return result;
        }
        #endregion
        #region get llg list
        /// <summary>
        /// Gets the LLG list.
        /// </summary>
        /// <returns></returns>
        public async Task<Result<MKALLGModel>> GetMKALLGList()
        {
            List<MKALLGModel> LLGList;
            Result<MKALLGModel> result = new Result<MKALLGModel>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.GetMKALLGList;
                DynamicParameters param = new DynamicParameters();
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                LLGList = con.Query<MKALLGModel>(sQuery, param, commandType: CommandType.StoredProcedure).ToList();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                result.ListItems = LLGList;
            }

            return result;
        }
        #endregion
        #region llg transfer

        /// <summary>
        /// LLGs the transfer.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<int> MKALLGTransfer(MKALLGModel model)
        {
            int result = 0;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.MKALLG_Transfer;

                DynamicParameters param = new DynamicParameters();
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
