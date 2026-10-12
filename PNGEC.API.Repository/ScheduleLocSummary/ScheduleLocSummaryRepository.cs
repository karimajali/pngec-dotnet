using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Domain.ScheduleLocSummary;
using PNGEC.API.Helper;

namespace PNGEC.API.Repository.ScheduleLocSummary
{
    public class ScheduleLocSummaryRepository : IScheduleLocSummaryRepository
    {
        #region variable declaration
        /// <summary>
        /// The connection string
        /// </summary>
        private readonly string connectionString;

        /// <summary>
        /// The result
        /// </summary>
        private Result<ScheduleLocSummaryModel> result = null;

        private Result<MKAScheduleLocSummaryModel> mkaresult = null;
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ScheduleLocSummaryRepository"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public ScheduleLocSummaryRepository(string connection)
        {
            this.connectionString = connection;
        }

        #endregion

        #region repository

        #region get roll stat list
        /// <summary>
        /// Gets the roll stat list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Result<ScheduleLocSummaryModel>> GetRollStatList(PageList model)
        {
            result = new Result<ScheduleLocSummaryModel>();

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
            else if (model.GeoName == "Location")
            {
                SortField = "LocationId";
            }
            #endregion

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.Get_All_RollStat_List;

                DynamicParameters param = new DynamicParameters();
                param.Add("@DatabaseName", model.DatabaseName);

                param.Add("@PageNo", model.PageNo);
                param.Add("@PageSize", model.PageSize);
                param.Add("@KeyWord", model.KeyWord);

                param.Add("@SortField", SortField);
                param.Add("@SortOrder", string.IsNullOrWhiteSpace(model.SortOrder) ? "DESC" : model.SortOrder);

                //param.Add("@FromDate", model.FromDate);
                //param.Add("@ToDate", model.ToDate);
                param.Add("@FromDate", model.DateFrom);
                param.Add("@ToDate", model.DateTo);

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
                    result.ListItems = await lstuser.ReadAsync<ScheduleLocSummaryModel>();
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

        #region get mka roll stat list
        /// <summary>
        /// Gets the mka roll stat list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Result<MKAScheduleLocSummaryModel>> GetMKARollStatList(PageList model)
        {
            mkaresult = new Result<MKAScheduleLocSummaryModel>();

            string SortField = "MKADistrictId";

            #region SortField
            if (model.GeoName == "MKADistrict")
            {
                SortField = "MKADistrictId";
            }
            else if (model.GeoName == "MKALLG")
            {
                SortField = "MKALLGId";
            }
            else if (model.GeoName == "MKAWard")
            {
                SortField = "MKAWardId";
            }
            else if (model.GeoName == "MKALocation")
            {
                SortField = "MKALocationId";
            }
            #endregion

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.Get_All_MKA_RollStat_List;

                DynamicParameters param = new DynamicParameters();
                param.Add("@DatabaseName", model.DatabaseName);

                param.Add("@PageNo", model.PageNo);
                param.Add("@PageSize", model.PageSize);
                param.Add("@KeyWord", model.KeyWord);

                param.Add("@SortField", SortField);
                param.Add("@SortOrder", string.IsNullOrWhiteSpace(model.SortOrder) ? "DESC" : model.SortOrder);

                param.Add("@FromDate", model.DateFrom);
                param.Add("@ToDate", model.DateTo);

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
                    mkaresult.ListItems = await lstuser.ReadAsync<MKAScheduleLocSummaryModel>();
                    mkaresult.TotalRecordCount = await lstuser.ReadFirstOrDefaultAsync<int>();
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                return mkaresult;
            }
        }
        #endregion

        #region get latest roll stat list

        /// <summary>
        /// Gets the latest roll stat list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Result<ScheduleLocSummaryModel>> GetLatestRollStatList(PageList model)
        {
            result = new Result<ScheduleLocSummaryModel>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.GetLatestRollStat;

                DynamicParameters param = new DynamicParameters();
                param.Add("@DatabaseName", model.DatabaseName);

                param.Add("@PageNo", model.PageNo);
                param.Add("@PageSize", model.PageSize);

                param.Add("@GeoName", model.GeoName);
                param.Add("@GeoId", model.GeoId);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                var lstuser = await con.QueryMultipleAsync(sQuery, param, commandType: CommandType.StoredProcedure);
                if (lstuser != null)
                {
                    result.ListItems = await lstuser.ReadAsync<ScheduleLocSummaryModel>();
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

        #region staging schedule notification
        /// <summary>
        /// Schedules the notification staging.
        /// </summary>
        /// <returns></returns>
        public async Task<List<ScheduleLocSummaryModel>> ScheduleNotificationStaging()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string squery = StoredProcedure.Schedule_Summary_Staging;

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                var res = await con.QueryAsync<ScheduleLocSummaryModel>(squery, commandType: CommandType.StoredProcedure);
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return res.ToList();
            }
        }
        #endregion

