using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PNGEC.API.Domain;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Domain.Mobile.Voter;
using PNGEC.API.Domain.Report;
using PNGEC.API.Helper;
using PNGEC.API.Repository.File;

namespace PNGEC.API.Service.File
{
    public class FileService : IFileService
    {
        #region variable declaration
        /// <summary>
        /// The i file repository
        /// </summary>
        private readonly IFileRepository iFileRepository;

        /// <summary>
        /// The response
        /// </summary>
        private Response response = null;

        /// <summary>
        /// The lstresponse
        /// </summary>
        private Response<VoterModel> lstresponse = null;

        /// <summary>
        /// The lstresponse
        /// </summary>
        private Response<MKAVoterModel> lstvoterresponse = null;

        /// <summary>
        /// The result
        /// </summary>
        private Result<VoterModel> result = null;

        /// <summary>
        /// The result
        /// </summary>
        private Result<MKAVoterModel> resultvoter = null;

        /// <summary>
        /// The API URL
        /// </summary>
        private string apiURL = string.Empty;

        /// <summary>
        /// The lstresponse ward roll report
        /// </summary>
        private Response<WardRollBookReortModel> lstresponseWardRollReport = null;

        /// <summary>
        /// The result ward roll book
        /// </summary>
        private Result<WardRollBookReortModel> resultWardRollBook = null;

        /// <summary>
        /// The lstresponse ward roll report
        /// </summary>
        private Response<WardRollBookCsvReportModel> lstresponseWardRollCsvReport = null;

        /// <summary>
        /// The result ward roll book
        /// </summary>
        private Result<WardRollBookCsvReportModel> resultWardRollBookCsv = null;

        /// <summary>
        /// The resultmkawardclansectionsummary
        /// </summary>
        private Result<GetMKAWardClanSectionSummaryModel> resultmkawardclansectionsummary = null;

        /// <summary>
        /// The resultmkawardclansectionsummary
        /// </summary>
        private Result<GetWardClanSectionSummaryModel> resultwardclansectionsummary = null;

        /// <summary>
        /// The lstresponse mka ward clan section summary report
        /// </summary>
        private Response<GetMKAWardClanSectionSummaryModel> lstresponseMKAWardClanSectionSummaryReport = null;

        /// <summary>
        /// The lstresponse ward clan section summary report
        /// </summary>
        private Response<GetWardClanSectionSummaryModel> lstresponseWardClanSectionSummaryReport = null;


        #endregion

        #region constructor 
        /// <summary>
        /// Initializes a new instance of the <see cref="FileService"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public FileService(string connection, string apiURL)
        {
            iFileRepository = new FileRepository(connection);
            this.apiURL = apiURL;
        }
        #endregion

        #region service

