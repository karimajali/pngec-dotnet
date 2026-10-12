using Dapper;
using PNGEC.API.Domain.AugmentedRollStat;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Helper;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PNGEC.API.Repository.AugmentedRollStat
{
    public class AugmentedRollStatRepository : IAugmentedRollStatRepository
    {
        #region variable declaration
        /// <summary>
        /// The connection string
        /// </summary>
        private readonly string connectionString;

        /// <summary>
        /// The result
        /// </summary>
        private Result<AugmentedRollStatModel> result = null;

        /// <summary>
        /// The augmented roll stat model
        /// </summary>
        private AugmentedRollStatModel augmentedRollStatModel = null;
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="AugmentedRollStatRepository"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public AugmentedRollStatRepository(string connection)
        {
            this.connectionString = connection;
        }
        #endregion

        #region repository

        #region get augmented roll stat list
        /// <summary>
        /// Gets the augmented roll stat list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Result<AugmentedRollStatModel>> GetAugmentedRollStatList(PageList model)
        {
            result = new Result<AugmentedRollStatModel>();

            string SortField = "RegionId";

            #region SortField
            if (model.GeoName == "Region")
            {
                SortField = "RegionId";
            }
            else if (model.GeoName == "Province")
            {
                SortField = "ProvinceId";
            }
            else if (model.GeoName == "District")
            {
                SortField = "DistrictId";
            }
            else if (model.GeoName == "LLG")
            {
                SortField = "LLGId";
            }
            else if (model.GeoName == "Ward")
            {
                SortField = "WardId";
            }
            else if (model.GeoName == "Ward")
            {
                SortField = "WardId";
            }
            else if (model.GeoName == "Location")
            {
                SortField = "LocationId";
            }
            #endregion

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.Get_All_Augmented_Roll_Stat_List;

                DynamicParameters param = new DynamicParameters();
                param.Add("@DatabaseName", model.DatabaseName);

                param.Add("@PageNo", model.PageNo);
                param.Add("@PageSize", model.PageSize);
                param.Add("@KeyWord", model.KeyWord);

                param.Add("@SortField", SortField);
                param.Add("@SortOrder", string.IsNullOrWhiteSpace(model.SortOrder) ? "DESC" : model.SortOrder);

                param.Add("@GeoName", model.GeoName);
                param.Add("@GeoId", model.GeoId);

                param.Add("@LoggedUserId", model.LoggedUserId);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                var lstuser = await con.QueryMultipleAsync(sQuery, param, commandType: CommandType.StoredProcedure);
                if (lstuser != null)
                {
                    result.ListItems = await lstuser.ReadAsync<AugmentedRollStatModel>();
                    result.TotalRecordCount = await lstuser.ReadFirstOrDefaultAsync<int>();
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                return result;
            }
        }
        #endregion

        #region add update augmented roll stat
        /// <summary>
        /// Adds the update augmented roll stat.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<AugmentedRollStatModel> AddUpdateAugmentedRollStat(AugmentedRollStatModel model)
        {
            augmentedRollStatModel = new AugmentedRollStatModel();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.Add_Update_AugmentedRollStat;

                DynamicParameters param = new DynamicParameters();
                param.Add("@DatabaseName", model.DatabaseName);
                param.Add("@RegionId", model.RegionId);
                param.Add("@ProvinceId", model.ProvinceId);
                param.Add("@DistrictId", model.DistrictId);
                param.Add("@LLGId", model.LLGId);
                param.Add("@WardId", model.WardId);
                param.Add("@LocationId", model.LocationId);

                param.Add("@OLD", model.OLD);
                param.Add("@NEW", model.NEW);
                param.Add("@UPDATED", model.UPDATED);
                param.Add("@TRANSFERIN", model.TRANSFERIN);
                param.Add("@TRANSFEROUT", model.TRANSFEROUT);
                param.Add("@DECEASED", model.DECEASED);
                param.Add("@DUPLICATE", model.DUPLICATE);
                param.Add("@GHOST", model.GHOST);
                param.Add("@UNDERAGE", model.UNDERAGE);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                var reader = await con.QueryMultipleAsync(sQuery, param, commandType: CommandType.StoredProcedure);
                var existCount = reader.Read<AugmentedRollStatModel>().FirstOrDefault();
                augmentedRollStatModel = reader.Read<AugmentedRollStatModel>().FirstOrDefault();

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return augmentedRollStatModel;
        }
        #endregion

        #endregion
    }
}
