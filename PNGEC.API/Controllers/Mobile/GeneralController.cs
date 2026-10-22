//using AspNetCore.Reporting;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PNGEC.API.Domain.Mobile.General;
using PNGEC.API.Helper;
using PNGEC.API.Service.Clan;
using PNGEC.API.Service.General;
using PNGEC.API.Service.MKAClan;
using PNGEC.API.Service.Setting;
using PNGEC.API.Service.User;

namespace PNGEC.API.Controllers.Mobile
{
    /// <summary>
    /// general controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/Mobile/[controller]")]
    [ApiController]
    public class GeneralController : ControllerBase
    {
        #region variable declaration
        /// <summary>
        /// The i general service
        /// </summary>
        private IGeneralService iGeneralService;
        /// <summary>
        /// The i mkageneral service
        /// </summary>
        private IMKAGeneralService iMKAGeneralService;

        /// <summary>
        /// The user service
        /// </summary>
        private IUserService userService;

        /// <summary>
        /// The i setting service
        /// </summary>
        private ISettingService iSettingService;

        /// <summary>
        /// the i clan service
        /// </summary>
        private IClanService iclanService;

        /// <summary>
        /// The data
        /// </summary>
        private Dictionary<string, string> data = null;
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="GeneralController"/> class.
        /// </summary>
        /// <param name="iGeneralService">The i general service.</param>
        public GeneralController(IGeneralService iGeneralService,IMKAGeneralService iMKAGeneralService, IUserService userService, ISettingService settingService, IClanService clanService)
        {
            this.iGeneralService = iGeneralService;
            this.iMKAGeneralService = iMKAGeneralService;
            this.userService = userService;
            this.iSettingService = settingService;
            this.iclanService = clanService;
        }
        #endregion

        #region api

        /// <summary>
        /// Gets the geo list mobile.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetGeoList")]
        public async Task<IActionResult> GetGeoListMobile()
        {
            var lstresponse = new Response();
            lstresponse = await iGeneralService.GetGeoListMobile();
            return Ok(lstresponse);
        }

        /// <summary>
        /// Gets the mkageo list mobile.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetMKAGeoList")]
        public async Task<IActionResult> GetMKAGeoListMobile()
        {
            var lstresponse = new Response();
            lstresponse = await iMKAGeneralService.GetMKAGeoListMobile();
            return Ok(lstresponse);
        }
        /// <summary>
        /// Gets the active province list.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetActiveProvinceList")]
        public async Task<IActionResult> GetActiveProvinceListMobile()
        {
            var lstresponse = new Response();
            lstresponse = await iGeneralService.GetActiveProvinceListMobile();
            return Ok(lstresponse);
        }

        /// <summary>
        /// Gets the occupation list.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetOccupationList")]
        public async Task<IActionResult> GetOccupationList()
        {
            var lstresponse = new Response();
            lstresponse = await iGeneralService.GetOccupationListMobile();
            return Ok(lstresponse);
        }

        /// <summary>
        /// Forgots the password.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] CommonRequest model)
        {
            var response = new Response();

            if (model != null)
            {
                response = await userService.ForgotPassword(model);
            }
            else
            {
                response.Status = false;
                response.Message = ResponseMessage.Invalid_Requests_Model;
            }
            return Ok(response);
        }

        /// <summary>
        /// Validates the enrolment number.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("ValidateEnrolmentNumber")]
        public async Task<IActionResult> ValidateEnrolmentNumber([FromBody] ValidateEnrolmentNumberRequestModel model)
        {
            var response = new Response();

            if (model != null)
            {
                response = await iGeneralService.ValidateEnrolmentNumber(model);
            }
            else
            {
                response.Status = false;
                response.Message = ResponseMessage.Invalid_Requests_Model;
            }
            return Ok(response);
        }

        /// <summary>
        /// Gets all locations mobile.
        /// </summary>
        /// <param name="locationRequestModel">The location request model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetAllLocations")]
        public async Task<IActionResult> GetAllLocationsMobile(LocationRequestModel locationRequestModel)
        {
            var lstresponse = new Response<LocationModel>();
            if (locationRequestModel != null)
            {
                lstresponse = await iGeneralService.GetAllLocationsMobile(locationRequestModel);
            }
            else
            {
                lstresponse.Status = false;
                lstresponse.Message = ResponseMessage.Invalid_Requests_Model;
            }

            return Ok(lstresponse);
        }

        /// <summary>
        /// Gets the locations by ids mobile.
        /// </summary>
        /// <param name="locationRequestModel">The location request model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetLocationsByIds")]
        public async Task<IActionResult> GetLocationsByIdsMobile(LocationRequestModel locationRequestModel)
        {
            var lstresponse = new Response();
            lstresponse = await iGeneralService.GetLocationsByIdsMobile(locationRequestModel);
            return Ok(lstresponse);
        }

        /// <summary>
        /// Logouts the mobile.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Logout")]
        public async Task<IActionResult> LogoutMobile([FromBody] LogoutRequest model)
        {
            var response = new Response();

            if (model != null)
            {
                response = await iGeneralService.LogoutMobile(model);
            }
            else
            {
                response.Status = false;
                response.Message = ResponseMessage.Invalid_Requests_Model;
            }
            return Ok(response);
        }

