using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PNGEC.API.Domain;
using PNGEC.API.Domain.Mobile.Voter;
using PNGEC.API.Helper;
using PNGEC.API.Repository.Voter;

namespace PNGEC.API.Service.Voter
{
    public class VoterService : IVoterService
    {
        #region variable declaration        
        /// <summary>
        /// The i water repository
        /// </summary>
        private readonly IVoterRepository iVoterRepository;

        /// <summary>
        /// The API URL
        /// </summary>
        private string apiURL = string.Empty;

        /// <summary>
        /// The response
        /// </summary>
        private Response response = null;
        #endregion

        #region constructor         
        /// <summary>
        /// Initializes a new instance of the <see cref="VoterService"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public VoterService(string connection, string apiURL)
        {
            iVoterRepository = new VoterRepository(connection);
            this.apiURL = apiURL;
        }
        #endregion

        #region service

        #region get voter list mobile
        /// <summary>
        /// Gets the voter list mobile.
        /// </summary>
        /// <param name="voterReqModel">The voter req model.</param>
        /// <returns>
        /// return voter list
        /// </returns>
        public async Task<Response> GetVoterListMobile(VoterRequestModel voterReqModel)
        {
            var res = new Response();
            try
            {
                var voters = await iVoterRepository.GetVoterListMobile(voterReqModel);
                if (voters != null && voters.ListItems != null)
                {
                    foreach (var item in voters.ListItems)
                    {
                        item.SetImageUrl(this.apiURL);
                        item.SetFormImageUrl(this.apiURL);
                    }
                }

                res.Data = voters;
                res.Status = true;
                res.Message = "Success";
            }
            catch (Exception ex)
            {
                res.Message = ResponseMessage.SomethingWentWrong;
                res.Data = null;
                res.Status = false;
                ErrorLog.Write("VoterService", "GetVoterListMobile", ex);
            }
            return res;
        }

        /// <summary>
        /// Downloads the voter list mobile.
        /// </summary>
        /// <param name="voterReqModel">The voter req model.</param>
        /// <returns></returns>
        public async Task<Response> DownloadVoterListMobile(VoterRequestModel voterReqModel)
        {
            var res = new Response();
            try
            {
                var voters = await iVoterRepository.DownloadVoterListMobile(voterReqModel);
                if (voters != null && voters.ListItems != null && voters.ListItems.Count() > 0)
                {
                    int maxFileRecords = 25000;
                    int fileCounter = 0;
                    int recordCounter = 0;
                    List<VoterModel> lstVoter = new List<VoterModel>();
                    //string filePath = string.Format("{0}/{1}/{2}/{3}/{4}", apiURL, Constants.UploadFolderPath, Constants.ProvinceDownloadFolderPath);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), Constants.UploadFolderPath, Constants.MobileDownloadFolderPath);
                    string districtName;
                    if (voters.ListItems.FirstOrDefault().DistrictName != null)
                    {
                         districtName = CommonFunction.RemoveSpecialCharacters(voters.ListItems.FirstOrDefault().DistrictName);
                    }
                    else
                    {
                         districtName = CommonFunction.RemoveSpecialCharacters(voters.ListItems.FirstOrDefault().MKADistrictName);
                    }
                    string zipFileName = $"{voterReqModel.UserId}{districtName}";
                    if (Directory.Exists(filePath))
                    {
                        foreach (var item in Directory.GetFiles(filePath, $"{zipFileName}*"))
                        {
                            System.IO.File.Delete(item);
                        }
                    }
                    else
                    {
                        Directory.CreateDirectory(filePath);
                    }

                    foreach (var item in voters.ListItems)
                    {
                        item.SetImageUrl(this.apiURL);
                        item.SetFormImageUrl(this.apiURL);


                        lstVoter.Add(item);
                        recordCounter++;
                        if ((maxFileRecords == recordCounter) || (((fileCounter * maxFileRecords) + recordCounter) == voters.ListItems.Count()))
                        {
                            fileCounter++;
                            if (lstVoter != null && lstVoter.Count > 0)
                            {
                                //var jsonResponse = new Response<VoterModel>();
                                //jsonResponse.Status = true;
                                //jsonResponse.Data = new Result<VoterModel>();
                                //jsonResponse.Data.ListItems = lstVoter;

                                var jsonResponse = new Result<VoterModel>();
                                jsonResponse.ListItems = lstVoter;
                                //using (TextWriter writer = System.IO.File.CreateText($"{filePath}/{CommonFunction.RemoveSpecialCharacters(item.DistrictName)}_{fileCounter}.json"))
                                //{
                                //    var serializer = new JsonSerializer();                                    
                                //    serializer.Serialize(writer, jsonResponse);
                                //}

                                string JSONresult = JsonConvert.SerializeObject(jsonResponse, new JsonSerializerSettings { ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver() });
                                if (item.DistrictName != null)
                                {
                                    using (var tw = new StreamWriter($"{filePath}/{CommonFunction.RemoveSpecialCharacters(item.DistrictName)}_{fileCounter}.json", false))
                                    {
                                        tw.WriteLine(JSONresult.ToString());
                                        tw.Close();
                                    }
                                }
                                else
                                {
                                    using (var tw = new StreamWriter($"{filePath}/{CommonFunction.RemoveSpecialCharacters(item.MKADistrictName)}_{fileCounter}.json", false))
                                    {
                                        tw.WriteLine(JSONresult.ToString());
                                        tw.Close();
                                    }
                                }

                                jsonResponse = null;
                            }


                            recordCounter = 0;
                            lstVoter.Clear();
                        }
                    }

                    CommonFunction.CreateZipFile($"{filePath}\\{zipFileName}.zip", Directory.GetFiles(filePath, $"{districtName}_*"));
                    //res.Message = $"{filePath}\\{zipFileName}.zip";
                    res.Message = $"{apiURL}/{Constants.UploadFolderPath}/{Constants.MobileDownloadFolderPath}/{zipFileName}.zip";

                    try
                    {
                        if (Directory.Exists(filePath))
                        {
                            foreach (var item in Directory.GetFiles(filePath, $"{districtName}_*"))
                            {
                                System.IO.File.Delete(item);
                            }

                            foreach (var item in Directory.GetFiles(filePath))
                            {
                                FileInfo fi = new FileInfo(item);
                                if (fi.LastAccessTime < DateTime.Now.AddHours(-6))
                                {
                                    fi.Delete();
                                }
                            }
                        }
                    }
                    catch
                    { }
                }
                
                res.Status = true;
            }
            catch (Exception ex)
            {
                res.Message = ResponseMessage.SomethingWentWrong;
                res.Data = null;
                res.Status = false;
                ErrorLog.Write("VoterService", "DownloadVoterListMobile", ex);
            }
            return res;
        }

