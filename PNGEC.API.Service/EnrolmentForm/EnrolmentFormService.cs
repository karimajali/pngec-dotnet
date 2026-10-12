using PNGEC.API.Domain.EnrolmentForm;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Domain.MKAEnrolmentForm;
using PNGEC.API.Helper;
using PNGEC.API.Repository.EnrolmentForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PNGEC.API.Service.EnrolmentForm
{
    public class EnrolmentFormService : IEnrolmentFormService
    {
        #region variable declaration
        /// <summary>
        /// The i EnrolmentForm repository
        /// </summary>
        private readonly IEnrolmentFormRepository iEnrolmentFormRepository;

        /// <summary>
        /// The response
        /// </summary>
        private Response response = null;

        /// <summary>
        /// The lstresponse
        /// </summary>
        private Response<EnrolmentFormModel> lstresponse = null;

        /// <summary>
        /// The result
        /// </summary>
        private Result<EnrolmentFormModel> result = null;
        /// <summary>
        /// The lstresponse
        /// </summary>
        private Response<MKAEnrolmentFormModel> lstmkaresponse = null;

        /// <summary>
        /// The result
        /// </summary>
        private Result<MKAEnrolmentFormModel> mkaresult = null;

        /// <summary>
        /// The module name
        /// </summary>
        private const string ModuleName = "enrolment";

        /// <summary>
        /// The API URL
        /// </summary>
        private string apiURL = string.Empty;
        #endregion

        #region constructor 
        /// <summary>
        /// Initializes a new instance of the <see cref="EnrolmentFormService"/> class.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public EnrolmentFormService(string connection, string apiUrl)
        {
            iEnrolmentFormRepository = new EnrolmentFormRepository(connection);
            this.apiURL = apiUrl;
        }
        #endregion

        #region service

        #region add enrolmentform
        /// <summary>
        /// Adds the EnrolmentForm
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Response> AddEnrolmentForm(EnrolmentFormModel model)
        {
            try
            {
                response = new Response();
                //if (model.AttachmnetToUpoad != null && model.AttachmnetToUpoad.Length > 0)
                //{
                //    model.Attachment = await UploadFiles.UploadFile(model.AttachmnetToUpoad, ModuleName);
                //}
                if (model.AttachmnetToUpoad != null && model.AttachmnetToUpoad.Count >= 1)
                {
                    List<string> attachmnetsStr = new List<string>();
                    foreach (var attach in model.AttachmnetToUpoad)
                    {
                        attachmnetsStr.Add(await UploadFiles.UploadFile(attach, ModuleName, attach.FileName));
                    }
                    model.Attachment = String.Join(",", attachmnetsStr);
                }
                EnrolmentFormModel EnrolmentForm = await iEnrolmentFormRepository.AddEnrolmentForm(model);

                response.Status = EnrolmentForm.ResponseCode > 0;

                if (EnrolmentForm.ResponseCode == -1)
                {
                    response.Message = ResponseMessage.EnrolmentFormError_Same_BatchRecordExist;
                }
                else
                {
                    response.Message = ResponseMessage.Record_Add_Update_Success;
                    response.Data = EnrolmentForm;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("EnrolmentFormService", "AddEnrolmentForm", ex);
            }
            return response;
        }
        #endregion
        #region add enrolmentform
        /// <summary>
        /// Adds the EnrolmentForm
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Response> AddMKAEnrolmentForm(MKAEnrolmentFormModel model)
        {
            try
            {
                response = new Response();
                //if (model.AttachmnetToUpoad != null && model.AttachmnetToUpoad.Length > 0)
                //{
                //    model.Attachment = await UploadFiles.UploadFile(model.AttachmnetToUpoad, ModuleName);
                //}
                if (model.AttachmnetToUpoad != null && model.AttachmnetToUpoad.Count >= 1)
                {
                    List<string> attachmnetsStr = new List<string>();
                    foreach (var attach in model.AttachmnetToUpoad)
                    {
                        attachmnetsStr.Add(await UploadFiles.UploadFile(attach, ModuleName, attach.FileName));
                    }
                    model.Attachment = String.Join(",", attachmnetsStr);
                }
                MKAEnrolmentFormModel EnrolmentForm = await iEnrolmentFormRepository.AddMKAEnrolmentForm(model);

                response.Status = EnrolmentForm.ResponseCode > 0;

                if (EnrolmentForm.ResponseCode == -1)
                {
                    response.Message = ResponseMessage.EnrolmentFormError_Same_BatchRecordExist;
                }
                else
                {
                    response.Message = ResponseMessage.Record_Add_Update_Success;
                    response.Data = EnrolmentForm;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("EnrolmentFormService", "AddEnrolmentForm", ex);
            }
            return response;
        }
        #endregion
        #region get EnrolmentForm list
        /// <summary>
        /// Gets the EnrolmentForm list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Response<EnrolmentFormModel>> GetEnrolmentFormList(PageList model)
        {
            lstresponse = new Response<EnrolmentFormModel>();

            try
            {
                result = await iEnrolmentFormRepository.GetEnrolmentFormList(model);

                if (result != null && result.ListItems != null)
                {
                    foreach (var item in result.ListItems)
                    {
                        item.SetAttachmentUrl(this.apiURL);
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
                ErrorLog.Write("EnrolmentFormService", "GetEnrolmentFormList", ex);
            }

            return lstresponse;
        }
        #endregion
        #region get EnrolmentForm list
        /// <summary>
        /// Gets the EnrolmentForm list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Response<MKAEnrolmentFormModel>> GetMKAEnrolmentFormList(PageList model)
        {
            lstmkaresponse = new Response<MKAEnrolmentFormModel>();

            try
            {
                mkaresult = await iEnrolmentFormRepository.GetMKAEnrolmentFormList(model);

                if (mkaresult != null && mkaresult.ListItems != null)
                {
                    foreach (var item in mkaresult.ListItems)
                    {
                        item.SetAttachmentUrl(this.apiURL);
                    }
                    lstmkaresponse.Data = mkaresult;
                    lstmkaresponse.Status = true;
                }
                else
                {
                    lstresponse.Message = ResponseMessage.RecordNotExist;
                }
            }
            catch (Exception ex)
            {
                lstresponse.Message = ex.Message;
                ErrorLog.Write("EnrolmentFormService", "GetEnrolmentFormList", ex);
            }

            return lstmkaresponse;
        }
        #endregion
        #region get ward quality check data

        /// <summary>
        /// Gets the ward quality check data.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<CommonResponse<List<WardQualityCheckResponseModel>>> GetWardQualityCheckData(EnrolmentFormModel model)
        {
            var response = new CommonResponse<List<WardQualityCheckResponseModel>>();

            try
            {
                response.Status = true;
                var wardQualityCheckData = await iEnrolmentFormRepository.GetWardQualityCheckData(model);

                if (wardQualityCheckData?.enrolmentFormModels?.Count() > 0)
                {
                    var enrolmentFormModel = wardQualityCheckData.enrolmentFormModels.FirstOrDefault();
                    if (enrolmentFormModel != null)
                    {
                        var numberOfForms = enrolmentFormModel.EndRange - enrolmentFormModel.StartRange;
                        var wardQualityCheckResponse = new List<WardQualityCheckResponseModel>();
                        for (int i = 0; i <= numberOfForms; i++)
                        {
                            var wardQualityCheck = new WardQualityCheckResponseModel();
                            var ward = enrolmentFormModel.WardId.ToString().PadLeft(4, '0');
                            var numberWithRange = (enrolmentFormModel.StartRange + i).ToString().PadLeft(7, '0');
                            wardQualityCheck.FormNumber = string.Format("{0}{1}{2}", enrolmentFormModel.ProvinceId, ward, numberWithRange);
                            wardQualityCheckResponse.Add(wardQualityCheck);
                        }

                        try
                        {
                            if (wardQualityCheckData?.voterModels?.Count() > 0)
                            {
                                foreach (var item in wardQualityCheckData.voterModels.ToList())
                                {
                                    if (!string.IsNullOrWhiteSpace(item.EnrolmentFormNumber))
                                    {
                                        string[] EnrolmentFormArray = item.EnrolmentFormNumber.Split('#');

                                        if (EnrolmentFormArray.Length == 7)
                                        {
                                            item.EnrolmentFormNumber = EnrolmentFormArray[1] + EnrolmentFormArray[4].ToString().PadLeft(4, '0') + EnrolmentFormArray[6];

                                            var formNumber = wardQualityCheckResponse.FirstOrDefault(w => w.FormNumber == item.EnrolmentFormNumber);
                                            if (formNumber != null)
                                            {
                                                formNumber.IsUsed = true;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorLog.Write("EnrolmentFormService", "GetWardQualityCheckData - Set Used or not", ex);
                        }

                        response.Data = wardQualityCheckResponse;
                    }
                }
                else
                {
                    response.Message = ResponseMessage.RecordNotExist;
                }
                //if (result != null && result.ListItems != null)
                //{
                //    //foreach (var item in result.ListItems)
                //    //{
                //    //    item.SetAttachmentUrl(this.apiURL);
                //    //}
                //    //response.Data = result;
                //    //response.Status = true;
                //}
                //else
                //{
                //    response.Message = ResponseMessage.RecordNotExist;
                //}
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                ErrorLog.Write("EnrolmentFormService", "GetWardQualityCheckData", ex);
            }

            return response;
        }
        #endregion
        #region get ward quality check data

        /// <summary>
        /// Gets the ward quality check data.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<CommonResponse<List<WardQualityCheckResponseModel>>> GetMKAWardQualityCheckData(MKAEnrolmentFormModel model)
        {
            var response = new CommonResponse<List<WardQualityCheckResponseModel>>();

            try
            {
                response.Status = true;
                var wardQualityCheckData = await iEnrolmentFormRepository.GetMKAWardQualityCheckData(model);

                if (wardQualityCheckData?.mkaenrolmentFormModels?.Count() > 0)
                {
                    var enrolmentFormModel = wardQualityCheckData.mkaenrolmentFormModels.FirstOrDefault();
                    if (enrolmentFormModel != null)
                    {
                        var numberOfForms = enrolmentFormModel.EndRange - enrolmentFormModel.StartRange;
                        var wardQualityCheckResponse = new List<WardQualityCheckResponseModel>();
                        for (int i = 0; i <= numberOfForms; i++)
                        {
                            var wardQualityCheck = new WardQualityCheckResponseModel();
                            var ward = enrolmentFormModel.MKAWardId.ToString().PadLeft(4, '0');
                            var numberWithRange = (enrolmentFormModel.StartRange + i).ToString().PadLeft(7, '0');
                            wardQualityCheck.FormNumber = string.Format("{0}{1}",  ward, numberWithRange);
                            wardQualityCheckResponse.Add(wardQualityCheck);
                        }

                        try
                        {
                            if (wardQualityCheckData?.voterModels?.Count() > 0)
                            {
                                foreach (var item in wardQualityCheckData.voterModels.ToList())
                                {
                                    if (!string.IsNullOrWhiteSpace(item.EnrolmentFormNumber))
                                    {
                                        string[] EnrolmentFormArray = item.EnrolmentFormNumber.Split('#');

                                        if (EnrolmentFormArray.Length == 7)
                                        {
                                            item.EnrolmentFormNumber = EnrolmentFormArray[1] + EnrolmentFormArray[4].ToString().PadLeft(4, '0') + EnrolmentFormArray[6];

                                            var formNumber = wardQualityCheckResponse.FirstOrDefault(w => w.FormNumber == item.EnrolmentFormNumber);
                                            if (formNumber != null)
                                            {
                                                formNumber.IsUsed = true;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorLog.Write("EnrolmentFormService", "GetWardQualityCheckData - Set Used or not", ex);
                        }

                        response.Data = wardQualityCheckResponse;
                    }
                }
                else
                {
                    response.Message = ResponseMessage.RecordNotExist;
                }
                //if (result != null && result.ListItems != null)
                //{
                //    //foreach (var item in result.ListItems)
                //    //{
                //    //    item.SetAttachmentUrl(this.apiURL);
                //    //}
                //    //response.Data = result;
                //    //response.Status = true;
                //}
                //else
                //{
                //    response.Message = ResponseMessage.RecordNotExist;
                //}
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                ErrorLog.Write("EnrolmentFormService", "GetWardQualityCheckData", ex);
            }

            return response;
        }
        #endregion
        #region delete enrolment form
        /// <summary>
        /// Deletes the EnrolmentForm.
        /// </summary>
        /// <param name="Id">The Enrolment form Id.</param>
        /// <returns></returns>
        public async Task<Response> DeleteEnrolmentForm(int enrolmentFormId)
        {
            response = new Response();
            try
            {
                await iEnrolmentFormRepository.DeleteEnrolmentForm(enrolmentFormId);
                response.Status = true;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                ErrorLog.Write("EnrolmentFormService", "DeleteEnrolmentForm", ex);
            }

            return response;
        }
        #endregion
        #region delete enrolment form
        /// <summary>
        /// Deletes the EnrolmentForm.
        /// </summary>
        /// <param name="Id">The Enrolment form Id.</param>
        /// <returns></returns>
        public async Task<Response> DeleteMKAEnrolmentForm(int enrolmentFormId)
        {
            response = new Response();
            try
            {
                await iEnrolmentFormRepository.DeleteMKAEnrolmentForm(enrolmentFormId);
                response.Status = true;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                ErrorLog.Write("EnrolmentFormService", "DeleteEnrolmentForm", ex);
            }

            return response;
        }
        #endregion

        #endregion
    }
}
