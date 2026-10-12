using PNGEC.API.Domain.Device;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Helper;
using PNGEC.API.Repository.Device;
using System;
using System.Threading.Tasks;

namespace PNGEC.API.Service.Device
{
    public class DeviceService : IDeviceService
    {
        #region variable declaration
        /// <summary>
        /// The i device repository
        /// </summary>
        private readonly IDeviceRepository iDeviceRepository;

        /// <summary>
        /// The response
        /// </summary>
        private Response response = null;

        /// <summary>
        /// The lstresponse
        /// </summary>
        private Response<DeviceModel> lstresponse = null;

        /// <summary>
        /// The result
        /// </summary>
        private Result<DeviceModel> result = null;

        /// <summary>
        /// The module name
        /// </summary>
        private const string ModuleName = "Device";
        #endregion

        #region constructor 
        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceService"/> class.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public DeviceService(string connection)
        {
            iDeviceRepository = new DeviceRepository(connection);
        }
        #endregion

        #region service

        #region add update device
        /// <summary>
        /// Adds/update the Device
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Response> AddUpdateDevice(DeviceModel model)
        {
            try
            {
                response = new Response();
                DeviceModel device = await iDeviceRepository.AddUpdateDevice(model);

                response.Status = device.ResponseCode > 0;

                if (device.ResponseCode == -1)
                {
                    response.Message = ResponseMessage.DeviceError_DeviceNumber_Exists;
                } 
                else if (device.ResponseCode == -2)
                {
                    response.Message = ResponseMessage.DeviceError_DeviceName_Exists;
                }
                else if (device.ResponseCode == -3)
                {
                    response.Message = ResponseMessage.DeviceError_DeviceAssignedTo_Exists;
                }
                else
                {
                    if (device.AssignedToChanged || device.DeviceNameChanged)
                    {
                        //Send an email to the user for the device assigned
                        string content = string.Format("Hello {0}, {1} {2} The Device {3} is assigned to you. {4} {5} Thanks & Regards {6} PNGEC",
                           device.UserName,
                           Environment.NewLine,
                           Environment.NewLine,
                           device.DeviceName,
                           Environment.NewLine,
                           Environment.NewLine,
                           Environment.NewLine);

                        bool IsSuccess = Email.SendMail(device.EmailAddress, "Device Assigned", content);

                        if (IsSuccess)
                        {
                            response.Message = ResponseMessage.Record_Add_Update_Success;
                            response.Data = device;
                        }
                        else
                        {
                            response.Status = false;
                            response.Message = "something went wrong, try again later";
                        }
                    }
                    else
                    {
                        response.Message = ResponseMessage.Record_Add_Update_Success;
                        response.Data = device;
                    }
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("DeviceService", "AddUpdateDevice", ex);
            }
            return response;
        }
        #endregion

        #region get Device list
        /// <summary>
        /// Gets the Device list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Response<DeviceModel>> GetDeviceList(PageList model)
        {
            lstresponse = new Response<DeviceModel>();

            try
            {
                result = await iDeviceRepository.GetDeviceList(model);

                if (result != null && result.ListItems != null)
                {
                    lstresponse.Data = result;
                    lstresponse.Status = true;
                }
                else
                {
                    lstresponse.Message = ResponseMessage.RecordNotExist;
                }
            }
            catch (Exception ex)
            {
                lstresponse.Message = ex.Message;
                ErrorLog.Write("DeviceService", "GetDeviceList", ex);
            }

            return lstresponse;
        }
        #endregion

        #region update device status
        /// <summary>
        /// Updates the device status.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Response<DeviceModel>> UpdateDeviceStatus(CommonRequest model)
        {
            lstresponse = new Response<DeviceModel>();
            try
            {
                result = await iDeviceRepository.UpdateDeviceStatus(model);

                if (result != null && result.ListItems != null)
                {
                    lstresponse.Data = result;
                    lstresponse.Status = true;
                }
                else
                {
                    lstresponse.Message = ResponseMessage.RecordNotExist;
                }
            }
            catch (Exception ex)
            {
                lstresponse.Message = ex.Message;
                ErrorLog.Write("DeviceService", "UpdateDeviceStatus", ex);
            }

            return lstresponse;
        }
        #endregion

        #endregion
    }
}
