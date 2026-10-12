using Microsoft.Reporting.WebForms;
using Newtonsoft.Json;
using PNGEC.API.Domain.Document;
using PNGEC.API.Helper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Reflection;
using System.Threading.Tasks;

namespace PNGEC.ScheduleJob.Report
{
    public class NightlySummaryReport
    {
        #region generate report
        /// <summary>
        /// Generates the nightly summary PDF.
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <param name="rdlcReport">The RDLC report.</param>
        /// <param name="dataSourceName">Name of the data source.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <returns></returns>
        public static byte[] GenerateNightlySummaryPDF(DataTable dt, string rdlcReport, string dataSourceName, string tableName)
        {
            byte[] bytes = null;
            try
            {
                DataSet dsExportReport = new DataSet();

                dt.TableName = tableName;
                if (!dsExportReport.Tables.Contains(dt.TableName))
                {
                    dsExportReport.Tables.Add(dt);
                }
                if (dsExportReport.Tables[0].Rows != null && dsExportReport.Tables[0].Rows.Count > 0)
                {
                    Warning[] warnings;
                    string[] streamIds;
                    string mimeType = string.Empty;
                    string encoding = string.Empty;
                    string extension = string.Empty;

                    ReportDataSource dataSource = new ReportDataSource(dataSourceName, dsExportReport.Tables[0].Copy());
                    ReportViewer viewer = new ReportViewer();
                    viewer.LocalReport.Refresh();

                    string fileName = "Report\\" + rdlcReport;
                    string reportPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

                    viewer.LocalReport.ReportPath = reportPath; //This is your rdlc name.
                    viewer.LocalReport.DataSources.Add(dataSource); // Add  datasource here   
                    bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
                }

            }
            catch (Exception ex)
            {
                ErrorLog.Write("ScheduleJob", "GenerateNightlySummaryPDF", ex);
            }
            return bytes;
        }
        #endregion

        #region convert model to datatable
        /// <summary>
        /// Converts to datatable.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            try
            {
                DataTable dt = new DataTable();
                //Get all the properties by using reflection   
                PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo prop in Props)
                {
                    //Setting column names as Property names  
                    dataTable.Columns.Add(prop.Name);
                }
                foreach (T item in items)
                {
                    var values = new object[Props.Length];
                    for (int i = 0; i < Props.Length; i++)
                    {

                        values[i] = Props[i].GetValue(item, null);
                    }
                    dataTable.Rows.Add(values);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Write("ScheduleJob", "convert ToDataTable", ex);
            }
            return dataTable;
        }
        #endregion

        #region add document into db and folder
        /// <summary>
        /// Adds the document.
        /// </summary>
        /// <param name="Type">The type.</param>
        /// <param name="FileName">Name of the file.</param>
        /// <param name="bytes">The bytes.</param>
        /// <param name="Description">The description.</param>
        /// <returns></returns>
        public static async Task<string> AddDocument(int Type, string FileName, byte[] bytes, string Description)
        {
            string filePath = string.Empty;
            try
            {
                MultipartFormDataContent formContent;

                formContent = new MultipartFormDataContent
                    {
                        {new StringContent(Type.ToString()),"Type"},
                        {new StringContent(Description), "Description"},
                        {new StreamContent(new MemoryStream(bytes)),"FileImage",FileName},
                        {new StringContent(FileName),"FileName" },
                        //{new StringContent(GlobalsModel.LoggedUser.User.UserId.ToString()), "CreatedBy"}
                    };

                Response res = null;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseAddress"]);
                    using (HttpResponseMessage response = client.PostAsync("api/Document/AddDeleteDocument", formContent).Result)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            string responseBody = await response.Content.ReadAsStringAsync();
                            res = JsonConvert.DeserializeObject<Response>(responseBody);
                        }
                    }
                }
                if (res != null)
                {
                    if (res.Status)
                    {
                        var documentData = JsonConvert.DeserializeObject<DocumentModel>(res.Data.ToString());
                        filePath = documentData.FileUrl;
                    }
                }

            }
            catch (Exception ex)
            {
                filePath = string.Empty;
                ErrorLog.Write("ScheduleJob", "Error into save document - 14 ", ex);
            }
            return filePath;
        }
        #endregion

        #region send email
        /// <summary>
        /// Sends the mail.
        /// </summary>
        /// <param name="toMailAddress">To mail address.</param>
        /// <param name="mailSubject">The mail subject.</param>
        /// <param name="mailBody">The mail body.</param>
        /// <returns></returns>
        public static bool SendMail(string toMailAddress, string mailSubject, string mailBody)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(ConfigurationManager.AppSettings["ServerName"]);

                //mail.From = new MailAddress(SettingsConfig.AppSetting("SmtpServer:FromAddress"));
                mail.From = new MailAddress(ConfigurationManager.AppSettings["FromAddress"]);
                mail.To.Add(toMailAddress);
                mail.Subject = mailSubject;
                mail.Body = mailBody;
                mail.IsBodyHtml = true;
                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                SmtpServer.Port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]);

                if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["UseDefaultCredentials"]))
                {
                    SmtpServer.UseDefaultCredentials = Convert.ToBoolean(ConfigurationManager.AppSettings["UseDefaultCredentials"]);
                }
                else
                {
                    SmtpServer.UseDefaultCredentials = true;
                }

                //SmtpServer.Credentials = new System.Net.NetworkCredential(SettingsConfig.AppSetting("SmtpServer:FromAddress"), SettingsConfig.AppSetting("SmtpServer:Password"));
                SmtpServer.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["FromAddress"], ConfigurationManager.AppSettings["Password"]);
                SmtpServer.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);

                SmtpServer.Send(mail);

                return true;
            }
            catch (Exception ex)
            {
                ErrorLog.Write("Email", "SendMail", ex);
                return false;
            }
        }
        #endregion
    }
}
