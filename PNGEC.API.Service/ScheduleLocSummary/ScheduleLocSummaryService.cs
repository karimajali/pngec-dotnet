using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Domain.ScheduleLocSummary;
using PNGEC.API.Helper;
using PNGEC.API.Repository.ScheduleLocSummary;

namespace PNGEC.API.Service.ScheduleLocSummary
{
    public class ScheduleLocSummaryService : IScheduleLocSummaryService
    {
        #region variable declaration

        /// <summary>
        /// The i schedule loc summary repository
        /// </summary>
        private readonly IScheduleLocSummaryRepository iScheduleLocSummaryRepository;

        /// <summary>
        /// The response
        /// </summary>
        private Response response = null;

        /// <summary>
        /// The lstresponse
        /// </summary>
        private Response<ScheduleLocSummaryModel> lstresponse = null;

        /// <summary>
        /// The lstmkaresponse
        /// </summary>
        private Response<MKAScheduleLocSummaryModel> lstmkaresponse = null;

        /// <summary>
        /// The result
        /// </summary>
        private Result<ScheduleLocSummaryModel> result = null;

        /// <summary>
        /// The mkaresult
        /// </summary>
        private Result<MKAScheduleLocSummaryModel> mkaresult = null;

        #endregion

        #region constructor 
        /// <summary>
        /// Initializes a new instance of the <see cref="ScheduleLocSummaryService"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public ScheduleLocSummaryService(string connection)
        {
            iScheduleLocSummaryRepository = new ScheduleLocSummaryRepository(connection);
        }
        #endregion

        #region service

        #region get roll stat list
        /// <summary>
        /// Gets the roll stat list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Response<ScheduleLocSummaryModel>> GetRollStatList(PageList model)
        {
            lstresponse = new Response<ScheduleLocSummaryModel>();

            try
            {
                result = await iScheduleLocSummaryRepository.GetRollStatList(model);

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
                ErrorLog.Write("ScheduleLocSummaryService", "GetRollStatList", ex);
            }

            return lstresponse;
        }
        #endregion

        #region get mka roll stat list
        /// <summary>
        /// Gets the mka roll stat list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Response<MKAScheduleLocSummaryModel>> GetMKARollStatList(PageList model)
        {
            lstmkaresponse = new Response<MKAScheduleLocSummaryModel>();

            try
            {
                mkaresult = await iScheduleLocSummaryRepository.GetMKARollStatList(model);

                if (mkaresult != null && mkaresult.ListItems != null)
                {
                    lstmkaresponse.Data = mkaresult;
                    lstmkaresponse.Status = true;
                }
                else
                {
                    lstmkaresponse.Message = ResponseMessage.RecordNotExist;
                }
            }
            catch (Exception ex)
            {
                lstmkaresponse.Message = ex.Message;
                ErrorLog.Write("ScheduleLocSummaryService", "GetRollStatList", ex);
            }

            return lstmkaresponse;
        }
        #endregion

        #region get latest roll stat list

        /// <summary>
        /// Gets the latest roll stat list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Response<ScheduleLocSummaryModel>> GetLatestRollStatList(PageList model)
        {
            lstresponse = new Response<ScheduleLocSummaryModel>();

            try
            {
                result = await iScheduleLocSummaryRepository.GetLatestRollStatList(model);

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
                ErrorLog.Write("ScheduleLocSummaryService", "GetLatestRollStatList", ex);
            }

            return lstresponse;
        }
        #endregion

