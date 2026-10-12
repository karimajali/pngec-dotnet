using Newtonsoft.Json;
using PNGEC.API.Domain.ScheduleLocSummary;
using PNGEC.API.Helper;
using PNGEC.ScheduleJob.Report;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PNGEC.ScheduleJob.APIs
{
    public class ScheduleJob
    {
        #region Schedule Notification
        /// <summary>
        /// Schedules the notification.
        /// </summary>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> ScheduleNotification()
        {
            HttpResponseMessage response = null;

            //using (var client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseAddress"]);
            //    client.DefaultRequestHeaders.Accept.Clear();
            //    client.Timeout = new TimeSpan(0, 30, 0);
            //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //    response = await client.GetAsync("api/ScheduleLocSummary/ScheduleNotification");
            //}

            #region Staging
            try
            {
                //ErrorLog.WriteLogFile("ScheduleNotification()", "Staging schedule notification started");
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseAddress"]);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    using (response = await client.GetAsync("api/ScheduleLocSummary/StagingScheduleNotification"))
                    {
                        //ErrorLog.WriteLogFile("ScheduleNotification()", "Staging schedule notification api call");
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            string responseBody = await response.Content.ReadAsStringAsync();
                            List<ScheduleLocSummaryModel> scheduleLocSummaryModelsList = JsonConvert.DeserializeObject<List<ScheduleLocSummaryModel>>(responseBody);

                            if (scheduleLocSummaryModelsList != null && scheduleLocSummaryModelsList.Count > 0)
                            {
                                if (IsSunday())
                                {
                                    #region Nightly Report

                                    var groupingList = scheduleLocSummaryModelsList.GroupBy(item => item.ProvinceId)
                                   .Select(grouping => grouping.FirstOrDefault())
                                   .OrderByDescending(item => item.ProvinceId)
                                   .ToList();

                                    foreach (var groupingItem in groupingList)
                                    {
                                        var scheduleLocSummary = scheduleLocSummaryModelsList.Where(x => x.ProvinceId == groupingItem.ProvinceId).ToList();

                                        if (scheduleLocSummary != null && scheduleLocSummary.Count > 0)
                                        {
                                            string ProvinceName = scheduleLocSummary.Select(x => x.ProvinceName).FirstOrDefault();
                                            Int64 ProvinceId = scheduleLocSummary.Select(x => x.ProvinceId).FirstOrDefault();

                                            //Generate PDF
                                            string filename1 = ProvinceId + "_" + ProvinceName + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".pdf";
                                            DataTable stagingDT = NightlySummaryReport.ToDataTable(scheduleLocSummary);
                                            byte[] bytes1 = NightlySummaryReport.GenerateNightlySummaryPDF(stagingDT, "NightlySummary.rdlc", "ds_NightlyReport", "tblNightlyReport");

                                            //save Document and insert record
                                            string emailTitle1 = ProvinceName + " Provincial Data Center Ward Roll Update Summary Report for " + DateTime.Now.Date.ToString("dd/MM/yyyy");
                                            string fileUrl1 = await NightlySummaryReport.AddDocument(1, filename1, bytes1, emailTitle1);

                                            if (!string.IsNullOrWhiteSpace(fileUrl1))
                                            {
                                                ErrorLog.WriteLogFile("ScheduleNotification()", "Staging schedule notification file generated successfully.FileURL :" + fileUrl1);

                                                #region Send Email 
                                                string toMailAddress = scheduleLocSummary.Select(x => x.UserEmails).FirstOrDefault();
                                                string UserNames = scheduleLocSummary.Select(x => x.UserNames).FirstOrDefault();

                                                //Read email template
                                                if (!string.IsNullOrWhiteSpace(toMailAddress))
                                                {
                                                    var splitUserNames = UserNames.Split(',');
                                                    var splitUserEmails = toMailAddress.Split(',');

                                                    string fileName = "EmailTemplates\\NightlySummary.html";
                                                    string templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
                                                    StreamReader str = new StreamReader(templatePath);
                                                    string emailBody = str.ReadToEnd();
                                                    str.Close();

                                                    emailBody = emailBody.Replace("[ProvinceName]", ProvinceName);
                                                    emailBody = emailBody.Replace("[Date]", DateTime.Now.Date.ToString("dd/MM/yyyy"));
                                                    emailBody = emailBody.Replace("[FileName]", fileUrl1);
                                                    emailBody = emailBody.Replace("[Description]", emailTitle1);

                                                    for (int i = 0; i < splitUserEmails.Length; i++)
                                                    {
                                                        string finalBody = emailBody;
                                                        finalBody = finalBody.Replace("[UserName]", splitUserNames[i]);
                                                        bool isMailSent = NightlySummaryReport.SendMail(splitUserEmails[i], emailTitle1, finalBody);
                                                    }
                                                }
                                                #endregion
                                            }
                                        }
                                    }

                                    #endregion

                                    #region Management Report

                                    string RegionName = scheduleLocSummaryModelsList.Select(x => x.RegionName).FirstOrDefault();
                                    Int64 RegionId = scheduleLocSummaryModelsList.Select(x => x.RegionId).FirstOrDefault();

                                    //Generate PDF
                                    string filename = "Management_Report_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".pdf";
                                    DataTable finalDT = NightlySummaryReport.ToDataTable(scheduleLocSummaryModelsList);
                                    byte[] bytes = NightlySummaryReport.GenerateNightlySummaryPDF(finalDT, "MgmntNightlySummary.rdlc", "ds_NightlyReport", "tblNightlyReport");

                                    //save Document and insert record
                                    string emailTitle = "Provincial Data Centers Ward Roll Update Summary Report for " + DateTime.Now.Date.ToString("dd/MM/yyyy");
                                    string fileUrl = await NightlySummaryReport.AddDocument(2, filename, bytes, emailTitle);

                                    if (!string.IsNullOrWhiteSpace(fileUrl))
                                    {
                                        ErrorLog.WriteLogFile("ScheduleNotification()", "Final schedule notification file generated successfully.FileURL :" + fileUrl);

                                        #region Send Email 
                                        string toMailAddress = scheduleLocSummaryModelsList.Select(x => x.ManagementUserEmails).FirstOrDefault();
                                        string UserNames = scheduleLocSummaryModelsList.Select(x => x.ManagementUserNames).FirstOrDefault();

                                        //Read email template
                                        if (!string.IsNullOrWhiteSpace(toMailAddress))
                                        {
                                            var splitUserNames = UserNames.Split(',');
                                            var splitUserEmails = toMailAddress.Split(',');

                                            string fileName = "EmailTemplates\\MgmntNightlySummary.html";
                                            string templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
                                            StreamReader str = new StreamReader(templatePath);
                                            string emailBody = str.ReadToEnd();
                                            str.Close();

                                            emailBody = emailBody.Replace("[Date]", DateTime.Now.Date.ToString("dd/MM/yyyy"));
                                            emailBody = emailBody.Replace("[FileName]", fileUrl);
                                            emailBody = emailBody.Replace("[Description]", emailTitle);

                                            for (int i = 0; i < splitUserEmails.Length; i++)
                                            {
                                                string finalBody = emailBody;
                                                finalBody = finalBody.Replace("[UserName]", splitUserNames[i]);
                                                bool isMailSent = NightlySummaryReport.SendMail(splitUserEmails[i], emailTitle, finalBody);
                                            }
                                        }
                                        #endregion
                                    }
                                    #endregion
                                }
                            }
                            else
                            {
                                //ErrorLog.WriteLogFile("ScheduleNotification()", "Staging schedule notification no found any data");
                            }
                        }
                    }
                }
                //ErrorLog.WriteLogFile("ScheduleNotification()", "Staging schedule notification ended");
            }
            catch (Exception ex)
            {
                ErrorLog.Write("ScheduleJob", "ScheduleNotification - Staging", ex);
            }
            #endregion

            #region unused code

            //#region Preliminary
            //try
            //{
            //    ErrorLog.WriteLogFile("ScheduleNotification()", "Preliminary schedule notification started");
            //    using (var client = new HttpClient())
            //    {
            //        client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseAddress"]);
            //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //        using (response = await client.GetAsync("api/ScheduleLocSummary/PreliminaryScheduleNotification"))
            //        {
            //            ErrorLog.WriteLogFile("ScheduleNotification()", "Preliminary schedule notification api call");
            //            if (response.StatusCode == HttpStatusCode.OK)
            //            {
            //                string responseBody = await response.Content.ReadAsStringAsync();
            //                List<ScheduleLocSummaryModel> scheduleLocSummaryModelsList = JsonConvert.DeserializeObject<List<ScheduleLocSummaryModel>>(responseBody);

            //                if (scheduleLocSummaryModelsList != null && scheduleLocSummaryModelsList.Count > 0)
            //                {
            //                    var groupingList = scheduleLocSummaryModelsList.GroupBy(item => item.RegionId)
            //                    .Select(grouping => grouping.FirstOrDefault())
            //                    .OrderByDescending(item => item.RegionId)
            //                    .ToList();

            //                    foreach (var groupingItem in groupingList)
            //                    {
            //                        var scheduleLocSummary = scheduleLocSummaryModelsList.Where(x => x.ProvinceId == groupingItem.ProvinceId).ToList();

            //                        if (scheduleLocSummary != null && scheduleLocSummary.Count > 0)
            //                        {
            //                            string RegionName = scheduleLocSummary.Select(x => x.RegionName).FirstOrDefault();
            //                            Int64 RegionId = scheduleLocSummary.Select(x => x.RegionId).FirstOrDefault();
            //                            string ProvinceName = scheduleLocSummary.Select(x => x.ProvinceName).FirstOrDefault();

            //                            //Generate PDF
            //                            string filename = RegionId + "_" + RegionName + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".pdf";
            //                            DataTable stagingDT = NightlySummaryReport.ToDataTable(scheduleLocSummary);
            //                            byte[] bytes = NightlySummaryReport.GenerateNightlySummaryPDF(stagingDT, "NightlySummary.rdlc", "ds_NightlyReport", "tblNightlyReport");

            //                            //save Document and insert record
            //                            string emailTitle = ProvinceName + " Provincial Data Center Ward Roll Update Summary Report for " + DateTime.Now.Date.ToString("dd/MM/yyyy");
            //                            string fileUrl = await NightlySummaryReport.AddDocument(1, filename, bytes, emailTitle);

            //                            if (!string.IsNullOrWhiteSpace(fileUrl))
            //                            {
            //                                ErrorLog.WriteLogFile("ScheduleNotification()", "Preliminary schedule notification file generated successfully.FileURL :" + fileUrl);

            //                                #region Send Email 
            //                                string toMailAddress = scheduleLocSummary.Select(x => x.UserEmails).FirstOrDefault();
            //                                string UserNames = scheduleLocSummary.Select(x => x.UserNames).FirstOrDefault();

            //                                //Read email template
            //                                if (!string.IsNullOrWhiteSpace(toMailAddress))
            //                                {
            //                                    var splitUserNames = UserNames.Split(',');
            //                                    var splitUserEmails = toMailAddress.Split(',');

            //                                    string fileName = "EmailTemplates\\NightlySummary.html";
            //                                    string templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            //                                    StreamReader str = new StreamReader(templatePath);
            //                                    string emailBody = str.ReadToEnd();
            //                                    str.Close();

            //                                    emailBody = emailBody.Replace("[ProvinceName]", ProvinceName);
            //                                    emailBody = emailBody.Replace("[Date]", DateTime.Now.Date.ToString("dd/MM/yyyy"));
            //                                    emailBody = emailBody.Replace("[FileName]", fileUrl);
            //                                    emailBody = emailBody.Replace("[Description]", emailTitle);

            //                                    for (int i = 0; i < splitUserEmails.Length; i++)
            //                                    {
            //                                        string finalBody = emailBody;
            //                                        finalBody = finalBody.Replace("[UserName]", splitUserNames[i]);
            //                                        bool isMailSent = NightlySummaryReport.SendMail(splitUserEmails[i], emailTitle, finalBody);
            //                                    }
            //                                }
            //                                #endregion
            //                            }
            //                        }
            //                    }
            //                }
            //                else
            //                {
            //                    ErrorLog.WriteLogFile("ScheduleNotification()", "Preliminary schedule notification no found any data");
            //                }
            //            }
            //        }
            //    }
            //    ErrorLog.WriteLogFile("ScheduleNotification()", "Preliminary schedule notification ended");
            //}
            //catch (Exception ex)
            //{
            //    ErrorLog.Write("ScheduleJob", "ScheduleNotification - Preliminary", ex);
            //}
            //#endregion

            //#region Final
            //try
            //{
            //    ErrorLog.WriteLogFile("ScheduleNotification()", "Final schedule notification started");
            //    using (var client = new HttpClient())
            //    {
            //        client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseAddress"]);
            //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //        using (response = await client.GetAsync("api/ScheduleLocSummary/FinalScheduleNotification"))
            //        {
            //            ErrorLog.WriteLogFile("ScheduleNotification()", "Final schedule notification api call");
            //            if (response.StatusCode == HttpStatusCode.OK)
            //            {
            //                string responseBody = await response.Content.ReadAsStringAsync();
            //                List<ScheduleLocSummaryModel> scheduleLocSummaryModelsList = JsonConvert.DeserializeObject<List<ScheduleLocSummaryModel>>(responseBody);

            //                if (scheduleLocSummaryModelsList != null && scheduleLocSummaryModelsList.Count > 0)
            //                {
            //                    string RegionName = scheduleLocSummaryModelsList.Select(x => x.RegionName).FirstOrDefault();
            //                    Int64 RegionId = scheduleLocSummaryModelsList.Select(x => x.RegionId).FirstOrDefault();

            //                    //Generate PDF
            //                    string filename = RegionId + "_" + RegionName + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".pdf";
            //                    DataTable finalDT = NightlySummaryReport.ToDataTable(scheduleLocSummaryModelsList);
            //                    byte[] bytes = NightlySummaryReport.GenerateNightlySummaryPDF(finalDT, "MgmntNightlySummary.rdlc", "ds_NightlyReport", "tblNightlyReport");

            //                    //save Document and insert record
            //                    string emailTitle = "Provincial Data Centers Ward Roll Update Summary Report for " + DateTime.Now.Date.ToString("dd/MM/yyyy");
            //                    string fileUrl = await NightlySummaryReport.AddDocument(2, filename, bytes, emailTitle);

            //                    if (!string.IsNullOrWhiteSpace(fileUrl))
            //                    {
            //                        ErrorLog.WriteLogFile("ScheduleNotification()", "Final schedule notification file generated successfully.FileURL :" + fileUrl);

            //                        #region Send Email 
            //                        string toMailAddress = scheduleLocSummaryModelsList.Select(x => x.UserEmails).FirstOrDefault();
            //                        string UserNames = scheduleLocSummaryModelsList.Select(x => x.UserNames).FirstOrDefault();

            //                        //Read email template
            //                        if (!string.IsNullOrWhiteSpace(toMailAddress))
            //                        {
            //                            var splitUserNames = UserNames.Split(',');
            //                            var splitUserEmails = toMailAddress.Split(',');

            //                            string fileName = "EmailTemplates\\MgmntNightlySummary.html";
            //                            string templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            //                            StreamReader str = new StreamReader(templatePath);
            //                            string emailBody = str.ReadToEnd();
            //                            str.Close();

            //                            emailBody = emailBody.Replace("[Date]", DateTime.Now.Date.ToString("dd/MM/yyyy"));
            //                            emailBody = emailBody.Replace("[FileName]", fileUrl);
            //                            emailBody = emailBody.Replace("[Description]", emailTitle);

            //                            for (int i = 0; i < splitUserEmails.Length; i++)
            //                            {
            //                                string finalBody = emailBody;
            //                                finalBody = finalBody.Replace("[UserName]", splitUserNames[i]);
            //                                bool isMailSent = NightlySummaryReport.SendMail(splitUserEmails[i], emailTitle, finalBody);
            //                            }
            //                        }
            //                        #endregion
            //                    }
            //                }
            //                else
            //                {
            //                    ErrorLog.WriteLogFile("ScheduleNotification()", "Final schedule notification no found any data");
            //                }
            //            }
            //        }
            //        ErrorLog.WriteLogFile("ScheduleNotification()", "Final schedule notification ended");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ErrorLog.Write("ScheduleJob", "ScheduleNotification - final", ex);
            //}
            //#endregion

            #endregion

            #region Rejected
            try
            {
                //ErrorLog.WriteLogFile("ScheduleNotification()", "Rejected schedule notification started");

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseAddress"]);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    using (response = await client.GetAsync("api/ScheduleLocSummary/RejectedScheduleNotification"))
                    {
                        //ErrorLog.WriteLogFile("ScheduleNotification()", "Rejected schedule notification api call");
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            string responseBody = await response.Content.ReadAsStringAsync();
                            List<ScheduleLocSummaryRejectedModel> scheduleLocSummaryRejectedModelList = JsonConvert.DeserializeObject<List<ScheduleLocSummaryRejectedModel>>(responseBody);

                            if (scheduleLocSummaryRejectedModelList != null && scheduleLocSummaryRejectedModelList.Count > 0)
                            {
                                if (IsSunday())
                                {
                                    foreach (var item in scheduleLocSummaryRejectedModelList)
                                    {
                                        string RejectedTemplate = string.Format("{0} records are rejected, please check rejection section in mobile application.", item.RejectedRecords);

                                        bool isMailSent = NightlySummaryReport.SendMail(item.EmailAddress, "ScheduleNotification", RejectedTemplate);
                                    }
                                }
                            }
                            else
                            {
                                //ErrorLog.WriteLogFile("ScheduleNotification()", "Rejected schedule notification no found any data");
                            }
                        }
                    }
                }
                //ErrorLog.WriteLogFile("ScheduleNotification()", "Rejected schedule notification ended");
            }
            catch (Exception ex)
            {
                ErrorLog.Write("ScheduleJob", "ScheduleNotification - Rejected", ex);
            }
            #endregion

            #region RejectedByRD
            try
            {
                //ErrorLog.WriteLogFile("ScheduleNotification()", "RejectedByRD schedule notification started");
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseAddress"]);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    using (response = await client.GetAsync("api/ScheduleLocSummary/RejectedByRDScheduleNotification"))
                    {
                        //ErrorLog.WriteLogFile("ScheduleNotification()", "RejectedByRD schedule notification api call");
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            string responseBody = await response.Content.ReadAsStringAsync();
                            List<ScheduleLocSummaryRejectedByRDModel> scheduleLocSummaryRejectedByRDModelList = JsonConvert.DeserializeObject<List<ScheduleLocSummaryRejectedByRDModel>>(responseBody);

                            if (scheduleLocSummaryRejectedByRDModelList != null && scheduleLocSummaryRejectedByRDModelList.Count > 0)
                            {
                                if (IsSunday())
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
                                                bool isMailSent = NightlySummaryReport.SendMail(toMailAddress, "ScheduleNotification", RejectedByRDTemplate);
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                //ErrorLog.WriteLogFile("ScheduleNotification()", "RejectedByRD schedule notification no found any data");
                            }
                        }
                    }
                }
                //ErrorLog.WriteLogFile("ScheduleNotification()", "RejectedByRD schedule notification ended");
            }
            catch (Exception ex)
            {
                ErrorLog.Write("ScheduleJob", "ScheduleNotification - RejectedByRD", ex);
            }
            #endregion

            return response;
        }

        #endregion

        #region IsSunday
        /// <summary>
        /// Determines whether this instance is sunday.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is sunday; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsSunday()
        {
            // 0 is Sunday
            int dayOfWeek = 0;

            if (ConfigurationManager.AppSettings.AllKeys.Contains("dayOfWeek"))
            {
                string dayValue = Convert.ToString(ConfigurationManager.AppSettings["dayOfWeek"]);
                if (int.TryParse(dayValue, out int value))
                {
                    if(Convert.ToInt32(dayValue) >= 0 && Convert.ToInt32(dayValue) <= 6)
                    {
                        dayOfWeek = Convert.ToInt32(dayValue);
                    }
                }
            }

            if (DateTime.Now.DayOfWeek == (DayOfWeek)dayOfWeek)
            {
                return true;
            }

            return false;
        }
        #endregion
    }
}