        #region get voter list
        /// <summary>
        /// Gets the voter list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Response<VoterModel>> GetVoterList(PageList model)
        {
            lstresponse = new Response<VoterModel>();

            try
            {
                result = await iFileRepository.GetVoterList(model);

                if (result != null && result.ListItems != null)
                {
                    foreach (var item in result.ListItems)
                    {
                        item.VoterName = item.LName + " " + item.FName;
                        item.DuplicateVoterName = item.DuplicateLName + " " + item.DuplicateFName;
                        item.SetImageUrl(this.apiURL);
                        item.SetFormImageUrl(this.apiURL);
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
                ErrorLog.Write("FileService", "GetVoterList", ex);
            }

            return lstresponse;
        }
        #endregion

        #region get voter list
        /// <summary>
        /// Gets the voter list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Response<MKAVoterModel>> GetMKAVoterList(PageList model)
        {
            lstvoterresponse = new Response<MKAVoterModel>();

            try
            {
                resultvoter = await iFileRepository.GetMKAVoterList(model);

                if (resultvoter != null && resultvoter.ListItems != null)
                {
                    foreach (var item in resultvoter.ListItems)
                    {
                        
                        item.VoterName = item.LName + " " + item.FName;
                        item.DuplicateVoterName = item.DuplicateLName + " " + item.DuplicateFName;
                        item.SetImageUrl(this.apiURL);
                        item.SetFormImageUrl(this.apiURL);
                    }

                    lstvoterresponse.Data = resultvoter;
                    lstvoterresponse.Status = true;
                }
                else
                {
                    lstvoterresponse.Message = ResponseMessage.RecordNotExist;
                }
            }
            catch (Exception ex)
            {
                lstresponse.Message = ex.Message;
                ErrorLog.Write("FileService", "GetVoterList", ex);
            }

            return lstvoterresponse;
        }
        #endregion

        #region get export records list
        /// <summary>
        /// Gets the export records list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Response<WardRollBookReortModel>> GetExportRecordsList(PageList model)
        {
            lstresponseWardRollReport = new Response<WardRollBookReortModel>();

            try
            {
                resultWardRollBook = await iFileRepository.GetExportRecordsList(model);

                if (resultWardRollBook != null && resultWardRollBook.ListItems != null)
                {
                    foreach (var item in resultWardRollBook.ListItems)
                    {
                        item.SetImageUrl(this.apiURL);
                    }

                    lstresponseWardRollReport.Data = resultWardRollBook;
                    lstresponseWardRollReport.Status = true;
                }
                else
                {
                    lstresponse.Message = ResponseMessage.RecordNotExist;
                }
            }
            catch (Exception ex)
            {
                lstresponse.Message = ResponseMessage.SomethingWentWrong;
                lstresponse.Data = null;
                lstresponse.Status = false;
                ErrorLog.Write("FileService", "GetExportRecordsList", ex);
            }

            return lstresponseWardRollReport;
        }
        #endregion

        #region get export csv records list
        /// <summary>
        /// Gets the export CSV records list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Response<WardRollBookCsvReportModel>> GetExportCsvRecordsList(PageList model)
        {
            lstresponseWardRollCsvReport = new Response<WardRollBookCsvReportModel>();

            try
            {
                resultWardRollBookCsv = await iFileRepository.GetExportCsvRecordsList(model);

                if (resultWardRollBookCsv != null && resultWardRollBookCsv.ListItems != null)
                {
                    lstresponseWardRollCsvReport.Data = resultWardRollBookCsv;
                    lstresponseWardRollCsvReport.Status = true;
                }
                else
                {
                    lstresponse.Message = ResponseMessage.RecordNotExist;
                }
            }
            catch (Exception ex)
            {
                lstresponse.Message = ResponseMessage.SomethingWentWrong;
                lstresponse.Data = null;
                lstresponse.Status = false;
                lstresponse.Message = ex.Message;
                ErrorLog.Write("FileService", "GetExportCsvRecordsList", ex);
            }

            return lstresponseWardRollCsvReport;
        }
        #endregion

        #region get duplicate voter list
        /// <summary>
        /// Gets the duplicate voter list.
        /// </summary>
        /// <param name="voterReqModel">The voter req model.</param>
        /// <returns></returns>
        public async Task<Response> GetDuplicateVoterList(VoterRequestModel voterReqModel)
        {
            var res = new Response();
            try
            {
                var voters = await iFileRepository.GetDuplicateVoterList(voterReqModel);
                if (voters != null && voters.ListItems != null)
                {
                    foreach (var item in voters.ListItems)
                    {
                        item.SetImageUrl(this.apiURL);
                        //item.SetFormImageUrl(this.apiURL);
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
                ErrorLog.Write("FileService", "GetDuplicateVoterList", ex);
            }
            return res;
        }
        #endregion

        #region get tranfer in voter with condition
        /// <summary>
        /// Gets the transfor in voter list.
        /// </summary>
        /// <param name="voterReqModel">The voter req model.</param>
        /// <returns></returns>
        public async Task<Response<WardRollBookReortModel>> GetTranferinVoterWithCondition(PageList model)
        {
            lstresponseWardRollReport = new Response<WardRollBookReortModel>();

            try
            {
                resultWardRollBook = await iFileRepository.GetTranferinVoterWithCondition(model);

                if (resultWardRollBook != null && resultWardRollBook.ListItems != null)
                {
                    foreach (var item in resultWardRollBook.ListItems)
                    {
                        item.SetImageUrl(this.apiURL);
                    }

                    lstresponseWardRollReport.Data = resultWardRollBook;
                    lstresponseWardRollReport.Status = true;
                }
                else
                {
                    lstresponse.Message = ResponseMessage.RecordNotExist;
                }
            }
            catch (Exception ex)
            {
                lstresponse.Message = ResponseMessage.SomethingWentWrong;
                lstresponse.Data = null;
                lstresponse.Status = false;
                ErrorLog.Write("FileService", "GetTranferinVoterWithCondition", ex);
            }

            return lstresponseWardRollReport;
        }
        #endregion

        #region get voterlist by province mobile
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
                var voters = await iFileRepository.GetVoterListByProvinceMobile(voterReqModel);
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
                ErrorLog.Write("FileService", "GetVoterListByProvinceMobile", ex);
            }
            return res;
        }
        #endregion

        #region download voters file by province

        /// <summary>
        /// Downloads the voters file by province.
        /// </summary>
        /// <param name="voterReqModel">The voter req model.</param>
        /// <returns></returns>
        public async Task<Response> DownloadVotersFileByProvince(VoterRequestModel voterReqModel)
        {
            var res = new Response();
            try
            {
                var voters = await iFileRepository.DownloadVotersFileByProvince(voterReqModel);
                if (voters != null && voters.ListItems != null)
                {
                    int maxFileRecords = 25000;
                    int fileCounter = 0;
                    int recordCounter = 0;
                    List<VoterModelOffline> lstVoter = new List<VoterModelOffline>();
                    //string filePath = string.Format("{0}/{1}/{2}/{3}/{4}", apiURL, Constants.UploadFolderPath, Constants.ProvinceDownloadFolderPath);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), Constants.UploadFolderPath, Constants.ProvinceDownloadFolderPath);

                    string provinceName = voters.ListItems.FirstOrDefault().provinceName;

                    if (Directory.Exists(filePath))
                    {
                        foreach (var item in Directory.GetFiles(filePath, $"{provinceName}*"))
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

                        item.VoterName = (!string.IsNullOrWhiteSpace(item.VoterName)) ? item.VoterName.Replace("'", "''") : null;
                        item.lName = (!string.IsNullOrWhiteSpace(item.lName)) ? item.lName.Replace("'", "''") : null;
                        item.fName = (!string.IsNullOrWhiteSpace(item.fName)) ? item.fName.Replace("'", "''") : null;
                        item.gender  = (!string.IsNullOrWhiteSpace(item.gender)) ? item.gender.Replace("'", "''") : null;
                        item.Gen = (!string.IsNullOrWhiteSpace(item.Gen)) ? item.Gen.Replace("'", "''") : null;
                        item.DateOfBirth = (!string.IsNullOrWhiteSpace(item.DateOfBirth)) ? item.DateOfBirth.Replace("'", "''") : null;
                        item.fatherLName = (!string.IsNullOrWhiteSpace(item.fatherLName)) ? item.fatherLName.Replace("'", "''") : null;
                        item.fatherFName = (!string.IsNullOrWhiteSpace(item.fatherFName)) ? item.fatherFName.Replace("'", "''") : null;
                        item.motherLName = (!string.IsNullOrWhiteSpace(item.motherLName)) ? item.motherLName.Replace("'", "''") : null;
                        item.motherFName = (!string.IsNullOrWhiteSpace(item.motherFName)) ? item.motherFName.Replace("'", "''") : null;
                        item.ProvinceOfBirth = (!string.IsNullOrWhiteSpace(item.ProvinceOfBirth)) ? item.ProvinceOfBirth.Replace("'", "''") : null;
                        item.OpenElectorate = (!string.IsNullOrWhiteSpace(item.OpenElectorate)) ? item.OpenElectorate.Replace("'", "''") : null;
                        item.placeOfBirth = (!string.IsNullOrWhiteSpace(item.placeOfBirth)) ? item.placeOfBirth.Replace("'", "''") : null;
                        item.Occupation = (!string.IsNullOrWhiteSpace(item.Occupation)) ? item.Occupation.Replace("'", "''") : null;
                        item.religion = (!string.IsNullOrWhiteSpace(item.religion)) ? item.religion.Replace("'", "''") : null;
                        item.clan = (!string.IsNullOrWhiteSpace(item.clan)) ? item.clan.Replace("'", "''") : null;
                        item.subClan = (!string.IsNullOrWhiteSpace(item.subClan)) ? item.subClan.Replace("'", "''") : null;
                        item.address = (!string.IsNullOrWhiteSpace(item.address)) ? item.address.Replace("'", "''") : null;
                        item.natID = (!string.IsNullOrWhiteSpace(item.natID)) ? item.natID.Replace("'", "''") : null;
                        item.natIDRecordNo = (!string.IsNullOrWhiteSpace(item.natIDRecordNo)) ? item.natIDRecordNo.Replace("'", "''") : null;
                        item.status = (!string.IsNullOrWhiteSpace(item.status)) ? item.status.Replace("'", "''") : null;
                        item.provinceName = (!string.IsNullOrWhiteSpace(item.provinceName)) ? item.provinceName.Replace("'", "''") : null;
                        item.districtName = (!string.IsNullOrWhiteSpace(item.districtName)) ? item.districtName.Replace("'", "''") : null;
                        item.llgName = (!string.IsNullOrWhiteSpace(item.llgName)) ? item.llgName.Replace("'", "''") : null;
                        item.wardName = (!string.IsNullOrWhiteSpace(item.wardName)) ? item.wardName.Replace("'", "''") : null;
                        item.locationName = (!string.IsNullOrWhiteSpace(item.locationName)) ? item.locationName.Replace("'", "''") : null;
                        item.restHouse = (!string.IsNullOrWhiteSpace(item.restHouse)) ? item.restHouse.Replace("'", "''") : null;
                        item.sectionNo = (!string.IsNullOrWhiteSpace(item.sectionNo)) ? item.sectionNo.Replace("'", "''") : null;
                        item.lotNo = (!string.IsNullOrWhiteSpace(item.lotNo)) ? item.lotNo.Replace("'", "''") : null;
                        item.streetName = (!string.IsNullOrWhiteSpace(item.streetName)) ? item.streetName.Replace("'", "''") : null;
                        item.suburbTownSettlement = (!string.IsNullOrWhiteSpace(item.suburbTownSettlement)) ? item.suburbTownSettlement.Replace("'", "''") : null;
                        item.familyNumber = (!string.IsNullOrWhiteSpace(item.familyNumber)) ? item.familyNumber.Replace("'", "''") : null;
                        item.familyName = (!string.IsNullOrWhiteSpace(item.familyName)) ? item.familyName.Replace("'", "''") : null;
                        item.dpo = (!string.IsNullOrWhiteSpace(item.dpo)) ? item.dpo.Replace("'", "''") : null;
                        item.comment = (!string.IsNullOrWhiteSpace(item.comment)) ? item.comment.Replace("'", "''") : null;
                        item.electorProfileImage = (!string.IsNullOrWhiteSpace(item.electorProfileImage)) ? item.electorProfileImage.Replace("'", "''") : null;
                        item.ElectorProfileImageUrl = (!string.IsNullOrWhiteSpace(item.ElectorProfileImageUrl)) ? item.ElectorProfileImageUrl.Replace("'", "''") : null;
                        item.electorFormImage = (!string.IsNullOrWhiteSpace(item.electorFormImage)) ? item.electorFormImage.Replace("'", "''") : null;
                        item.ElectorFormImageUrl = (!string.IsNullOrWhiteSpace(item.ElectorFormImageUrl)) ? item.ElectorFormImageUrl.Replace("'", "''") : null;
                        item.enrolmentFormNumber = (!string.IsNullOrWhiteSpace(item.enrolmentFormNumber)) ? item.enrolmentFormNumber.Replace("'", "''") : null;
                        item.AuditDescription = (!string.IsNullOrWhiteSpace(item.AuditDescription)) ? item.AuditDescription.Replace("'", "''") : null;

                        item.transferInProvinceName = (!string.IsNullOrWhiteSpace(item.transferInProvinceName)) ? item.transferInProvinceName.Replace("'", "''") : null;
                        item.transferInDistrictName = (!string.IsNullOrWhiteSpace(item.transferInDistrictName)) ? item.transferInDistrictName.Replace("'", "''") : null;
                        item.transferInLLGName = (!string.IsNullOrWhiteSpace(item.transferInLLGName)) ? item.transferInLLGName.Replace("'", "''") : null;
                        item.transferInWardName = (!string.IsNullOrWhiteSpace(item.transferInWardName)) ? item.transferInWardName.Replace("'", "''") : null;
                        item.transferInLocationName = (!string.IsNullOrWhiteSpace(item.transferInLocationName)) ? item.transferInLocationName.Replace("'", "''") : null;

                        item.transferOutProvinceName = (!string.IsNullOrWhiteSpace(item.transferOutProvinceName)) ? item.transferOutProvinceName.Replace("'", "''") : null;
                        item.transferOutDistrictName = (!string.IsNullOrWhiteSpace(item.transferOutDistrictName)) ? item.transferOutDistrictName.Replace("'", "''") : null;
                        item.transferOutLLGName = (!string.IsNullOrWhiteSpace(item.transferOutLLGName)) ? item.transferOutLLGName.Replace("'", "''") : null;
                        item.transferOutWardName = (!string.IsNullOrWhiteSpace(item.transferOutWardName)) ? item.transferOutWardName.Replace("'", "''") : null;
                        item.transferOutLocationName = (!string.IsNullOrWhiteSpace(item.transferOutLocationName)) ? item.transferOutLocationName.Replace("'", "''") : null;

                        item.createdDate = (!string.IsNullOrWhiteSpace(item.createdDate)) ? item.createdDate.Replace("'", "''") : null;
                        item.ErrorMessage = (!string.IsNullOrWhiteSpace(item.ErrorMessage)) ? item.ErrorMessage.Replace("'", "''") : null;
                        item.RejectedReason = (!string.IsNullOrWhiteSpace(item.RejectedReason)) ? item.RejectedReason.Replace("'", "''") : null;
                        item.IsApproveOrReject = (!string.IsNullOrWhiteSpace(item.IsApproveOrReject)) ? item.IsApproveOrReject.Replace("'", "''") : null;
                        item.modifiedByName = (!string.IsNullOrWhiteSpace(item.modifiedByName)) ? item.modifiedByName.Replace("'", "''") : null;
                        item.DuplicateLName = (!string.IsNullOrWhiteSpace(item.DuplicateLName)) ? item.DuplicateLName.Replace("'", "''") : null;
                        item.DuplicateFName = (!string.IsNullOrWhiteSpace(item.DuplicateFName)) ? item.DuplicateFName.Replace("'", "''") : null;
                        item.DuplicateVoterName = (!string.IsNullOrWhiteSpace(item.DuplicateVoterName)) ? item.DuplicateVoterName.Replace("'", "''") : null;
                        item.DuplicateGender = (!string.IsNullOrWhiteSpace(item.DuplicateGender)) ? item.DuplicateGender.Replace("'", "''") : null;
                        item.DuplicateGen = (!string.IsNullOrWhiteSpace(item.DuplicateGen)) ? item.DuplicateGen.Replace("'", "''") : null;
                        item.DuplicateDateOfBirth = (!string.IsNullOrWhiteSpace(item.DuplicateDateOfBirth)) ? item.DuplicateDateOfBirth.Replace("'", "''") : null;
                        item.DuplicateOccupation = (!string.IsNullOrWhiteSpace(item.DuplicateOccupation)) ? item.DuplicateOccupation.Replace("'", "''") : null;
                        item.DuplicateProvinceName = (!string.IsNullOrWhiteSpace(item.DuplicateProvinceName)) ? item.DuplicateProvinceName.Replace("'", "''") : null;
                        item.DuplicateDistrictName = (!string.IsNullOrWhiteSpace(item.DuplicateDistrictName)) ? item.DuplicateDistrictName.Replace("'", "''") : null;
                        item.DuplicateLLGName = (!string.IsNullOrWhiteSpace(item.DuplicateLLGName)) ? item.DuplicateLLGName.Replace("'", "''") : null;
                        item.DuplicateWardName = (!string.IsNullOrWhiteSpace(item.DuplicateWardName)) ? item.DuplicateWardName.Replace("'", "''") : null;
                        item.DuplicateLocationName = (!string.IsNullOrWhiteSpace(item.DuplicateLocationName)) ? item.DuplicateLocationName.Replace("'", "''") : null;
                        item.RecordType = (!string.IsNullOrWhiteSpace(item.RecordType)) ? item.RecordType.Replace("'", "''") : null;
                        item.DuplicateRecordType = (!string.IsNullOrWhiteSpace(item.DuplicateRecordType)) ? item.DuplicateRecordType.Replace("'", "''") : null;

                        lstVoter.Add(item);
                        recordCounter++;
                        if ((maxFileRecords == recordCounter) || (((fileCounter * maxFileRecords) + recordCounter) == voters.ListItems.Count()))
                        {
                            fileCounter++;
                            if (lstVoter != null && lstVoter.Count > 0)
                            {
                                var jsonResponse = new Response<VoterModelOffline>();
                                jsonResponse.Status = true;
                                jsonResponse.Data = new Result<VoterModelOffline>();
                                jsonResponse.Data.ListItems = lstVoter;

                                using (TextWriter writer = System.IO.File.CreateText($"{filePath}/{item.provinceName}_{fileCounter}.json"))
                                {
                                    var serializer = new JsonSerializer();
                                    serializer.Serialize(writer, jsonResponse);
                                }

                                jsonResponse = null;
                            }


                            recordCounter = 0;
                            lstVoter.Clear();
                        }
                    }

                    CreateZipFile($"{filePath}\\{provinceName}.zip", Directory.GetFiles(filePath, $"{provinceName}_*"));
                    res.Message = $"{filePath}\\{provinceName}.zip";
                    res.Message = $"{apiURL}/{Constants.UploadFolderPath}/{Constants.ProvinceDownloadFolderPath}/{provinceName}.zip";

                    if (Directory.Exists(filePath))
                    {
                        foreach (var item in Directory.GetFiles(filePath, $"{provinceName}_*"))
                        {
                            System.IO.File.Delete(item);
                        }
                    }
                }

                res.Status = true;
            }
            catch (Exception ex)
            {
                res.Message = ResponseMessage.SomethingWentWrong;
                res.Data = null;
                res.Status = false;
                ErrorLog.Write("FileService", "DownloadVotersFileByProvince", ex);
            }
            return res;
        }

        /// <summary>
        /// Creates the zip file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="files">The files.</param>
        public static void CreateZipFile(string fileName, IEnumerable<string> files)
        {
            // Create and open a new ZIP file
            var zip = ZipFile.Open(fileName, ZipArchiveMode.Create);
            foreach (var file in files)
            {
                // Add the entry for each file
                zip.CreateEntryFromFile(file, Path.GetFileName(file), CompressionLevel.Optimal);
            }
            // Dispose of the object when we are done
            zip.Dispose();
        }
        #endregion

        #region get mka ward clan section summary list
        /// <summary>
        /// Gets the mka ward clan section summary list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Response<GetMKAWardClanSectionSummaryModel>> GetMKAWardClanSectionSummaryList(PageList model)
        {
            lstresponseMKAWardClanSectionSummaryReport = new Response<GetMKAWardClanSectionSummaryModel>();

            try
            {
                resultmkawardclansectionsummary = await iFileRepository.GetMKAWardClanSectionSummaryList(model);

                if (resultmkawardclansectionsummary != null && resultmkawardclansectionsummary.ListItems != null)
                {

                    lstresponseMKAWardClanSectionSummaryReport.Data = resultmkawardclansectionsummary;
                    lstresponseMKAWardClanSectionSummaryReport.Status = true;
                }
                else
                {
                    lstresponseMKAWardClanSectionSummaryReport.Message = ResponseMessage.RecordNotExist;
                }
            }
            catch (Exception ex)
            {
                lstresponse.Message = ex.Message;
                ErrorLog.Write("FileService", "GetVoterList", ex);
            }

            return lstresponseMKAWardClanSectionSummaryReport;
        }
        #endregion

        #region get mka ward clan section summary list
        /// <summary>
        /// Gets the mka ward clan section summary list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Response<GetWardClanSectionSummaryModel>> GetWardClanSectionSummaryList(PageList model)
        {
            lstresponseWardClanSectionSummaryReport = new Response<GetWardClanSectionSummaryModel>();

            try
            {
                resultwardclansectionsummary = await iFileRepository.GetWardClanSectionSummaryList(model);

                if (resultwardclansectionsummary != null && resultwardclansectionsummary.ListItems != null)
                {

                    lstresponseWardClanSectionSummaryReport.Data = resultwardclansectionsummary;
                    lstresponseWardClanSectionSummaryReport.Status = true;
                }
                else
                {
                    lstresponseWardClanSectionSummaryReport.Message = ResponseMessage.RecordNotExist;
                }
            }
            catch (Exception ex)
            {
                lstresponse.Message = ex.Message;
                ErrorLog.Write("FileService", "GetVoterList", ex);
            }

            return lstresponseWardClanSectionSummaryReport;
        }
        #endregion

        #endregion
    }
}
