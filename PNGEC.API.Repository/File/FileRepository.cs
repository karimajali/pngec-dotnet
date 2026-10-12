using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Domain.Mobile.Voter;
using PNGEC.API.Domain.Report;
using PNGEC.API.Helper;

namespace PNGEC.API.Repository.File
{
    public class FileRepository : IFileRepository
    {
        #region variable declaration
        /// <summary>
        /// The connection string
        /// </summary>
        private readonly string connectionString;

        /// <summary>
        /// The result
        /// </summary>
        private Result<VoterModel> result = null;
        // <summary>
        /// The result
        /// </summary>
        private Result<MKAVoterModel> resultvoter = null;

        /// <summary>
        /// The result ward roll book
        /// </summary>
        private Result<WardRollBookReortModel> resultWardRollBook = null;

        /// <summary>
        /// The result ward roll book
        /// </summary>
        private Result<WardRollBookCsvReportModel> resultWardRollBookCsv = null;

        /// <summary>
        /// The resultmkawardclansectionsummary
        /// </summary>
        private Result<GetMKAWardClanSectionSummaryModel> resultmkawardclansectionsummary = null;

        /// <summary>
        /// The resultwardclansectionsummary
        /// </summary>
        private Result<GetWardClanSectionSummaryModel> resultwardclansectionsummary = null;
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="FileRepository"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public FileRepository(string connection)
        {
            connectionString = connection;
        }
        #endregion

        #region repository

