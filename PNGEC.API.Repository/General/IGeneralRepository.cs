using System.Collections.Generic;
using System.Threading.Tasks;
using PNGEC.API.Domain.Mobile.General;
using PNGEC.API.Domain.Mobile.Voter;
using PNGEC.API.Helper;

namespace PNGEC.API.Repository.General
{
    /// <summary>
    /// general repository interface
    /// </summary>
    public interface IGeneralRepository
    {
        /// <summary>
        /// Gets the geo list.
        /// </summary>
        /// <returns>return Geo list</returns>
        Task<GeoListModel> GetGeoListMobile();

        /// <summary>
        /// Gets the active province list mobile.
        /// </summary>
        /// <returns></returns>
        Task<List<ProvinceModel>> GetActiveProvinceListMobile();

        /// <summary>
        /// Gets the occupation list mobile.
        /// </summary>
        /// <returns>return occupation list</returns>
        Task<List<OccupationModel>> GetOccupationListMobile();

        /// <summary>
        /// Validates the enrolment number.
        /// </summary>
        /// <param name="validateEnrolmentNumberRequestModel">The validate enrolment number request model.</param>
        /// <returns>return enrolment id if available</returns>
        Task<long> ValidateEnrolmentNumber(ValidateEnrolmentNumberRequestModel validateEnrolmentNumberRequestModel);

        /// <summary>
        /// Gets all locations mobile.
        /// </summary>
        /// <param name="locationRequestModel">The location request model.</param>
        /// <returns>return location lists</returns>
        Task<Result<LocationModel>> GetAllLocationsMobile(LocationRequestModel locationRequestModel);

        /// <summary>
        /// Gets the locations by ids mobile.
        /// </summary>
        /// <param name="locationRequestModel">The location request model.</param>
        /// <returns>return location lists</returns>
        Task<List<LocationModel>> GetLocationsByIdsMobile(LocationRequestModel locationRequestModel);

        /// <summary>
        /// Logouts the mobile.
        /// </summary>
        /// <param name="logoutRequest">The logout request.</param>
        /// <returns>return user id</returns>
        Task<int> LogoutMobile(LogoutRequest logoutRequest);

        /// <summary>
        /// Updates the user geo structure.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Updated row</returns>
        Task<int> UpdateUserGeoStructureMobile(UserModel model);

        /// <summary>
        /// Gets the voter.
        /// </summary>
        /// <param name="VoterModel">The get voter model.</param>
        /// <returns></returns>
        Task<Result<VoterModel>> GetDeviceOperatorReport(GetDeviceOperatorRequestModel getDeviceOperatorModel);

        ///// <summary>
        ///// Gets the PDF device operator report.
        ///// </summary>
        ///// <param name="getDeviceOperatorModel">The get device operator model.</param>
        ///// <returns></returns>
        //Task<Result<DeviceOpertorModel>> GetPDFDeviceOperatorReport(GetDeviceOperatorRequestModel getDeviceOperatorModel);

        /// <summary>
        /// Gets the status and dob discrepancy report.
        /// </summary>
        /// <param name="getDeviceOperatorModel">The get device operator model.</param>
        /// <returns></returns>
        Task<Result<StatusAndDOBDiscrepancyResponseModel>> GetStatusAndDOBDiscrepancyReport(StatusAndDOBDiscrepancyModel getDeviceOperatorModel);

        /// <summary>
        /// Gets the synchronize status.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<SyncStatusResponseModel> GetSyncStatus(SyncStatusRequestModel model);
    }
}
