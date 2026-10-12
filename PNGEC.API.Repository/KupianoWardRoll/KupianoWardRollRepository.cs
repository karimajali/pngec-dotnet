using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using PNGEC.API.Domain.KupianoWardRoll;
using PNGEC.API.Helper;

namespace PNGEC.API.Repository.KupianoWardRoll
{
    public class KupianoWardRollRepository : IKupianoWardRollRepository
    {
        #region variable declaration

        /// <summary>
        /// The connection string
        /// </summary>
        private readonly string connectionString;

        /// <summary>
        /// The result
        /// </summary>
        private Result<KupianoWardRollModel> result = null;
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="KupianoWardRollRepository"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public KupianoWardRollRepository(string connection)
        {
            this.connectionString = connection;
        }
        #endregion

        #region get kupiano ward roll list
        /// <summary>
        /// Gets the kupiano ward roll list.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Result<KupianoWardRollModel>> GetKupianoWardRollList()
        {
            result = new Result<KupianoWardRollModel>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.Get_Kupiano_Ward_Roll_List;

                //DynamicParameters param = new DynamicParameters();

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                var lstKupianoWardRoll = await con.QueryMultipleAsync(sQuery, commandType: CommandType.StoredProcedure);
                if (lstKupianoWardRoll != null)
                {
                    result.ListItems = await lstKupianoWardRoll.ReadAsync<KupianoWardRollModel>();
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                return result;
            }
        }
        #endregion
    }
}
