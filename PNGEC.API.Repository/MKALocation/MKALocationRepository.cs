using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using PNGEC.API.Helper;
using PNGEC.API.Domain.MKAGeoStructure;

namespace PNGEC.API.Repository.MKALocation
{
    public class MKALocationRepository : IMKALocationRepository
    {
        #region variable declaration
        /// <summary>
        /// The connection string
        /// </summary>
        private readonly string connectionString;

        /// <summary>
        /// The Location
        /// </summary>
        private MKALocationModel MKALocation = null;

        /// <summary>
        /// The lstLocation
        /// </summary>
        private Result<MKALocationModel> result = null;
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="LocationRepository"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public MKALocationRepository(string connection)
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
        public async Task<MKALocationModel> AddUpdateMKALocation(MKALocationModel model)
        {
            MKALocation = new MKALocationModel();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.Add_Update_MKALocation;

                DynamicParameters param = new DynamicParameters();
                param.Add("@LocationId", model.LocationId);
                param.Add("@LocationName", model.LocationName);
                param.Add("@DistrictId", model.DistrictId);
                param.Add("@LLGId", model.LLGId);
                param.Add("@WardId", model.WardId);
                param.Add("@Status", model.Status);
                param.Add("@ActionBy", model.ActionBy);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                MKALocation = await con.QueryFirstOrDefaultAsync<MKALocationModel>(sQuery, param, commandType: CommandType.StoredProcedure);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return MKALocation;
        }
        #endregion

        #region list location
        /// <summary>
        /// List the Location.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Result<MKALocationModel>> ListMKALocation(int LocationId)
        {
            List<MKALocationModel> MKALocationList;
            Result<MKALocationModel> result = new Result<MKALocationModel>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.List_MKALocation;

                DynamicParameters param = new DynamicParameters();
                param.Add("@LocationId", LocationId);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                MKALocationList = con.Query<MKALocationModel>(sQuery, param, commandType: CommandType.StoredProcedure).ToList();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                result.ListItems = MKALocationList;
            }

            return result;
        }
        #endregion

        #region Get Location List
        /// <summary>
        /// Gets the location list.
        /// </summary>
        /// <returns></returns>
        public async Task<Result<MKALocationModel>> GetMKALocationList()
        {
            List<MKALocationModel> MKALocationList;
            Result<MKALocationModel> result = new Result<MKALocationModel>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.GetMKALocationList;
                DynamicParameters param = new DynamicParameters();
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                MKALocationList = con.Query<MKALocationModel>(sQuery, param, commandType: CommandType.StoredProcedure).ToList();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                result.ListItems = MKALocationList;
            }
            return result;
        }
        #endregion

        #region ListLocationByWardId
        /// <summary>
        /// Lists the location by ward identifier.
        /// </summary>
        /// <param name="wardId">The ward identifier.</param>
        /// <returns></returns>
        public async Task<Result<MKALocationModel>> ListMKALocationByWardId(int wardId)
        {
            List<MKALocationModel> MKALocationList;
            Result<MKALocationModel> result = new Result<MKALocationModel>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.List_MKALocation_By_WardId;

                DynamicParameters param = new DynamicParameters();
                param.Add("@WardId", wardId);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                MKALocationList = con.Query<MKALocationModel>(sQuery, param, commandType: CommandType.StoredProcedure).ToList();

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                result.ListItems = MKALocationList;
            }

            return result;
        }
        #endregion

        #region location transfer

        /// <summary>
        /// Locations the transfer.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<int> MKALocationTransfer(MKALocationModel model)
        {
            int result = 0;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.MKALocation_Transfer;

                DynamicParameters param = new DynamicParameters();
                param.Add("@LocationId", model.LocationId);
                param.Add("@WardId", model.WardId);
                param.Add("@LLGId", model.LLGId);
                param.Add("@DistrictId", model.DistrictId);
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
