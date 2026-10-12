using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using PNGEC.API.Domain.District;
using PNGEC.API.Helper;

namespace PNGEC.API.Repository.District
{
    public class DistrictRepository : IDistrictRepository
    {
        #region variable declaration
        /// <summary>
        /// The connection string
        /// </summary>
        private readonly string connectionString;

        /// <summary>
        /// The District
        /// </summary>
        private DistrictModel District = null;

        /// <summary>
        /// The lstDistrict
        /// </summary>
        private Result<DistrictModel> result = null;
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="DistrictRepository"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public DistrictRepository(string connection)
        {
            connectionString = connection;
        }
        #endregion

        #region repository

        #region Add Update District
        /// <summary>
        /// Add/update the District.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<DistrictModel> AddUpdateDistrict(DistrictModel model)
        {
            District = new DistrictModel();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.Add_Update_District;

                DynamicParameters param = new DynamicParameters();
                param.Add("@DistrictId", model.DistrictId);
                param.Add("@BatchID", model.BatchID);
                param.Add("@DistrictName", model.DistrictName);
                param.Add("@RegionId", model.RegionId);
                param.Add("@ProvinceId", model.ProvinceId);
                param.Add("@Status", model.Status);
                param.Add("@ActionBy", model.ActionBy);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                District = await con.QueryFirstOrDefaultAsync<DistrictModel>(sQuery, param, commandType: CommandType.StoredProcedure);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return District;
        }
        #endregion

        #region List District
        /// <summary>
        /// List the District.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<List<DistrictModel>> ListDistrict(int DistrictId)
        {
            List<DistrictModel> DistrictList;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.List_District;

                DynamicParameters param = new DynamicParameters();
                param.Add("@DistrictId", DistrictId);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                DistrictList = con.Query<DistrictModel>(sQuery, param, commandType: CommandType.StoredProcedure).ToList();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return DistrictList;
        }
        #endregion

        #region get district list
        /// <summary>
        /// Gets the district list.
        /// </summary>
        /// <returns></returns>
        public async Task<List<DistrictModel>> GetDistrictList()
        {
            List<DistrictModel> DistrictList;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.GetDistrictList;
                DynamicParameters param = new DynamicParameters();
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                DistrictList = con.Query<DistrictModel>(sQuery, param, commandType: CommandType.StoredProcedure).ToList();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return DistrictList;
        }
        #endregion

        #endregion
    }
}
