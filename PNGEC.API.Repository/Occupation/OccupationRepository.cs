using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Domain.Mobile.General;
using PNGEC.API.Helper;

namespace PNGEC.API.Repository.Occupation
{
    /// <summary>
    /// occupation repository
    /// </summary>
    /// <seealso cref="PNGEC.API.Repository.Occupation.IOccupationRepository" />
    public class OccupationRepository : IOccupationRepository
    {
        #region variable declaration
        /// <summary>
        /// The connection string
        /// </summary>
        private readonly string connectionString;

        #endregion

        #region constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="OccupationRepository"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public OccupationRepository(string connection)
        {
            connectionString = connection;
        }
        #endregion

        #region repository
        /// <summary>
        /// Gets the occupation list.
        /// </summary>
        /// <returns>return list of occupation</returns>
        public async Task<List<OccupationModel>> GetOccupationList(PageList model)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string squery = StoredProcedure.GetOccupationList;
                DynamicParameters param = new DynamicParameters();
                param.Add("@KeyWord", model.KeyWord);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                var res = await con.QueryAsync<OccupationModel>(squery, param, commandType: CommandType.StoredProcedure);
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return res.ToList();
            }
        }

        /// <summary>
        /// Adds the update occupation.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// return added or updated occupation
        /// </returns>
        public async Task<OccupationModel> AddUpdateOccupation(OccupationModel model)
        {
            OccupationModel occupation = new OccupationModel();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.AddUpdateOccupation;

                DynamicParameters param = new DynamicParameters();
                param.Add("@OccupationPK", model.OccupationPK);
                param.Add("@OccupationName", model.OccupationName);
                param.Add("@ActionBy", model.ActionBy);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                occupation = await con.QueryFirstOrDefaultAsync<OccupationModel>(sQuery, param, commandType: CommandType.StoredProcedure);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return occupation;
        }
        #endregion
    }
}