        #endregion

        #region get voter list by province mobile
        /// <summary>
        /// Gets the voter list by province mobile.
        /// </summary>
        /// <param name="voterReqModel">The voter req model.</param>
        /// <returns></returns>
        public async Task<Response> GetVoterListByProvinceMobile(VoterRequestModel voterReqModel)
        {
            var res = new Response();
            try
            {
                var voters = await iVoterRepository.GetVoterListByProvinceMobile(voterReqModel);
                if (voters != null && voters.ListItems != null)
                {
                    foreach (var item in voters.ListItems)
                    {
                        item.SetImageUrl(this.apiURL);
                        item.SetFormImageUrl(this.apiURL);
                    }
                }
                res.Data = voters;
                res.Status = true;
                res.Message = "Success";
            }
            catch (Exception ex)
            {
                res.Message = ResponseMessage.SomethingWentWrong;
                res.Data = null;
                res.Status = false;
                ErrorLog.Write("VoterService", "GetVoterListByProvinceMobile", ex);
            }
            return res;
        }
        #endregion

        #region get rejected voter list mobile
        /// <summary>
        /// Gets the rejected voter list mobile.
        /// </summary>
        /// <param name="voterReqModel">The voter req model.</param>
        /// <returns>
        /// return rejected voter list
        /// </returns>
        public async Task<Response> GetRejectedVoterListMobile(VoterRequestModel voterReqModel)
        {
            var res = new Response();
            try
            {
                var voters = await iVoterRepository.GetRejectedVoterListMobile(voterReqModel);
                if (voters != null && voters.ListItems != null)
                {
                    foreach (var item in voters.ListItems)
                    {
                        item.SetImageUrl(this.apiURL);
                        item.SetFormImageUrl(this.apiURL);
                    }
                }

                res.Data = voters;
                res.Status = true;
                res.Message = "Success";
            }
            catch (Exception ex)
            {
                res.Message = ResponseMessage.SomethingWentWrong;
                res.Data = null;
                res.Status = false;
                ErrorLog.Write("VoterService", "GetRejectedVoterListMobile", ex);
            }
            return res;
        }
        #endregion

