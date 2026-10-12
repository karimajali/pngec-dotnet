using Dapper;
using PNGEC.API.Domain.GeoStructure;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Helper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace PNGEC.API.Repository.GeoStructure
{
    public class GeoStructureRepository : IGeoStructureRepository
    {
        #region variable declaration
        /// <summary>
        /// The connection string
        /// </summary>
        private readonly string connectionString;

        /// <summary>
        /// The GeoStructure
        /// </summary>
        private GeoStructureModel geoStructure = null;

        /// <summary>
        /// The lstGeoStructure
        /// </summary>
        private Result<GeoStructureModel> result = null;
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="GeoStructureRepository"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public GeoStructureRepository(string connection)
        {
            connectionString = connection;
        }
        #endregion

        #region repository

        #region add update geostructure
        /// <summary>
        /// Add/update the GeoStructure.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<GeoStructureModel> AddUpdateGeoStructure(GeoStructureModel model)
        {
            geoStructure = new GeoStructureModel();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.Add_Update_GeoStructure;

                DynamicParameters param = new DynamicParameters();
                param.Add("@GeoStructureId", model.GeoStructureId);
                param.Add("@GeoStructureName", model.GeoStructureName);
                param.Add("@Sequence", model.Sequence);
                param.Add("@Status", model.Status);
                param.Add("@ActionBy", Globals.ActionBy);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                geoStructure = await con.QueryFirstOrDefaultAsync<GeoStructureModel>(sQuery, param, commandType: CommandType.StoredProcedure);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return geoStructure;
        }

        /// <summary>
        /// Gets the geo list.
        /// </summary>
        /// <returns>
        /// return Geo list
        /// </returns>
        public async Task<GeoListModel> GetGeoList(int userId)
        {
            try
            {
                var geoList = new GeoListModel();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@UserId", userId);

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    var lstGeoStructure = await con.QueryMultipleAsync(StoredProcedure.Get_Geo_List, param, commandType: CommandType.StoredProcedure);
                    if (lstGeoStructure != null)
                    {
                        geoList.RegionList = await lstGeoStructure.ReadAsync<Domain.Region.RegionModel>();
                        geoList.ProvinceList = await lstGeoStructure.ReadAsync<Domain.Province.ProvinceModel>();
                        geoList.DistrictList = await lstGeoStructure.ReadAsync<Domain.District.DistrictModel>();
                        geoList.LLGList = await lstGeoStructure.ReadAsync<Domain.LLG.LLGModel>();
                        geoList.WardList = await lstGeoStructure.ReadAsync<Domain.Ward.WardModel>();
                        geoList.LocationList = await lstGeoStructure.ReadAsync<Domain.Location.LocationModel>();
                        geoList.ClanList = await lstGeoStructure.ReadAsync<Domain.Clan.ClanModel>();
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
                throw;
            }

        }

        #endregion

        #region get geostructure list
        /// <summary>
        /// Get the GeoStructure list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Result<GeoStructureModel>> GetGeoStructureList(PageList model)
        {
            result = new Result<GeoStructureModel>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.Get_All_GeoStructure_List;

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
                    result.ListItems = await lstGeoStructure.ReadAsync<GeoStructureModel>();
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
