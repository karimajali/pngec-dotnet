using System.Collections.Generic;
using System.Threading.Tasks;
using PNGEC.API.Domain.Mobile.General;
using PNGEC.API.Helper;

namespace PNGEC.API.Service.General
{
    /// <summary>
    /// general service interface
    /// </summary>
    public interface IGeneralService
    {
        /// <summary>
        /// Gets the geo list mobile.
        /// </summary>
        /// <returns>return geo list</returns>
        Task<Response> GetGeoListMobile();
        

        /// <summary>
        /// Gets the active province list mobile.
        /// </summary>
        /// <returns></returns>
        Task<Response> GetActiveProvinceListMobile();

        /// <summary>
        /// Gets the occupation list mobile.
        /// </summary>
        /// <returns>return occupation list</returns>
        Task<Response> GetOccupationListMobile();

        /// <summary>
        /// Validates the enrolment number.
        /// </summary>
        /// <param name="validateEnrolmentNumberRequestModel">The validate enrolment number request model.</param>
        /// <returns>return validation response</returns>
        Task<Response> ValidateEnrolmentNumber(ValidateEnrolmentNumberRequestModel validateEnrolmentNumberRequestModel);

        /// <summary>
        /// Gets all locations mobile.
        /// </summary>
        /// <param name="locationRequestModel">The location request model.</param>
        /// <returns>return location lists</returns>
        Task<Response<LocationModel>> GetAllLocationsMobile(LocationRequestModel locationRequestModel);

        /// <summary>
        /// Gets the locations by ids mobile.
        /// </summary>
        /// <param name="locationRequestModel">The location request model.</param>
        /// <returns>return location lists</returns>
        Task<Response> GetLocationsByIdsMobile(LocationRequestModel locationRequestModel);

        /// <summary>
        /// Logouts the mobile.
        /// </summary>
        /// <param name="logoutRequest">The logout request.</param>
        /// <returns>return logout response</returns>
        Task<Response> LogoutMobile(LogoutRequest logoutRequest);

        /// <summary>
        /// Updates the user geo structure.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Updated rows</returns>
        Task<Response> UpdateUserGeoStructureMobile(UserModel model);

        /// <summary>
        /// Gets the device operator report.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Response> GetDeviceOperatorReport(GetDeviceOperatorRequestModel model);

        ///// <summary>
        ///// Gets the PDF device operator report.
        ///// </summary>
        ///// <param name="model">The model.</param>
        ///// <returns></returns>
        //Task<Response> GetPDFDeviceOperatorReport(GetDeviceOperatorRequestModel model);

        /// <summary>
        /// Gets the status and dob discrepancy report.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Response> GetStatusAndDOBDiscrepancyReport(StatusAndDOBDiscrepancyModel model);

        /// <summary>
        /// Synchronizes the electors.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Response> GetSyncStatus(List<SyncStatusRequestModel> model);
    }
}
