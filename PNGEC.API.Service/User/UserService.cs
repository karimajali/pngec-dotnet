using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using PNGEC.API.Domain;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Domain.Mobile.User;
using PNGEC.API.Domain.User;
using PNGEC.API.Helper;
using PNGEC.API.Repository.Setting;
using PNGEC.API.Repository.User;

namespace PNGEC.API.Service.User
{
    public class UserService : IUserService
    {
        #region variable declaration
        /// <summary>
        /// The i user repository
        /// </summary>
        private readonly IUserRepository iUserRepository;

        /// <summary>
        /// The i setting repository
        /// </summary>
        private readonly ISettingRepository iSettingRepository;

        /// <summary>
        /// The response
        /// </summary>
        private Response response = null;

        /// <summary>
        /// The lstresponse
        /// </summary>
        private Response<UserModel> lstresponse = null;

        /// <summary>
        /// The result
        /// </summary>
        private Result<UserModel> result = null;

        /// <summary>
        /// The module name
        /// </summary>
        private const string ModuleName = "user";

        /// <summary>
        /// The old profile image name
        /// </summary>
        private string OldProfileImageName = string.Empty;

        /// <summary>
        /// The API URL
        /// </summary>
        private string apiURL = string.Empty;
        #endregion

        #region constructor 
        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public UserService(string connection, string apiURL)
        {
            iUserRepository = new UserRepository(connection);
            iSettingRepository = new SettingRepository(connection);
            this.apiURL = apiURL;
        }
        #endregion

        #region service

        #region add update user
        /// <summary>
        /// Adds the update user.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Response> AddUpdateUser(UserModel model)
        {
            response = new Response();
            string tempPassword = model.Password;

            try
            {
                OldProfileImageName = string.Empty;
                // Save in folder
                if (model.ProfileImage != null && model.ProfileImage.Length > 0)
                {
                    if (!string.IsNullOrWhiteSpace(model.ProfileImageName))
                    {
                        OldProfileImageName = model.ProfileImageName;
                    }

                    model.ProfileImageName = await UploadFiles.UploadFile(model.ProfileImage, ModuleName, string.Empty);
                }

                // Paasword convert MD5Hash
                if (!string.IsNullOrWhiteSpace(model.Password) && model.UserId <= 0)
                {
                    model.Password = CommonFunction.MD5Hash(model.Password);
                }

                UserModel user = await iUserRepository.AddUpdateUser(model);

                response.Status = user.ResponseCode > 0;

                if (user.ResponseCode == -1)
                {
                    // Previous image delete on edit record
                    if (!string.IsNullOrWhiteSpace(model.ProfileImageName))
                    {
                        UploadFiles.DeleteFile(model.ProfileImageName, ModuleName);
                    }
                    response.Message = ResponseMessage.UserError_EmailAddress_Exists;
                }
                else if (user.ResponseCode == -2)
                {
                    // Previous image delete on edit record
                    if (!string.IsNullOrWhiteSpace(model.ProfileImageName))
                    {
                        UploadFiles.DeleteFile(model.ProfileImageName, ModuleName);
                    }
                    response.Message = ResponseMessage.UserError_ContactNumber_Exists;
                }
                else
                {
                    // Previous image delete on edit record
                    if (!string.IsNullOrWhiteSpace(OldProfileImageName))
                    {
                        UploadFiles.DeleteFile(OldProfileImageName, ModuleName);
                    }

                    if (model.UserId <= 0)
                    {
                        string content = string.Format("Hello {0} {1} {2}{3} Username : {4} {5} Password : {6} {7}{8} Thanks & Regards {9} PNGEC",
                        user.FirstName,
                        user.LastName,
                        Environment.NewLine,
                        Environment.NewLine,
                        user.EmailAddress,
                        Environment.NewLine,
                        tempPassword,
                        Environment.NewLine,
                        Environment.NewLine,
                        Environment.NewLine);

                        bool IsSuccess = Email.SendMail(user.EmailAddress, "PNGEC Login Credentials", content);
                        if (IsSuccess)
                        {
                            response.Message = string.Format(ResponseMessage.RecordAddSuccess, "User");
                            response.Data = user;
                        }
                        else
                        {
                            response.Status = false;
                            response.Message = "something went wrong, try again later";
                            response.Data = user;
                        }
                    }
                    else
                    {
                        response.Message = string.Format(ResponseMessage.RecordUpdateSuccess, "User");
                        response.Data = user;
                    }
                }
            }
            catch (Exception ex)
            {
                // Previous image delete on edit record
                if (!string.IsNullOrWhiteSpace(model.ProfileImageName))
                {
                    UploadFiles.DeleteFile(model.ProfileImageName, ModuleName);
                }
                response.Message = ex.Message;
                ErrorLog.Write("UserService", "AddUpdateUser", ex);
            }
            return response;
        }
        #endregion

