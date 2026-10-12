using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using PNGEC.API.Domain.LLG;
using PNGEC.API.Helper;

namespace PNGEC.API.Repository.LLG
{
    public class LLGRepository : ILLGRepository
    {
        #region variable declaration
        /// <summary>
        /// The connection string
        /// </summary>
        private readonly string connectionString;

        /// <summary>
        /// The LLG
        /// </summary>
        private LLGModel LLG = null;

        /// <summary>
        /// The lstLLG
        /// </summary>
        private Result<LLGModel> result = null;
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="LLGRepository"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public LLGRepository(string connection)
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
        public async Task<LLGModel> AddUpdateLLG(LLGModel model)
        {
            LLG = new LLGModel();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.Add_Update_LLG;

                DynamicParameters param = new DynamicParameters();
                param.Add("@LLGId", model.LLGId);
                param.Add("@LLGName", model.LLGName);
                param.Add("@RegionId", model.RegionId);
                param.Add("@ProvinceId", model.ProvinceId);
                param.Add("@DistrictId", model.DistrictId);
                param.Add("@Status", model.Status);
                param.Add("@ActionBy", model.ActionBy);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                LLG = await con.QueryFirstOrDefaultAsync<LLGModel>(sQuery, param, commandType: CommandType.StoredProcedure);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return LLG;
        }
        #endregion

        #region list llg
        /// <summary>
        /// List the LLG.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<List<LLGModel>> ListLLG(int LLGId)
        {
            List<LLGModel> LLGList;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.List_LLG;

                DynamicParameters param = new DynamicParameters();
                param.Add("@LLGId", LLGId);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                LLGList = con.Query<LLGModel>(sQuery, param, commandType: CommandType.StoredProcedure).ToList();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return LLGList;
        }
        #endregion

        #region get llg list
        /// <summary>
        /// Gets the LLG list.
        /// </summary>
        /// <returns></returns>
        public async Task<List<LLGModel>> GetLLGList()
        {
            List<LLGModel> LLGList;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.GetLLGList;
                DynamicParameters param = new DynamicParameters();
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                LLGList = con.Query<LLGModel>(sQuery, param, commandType: CommandType.StoredProcedure).ToList();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return LLGList;
        }
        #endregion

        #region llg transfer

        /// <summary>
        /// LLGs the transfer.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<int> LLGTransfer(LLGModel model)
        {
            int result = 0;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.LLG_Transfer;

                DynamicParameters param = new DynamicParameters();
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
