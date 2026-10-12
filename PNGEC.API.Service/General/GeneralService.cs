using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PNGEC.API.Domain.Mobile.General;
using PNGEC.API.Domain.Mobile.Voter;
using PNGEC.API.Helper;
using PNGEC.API.Repository.General;

namespace PNGEC.API.Service.General
{
    /// <summary>
    /// general service class
    /// </summary>
    public class GeneralService : IGeneralService
    {
        #region variable declaration
        /// <summary>
        /// The i general repository
        /// </summary>
        private readonly IGeneralRepository iGeneralRepository;

        /// <summary>
        /// The voters
        /// </summary>
        private List<DeviceOpertorModel> deviceOpertor = null;
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="GeneralService"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public GeneralService(string connection)
        {
            iGeneralRepository = new GeneralRepository(connection);
        }
        #endregion

        #region service

        #region get geo list mobile
        /// <summary>
        /// Gets the geo list mobile.
        /// </summary>
        /// <returns>
        /// return geo list
        /// </returns>
        public async Task<Response> GetGeoListMobile()
        {
            Response response = new Response();

            try
            {
                var geolist = await iGeneralRepository.GetGeoListMobile();

                if (geolist != null)
                {
                    response.Data = geolist;
                    response.Status = true;
                }
                else
                {
                    response.Message = ResponseMessage.RecordNotExist;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("GeneralService", "GetGeoListMobile", ex);
            }

            return response;
        }
        #endregion

        #region get active province list mobile
        /// <summary>
        /// Gets the active province list mobile.
        /// </summary>
        /// <returns></returns>
        public async Task<Response> GetActiveProvinceListMobile()
        {
            var response = new Response();
            try
            {
                var provinceList = await iGeneralRepository.GetActiveProvinceListMobile();

                if (provinceList != null)
                {
                    response.Data = provinceList;
                    response.Status = true;
                }
                else
                {
                    response.Message = ResponseMessage.RecordNotExist;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("GeneralService", "GetActiveProvinceListMobile", ex);
            }

            return response;
        }
        #endregion

        #region get occupation list mobile
        /// <summary>
        /// Gets the occupation list mobile.
        /// </summary>
        /// <returns>
        /// return occupation list
        /// </returns>
        public async Task<Response> GetOccupationListMobile()
        {
            var response = new Response();
            try
            {
                var occupationlist = await iGeneralRepository.GetOccupationListMobile();

                if (occupationlist != null)
                {
                    response.Data = occupationlist;
                    response.Status = true;
                }
                else
                {
                    response.Message = ResponseMessage.RecordNotExist;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("GeneralService", "GetOccupationListMobile", ex);
            }

            return response;
        }
        #endregion

        #region validate enrolment number
        /// <summary>
        /// Validates the enrolment number.
        /// </summary>
        /// <param name="validateEnrolmentNumberRequestModel">The validate enrolment number request model.</param>
        /// <returns>
        /// return validation response
        /// </returns>
        public async Task<Response> ValidateEnrolmentNumber(ValidateEnrolmentNumberRequestModel validateEnrolmentNumberRequestModel)
        {
            var response = new Response();
            try
            {
                var electorId = await iGeneralRepository.ValidateEnrolmentNumber(validateEnrolmentNumberRequestModel);

                if (electorId > 0)
                {
                    response.Message = ResponseMessage.EnrolmentFormError_Validation;
                    response.Status = false;
                }
                else
                {
                    response.Status = true;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("GeneralService", "ValidateEnrolmentNumber", ex);
            }

            return response;
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
        public async Task<Response<LocationModel>> GetAllLocationsMobile(LocationRequestModel locationRequestModel)
        {
            var response = new Response<LocationModel>();

            try
            {
                var result = await iGeneralRepository.GetAllLocationsMobile(locationRequestModel);
                if (result != null)
                {
                    response.Data = result;
                    response.Status = true;
                    response.Message = "success";
                }
            }
            catch (Exception ex)
            {
                response.Message = ResponseMessage.SomethingWentWrong;
                response.Data = null;
                response.Status = false;
                ErrorLog.Write("GeneralService", "GetAllLocationsMobile", ex);
            }
            return response;
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
        public async Task<Response> GetLocationsByIdsMobile(LocationRequestModel locationRequestModel)
        {
            var response = new Response();
            try
            {
                var locationlist = await iGeneralRepository.GetLocationsByIdsMobile(locationRequestModel);

                if (locationlist != null)
                {
                    response.Data = locationlist;
                    response.Status = true;
                }
                else
                {
                    response.Message = ResponseMessage.RecordNotExist;
                }
            }
            catch (Exception ex)
            {
                response.Message = ResponseMessage.SomethingWentWrong;
                ErrorLog.Write("GeneralService", "GetLocationsByIdsMobile", ex);
            }

            return response;
        }
        #endregion

        #region logout mobile
        /// <summary>
        /// Logouts the mobile.
        /// </summary>
        /// <param name="logoutRequest">The logout request.</param>
        /// <returns>
        /// return logout response
        /// </returns>
        public async Task<Response> LogoutMobile(LogoutRequest logoutRequest)
        {
            var response = new Response();
            try
            {
                var userId = await iGeneralRepository.LogoutMobile(logoutRequest);

                if (userId > 0)
                {
                    response.Message = ResponseMessage.SuccessUserLogout;
                    response.Status = true;
                }
                else
                {
                    response.Message = ResponseMessage.FailureUserLogout;
                    response.Status = false;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("GeneralService", "LogoutMobile", ex);
            }

            return response;
        }
        #endregion

        #region update user geo structure mobile
        /// <summary>
        /// Updates the user geo structure.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>updated rows</returns>
        public async Task<Response> UpdateUserGeoStructureMobile(UserModel model)
        {
            var response = new Response();
            try
            {
                var rowUpdated = await iGeneralRepository.UpdateUserGeoStructureMobile(model);

                if (rowUpdated > 0)
                {
                    response.Message = ResponseMessage.Updated_successfully;
                    response.Status = true;
                }
                else
                {
                    response.Message = ResponseMessage.Not_updated_successfully;
                    response.Status = false;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("GeneralService", "UpdateUserGeoStructureMobile", ex);
            }

            return response;
        }
        #endregion

        #region get device operator report mobile
        /// <summary>
        /// Gets the device operator report.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Response> GetDeviceOperatorReport(GetDeviceOperatorRequestModel model)
        {
            var response = new Response();
            try
            {
                var result = await iGeneralRepository.GetDeviceOperatorReport(model);
                if (result != null)
                {
                    response.Data = result;
                    response.Status = true;
                    response.Message = "Success";
                }
                else
                {
                    response.Message = ResponseMessage.RecordNotExist;
                }
            }
            catch (Exception ex)
            {
                response.Message = ResponseMessage.SomethingWentWrong;
                response.Data = null;
                response.Status = false;
                ErrorLog.Write("GeneralService", "GetLocationsByIdsMobile", ex);
            }
            return response;
        }
        #endregion

        #region get PDF device operator report mobile
        ///// <summary>
        ///// Gets the PDF device operator report.
        ///// </summary>
        ///// <param name="model">The model.</param>
        ///// <returns></returns>
        //public async Task<Response> GetPDFDeviceOperatorReport(GetDeviceOperatorRequestModel model)
        //{
        //    var response = new Response();
        //    int PageNo = 1;
        //    bool DataFound = false;
        //    deviceOpertor = new List<DeviceOpertorModel>();
        //    model.PageSize = 1000;

        //    try
        //    {
        //        do
        //        {
        //            model.PageNo = PageNo;
        //            Result<DeviceOpertorModel> result = await iGeneralRepository.GetPDFDeviceOperatorReport(model);
        //            if (result != null && result.ListItems != null && result.ListItems.Count() > 0)
        //            {
        //                deviceOpertor.AddRange(result.ListItems);

        //                DataFound = true;
        //                PageNo++;
        //            }
        //            else
        //            {
        //                DataFound = false;
        //            }

        //            result = null;

        //        } while (DataFound);

        //        if(deviceOpertor != null && deviceOpertor.Count > 0)
        //        {
        //            response.Status = true;
        //            response.Data = deviceOpertor.GetRange(0, deviceOpertor.Count);
        //        }
        //        else
        //        {
        //            response.Status = false;
        //            response.Data = deviceOpertor.GetRange(0, deviceOpertor.Count);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        response.Status = false;
        //        response.Data = null;
        //        response.Message = ResponseMessage.SomethingWentWrong;

        //        deviceOpertor.Clear();
        //        deviceOpertor = null;

        //        ErrorLog.Write("GeneralService", "GetLocationsByIdsMobile", ex);
        //    }

        //    deviceOpertor.Clear();
        //    deviceOpertor = null;

        //    return response;
        //}
        #endregion

        #region Get status and DOB discrepancy report

        /// <summary>
        /// Gets the status and dob discrepancy report.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Response> GetStatusAndDOBDiscrepancyReport(StatusAndDOBDiscrepancyModel model)
        {
            var response = new Response();
            try
            {
                var result = await iGeneralRepository.GetStatusAndDOBDiscrepancyReport(model);
                if (result != null)
                {
                    response.Data = result;
                    response.Status = true;
                    response.Message = "Success";
                }
                else
                {
                    response.Message = ResponseMessage.RecordNotExist;
                }
            }
            catch (Exception ex)
            {
                response.Message = ResponseMessage.SomethingWentWrong;
                response.Data = null;
                response.Status = false;
                ErrorLog.Write("GeneralService", "GetStatusAndDOBDiscrepancyReport", ex);
            }
            return response;
        }
        #endregion

        #region Get sync status
        /// <summary>
        /// Synchronizes the electors.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Response> GetSyncStatus(List<SyncStatusRequestModel> model)
        {
            List<SyncStatusResponseModel> syncStatusResponseList = new List<SyncStatusResponseModel>();
            var response = new Response();

            try
            {
                if (model != null && model.Count > 0)
                {
                    foreach (var item in model)
                    {
                        try
                        {
                            syncStatusResponseList.Add(await iGeneralRepository.GetSyncStatus(item));
                        }
                        catch (Exception ex)
                        {
                            ErrorLog.Write("GeneralService", "GetSyncStatus-SQL", ex);
                        }

                    }
                    response.Data = syncStatusResponseList;
                    response.Status = true;
                }
                else
                {
                    response.Status = false;
                    response.Message = ResponseMessage.SomethingWentWrong;
                    response.Data = null;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Write("GeneralService", "GetSyncStatus", ex);
            }

            return response;
        }
        #endregion
        #endregion
    }
}
