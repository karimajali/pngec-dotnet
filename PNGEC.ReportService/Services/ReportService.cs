using Microsoft.Reporting.WebForms;
using Newtonsoft.Json;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Domain.Report;
using PNGEC.API.Domain.User;
using PNGEC.API.Helper;
using PNGEC.ReportService.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace PNGEC.ReportService.Report
{
    public static class ReportService
    {
        #region declare variable
        /// <summary>
        /// The LST ward roll
        /// </summary>
        private static List<WardRollBookReortModel> lstWardRoll = null;

        /// <summary>
        /// The LST deletion ward roll
        /// </summary>
        private static List<WardRollBookReortModel> lstDeletionWardRoll = null;

        /// <summary>
        /// The dt export report, dt Voter Card, dt SubVoter Card, dt Ward Roll Front Cover
        /// </summary>
        private static DataTable dtExportReport = null, dtDeletionExportReport = null, dtVoterCard = null, dtSubVoterCard = null, dtWardRollFrontCover = null, dtWardClanSectionSummary = null;

        /// <summary>
        /// The ds export report, ds Voter Card
        /// </summary>
        private static DataSet dsExportReport = null, dsVoterCard = null;

        /// <summary>
        /// The super admin identifier
        /// </summary>
        private static int superAdminId = Convert.ToInt16(ConfigurationManager.AppSettings["SuperAdminId"]);
        #endregion

        /// <summary>
        /// Generates the ward roll report.
        /// </summary>
        /// <param name="selectedReport">The selected report.</param>
        /// <param name="selectedDBName">Name of the selected database.</param>
        /// <param name="TreeStructureName">Name of the tree structure.</param>
        /// <param name="GeoName">Name of the geo.</param>
        /// <param name="GeoId">The geo identifier.</param>
        /// <param name="IsEnvelopLabel">if set to <c>true</c> [is envelop label].</param>
        /// <returns></returns>
        public static async Task<int> GenerateWardRollReport(int selectedReport, string selectedDBName, string selectedServerName, string Gender, string TreeStructureName, string GeoName, int GeoId, bool IsEnvelopLabel, string saveFolderPath, EnvelopeExportModel envelopeExport, bool IsPossibleDeletion, int alphaSplitOption, string alphaSplitStart, string alphaSplitEnd, int selectedReportWardRollType)
        {
            lstWardRoll = null; dtExportReport = null; dtVoterCard = null; dtSubVoterCard = null; dtWardRollFrontCover = null;
            dsExportReport = null; dsVoterCard = null;
            string reportPath = string.Empty, genderSplit = string.Empty;
            int returnStatus = 0;

            #region gender split wise report name
            if (!string.IsNullOrWhiteSpace(Gender) && Gender == "M")
            {
                genderSplit = "_Male";
            }
            else if (!string.IsNullOrWhiteSpace(Gender) && Gender == "F")
            {
                genderSplit = "_Female";
            }
            else
            {
                genderSplit = string.Empty;
            }
            #endregion

            if (selectedReport == 1 || selectedReport == 5) //1 = ward roll, 5 = location roll
            {
                #region Ward Roll Report
                //string exportReportName = selectedDBName + "_" + TreeStructureName + genderSplit + "_WardRollBook" + ".pdf";
                string exportReportName = $"{selectedDBName}_{TreeStructureName}{genderSplit}_{GeoName}RollBook.pdf";
                reportPath = saveFolderPath + "\\" + exportReportName;

                await GetExportRecordsData(selectedDBName, selectedServerName, Gender, GeoName, GeoId, alphaSplitOption, alphaSplitStart, alphaSplitEnd);
                if (IsPossibleDeletion)
                {
                    await GetDeletionExportRecordsData(selectedDBName, selectedServerName, Gender, GeoName, GeoId, alphaSplitOption, alphaSplitStart, alphaSplitEnd);
                }
                else
                {
                    dtDeletionExportReport = new DataTable();
                }

                if (dtExportReport != null && dtExportReport.Rows.Count > 0)
                {
                    byte[] bytes = WardRollBookExportReport(selectedDBName, Gender, IsEnvelopLabel, IsPossibleDeletion, GeoName, alphaSplitOption, alphaSplitStart, alphaSplitEnd, selectedReportWardRollType);
                    if (bytes != null)
                    {
                        using (FileStream stream = new FileStream(reportPath, FileMode.Create))
                        {
                            stream.Write(bytes, 0, bytes.Length);
                        }

                        // Envelope Export Report
                        if (IsEnvelopLabel == true && envelopeExport != null)
                        {
                            byte[] bytesEnv = VoterRollEnvelopeExportReportSource(envelopeExport, selectedDBName);
                            if (bytesEnv != null)
                            {
                                if (GeoName == "Location")
                                {
                                    reportPath = reportPath.Replace("LocationRollBook", "LocationRollEnvelope");
                                }
                                else
                                {
                                    reportPath = reportPath.Replace("WardRollBook", "WardRollEnvelope");
                                }

                                using (FileStream stream = new FileStream(reportPath, FileMode.Create))
                                {
                                    stream.Write(bytesEnv, 0, bytesEnv.Length);
                                }
                            }
                        }

                        returnStatus = 1; // success
                    }
                    else
                    {
                        returnStatus = -1; // something went wrong
                    }
                }
                else
                {
                    returnStatus = -2; // no data found
                }

                #endregion
            }
            else if (selectedReport == 2)
            {
                #region Ward Photo Roll Report

                string exportReportName = selectedDBName + "_" + TreeStructureName + genderSplit + "_WardPhotoRollBook" + ".pdf";
                reportPath = saveFolderPath + "\\" + exportReportName;

                await GetExportRecordsDataForWardPhoto(selectedDBName, selectedServerName, Gender, GeoName, GeoId, alphaSplitOption, alphaSplitStart, alphaSplitEnd);

                if (IsPossibleDeletion)
                {
                    await GetDeletionExportRecordsData(selectedDBName, selectedServerName, Gender, GeoName, GeoId, alphaSplitOption, alphaSplitStart, alphaSplitEnd);
                }
                else
                {
                    dtDeletionExportReport = new DataTable();
                }

                if (dtExportReport != null && dtExportReport.Rows.Count > 0)
                {
                    byte[] bytes = WardPhotoRollBookExportReport(selectedDBName, Gender, IsEnvelopLabel, IsPossibleDeletion, alphaSplitOption, alphaSplitStart, alphaSplitEnd);

                    if (bytes != null)
                    {
                        using (FileStream stream = new FileStream(reportPath, FileMode.Create))
                        {
                            stream.Write(bytes, 0, bytes.Length);
                        }

                        // Envelope Export Report
                        if (IsEnvelopLabel == true && envelopeExport != null)
                        {
                            byte[] bytesEnv = VoterRollEnvelopeExportReportSource(envelopeExport, selectedDBName);
                            if (bytesEnv != null)
                            {
                                reportPath = reportPath.Replace("WardPhotoRollBook", "WardPhotoRollBook_RollEnvelope");
                                using (FileStream stream = new FileStream(reportPath, FileMode.Create))
                                {
                                    stream.Write(bytesEnv, 0, bytesEnv.Length);
                                }
                            }
                        }

                        returnStatus = 1;
                    }
                    else
                    {
                        returnStatus = -1;
                    }
                }
                else
                {
                    returnStatus = -2;
                }

                #endregion
            }
            else if (selectedReport == 3)
            {
                #region Ward Voter Id Card Report

                string exportReportName = selectedDBName + "_" + TreeStructureName + "_WardVoterIdCards" + ".pdf";
                reportPath = saveFolderPath + "\\" + exportReportName;

                dsVoterCard = new DataSet();
                await GetExportRecordsData(selectedDBName, selectedServerName, Gender, GeoName, GeoId, alphaSplitOption, alphaSplitStart, alphaSplitEnd);

                if (dtExportReport != null && dtExportReport.Rows.Count > 0)
                {
                    DataView view = new DataView(dtExportReport);
                    dtVoterCard = view.ToTable("tblVoterIdCard", false, "ElectorId", "Seq");
                    dtSubVoterCard = view.ToTable("tblSubVoterIdCard", false,
                    "ElectorId",
                    "Seq",
                    "LName",
                    "FName",
                    "Gen",
                    "Gender",
                    "BDate",
                    "WithDisability",
                    "Province",
                    "District",
                    "LLG",
                    "Ward",
                    "Location",
                    "ElectorProfileImageUrl",
                    "NID",
                    "NatIDRecordNo",
                    "Religion",
                    "Occupation",
                    "WardNo",
                    "ProvinceCode",
                    "Clan");
                    dsVoterCard.Tables.AddRange(new DataTable[] { dtVoterCard, dtSubVoterCard });

                    if (dsVoterCard.Tables[0] != null && dsVoterCard.Tables[0].Rows.Count > 0)
                    {
                        byte[] bytes = VoterIdCardExportReportSource();

                        if (bytes != null)
                        {
                            using (FileStream stream = new FileStream(reportPath, FileMode.Create))
                            {
                                stream.Write(bytes, 0, bytes.Length);
                            }

                            returnStatus = 1;
                        }
                        else
                        {
                            returnStatus = -1;
                        }
                    }
                }
                else
                {
                    returnStatus = -2;
                }
                #endregion
            }
            else if (selectedReport == 6) //6 = ward clan roll
            {
                #region Ward Roll Report
                //string exportReportName = selectedDBName + "_" + TreeStructureName + genderSplit + "_WardRollBook" + ".pdf";
                string exportReportName = $"{selectedDBName}_{TreeStructureName}{genderSplit}_{GeoName}ClanRollBook.pdf";
                reportPath = saveFolderPath + "\\" + exportReportName;

                await GetExportRecordsData(selectedDBName, selectedServerName, Gender, GeoName, GeoId, alphaSplitOption, alphaSplitStart, alphaSplitEnd);

                await GetWardClanSectionSummary(selectedDBName, GeoName, GeoId, selectedServerName);

                if (IsPossibleDeletion)
                {
                    await GetDeletionExportRecordsData(selectedDBName, selectedServerName, Gender, GeoName, GeoId, alphaSplitOption, alphaSplitStart, alphaSplitEnd);
                }
                else
                {
                    dtDeletionExportReport = new DataTable();
                }

                if (dtExportReport != null && dtExportReport.Rows.Count > 0)
                {
                    byte[] bytes = WardClanRollBookExportReport(selectedDBName, Gender, IsEnvelopLabel, IsPossibleDeletion, GeoName, alphaSplitOption, alphaSplitStart, alphaSplitEnd);
                    if (bytes != null)
                    {
                        using (FileStream stream = new FileStream(reportPath, FileMode.Create))
                        {
                            stream.Write(bytes, 0, bytes.Length);
                        }

                        // Envelope Export Report
                        if (IsEnvelopLabel == true && envelopeExport != null)
                        {
                            byte[] bytesEnv = VoterRollEnvelopeExportReportSource(envelopeExport, selectedDBName);
                            if (bytesEnv != null)
                            {
                                if (GeoName == "Location")
                                {
                                    reportPath = reportPath.Replace("LocationRollBook", "LocationRollEnvelope");
                                }
                                else
                                {
                                    reportPath = reportPath.Replace("WardRollBook", "WardRollEnvelope");
                                }

                                using (FileStream stream = new FileStream(reportPath, FileMode.Create))
                                {
                                    stream.Write(bytesEnv, 0, bytesEnv.Length);
                                }
                            }
                        }

                        returnStatus = 1; // success
                    }
                    else
                    {
                        returnStatus = -1; // something went wrong
                    }
                }
                else
                {
                    returnStatus = -2; // no data found
                }

                #endregion
            }
            return returnStatus;
        }

        #region GetWardClanSectionSummary
        /// <summary>
        /// Gets the ward clan section summary.
        /// </summary>
        private static async Task GetWardClanSectionSummary(string selectedDBName, string GeoName, int GeoId, string selectedServerName)
        {
            #region pageList
            PageList pageList = new PageList();
            // Current Database Filed 
            pageList.DatabaseName = selectedDBName;

            // TreeView Filed
            pageList.GeoName = GeoName;
            pageList.GeoId = GeoId;
            #endregion

            var lstWardClanSectionSummary = new List<GetWardClanSectionSummaryModel>();

            HttpResponseMessage responseNew = null;
            Response<GetWardClanSectionSummaryModel> response = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(GetServerBaseUrl(selectedServerName));
                client.DefaultRequestHeaders.Accept.Clear();
                var dataContent = new StringContent(JsonConvert.SerializeObject(pageList), Encoding.UTF8, "application/json");
                responseNew = await client.PostAsync("File/GetWardClanSectionSummaryList", dataContent);

                if (responseNew.IsSuccessStatusCode)
                {
                    var returnData = await responseNew.Content.ReadAsStringAsync();
                    //var returnData = responseNew.Content.ReadAsStringAsync().Result;
                    response = JsonConvert.DeserializeObject<Response<GetWardClanSectionSummaryModel>>(returnData);
                }
            }

            if (response != null && response.Status && response.Data.ListItems.ToList() != null && response.Data.ListItems.ToList().Count > 0)
            {
                lstWardClanSectionSummary.AddRange(response.Data.ListItems.ToList());
            }

            if (lstWardClanSectionSummary != null && lstWardClanSectionSummary.Count > 0)
            {
                dtWardClanSectionSummary = new DataTable();
                dtWardClanSectionSummary = ToDataTable(lstWardClanSectionSummary);
            }

            lstWardClanSectionSummary.Clear();
        }

        #endregion

        /// <summary>
        /// Gets the export records data.
        /// </summary>
        /// <param name="selectedDBName">Name of the selected database.</param>
        /// <param name="GeoName">Name of the geo.</param>
        /// <param name="GeoId">The geo identifier.</param>
        private static async Task GetExportRecordsData(string selectedDBName, string selectedServerName, string Gender, string GeoName, int GeoId, int alphaSplitOption, string alphaSplitStart, string alphaSplitEnd)
        {
            try
            {
                #region pageList
                PageList pageList = new PageList();

                // Current Database Filed 
                pageList.DatabaseName = selectedDBName;


                //pageList.KeyWord = txtSearch.Text;

                if (selectedDBName == "Final")
                {
                    // Filter Filed
                    pageList.FiledName = "Status";
                    pageList.FiledValue = "WARD ROLL";
                }
                else
                {
                    // Filter Filed
                    pageList.FiledName = "Status";
                    pageList.FiledValue = "WARD ROLL";
                }

                // Gender Split
                pageList.Gender = Gender;
                pageList.Disability = "All";

                pageList.Photo = "All";
                pageList.DateRange = "All";
                pageList.DateFrom = null;
                pageList.DateTo = null;

                pageList.ElectorByIDs = "All";

                //pageList.AlphaSplit = "All";
                //pageList.AlphaSplitStart = string.Empty;
                //pageList.AlphaSplitEnd = string.Empty;

                if (alphaSplitOption == 1)
                {
                    pageList.AlphaSplit = "AlphaSplit";
                    pageList.AlphaSplitStart = alphaSplitStart;
                    pageList.AlphaSplitEnd = alphaSplitEnd;
                }
                else
                {
                    pageList.AlphaSplit = "All";
                    pageList.AlphaSplitStart = string.Empty;
                    pageList.AlphaSplitEnd = string.Empty;
                }

                // TreeView Filed
                pageList.GeoName = GeoName;
                pageList.GeoId = GeoId;

                // Login User
                pageList.LoggedUserId = superAdminId;

                // Page No
                int PageNo = 1;

                // Page Size
                pageList.PageSize = 1000;
                #endregion

                lstWardRoll = new List<WardRollBookReortModel>();

                bool DataFound = false;

                do
                {
                    pageList.PageNo = PageNo;
                    HttpResponseMessage responseNew = null;
                    Response<WardRollBookReortModel> response = null;
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(GetServerBaseUrl(selectedServerName));
                        client.DefaultRequestHeaders.Accept.Clear();
                        var dataContent = new StringContent(JsonConvert.SerializeObject(pageList), Encoding.UTF8, "application/json");

                        responseNew = await client.PostAsync("File/GetExportRecordsList", dataContent);
                        //responseNew = await client.PostAsync("File/GetExportRecordsList", dataContent).Result;

                        if (responseNew.IsSuccessStatusCode)
                        {
                            var returnData = await responseNew.Content.ReadAsStringAsync();
                            //var returnData = responseNew.Content.ReadAsStringAsync().Result;
                            response = JsonConvert.DeserializeObject<Response<WardRollBookReortModel>>(returnData);
                        }
                    }
                    if (response != null && response.Status && response.Data.ListItems.ToList() != null && response.Data.ListItems.ToList().Count > 0)
                    {
                        lstWardRoll.AddRange(response.Data.ListItems.ToList());
                        DataFound = true;
                    }
                    else
                    {
                        DataFound = false;
                    }

                    PageNo++;

                } while (DataFound);

                if (lstWardRoll != null && lstWardRoll.Count > 0)
                {
                    dtExportReport = new DataTable();
                    lstWardRoll.ForEach(x => x.MKAClanName = x.ClanName);
                    dtExportReport = ToDataTable(lstWardRoll);
                }

                //lstWardRoll.Clear();

            }
            catch (Exception ex)
            {
                ErrorLog.Write("ReportService", "GetExportRecordsData", ex);
            }
        }

        /// <summary>
        /// Gets the deletion export records data.
        /// </summary>
        private static async Task GetDeletionExportRecordsData(string selectedDBName, string selectedServerName, string Gender, string GeoName, int GeoId, int alphaSplitOption, string alphaSplitStart, string alphaSplitEnd)
        {
            #region pageList
            PageList pageList = new PageList();

            // Current Database Filed 
            pageList.DatabaseName = selectedDBName;

            // Filter Filed
            pageList.FiledName = "Status";
            pageList.FiledValue = "DELETED";

            // Gender Split
            pageList.Gender = Gender;
            pageList.Disability = "All";

            pageList.Photo = "All";
            pageList.DateRange = "All";
            pageList.DateFrom = null;
            pageList.DateTo = null;

            pageList.ElectorByIDs = "All";

            //pageList.AlphaSplit = "All";
            //pageList.AlphaSplitStart = string.Empty;
            //pageList.AlphaSplitEnd = string.Empty;

            if (alphaSplitOption == 1)
            {
                pageList.AlphaSplit = "AlphaSplit";
                pageList.AlphaSplitStart = alphaSplitStart;
                pageList.AlphaSplitEnd = alphaSplitEnd;
            }
            else
            {
                pageList.AlphaSplit = "All";
                pageList.AlphaSplitStart = string.Empty;
                pageList.AlphaSplitEnd = string.Empty;
            }

            // TreeView Filed
            pageList.GeoName = GeoName;
            pageList.GeoId = GeoId;

            // Login User
            pageList.LoggedUserId = superAdminId;

            // Page No
            int PageNo = 1;

            // Page Size
            pageList.PageSize = 1000;
            #endregion

            lstDeletionWardRoll = new List<WardRollBookReortModel>();

            bool DataFound = false;

            do
            {
                pageList.PageNo = PageNo;

                HttpResponseMessage responseNew = null;
                Response<WardRollBookReortModel> response = null;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(GetServerBaseUrl(selectedServerName));
                    client.DefaultRequestHeaders.Accept.Clear();
                    var dataContent = new StringContent(JsonConvert.SerializeObject(pageList), Encoding.UTF8, "application/json");
                    responseNew = await client.PostAsync("File/GetExportRecordsList", dataContent);
                    //responseNew = await client.PostAsync("File/GetExportRecordsList", dataContent).Result;

                    if (responseNew.IsSuccessStatusCode)
                    {
                        var returnData = await responseNew.Content.ReadAsStringAsync();
                        //var returnData = responseNew.Content.ReadAsStringAsync().Result;
                        response = JsonConvert.DeserializeObject<Response<WardRollBookReortModel>>(returnData);
                    }
                }
                if (response != null && response.Status && response.Data.ListItems.ToList() != null && response.Data.ListItems.ToList().Count > 0)
                {
                    lstDeletionWardRoll.AddRange(response.Data.ListItems.ToList());
                    DataFound = true;
                }
                else
                {
                    DataFound = false;
                }

                PageNo++;

            } while (DataFound);

            if (dtDeletionExportReport == null)
            {
                dtDeletionExportReport = new DataTable();
            }
            else
            {
                dtDeletionExportReport.Clear();
            }

            if (lstDeletionWardRoll != null && lstDeletionWardRoll.Count > 0)
            {
                dtDeletionExportReport = ToDataTable(lstDeletionWardRoll);
            }

            lstDeletionWardRoll.Clear();
        }


        /// <summary>
        /// Gets the export records list.
        /// </summary>
        private static byte[] WardClanRollBookExportReport(string selectedDBName, string Gender, bool IsEnvelopLabel, bool IsPossibleDeletion, string geoName, int alphaSplitOption, string alphaSplitStart, string alphaSplitEnd)
        {
            byte[] bytest = null;

            if (dtExportReport != null && dtExportReport.Rows.Count > 0)
            {
                bytest = WardClanRollBookExportReportSource(selectedDBName, Gender, IsEnvelopLabel, IsPossibleDeletion, geoName, alphaSplitOption, alphaSplitStart, alphaSplitEnd);
            }
            return bytest;
        }

        /// <summary>
        /// Wards clan  roll book export report source.
        /// </summary>
        /// <param name="report">The report.</param>
        /// <param name="dt">The dt.</param>
        /// <returns></returns>      
        private static byte[] WardClanRollBookExportReportSource(string report, string Gender, bool IsEnvelopLabel, bool IsPossibleDeletion, string geoName, int alphaSplitOption, string alphaSplitStart, string alphaSplitEnd)
        {
            dsExportReport = new DataSet();
            byte[] bytes = null;

            dtExportReport.TableName = "tblWardRollBook";
            if (!dsExportReport.Tables.Contains(dtExportReport.TableName))
            {
                dsExportReport.Tables.Add(dtExportReport);
            }

            if (dsExportReport.Tables[0].Rows != null && dsExportReport.Tables[0].Rows.Count > 0)
            {
                string ReportTitle = string.Empty, Version = string.Empty, rdlcReport = string.Empty, GenderSplit = string.Empty, AlphaSplitStart = string.Empty, AlphaSplitEnd = string.Empty, ClanName = string.Empty;
                bool IsLocation = false, IsGenderSplit = false, IsAlphaSplit = false;

                if (geoName == "Location")
                {
                    IsLocation = true;
                }

                GenderSplit = Gender;

                if (Gender != "All")
                {
                    IsGenderSplit = true;
                    if (Gender == "M")
                    {
                        GenderSplit = "Male";
                    }
                    else if (Gender == "F")
                    {
                        GenderSplit = "Female";
                    }
                }
                else
                {
                    IsGenderSplit = false;
                }

                // get Clan Name

                //int wardId = Convert.ToInt32(dtWardClanSectionSummary.Rows[0]["WardId"].ToString());
                var clans = lstWardRoll.Where(c => c.ClanName != "").Select(x => x.ClanName).OrderBy(name => name).Distinct().ToList();

                ClanName = string.Join(", ", clans);


                // isAlphaSplit
                if (alphaSplitOption == 1)
                {
                    IsAlphaSplit = true;
                    AlphaSplitStart = alphaSplitStart;
                    AlphaSplitEnd = alphaSplitEnd;
                }

                #region RDLC Report Name
                if (report == "Staging")
                {
                    ReportTitle = $"WORKING ROLL {DateTime.Now.ToString("yyyy")}";
                    Version = "V1";

                    rdlcReport = "WardClanRollBook.rdlc";
                }
                else if (report == "Preliminary")
                {
                    ReportTitle = $"PRELIMINARY ELECTORAL ROLL {DateTime.Now.ToString("yyyy")}";
                    Version = "version 1";

                    rdlcReport = "WardClanRollBookPreliminary.rdlc";
                }
                else if (report == "Final")
                {
                    ReportTitle = $"ELECTORAL ROLL {DateTime.Now.ToString("yyyy")}";
                    Version = "version 1";

                    rdlcReport = "WardClanRollBookFinal.rdlc";
                }
                #endregion


                Warning[] warnings;
                string[] streamIds;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;

                #region RDLC parameters
                int paramCount = 19;
                if (report == "Final")
                {
                    paramCount = 21;
                }

                //This is optional if you have parameter then you can add parameters as much as you want
                ReportParameter[] param = new ReportParameter[paramCount];
                param[0] = new ReportParameter("Region", dsExportReport.Tables[0].Rows[0]["Region"].ToString(), true);
                param[1] = new ReportParameter("Province", dsExportReport.Tables[0].Rows[0]["Province"].ToString(), true);
                param[2] = new ReportParameter("District", dsExportReport.Tables[0].Rows[0]["District"].ToString(), true);
                param[3] = new ReportParameter("LLG", dsExportReport.Tables[0].Rows[0]["LLG"].ToString(), true);
                param[4] = new ReportParameter("Ward", string.Format("({0}) - {1}", dsExportReport.Tables[0].Rows[0]["WardNo"].ToString(), dsExportReport.Tables[0].Rows[0]["MKAWard"].ToString()), true);
                //param[4] = new ReportParameter("Ward", dsExportReport.Tables[0].Rows[0]["Ward"].ToString(), true);
                if (IsLocation)
                {
                    param[5] = new ReportParameter("Location", dsExportReport.Tables[0].Rows[0]["Location"].ToString(), true);
                }
                else
                {
                    IsLocation = true;
                    var locations = (from DataRow dr in dsExportReport.Tables[0].Rows where dr["Location"] != DBNull.Value select (string)dr["Location"]).Distinct().OrderBy(name => name);
                    //var locations = dsExportReport.Tables[0].AsEnumerable().Where(r => r.Field<string>("MKALocation") != null).Select(x => x.Field<string>("MKALocation")).Distinct().OrderBy(name => name);

                    param[5] = new ReportParameter("Location", string.Join(", ", locations), true);
                }
                param[6] = new ReportParameter("Clan", ClanName, true);
                param[7] = new ReportParameter("Electors", dsExportReport.Tables[0].Rows.Count.ToString(), true);
                param[8] = new ReportParameter("DateTime", DateTime.Now.ToString("dddd, MMMM dd, yyyy"), true);
                param[9] = new ReportParameter("ReportTitle", ReportTitle, true);
                param[10] = new ReportParameter("Version", Version, true);
                param[11] = new ReportParameter("WardNumberWardName", dsExportReport.Tables[0].Rows[0]["WardNumberWardName"].ToString(), true);
                param[12] = new ReportParameter("IsDeletionReport", Convert.ToString(IsPossibleDeletion), true);
                param[13] = new ReportParameter("IsLocation", Convert.ToString(IsLocation), true);
                param[14] = new ReportParameter("IsGenderSplit", Convert.ToString(IsGenderSplit), true);
                param[15] = new ReportParameter("GenderSplit", GenderSplit, true);
                param[16] = new ReportParameter("IsAlphaSplit", Convert.ToString(IsAlphaSplit), true);
                param[17] = new ReportParameter("AlphaSplitStart", AlphaSplitStart, true);
                param[18] = new ReportParameter("AlphaSplitEnd", AlphaSplitEnd, true);

                if (report == "Final")
                {
                    param[19] = new ReportParameter("GeoNameAndYear", "Electorate Election 2024", true);
                    param[20] = new ReportParameter("FooterDate", DateTime.Now.ToString("dd-MMM-yyyy"), true);
                }
                #endregion

                ReportDataSource dataSource = new ReportDataSource("ds_MKAWardRollBook", dsExportReport.Tables[0].Copy());

                #region Possible Deletion Report

                dtDeletionExportReport.TableName = "tblWardRollDeletion";
                dsExportReport.Tables.Add(dtDeletionExportReport.Copy());
                ReportDataSource datasourceDeletion = new ReportDataSource("ds_WardRollDeletion", dsExportReport.Tables[1].Copy());

                // Create Tables
                DataTable tblMKAWardClanSectionSummary = new DataTable("tblMKAWardClanSectionSummary");
                DataTable tblWardRollDeletion = new DataTable("tblWardRollDeletion");
                DataTable tblWardRollDeletionTrnsOutStatus = new DataTable("tblWardRollDeletionTrnsOutStatus");
                DataTable tblWardRollDeletionTrnsOutForm11 = new DataTable("tblWardRollDeletionTrnsOutForm11");

                #region Ward Roll Deletion
                tblWardRollDeletion.Clear();
                var wardRollDeletion = from rows in dtDeletionExportReport.AsEnumerable()
                                       where (rows.Field<string>("Status") != "TRANSFERRED OUT")
                                       select rows;

                if (wardRollDeletion != null && wardRollDeletion.Count() > 0)
                {
                    tblWardRollDeletion.Merge(wardRollDeletion.CopyToDataTable());

                    if (tblWardRollDeletion != null && tblWardRollDeletion.Rows != null && tblWardRollDeletion.Rows.Count > 0)
                    {
                        for (int i = 0; i < tblWardRollDeletion.Rows.Count; i++)
                        {
                            tblWardRollDeletion.Rows[i]["Seq"] = (i + 1);
                        }
                    }
                }
                #endregion

                #region Ward Roll Deletion Transferred Out Status
                tblWardRollDeletionTrnsOutStatus.Clear();
                var wardRollDeletionTrnsOutStatus = from rows in dtDeletionExportReport.AsEnumerable()
                                                    where (rows.Field<string>("Status") == "TRANSFERRED OUT")
                                                       && (rows.Field<int>("TransferInLocationId") == 0)
                                                    select rows;
                if (wardRollDeletionTrnsOutStatus != null && wardRollDeletionTrnsOutStatus.Count() > 0)
                {
                    tblWardRollDeletionTrnsOutStatus.Merge(wardRollDeletionTrnsOutStatus.CopyToDataTable());

                    if (tblWardRollDeletionTrnsOutStatus != null && tblWardRollDeletionTrnsOutStatus.Rows != null && tblWardRollDeletionTrnsOutStatus.Rows.Count > 0)
                    {
                        for (int i = 0; i < tblWardRollDeletionTrnsOutStatus.Rows.Count; i++)
                        {
                            tblWardRollDeletionTrnsOutStatus.Rows[i]["Seq"] = (i + 1);
                        }
                    }
                }
                #endregion

                #region Ward Roll Deletion Transferred Out Form11
                tblWardRollDeletionTrnsOutForm11.Clear();
                var wardRollDeletionTrnsOutForm11 = from rows in dtDeletionExportReport.AsEnumerable()
                                                    where (rows.Field<string>("Status") == "TRANSFERRED OUT")
                                                       && (rows.Field<int>("TransferInLocationId") > 0)
                                                    select rows;
                if (wardRollDeletionTrnsOutForm11 != null && wardRollDeletionTrnsOutForm11.Count() > 0)
                {
                    tblWardRollDeletionTrnsOutForm11.Merge(wardRollDeletionTrnsOutForm11.CopyToDataTable());

                    if (tblWardRollDeletionTrnsOutForm11 != null && tblWardRollDeletionTrnsOutForm11.Rows != null && tblWardRollDeletionTrnsOutForm11.Rows.Count > 0)
                    {
                        for (int i = 0; i < tblWardRollDeletionTrnsOutForm11.Rows.Count; i++)
                        {
                            tblWardRollDeletionTrnsOutForm11.Rows[i]["Seq"] = (i + 1);
                        }
                    }
                }
                #endregion

                #endregion

                if (dtWardRollFrontCover == null)
                {
                    dtWardRollFrontCover = new DataTable("tblWardRollFrontCover");
                }
                dtWardRollFrontCover = new DataTable("tblWardRollFrontCover");
                ReportDataSource datasourceWardRollFrontCover = new ReportDataSource("ds_MKAWardRollFrontCover", dtWardRollFrontCover.Copy());

                #region MKAWardClanSectionSummary
                if (report == "Final")
                {
                    if (dtWardClanSectionSummary.Rows != null && dtWardClanSectionSummary.Rows.Count > 0)
                    {
                        tblMKAWardClanSectionSummary.Clear();
                        tblMKAWardClanSectionSummary = dtWardClanSectionSummary.Copy();
                    }
                }
                #endregion

                ReportDataSource datasourceMKAWardClanSectionSummary = new ReportDataSource("ds_MKAWardClanSectionSummary", tblMKAWardClanSectionSummary);
                ReportDataSource dataSourceWardRollDeletionReport = new ReportDataSource("ds_MKAWardRollDeletion", tblWardRollDeletion);
                ReportDataSource datasourceWardRollDeletionTrnsOutStatus = new ReportDataSource("ds_MKAWardRollDeletionTrnsOutStatus", tblWardRollDeletionTrnsOutStatus);
                ReportDataSource datasourceWardRollDeletionTrnsOutForm11 = new ReportDataSource("ds_MKAWardRollDeletionTrnsOutForm11", tblWardRollDeletionTrnsOutForm11);

                //ReportViewer viewer = new ReportViewer();
                //viewer.LocalReport.Refresh();
                //viewer.LocalReport.ReportPath = "Report/" + rdlcReport; //This is your rdlc name.

                ReportViewer viewer = new ReportViewer();
                viewer.LocalReport.Refresh();
                string fileName = "Reports\\" + rdlcReport;
                string reportPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
                viewer.LocalReport.ReportPath = reportPath; //This is your rdlc name. 

                viewer.LocalReport.SetParameters(param);

                viewer.LocalReport.DataSources.Add(dataSource); // Add  datasource here
                if (report == "Final")
                {
                    viewer.LocalReport.DataSources.Add(datasourceMKAWardClanSectionSummary); // Add Ward clan section summary here
                }
                //viewer.LocalReport.DataSources.Add(datasourceDeletion); // Add Deletion datasource here
                viewer.LocalReport.DataSources.Add(dataSourceWardRollDeletionReport); // Add Deletion datasource here
                viewer.LocalReport.DataSources.Add(datasourceWardRollDeletionTrnsOutStatus); // Add Deletion datasource here
                viewer.LocalReport.DataSources.Add(datasourceWardRollDeletionTrnsOutForm11); // Add Deletion datasource here
                viewer.LocalReport.DataSources.Add(datasourceWardRollFrontCover); // Add Deletion datasource here

                bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
            }
            return bytes;
        }

        /// <summary>
        /// Gets the export records list.
        /// </summary>
        private static byte[] WardRollBookExportReport(string selectedDBName, string Gender, bool IsEnvelopLabel, bool IsPossibleDeletion, string geoName, int alphaSplitOption, string alphaSplitStart, string alphaSplitEnd, int selectedReportWardRollType)
        {
            byte[] bytest = null;

            if (dtExportReport != null && dtExportReport.Rows.Count > 0)
            {
                bytest = WardRollBookExportReportSource(selectedDBName, Gender, IsEnvelopLabel, IsPossibleDeletion, geoName, alphaSplitOption, alphaSplitStart, alphaSplitEnd, selectedReportWardRollType);
            }
            return bytest;
        }

        /// <summary>
        /// Wards the roll book export report source.
        /// </summary>
        /// <param name="report">The report.</param>
        /// <param name="dt">The dt.</param>
        /// <returns></returns>
        private static byte[] WardRollBookExportReportSource(string report, string Gender, bool IsEnvelopLabel, bool IsPossibleDeletion, string geoName, int alphaSplitOption, string alphaSplitStart, string alphaSplitEnd, int selectedReportWardRollType)
        {
            byte[] bytes = null; string errormsg = string.Empty;
            try
            {
                dsExportReport = new DataSet();

                dtExportReport.TableName = "tblWardRollBook";
                if (!dsExportReport.Tables.Contains(dtExportReport.TableName))
                {

                    if (selectedReportWardRollType == 2) // landscap report
                    {
                        var sortedVyLocation = dtExportReport.AsEnumerable()
                   .OrderBy(r => r.Field<string>("Location")).CopyToDataTable();

                        for (int i = 0; i < sortedVyLocation.Rows.Count; i++)
                        {
                            sortedVyLocation.Rows[i]["Seq"] = (i + 1);
                        }

                        dsExportReport.Tables.Add(sortedVyLocation);
                    }
                    else
                    {
                        dsExportReport.Tables.Add(dtExportReport);
                    }
                }

                if (dsExportReport.Tables[0].Rows != null && dsExportReport.Tables[0].Rows.Count > 0)
                {
                    string ReportTitle = string.Empty, Version = string.Empty, rdlcReport = string.Empty, GenderSplit = string.Empty, AlphaSplitStart = string.Empty, AlphaSplitEnd = string.Empty;
                    bool IsLocation = false, IsGenderSplit = false, IsAlphaSplit = false;

                    if (geoName == "Location" || geoName == "Ward")
                    {
                        IsLocation = true;
                    }

                    GenderSplit = Gender;

                    if (Gender != "All")
                    {
                        IsGenderSplit = true;
                        if (Gender == "M")
                        {
                            GenderSplit = "Male";
                        }
                        else if (Gender == "F")
                        {
                            GenderSplit = "Female";
                        }
                    }
                    else
                    {
                        IsGenderSplit = false;
                    }

                    // isAlphaSplit
                    if (alphaSplitOption == 1)
                    {
                        IsAlphaSplit = true;
                        AlphaSplitStart = alphaSplitStart;
                        AlphaSplitEnd = alphaSplitEnd;
                    }

                    #region RDLC Report Name
                    if (report == "Staging")
                    {
                        ReportTitle = $"WORKING ROLL {DateTime.Now.ToString("yyyy")}";
                        Version = "V1";

                        if (selectedReportWardRollType == 2) // landscap report
                        {
                            rdlcReport = "WardRollBookLandscap.rdlc";
                        }
                        else
                        {
                            rdlcReport = "WardRollBook.rdlc";
                        }
                    }
                    else if (report == "Preliminary")
                    {
                        ReportTitle = $"PRELIMINARY ELECTORAL ROLL {DateTime.Now.ToString("yyyy")}";
                        Version = "version 1";

                        rdlcReport = "WardRollBookPreliminary.rdlc";
                    }
                    else if (report == "Final")
                    {
                        ReportTitle = $"ELECTORAL ROLL {DateTime.Now.ToString("yyyy")}";
                        Version = "version 1";

                        rdlcReport = "WardRollBookFinal.rdlc";
                    }
                    #endregion

                    Warning[] warnings;
                    string[] streamIds;
                    string mimeType = string.Empty;
                    string encoding = string.Empty;
                    string extension = string.Empty;

                    #region RDLC parameters
                    int paramCount = 19;
                    if (report == "Final")
                    {
                        paramCount = 21;
                    }

                    //This is optional if you have parameter then you can add parameters as much as you want
                    ReportParameter[] param = new ReportParameter[paramCount];
                    param[0] = new ReportParameter("Region", dsExportReport.Tables[0].Rows[0]["Region"].ToString(), true);
                    param[1] = new ReportParameter("Province", dsExportReport.Tables[0].Rows[0]["Province"].ToString(), true);
                    param[2] = new ReportParameter("District", dsExportReport.Tables[0].Rows[0]["District"].ToString(), true);
                    param[3] = new ReportParameter("LLG", dsExportReport.Tables[0].Rows[0]["LLG"].ToString(), true);
                    param[4] = new ReportParameter("Ward", string.Format("({0}) - {1}", dsExportReport.Tables[0].Rows[0]["WardNo"].ToString(), dsExportReport.Tables[0].Rows[0]["Ward"].ToString()), true);

                    if (geoName == "Ward" && selectedReportWardRollType == 2)
                    {

                        var locations = string.Join(", ",
                                      dsExportReport.Tables[0].AsEnumerable()
                                         .Select(row => row.Field<string>("Location"))
                                         .Where(loc => !string.IsNullOrEmpty(loc))
                                         .Distinct()
                                         .OrderBy(loc => loc));

                        param[5] = new ReportParameter("Location", locations, true);
                    }
                    else
                    {
                        param[5] = new ReportParameter("Location", dsExportReport.Tables[0].Rows[0]["Location"].ToString(), true);
                    }


                    param[6] = new ReportParameter("Electors", dsExportReport.Tables[0].Rows.Count.ToString(), true);
                    param[7] = new ReportParameter("DateTime", DateTime.Now.ToString("dddd, MMMM dd, yyyy"), true);
                    param[8] = new ReportParameter("ReportTitle", ReportTitle, true);
                    param[9] = new ReportParameter("Version", Version, true);
                    param[10] = new ReportParameter("WardNumberWardName", dsExportReport.Tables[0].Rows[0]["WardNumberWardName"].ToString(), true);
                    param[11] = new ReportParameter("IsDeletionReport", Convert.ToString(IsPossibleDeletion), true);
                    param[12] = new ReportParameter("IsLocation", Convert.ToString(IsLocation), true);
                    param[13] = new ReportParameter("IsGenderSplit", Convert.ToString(IsGenderSplit), true);
                    param[14] = new ReportParameter("GenderSplit", GenderSplit, true);
                    param[15] = new ReportParameter("IsAlphaSplit", Convert.ToString(IsAlphaSplit), true);
                    param[16] = new ReportParameter("AlphaSplitStart", AlphaSplitStart, true);
                    param[17] = new ReportParameter("AlphaSplitEnd", AlphaSplitEnd, true);
                    param[18] = new ReportParameter("IsEnvelopLabel", Convert.ToString(true), true);

                    if (report == "Final")
                    {
                        param[19] = new ReportParameter("GeoNameAndYear", "LLG Election 2020 ", true);
                        param[20] = new ReportParameter("FooterDate", DateTime.Now.ToString("dd-MMM-yyyy"), true);
                    }
                    #endregion

                    ReportDataSource dataSource = new ReportDataSource("ds_WardRollBook", dsExportReport.Tables[0].Copy());

                    #region Possible Deletion Report

                    //dtDeletionExportReport.TableName = "tblWardRollDeletion";
                    //dsExportReport.Tables.Add(dtDeletionExportReport.Copy());
                    //ReportDataSource datasourceDeletion = new ReportDataSource("ds_WardRollDeletion", dsExportReport.Tables[1].Copy());

                    // Create Tables
                    DataTable tblWardRollDeletion = new DataTable("tblWardRollDeletion");
                    DataTable tblWardRollDeletionTrnsOutStatus = new DataTable("tblWardRollDeletionTrnsOutStatus");
                    DataTable tblWardRollDeletionTrnsOutForm11 = new DataTable("tblWardRollDeletionTrnsOutForm11");
                    DataTable tblWardRollBookSummaryByLocation = new DataTable("tblWardRollBookSummaryByLocation");


                    tblWardRollBookSummaryByLocation.Columns.Add("Location", typeof(string));
                    tblWardRollBookSummaryByLocation.Columns.Add("MaleCount", typeof(string));
                    tblWardRollBookSummaryByLocation.Columns.Add("FemaleCount", typeof(string));
                    tblWardRollBookSummaryByLocation.Columns.Add("OtherCount", typeof(string));
                    tblWardRollBookSummaryByLocation.Columns.Add("TotalCount", typeof(string));

                    #region Ward Roll Deletion
                    tblWardRollDeletion.Clear();
                    var wardRollDeletion = from rows in dtDeletionExportReport.AsEnumerable()
                                           where (rows.Field<string>("Status") != "TRANSFERRED OUT")
                                           select rows;

                    if (wardRollDeletion != null && wardRollDeletion.Count() > 0)
                    {
                        tblWardRollDeletion.Merge(wardRollDeletion.CopyToDataTable());

                        if (tblWardRollDeletion != null && tblWardRollDeletion.Rows != null && tblWardRollDeletion.Rows.Count > 0)
                        {
                            for (int i = 0; i < tblWardRollDeletion.Rows.Count; i++)
                            {
                                tblWardRollDeletion.Rows[i]["Seq"] = (i + 1);
                            }
                        }
                    }
                    #endregion

                    #region Ward Roll Deletion Transferred Out Status
                    tblWardRollDeletionTrnsOutStatus.Clear();
                    var wardRollDeletionTrnsOutStatus = from rows in dtDeletionExportReport.AsEnumerable()
                                                        where (rows.Field<string>("Status") == "TRANSFERRED OUT")
                                                           && (rows.Field<int>("TransferInLocationId") == 0)
                                                        select rows;
                    if (wardRollDeletionTrnsOutStatus != null && wardRollDeletionTrnsOutStatus.Count() > 0)
                    {
                        tblWardRollDeletionTrnsOutStatus.Merge(wardRollDeletionTrnsOutStatus.CopyToDataTable());

                        if (tblWardRollDeletionTrnsOutStatus != null && tblWardRollDeletionTrnsOutStatus.Rows != null && tblWardRollDeletionTrnsOutStatus.Rows.Count > 0)
                        {
                            for (int i = 0; i < tblWardRollDeletionTrnsOutStatus.Rows.Count; i++)
                            {
                                tblWardRollDeletionTrnsOutStatus.Rows[i]["Seq"] = (i + 1);
                            }
                        }
                    }
                    #endregion

                    #region Ward Roll Deletion Transferred Out Form11
                    tblWardRollDeletionTrnsOutForm11.Clear();
                    var wardRollDeletionTrnsOutForm11 = from rows in dtDeletionExportReport.AsEnumerable()
                                                        where (rows.Field<string>("Status") == "TRANSFERRED OUT")
                                                           && (rows.Field<int>("TransferInLocationId") > 0)
                                                        select rows;
                    if (wardRollDeletionTrnsOutForm11 != null && wardRollDeletionTrnsOutForm11.Count() > 0)
                    {
                        tblWardRollDeletionTrnsOutForm11.Merge(wardRollDeletionTrnsOutForm11.CopyToDataTable());

                        if (tblWardRollDeletionTrnsOutForm11 != null && tblWardRollDeletionTrnsOutForm11.Rows != null && tblWardRollDeletionTrnsOutForm11.Rows.Count > 0)
                        {
                            for (int i = 0; i < tblWardRollDeletionTrnsOutForm11.Rows.Count; i++)
                            {
                                tblWardRollDeletionTrnsOutForm11.Rows[i]["Seq"] = (i + 1);
                            }
                        }
                    }
                    #endregion


                    tblWardRollBookSummaryByLocation.Clear();

                    var result = dsExportReport.Tables[0].AsEnumerable()
                                .Where(r => !string.IsNullOrEmpty(r.Field<string>("Location")))
                                .GroupBy(r => r.Field<string>("Location"))
                                .Select(g => new
                                {
                                    Location = g.Key,
                                    MaleCount = g.Count(r => r.Field<string>("Gen") == "Male"),
                                    FemaleCount = g.Count(r => r.Field<string>("Gen") == "Female"),
                                    OtherCount = g.Count(r => r.Field<string>("Gen") == "Other"),
                                    TotalCount = g.Count()
                                })
                                .OrderBy(x => x.Location)
                                .ToList();

                    foreach (var item in result)
                    {
                        tblWardRollBookSummaryByLocation.Rows.Add(
                            item.Location,
                            item.MaleCount,
                            item.FemaleCount,
                            item.OtherCount,
                            item.TotalCount
                        );
                    }


                    #endregion

                    if (dtWardRollFrontCover == null)
                    {
                        dtWardRollFrontCover = new DataTable("tblWardRollFrontCover");
                    }
                    dtWardRollFrontCover = new DataTable("tblWardRollFrontCover");
                    ReportDataSource datasourceWardRollFrontCover = new ReportDataSource("ds_WardRollFrontCover", dtWardRollFrontCover.Copy());

                    ReportDataSource dataSourceWardRollDeletionReport = new ReportDataSource("ds_WardRollDeletion", tblWardRollDeletion);
                    ReportDataSource datasourceWardRollDeletionTrnsOutStatus = new ReportDataSource("ds_WardRollDeletionTrnsOutStatus", tblWardRollDeletionTrnsOutStatus);
                    ReportDataSource datasourceWardRollDeletionTrnsOutForm11 = new ReportDataSource("ds_WardRollDeletionTrnsOutForm11", tblWardRollDeletionTrnsOutForm11);

                    ReportDataSource datasourceWardRollBookSummaryByLocation = new ReportDataSource("ds_WardRollBookSummaryByLocation", tblWardRollBookSummaryByLocation.Copy());

                    ReportViewer viewer = new ReportViewer();
                    viewer.LocalReport.Refresh();
                    string fileName = "Reports\\" + rdlcReport;
                    string reportPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
                    viewer.LocalReport.ReportPath = reportPath; //This is your rdlc name.
                    viewer.LocalReport.SetParameters(param);

                    viewer.LocalReport.DataSources.Add(dataSource); // Add  datasource here
                    viewer.LocalReport.DataSources.Add(datasourceWardRollBookSummaryByLocation); // Add summary by location datasource here
                    viewer.LocalReport.DataSources.Add(datasourceWardRollFrontCover);
                    viewer.LocalReport.DataSources.Add(dataSourceWardRollDeletionReport); // Add Deletion datasource here
                    viewer.LocalReport.DataSources.Add(datasourceWardRollDeletionTrnsOutStatus); // Add Deletion datasource here
                    viewer.LocalReport.DataSources.Add(datasourceWardRollDeletionTrnsOutForm11); // Add Deletion datasource here

                    bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
                }

            }
            catch (Exception ex)
            {
                errormsg = ex.Message;
            }
            return bytes;
        }

        /// <summary>
        /// Gets the export records list.
        /// </summary>
        private static async Task GetExportRecordsDataForWardPhoto(string selectedDBName, string selectedServerName, string Gender, string GeoName, int GeoId, int alphaSplitOption, string alphaSplitStart, string alphaSplitEnd)
        {
            string errorMsg = string.Empty;
            try
            {
                #region pageList
                PageList pageList = new PageList();

                // Current Database Filed 
                pageList.DatabaseName = selectedDBName;

                //pageList.KeyWord = txtSearch.Text;

                // Filter Filed
                pageList.FiledName = "All";
                pageList.FiledValue = "All";

                // Gender Split
                pageList.Gender = Gender;
                pageList.Disability = "All";
                pageList.Photo = "All";
                pageList.DateRange = "All";
                pageList.DateFrom = null;
                pageList.DateTo = null;
                pageList.ElectorByIDs = "All";
                //pageList.AlphaSplit = "All";
                //pageList.AlphaSplitStart = string.Empty;
                //pageList.AlphaSplitEnd = string.Empty;
                if (alphaSplitOption == 1)
                {
                    pageList.AlphaSplit = "AlphaSplit";
                    pageList.AlphaSplitStart = alphaSplitStart;
                    pageList.AlphaSplitEnd = alphaSplitEnd;
                }
                else
                {
                    pageList.AlphaSplit = "All";
                    pageList.AlphaSplitStart = string.Empty;
                    pageList.AlphaSplitEnd = string.Empty;
                }

                pageList.FName = "";
                pageList.LName = "";

                // TreeView Filed
                pageList.GeoName = GeoName;
                pageList.GeoId = GeoId;

                // Login User
                pageList.LoggedUserId = superAdminId;

                // Page No
                int PageNo = 1;

                // Page Size
                pageList.PageSize = 1000;
                #endregion

                lstWardRoll = new List<WardRollBookReortModel>();

                bool DataFound = false;

                do
                {
                    pageList.PageNo = PageNo;
                    //Response<WardRollBookReortModel> response = await GenericHttpClient.PostRequest<WardRollBookReortModel>("File/GetExportRecordsList", pageList);
                    HttpResponseMessage responseNew = null;
                    Response<WardRollBookReortModel> response = null;
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(GetServerBaseUrl(selectedServerName));
                        client.DefaultRequestHeaders.Accept.Clear();
                        var dataContent = new StringContent(JsonConvert.SerializeObject(pageList), Encoding.UTF8, "application/json");
                        responseNew = await client.PostAsync("File/GetExportRecordsList", dataContent);
                        //responseNew = client.PostAsync("File/GetExportRecordsList", dataContent).Result;

                        if (responseNew.IsSuccessStatusCode)
                        {
                            var returnData = await responseNew.Content.ReadAsStringAsync();
                            //var returnData = responseNew.Content.ReadAsStringAsync().Result;
                            response = JsonConvert.DeserializeObject<Response<WardRollBookReortModel>>(returnData);
                        }
                    }
                    if (response != null && response.Status && response.Data.ListItems.ToList() != null && response.Data.ListItems.ToList().Count > 0)
                    {
                        lstWardRoll.AddRange(response.Data.ListItems.ToList());
                        DataFound = true;
                    }
                    else
                    {
                        DataFound = false;
                    }

                    PageNo++;

                } while (DataFound);

                if (lstWardRoll != null && lstWardRoll.Count > 0)
                {
                    dtExportReport = new DataTable();
                    dtExportReport = ToDataTable(lstWardRoll);
                }

                lstWardRoll.Clear();
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
            }
        }

        /// <summary>
        /// Voters the identifier card export report source.
        /// </summary>
        /// <returns></returns>
        private static byte[] VoterIdCardExportReportSource()
        {
            byte[] bytes = null;
            try
            {
                if (dsVoterCard.Tables[0].Rows != null && dsVoterCard.Tables[0].Rows.Count > 0)
                {
                    Warning[] warnings;
                    string[] streamIds;
                    string mimeType = string.Empty;
                    string encoding = string.Empty;
                    string extension = string.Empty;

                    dsVoterCard.Tables[0].TableName = "tblVoterIdCard";
                    ReportDataSource dataSource = new ReportDataSource("ds_VoterIdCard", dsVoterCard.Tables[0].Copy());
                    ReportViewer viewer = new ReportViewer();
                    viewer.LocalReport.Refresh();

                    string fileName = "Reports\\VoterIdCard.rdlc";
                    string reportPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
                    viewer.LocalReport.ReportPath = reportPath;
                    viewer.LocalReport.DataSources.Add(dataSource);
                    viewer.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SetVoterIdCardSubDataSource);
                    bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
                }
            }
            catch (Exception)
            {
                bytes = null;
            }
            return bytes;
        }

        /// <summary>
        /// Sets the voter identifier card sub data source.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="SubreportProcessingEventArgs"/> instance containing the event data.</param>
        private static void SetVoterIdCardSubDataSource(object sender, SubreportProcessingEventArgs e)
        {
            int Seq = Convert.ToInt32(e.Parameters[0].Values[0]);

            if (Seq > 0)
            {
                DataTable tblSubRCardFiltered = dsVoterCard.Tables[1].AsEnumerable().Where(row => row.Field<int>("Seq") == Seq).CopyToDataTable();
                tblSubRCardFiltered.TableName = "tblSubVoterIdCard";
                ReportDataSource datasource = new ReportDataSource("ds_SubVoterIdCard", tblSubRCardFiltered);
                e.DataSources.Add(datasource);
            }
        }

        /// <summary>
        /// Gets the export records list.
        /// </summary>
        private static byte[] WardPhotoRollBookExportReport(string selectedDBName, string Gender, bool IsEnvelopLabel, bool IsPossibleDeletion, int alphaSplitOption, string alphaSplitStart, string alphaSplitEnd)
        {
            byte[] bytest = null;

            if (dtExportReport != null && dtExportReport.Rows.Count > 0)
            {
                bytest = WardPhotoRollBookExportReportSource(selectedDBName, Gender, IsEnvelopLabel, IsPossibleDeletion, alphaSplitOption, alphaSplitStart, alphaSplitEnd);

            }

            return bytest;
        }

        /// <summary>
        /// Wards the roll book export report source.
        /// </summary>
        /// <param name="report">The report.</param>
        /// <param name="dt">The dt.</param>
        /// <returns></returns>
        private static byte[] WardPhotoRollBookExportReportSource(string report, string Gender, bool IsEnvelopLabel, bool IsPossibleDeletion, int alphaSplitOption, string alphaSplitStart, string alphaSplitEnd)
        {
            byte[] bytes = null;
            string errorMsg = string.Empty;
            try
            {
                dsExportReport = new DataSet();
                DataTable dtSubWardPhotoRollBook = new DataTable();

                dtExportReport.TableName = "tblWardPhotoRollBook";
                if (!dsExportReport.Tables.Contains(dtExportReport.TableName))
                {
                    dsExportReport.Tables.Add(dtExportReport);
                }

                if (dsExportReport.Tables[0].Rows != null && dsExportReport.Tables[0].Rows.Count > 0)
                {
                    #region dt Sub Ward Photo Roll Book

                    dtSubWardPhotoRollBook = dsExportReport.Tables[0].Copy();

                    dtSubWardPhotoRollBook.TableName = "tblSubWardPhotoRollBook";

                    string[] ColumnsToBeDeleted = { "Gender",
                    "DOB",
                    "WardNumberWardName",
                    "Count",
                    "BDate",
                    "ElectorProfileImage",
                    "isPWD",
                    "SetImageUrl",
                    "Christianity",
                    //"Religion",
                    "Region",
                    "Province",
                    "District",
                    "Electors",
                    "DateTime",
                    "DuplicateId",
                    "WardNo",
                    "ProvinceCode"};

                    foreach (string ColName in ColumnsToBeDeleted)
                    {
                        if (dtSubWardPhotoRollBook.Columns.Contains(ColName))
                            dtSubWardPhotoRollBook.Columns.Remove(ColName);
                    }

                    dsExportReport.Tables.Add(dtSubWardPhotoRollBook);

                    #endregion

                    string ReportTitle = string.Empty, Version = string.Empty, rdlcReport = string.Empty, GenderSplit = string.Empty, AlphaSplitStart = string.Empty, AlphaSplitEnd = string.Empty;
                    bool IsLocation = false, IsGenderSplit = false, IsAlphaSplit = false;

                    GenderSplit = Gender;

                    if (Gender != "All")
                    {
                        IsGenderSplit = true;
                        if (Gender == "M")
                        {
                            GenderSplit = "Male";
                        }
                        else if (Gender == "F")
                        {
                            GenderSplit = "Female";
                        }
                    }
                    else
                    {
                        IsGenderSplit = false;
                    }

                    // isAlphaSplit
                    if (alphaSplitOption == 1)
                    {
                        IsAlphaSplit = true;
                        AlphaSplitStart = alphaSplitStart;
                        AlphaSplitEnd = alphaSplitEnd;
                    }

                    if (report == "Staging")
                    {
                        ReportTitle = $"WORKING ROLL {DateTime.Now.ToString("yyyy")}";
                        Version = "V1";

                        rdlcReport = "WardPhotoRollBook.rdlc";
                    }
                    else if (report == "Preliminary")
                    {
                        ReportTitle = $"PRELIMINARY ELECTORAL ROLL {DateTime.Now.ToString("yyyy")}";
                        Version = "version 1";

                        rdlcReport = "WardPhotoRollBookPreliminary.rdlc";
                    }
                    else if (report == "Final")
                    {
                        ReportTitle = $"ELECTORAL ROLL {DateTime.Now.ToString("yyyy")}";
                        //ReportTitle = "ELECTORAL ROLL 2022";
                        Version = "version 1";

                        rdlcReport = "WardPhotoRollBookFinal.rdlc";
                    }

                    Warning[] warnings;
                    string[] streamIds;
                    string mimeType = string.Empty;
                    string encoding = string.Empty;
                    string extension = string.Empty;

                    int paramCount = 19;
                    if (report == "Final")
                    {
                        paramCount = 21;
                    }

                    //This is optional if you have parameter then you can add parameters as much as you want
                    ReportParameter[] param = new ReportParameter[paramCount];
                    param[0] = new ReportParameter("Region", dsExportReport.Tables[0].Rows[0]["Region"].ToString(), true);
                    param[1] = new ReportParameter("Province", dsExportReport.Tables[0].Rows[0]["Province"].ToString(), true);
                    param[2] = new ReportParameter("District", dsExportReport.Tables[0].Rows[0]["District"].ToString(), true);
                    param[3] = new ReportParameter("LLG", dsExportReport.Tables[0].Rows[0]["LLG"].ToString(), true);
                    param[4] = new ReportParameter("Ward", string.Format("({0}) - {1}", dsExportReport.Tables[0].Rows[0]["WardNo"].ToString(), dsExportReport.Tables[0].Rows[0]["Ward"].ToString()), true);
                    param[5] = new ReportParameter("Location", dsExportReport.Tables[0].Rows[0]["Location"].ToString(), true);
                    param[6] = new ReportParameter("Electors", dsExportReport.Tables[0].Rows.Count.ToString(), true);
                    param[7] = new ReportParameter("DateTime", DateTime.Now.ToString("dddd, MMMM dd, yyyy"), true);
                    param[8] = new ReportParameter("ReportTitle", ReportTitle, true);
                    param[9] = new ReportParameter("Version", Version, true);
                    param[10] = new ReportParameter("WardNumberWardName", dsExportReport.Tables[0].Rows[0]["WardNumberWardName"].ToString(), true);
                    param[11] = new ReportParameter("IsDeletionReport", Convert.ToString(IsPossibleDeletion), true);
                    param[12] = new ReportParameter("IsLocation", Convert.ToString(IsLocation), true);
                    param[13] = new ReportParameter("IsGenderSplit", Convert.ToString(IsGenderSplit), true);
                    param[14] = new ReportParameter("GenderSplit", GenderSplit, true);
                    param[15] = new ReportParameter("IsAlphaSplit", Convert.ToString(IsAlphaSplit), true);
                    param[16] = new ReportParameter("AlphaSplitStart", AlphaSplitStart, true);
                    param[17] = new ReportParameter("AlphaSplitEnd", AlphaSplitEnd, true);
                    param[18] = new ReportParameter("IsEnvelopLabel", Convert.ToString(true), true);
                    if (report == "Final")
                    {
                        param[19] = new ReportParameter("GeoNameAndYear", "LLG Election 2020 ", true);
                        param[20] = new ReportParameter("FooterDate", DateTime.Now.ToString("dd-MMM-yyyy"), true);
                    }

                    ReportDataSource dataSource = new ReportDataSource("ds_WardPhotoRollBook", dsExportReport.Tables[0].Copy());

                    #region Possible Deletion Report

                    //dtDeletionExportReport.TableName = "tblWardRollDeletion";
                    //dsExportReport.Tables.Add(dtDeletionExportReport.Copy());
                    //ReportDataSource datasourceDeletion = new ReportDataSource("ds_WardRollDeletion", dsExportReport.Tables[1].Copy());

                    // Create Tables
                    DataTable tblWardRollDeletion = new DataTable("tblWardRollDeletion");
                    DataTable tblWardRollDeletionTrnsOutStatus = new DataTable("tblWardRollDeletionTrnsOutStatus");
                    DataTable tblWardRollDeletionTrnsOutForm11 = new DataTable("tblWardRollDeletionTrnsOutForm11");


                    #region Ward Roll Deletion
                    tblWardRollDeletion.Clear();
                    var wardRollDeletion = from rows in dtDeletionExportReport.AsEnumerable()
                                           where (rows.Field<string>("Status") != "TRANSFERRED OUT")
                                           select rows;

                    if (wardRollDeletion != null && wardRollDeletion.Count() > 0)
                    {
                        tblWardRollDeletion.Merge(wardRollDeletion.CopyToDataTable());

                        if (tblWardRollDeletion != null && tblWardRollDeletion.Rows != null && tblWardRollDeletion.Rows.Count > 0)
                        {
                            for (int i = 0; i < tblWardRollDeletion.Rows.Count; i++)
                            {
                                tblWardRollDeletion.Rows[i]["Seq"] = (i + 1);
                            }
                        }
                    }
                    #endregion

                    #region Ward Roll Deletion Transferred Out Status
                    tblWardRollDeletionTrnsOutStatus.Clear();
                    var wardRollDeletionTrnsOutStatus = from rows in dtDeletionExportReport.AsEnumerable()
                                                        where (rows.Field<string>("Status") == "TRANSFERRED OUT")
                                                           && (rows.Field<int>("TransferInLocationId") == 0)
                                                        select rows;
                    if (wardRollDeletionTrnsOutStatus != null && wardRollDeletionTrnsOutStatus.Count() > 0)
                    {
                        tblWardRollDeletionTrnsOutStatus.Merge(wardRollDeletionTrnsOutStatus.CopyToDataTable());

                        if (tblWardRollDeletionTrnsOutStatus != null && tblWardRollDeletionTrnsOutStatus.Rows != null && tblWardRollDeletionTrnsOutStatus.Rows.Count > 0)
                        {
                            for (int i = 0; i < tblWardRollDeletionTrnsOutStatus.Rows.Count; i++)
                            {
                                tblWardRollDeletionTrnsOutStatus.Rows[i]["Seq"] = (i + 1);
                            }
                        }
                    }
                    #endregion

                    #region Ward Roll Deletion Transferred Out Form11
                    tblWardRollDeletionTrnsOutForm11.Clear();
                    var wardRollDeletionTrnsOutForm11 = from rows in dtDeletionExportReport.AsEnumerable()
                                                        where (rows.Field<string>("Status") == "TRANSFERRED OUT")
                                                           && (rows.Field<int>("TransferInLocationId") > 0)
                                                        select rows;
                    if (wardRollDeletionTrnsOutForm11 != null && wardRollDeletionTrnsOutForm11.Count() > 0)
                    {
                        tblWardRollDeletionTrnsOutForm11.Merge(wardRollDeletionTrnsOutForm11.CopyToDataTable());

                        if (tblWardRollDeletionTrnsOutForm11 != null && tblWardRollDeletionTrnsOutForm11.Rows != null && tblWardRollDeletionTrnsOutForm11.Rows.Count > 0)
                        {
                            for (int i = 0; i < tblWardRollDeletionTrnsOutForm11.Rows.Count; i++)
                            {
                                tblWardRollDeletionTrnsOutForm11.Rows[i]["Seq"] = (i + 1);
                            }
                        }
                    }
                    #endregion

                    #endregion


                    //dtDeletionExportReport.TableName = "tblWardRollDeletion";
                    //dsExportReport.Tables.Add(dtDeletionExportReport.Copy());
                    //ReportDataSource datasourceDeletion = new ReportDataSource("ds_WardRollDeletion", dsExportReport.Tables[2].Copy());

                    if (dtWardRollFrontCover == null)
                    {
                        dtWardRollFrontCover = new DataTable("tblWardRollFrontCover");
                    }
                    dtWardRollFrontCover = new DataTable("tblWardRollFrontCover");
                    ReportDataSource datasourceWardRollFrontCover = new ReportDataSource("ds_WardRollFrontCover", dtWardRollFrontCover.Copy());

                    ReportDataSource dataSourceWardRollDeletionReport = new ReportDataSource("ds_WardRollDeletion", tblWardRollDeletion);
                    ReportDataSource datasourceWardRollDeletionTrnsOutStatus = new ReportDataSource("ds_WardRollDeletionTrnsOutStatus", tblWardRollDeletionTrnsOutStatus);
                    ReportDataSource datasourceWardRollDeletionTrnsOutForm11 = new ReportDataSource("ds_WardRollDeletionTrnsOutForm11", tblWardRollDeletionTrnsOutForm11);

                    ReportViewer viewer = new ReportViewer();
                    viewer.LocalReport.Refresh();
                    string fileName = "Reports\\" + rdlcReport;
                    string reportPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
                    viewer.LocalReport.ReportPath = reportPath; //This is your rdlc name.
                    viewer.LocalReport.EnableExternalImages = true;
                    viewer.LocalReport.SetParameters(param);
                    viewer.LocalReport.DataSources.Add(dataSource); // Add  datasource here 
                    viewer.LocalReport.DataSources.Add(datasourceWardRollFrontCover); // Add Deletion datasource here
                    viewer.LocalReport.DataSources.Add(dataSourceWardRollDeletionReport); // Add Deletion datasource here
                    viewer.LocalReport.DataSources.Add(datasourceWardRollDeletionTrnsOutStatus); // Add Deletion datasource here
                    viewer.LocalReport.DataSources.Add(datasourceWardRollDeletionTrnsOutForm11); // Add Deletion datasource here
                    viewer.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SetWardPhotoRollBookSubDataSource);
                    bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
                }

                dtSubWardPhotoRollBook.Clear();
            }
            catch (Exception ex)
            {
                bytes = null;
                errorMsg = ex.Message;
            }
            return bytes;
        }

        /// <summary>
        /// Sets the ward photo roll book sub data source.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="SubreportProcessingEventArgs"/> instance containing the event data.</param>
        private static void SetWardPhotoRollBookSubDataSource(object sender, SubreportProcessingEventArgs e)
        {
            int Seq = Convert.ToInt32(e.Parameters[0].Values[0]);

            if (Seq > 0)
            {
                DataTable tblSubRCardFiltered = dsExportReport.Tables[1].AsEnumerable().Where(row => row.Field<int>("Seq") == Seq).CopyToDataTable();
                tblSubRCardFiltered.TableName = "tblSubWardPhotoRollBook";
                ReportDataSource datasource = new ReportDataSource("ds_SubWardPhotoRollBook", tblSubRCardFiltered);
                e.DataSources.Add(datasource);
            }
        }

        /// <summary>
        /// Converts to datatable.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        /// <summary>
        /// Gets the server base URL.
        /// </summary>
        /// <param name="serverName">Name of the server.</param>
        /// <returns></returns>
        public static string GetServerBaseUrl(string serverName)
        {
            if (!string.IsNullOrEmpty(serverName))
            {
                return ConfigurationManager.AppSettings[serverName];
            }
            else
            {
                return ConfigurationManager.AppSettings["ProductionApiURL"];
            }
        }

        /// <summary>
        /// Voters the roll envelope export report source.
        /// </summary>
        /// <returns></returns>
        private static byte[] VoterRollEnvelopeExportReportSource(EnvelopeExportModel model, string selectedDBName)
        {
            byte[] bytes = null;

            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;

            string LocationStr = string.Join(",", model.LocationNameList.Select(x => x.LocationName));

            //This is optional if you have parameter then you can add parameters as much as you want
            ReportParameter[] param = new ReportParameter[12];
            param[0] = new ReportParameter("Region", model.RegionName, true);
            param[1] = new ReportParameter("Province", model.ProvinceName, true);
            param[2] = new ReportParameter("District", model.DistrictName, true);
            param[3] = new ReportParameter("LLG", model.LLGName, true);
            param[4] = new ReportParameter("Ward", model.WardName, true);
            param[5] = new ReportParameter("Location", LocationStr, true);
            param[6] = new ReportParameter("WardNo", Convert.ToString(model.WardNo).PadLeft(2, '0'), true);

            if (selectedDBName == "Staging")
            {
                param[7] = new ReportParameter("RollDescription", "This envelope contains the Working Roll marked up from the Roll update for village / Location (s) :", true);
                param[8] = new ReportParameter("Point1", "1 - Place the updated Working Roll in this envelope and send ARO", true);
                param[9] = new ReportParameter("Point2", "2 - ARO to quality check and sign then send to RO", true);
                param[10] = new ReportParameter("Point3", "3 - RO to quality check and sign, then send to Data Coordinator.", true);
            }
            else if (selectedDBName == "Preliminary")
            {
                param[7] = new ReportParameter("RollDescription", "This envelope contains the Preliminary Roll marked up from the Roll update for village / Location (s) :", true);
                param[8] = new ReportParameter("Point1", "1 - Place the updated Preliminary Roll in this envelope and send ARO", true);
                param[9] = new ReportParameter("Point2", "2 - ARO to quality check and sign then send to RO", true);
                param[10] = new ReportParameter("Point3", "3 - RO to quality check and sign, then send to Data Coordinator.", true);
            }
            else if (selectedDBName == "Final")
            {
                param[7] = new ReportParameter("RollDescription", "This envelope contains the Final Roll marked up from the Roll update for village / Location (s) :", true);
                param[8] = new ReportParameter("Point1", "1 - Place the updated Final Roll in this envelope and send ARO", true);
                param[9] = new ReportParameter("Point2", "2 - ARO to quality check and sign then send to RO", true);
                param[10] = new ReportParameter("Point3", "3 - RO to quality check and sign, then send to Data Coordinator.", true);
            }

            param[11] = new ReportParameter("ReportTitle", $"PNGEC - ELECTORAL ROLL UPDATE {DateTime.Now.ToString("yyyy")}", true);

            ReportViewer viewer = new ReportViewer();
            viewer.LocalReport.Refresh();

            viewer.LocalReport.ReportPath = "Reports\\WardRollEnvelope.rdlc";
            viewer.LocalReport.SetParameters(param);
            bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

            return bytes;
        }


        /// <summary>
        /// add AddActivity Log For Report
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="logSource"></param>
        /// <param name="Event"></param>
        /// <param name="LogDescription"></param>
        /// <returns></returns>
        public static async Task<int> AddActivityLogForReport(int userId, string Event, string LogDescription)
        {
            try
            {
                ActivityLogModel objModel = new ActivityLogModel
                {
                    UserID = userId,
                    LogSource = 1,
                    Event = Event,
                    LogDescription = LogDescription
                };

                var activResponse = await GenericHttpClient.PostRequest<ActivityLogModel>("ActivityLog/AddActivityLog", objModel);

                if (activResponse != null && activResponse.Status)
                {
                    return 1;
                }
            }
            catch (Exception) { return 0; }

            return 0;

        }
    }
}
