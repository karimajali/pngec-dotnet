using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using PNGEC.API.Domain.Location;
using PNGEC.API.Helper;

namespace PNGEC.API.Repository.Location
{
    public class LocationRepository : ILocationRepository
    {
        #region variable declaration
        /// <summary>
        /// The connection string
        /// </summary>
        private readonly string connectionString;

        /// <summary>
        /// The Location
        /// </summary>
        private LocationModel Location = null;

        /// <summary>
        /// The lstLocation
        /// </summary>
        private Result<LocationModel> result = null;
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="LocationRepository"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public LocationRepository(string connection)
        {
            connectionString = connection;
        }
        #endregion

        #region repository

        #region add update location
        /// <summary>
        /// Add/update the Location.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<LocationModel> AddUpdateLocation(LocationModel model)
        {
            Location = new LocationModel();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.Add_Update_Location;

                DynamicParameters param = new DynamicParameters();
                param.Add("@LocationId", model.LocationId);
                param.Add("@LocationName", model.LocationName);
                param.Add("@RegionId", model.RegionId);
                param.Add("@ProvinceId", model.ProvinceId);
                param.Add("@DistrictId", model.DistrictId);
                param.Add("@LLGId", model.LLGId);
                param.Add("@WardId", model.WardId);
                param.Add("@Status", model.Status);
                param.Add("@ActionBy", model.ActionBy);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                Location = await con.QueryFirstOrDefaultAsync<LocationModel>(sQuery, param, commandType: CommandType.StoredProcedure);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return Location;
        }
        #endregion

        #region list location
        /// <summary>
        /// List the Location.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<List<LocationModel>> ListLocation(int LocationId)
        {
            List<LocationModel> LocationList;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.List_Location;

                DynamicParameters param = new DynamicParameters();
                param.Add("@LocationId", LocationId);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                LocationList = con.Query<LocationModel>(sQuery, param, commandType: CommandType.StoredProcedure).ToList();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return LocationList;
        }
        #endregion

        #region Get Location List
        /// <summary>
        /// Gets the location list.
        /// </summary>
        /// <returns></returns>
        public async Task<List<LocationModel>> GetLocationList()
        {
            List<LocationModel> LocationList;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.GetLocationList;
                DynamicParameters param = new DynamicParameters();
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                LocationList = con.Query<LocationModel>(sQuery, param, commandType: CommandType.StoredProcedure).ToList();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return LocationList;
        }
        #endregion

        #region ListLocationByWardId
        /// <summary>
        /// Lists the location by ward identifier.
        /// </summary>
        /// <param name="wardId">The ward identifier.</param>
        /// <returns></returns>
        public async Task<List<LocationModel>> ListLocationByWardId(int wardId)
        {
            List<LocationModel> LocationList;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.List_Location_By_WardId;

                DynamicParameters param = new DynamicParameters();
                param.Add("@WardId", wardId);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                LocationList = con.Query<LocationModel>(sQuery, param, commandType: CommandType.StoredProcedure).ToList();
                
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return LocationList;
        }
        #endregion

        #region location transfer

        /// <summary>
        /// Locations the transfer.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<int> LocationTransfer(LocationModel model)
        {
            int result = 0;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.Location_Transfer;

                DynamicParameters param = new DynamicParameters();
                param.Add("@LocationId", model.LocationId);
                param.Add("@WardId", model.WardId);
                param.Add("@LLGId", model.LLGId);
                param.Add("@DistrictId", model.DistrictId);
                param.Add("@ProvinceId", model.ProvinceId);
                param.Add("@RegionId", model.RegionId);
                param.Add("@ActionBy", model.ActionBy);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                result = await con.ExecuteScalarAsync<int>(sQuery, param, commandType: CommandType.StoredProcedure, commandTimeout:600);

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