        #region get voter list
        /// <summary>
        /// Gets the voter list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Result<VoterModel>> GetVoterList(PageList model)
        {
            result = new Result<VoterModel>();
            if (!string.IsNullOrEmpty(model.FName))
            {
                model.FName = model.FName.Replace("'", "''");
            }
            if (!string.IsNullOrEmpty(model.LName))
            {
                model.LName = model.LName.Replace("'", "''");
            }


            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = string.Empty;
                if (model.PageName == "TransferOut")
                {
                    sQuery = StoredProcedure.Get_All_Voter_TransferOut_List;
                }
                else if (model.PageName == "Duplicate")
                {
                    sQuery = StoredProcedure.Get_All_Voter_Duplicate_List;
                }
                else if (model.PageName == "TransferInApproval")
                {
                    sQuery = StoredProcedure.GetTranferinVoterWithCondition;
                }
                else if (model.PageName == "WardStatus" || model.PageName == "FieldTallySummarySheet")
                {
                    sQuery = StoredProcedure.Get_All_WardStatus_List;
                }
                else if (model.PageName == "DataMigrationReport")
                {
                    sQuery = StoredProcedure.GetAllDataMigrationReportList;
                }
                else 
                {
                    sQuery = StoredProcedure.Get_All_Voter_List;
                }

                DynamicParameters param = new DynamicParameters();
                param.Add("@DatabaseName", model.DatabaseName);

                param.Add("@PageNo", model.PageNo);
                param.Add("@PageSize", model.PageSize);
                param.Add("@KeyWord", model.KeyWord);
                param.Add("@SortField", string.IsNullOrWhiteSpace(model.SortField) ? "ElectorId" : model.SortField);
                param.Add("@SortOrder", string.IsNullOrWhiteSpace(model.SortOrder) ? "DESC" : model.SortOrder);

                if (model.PageName == "WardStatus" || model.PageName == "FieldTallySummarySheet")
                {
                    param.Add("@FromDate", model.DateFrom);
                    param.Add("@ToDate", model.DateTo);
                }

                param.Add("@FiledName", model.FiledName);
                param.Add("@FiledValue", model.FiledValue);

                param.Add("@FName", model.FName);
                param.Add("@LName", model.LName);
                param.Add("@Gender", model.Gender);
                param.Add("@ClanId", model.ClanId);

                param.Add("@ApprovalStatus", model.ApprovalStatus);

                param.Add("@GeoName", model.GeoName);
                param.Add("@GeoId", model.GeoId);
                //param.Add("@GeoClanId", model.GeoClanId);
                

                param.Add("@LoggedUserId", model.LoggedUserId);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                var queryTimeoutInSeconds = 1800;
                var lstuser = await con.QueryMultipleAsync(sQuery, param, commandType: CommandType.StoredProcedure, commandTimeout: queryTimeoutInSeconds);

                if (lstuser != null)
                {
                    result.ListItems = await lstuser.ReadAsync<VoterModel>();
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

        #region get mka voter list
        /// <summary>
        /// Gets the voter list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Result<MKAVoterModel>> GetMKAVoterList(PageList model)
        {
            resultvoter = new Result<MKAVoterModel>();
            if (!string.IsNullOrEmpty(model.FName))
            {
                model.FName = model.FName.Replace("'", "''");
            }
            if (!string.IsNullOrEmpty(model.LName))
            {
                model.LName = model.LName.Replace("'", "''");
            }


            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = string.Empty;
                if (model.PageName == "TransferOut")
                {
                    sQuery = StoredProcedure.Get_All_Voter_TransferOut_List;
                }
                else if (model.PageName == "Duplicate")
                {
                    sQuery = StoredProcedure.Get_All_Voter_Duplicate_List;
                }
                else if (model.PageName == "TransferInApproval")
                {
                    sQuery = StoredProcedure.GetTranferinVoterWithCondition;
                }
                else if (model.PageName == "WardStatus" || model.PageName == "FieldTallySummarySheet")
                {
                    sQuery = StoredProcedure.Get_All_WardStatus_List;
                }
                else if (model.PageName == "DataMigrationReport")
                {
                    sQuery = StoredProcedure.GetAllDataMigrationReportList;
                }
                else
                {
                    sQuery = StoredProcedure.Get_All_MKAVoter_List;
                }

                DynamicParameters param = new DynamicParameters();
                param.Add("@DatabaseName", model.DatabaseName);

                param.Add("@PageNo", model.PageNo);
                param.Add("@PageSize", model.PageSize);
                param.Add("@KeyWord", model.KeyWord);
                param.Add("@SortField", string.IsNullOrWhiteSpace(model.SortField) ? "ElectorId" : model.SortField);
                param.Add("@SortOrder", string.IsNullOrWhiteSpace(model.SortOrder) ? "DESC" : model.SortOrder);

                if (model.PageName == "WardStatus" || model.PageName == "FieldTallySummarySheet")
                {
                    param.Add("@FromDate", model.DateFrom);
                    param.Add("@ToDate", model.DateTo);
                }

                param.Add("@FiledName", model.FiledName);
                param.Add("@FiledValue", model.FiledValue);

                param.Add("@FName", model.FName);
                param.Add("@LName", model.LName);
                param.Add("@Gender", model.Gender);
                param.Add("@ApprovalStatus", model.ApprovalStatus);

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
                    resultvoter.ListItems = await lstuser.ReadAsync<MKAVoterModel>();
                    resultvoter.TotalRecordCount = await lstuser.ReadFirstOrDefaultAsync<int>();
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                return resultvoter;
            }
        }
        #endregion

        #region get export records list
        /// <summary>
        /// Gets the export records list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Result<WardRollBookReortModel>> GetExportRecordsList(PageList model)
        {
            resultWardRollBook = new Result<WardRollBookReortModel>();
            if (!string.IsNullOrEmpty(model.FName))
            {
                model.FName = model.FName.Replace("'", "''");
            }
            if (!string.IsNullOrEmpty(model.LName))
            {
                model.LName = model.LName.Replace("'", "''");
            }
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = string.Empty;
                if (model.PageName == "WardRollBook" && model.isReportOrCSV == true)
                {
                    sQuery = StoredProcedure.Get_Export_WardRollStatusCount_Records_List;
                }
                else if(model.PageName == "MKAWardRollBook" && model.isReportOrCSV == true)
                {
                    sQuery = StoredProcedure.Get_MKAExport_WardRollStatusCount_Records_List;                    
                }
                else
                {
                    sQuery = StoredProcedure.Get_Export_Records_List;
                }

                DynamicParameters param = new DynamicParameters();
                param.Add("@DatabaseName", model.DatabaseName);
                param.Add("@PageNo", model.PageNo);
                param.Add("@PageSize", model.PageSize);
                param.Add("@KeyWord", model.KeyWord);
                param.Add("@FiledName", model.FiledName);
                param.Add("@FiledValue", model.FiledValue);

                param.Add("@FName", model.FName);
                param.Add("@LName", model.LName);
                param.Add("@Gender", model.Gender);

                param.Add("@ClanId", model.ClanId);

                param.Add("@Disability", model.Disability);
                param.Add("@Photo", model.Photo);

                param.Add("@DateRange", model.DateRange);
                param.Add("@FromDate", model.DateFrom);
                param.Add("@ToDate", model.DateTo);
                param.Add("@ElectorByIDs", model.ElectorByIDs);

                param.Add("@AlphaSplit", model.AlphaSplit);
                param.Add("@AlphaSplitStart", model.AlphaSplitStart);
                param.Add("@AlphaSplitEnd", model.AlphaSplitEnd);

                param.Add("@GeoName", model.GeoName);
                param.Add("@GeoId", model.GeoId);
                param.Add("@LoggedUserId", model.LoggedUserId);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                var queryTimeoutInSeconds = 1800;
                var result = await con.QueryMultipleAsync(sQuery, param, commandType: CommandType.StoredProcedure, commandTimeout: queryTimeoutInSeconds);
                if (result != null)
                {
                    resultWardRollBook.ListItems = await result.ReadAsync<WardRollBookReortModel>();
                    if (model.PageName != "WardRollBook" && model.PageName != "MKAWardRollBook" && model.isReportOrCSV == false)
                    {
                        resultWardRollBook.TotalRecordCount = await result.ReadFirstOrDefaultAsync<int>();
                    }
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                return resultWardRollBook;
            }
        }
        #endregion

        #region get export csv records list
        /// <summary>
        /// Gets the export CSV records list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Result<WardRollBookCsvReportModel>> GetExportCsvRecordsList(PageList model)
        {
            resultWardRollBookCsv = new Result<WardRollBookCsvReportModel>();

            if (!string.IsNullOrEmpty(model.FName))
            {
                model.FName = model.FName.Replace("'", "''");
            }
            if (!string.IsNullOrEmpty(model.LName))
            {
                model.LName = model.LName.Replace("'", "''");
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.Get_Export_Records_Csv_List;

                DynamicParameters param = new DynamicParameters();
                param.Add("@DatabaseName", model.DatabaseName);
                param.Add("@PageNo", model.PageNo);
                param.Add("@PageSize", model.PageSize);
                param.Add("@KeyWord", model.KeyWord);
                param.Add("@FiledName", model.FiledName);
                param.Add("@FiledValue", model.FiledValue);

                param.Add("@FName", model.FName);
                param.Add("@LName", model.LName);
                param.Add("@Gender", model.Gender);

                param.Add("@ClanId", model.ClanId);

                param.Add("@Disability", model.Disability);
                param.Add("@Photo", model.Photo);

                param.Add("@GeoName", model.GeoName);
                param.Add("@GeoId", model.GeoId);
                param.Add("@LoggedUserId", model.LoggedUserId);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                var result = await con.QueryMultipleAsync(sQuery, param, commandType: CommandType.StoredProcedure);
                if (result != null)
                {
                    resultWardRollBookCsv.ListItems = await result.ReadAsync<WardRollBookCsvReportModel>();
                    resultWardRollBookCsv.TotalRecordCount = await result.ReadFirstOrDefaultAsync<int>();
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                return resultWardRollBookCsv;
            }
        }
        #endregion

        #region get duplicate voter list
        /// <summary>
        /// Gets the duplicate voter list.
        /// </summary>
        /// <param name="voterReqModel">The voter req model.</param>
        /// <returns></returns>
        public async Task<Result<WardRollBookReortModel>> GetDuplicateVoterList(VoterRequestModel voterReqModel)
        {
            Result<WardRollBookReortModel> result = new Result<WardRollBookReortModel>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string squery = StoredProcedure.GetDuplicateVoterList;

                DynamicParameters param = new DynamicParameters();
                param.Add("@DatabaseName", voterReqModel.DatabaseName);
                param.Add("@PageNo", voterReqModel.PageNo);
                param.Add("@PageSize", voterReqModel.PageSize);

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

        #region get tranfer in voter with condition
        /// <summary>
        /// Gets the transfor in voter list.
        /// </summary>
        /// <param name="voterReqModel">The voter req model.</param>
        /// <returns></returns>
        public async Task<Result<WardRollBookReortModel>> GetTranferinVoterWithCondition(PageList model)
        {
            resultWardRollBook = new Result<WardRollBookReortModel>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.GetTranferinVoterWithCondition;

                DynamicParameters param = new DynamicParameters();
                param.Add("@DatabaseName", model.DatabaseName);
                param.Add("@PageNo", model.PageNo);
                param.Add("@PageSize", model.PageSize);
                param.Add("@KeyWord", model.KeyWord);
                param.Add("@FiledName", model.FiledName);
                param.Add("@FiledValue", model.FiledValue);
                param.Add("@GeoName", model.GeoName);
                param.Add("@GeoId", model.GeoId);
                param.Add("@LoggedUserId", model.LoggedUserId);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                var result = await con.QueryMultipleAsync(sQuery, param, commandType: CommandType.StoredProcedure);
                if (result != null)
                {
                    resultWardRollBook.ListItems = await result.ReadAsync<WardRollBookReortModel>();
                    resultWardRollBook.TotalRecordCount = await result.ReadFirstOrDefaultAsync<int>();
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                return resultWardRollBook;
            }
        }
        #endregion

        #region get voter list by province mobile
        /// <summary>
        /// Gets the voter list by province mobile.
        /// </summary>
        /// <param name="voterReqModel">The voter req model.</param>
        /// <returns></returns>
        public async Task<Result<VoterModel>> GetVoterListByProvinceMobile(VoterRequestModel voterReqModel)
        {
            Result<VoterModel> result = new Result<VoterModel>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string squery = StoredProcedure.GetVoterListByProvince;

                DynamicParameters param = new DynamicParameters();
                param.Add("@ProvinceId", voterReqModel.ProvinceId);
                param.Add("@PageNo", voterReqModel.PageNo);
                param.Add("@PageSize", voterReqModel.PageSize);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                var queryTimeoutInSeconds = 1800;
                var res = await con.QueryMultipleAsync(squery, param, commandType: CommandType.StoredProcedure, commandTimeout: queryTimeoutInSeconds);
                if (res != null)
                {
                    result.ListItems = await res.ReadAsync<VoterModel>();
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

        #region download voters file by province
        /// <summary>
        /// Downloads the voters file by province.
        /// </summary>
        /// <param name="voterReqModel">The voter req model.</param>
        /// <returns></returns>
        public async Task<Result<VoterModelOffline>> DownloadVotersFileByProvince(VoterRequestModel voterReqModel)
        {
            Result<VoterModelOffline> result = new Result<VoterModelOffline>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string squery = StoredProcedure.DownloadVotersFileByProvince;

                DynamicParameters param = new DynamicParameters();
                param.Add("@ProvinceId", voterReqModel.ProvinceId);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                var queryTimeoutInSeconds = 1800;
                result.ListItems = await con.QueryAsync< VoterModelOffline>(squery, param, commandType: CommandType.StoredProcedure, commandTimeout: queryTimeoutInSeconds);
                
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return result;
            }
        }
        #endregion

        #region get mka ward clan section summary list
        /// <summary>
        /// Gets the mka ward clan section summary list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Result<GetMKAWardClanSectionSummaryModel>> GetMKAWardClanSectionSummaryList(PageList model)
        {
            resultmkawardclansectionsummary = new Result<GetMKAWardClanSectionSummaryModel>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.GetMKAWardClanSectionSummaryList;

                DynamicParameters param = new DynamicParameters();
                param.Add("@DatabaseName", model.DatabaseName);
                param.Add("@Gender", model.Gender);
                param.Add("@GeoName", model.GeoName);
                param.Add("@GeoId", model.GeoId);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                var lstuser = await con.QueryMultipleAsync(sQuery, param, commandType: CommandType.StoredProcedure);

                if (lstuser != null)
                {
                    resultmkawardclansectionsummary.ListItems = await lstuser.ReadAsync<GetMKAWardClanSectionSummaryModel>();
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                return resultmkawardclansectionsummary;
            }
        }
        #endregion

        #region get ward clan section summary list
        /// <summary>
        /// Gets the ward clan section summary list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Result<GetWardClanSectionSummaryModel>> GetWardClanSectionSummaryList(PageList model)
        {
            resultwardclansectionsummary = new Result<GetWardClanSectionSummaryModel>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.GetWardClanSectionSummaryList;

                DynamicParameters param = new DynamicParameters();
                param.Add("@DatabaseName", model.DatabaseName);

                param.Add("@GeoName", model.GeoName);
                param.Add("@GeoId", model.GeoId);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                var lstuser = await con.QueryMultipleAsync(sQuery, param, commandType: CommandType.StoredProcedure);

                if (lstuser != null)
                {
                    resultwardclansectionsummary.ListItems = await lstuser.ReadAsync<GetWardClanSectionSummaryModel>();
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                return resultwardclansectionsummary;
            }
        }
        #endregion

        #endregion
    }
}
