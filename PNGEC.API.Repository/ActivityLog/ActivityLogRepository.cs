using Dapper;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Domain.User;
using PNGEC.API.Helper;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace PNGEC.API.Repository.ActivityLog
{
    public class ActivityLogRepository : IActivityLogRepository
    {
        #region variable declaration
        /// <summary>
        /// The connection string
        /// </summary>
        private readonly string connectionString;

        /// <summary>
        /// The ActivityLog
        /// </summary>
        private ActivityLogModel ActivityLog = null;

        /// <summary>
        /// The lstActivityLog
        /// </summary>
        private Result<ActivityLogModel> result = null;
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityLogRepository"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public ActivityLogRepository(string connection)
        {
            connectionString = connection;
        }
        #endregion

        #region repository

        #region add ActivityLog
        /// <summary>
        /// Add/update the ActivityLog.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<ActivityLogModel> AddActivityLog(ActivityLogModel model)
        {
            ActivityLog = new ActivityLogModel();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.Add_Activity_Log;

                DynamicParameters param = new DynamicParameters();
                param.Add("@UserID", model.UserID);
                param.Add("@LogSource", model.LogSource);
                param.Add("@Event", model.Event);
                param.Add("@LogDescription", model.LogDescription);              

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                ActivityLog = await con.QueryFirstOrDefaultAsync<ActivityLogModel>(sQuery, param, commandType: CommandType.StoredProcedure);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return ActivityLog;
        }
        #endregion

        #region get ActivityLog list
        /// <summary>
        /// Get the ActivityLog list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Result<ActivityLogModel>> GetActivityLogList(PageList model)
        {
            result = new Result<ActivityLogModel>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.Get_All_Activity_Log;

                DynamicParameters param = new DynamicParameters();
                param.Add("@PageNo", model.PageNo);
                param.Add("@PageSize", model.PageSize);
                param.Add("@KeyWord", model.KeyWord);
                param.Add("@SortField", string.IsNullOrWhiteSpace(model.SortField) ? "LogId" : model.SortField);
                param.Add("@SortOrder", string.IsNullOrWhiteSpace(model.SortOrder) ? "DESC" : model.SortOrder);
                param.Add("@LogSource", model.LogSource);


                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                var lsActivityLog = await con.QueryMultipleAsync(sQuery, param, commandType: CommandType.StoredProcedure);
                if (lsActivityLog != null)
                {
                    result.ListItems = await lsActivityLog.ReadAsync<ActivityLogModel>();
                    result.TotalRecordCount = await lsActivityLog.ReadFirstOrDefaultAsync<int>();
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
