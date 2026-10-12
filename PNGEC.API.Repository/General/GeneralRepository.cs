using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using PNGEC.API.Domain.Mobile.General;
using PNGEC.API.Domain.Mobile.Voter;
using PNGEC.API.Helper;

namespace PNGEC.API.Repository.General
{
    /// <summary>
    /// General repository class
    /// </summary>
    /// <seealso cref="PNGEC.API.Repository.General.IGeneralRepository" />
    public class GeneralRepository : IGeneralRepository
    {
        #region variable declaration
        /// <summary>
        /// The connection string
        /// </summary>
        private readonly string connectionString;
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="GeneralRepository"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public GeneralRepository(string connection)
        {
            connectionString = connection;
        }
        #endregion

        #region repository

        #region get geolist mobile
        /// <summary>
        /// Gets the geo list.
        /// </summary>
        /// <returns>
        /// return Geo list
        /// </returns>
        public async Task<Domain.Mobile.General.GeoListModel> GetGeoListMobile()
        {
            try
            {
                var geoList = new Domain.Mobile.General.GeoListModel();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    var lstGeoStructure = await con.QueryMultipleAsync(StoredProcedure.GetGeoListMobile, commandType: CommandType.StoredProcedure);
                    if (lstGeoStructure != null)
                    {
                        geoList.RegionList = await lstGeoStructure.ReadAsync<Domain.Mobile.General.RegionModel>();
                        geoList.ProvinceList = await lstGeoStructure.ReadAsync<Domain.Mobile.General.ProvinceModel>();
                        geoList.DistrictList = await lstGeoStructure.ReadAsync<Domain.Mobile.General.DistrictModel>();
                        geoList.LLGList = await lstGeoStructure.ReadAsync<Domain.Mobile.General.LLGModel>();
                        geoList.WardList = await lstGeoStructure.ReadAsync<Domain.Mobile.General.WardModel>();
                        geoList.LocationList = new List<LocationModel>();
                        geoList.ClanList = await lstGeoStructure.ReadAsync<Domain.Mobile.General.ClanModel>();
                        //geoList.LocationList = await lstGeoStructure.ReadAsync<Domain.Mobile.General.LocationModel>();
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
                ErrorLog.Write("GeoStructureRepository", "GetGeoList()", ex);
                return null;
            }
        }
        #endregion

        #region get active province list mobile
        /// <summary>
        /// Gets the active province list mobile.
        /// </summary>
        /// <returns></returns>
        public async Task<List<ProvinceModel>> GetActiveProvinceListMobile()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string squery = StoredProcedure.GetActiveProvinceListMobile;

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    var res = await con.QueryAsync<ProvinceModel>(squery, commandType: CommandType.StoredProcedure);
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    return res.ToList();
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Write("GeoStructureRepository", "GetActiveProvinceListMobile()", ex);
                return null;
            }
        }
        #endregion