        #region get user list
        /// <summary>
        /// Gets the user list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Response<UserModel>> GetUserList(PageList model)
        {
            lstresponse = new Response<UserModel>();

            try
            {
                result = await iUserRepository.GetUserList(model);

                if (result != null && result.ListItems != null)
                {
                    foreach (var item in result.ListItems)
                    {
                        item.ProfileImageUrl = GenerateImagePath(item.ProfileImageName);
                    }

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
                ErrorLog.Write("UserService", "GetUserList", ex);
            }

            return lstresponse;
        }
        #endregion

        #region update user status
        /// <summary>
        /// Updates the user status.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Response<UserModel>> UpdateUserStatus(CommonRequest model)
        {
            lstresponse = new Response<UserModel>();

            try
            {
                result = await iUserRepository.UpdateUserStatus(model);

                if (result != null && result.ListItems != null)
                {
                    foreach (var item in result.ListItems)
                    {
                        item.ProfileImageUrl = GenerateImagePath(item.ProfileImageName);
                    }

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
                ErrorLog.Write("UserService", "UpdateUserStatus", ex);
            }

            return lstresponse;
        }
        #endregion

        #region user login
        /// <summary>
        /// Users the login.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Response<UserModel>> UserLogin(CommonRequest model)
        {
            lstresponse = new Response<UserModel>();

            try
            {
                if (!string.IsNullOrWhiteSpace(model.password) && model.password.Length > 0)
                {
                    model.password = CommonFunction.MD5Hash(model.password);
                }

                result = await iUserRepository.UserLogin(model);

                if (result != null && result.ListItems != null && result.TotalRecordCount > 0)
                {
                    string geoFileUrl = string.Empty;
                    string mkageoFileUrl = string.Empty;
                    int geoVersion = 0;
                    var directory="";
                    try
                    {
                        //var directory = $"{apiURL}/{Constants.UploadFolderPath}/{Constants.GeoFolderPath}";
                        if (result.ListItems.FirstOrDefault().Role == 9)
                        {
                             directory = Path.Combine(Directory.GetCurrentDirectory(), Constants.UploadFolderPath, Constants.MKAGeoFolderPath);

                        }
                        else
                        {
                             directory = Path.Combine(Directory.GetCurrentDirectory(), Constants.UploadFolderPath, Constants.GeoFolderPath);
                        }
                        if (Directory.Exists(directory))
                        {
                            string pattern = "*.json";
                            var dirInfo = new DirectoryInfo(directory);
                            var file = (from f in dirInfo.GetFiles(pattern) orderby f.LastWriteTime descending select f).FirstOrDefault();
                          
                            if (file != null && result.ListItems.FirstOrDefault().Role == 9)
                            {
                                mkageoFileUrl = $"{apiURL}/{Constants.UploadFolderPath}/{Constants.MKAGeoFolderPath}/{file.Name}";
                            }
                            else
                            {
                                geoFileUrl = $"{apiURL}/{Constants.UploadFolderPath}/{Constants.GeoFolderPath}/{file.Name}";
                            }
                        }
                        
                        var setting = await iSettingRepository.GetSetting();
                        if (setting != null)
                        {
                            geoVersion = setting.GeoVersion;
                        }
                    }
                    catch (Exception ex)
                    {
                        ErrorLog.Write("UserService", "UserLogin - GetGeo", ex);
                    }
                    
                    foreach (var item in result.ListItems)
                    {
                        item.ProfileImageUrl = GenerateImagePath(item.ProfileImageName);
                        item.GeoFileUrl = geoFileUrl;
                        item.GeoVersion = geoVersion;
                        item.MKAGeoFileUrl = mkageoFileUrl;
                    }

                    lstresponse.Message = "success";
                    lstresponse.Data = result;
                    lstresponse.Status = true;
                }
                else if (result != null && result.ListItems != null && result.TotalRecordCount == -2)
                {
                    lstresponse.Data = result;
                    lstresponse.Message = "this user, no access rights to the " + model.database + " database.";
                }
                else
                {
                    lstresponse.Message = "Please enter valid username and password.";
                }
            }
            catch (Exception ex)
            {
                lstresponse.Message = ex.Message;
                ErrorLog.Write("UserService", "UserLogin", ex);
            }

            return lstresponse;
        }
        #endregion

        #region forgot password
        /// <summary>
        /// Forgots the password.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Response> ForgotPassword(CommonRequest model)
        {
            response = new Response();

            try
            {
                UserModel user = await iUserRepository.GetSuperAdminDetails();

                if (user != null && user.UserId > 0 && !string.IsNullOrWhiteSpace(user.EmailAddress))
                {
                    string content = string.Format("{0} user send forgot password request, please check it details.", model.username);

                    bool IsSuccess = Email.SendMail(user.EmailAddress, "Forgot Password", content);

                    if (IsSuccess)
                    {
                        response.Status = true;
                        response.Message = "The password change request has been sent sucessfully to the super admin.";
                    }
                    else
                    {
                        response.Status = false;
                        response.Message = "something went wrong, try again later";
                    }
                }
                else
                {
                    response.Status = false;
                    response.Message = "something went wrong, try again later";
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("UserService", "ForgotPassword", ex);
            }

            return response;
        }
        #endregion

        #region reset password
        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Response> ResetPassword(UserModel model)
        {
            response = new Response();
            string tempPassword = model.Password;

            try
            {
                // Paasword convert MD5Hash
                if (!string.IsNullOrWhiteSpace(model.Password) && model.UserId > 0)
                {
                    model.Password = CommonFunction.MD5Hash(model.Password);
                }

                UserModel user = await iUserRepository.ResetPassword(model);

                if (user != null && user.UserId > 0 && !string.IsNullOrWhiteSpace(user.EmailAddress))
                {
                    string content = string.Format("Hello {0} {1} {2}{3} Username : {4} {5} New Password : {6} {7}{8} Thanks & Regards {9} PNGEC",
                       user.FirstName,
                       user.LastName,
                       Environment.NewLine,
                       Environment.NewLine,
                       user.EmailAddress,
                       Environment.NewLine,
                       tempPassword,
                       Environment.NewLine,
                       Environment.NewLine,
                       Environment.NewLine);

                    bool IsSuccess = Email.SendMail(user.EmailAddress, "Reset Password", content);

                    if (IsSuccess)
                    {
                        response.Status = true;
                        response.Message = "User mail send successfully.";
                    }
                    else
                    {
                        response.Status = false;
                        response.Message = "something went wrong, try again later";
                    }
                }
                else
                {
                    response.Status = false;
                    response.Message = "something went wrong, try again later";
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("UserService", "ResetPassword", ex);
            }

            return response;
        }
        #endregion

        #region generate image path
        /// <summary>
        /// Generates the image path.
        /// </summary>
        /// <param name="profileImageName">Name of the profile image.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private string GenerateImagePath(string profileImageName)
        {
            string fileUrl = string.Empty;

            try
            {
                if (!string.IsNullOrWhiteSpace(profileImageName))
                {
                    fileUrl = $"{this.apiURL}/{Constants.UploadFolderPath}/{Constants.UserImageFolderPath}/{profileImageName}";
                    //return Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\Uploads\\user\\") + profileImageName;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Write("UserService", "ResetPassword", ex);
            }

            return fileUrl;
        }
        #endregion

        #region MobileUserLogin        
        /// <summary>
        /// Mobiles the user login.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Response> MobileUserLogin(LoginUserRequestModel model)
        {
            response = new Response();

            try
            {
                if (!string.IsNullOrWhiteSpace(model.Password) && model.Password.Length > 0)
                {
                    model.Password = CommonFunction.MD5Hash(model.Password);
                }
                var res = await iUserRepository.MobileUserLogin(model);
                if (res != null && res.ResponseCode == 1)
                {
                    response.Data = res;
                    response.Status = true;
                    response.Message = "Successfully login";
                }
                else
                {
                    response.Data = null;
                    response.Status = false;
                    if (res.ResponseCode == -1)
                    {
                        response.Message = "It seems this device is not Authorized to use PNG Provincial Application. Please Consult PNG Administrator.";
                    }
                    else if (res.ResponseCode == -2)
                    {
                        response.Message = "Please enter the valid credentials.";
                    }
                    else if (res.ResponseCode == -3)
                    {
                        response.Message = "Device is not active, Please Consult PNG Administrator.";
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Write("UserService", "MobileUserLogin", ex);
            }

            return response;
        }
        #endregion

        #endregion
    }
}