        /// <summary>
        /// Updates the user geo structure mobile.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Updated rows</returns>
        [HttpPost]
        [Route("UpdateUserGeoStructure")]
        public async Task<IActionResult> UpdateUserGeoStructureMobile([FromBody] UserModel model)
        {
            var response = new Response();
            if (model != null)
            {
                response = await iGeneralService.UpdateUserGeoStructureMobile(model);
            }
            else
            {
                response.Status = false;
                response.Message = ResponseMessage.Invalid_Requests_Model;
            }
            return Ok(response);
        }

        /// <summary>
        /// Gets the device operator report.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetDeviceOperatorReport")]
        public async Task<IActionResult> GetDeviceOperatorReport([FromBody] GetDeviceOperatorRequestModel model)
        {
            var lstresponse = new Response();
            if (model != null)
            {
                lstresponse = await iGeneralService.GetDeviceOperatorReport(model);
            }
            else
            {
                lstresponse.Status = false;
                lstresponse.Message = ResponseMessage.Invalid_Requests_Model;
            }

            return Ok(lstresponse);
        }

        ///// <summary>
        ///// Gets the PDF device operator report.
        ///// </summary>
        ///// <param name="model">The model.</param>
        ///// <returns></returns>
        //[HttpPost]
        //[Route("GetPDFDeviceOperatorReport")]
        //public async Task<IActionResult> GetPDFDeviceOperatorReport([FromBody] GetDeviceOperatorRequestModel model)
        //{
        //    var response = new Response();
        //    data = new Dictionary<string, string>();

        //    try
        //    {
        //        if (model != null)
        //        {
        //            response = await iGeneralService.GetPDFDeviceOperatorReport(model);
        //            if (response != null && response.Status == true)
        //            {
        //                List<DeviceOpertorModel> deviceOpertor = (List<DeviceOpertorModel>)response.Data;
        //                if (deviceOpertor != null && deviceOpertor.Count > 0)
        //                {
        //                    byte[] bytes = GenerateDeviceOperatorReport(deviceOpertor, model);

        //                    //var UniqueFileName = $@"{Guid.NewGuid()}.pdf";
        //                    var fileFullPath = string.Format("{0}/{1}/{2}", Constants.UploadFolderPath, Constants.DeviceOperatorPDFFolderPath, $@"{Guid.NewGuid()}.pdf");

        //                    using (FileStream stream = new FileStream(fileFullPath, FileMode.Create))
        //                    {
        //                        stream.Write(bytes, 0, bytes.Length);
        //                    }

        //                    response.Status = true;
        //                    response.Message = string.Format(ResponseMessage.ReportGeneratedSuccess, "Device Operator");
        //                    data.Add("reportPath", string.Format("{0}/{1}", SettingsConfig.AppSetting("APIUrl", "base"), fileFullPath));
        //                    response.Data = data;
        //                }
        //                else
        //                {
        //                    response.Status = false;
        //                    response.Message = ResponseMessage.SomethingWentWrong;
        //                    data.Add("reportPath", "");
        //                    response.Data = data;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            response.Status = false;
        //            response.Message = ResponseMessage.Invalid_Requests_Model;
        //            data.Add("reportPath", "");
        //            response.Data = data;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        response.Status = false;
        //        response.Message = ResponseMessage.Invalid_Requests_Model;
        //        data.Add("reportPath", "");
        //        response.Data = data;

        //        ErrorLog.Write("GeneralController", "GetPDFDeviceOperatorReport", ex);
        //    }
        //    finally
        //    {
        //        data = null;
        //    }

        //    return Ok(response);
        //}

        /// <summary>
        /// Gets the status and dob discrepancy report.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetStatusAndDOBDiscrepancyReport")]
        public async Task<IActionResult> GetStatusAndDOBDiscrepancyReport([FromBody] StatusAndDOBDiscrepancyModel model)
        {
            var lstresponse = new Response();
            if (model != null)
            {
                lstresponse = await iGeneralService.GetStatusAndDOBDiscrepancyReport(model);
            }
            else
            {
                lstresponse.Status = false;
                lstresponse.Message = ResponseMessage.Invalid_Requests_Model;
            }

            return Ok(lstresponse);
        }

        /// <summary>
        /// Gets the setting.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetSetting")]
        public async Task<IActionResult> GetSetting()
        {
            return Ok(await iSettingService.GetSetting());
        }

        /// <summary>
        /// Gets the synchronize status.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetSyncStatus")]
        public async Task<IActionResult> GetSyncStatus([FromBody] List<SyncStatusRequestModel> model)
        {
            var lstresponse = new Response();
            if (model != null)
            {
                lstresponse = await iGeneralService.GetSyncStatus(model);
            }
            else
            {
                lstresponse.Status = false;
                lstresponse.Message = ResponseMessage.Invalid_Requests_Model;
            }

            return Ok(lstresponse);
        }
        #endregion
        #region get clan list
        /// <summary>
        /// Gets the clan list.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetMKAClanList")]
        public async Task<IActionResult> GetMKAClanList()
        {
            var lstresponse = new Response();
            lstresponse = await iMKAGeneralService.GetMKAClanList();

            return Ok(lstresponse);
        }
        #endregion

