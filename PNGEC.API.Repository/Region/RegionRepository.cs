using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using PNGEC.API.Domain.Region;
using PNGEC.API.Helper;

namespace PNGEC.API.Repository.Region
{
    public class RegionRepository : IRegionRepository
    {
        #region variable declaration
        /// <summary>
        /// The connection string
        /// </summary>
        private readonly string connectionString;

        /// <summary>
        /// The Region
        /// </summary>
        private RegionModel Region = null;

        /// <summary>
        /// The lstRegion
        /// </summary>
        private Result<RegionModel> result = null;
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="RegionRepository"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public RegionRepository(string connection)
        {
            connectionString = connection;
        }
        #endregion

        #region repository

        #region Add Update Region
        /// <summary>
        /// Add/update the Region.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<RegionModel> AddUpdateRegion(RegionModel model)
        {
            Region = new RegionModel();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.Add_Update_Region;

                DynamicParameters param = new DynamicParameters();
                param.Add("@RegionId", model.RegionId);
                param.Add("@RegionName", model.RegionName);
                param.Add("@Status", model.Status);
                param.Add("@ActionBy", model.ActionBy);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                Region = await con.QueryFirstOrDefaultAsync<RegionModel>(sQuery, param, commandType: CommandType.StoredProcedure);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return Region;
        }
        #endregion

        #region List Region
        /// <summary>
        /// List the Region.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<List<RegionModel>> ListRegion(int RegionId)
        {
            List<RegionModel> regionList;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.List_Region;

                DynamicParameters param = new DynamicParameters();
                param.Add("@RegionId", RegionId);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                regionList = con.Query<RegionModel>(sQuery, param, commandType: CommandType.StoredProcedure).ToList();

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return regionList;
        }
        #endregion

        #region get region list
        /// <summary>
        /// Gets the region list.
        /// </summary>
        /// <returns></returns>
        public async Task<List<RegionModel>> GetRegionList()
        {
            List<RegionModel> regionList;
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string sQuery = StoredProcedure.GetRegionList;
                    DynamicParameters param = new DynamicParameters();
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    regionList = con.Query<RegionModel>(sQuery, param, commandType: CommandType.StoredProcedure).ToList();
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }
            catch (System.Exception ex)
            {
                regionList = new List<RegionModel>();
                regionList.Add(new RegionModel { ResponseCode = 500, StatusName = ex.Message });
            }
            return regionList;
        }
        #endregion

        #endregion
    }
}
