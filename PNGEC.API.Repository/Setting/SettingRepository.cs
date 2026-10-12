using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using PNGEC.API.Domain.Setting;
using PNGEC.API.Helper;

namespace PNGEC.API.Repository.Setting
{
    /// <summary>
    /// setting repository
    /// </summary>
    /// <seealso cref="PNGEC.API.Repository.Setting.ISettingRepository" />
    public class SettingRepository : ISettingRepository
    {
        #region variable declaration
        /// <summary>
        /// The connection string
        /// </summary>
        private readonly string connectionString;

        #endregion

        #region constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingRepository"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public SettingRepository(string connection)
        {
            connectionString = connection;
        }
        #endregion

        #region repository

        /// <summary>
        /// Gets the setting.
        /// </summary>
        /// <returns></returns>
        public async Task<SettingModel> GetSetting()
        {
            SettingModel setting = new SettingModel();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.GetSetting;

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                setting = await con.QueryFirstOrDefaultAsync<SettingModel>(sQuery, commandType: CommandType.StoredProcedure);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return setting;
        }

        /// <summary>
        /// Updates the setting.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<SettingModel> UpdateSetting(SettingModel model)
        {
            SettingModel setting = new SettingModel();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.UpdateSetting;

                DynamicParameters param = new DynamicParameters();
                param.Add("@DCPassword", model.DCPassword);
                param.Add("@DCVersion", model.DCVersion);
                param.Add("@GeoVersion", model.GeoVersion);
                param.Add("@OccupationVersion", model.OccupationVersion);
                param.Add("@IsDCChange", model.IsDCChange);
                param.Add("@IsGeoChange", model.IsGeoChange);
                param.Add("@IsOccupationChange", model.IsOccupationChange);
                param.Add("@ActionBy", model.ActionBy);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                setting = await con.QueryFirstOrDefaultAsync<SettingModel>(sQuery, param, commandType: CommandType.StoredProcedure);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return setting;
        }

        /// <summary>
        /// Updates the setting geo version.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<SettingModel> UpdateSettingGeoVersion(SettingModel model)
        {
            SettingModel setting = new SettingModel();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.UpdateSettingGeoVersion;

                DynamicParameters param = new DynamicParameters();
                param.Add("@ActionBy", model.ActionBy);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                setting = await con.QueryFirstOrDefaultAsync<SettingModel>(sQuery, param, commandType: CommandType.StoredProcedure);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return setting;
        }
        /// <summary>
        /// Updates the setting mka geo version.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<SettingModel> UpdateSettingMKAGeoVersion(SettingModel model)
        {
            SettingModel setting = new SettingModel();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.UpdateSettingMKAGeoVersion;

                DynamicParameters param = new DynamicParameters();
                param.Add("@ActionBy", model.ActionBy);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                setting = await con.QueryFirstOrDefaultAsync<SettingModel>(sQuery, param, commandType: CommandType.StoredProcedure);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return setting;
        }
        #endregion
    }
}
