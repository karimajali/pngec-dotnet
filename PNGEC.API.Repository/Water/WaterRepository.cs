using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using PNGEC.API.Domain.Mobile.Water;
using PNGEC.API.Helper;

namespace PNGEC.API.Repository.Water
{
    public class WaterRepository : IWaterRepository
    {
        #region variable        
        /// <summary>
        /// The connection string
        /// </summary>
        private readonly string connectionString;
        #endregion

        #region constructor        
        /// <summary>
        /// Initializes a new instance of the <see cref="WaterRepository"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public WaterRepository(string connection)
        {
            connectionString = connection;
        }
        #endregion

        #region Repository     

        #region GetWaterListForMobile
        /// <summary>
        /// Gets the water list mobile.
        /// </summary>
        /// <param name="locationId">The location identifier.</param>
        /// <returns></returns>
        public async Task<List<WaterModel>> GetWaterListMobile(int locationId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string squery = StoredProcedure.Mobile_Water_List;

                DynamicParameters param = new DynamicParameters();
                param.Add("@LocationId", locationId);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                var res = await con.QueryAsync<WaterModel>(squery, param, commandType: CommandType.StoredProcedure);
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