        #region get occupation list mobile
        /// <summary>
        /// Gets the occupation list mobile.
        /// </summary>
        /// <returns>
        /// return occupation list
        /// </returns>
        public async Task<List<OccupationModel>> GetOccupationListMobile()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string squery = StoredProcedure.GetOccupationListMobile;

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                var res = await con.QueryAsync<OccupationModel>(squery, commandType: CommandType.StoredProcedure);
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return res.ToList();
            }
        }
        #endregion

        #region validate enrolment number
        /// <summary>
        /// Validates the enrolment number.
        /// </summary>
        /// <param name="validateEnrolmentNumberRequestModel">The validate enrolment number request model.</param>
        /// <returns>
        /// return enrolment id if available
        /// </returns>
        public async Task<long> ValidateEnrolmentNumber(ValidateEnrolmentNumberRequestModel validateEnrolmentNumberRequestModel)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string squery = StoredProcedure.ValidateEnrolmentNumber;

                DynamicParameters param = new DynamicParameters();
                param.Add("@EnrolmentFormNumber", validateEnrolmentNumberRequestModel.EnrolmentFormNumber);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                var electorId = await con.QueryFirstOrDefaultAsync<long>(squery, param, commandType: CommandType.StoredProcedure);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                return electorId;
            }
        }
        #endregion

        #region get all locations mobile
        /// <summary>
        /// Gets all locations mobile.
        /// </summary>
        /// <param name="locationRequestModel">The location request model.</param>
        /// <returns>
        /// return location lists
        /// </returns>
        public async Task<Result<LocationModel>> GetAllLocationsMobile(LocationRequestModel locationRequestModel)
        {
            Result<LocationModel> result = new Result<LocationModel>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string squery = StoredProcedure.GetAllLocationsMobile;

                DynamicParameters param = new DynamicParameters();
                param.Add("@PageNo", locationRequestModel.PageNo);
                param.Add("@PageSize", locationRequestModel.PageSize);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                var lstLocation = await con.QueryMultipleAsync(squery, param, commandType: CommandType.StoredProcedure);
                if (lstLocation != null)
                {
                    result.ListItems = await lstLocation.ReadAsync<LocationModel>();
                    result.TotalRecordCount = await lstLocation.ReadFirstOrDefaultAsync<int>();
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                return result;
            }
        }
        #endregion

        #region get locations by ids mobile
        /// <summary>
        /// Gets the locations by ids mobile.
        /// </summary>
        /// <param name="locationRequestModel">The location request model.</param>
        /// <returns>
        /// return location lists
        /// </returns>
        public async Task<List<LocationModel>> GetLocationsByIdsMobile(LocationRequestModel locationRequestModel)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string squery = StoredProcedure.GetLocationsByIdsMobile;

                DynamicParameters param = new DynamicParameters();
                param.Add("@RegionId", locationRequestModel.RegionId);
                param.Add("@ProvinceId", locationRequestModel.ProvinceId);
                param.Add("@DistrictId", locationRequestModel.DistrictId);
                param.Add("@LLGId", locationRequestModel.LLGId);
                param.Add("@WardId", locationRequestModel.WardId);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                var res = await con.QueryAsync<LocationModel>(squery, param, commandType: CommandType.StoredProcedure);
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                return res.ToList();
            }
        }
        #endregion

        #region logout mobile
        /// <summary>
        /// Logouts the mobile.
        /// </summary>
        /// <param name="logoutRequest">The logout request.</param>
        /// <returns>
        /// return user id
        /// </returns>
        public async Task<int> LogoutMobile(LogoutRequest logoutRequest)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string squery = StoredProcedure.LogoutMobile;

                DynamicParameters param = new DynamicParameters();
                param.Add("@UserId", logoutRequest.UserId);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                var userId = await con.QueryFirstOrDefaultAsync<int>(squery, param, commandType: CommandType.StoredProcedure);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                return userId;
            }
        }
        #endregion

        #region update user geo structure mobile
        /// <summary>
        /// Updates the user geo structure.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// Updated row
        /// </returns>
        public async Task<int> UpdateUserGeoStructureMobile(UserModel model)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string squery = StoredProcedure.UpdateUserGeoStructureMobile;

                DynamicParameters param = new DynamicParameters();
                param.Add("@UserId", model.UserId);
                param.Add("@Region", model.Region);
                param.Add("@Province", model.Province);
                param.Add("@District", model.District);
                param.Add("@LLG", model.LLG);
                param.Add("@Ward", model.Ward);
                param.Add("@Location", model.Location);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                var rowUpdated = await con.QueryFirstOrDefaultAsync<int>(squery, param, commandType: CommandType.StoredProcedure);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                return rowUpdated;
            }
        }
        #endregion

        #region GetDeviceOperatorReport
        /// <summary>
        /// Gets the voter.
        /// </summary>
        /// <param name="getDeviceOperatorModel"></param>
        /// <returns></returns>
        public async Task<Result<VoterModel>> GetDeviceOperatorReport(GetDeviceOperatorRequestModel getDeviceOperatorModel)
        {
            Result<VoterModel> result = new Result<VoterModel>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string squery = StoredProcedure.GetDeviceOperatorMobile;

                DynamicParameters param = new DynamicParameters();
                param.Add("@StartDate", getDeviceOperatorModel.StartDate);
                param.Add("@EndDate", getDeviceOperatorModel.EndDate);
                param.Add("@ProvinceId", getDeviceOperatorModel.ProvinceId);
                param.Add("@DistrictId", getDeviceOperatorModel.DistrictId);
                param.Add("@LLGId", getDeviceOperatorModel.LLGId);
                param.Add("@WardId", getDeviceOperatorModel.WardId);
                param.Add("@LocationId", getDeviceOperatorModel.LocationId);
                param.Add("@MKADistrictId", getDeviceOperatorModel.MKADistrictId);
                param.Add("@MKALLGId", getDeviceOperatorModel.MKALLGId);
                param.Add("@MKAWardId", getDeviceOperatorModel.MKAWardId);
                param.Add("@MKALocationId", getDeviceOperatorModel.MKALocationId);
                param.Add("@RequestType", getDeviceOperatorModel.RequestType);
                param.Add("@PageNo", getDeviceOperatorModel.PageNo);
                param.Add("@PageSize", getDeviceOperatorModel.PageSize);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                var res = await con.QueryMultipleAsync(squery, param, commandType: CommandType.StoredProcedure);
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

        #region GetPDFDeviceOperatorReport
        ///// <summary>
        ///// Gets the PDF device operator report.
        ///// </summary>
        ///// <param name="getDeviceOperatorModel">The get device operator model.</param>
        ///// <returns></returns>
        //public async Task<Result<DeviceOpertorModel>> GetPDFDeviceOperatorReport(GetDeviceOperatorRequestModel getDeviceOperatorModel)
        //{
        //    Result<DeviceOpertorModel> result = new Result<DeviceOpertorModel>();
        //    using (SqlConnection con = new SqlConnection(connectionString))
        //    {
        //        string squery = StoredProcedure.GetPDFDeviceOperatorMobile;

        //        DynamicParameters param = new DynamicParameters();
        //        param.Add("@StartDate", getDeviceOperatorModel.StartDate);
        //        param.Add("@EndDate", getDeviceOperatorModel.EndDate);
        //        param.Add("@ProvinceId", getDeviceOperatorModel.ProvinceId);
        //        param.Add("@DistrictId", getDeviceOperatorModel.DistrictId);
        //        param.Add("@LLGId", getDeviceOperatorModel.LLGId);
        //        param.Add("@WardId", getDeviceOperatorModel.WardId);
        //        param.Add("@PageNo", getDeviceOperatorModel.PageNo);
        //        param.Add("@PageSize", getDeviceOperatorModel.PageSize);

        //        if (con.State == ConnectionState.Closed)
        //        {
        //            con.Open();
        //        }
        //        var res = await con.QueryMultipleAsync(squery, param, commandType: CommandType.StoredProcedure);
        //        if (res != null)
        //        {
        //            result.ListItems = await res.ReadAsync<DeviceOpertorModel>();
        //            result.TotalRecordCount = await res.ReadFirstOrDefaultAsync<int>();
        //        }

        //        if (con.State == ConnectionState.Open)
        //        {
        //            con.Close();
        //        }
        //        return result;
        //    }
        //}
        #endregion

        #region Get status and DOB discrepancy report

        public async Task<Result<StatusAndDOBDiscrepancyResponseModel>> GetStatusAndDOBDiscrepancyReport(StatusAndDOBDiscrepancyModel statusAndDOBDiscrepancyModel)
        {
            Result<StatusAndDOBDiscrepancyResponseModel> result = new Result<StatusAndDOBDiscrepancyResponseModel>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string squery = StoredProcedure.GetStatusAndDOBDiscrepancyMobile;

                DynamicParameters param = new DynamicParameters();
                param.Add("@ProvinceId", statusAndDOBDiscrepancyModel.ProvinceId);
                param.Add("@DistrictId", statusAndDOBDiscrepancyModel.DistrictId);
                param.Add("@LLGId", statusAndDOBDiscrepancyModel.LLGId);
                param.Add("@WardId", statusAndDOBDiscrepancyModel.WardId);
                param.Add("@LocationId", statusAndDOBDiscrepancyModel.LocationId);
                param.Add("@MKADistrictId", statusAndDOBDiscrepancyModel.MKADistrictId);
                param.Add("@MKALLGId", statusAndDOBDiscrepancyModel.MKALLGId);
                param.Add("@MKAWardId", statusAndDOBDiscrepancyModel.MKAWardId);
                param.Add("@MKALocationId", statusAndDOBDiscrepancyModel.MKALocationId);
                param.Add("@RequestType", statusAndDOBDiscrepancyModel.RequestType);
                param.Add("@PageNo", statusAndDOBDiscrepancyModel.PageNo);
                param.Add("@PageSize", statusAndDOBDiscrepancyModel.PageSize);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                var res = await con.QueryMultipleAsync(squery, param, commandType: CommandType.StoredProcedure);
                if (res != null)
                {
                    result.ListItems = await res.ReadAsync<StatusAndDOBDiscrepancyResponseModel>();
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

        #region get sync status

        /// <summary>
        /// Gets the synchronize status.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<SyncStatusResponseModel> GetSyncStatus(SyncStatusRequestModel model)
        {
            SyncStatusResponseModel syncStatusResponseModel;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string squery = StoredProcedure.GetSyncStatus;
                DynamicParameters param = new DynamicParameters(model);
                param.Add("@RegionId", model.RegionId);
                param.Add("@ProvinceId", model.ProvinceId);
                param.Add("@DistrictId", model.DistrictId);
                param.Add("@LLGId", model.LLGId);
                param.Add("@WardId", model.WardId);
                param.Add("@LocationId", model.LocationId);
                param.Add("@Status", model.Status);
                param.Add("@TempElectorId", model.TempElectorId);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                syncStatusResponseModel = await con.QueryFirstOrDefaultAsync<SyncStatusResponseModel>(squery, param, commandType: CommandType.StoredProcedure);
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return syncStatusResponseModel;
        }

        #endregion

        #endregion
    }
}