        #region schedule notification
        /// <summary>
        /// Schedules the notification.
        /// </summary>
        public async Task<Response> ScheduleNotification()
        {
            response = new Response();

            #region staging
            try
            {
                List<ScheduleLocSummaryModel> scheduleLocSummaryModelsList = await iScheduleLocSummaryRepository.ScheduleNotificationStaging();
                if (scheduleLocSummaryModelsList != null && scheduleLocSummaryModelsList.Count > 0)
                {
                    var groupingList = scheduleLocSummaryModelsList.GroupBy(item => item.ProvinceId)
                    .Select(grouping => grouping.FirstOrDefault())
                    .OrderByDescending(item => item.ProvinceId)
                    .ToList();

                    foreach (var groupingItem in groupingList)
                    {
                        var scheduleLocSummary = scheduleLocSummaryModelsList.Where(x => x.ProvinceId == groupingItem.ProvinceId).ToList();

                        if (scheduleLocSummary != null && scheduleLocSummary.Count > 0)
                        {
                            string stagingContains = string.Empty;
                            #region staging Table Contains
                            string stagingTableContains = @"
                                <tr>
                                    <td align='center'>{0}</td>
                                    <td align='center'>{1}</td>
                                    <td align='center'>{2}</td>
                                    <td align='center'>{3}</td>
                                    <td align='center'>{4}</td>
                                    <td align='center'>{5}</td>
                                    <td align='center'>{6}</td>
                                    <td align='center'>{7}</td>
                                    <td align='center'>{8}</td>
                                    <td align='center'>{9}</td>
                                    <td align='center'>{10}</td>
                                    <td align='center'>{11}</td>
                                    <td align='center'>{12}</td>
                                    <td align='center'>{13}</td>
                                </tr>";
                            #endregion

                            foreach (var item in scheduleLocSummary)
                            {
                                stagingContains = stagingContains + string.Format(stagingTableContains, item.DistrictName, item.LLGName, item.WardName, item.ScheduleStringDate, item.PreviousTotal, item.TOTAL, item.OLD, item.NEW, item.UPDATED, item.TRANSFERIN, item.TRANSFEROUT, item.DECEASED, item.DUPLICATE, item.GHOST, item.UNDERAGE);
                            }

                            string RegionName = scheduleLocSummary.Select(x => x.RegionName).FirstOrDefault();
                            string ProvinceName = scheduleLocSummary.Select(x => x.ProvinceName).FirstOrDefault();

                            #region staging Template
                            string stagingTemplate = @"
                                Region : {0} <br/> Province : {1}
                                <table border='1' cellspacing='0' cellpadding='0' width='100%'>
                                    <tr>
                                        <td colspan='15' align='center' style='background-color: #fed8b1;'>
                                            Staging Database
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>District</th>
                                        <th>LLG</th>
                                        <th>Ward</th>
                                        <th>Schhedule</th>
                                        <th>Previous</th>
                                        <th>Total</th>
                                        <th>OLD</th>
                                        <th>NEW</th>
                                        <th>UPDATED</th>
                                        <th>TRANSFER IN</th>
                                        <th>TRANSFER OUT</th>
                                        <th>DECEASED</th>
                                        <th>DUPLICATE</th>
                                        <th>GHOST</th>
                                        <th>UNDER AGE</th>
                                    </tr>
                                    {2}
                                </table>";
                            #endregion

                            stagingTemplate = string.Format(stagingTemplate, RegionName, ProvinceName, stagingContains);
                            string toMailAddress = scheduleLocSummary.Select(x => x.UserEmails).FirstOrDefault();
                            if (!string.IsNullOrWhiteSpace(toMailAddress))
                            {
                                bool isMailSent = Email.SendMail(toMailAddress, "ScheduleNotification", stagingTemplate);
                            }
                        }
                    }
                    response.Status = true;
                }
                else
                {
                    response.Status = false;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Write("ScheduleLocSummaryService", "ScheduleNotification - Staging", ex);
            }
            #endregion

            #region Preliminary
            try
            {
                List<ScheduleLocSummaryModel> scheduleLocSummaryModelsList = await iScheduleLocSummaryRepository.ScheduleNotificationPreliminary();
                if (scheduleLocSummaryModelsList != null && scheduleLocSummaryModelsList.Count > 0)
                {
                    var groupingList = scheduleLocSummaryModelsList.GroupBy(item => item.RegionId)
                    .Select(grouping => grouping.FirstOrDefault())
                    .OrderByDescending(item => item.RegionId)
                    .ToList();

                    foreach (var groupingItem in groupingList)
                    {
                        var scheduleLocSummary = scheduleLocSummaryModelsList.Where(x => x.ProvinceId == groupingItem.ProvinceId).ToList();

                        if (scheduleLocSummary != null && scheduleLocSummary.Count > 0)
                        {
                            string PreliminaryContains = string.Empty;
                            #region Preliminary Table Contains
                            string PreliminaryTableContains = @"
                                <tr>
                                    <td align='center'>{0}</td>
                                    <td align='center'>{1}</td>
                                    <td align='center'>{2}</td>
                                    <td align='center'>{3}</td>
                                    <td align='center'>{4}</td>
                                    <td align='center'>{5}</td>
                                    <td align='center'>{6}</td>
                                    <td align='center'>{7}</td>
                                    <td align='center'>{8}</td>
                                    <td align='center'>{9}</td>
                                    <td align='center'>{10}</td>
                                    <td align='center'>{11}</td>
                                    <td align='center'>{12}</td>
                                    <td align='center'>{13}</td>
                                    <td align='center'>{14}</td>
                                </tr>";
                            #endregion

                            foreach (var item in scheduleLocSummary)
                            {
                                PreliminaryContains = PreliminaryContains + string.Format(PreliminaryTableContains, item.ProvinceName, item.DistrictName, item.LLGName, item.WardName, item.ScheduleStringDate, item.PreviousTotal, item.TOTAL, item.OLD, item.NEW, item.UPDATED, item.TRANSFERIN, item.TRANSFEROUT, item.DECEASED, item.DUPLICATE, item.GHOST, item.UNDERAGE);
                            }

                            string RegionName = scheduleLocSummary.Select(x => x.RegionName).FirstOrDefault();

                            #region staging Template
                            string stagingTemplate = @"
                                Region : {0}
                                <table border='1' cellspacing='0' cellpadding='0' width='100%'>
                                    <tr>
                                        <td colspan='16' align='center' style='background-color: #add8e6;'>
                                            Preliminary Database
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>Province</th>
                                        <th>District</th>
                                        <th>LLG</th>
                                        <th>Ward</th>
                                        <th>Schhedule</th>
                                        <th>Previous</th>
                                        <th>Total</th>
                                        <th>OLD</th>
                                        <th>NEW</th>
                                        <th>UPDATED</th>
                                        <th>TRANSFER IN</th>
                                        <th>TRANSFER OUT</th>
                                        <th>DECEASED</th>
                                        <th>DUPLICATE</th>
                                        <th>GHOST</th>
                                        <th>UNDER AGE</th>
                                    </tr>
                                    {1}
                                </table>";
                            #endregion

                            stagingTemplate = string.Format(stagingTemplate, RegionName, PreliminaryContains);
                            string toMailAddress = scheduleLocSummary.Select(x => x.UserEmails).FirstOrDefault();
                            if (!string.IsNullOrWhiteSpace(toMailAddress))
                            {
                                bool isMailSent = Email.SendMail(toMailAddress, "ScheduleNotification", stagingTemplate);
                            }
                        }
                    }
                    response.Status = true;
                }
                else
                {
                    response.Status = false;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Write("ScheduleLocSummaryService", "ScheduleNotification - Preliminary", ex);
            }
            #endregion

            #region Final
            try
            {
                List<ScheduleLocSummaryModel> scheduleLocSummaryModelsList = await iScheduleLocSummaryRepository.ScheduleNotificationFinal();
                if (scheduleLocSummaryModelsList != null && scheduleLocSummaryModelsList.Count > 0)
                {
                    string finalContains = string.Empty;
                    #region final Table Contains
                    string finalTableContains = @"
                        <tr>
                            <td align='center'>{0}</td>
                            <td align='center'>{1}</td>
                            <td align='center'>{2}</td>
                            <td align='center'>{3}</td>
                            <td align='center'>{4}</td>
                            <td align='center'>{5}</td>
                            <td align='center'>{6}</td>
                            <td align='center'>{7}</td>
                            <td align='center'>{8}</td>
                            <td align='center'>{9}</td>
                            <td align='center'>{10}</td>
                            <td align='center'>{11}</td>
                            <td align='center'>{12}</td>
                            <td align='center'>{13}</td>
                            <td align='center'>{14}</td>
                            <td align='center'>{15}</td>
                        </tr>";
                    #endregion

                    foreach (var item in scheduleLocSummaryModelsList)
                    {
                        finalContains = finalContains + string.Format(finalTableContains, item.RegionName, item.ProvinceName, item.DistrictName, item.LLGName, item.WardName, item.ScheduleStringDate, item.PreviousTotal, item.TOTAL, item.OLD, item.NEW, item.UPDATED, item.TRANSFERIN, item.TRANSFEROUT, item.DECEASED, item.DUPLICATE, item.GHOST, item.UNDERAGE);
                    }

                    #region staging Template
                    string stagingTemplate = @"
                        <table border='1' cellspacing='0' cellpadding='0' width='100%'>
                            <tr>
                                <td colspan='17' align='center' style='background-color: #90EE90;'>
                                    Final Database
                                </td>
                            </tr>
                            <tr>
                                <th>Region</th>
                                <th>Province</th>
                                <th>District</th>
                                <th>LLG</th>
                                <th>Ward</th>
                                <th>Schhedule</th>
                                <th>Previous</th>
                                <th>Total</th>
                                <th>OLD</th>
                                <th>NEW</th>
                                <th>UPDATED</th>
                                <th>TRANSFER IN</th>
                                <th>TRANSFER OUT</th>
                                <th>DECEASED</th>
                                <th>DUPLICATE</th>
                                <th>GHOST</th>
                                <th>UNDER AGE</th>
                            </tr>
                            {0}
                        </table>";
                    #endregion

                    stagingTemplate = string.Format(stagingTemplate, finalContains);
                    string toMailAddress = scheduleLocSummaryModelsList.Select(x => x.UserEmails).FirstOrDefault();

                    if (!string.IsNullOrWhiteSpace(toMailAddress))
                    {
                        bool isMailSent = Email.SendMail(toMailAddress, "ScheduleNotification", stagingTemplate);
                    }

                    response.Status = true;
                }
                else
                {
                    response.Status = false;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Write("ScheduleLocSummaryService", "ScheduleNotification - Final", ex);
            }
            #endregion

            #region Rejected
            try
            {
                List<ScheduleLocSummaryRejectedModel> scheduleLocSummaryRejectedModelList = await iScheduleLocSummaryRepository.ScheduleNotificationRejected();
                if (scheduleLocSummaryRejectedModelList != null && scheduleLocSummaryRejectedModelList.Count > 0)
                {
                    foreach (var item in scheduleLocSummaryRejectedModelList)
                    {
                        string RejectedTemplate = string.Format("{0} records are rejected, please check rejection section in mobile application.", item.RejectedRecords);

                        bool isMailSent = Email.SendMail(item.EmailAddress, "ScheduleNotification", RejectedTemplate);
                    }

                    response.Status = true;
                }
                else
                {
                    response.Status = false;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Write("ScheduleLocSummaryService", "ScheduleNotification - Rejected", ex);
            }
            #endregion

            #region RejectedByRD
            try
            {
                List<ScheduleLocSummaryRejectedByRDModel> scheduleLocSummaryRejectedByRDModelList = await iScheduleLocSummaryRepository.ScheduleNotificationRejectedByRD();
                if (scheduleLocSummaryRejectedByRDModelList != null && scheduleLocSummaryRejectedByRDModelList.Count > 0)
                {
                    var groupingList = scheduleLocSummaryRejectedByRDModelList.GroupBy(item => item.ProvinceId)
                    .Select(grouping => grouping.FirstOrDefault())
                    .OrderByDescending(item => item.ProvinceId)
                    .ToList();

                    foreach (var groupingItem in groupingList)
                    {
                        var scheduleLocSummary = scheduleLocSummaryRejectedByRDModelList.Where(x => x.ProvinceId == groupingItem.ProvinceId).ToList();

                        if (scheduleLocSummary != null && scheduleLocSummary.Count > 0)
                        {
                            string RejectedByRDContains = string.Empty;
                            #region RejectedByRD Table Contains
                            string RejectedByRDTableContains = @"
                                <tr>
                                    <td align='center'>{0}</td>
                                    <td align='center'>{1}</td>
                                    <td align='center'>{2}</td>
                                    <td align='center'>{3}</td>
                                    <td align='center'>{4} {5}</td>
                                    <td align='center'>{6}</td>
                                </tr>";
                            #endregion

                            int rowno = 1;
                            foreach (var item in scheduleLocSummary)
                            {
                                RejectedByRDContains = RejectedByRDContains + string.Format(RejectedByRDTableContains, rowno, item.RegionName, item.ProvinceName, item.ElectorId, item.LName, item.FName, item.Status);

                                rowno++;
                            }

                            #region RejectedByRD Template
                            string RejectedByRDTemplate = @"
                                <table border='1' cellspacing='0' cellpadding='0' width='100%'>
                                    <tr>
                                        <td colspan='6' align='center'>
                                            Rejected Records
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>RowNo</th>
                                        <th>Region</th>
                                        <th>Province</th>
                                        <th>ElectorId</th>
                                        <th>Name</th>
                                        <th>Status</th>
                                    </tr>
                                    {0}
                                </table>";
                            #endregion

                            RejectedByRDTemplate = string.Format(RejectedByRDTemplate, RejectedByRDContains);
                            string toMailAddress = scheduleLocSummary.Select(x => x.UserEmails).FirstOrDefault();

                            if (!string.IsNullOrWhiteSpace(toMailAddress))
                            {
                                bool isMailSent = Email.SendMail(toMailAddress, "ScheduleNotification", RejectedByRDTemplate);
                            }
                        }
                    }
                    response.Status = true;
                }
                else
                {
                    response.Status = false;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Write("ScheduleLocSummaryService", "ScheduleNotification - RejectedByRD", ex);
            }
            #endregion

            return response;
        }
        #endregion

        #region staging
        /// <summary>
        /// Stagings the schedule notification.
        /// </summary>
        /// <returns></returns>
        public async Task<List<ScheduleLocSummaryModel>> StagingScheduleNotification()
        {
            List<ScheduleLocSummaryModel> stagingList = new List<ScheduleLocSummaryModel>();
            try
            {
                stagingList = await iScheduleLocSummaryRepository.StagingScheduleNotification();
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("ScheduleLocSummaryService", "StagingScheduleNotification()", ex);
            }
            return stagingList;
        }
        #endregion

        #region preliminary
        /// <summary>
        /// Preliminaries the schedule notification.
        /// </summary>
        /// <returns></returns>
        public async Task<List<ScheduleLocSummaryModel>> PreliminaryScheduleNotification()
        {
            List<ScheduleLocSummaryModel> preliminaryList = new List<ScheduleLocSummaryModel>();
            try
            {
                preliminaryList = await iScheduleLocSummaryRepository.PreliminaryScheduleNotification();
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("ScheduleLocSummaryService", "PreliminaryScheduleNotification()", ex);
            }
            return preliminaryList;
        }
        #endregion

        #region final
        /// <summary>
        /// Finals the schedule notification.
        /// </summary>
        /// <returns></returns>
        public async Task<List<ScheduleLocSummaryModel>> FinalScheduleNotification()
        {
            List<ScheduleLocSummaryModel> finalList = new List<ScheduleLocSummaryModel>();
            try
            {
                finalList = await iScheduleLocSummaryRepository.FinalScheduleNotification();
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("ScheduleLocSummaryService", "PreliminaryScheduleNotification()", ex);
            }
            return finalList;
        }
        #endregion

        #region Rejected
        /// <summary>
        /// Rejecteds the schedule notification.
        /// </summary>
        /// <returns></returns>
        public async Task<List<ScheduleLocSummaryRejectedModel>> RejectedScheduleNotification()
        {
            List<ScheduleLocSummaryRejectedModel> rejectedList = new List<ScheduleLocSummaryRejectedModel>();
            try
            {
                rejectedList = await iScheduleLocSummaryRepository.RejectedScheduleNotification();
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("ScheduleLocSummaryService", "RejectedScheduleNotification()", ex);
            }
            return rejectedList;
        }
        #endregion

        #region RejectedByRD
        /// <summary>
        /// Rejecteds the by rd schedule notification.
        /// </summary>
        /// <returns></returns>
        public async Task<List<ScheduleLocSummaryRejectedByRDModel>> RejectedByRDScheduleNotification()
        {
            List<ScheduleLocSummaryRejectedByRDModel> rejectedByRDList = new List<ScheduleLocSummaryRejectedByRDModel>();
            try
            {
                rejectedByRDList = await iScheduleLocSummaryRepository.RejectedByRDScheduleNotification();
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                ErrorLog.Write("ScheduleLocSummaryService", "RejectedByRDScheduleNotification()", ex);
            }
            return rejectedByRDList;
        }
        #endregion

        #endregion
    }
}