        #region preliminary schedule notification
        /// <summary>
        /// Schedules the notification preliminary.
        /// </summary>
        /// <returns></returns>
        public async Task<List<ScheduleLocSummaryModel>> ScheduleNotificationPreliminary()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string squery = StoredProcedure.Schedule_Summary_Preliminary;

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                var res = await con.QueryAsync<ScheduleLocSummaryModel>(squery, commandType: CommandType.StoredProcedure);
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return res.ToList();
            }
        }
        #endregion

        #region final schedule notification
        /// <summary>
        /// Schedules the notification final.
        /// </summary>
        /// <returns></returns>
        public async Task<List<ScheduleLocSummaryModel>> ScheduleNotificationFinal()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string squery = StoredProcedure.Schedule_Summary_Final;

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                var res = await con.QueryAsync<ScheduleLocSummaryModel>(squery, commandType: CommandType.StoredProcedure);
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return res.ToList();
            }
        }
        #endregion

        #region schedule notification rejected
        /// <summary>
        /// Schedules the notification rejected.
        /// </summary>
        /// <returns></returns>
        public async Task<List<ScheduleLocSummaryRejectedModel>> ScheduleNotificationRejected()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string squery = StoredProcedure.Schedule_Summary_Rejected;

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                var res = await con.QueryAsync<ScheduleLocSummaryRejectedModel>(squery, commandType: CommandType.StoredProcedure);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                return res.ToList();
            }
        }
        #endregion

        #region schedule notification rejectedbyrd
        /// <summary>
        /// Schedules the notification rejected by rd.
        /// </summary>
        /// <returns></returns>
        public async Task<List<ScheduleLocSummaryRejectedByRDModel>> ScheduleNotificationRejectedByRD()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string squery = StoredProcedure.Schedule_Summary_RejectedByRD;

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                var res = await con.QueryAsync<ScheduleLocSummaryRejectedByRDModel>(squery, commandType: CommandType.StoredProcedure);
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return res.ToList();
            }
        }
        #endregion

        #region staging schedule notification

        /// <summary>
        /// Stagings the schedule notification.
        /// </summary>
        /// <returns></returns>
        public async Task<List<ScheduleLocSummaryModel>> StagingScheduleNotification()
        {
            //List<ScheduleLocSummaryModel> stagingList;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.Schedule_Summary_Staging;

                DynamicParameters param = new DynamicParameters();

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                var stagingList = await con.QueryAsync<ScheduleLocSummaryModel>(sQuery, param, commandType: CommandType.StoredProcedure, commandTimeout: 600);
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return stagingList.ToList();
            }
        }
        #endregion

        #region preliminary schedule notification
        /// <summary>
        /// Preliminaries the schedule notification.
        /// </summary>
        /// <returns></returns>
        public async Task<List<ScheduleLocSummaryModel>> PreliminaryScheduleNotification()
        {
            //List<ScheduleLocSummaryModel> preliminaryList;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string squery = StoredProcedure.Schedule_Summary_Preliminary;
                DynamicParameters param = new DynamicParameters();

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                var preliminaryList = await con.QueryAsync<ScheduleLocSummaryModel>(squery, param, commandType: CommandType.StoredProcedure);
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return preliminaryList.ToList();
            }
        }
        #endregion

        #region final schedule notification
        /// <summary>
        /// Finals the schedule notification.
        /// </summary>
        /// <returns></returns>
        public async Task<List<ScheduleLocSummaryModel>> FinalScheduleNotification()
        {
            //List<ScheduleLocSummaryModel> finalList;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string squery = StoredProcedure.Schedule_Summary_Final;
                DynamicParameters param = new DynamicParameters();

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                var finalList = await con.QueryAsync<ScheduleLocSummaryModel>(squery, param, commandType: CommandType.StoredProcedure);
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return finalList.ToList();
            }
        }
        #endregion

        #region schedule notification rejected
        /// <summary>
        /// Rejecteds the schedule notification.
        /// </summary>
        /// <returns></returns>
        public async Task<List<ScheduleLocSummaryRejectedModel>> RejectedScheduleNotification()
        {
            //List<ScheduleLocSummaryRejectedModel> rejectedList;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string squery = StoredProcedure.Schedule_Summary_Rejected;
                DynamicParameters param = new DynamicParameters();

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                var rejectedList = await con.QueryAsync<ScheduleLocSummaryRejectedModel>(squery, param, commandType: CommandType.StoredProcedure);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                return rejectedList.ToList();
            }
        }
        #endregion

        #region schedule notification rejectedbyrd
        /// <summary>
        /// Rejecteds the by rd schedule notification.
        /// </summary>
        /// <returns></returns>
        public async Task<List<ScheduleLocSummaryRejectedByRDModel>> RejectedByRDScheduleNotification()
        {
            //List<ScheduleLocSummaryRejectedByRDModel> rejectedByRDList;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string squery = StoredProcedure.Schedule_Summary_RejectedByRD;
                DynamicParameters param = new DynamicParameters();

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                var rejectedByRDList = await con.QueryAsync<ScheduleLocSummaryRejectedByRDModel>(squery, param, commandType: CommandType.StoredProcedure);
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return rejectedByRDList.ToList();
            }
        }
        #endregion
        #endregion
    }
}
