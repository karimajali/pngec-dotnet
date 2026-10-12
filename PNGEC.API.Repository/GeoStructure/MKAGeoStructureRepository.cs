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
using PNGEC.API.Repository.MKAGeoStructure;
using PNGEC.API.Domain.Listing;

namespace PNGEC.API.Repository.GeoStructure
{
    public class MKAGeoStructureRepository : IMKAGeoStructureRepository
    {
        #region variable declaration
        /// <summary>
        /// The connection string
        /// </summary>
        private readonly string connectionString;

        /// <summary>
        /// The GeoStructure
        /// </summary>
        private MKAGeoStructureModel geoStructure = null;

        /// <summary>
        /// The lstGeoStructure
        /// </summary>
        private Result<MKAGeoStructureModel> result = null;
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="GeoStructureRepository"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public MKAGeoStructureRepository(string connection)
        {
            connectionString = connection;
        }
        #endregion

        #region repository

       
        /// <summary>
        /// Gets the geo list.
        /// </summary>
        /// <returns>
        /// return Geo list
        /// </returns>
        public async Task<MKAGeoListModel> GetMKAGeoList(int userId)
        {
            try
            {
                var mkageoList = new MKAGeoListModel();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@UserId", userId);

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    var lstMKAGeoStructure = await con.QueryMultipleAsync(StoredProcedure.Get_MKAGeo_List, param, commandType: CommandType.StoredProcedure);
                    if (lstMKAGeoStructure != null)
                    {

                        mkageoList.MKADistrictList = await lstMKAGeoStructure.ReadAsync<MKADistrictModel>();
                        mkageoList.MKALLGList = await lstMKAGeoStructure.ReadAsync<MKALLGModel>();
                        mkageoList.MKAWardList = await lstMKAGeoStructure.ReadAsync<MKAWardModel>();
                        mkageoList.MKALocationList = await lstMKAGeoStructure.ReadAsync<MKALocationModel>();
                        mkageoList.MKAClanList = await lstMKAGeoStructure.ReadAsync<MKAClanModel>();
                    }

                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }

                    return mkageoList;
                }
            }

            catch (Exception ex)
            {
                throw;
            }

        }
        #region get geostructure list
        /// <summary>
        /// Get the GeoStructure list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Result<MKAGeoStructureModel>> GetMKAGeoStructureList(PageList model)
        {
            result = new Result<MKAGeoStructureModel>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.Get_All_GeoMKAStructure_List;

                DynamicParameters param = new DynamicParameters();
                param.Add("@PageNo", model.PageNo);
                param.Add("@PageSize", model.PageSize);
                param.Add("@KeyWord", model.KeyWord);
                param.Add("@SortField", string.IsNullOrWhiteSpace(model.SortField) ? "GeoStructureId" : model.SortField);
                param.Add("@SortOrder", string.IsNullOrWhiteSpace(model.SortOrder) ? "DESC" : model.SortOrder);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                var lstGeoStructure = await con.QueryMultipleAsync(sQuery, param, commandType: CommandType.StoredProcedure);
                if (lstGeoStructure != null)
                {
                    result.ListItems = await lstGeoStructure.ReadAsync<MKAGeoStructureModel>();
                    result.TotalRecordCount = await lstGeoStructure.ReadFirstOrDefaultAsync<int>();
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                return result;
            }
        }
        #endregion
        #endregion

    }
}
