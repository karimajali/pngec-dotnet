using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Dapper;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Domain.Mobile.General;
using PNGEC.API.Domain.Writ;
using PNGEC.API.Helper;

namespace PNGEC.API.Repository.Writ
{
    public class WritRORepository : IWritRORepository
    {
        #region variable declaration
        /// <summary>
        /// The connection string
        /// </summary>
        private readonly string connectionString;

        /// <summary>
        /// The Writ
        /// </summary>
        private WritRO Writ = null;

        /// <summary>
        /// The lstWrit
        /// </summary>
        private Result<WritRO> result = null;
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="WritRepository"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public WritRORepository(string connection)
        {
            connectionString = connection;
        }
        #endregion

        #region repository

        #region add update Writ RO
        /// <summary>
        /// Add/update the Writ RO
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<WritRO> AddUpdateWritRO(WritRO model)
        {
            Writ = new WritRO();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.Add_Update_Writ_RO;

                DynamicParameters param = new DynamicParameters();
                param.Add("@ElectionWritId", model.ElectionWritId);
                param.Add("@ProvinceId", model.ProvinceId);
                param.Add("@ROName", model.ROName);
                param.Add("@WritROId", model.WritROId);              

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                Writ = await con.QueryFirstOrDefaultAsync<WritRO>(sQuery, param, commandType: CommandType.StoredProcedure);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return Writ;
        }
        #endregion


        #region Get Writ RO List
        /// <summary>
        /// Gets the Writ RO Name list.
        /// </summary>
        /// <returns></returns>
        public async Task<List<WritRO>> GetWritROList(PageList model)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string squery = StoredProcedure.GetWritROList;
                DynamicParameters param = new DynamicParameters();
                param.Add("@ElectionWritId", model.ElectionWritId);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                var res = await con.QueryAsync<WritRO>(squery, param, commandType: CommandType.StoredProcedure);
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return res.ToList();
            }
        }

        #endregion


        #endregion

    }
}
