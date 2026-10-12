using Dapper;
using PNGEC.API.Domain.Report;
using PNGEC.API.Helper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PNGEC.API.Repository.Report
{
    public class ReportsRepository : IReportsRepository
    {
        #region variable declaration
        /// <summary>
        /// The connection string
        /// </summary>
        private readonly string connectionString;

        /// <summary>
        /// The ward status report request model
        /// </summary>
        private WardStatusReportRequestModel wardStatusReportRequestModel = null;

        /// <summary>
        /// The result
        /// </summary>
        private Result<WardStatusReportRequestModel> result = null;
        #endregion

        #region constructor
        public ReportsRepository(string connection)
        {
            connectionString = connection;
        }
        #endregion

        #region repository

        #region get ward Status Report
        /// <summary>
        /// Gets the ward status report.
        /// </summary>
        /// <param name="wardStatusReportRequestModel">The ward status report request model.</param>
        /// <returns></returns>
        public async Task<Result<WardRollBookReortModel>> GetWardStatusReport(WardStatusReportRequestModel wardStatusReportRequestModel)
        {
            Result<WardRollBookReortModel> result = new Result<WardRollBookReortModel>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string squery = StoredProcedure.GetWardStatusReport;

                DynamicParameters param = new DynamicParameters();
                param.Add("@StartDate", wardStatusReportRequestModel.StartDate);
                param.Add("@EndDate", wardStatusReportRequestModel.EndDate);
                param.Add("@ProvinceId", wardStatusReportRequestModel.ProvinceId);
                param.Add("@DistrictId", wardStatusReportRequestModel.DistrictId);
                param.Add("@LLGId", wardStatusReportRequestModel.LLGId);
                param.Add("@WardId", wardStatusReportRequestModel.WardId);
                param.Add("@LocationId", wardStatusReportRequestModel.LocationId);
                param.Add("@MKADistrictId", wardStatusReportRequestModel.MKADistrictId);
                param.Add("@MKALLGId", wardStatusReportRequestModel.MKALLGId);
                param.Add("@MKAWardId", wardStatusReportRequestModel.MKAWardId);
                param.Add("@MKALocationId", wardStatusReportRequestModel.MKALocationId);
                param.Add("@RequestType", wardStatusReportRequestModel.RequestType);
                param.Add("@PageNo", wardStatusReportRequestModel.PageNo);
                param.Add("@PageSize", wardStatusReportRequestModel.PageSize);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                var res = await con.QueryMultipleAsync(squery, param, commandType: CommandType.StoredProcedure);
                if (res != null)
                {
                    result.ListItems = await res.ReadAsync<WardRollBookReortModel>();
                    result.TotalRecordCount = await res.ReadFirstOrDefaultAsync<int>();
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return result;
            }
        }
        #endregion

        #region Get wards number of form11
        /// <summary>
        /// Gets the wards number of form11.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<List<GetWardsNumberOfForm11Response>> GetWardsNumberOfForm11(GetWardsNumberOfForm11Request model)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string squery = StoredProcedure.GetWardsNumberOfForm11;

                DynamicParameters param = new DynamicParameters();
                param.Add("@ProvinceId", model.ProvinceId);
                param.Add("@DistrictId", model.DistrictId);
                param.Add("@LLGId", model.LLGId);
                param.Add("@Percentage", model.Percentage);
                param.Add("@DatabaseName", model.DatabaseName);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                var result = await con.QueryAsync<GetWardsNumberOfForm11Response>(squery, param, commandType: CommandType.StoredProcedure);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return result.ToList();
            }
        }
        #endregion
        #region Get wards number of form11
        /// <summary>
        /// Gets the wards number of form11.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<List<GetWardsNumberOfForm11Response>> GetWardsNumberOfMKAForm11(GetWardsNumberOfForm11Request model)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string squery = StoredProcedure.GetWardsNumberOfMKAForm11;

                DynamicParameters param = new DynamicParameters();
                param.Add("@DistrictId", model.DistrictId);
                param.Add("@LLGId", model.LLGId);
                param.Add("@Percentage", model.Percentage);
                param.Add("@DatabaseName", model.DatabaseName);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                var result = await con.QueryAsync<GetWardsNumberOfForm11Response>(squery, param, commandType: CommandType.StoredProcedure);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return result.ToList();
            }
        }
        #endregion
        #endregion
    }
}