        #region get geo clan list
        /// <summary>
        /// Gets the clan list.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetGeoClanList")]
        public async Task<IActionResult> GetGeoClanList()
        {
            var lstresponse = new Response();
            lstresponse = await iclanService.GetGeoClanList();

            return Ok(lstresponse);
        }
        #endregion


        #region Method

        #region GenerateDeviceOperatorReport
        ///// <summary>
        ///// Generates the device operator report.
        ///// </summary>
        ///// <param name="voters">The voters.</param>
        ///// <returns></returns>
        //private byte[] GenerateDeviceOperatorReport(List<DeviceOpertorModel> deviceOpertor, GetDeviceOperatorRequestModel model)
        //{
        //    byte[] bytes = null;

        //    if (deviceOpertor != null && deviceOpertor.Count > 0)
        //    {
        //        string mimtype = "";
        //        int extension = 1;
        //        var path = "Report/DeviceOpertor.rdlc";

        //        string WardName = string.Empty, WardNumber = string.Empty, LLGName = string.Empty, DistrictName = string.Empty, ProvinceName = string.Empty;
        //        int New = 0, Updated = 0, Deceased = 0, Ghost = 0, Duplicate = 0, TranOut = 0, TranIn = 0, UnderAge = 0, Total = 0;


        //        WardName = deviceOpertor.Select(x => x.WardName).FirstOrDefault();
        //        LLGName = deviceOpertor.Select(x => x.LLGName).FirstOrDefault();
        //        DistrictName = deviceOpertor.Select(x => x.DistrictName).FirstOrDefault();
        //        ProvinceName = deviceOpertor.Select(x => x.ProvinceName).FirstOrDefault();

        //        #region param
        //        Dictionary<string, string> param = new Dictionary<string, string>();
        //        param.Add("DeviceRun", model.DeviceRun);
        //        param.Add("DeviceID", model.DeviceID);
        //        param.Add("DPO", model.DPO);
        //        param.Add("WardName", string.Format("{0}/{1}/{2}/{3}", ProvinceName, DistrictName, LLGName, WardName));
        //        New = deviceOpertor.Count(x => x.Status == "NEW").Equals(DBNull.Value) ? 0 : deviceOpertor.Count(x => x.Status == "NEW");
        //        param.Add("New", Convert.ToString(New));
        //        Updated = deviceOpertor.Count(x => x.Status == "UPDATED").Equals(DBNull.Value) ? 0 : deviceOpertor.Count(x => x.Status == "UPDATED");
        //        param.Add("Updated", Convert.ToString(Updated));
        //        Deceased = deviceOpertor.Count(x => x.Status == "DECEASED").Equals(DBNull.Value) ? 0 : deviceOpertor.Count(x => x.Status == "DECEASED");
        //        param.Add("Deceased", Convert.ToString(Deceased));
        //        Ghost = deviceOpertor.Count(x => x.Status == "GHOST").Equals(DBNull.Value) ? 0 : deviceOpertor.Count(x => x.Status == "GHOST");
        //        param.Add("Ghost", Convert.ToString(Ghost));
        //        Duplicate = deviceOpertor.Count(x => x.Status == "DUPLICATE").Equals(DBNull.Value) ? 0 : deviceOpertor.Count(x => x.Status == "DUPLICATE");
        //        param.Add("Duplicate", Convert.ToString(Duplicate));
        //        TranOut = deviceOpertor.Count(x => x.Status == "TRANSFERRED OUT").Equals(DBNull.Value) ? 0 : deviceOpertor.Count(x => x.Status == "TRANSFERRED OUT");
        //        param.Add("TranOut", Convert.ToString(TranOut));
        //        TranIn = deviceOpertor.Count(x => x.Status == "TRANSFERRED IN").Equals(DBNull.Value) ? 0 : deviceOpertor.Count(x => x.Status == "TRANSFERRED IN");
        //        param.Add("TranIn", Convert.ToString(TranIn));
        //        UnderAge = deviceOpertor.Count(x => x.Status == "UNDER AGE").Equals(DBNull.Value) ? 0 : deviceOpertor.Count(x => x.Status == "UNDER AGE");
        //        param.Add("UnderAge", Convert.ToString(UnderAge));
        //        param.Add("Total", Convert.ToString(New + Updated + Deceased + Ghost + Duplicate + TranOut + TranIn + UnderAge));
        //        #endregion

        //        LocalReport localReport = new LocalReport(path);
        //        localReport.AddDataSource("ds_DeviceOpertor", deviceOpertor);
        //        var result = localReport.Execute(RenderType.Pdf, extension, param, mimtype);
        //        return result.MainStream;
        //    }

        //    return bytes;
        //}
        #endregion

        #endregion

    }
}