        #region get transfer in voter list
        /// <summary>
        /// Gets the transfer in voter list.
        /// </summary>
        /// <param name="transferInVoterRequestModel">The transfer in voter request model.</param>
        /// <returns>
        /// return voter list
        /// </returns>
        public async Task<Response<VoterModel>> GetTransferInVoterList(TransferInVoterRequestModel transferInVoterRequestModel)
        {
            var response = new Response<VoterModel>();

            try
            {

                var result = await iVoterRepository.GetTransferInVoterList(transferInVoterRequestModel);
                if (result != null && result.ListItems != null)
                {
                    foreach (var item in result.ListItems)
                    {
                        item.VoterName = item.FName + " " + item.LName;
                        item.SetImageUrl(apiURL);
                        item.SetFormImageUrl(apiURL);
                    }

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
                ErrorLog.Write("VoterService", "GetTransferInVoterList", ex);
            }
            return response;
        }
        #endregion

        #region sync electors
        /// <summary>
        /// Synchronizes the electors.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// return sync status
        /// </returns>
        public async Task<Response> SyncElectors(List<VoterModel> model)
        {
            try
            {
                bool isLogEnable = Convert.ToBoolean(SettingsConfig.AppSetting("LogEnable", "base"));
                if (isLogEnable)
                {
                    ErrorLog.WriteLog("LogService", JsonConvert.SerializeObject(model));
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Write("VoterService", "SyncElectors -> create log", ex);
            }

            List<VoterModel> notSyncedVoterList = new List<VoterModel>();
            response = new Response();
            int countOfNotSyncRecords = 0;

            try
            {
                if (model != null && model.Count > 0)
                {
                    foreach (var item in model)
                    {
                        try
                        {

                            long result = await iVoterRepository.SyncElectors(item);

                            #region unused code
                            ////if (item.ElectorProfileImageFile != null)
                            ////{
                            ////    item.ElectorProfileImage = $"{Guid.NewGuid().ToString("N")}{Path.GetExtension(item.ElectorProfileImageFile.FileName)}";
                            ////}

                            ////if (item.ElectorFormImageFile != null)
                            ////{
                            ////    item.ElectorFormImage = $"{Guid.NewGuid().ToString("N")}{Path.GetExtension(item.ElectorFormImageFile.FileName)}";
                            ////}

                            ////var electorId = await iVoterRepository.SyncElectors(item);

                            ////if (electorId > 0)
                            ////{
                            ////    try
                            ////    {
                            ////        if (item.ElectorProfileImageFile != null)
                            ////        {
                            ////            if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), Constants.UploadFolderPath, Constants.ElectorProfileImageFolderPath)))
                            ////            {
                            ////                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), Constants.UploadFolderPath, Constants.ElectorProfileImageFolderPath));
                            ////            }

                            ////            if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), Constants.UploadFolderPath, Constants.ElectorProfileImageFolderPath, electorId.ToString())))
                            ////            {
                            ////                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), Constants.UploadFolderPath, Constants.ElectorProfileImageFolderPath, electorId.ToString()));
                            ////            }

                            ////            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), Constants.UploadFolderPath, Constants.ElectorProfileImageFolderPath, electorId.ToString());

                            ////            using (var fileStream = new FileStream(Path.Combine(folderPath, item.ElectorProfileImage), FileMode.Create))
                            ////            {
                            ////                await item.ElectorProfileImageFile.CopyToAsync(fileStream);
                            ////                await fileStream.FlushAsync();
                            ////            }
                            ////        }
                            ////    }
                            ////    catch (Exception ex)
                            ////    {
                            ////        ErrorLog.Write("VoterService", "SyncElectors -> Save elector profile image", ex);
                            ////    }

                            ////    try
                            ////    {
                            ////        if (item.ElectorFormImageFile != null)
                            ////        {
                            ////            if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), Constants.UploadFolderPath, Constants.ElectorFormImageFolderPath)))
                            ////            {
                            ////                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), Constants.UploadFolderPath, Constants.ElectorFormImageFolderPath));
                            ////            }

                            ////            if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), Constants.UploadFolderPath, Constants.ElectorFormImageFolderPath, electorId.ToString())))
                            ////            {
                            ////                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), Constants.UploadFolderPath, Constants.ElectorFormImageFolderPath, electorId.ToString()));
                            ////            }

                            ////            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), Constants.UploadFolderPath, Constants.ElectorFormImageFolderPath, electorId.ToString());

                            ////            using (var fileStream = new FileStream(Path.Combine(folderPath, item.ElectorFormImage), FileMode.Create))
                            ////            {
                            ////                await item.ElectorFormImageFile.CopyToAsync(fileStream);
                            ////                await fileStream.FlushAsync();
                            ////            }
                            ////        }                                
                            ////    }
                            ////    catch (Exception ex)
                            ////    {
                            ////        ErrorLog.Write("VoterService", "SyncElectors -> Save elector form image", ex);
                            ////    }
                            ////}
                            #endregion

                            if (result == -1)
                            {
                                countOfNotSyncRecords++;
                                item.ErrorMessage = ResponseMessage.EnrolmentFormError_Validation;
                                notSyncedVoterList.Add(item);
                            }
                            else if (result == -2)
                            {
                                countOfNotSyncRecords++;
                                item.ErrorMessage = ResponseMessage.EnrolmentFormNotExistError_Validation;
                                notSyncedVoterList.Add(item);
                            }
                            else if (result == -3)
                            {
                                countOfNotSyncRecords++;
                                item.ErrorMessage = ResponseMessage.ElectorNotExist_Validation;
                                notSyncedVoterList.Add(item);
                            }
                            else if (result == -4)
                            {
                                countOfNotSyncRecords++;
                                item.ErrorMessage = string.Format(ResponseMessage.LocationNotMatchError_Validation,Convert.ToString(item.LocationId), Convert.ToString(item.OldLocationId));
                                notSyncedVoterList.Add(item);
                            }
                            else if(result == -5)
                            {
                                countOfNotSyncRecords++;
                                item.ErrorMessage = ResponseMessage.ElectorOtherDataMismatch_Validation;
                                notSyncedVoterList.Add(item);
                            }
                        }
                        catch (Exception ex)
                        {
                            countOfNotSyncRecords++;
                            if (ex.Message.Contains("Violation of PRIMARY KEY constraint 'PK_tblRoll'"))
                            {
                                item.ErrorMessage = "Failed to insert Voter Record - ElectorId already exists in the system";
                            }
                            else
                            {
                                item.ErrorMessage = ex.Message;
                            }

                            notSyncedVoterList.Add(item);
                            ErrorLog.Write("VoterService", "SyncElectors", ex);
                            ErrorLog.WriteLog("VoterService", $"SyncElectors - Voter Request Model: { JsonConvert.SerializeObject(model)}");
                        }
                    }

                    if (countOfNotSyncRecords > 0)
                    {
                        response.Message = $"{countOfNotSyncRecords} record(s) are not synced out of {model.Count} record(s)";
                        response.Data = notSyncedVoterList;
                    }
                    else
                    {
                        response.Message = "All records are successfully synced";
                    }

                    response.Status = true;
                }
                else
                {
                    response.Status = false;
                    response.Message = ResponseMessage.SomethingWentWrong;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Write("VoterService", "SyncElectors", ex);
            }

            return response;
        }
        #endregion

        #region sync electors images
        /// <summary>
        /// Synchronizes the electors.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// return sync status
        /// </returns>
        public async Task<Response> SyncElectorsImages(VoterModel model)
        {
            Response response = new Response();
            string errMsg = string.Empty;
            if (model != null)
            {
                try
                {
                    if (model.ElectorProfileImageFile != null)
                    {
                        model.ElectorProfileImage = $"{Guid.NewGuid().ToString("N")}{Path.GetExtension(model.ElectorProfileImageFile.FileName)}";
                    }

                    if (model.ElectorFormImageFile != null)
                    {
                        model.ElectorFormImage = $"{Guid.NewGuid().ToString("N")}{Path.GetExtension(model.ElectorFormImageFile.FileName)}";
                    }

                    var electorId = await iVoterRepository.SyncElectorsImages(model);

                    if (electorId > 0)
                    {
                        try
                        {
                            if (model.ElectorProfileImageFile != null)
                            {
                                if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), Constants.UploadFolderPath, Constants.ElectorProfileImageFolderPath)))
                                {
                                    Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), Constants.UploadFolderPath, Constants.ElectorProfileImageFolderPath));
                                }

                                if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), Constants.UploadFolderPath, Constants.ElectorProfileImageFolderPath, electorId.ToString())))
                                {
                                    Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), Constants.UploadFolderPath, Constants.ElectorProfileImageFolderPath, electorId.ToString()));
                                }

                                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), Constants.UploadFolderPath, Constants.ElectorProfileImageFolderPath, electorId.ToString());

                                using (var fileStream = new FileStream(Path.Combine(folderPath, model.ElectorProfileImage), FileMode.Create))
                                {
                                    await model.ElectorProfileImageFile.CopyToAsync(fileStream);
                                    await fileStream.FlushAsync();
                                    if (model.Status == "TRANSFERRED IN")
                                    {
                                        var newfolderPath = Path.Combine(Directory.GetCurrentDirectory(), Constants.UploadFolderPath, Constants.ElectorProfileImageFolderPath, "99" + electorId.ToString());
                                        if (!Directory.Exists(newfolderPath))
                                        {
                                            Directory.CreateDirectory(newfolderPath);
                                        }

                                        System.IO.File.Copy(Path.Combine(folderPath, model.ElectorProfileImage), Path.Combine(newfolderPath, model.ElectorProfileImage), true);
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorLog.Write("VoterService", "SyncElectorsImages -> Save elector profile image", ex);
                            ErrorLog.WriteLog("VoterService", $"SyncElectorsImages - Voter Request Model: {JsonConvert.SerializeObject(model)}");
                            errMsg = "Error into save elector profile image -" + ex.Message;
                        }

                        try
                        {
                            if (model.ElectorFormImageFile != null)
                            {
                                if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), Constants.UploadFolderPath, Constants.ElectorFormImageFolderPath)))
                                {
                                    Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), Constants.UploadFolderPath, Constants.ElectorFormImageFolderPath));
                                }

                                if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), Constants.UploadFolderPath, Constants.ElectorFormImageFolderPath, electorId.ToString())))
                                {
                                    Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), Constants.UploadFolderPath, Constants.ElectorFormImageFolderPath, electorId.ToString()));
                                }

                                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), Constants.UploadFolderPath, Constants.ElectorFormImageFolderPath, electorId.ToString());

                                using (var fileStream = new FileStream(Path.Combine(folderPath, model.ElectorFormImage), FileMode.Create))
                                {
                                    await model.ElectorFormImageFile.CopyToAsync(fileStream);
                                    await fileStream.FlushAsync();

                                    if (model.Status == "TRANSFERRED IN")
                                    {
                                        var newfolderPath = Path.Combine(Directory.GetCurrentDirectory(), Constants.UploadFolderPath, Constants.ElectorFormImageFolderPath, "99" + electorId.ToString());
                                        if (!Directory.Exists(newfolderPath))
                                        {
                                            Directory.CreateDirectory(newfolderPath);
                                        }

                                        System.IO.File.Copy(Path.Combine(folderPath, model.ElectorFormImage), Path.Combine(newfolderPath, model.ElectorFormImage), true);
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorLog.Write("VoterService", "SyncElectorsImages -> Save elector form image", ex);
                            errMsg = errMsg + "  Error into save elector form image -" + ex.Message;
                        }
                    }
                }
                catch (Exception ex)
                {
                    ErrorLog.Write("VoterService", "SyncElectorsImages", ex);
                    errMsg = errMsg + "  Error into SyncElectorsImages -" + ex.Message;
                }
                if (!string.IsNullOrEmpty(errMsg))
                {
                    response.Message = errMsg;
                    response.Status = false;
                }
                else
                {
                    response.Message = "Voter images are successfully synced";
                    response.Status = true;
                }
            }
            else
            {
                response.Status = false;
                response.Message = ResponseMessage.SomethingWentWrong;
            }

            return response;
        }
        #endregion

        #region sync electors and images
        /// <summary>
        /// Synchronizes the electors and images.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Response> SyncElectorsAndImages(List<VoterModel> model)
        {
            List<VoterModel> notSyncedVoterList = new List<VoterModel>();
            Response response = new Response();
            int countOfNotSyncRecords = 0;
            if (model != null && model.Count > 0)
            {
                try
                {
                    bool isLogEnable = Convert.ToBoolean(SettingsConfig.AppSetting("LogEnable", "base"));
                    if (isLogEnable)
                    {
                        ErrorLog.WriteLog("LogService", JsonConvert.SerializeObject(model));
                        ErrorLog.WriteLog("VoterService", $"SyncElectorsAndImages - if (model != null && model.Count > 0) - isLogEnable: {JsonConvert.SerializeObject(model)}");
                    }                    
                }
                catch (Exception ex)
                {
                    ErrorLog.Write("VoterService", "SyncElectorsAndImages -> create log", ex);
                    ErrorLog.WriteLog("VoterService", $"SyncElectorsAndImages - model != null && model.Count > 0: {JsonConvert.SerializeObject(model)}");
                }

                foreach (var item in model)
                {
                    try
                    {
                        if (item.ElectorProfileImageFile != null)
                        {
                            item.ElectorProfileImage = $"{Guid.NewGuid().ToString("N")}{Path.GetExtension(item.ElectorProfileImageFile.FileName)}";
                        }

                        if (item.ElectorFormImageFile != null)
                        {
                            item.ElectorFormImage = $"{Guid.NewGuid().ToString("N")}{Path.GetExtension(item.ElectorFormImageFile.FileName)}";
                        }

                        var electorId = await iVoterRepository.SyncElectors(item);

                        if (electorId > 0)
                        {
                            try
                            {
                                if (item.ElectorProfileImageFile != null)
                                {
                                    if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), Constants.UploadFolderPath, Constants.ElectorProfileImageFolderPath)))
                                    {
                                        Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), Constants.UploadFolderPath, Constants.ElectorProfileImageFolderPath));
                                    }

                                    if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), Constants.UploadFolderPath, Constants.ElectorProfileImageFolderPath, electorId.ToString())))
                                    {
                                        Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), Constants.UploadFolderPath, Constants.ElectorProfileImageFolderPath, electorId.ToString()));
                                    }

                                    var folderPath = Path.Combine(Directory.GetCurrentDirectory(), Constants.UploadFolderPath, Constants.ElectorProfileImageFolderPath, electorId.ToString());

                                    using (var fileStream = new FileStream(Path.Combine(folderPath, item.ElectorProfileImage), FileMode.Create))
                                    {
                                        await item.ElectorProfileImageFile.CopyToAsync(fileStream);
                                        await fileStream.FlushAsync();

                                        if (item.Status == "TRANSFERRED IN")
                                        {
                                            var newfolderPath = Path.Combine(Directory.GetCurrentDirectory(), Constants.UploadFolderPath, Constants.ElectorProfileImageFolderPath, "99" + electorId.ToString());
                                            if (!Directory.Exists(newfolderPath))
                                            {
                                                Directory.CreateDirectory(newfolderPath);
                                            }

                                            System.IO.File.Copy(Path.Combine(folderPath, item.ElectorProfileImage), Path.Combine(newfolderPath, item.ElectorProfileImage), true);
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                ErrorLog.Write("VoterService", "SyncElectorsAndImages -> Save elector profile image", ex);
                                ErrorLog.WriteLog("VoterService", $"SyncElectorsAndImages - if (item.ElectorProfileImageFile != null): {JsonConvert.SerializeObject(model)}");
                            }

                            try
                            {
                                if (item.ElectorFormImageFile != null)
                                {
                                    if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), Constants.UploadFolderPath, Constants.ElectorFormImageFolderPath)))
                                    {
                                        Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), Constants.UploadFolderPath, Constants.ElectorFormImageFolderPath));
                                    }

                                    if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), Constants.UploadFolderPath, Constants.ElectorFormImageFolderPath, electorId.ToString())))
                                    {
                                        Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), Constants.UploadFolderPath, Constants.ElectorFormImageFolderPath, electorId.ToString()));
                                    }

                                    var folderPath = Path.Combine(Directory.GetCurrentDirectory(), Constants.UploadFolderPath, Constants.ElectorFormImageFolderPath, electorId.ToString());

                                    using (var fileStream = new FileStream(Path.Combine(folderPath, item.ElectorFormImage), FileMode.Create))
                                    {
                                        await item.ElectorFormImageFile.CopyToAsync(fileStream);
                                        await fileStream.FlushAsync();

                                        if (item.Status == "TRANSFERRED IN")
                                        {
                                            var newfolderPath = Path.Combine(Directory.GetCurrentDirectory(), Constants.UploadFolderPath, Constants.ElectorFormImageFolderPath, "99" + electorId.ToString());
                                            if (!Directory.Exists(newfolderPath))
                                            {
                                                Directory.CreateDirectory(newfolderPath);
                                            }

                                            System.IO.File.Copy(Path.Combine(folderPath, item.ElectorFormImage), Path.Combine(newfolderPath, item.ElectorFormImage), true);
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                ErrorLog.Write("VoterService", "SyncElectorsAndImages -> Save elector form image", ex);
                                ErrorLog.WriteLog("VoterService", $"SyncElectorsAndImages - if (item.ElectorFormImageFile != null): {JsonConvert.SerializeObject(model)}");
                            }

                        }
                        else if (electorId == -1)
                        {
                            countOfNotSyncRecords++;
                            item.ErrorMessage = ResponseMessage.EnrolmentFormError_Validation;
                            notSyncedVoterList.Add(item);
                        }
                        else if (electorId == -2)
                        {
                            countOfNotSyncRecords++;
                            item.ErrorMessage = ResponseMessage.EnrolmentFormNotExistError_Validation;
                            notSyncedVoterList.Add(item);
                        }
                        else if (electorId == -3)
                        {
                            countOfNotSyncRecords++;
                            item.ErrorMessage = ResponseMessage.ElectorNotExist_Validation;
                            notSyncedVoterList.Add(item);
                        }
                        else if (electorId == -4)
                        {
                            countOfNotSyncRecords++;
                            item.ErrorMessage = ResponseMessage.LocationNotMatchError_Validation;
                            notSyncedVoterList.Add(item);
                        }
                        else if (electorId == -5)
                        {
                            countOfNotSyncRecords++;
                            item.ErrorMessage = ResponseMessage.ElectorOtherDataMismatch_Validation;
                            notSyncedVoterList.Add(item);
                        }
                    }
                    catch (Exception ex)
                    {
                        countOfNotSyncRecords++;
                        notSyncedVoterList.Add(item);
                        ErrorLog.Write("VoterService", "SyncElectorsAndImages", ex);
                        ErrorLog.WriteLog("VoterService", $"SyncElectorsAndImages - foreach (var item in model): {JsonConvert.SerializeObject(model)}");
                    }
                }

                if (countOfNotSyncRecords > 0)
                {
                    response.Message = $"{countOfNotSyncRecords} record(s) are not synced out of {model.Count} record(s)";
                    response.Data = notSyncedVoterList;
                }
                else
                {
                    try
                    {
                        bool isLogEnable = Convert.ToBoolean(SettingsConfig.AppSetting("LogEnable", "base"));
                        if (isLogEnable)
                        {
                            ErrorLog.WriteLog("LogService", JsonConvert.SerializeObject(model));
                            ErrorLog.WriteLog("VoterService", $"SyncElectorsAndImages - if (countOfNotSyncRecords > 0) else - isLogEnable : {JsonConvert.SerializeObject(model)}");
                        }
                    }
                    catch (Exception ex)
                    {
                        ErrorLog.Write("VoterService", "SyncElectorsAndImages -> test", ex);
                        ErrorLog.WriteLog("VoterService", $"SyncElectorsAndImages - if (countOfNotSyncRecords > 0): {JsonConvert.SerializeObject(model)}");
                    }

                    response.Message = "All records with images are successfully synced";
                }

                response.Status = true;
            }
            else
            {
                response.Status = false;
                response.Message = ResponseMessage.SomethingWentWrong;
            }

            return response;
        }
        #endregion

        #region import biodata details
        /// <summary>
        /// Imports the bio data details.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Response> ImportBioDataDetails(List<VoterModel> model)
        {
            string ElectorProfileImage = string.Empty;
            string ElectorSignatureImage = string.Empty;
            List<VoterModel> notSyncedVoterList = new List<VoterModel>();
            Response response = new Response();
            int countOfNotSyncRecords = 0;
            if (model != null && model.Count > 0)
            {
                foreach (var item in model)
                {
                    try
                    {
                        if (!string.IsNullOrWhiteSpace(item.ElectorProfileImage))
                        {
                            ElectorProfileImage = item.ElectorProfileImage;
                            item.ElectorProfileImage = $"{Guid.NewGuid().ToString("N")}{".jpg"}";
                        }
                        if (!string.IsNullOrWhiteSpace(item.ElectorSignatureImage))
                        {
                            ElectorSignatureImage = item.ElectorSignatureImage;
                            item.ElectorSignatureImage = $"{Guid.NewGuid().ToString("N")}{".jpg"}";
                        }

                        var electorId = await iVoterRepository.ImportBioDataDetails(item);

                        if (electorId > 0)
                        {
                            try
                            {
                                if (!string.IsNullOrWhiteSpace(item.ElectorProfileImage))
                                {
                                    if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), Constants.UploadFolderPath, Constants.ElectorProfileImageFolderPath)))
                                    {
                                        Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), Constants.UploadFolderPath, Constants.ElectorProfileImageFolderPath));
                                    }

                                    if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), Constants.UploadFolderPath, Constants.ElectorProfileImageFolderPath, electorId.ToString())))
                                    {
                                        Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), Constants.UploadFolderPath, Constants.ElectorProfileImageFolderPath, electorId.ToString()));
                                    }

                                    var folderPath = Path.Combine(Directory.GetCurrentDirectory(), Constants.UploadFolderPath, Constants.ElectorProfileImageFolderPath, electorId.ToString());

                                    byte[] imageBytes = Convert.FromBase64String(ElectorProfileImage);
                                    System.IO.File.WriteAllBytes(Path.Combine(folderPath, item.ElectorProfileImage), imageBytes);
                                }
                                if (!string.IsNullOrWhiteSpace(item.ElectorSignatureImage))
                                {
                                    if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), Constants.UploadFolderPath, Constants.ElectorSignatureImageFolderPath)))
                                    {
                                        Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), Constants.UploadFolderPath, Constants.ElectorSignatureImageFolderPath));
                                    }

                                    if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), Constants.UploadFolderPath, Constants.ElectorSignatureImageFolderPath, electorId.ToString())))
                                    {
                                        Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), Constants.UploadFolderPath, Constants.ElectorSignatureImageFolderPath, electorId.ToString()));
                                    }

                                    var folderPath = Path.Combine(Directory.GetCurrentDirectory(), Constants.UploadFolderPath, Constants.ElectorSignatureImageFolderPath, electorId.ToString());

                                    byte[] imageBytes = Convert.FromBase64String(ElectorSignatureImage);
                                    System.IO.File.WriteAllBytes(Path.Combine(folderPath, item.ElectorSignatureImage), imageBytes);
                                }
                            }
                            catch (Exception ex)
                            {
                                ErrorLog.Write("VoterService", "SyncElectorsImages -> Save elector profile image", ex);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        countOfNotSyncRecords++;
                        notSyncedVoterList.Add(item);
                        ErrorLog.Write("VoterService", "ImportBioDataDetails", ex);
                    }
                }

                if (countOfNotSyncRecords > 0)
                {
                    response.Message = $"{countOfNotSyncRecords} record(s) are not import out of {model.Count} record(s)";
                    response.Data = notSyncedVoterList;
                }
                else
                {
                    response.Message = "All records are successfully imported";
                }

                response.Status = true;
            }
            else
            {
                response.Status = false;
                response.Message = ResponseMessage.SomethingWentWrong;
            }

            return response;
        }
        #endregion

        #region get updated voter list
        /// <summary>
        /// Gets the updated voter list.
        /// </summary>
        /// <param name="transferInVoterRequestModel">The transfer in voter request model.</param>
        /// <returns>
        /// return voter list
        /// </returns>
        public async Task<Response<VoterModel>> GetUpdatedVoterList(TransferInVoterRequestModel transferInVoterRequestModel)
        {
            var response = new Response<VoterModel>();
            try
            {
                var result = await iVoterRepository.GetUpdatedVoterList(transferInVoterRequestModel);
                if (result != null && result.ListItems != null)
                {
                    foreach (var item in result.ListItems)
                    {
                        item.VoterName = item.FName + " " + item.LName;
                        item.SetImageUrl(apiURL);
                        item.SetFormImageUrl(apiURL);
                    }

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
                ErrorLog.Write("VoterService", "GetUpdateVoterList", ex);
            }
            return response;
        }
        #endregion

        #region TransferVoterByRO
        /// <summary>
        /// Transfers the voter by ro.
        /// </summary>
        /// <param name="lstvoter">The lstvoter.</param>
        /// <returns></returns>
        public async Task<Response> TransferVoterByRO(List<VoterModel> lstvoter)
        {
            List<VoterModel> notTransferVoterList = new List<VoterModel>();
            response = new Response();
            int countOfNotSyncRecords = 0;
            if (lstvoter != null && lstvoter.Count > 0)
            {
                foreach (var item in lstvoter)
                {
                    try
                    {
                        int result = await iVoterRepository.TransferVoterByRO(item);
                        if (result <= 0)
                        {
                            countOfNotSyncRecords++;
                            notTransferVoterList.Add(item);
                        }
                    }
                    catch (Exception ex)
                    {
                        countOfNotSyncRecords++;
                        notTransferVoterList.Add(item);

                        response.Status = false;
                        response.Message = ResponseMessage.SomethingWentWrong;

                        ErrorLog.Write("VoterService", "TransferVoterByRO", ex);
                    }
                }

                if (countOfNotSyncRecords > 0)
                {
                    response.Message = $"{countOfNotSyncRecords} record(s) are not transfer out of {lstvoter.Count} record(s)";
                    response.Data = notTransferVoterList;
                }
                else
                {
                    response.Message = "All records are successfully transfer";
                }

                response.Status = true;
            }
            else
            {
                response.Status = false;
                response.Message = ResponseMessage.SomethingWentWrong;
            }
            return response;
        }
        #endregion

        #region Unused service method of finger print

        //public async Task<Response> GetFingerPrintByte()
        //{
        //    var res = new Response();
        //    try
        //    {
        //        var fingerBytes = await iVoterRepository.GetFingerPrintByte();
        //        if (fingerBytes != null && fingerBytes.ListItems != null)
        //        {
        //            foreach (var item in fingerBytes.ListItems)
        //            {
        //                byte[] fingerByte = item.ElectorFingerPrintImageByte;
        //                string decrypted = Encoding.UTF8.GetString(fingerByte);
        //                if (String.Equals(decrypted, item.ElectorFingerPrintImage))
        //                {
        //                    item.decryptedFingerPrint = decrypted;
        //                }
        //                item.SetSignatureImageUrl(this.apiURL);
        //            }
        //        }

        //        res.Data = fingerBytes;
        //        res.Status = true;
        //        res.Message = "Success";
        //    }
        //    catch (Exception ex)
        //    {
        //        res.Message = ResponseMessage.SomethingWentWrong;
        //        res.Data = null;
        //        res.Status = false;
        //        ErrorLog.Write("VoterService", "GetVoterListMobile", ex);
        //    }
        //    return res;
        //}
        #endregion

        #region get voter list mobile

        /// <summary>
        /// Gets the preliminary voter list mobile.
        /// </summary>
        /// <param name="voterReqModel">The voter req model.</param>
        /// <returns>return voter list</returns>
        public async Task<Response> GetPreliminaryVoterListMobile(VoterRequestModel voterReqModel)
        {
            var res = new Response();
            try
            {
                var voters = await iVoterRepository.GetPreliminaryVoterListMobile(voterReqModel);
                if (voters != null && voters.ListItems != null)
                {
                    foreach (var item in voters.ListItems)
                    {
                        item.SetImageUrl(this.apiURL);
                        item.SetFormImageUrl(this.apiURL);
                    }
                }

                res.Data = voters;
                res.Status = true;
                res.Message = "Success";
            }
            catch (Exception ex)
            {
                res.Message = ResponseMessage.SomethingWentWrong;
                res.Data = null;
                res.Status = false;
                ErrorLog.Write("VoterService", "GetPreliminaryVoterListMobile", ex);
            }
            return res;
        }
        #endregion
        #endregion
    }
}
