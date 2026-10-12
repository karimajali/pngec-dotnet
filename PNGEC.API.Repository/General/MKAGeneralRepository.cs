using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Newtonsoft.Json;
using PNGEC.API.Domain.Mobile.General;
using PNGEC.API.Helper;


namespace PNGEC.API.Repository.General
{
    public class MKAGeneralRepository : IMKAGeneralRepository
    {
        #region variable declaration
        /// <summary>
        /// The connection string
        /// </summary>
        private readonly string connectionString;
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="MKAGeneralRepository"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public MKAGeneralRepository(string connection)
        {
            connectionString = connection;
        }
        #endregion

        #region repository

        #region get geolist mobile
        /// <summary>
        /// Gets the geo list.
        /// </summary>
        /// <returns>
        /// return Geo list
        /// </returns>
        public async Task<List<MKAGeoListModel>> GetMKAGeoListMobile()
        {
            try
            {
                List<MKAGeoListModel> geoList = new List<MKAGeoListModel>();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    string lstGeoStructure = await con.QueryFirstOrDefaultAsync<string>(StoredProcedure.GetMKAGeoListMobile, commandType: CommandType.StoredProcedure);
                    if (lstGeoStructure != null)
                    {


                        geoList = JsonConvert.DeserializeObject<List<MKAGeoListModel>>(lstGeoStructure);



                    }

                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }

                    return geoList;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Write("MKAGeoStructureRepository", "GetMKAGeoList()", ex);
                return null;
            }
        }
        #endregion

        #region get Clan list
        /// <summary>
        /// Gets the Clan list.
        /// </summary>
        /// <returns></returns>
        public async Task<List<MKAClanModel>> GetMKAClanList()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.GetMKAClanList;
                DynamicParameters param = new DynamicParameters();
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                var ClanList =await con.QueryAsync<MKAClanModel>(sQuery, param, commandType: CommandType.StoredProcedure);
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return ClanList.ToList();
            }
            
        }
        #endregion

        #endregion
    }
}
