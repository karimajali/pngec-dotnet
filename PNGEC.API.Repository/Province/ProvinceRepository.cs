using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using PNGEC.API.Domain.Province;
using PNGEC.API.Helper;

namespace PNGEC.API.Repository.Province
{
    public class ProvinceRepository : IProvinceRepository
    {
        #region variable declaration
        /// <summary>
        /// The connection string
        /// </summary>
        private readonly string connectionString;

        /// <summary>
        /// The Province
        /// </summary>
        private ProvinceModel Province = null;

        /// <summary>
        /// The lstProvince
        /// </summary>
        private Result<ProvinceModel> result = null;
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ProvinceRepository"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public ProvinceRepository(string connection)
        {
            connectionString = connection;
        }
        #endregion

        #region repository

        #region Add Update Province
        /// <summary>
        /// Add/update the Province.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<ProvinceModel> AddUpdateProvince(ProvinceModel model)
        {
            Province = new ProvinceModel();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.Add_Update_Province;

                DynamicParameters param = new DynamicParameters();
                param.Add("@ProvinceId", model.ProvinceId);
                param.Add("@ProvinceName", model.ProvinceName);
                param.Add("@RegionId", model.RegionId);
                param.Add("@Status", model.Status);
                param.Add("@ActionBy", model.ActionBy);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                Province = await con.QueryFirstOrDefaultAsync<ProvinceModel>(sQuery, param, commandType: CommandType.StoredProcedure);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return Province;
        }
        #endregion

        #region List Province
        /// <summary>
        /// List the Province.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<List<ProvinceModel>> ListProvince(int ProvinceId)
        {
            List<ProvinceModel> ProvinceList;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.List_Province;

                DynamicParameters param = new DynamicParameters();
                param.Add("@ProvinceId", ProvinceId);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                ProvinceList = con.Query<ProvinceModel>(sQuery, param, commandType: CommandType.StoredProcedure).ToList();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return ProvinceList;
        }
        #endregion

        #region get province list
        /// <summary>
        /// Gets the province list.
        /// </summary>
        /// <returns></returns>
        public async Task<List<ProvinceModel>> GetProvinceList()
        {
            List<ProvinceModel> ProvinceList;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.GetProvinceList;

                DynamicParameters param = new DynamicParameters();
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                ProvinceList = con.Query<ProvinceModel>(sQuery, param, commandType: CommandType.StoredProcedure).ToList();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return ProvinceList;
        }

        #endregion

        #endregion
    }
}
