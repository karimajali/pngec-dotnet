using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using PNGEC.API.Domain.MKAGeoStructure;
using PNGEC.API.Helper;
using PNGEC.API.Domain.GeoStructure;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Repository.District;
using PNGEC.API.Domain.Mobile.Voter;

namespace PNGEC.API.Repository.MKADistrict
{
    public class MKADistrictRepository : IMKADistrictRepository
    {
        #region variable declaration
        /// <summary>
        /// The connection string
        /// </summary>
        private readonly string connectionString;

        /// <summary>
        /// The District
        /// </summary>
        private MKADistrictModel MKADistrict = null;

        /// <summary>
        /// The lstDistrict
        /// </summary>
        private Result<MKADistrictModel> result = null;
    #endregion
        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="DistrictRepository"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public MKADistrictRepository(string connection)
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
        public async Task<MKADistrictModel> AddUpdateMKADistrict(MKADistrictModel model)
        {
            MKADistrict = new MKADistrictModel();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.Add_Update_MKADistrict;

                DynamicParameters param = new DynamicParameters();
                param.Add("@DistrictId", model.DistrictId);
                param.Add("@DistrictName", model.DistrictName);
                param.Add("@Status", model.Status);
                param.Add("@ActionBy", model.ActionBy);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                MKADistrict = await con.QueryFirstOrDefaultAsync<MKADistrictModel>(sQuery, param, commandType: CommandType.StoredProcedure);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return MKADistrict;
        }
        #endregion
        #region List MKADistrict
        /// <summary>
        /// List the MKADistrict.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Result<MKADistrictModel>> ListMKADistrict(int DistrictId)
        {
            List<MKADistrictModel> DistrictList;
            Result<MKADistrictModel> result = new Result<MKADistrictModel>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.List_MKADistrict;

                DynamicParameters param = new DynamicParameters();
                param.Add("@DistrictId", DistrictId);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                DistrictList = con.Query<MKADistrictModel>(sQuery, param, commandType: CommandType.StoredProcedure).ToList();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                result.ListItems = DistrictList;
                

            }

            return result;
        }
        #endregion
        #region get district list
        /// <summary>
        /// Gets the district list.
        /// </summary>
        /// <returns></returns>
        public async Task<Result<MKADistrictModel>> GetMKADistrictList()
        {
            List<MKADistrictModel> DistrictList;
            Result<MKADistrictModel> result = new Result<MKADistrictModel>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.GetMKADistrictList;
                DynamicParameters param = new DynamicParameters();
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                DistrictList = con.Query<MKADistrictModel>(sQuery, param, commandType: CommandType.StoredProcedure).ToList();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                result.ListItems = DistrictList;
            }
            return result;
        }
        #endregion

        #endregion
    }
}
