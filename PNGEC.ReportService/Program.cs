using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Reporting.WebForms;
using Newtonsoft.Json;
using PNGEC.API.Domain.District;
using PNGEC.API.Domain.EnrolmentForm;
using PNGEC.API.Domain.LLG;
using PNGEC.API.Domain.Location;
using PNGEC.API.Domain.Province;
using PNGEC.API.Domain.Region;
using PNGEC.API.Domain.Report;
using PNGEC.API.Domain.User;
using PNGEC.API.Domain.Ward;
using PNGEC.API.Helper;
using PNGEC.ReportService.Data;
using PNGEC.ReportService.Model;
using ZXing;
using static PNGEC.API.Helper.CommonEnum;
using UserModel = PNGEC.ReportService.Model.UserModel;

namespace PNGEC.ReportService
{
    internal class Program
    {
        #region variable declaration

        /// <summary>
        /// The object region
        /// </summary>
        private static List<RegionModel> objRegion = null;

        /// <summary>
        /// The object province
        /// </summary>
        private static List<ProvinceModel> objProvince = null;

        /// <summary>
        /// The object district
        /// </summary>
        private static List<DistrictModel> objDistrict = null;

        /// <summary>
        /// The object LLG
        /// </summary>
        private static List<LLGModel> objLLG = null;

        /// <summary>
        /// The object ward
        /// </summary>
        private static List<WardModel> objWard = null;

        /// <summary>
        /// The object location
        /// </summary>
        private static List<LocationModel> objLocation = null;

        /// <summary>
        /// The envelope export
        /// </summary>
        private static EnvelopeExportModel envelopeExport = null;

        /// <summary>
        /// The selected report value
        /// </summary>
        private static int selectedReportVal = 0;

        /// <summary>
        /// the selected report ward roll type
        /// </summary>
        private static int selectedReportWardRollType = 0;

        /// <summary>
        /// The selected database value
        /// </summary>
        private static int selectedDBVal = 0;

        /// <summary>
        /// The selected server value
        /// </summary>
        private static int selectedServerVal = 0;

        /// <summary>
        /// The selected province value
        /// </summary>
        private static int selectedProvinceVal = 0;

        /// <summary>
        /// The selected district value
        /// </summary>
        private static int selectedDistrictVal = 0;

        /// <summary>
        /// The district all no
        /// </summary>
        private static int DistrictAllNo = 0;

        /// <summary>
        /// The selected LLG value
        /// </summary>
        private static int selectedLLGVal = 0;

        /// <summary>
        /// The LLG all no
        /// </summary>
        private static int LLGAllNo = 0;

        /// <summary>
        /// The selected ward value
        /// </summary>
        private static int selectedWardVal = 0;

        /// <summary>
        /// The ward all no
        /// </summary>
        private static int WardAllNo = 0;

        /// <summary>
        /// The selected possible deletion report value
        /// </summary>
        private static int selectedPossibleDeletionReportVal = 0;

        /// <summary>
        /// The selected envelop value
        /// </summary>
        private static int selectedEnvelopVal = 0;

        /// <summary>
        /// The selected gender value
        /// </summary>
        private static int selectedGenderVal = 0;

        /// <summary>
        /// The selected alpha split option
        /// </summary>
        private static int selectedAlphaSplitOption = 0;

        /// <summary>
        /// The selected start alphabet
        /// </summary>
        private static char selectedStartAlphabet;

        /// <summary>
        /// The selected end alphabet
        /// </summary>
        private static char selectedEndAlphabet;

        /// <summary>
        /// The selected percentage value
        /// </summary>
        private static int selectedPercentageVal = 0;

        /// <summary>
        /// The selected number of forms value
        /// </summary>
        private static int selectedNumberOfFormsVal = 0;

        /// <summary>
        /// The selected report generate again
        /// </summary>
        private static int selectedReportGenerateAgain = 0;

        /// <summary>
        /// The download report path
        /// </summary>
        private static string DownloadReportPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["DownloadReportFolderName"]);

        /// <summary>
        /// The distinct ward ids
        /// </summary>
        private static List<int> DistinctWardIds = null;

        /// <summary>
        /// The distinct location ids
        /// </summary>
        private static List<int> DistinctLocationIds = null;

        /// <summary>
        /// The save folder name
        /// </summary>
        private static string saveFolderName = string.Empty;

        /// <summary>
        /// Gets or sets the region.
        /// </summary>
        /// <value>
        /// The region.
        /// </value>
        private static API.Domain.Region.RegionModel Region { get; set; }

        /// <summary>
        /// Gets or sets the province.
        /// </summary>
        /// <value>
        /// The province.
        /// </value>
        private static API.Domain.Province.ProvinceModel Province { get; set; }

        /// <summary>
        /// Gets or sets the district.
        /// </summary>
        /// <value>
        /// The district.
        /// </value>
        private static API.Domain.District.DistrictModel District { get; set; }

        /// <summary>
        /// Gets or sets the LLG.
        /// </summary>
        /// <value>
        /// The LLG.
        /// </value>
        private static API.Domain.LLG.LLGModel LLG { get; set; }

        /// <summary>
        /// Gets or sets the ward.
        /// </summary>
        /// <value>
        /// The ward.
        /// </value>
        private static API.Domain.Ward.WardModel Ward { get; set; }

        /// <summary>
        /// The batch name
        /// </summary>
        private static string BatchName = string.Empty;

        /// <summary>
        /// The number of batch
        /// </summary>
        private static string NumberOfBatch = string.Empty;

        /// <summary>
        /// The comment
        /// </summary>
        private static string Comment = string.Empty;

        /// <summary>
        /// The ds form11
        /// </summary>
        private static DataSet dsForm11 = null;

        /// <summary>
        /// The dt form11, dt SubForm11
        /// </summary>
        private static DataTable dtForm11 = null, dtSubForm11 = null;

        /// <summary>
        /// The database name
        /// </summary>
        private static string DatabaseName = string.Empty;

        /// <summary>
        /// The server name
        /// </summary>
        //private static string ServerName = string.Empty;

        /// <summary>
        /// The server name key
        /// </summary>
        private static string ServerNameKey = string.Empty;

        /// <summary>
        /// The report path
        /// </summary>
        private static string reportPath = string.Empty;

        private static string ServerLogin = string.Empty;

        private static Version version;

        private static Response<UserModel> loginResponse = null;

        private static string ActivityLogMessage = string.Empty;

        #endregion

        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        private static async Task Main(string[] args)
        {
            try
            {
                if (!Directory.Exists(DownloadReportPath))
                {
                    Directory.CreateDirectory(DownloadReportPath);
                }

                await MainCall();
                //Console.ReadKey();
            }
            catch (Exception ex)
            {
                ErrorLog.Write("Program", "Main", ex);
                Console.WriteLine("Something went wrong, Please try again. \n");
            }
        }

        /// <summary>
        /// Mains the call.
        /// </summary>
        private static async Task MainCall()
        {
            version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            Console.WriteLine($"Version : {API.Helper.Globals.DesktopVersionYear}.{version.Major}.{version.Minor}");
            //Console.WriteLine("Load data continue...");
            // TLS Security Protocol
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;





            //if (await CallApis())
            {
                //DistinctWardIds = new List<int>();
                saveFolderName = string.Empty;

                try
                {
                    if (await GetSelectedServer())
                    {
                        //if (GetSelectedDB())
                        {
                            if (GetSelectedReport())
                            {                                
                                if ((selectedReportVal == 1) && selectedDBVal == 1)
                                {
                                    GetSelectedWardRollType();
                                }

                                if (objProvince != null)
                                {
                                    if (GetSelectedProvince())
                                    {
                                        if (GetSelectedDistrict())
                                        {
                                            if (selectedDistrictVal != DistrictAllNo)//specific district selected
                                            {
                                                if (GetSelectedLLG())
                                                {
                                                    if (selectedLLGVal != LLGAllNo)//specific llg selected
                                                    {
                                                        if (selectedReportVal == 4)
                                                        {
                                                            GetSelectedPercentage();
                                                        }
                                                        else
                                                        {
                                                            if (selectedReportVal != 3)
                                                            {
                                                                bool isPossibleDeletionSelect = true;
                                                                if (selectedDBVal != 3) //if DB not final then display possible deletion report
                                                                {
                                                                    isPossibleDeletionSelect = GetSelectedPossibleDeletionReport();
                                                                }

                                                                if (isPossibleDeletionSelect)
                                                                {
                                                                    if (GetSelectedEnvelop())
                                                                    {
                                                                        GetSelectedGender();
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                GetSelectedGender();
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (selectedReportVal == 4)
                                                        {
                                                            GetSelectedPercentage();
                                                        }
                                                        else
                                                        {
                                                            if (selectedReportVal != 3)
                                                            {
                                                                bool isPossibleDeletionSelect = true;
                                                                if (selectedDBVal != 3) //if DB not final then display possible deletion report
                                                                {
                                                                    isPossibleDeletionSelect = GetSelectedPossibleDeletionReport();
                                                                }

                                                                if (isPossibleDeletionSelect)
                                                                {
                                                                    if (GetSelectedEnvelop())
                                                                    {
                                                                        GetSelectedGender();
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                GetSelectedGender();
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if (selectedReportVal == 4)
                                                    {
                                                        GetSelectedPercentage();
                                                    }
                                                    else
                                                    {
                                                        if (selectedReportVal != 3)
                                                        {
                                                            bool isPossibleDeletionSelect = true;
                                                            if (selectedDBVal != 3) //if DB not final then display possible deletion report
                                                            {
                                                                isPossibleDeletionSelect = GetSelectedPossibleDeletionReport();
                                                            }

                                                            if (isPossibleDeletionSelect)
                                                            {
                                                                if (GetSelectedEnvelop())
                                                                {
                                                                    GetSelectedGender();
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            GetSelectedGender();
                                                        }
                                                    }
                                                }
                                            }
                                            else//All District Selected
                                            {
                                                if (selectedReportVal == 4)
                                                {
                                                    GetSelectedPercentage();
                                                }
                                                else
                                                {
                                                    if (selectedReportVal != 3)
                                                    {
                                                        bool isPossibleDeletionSelect = true;
                                                        if (selectedDBVal != 3) //if DB not final then display possible deletion report
                                                        {
                                                            isPossibleDeletionSelect = GetSelectedPossibleDeletionReport();
                                                        }

                                                        if (isPossibleDeletionSelect)
                                                        {
                                                            if (GetSelectedEnvelop())
                                                            {
                                                                GetSelectedGender();
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        GetSelectedGender();
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }


                            }
                        }
                        //else
                        //{
                        //    Console.ReadKey();
                        //    return;
                        //}
                    }

                    if (!await CheckVersion())
                    {
                        Console.WriteLine("Update to latest software to generate reports.\n");
                        await GenerateReportAgain();
                    }

                    if (selectedReportVal == 4)
                    {
                        //Console.WriteLine("\tCompleted " + locationString);
                        Console.WriteLine("The report generates is processing,please wait......\n");

                        #region Get Wards Number Of Form11 API Call

                        if (selectedDBVal == 1)
                        {
                            DatabaseName = "Staging";
                        }
                        else if (selectedDBVal == 2)
                        {
                            DatabaseName = "Preliminary";
                        }
                        else if (selectedDBVal == 3)
                        {
                            DatabaseName = "Final";
                        }

                        int provinceIdForForm11 = 0;
                        int districtIdForForm11 = 0;
                        int llgIdForForm11 = 0;
                        // is province 
                        if (selectedDistrictVal == DistrictAllNo)
                        {
                            provinceIdForForm11 = selectedProvinceVal;
                            districtIdForForm11 = 0;
                            llgIdForForm11 = 0;
                        }
                        else if (selectedLLGVal == LLGAllNo)
                        {
                            provinceIdForForm11 = 0;
                            districtIdForForm11 = selectedDistrictVal;
                            llgIdForForm11 = 0;
                        }
                        else
                        {
                            provinceIdForForm11 = 0;
                            districtIdForForm11 = 0;
                            llgIdForForm11 = selectedLLGVal;
                        }

                        GetWardsNumberOfForm11Request getWardsNumberOfForm11Request = new GetWardsNumberOfForm11Request
                        {
                            ProvinceId = provinceIdForForm11,
                            DistrictId = districtIdForForm11,
                            LLGId = llgIdForForm11,
                            Percentage = selectedPercentageVal,
                            DatabaseName = DatabaseName,
                            MajorVersionNumber = version.Major,
                            MinorVersionNumber = version.Minor
                        };

                        Response responseWardsNumberOfForm = await DataService.GetWardsNumberOfForm11(getWardsNumberOfForm11Request, ServerNameKey);

                        #endregion

                        //Generate Report
                        string folderName = string.Empty, saveFolderPath = string.Empty;
                        string ProvinceFolderName = string.Empty, DistrictFolderName = string.Empty, LlgFolderName = string.Empty;

                        if (responseWardsNumberOfForm != null && responseWardsNumberOfForm.Status == true)
                        {
                            List<GetWardsNumberOfForm11Response> lstWardsNumberOfForm11 = JsonConvert.DeserializeObject<List<GetWardsNumberOfForm11Response>>(Convert.ToString(responseWardsNumberOfForm.Data));

                            if (lstWardsNumberOfForm11 != null && lstWardsNumberOfForm11.Count > 0)
                            {
                                string serverName = string.Empty;
                                if (selectedServerVal == 1)
                                {
                                    serverName = "Production";
                                }
                                else if (selectedServerVal == 2)
                                {
                                    serverName = "Training";
                                }
                                else if (selectedServerVal == 3)
                                {
                                    serverName = "Dev";
                                }

                                foreach (GetWardsNumberOfForm11Response item in lstWardsNumberOfForm11)
                                {
                                    try
                                    {
                                        string fileName = string.Empty;
                                        if (selectedNumberOfFormsVal > 0)
                                        {
                                            item.NumberOfForms = selectedNumberOfFormsVal;
                                        }

                                        if (item.NumberOfForms > 0)
                                        {
                                            Ward = objWard.Where(c => c.WardId == item.WardId).FirstOrDefault();
                                            LLG = objLLG.Where(c => c.LLGId == item.LLGId).FirstOrDefault();
                                            District = objDistrict.Where(c => c.DistrictId == item.DistrictId).FirstOrDefault();
                                            Province = objProvince.Where(c => c.ProvinceId == item.ProvinceId).FirstOrDefault();
                                            Region = objRegion.Where(c => c.RegionId == item.RegionId).FirstOrDefault();

                                            #region Main Section
                                            string locationString = string.Format("{0}/{1}/{2}/{3}-{4}", Province.ProvinceName.Replace("/", "_"), District.DistrictName.Replace("/", "_"), LLG.LLGName.Replace("/", "_"), Ward.WardNumber, Ward.WardName.Replace("/", "_"));

                                            #region create directory folder
                                            if (string.IsNullOrWhiteSpace(ProvinceFolderName) || ProvinceFolderName != Province.ProvinceName)
                                            {
                                                ProvinceFolderName = Province.ProvinceName;
                                                folderName = ConfigurationManager.AppSettings["DownloadReportFolderName"] + "\\" + ProvinceFolderName;
                                                saveFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, folderName);

                                                if (!Directory.Exists(saveFolderPath))
                                                {
                                                    Directory.CreateDirectory(saveFolderPath);
                                                }
                                            }

                                            if (string.IsNullOrWhiteSpace(DistrictFolderName) || DistrictFolderName != District.DistrictName)
                                            {
                                                DistrictFolderName = District.DistrictName;
                                                folderName = ConfigurationManager.AppSettings["DownloadReportFolderName"] + "\\" + ProvinceFolderName + "\\" + DistrictFolderName;
                                                saveFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, folderName);

                                                if (!Directory.Exists(saveFolderPath))
                                                {
                                                    Directory.CreateDirectory(saveFolderPath);
                                                }
                                            }

                                            if (string.IsNullOrWhiteSpace(LlgFolderName) || LlgFolderName != LLG.LLGName)
                                            {
                                                LlgFolderName = LLG.LLGName;
                                                folderName = ConfigurationManager.AppSettings["DownloadReportFolderName"] + "\\" + ProvinceFolderName + "\\" + DistrictFolderName + "\\" + LlgFolderName;
                                                saveFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, folderName);

                                                if (!Directory.Exists(saveFolderPath))
                                                {
                                                    Directory.CreateDirectory(saveFolderPath);
                                                }
                                            }
                                            #endregion

                                            #region Generate Report

                                            if (selectedNumberOfFormsVal > 0)
                                            {
                                                fileName = $"{serverName}-{Province.ProvinceName.Replace("/", "_")}_{District.DistrictName.Replace("/", "_")}_{LLG.LLGName.Replace("/", "_")}_{Ward.WardName.Replace("/", "_")}_{Ward.WardId}_NumberOfForms{selectedNumberOfFormsVal}_Form11";
                                                Comment = $"Number of Form 11 are {selectedNumberOfFormsVal}";
                                            }
                                            else
                                            {
                                                fileName = $"{serverName}-{Province.ProvinceName.Replace("/", "_")}_{District.DistrictName.Replace("/", "_")}_{LLG.LLGName.Replace("/", "_")}_{Ward.WardName.Replace("/", "_")}_{Ward.WardId}_Percentage{selectedPercentageVal}_Form11";
                                                Comment = $"{selectedPercentageVal}% of Form 11";
                                            }

                                            BatchName = $"{fileName}_{DateTime.Now.ToString("ddMMyyyyHHmmss")}";
                                            NumberOfBatch = $"{item.NumberOfForms}";


                                            MultipartFormDataContent formContent = new MultipartFormDataContent()
                                        {
                                            {new StringContent("1"),"UserId"},
                                            {new StringContent(Convert.ToInt32(Province.ProvinceId).ToString()),"ProvinceId"},
                                            {new StringContent(Convert.ToInt32(Region.RegionId).ToString()),"RegionId"},
                                            {new StringContent(Convert.ToInt32(District.DistrictId).ToString()),"DistrictId"},
                                            {new StringContent(Convert.ToInt32(LLG.LLGId).ToString()),"LLGId"},
                                            {new StringContent(Convert.ToInt32(Ward.WardId).ToString()),"WardId"},
                                            {new StringContent(BatchName),"BatchName"},
                                            {new StringContent(NumberOfBatch),"NumberOfBatch"},
                                            {new StringContent(Comment),"Comment"},
                                            {new StringContent("1"),"ActionBy"}
                                        };

                                            Response res = null;
                                            using (var client = new HttpClient())
                                            {
                                                client.BaseAddress = new Uri(ReportService.Report.ReportService.GetServerBaseUrl(ServerNameKey));
                                                using (HttpResponseMessage respo = client.PostAsync("EnrolmentForm/AddEnrolmentForm", formContent).Result)
                                                {
                                                    if (respo.StatusCode == HttpStatusCode.OK)
                                                    {
                                                        string responseBody = await respo.Content.ReadAsStringAsync();
                                                        res = JsonConvert.DeserializeObject<Response>(responseBody);
                                                    }
                                                }
                                            }

                                            if (res != null)
                                            {
                                                if (res.Status)
                                                {
                                                    var enrolData = JsonConvert.DeserializeObject<EnrolmentFormModel>(res.Data.ToString());

                                                    byte[] bytesEnrolmentFormsExport = await EnrolmentFormsExportReportSource(Convert.ToInt32(enrolData.StartRange),
                                                            Convert.ToInt32(enrolData.EndRange),
                                                            Convert.ToInt32(Region.RegionId),
                                                            Convert.ToInt32(Province.ProvinceId),
                                                            Convert.ToInt32(District.DistrictId),
                                                            Convert.ToInt32(LLG.LLGId),
                                                            Convert.ToInt32(Ward.WardId),
                                                            Convert.ToString(BatchName));

                                                    if (bytesEnrolmentFormsExport != null)
                                                    {
                                                        reportPath = saveFolderPath + "\\" + $"{fileName}.pdf";
                                                        using (FileStream stream = new FileStream(reportPath, FileMode.Create))
                                                        {
                                                            stream.Write(bytesEnrolmentFormsExport, 0, bytesEnrolmentFormsExport.Length);
                                                        }
                                                        DeleteQRCodeImagesDirectory();

                                                        string strLogMessage = GetSelectedReportName(selectedReportVal) + " Reports tCompleted - " + locationString.Replace("/", "-");
                                                        await Report.ReportService.AddActivityLogForReport(loginResponse.Data.ListItems.FirstOrDefault().UserId, "Form11", strLogMessage);


                                                        Console.WriteLine("\tCompleted " + locationString + "\n");

                                                    }
                                                    else
                                                    {
                                                        string strLogMessage = GetSelectedReportName(selectedReportVal) + " Reports Failed - " + locationString.Replace("/", "-");
                                                        await Report.ReportService.AddActivityLogForReport(loginResponse.Data.ListItems.FirstOrDefault().UserId, "Form11", strLogMessage);
                                                        // some went wrong
                                                        DeleteQRCodeImagesDirectory();
                                                        Console.WriteLine("\tFailure " + locationString + "\n");
                                                    }

                                                }
                                            }

                                            #endregion
                                            #endregion
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        ErrorLog.Write("Program", "Form11 Generation", ex);
                                    }
                                }

                                Console.WriteLine("Batch Job Completed.Your files can be found in the '" + saveFolderPath + "' Directory.\n");

                                string strLogMsg = GetSelectedReportName(selectedReportVal) + " Reports Successfully Generated.";
                                await Report.ReportService.AddActivityLogForReport(loginResponse.Data.ListItems.FirstOrDefault().UserId, "Form11", strLogMsg);

                            }
                            else
                            {
                                string strLogMsg = GetSelectedReportName(selectedReportVal) + " Reports Batch Job Completed Record Not Found.";
                                await Report.ReportService.AddActivityLogForReport(loginResponse.Data.ListItems.FirstOrDefault().UserId, "Form11", strLogMsg);

                                Console.WriteLine("Batch Job Completed. Record Not Found.\n");
                            }
                        }
                        else
                        {
                            string strLogMsg = GetSelectedReportName(selectedReportVal) + " Reports Batch Job Failed.";
                            await Report.ReportService.AddActivityLogForReport(loginResponse.Data.ListItems.FirstOrDefault().UserId, "Form11", strLogMsg);
                            Console.WriteLine("Something went wrong, Please try again. \n");
                        }

                        Console.ReadLine();
                    }
                    else if (selectedReportVal == 5) //For location Roll
                    {
                        DistinctLocationIds = new List<int>();
                        if (selectedProvinceVal != 0)
                        {
                            if (selectedDistrictVal != DistrictAllNo)
                            {
                                if (selectedLLGVal != LLGAllNo)
                                {
                                    if (selectedWardVal != WardAllNo)
                                    {
                                        var GeoWardData = objWard.Where(c => c.WardId == selectedWardVal).FirstOrDefault();
                                        if (GeoWardData != null)
                                        {
                                            DistinctLocationIds = objLocation.Where(c => c.WardId == selectedWardVal).OrderBy(o => o.ProvinceName).ThenBy(o => o.DistrictName).ThenBy(o => o.LLGName).ThenBy(o => o.WardNumber).ThenBy(o => o.LocationName).Select(s => s.LocationId).Distinct().ToList();
                                            //saveFolderName = GeoLLgData.LLGName;
                                        }
                                    }
                                    else
                                    {
                                        var GeoLLgData = objLLG.Where(c => c.LLGId == selectedLLGVal).FirstOrDefault();
                                        if (GeoLLgData != null)
                                        {
                                            DistinctLocationIds = objLocation.Where(c => c.LLGId == selectedLLGVal).OrderBy(o => o.ProvinceName).ThenBy(o => o.DistrictName).ThenBy(o => o.LLGName).ThenBy(o => o.WardNumber).ThenBy(o => o.LocationName).Select(s => s.LocationId).Distinct().ToList();
                                            //saveFolderName = GeoLLgData.LLGName;
                                        }
                                    }
                                }
                                else
                                {
                                    var GeoDistrictData = objDistrict.Where(c => c.DistrictId == selectedDistrictVal).FirstOrDefault();
                                    if (GeoDistrictData != null)
                                    {
                                        DistinctLocationIds = objLocation.Where(c => c.DistrictId == selectedDistrictVal).OrderBy(o => o.ProvinceName).ThenBy(o => o.DistrictName).ThenBy(o => o.LLGName).ThenBy(o => o.WardNumber).ThenBy(o => o.LocationName).Select(s => s.LocationId).Distinct().ToList();
                                        //saveFolderName = GeoDistrictData.DistrictName;
                                    }
                                }
                            }
                            else
                            {
                                var GeoProvinceData = objProvince.Where(c => c.ProvinceId == selectedProvinceVal).FirstOrDefault();
                                if (GeoProvinceData != null)
                                {
                                    DistinctLocationIds = objLocation.Where(c => c.ProvinceId == selectedProvinceVal).OrderBy(o => o.ProvinceName).ThenBy(o => o.DistrictName).ThenBy(o => o.LLGName).ThenBy(o => o.WardNumber).ThenBy(o => o.LocationName).Select(s => s.LocationId).Distinct().ToList();
                                    //saveFolderName = GeoProvinceData.ProvinceName;
                                }
                            }
                        }

                        //Generate Report
                        if (DistinctLocationIds != null && DistinctLocationIds.Count > 0)
                        {
                            Console.WriteLine("The report generates is processing,please wait......\n");

                            string folderName = string.Empty, saveFolderPath = string.Empty;
                            string ProvinceFolderName = string.Empty, DistrictFolderName = string.Empty, LlgFolderName = string.Empty;

                            foreach (int value in DistinctLocationIds)
                            {
                                LocationModel locationData = objLocation.Where(c => c.LocationId == value).FirstOrDefault();
                                string llgName = Convert.ToString(objLLG.Where(c => c.LLGId == locationData.LLGId).Select(s => s.LLGName).FirstOrDefault()).Replace("/", "-").Replace("\\", "-");
                                string districtName = Convert.ToString(objDistrict.Where(c => c.DistrictId == locationData.DistrictId).Select(s => s.DistrictName).FirstOrDefault()).Replace("/", "-").Replace("\\", "-");
                                string provinceName = Convert.ToString(objProvince.Where(c => c.ProvinceId == locationData.ProvinceId).Select(s => s.ProvinceName).FirstOrDefault()).Replace("/", "-").Replace("\\", "-");
                                string regionName = objRegion.Where(c => c.RegionId == locationData.RegionId).Select(s => s.RegionName).FirstOrDefault();
                                string locationString = string.Format("{0}/{1}/{2}/{3}-{4}/{5}", provinceName, districtName, llgName, locationData.WardNumber, locationData.WardName, locationData.LocationName.Trim());

                                if (selectedEnvelopVal == 1)
                                {
                                    envelopeExport = new EnvelopeExportModel();
                                    envelopeExport.RegionName = regionName;
                                    envelopeExport.ProvinceName = provinceName;
                                    envelopeExport.DistrictName = districtName;
                                    envelopeExport.LLGName = llgName;
                                    envelopeExport.WardName = locationData.WardName;
                                    envelopeExport.WardNo = locationData.WardNumber;
                                }

                                #region create directory folder
                                if (string.IsNullOrWhiteSpace(ProvinceFolderName) || ProvinceFolderName != provinceName)
                                {
                                    ProvinceFolderName = provinceName;
                                    folderName = ConfigurationManager.AppSettings["DownloadReportFolderName"] + "\\" + ProvinceFolderName;
                                    saveFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, folderName);

                                    if (!Directory.Exists(saveFolderPath))
                                    {
                                        Directory.CreateDirectory(saveFolderPath);
                                    }
                                }

                                if (string.IsNullOrWhiteSpace(DistrictFolderName) || DistrictFolderName != districtName)
                                {
                                    DistrictFolderName = districtName;
                                    folderName = ConfigurationManager.AppSettings["DownloadReportFolderName"] + "\\" + ProvinceFolderName + "\\" + DistrictFolderName;
                                    saveFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, folderName);

                                    if (!Directory.Exists(saveFolderPath))
                                    {
                                        Directory.CreateDirectory(saveFolderPath);
                                    }
                                }

                                if (string.IsNullOrWhiteSpace(LlgFolderName) || LlgFolderName != llgName)
                                {
                                    LlgFolderName = llgName;
                                    folderName = ConfigurationManager.AppSettings["DownloadReportFolderName"] + "\\" + ProvinceFolderName + "\\" + DistrictFolderName + "\\" + LlgFolderName;
                                    saveFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, folderName);

                                    if (!Directory.Exists(saveFolderPath))
                                    {
                                        Directory.CreateDirectory(saveFolderPath);
                                    }
                                }
                                #endregion

                                int response = await GenerateReportAsync(locationString, "Location", value, saveFolderPath);
                                if (response == -1)
                                {
                                    string strLogMsg = GetSelectedReportName(selectedReportVal) + " Reports Failed - " + locationString.Replace("/", "-");
                                    await Report.ReportService.AddActivityLogForReport(loginResponse.Data.ListItems.FirstOrDefault().UserId, "location Roll", strLogMsg);
                                    Console.WriteLine("\tFailure " + locationString + "\n");
                                }
                                else if (response == -2)
                                {
                                    string strLogMsg = GetSelectedReportName(selectedReportVal) + " Reports No Data Found - " + locationString.Replace("/", "-");
                                    await Report.ReportService.AddActivityLogForReport(loginResponse.Data.ListItems.FirstOrDefault().UserId, "location Roll", strLogMsg);

                                    Console.WriteLine("\tNo Data Found " + locationString + "\n");
                                }
                                else
                                {
                                    string strLogMsg = GetSelectedReportName(selectedReportVal) + " Reports Successfully Completed - " + locationString.Replace("/", "-");
                                    await Report.ReportService.AddActivityLogForReport(loginResponse.Data.ListItems.FirstOrDefault().UserId, "location Roll", strLogMsg);

                                    Console.WriteLine("\tCompleted " + locationString + "\n");
                                }
                                envelopeExport = null;
                            }

                            Console.WriteLine("Batch Job Completed.Your files can be found in the '" + saveFolderPath + "' Directory.\n");
                        }
                        else
                        {
                            string strLogMsg = GetSelectedReportName(selectedReportVal) + " Reports Batch Job Completed - ";
                            await Report.ReportService.AddActivityLogForReport(loginResponse.Data.ListItems.FirstOrDefault().UserId, "location Roll", strLogMsg);

                            Console.WriteLine("Batch Job Completed. Record Not Found.\n");
                        }

                    }
                    else
                    {
                        DistinctWardIds = new List<int>();

                        if (selectedProvinceVal != 0)
                        {
                            if (selectedDistrictVal != DistrictAllNo)
                            {
                                if (selectedLLGVal != LLGAllNo)
                                {
                                    var GeoLLgData = objLLG.Where(c => c.LLGId == selectedLLGVal).FirstOrDefault();
                                    if (GeoLLgData != null)
                                    {
                                        DistinctWardIds = objWard.Where(c => c.LLGId == selectedLLGVal).OrderBy(o => o.ProvinceName).ThenBy(o => o.DistrictName).ThenBy(o => o.LLGName).ThenBy(o => o.WardNumber).Select(s => s.WardId).Distinct().ToList();
                                        //saveFolderName = GeoLLgData.LLGName;
                                    }
                                }
                                else
                                {
                                    var GeoDistrictData = objDistrict.Where(c => c.DistrictId == selectedDistrictVal).FirstOrDefault();
                                    if (GeoDistrictData != null)
                                    {
                                        DistinctWardIds = objWard.Where(c => c.DistrictId == selectedDistrictVal).OrderBy(o => o.ProvinceName).ThenBy(o => o.DistrictName).ThenBy(o => o.LLGName).ThenBy(o => o.WardNumber).Select(s => s.WardId).Distinct().ToList();
                                        //saveFolderName = GeoDistrictData.DistrictName;
                                    }
                                }
                            }
                            else
                            {
                                var GeoProvinceData = objProvince.Where(c => c.ProvinceId == selectedProvinceVal).FirstOrDefault();
                                if (GeoProvinceData != null)
                                {
                                    DistinctWardIds = objWard.Where(c => c.ProvinceId == selectedProvinceVal).OrderBy(o => o.ProvinceName).ThenBy(o => o.DistrictName).ThenBy(o => o.LLGName).ThenBy(o => o.WardNumber).Select(s => s.WardId).Distinct().ToList();
                                    //saveFolderName = GeoProvinceData.ProvinceName;
                                }
                            }
                        }

                        //Console.WriteLine("\tCompleted " + locationString);
                        Console.WriteLine("The report generates is processing,please wait......\n");

                        //Generate Report
                        if (DistinctWardIds != null && DistinctWardIds.Count > 0)
                        {
                            string folderName = string.Empty, saveFolderPath = string.Empty;
                            string ProvinceFolderName = string.Empty, DistrictFolderName = string.Empty, LlgFolderName = string.Empty;
                            string locationString;
                            foreach (int value in DistinctWardIds)
                            {
                                WardModel WardData = objWard.Where(c => c.WardId == value).FirstOrDefault();
                                string llgName = Convert.ToString(objLLG.Where(c => c.LLGId == WardData.LLGId).Select(s => s.LLGName).FirstOrDefault()).Replace("/", "-").Replace("\\", "-");
                                string districtName = Convert.ToString(objDistrict.Where(c => c.DistrictId == WardData.DistrictId).Select(s => s.DistrictName).FirstOrDefault()).Replace("/", "-").Replace("\\", "-");
                                string provinceName = Convert.ToString(objProvince.Where(c => c.ProvinceId == WardData.ProvinceId).Select(s => s.ProvinceName).FirstOrDefault()).Replace("/", "-").Replace("\\", "-");
                                string regionName = objRegion.Where(c => c.RegionId == WardData.RegionId).Select(s => s.RegionName).FirstOrDefault();
                                locationString = string.Format("{0}/{1}/{2}/{3}-{4}", provinceName, districtName, llgName, WardData.WardNumber, WardData.WardName);

                                if (selectedEnvelopVal == 1)
                                {
                                    envelopeExport = new EnvelopeExportModel();
                                    envelopeExport.RegionName = regionName;
                                    envelopeExport.ProvinceName = provinceName;
                                    envelopeExport.DistrictName = districtName;
                                    envelopeExport.LLGName = llgName;
                                    envelopeExport.WardName = WardData.WardName;
                                    envelopeExport.WardNo = WardData.WardNumber;
                                }

                                #region create directory folder
                                if (string.IsNullOrWhiteSpace(ProvinceFolderName) || ProvinceFolderName != provinceName)
                                {
                                    ProvinceFolderName = provinceName;
                                    folderName = ConfigurationManager.AppSettings["DownloadReportFolderName"] + "\\" + ProvinceFolderName;
                                    saveFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, folderName);

                                    if (!Directory.Exists(saveFolderPath))
                                    {
                                        Directory.CreateDirectory(saveFolderPath);
                                    }
                                }

                                if (string.IsNullOrWhiteSpace(DistrictFolderName) || DistrictFolderName != districtName)
                                {
                                    DistrictFolderName = districtName;
                                    folderName = ConfigurationManager.AppSettings["DownloadReportFolderName"] + "\\" + ProvinceFolderName + "\\" + DistrictFolderName;
                                    saveFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, folderName);

                                    if (!Directory.Exists(saveFolderPath))
                                    {
                                        Directory.CreateDirectory(saveFolderPath);
                                    }
                                }

                                if (string.IsNullOrWhiteSpace(LlgFolderName) || LlgFolderName != llgName)
                                {
                                    LlgFolderName = llgName;
                                    folderName = ConfigurationManager.AppSettings["DownloadReportFolderName"] + "\\" + ProvinceFolderName + "\\" + DistrictFolderName + "\\" + LlgFolderName;
                                    saveFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, folderName);

                                    if (!Directory.Exists(saveFolderPath))
                                    {
                                        Directory.CreateDirectory(saveFolderPath);
                                    }
                                }
                                #endregion

                                int response = await GenerateReportAsync(locationString, "Ward", value, saveFolderPath);
                                if (response == -1)
                                {
                                    string strLogMsg = GetSelectedReportName(selectedReportVal) + " Reports Job Failed - " + locationString.Replace("/", "-");
                                    await Report.ReportService.AddActivityLogForReport(loginResponse.Data.ListItems.FirstOrDefault().UserId, "Ward", strLogMsg);

                                    Console.WriteLine("\tFailure " + locationString + "\n");
                                }
                                else if (response == -2)
                                {
                                    string strLogMsg = GetSelectedReportName(selectedReportVal) + " Reports Job No Data Found - " + locationString.Replace("/", "-");
                                    await Report.ReportService.AddActivityLogForReport(loginResponse.Data.ListItems.FirstOrDefault().UserId, "Ward", strLogMsg);

                                    Console.WriteLine("\tNo Data Found " + locationString + "\n");
                                }
                                else
                                {
                                    string strLogMsg = GetSelectedReportName(selectedReportVal) + " Reports Job Completed - " + locationString.Replace("/", "-");
                                    await Report.ReportService.AddActivityLogForReport(loginResponse.Data.ListItems.FirstOrDefault().UserId, "Ward", strLogMsg);

                                    Console.WriteLine("\tCompleted " + locationString + "\n");
                                }
                                envelopeExport = null;
                            }
                            string strLogMessage = GetSelectedReportName(selectedReportVal) + " Reports Batch Job Completed";
                            await Report.ReportService.AddActivityLogForReport(loginResponse.Data.ListItems.FirstOrDefault().UserId, "Ward", strLogMessage);

                            Console.WriteLine("Batch Job Completed.Your files can be found in the '" + saveFolderPath + "' Directory.\n");
                        }
                        else
                        {
                            string strLogMsg = GetSelectedReportName(selectedReportVal) + "Reports Batch Job Completed Record Not Found";
                            await Report.ReportService.AddActivityLogForReport(loginResponse.Data.ListItems.FirstOrDefault().UserId, "Ward", strLogMsg);

                            Console.WriteLine("Batch Job Completed. Record Not Found.\n");
                        }
                    }


                    //Generate Report Again
                    await GenerateReportAgain();
                }
                catch (Exception ex)
                {
                    ErrorLog.Write("Program", "MainCall", ex);
                    Console.WriteLine("Something went wrong, Please try again. \n");
                }
                finally
                {
                    saveFolderName = string.Empty;
                    DistinctWardIds?.Clear();
                    DistinctLocationIds?.Clear();
                }
            }
            //else
            //{
            //    Console.WriteLine("Something went wrong, Please try again. \n");
            //    //await GenerateReportAgain();
            //}
            Console.ReadKey();
        }


        private static async Task<bool> CheckVersion()
        {
            #region Get check latest version

            if (selectedDBVal == 1)
            {
                DatabaseName = "Staging";
            }
            else if (selectedDBVal == 2)
            {
                DatabaseName = "Preliminary";
            }
            else if (selectedDBVal == 3)
            {
                DatabaseName = "Final";
            }

            int provinceIdForForm11 = 0;
            int districtIdForForm11 = 0;
            int llgIdForForm11 = 0;
            // is province 
            if (selectedDistrictVal == DistrictAllNo)
            {
                provinceIdForForm11 = selectedProvinceVal;
                districtIdForForm11 = 0;
                llgIdForForm11 = 0;
            }
            else if (selectedLLGVal == LLGAllNo)
            {
                provinceIdForForm11 = 0;
                districtIdForForm11 = selectedDistrictVal;
                llgIdForForm11 = 0;
            }
            else
            {
                provinceIdForForm11 = 0;
                districtIdForForm11 = 0;
                llgIdForForm11 = selectedLLGVal;
            }

            GetWardsNumberOfForm11Request getWardsNumberOfForm11Request = new GetWardsNumberOfForm11Request
            {
                ProvinceId = provinceIdForForm11,
                DistrictId = districtIdForForm11,
                LLGId = llgIdForForm11,
                Percentage = selectedPercentageVal,
                DatabaseName = DatabaseName,
                MajorVersionNumber = version.Major,
                MinorVersionNumber = version.Minor
            };

            Response responseWardsNumberOfForm = await DataService.GetWardsNumberOfForm11(getWardsNumberOfForm11Request, ServerNameKey);

            return responseWardsNumberOfForm.Status;

            #endregion
        }

        /// <summary>
        /// Calls the apis.
        /// </summary>
        /// <returns></returns>
        private static async Task<bool> CallApis()
        {
            try
            {
                objRegion = new List<RegionModel>();
                objRegion = await DataService.GetRegionListAsync(ServerNameKey);

                objProvince = new List<ProvinceModel>();
                objProvince = await DataService.GetProvinceListAsync(ServerNameKey);

                objDistrict = new List<DistrictModel>();
                objDistrict = await DataService.GetDistrictListAsync(ServerNameKey);

                objLLG = new List<LLGModel>();
                objLLG = await DataService.GetLLGListAsync(ServerNameKey);

                objWard = new List<WardModel>();
                objWard = await DataService.GetWardListAsync(ServerNameKey);

                // if (selectedReportVal == 5)
                {
                    objLocation = new List<LocationModel>();
                    objLocation = await DataService.GetLocationListAsync(ServerNameKey);
                }

                return true;
            }
            catch (Exception ex)
            {
                ErrorLog.Write("Program", "CallApis", ex);
                return false;
            }
        }

        /// <summary>
        /// Gets the selected report.
        /// </summary>
        /// <returns></returns>
        private static bool GetSelectedReport()
        {
            bool isReportSelected = false;
            Console.WriteLine("What Report would you like to Print?\r");
            Console.WriteLine("\t1 Ward Roll");
            Console.WriteLine("\t2 Ward Photo Roll");
            Console.WriteLine("\t3 Ward Voter Id Card");
            Console.WriteLine("\t4 Form 11 Generation");
            Console.WriteLine("\t5 Location Roll");
            Console.WriteLine("\t6 Ward Clan Roll");

            Console.WriteLine("Please enter select report");
            isReportSelected = int.TryParse(Console.ReadLine(), out selectedReportVal);

            while (!isReportSelected || (selectedReportVal < 1) || (selectedReportVal > 6))
            {
                Console.WriteLine("Please enter valid number");
                isReportSelected = int.TryParse(Console.ReadLine(), out selectedReportVal);
            }
            return isReportSelected;
        }


        private static bool GetSelectedWardRollType()
        {
            bool isWardRollTypeSelected = false;
            Console.WriteLine("Select Report Type\r");
            Console.WriteLine("\t1 Ward roll Portrait");
            Console.WriteLine("\t2 Ward roll Landscap");
            Console.WriteLine("Please enter select option");
            isWardRollTypeSelected = int.TryParse(Console.ReadLine(), out selectedReportWardRollType);
            while (!isWardRollTypeSelected || (selectedReportWardRollType < 1) || (selectedReportWardRollType > 2))
            {
                Console.WriteLine("Please enter valid number");
                isWardRollTypeSelected = int.TryParse(Console.ReadLine(), out selectedReportWardRollType);
            }
            return isWardRollTypeSelected;
        }

        private static string GetSelectedReportName(int selectedReport)
        {
            return selectedReport == 1 ? "Ward Roll" : selectedReport == 2 ? "Ward Photo Roll" : selectedReport == 3 ? "Ward Voter Id Card" : selectedReport == 4 ? "Form 11 Generation" : selectedReport == 5 ? "Location Roll" : "Ward Clan Roll";
        }

        /// <summary>
        /// Gets the selected server.
        /// </summary>
        /// <returns></returns>
        private async static Task<bool> GetSelectedServer()
        {
            bool isServerSelected = false;
            bool isDevelopmentShow = false;
            int lastOption = 2;
            Console.WriteLine("What Server would you like to use?\r");
            Console.WriteLine("\t1 Production");
            Console.WriteLine("\t2 Training");

            //isDevelopmentShow = ConfigurationManager.AppSettings["IsGeoFromProduction"] == "false";
            isDevelopmentShow = false;
            if (isDevelopmentShow)
            {
                Console.WriteLine("\t3 Development");
                lastOption = 3;
            }


            Console.WriteLine("Please enter select Server");
            isServerSelected = int.TryParse(Console.ReadLine(), out selectedServerVal);

            //while (!isServerSelected || (isDevelopmentShow && (selectedServerVal < 1) || (selectedServerVal > 3)) || (!isDevelopmentShow && (selectedServerVal < 1) || (selectedServerVal > 2)))
            while (!isServerSelected || (selectedServerVal < 1) || (selectedServerVal > lastOption))
            {
                Console.WriteLine("Please enter valid number");
                isServerSelected = int.TryParse(Console.ReadLine(), out selectedServerVal);

                return isServerSelected;
            }

            if (selectedServerVal == 1)
            {
                ServerNameKey = "ProductionApiURL";
                ServerLogin = "Production";
                Globals.serverURL = ConfigurationManager.AppSettings["ProductionApiURL"].ToString();
            }
            else if (selectedServerVal == 2)
            {
                ServerNameKey = "TrainingApiURL";
                ServerLogin = "Training";
                Globals.serverURL = ConfigurationManager.AppSettings["TrainingApiURL"].ToString();
            }
            else if (selectedServerVal == 3)
            {
                ServerNameKey = "DevelopmentApiURL";
                ServerLogin = "Development";
                Globals.serverURL = ConfigurationManager.AppSettings["DevelopmentApiURL"].ToString();
            }

            GetSelectedDB();

            if (loginResponse == null || !loginResponse.Status)
            {
                CommonRequest objUserLogin = new CommonRequest();
                try
                {
                    Console.WriteLine("Please enter User Name.");
                    objUserLogin.username = Console.ReadLine();
                    Console.WriteLine("Please enter Password");
                    ConsoleKeyInfo keyInfo;

                    do
                    {
                        keyInfo = Console.ReadKey(true);
                        // Skip if Backspace or Enter is Pressed
                        if (keyInfo.Key != ConsoleKey.Backspace && keyInfo.Key != ConsoleKey.Enter)
                        {
                            objUserLogin.password += keyInfo.KeyChar;
                            Console.Write("*");
                        }
                        else
                        {
                            if (keyInfo.Key == ConsoleKey.Backspace && objUserLogin.password.Length > 0)
                            {
                                // Remove last charcter if Backspace is Pressed
                                objUserLogin.password = objUserLogin.password.Substring(0, (objUserLogin.password.Length - 1));
                                Console.Write("\b \b");
                            }
                        }
                    }
                    // Stops Getting Password Once Enter is Pressed
                    while (keyInfo.Key != ConsoleKey.Enter);
                    objUserLogin.database = selectedDBVal == 1 ? "Staging" : selectedDBVal == 2 ? "Preliminary" : "Final";
                    objUserLogin.server = ServerLogin;


                    Console.WriteLine("\n");
                    objUserLogin.username = objUserLogin.username.Trim();
                    objUserLogin.password = objUserLogin.password.Trim();
                    loginResponse = await GenericHttpClient.PostRequest<UserModel>("User/UserLogin", objUserLogin);

                }
                catch (Exception ex)
                {
                    await Report.ReportService.AddActivityLogForReport(0, "Login", "Login Failed " + objUserLogin.username);
                    Console.WriteLine("Something went wrong, Please try again. \n");
                    await GenerateReportAgain();
                }


                if (loginResponse.Data != null && loginResponse.Data.ListItems != null && loginResponse.Data.ListItems.Count<UserModel>() > 0
                    && (loginResponse.Data.ListItems.FirstOrDefault().Role == 0 || loginResponse.Data.ListItems.FirstOrDefault().Role == 6))
                {

                    await Report.ReportService.AddActivityLogForReport(loginResponse.Data.ListItems.FirstOrDefault().UserId, "Login", "Login Success " + objUserLogin.username);

                    Console.WriteLine("\nLoad data continue... \n");
                    if (!await CallApis())
                    {
                        isServerSelected = false;
                        Console.WriteLine("Something went wrong, Please try again. \n");
                        await GenerateReportAgain();
                    }

                    return isServerSelected;
                }
                else if (loginResponse.Data != null && (loginResponse.Data.ListItems.FirstOrDefault().Role != 0 || loginResponse.Data.ListItems.FirstOrDefault().Role != 6))
                {
                    await Report.ReportService.AddActivityLogForReport(loginResponse.Data.ListItems.FirstOrDefault().UserId, "Login", "Login Success " + objUserLogin.username);
                    loginResponse = null;
                    Console.WriteLine("Please make sure you have the correct access rights. \n");
                    await GenerateReportAgain();
                }
                else
                {
                    await Report.ReportService.AddActivityLogForReport(0, "Login", "Login Failed " + objUserLogin.username + " " + loginResponse.Message);
                    Console.WriteLine(loginResponse.Message + "\n");
                    await GenerateReportAgain();
                }
                return false;
            }
            else
            {
                Console.WriteLine("\nLoad data continue... \n");
                if (!await CallApis())
                {
                    isServerSelected = false;
                    Console.WriteLine("Something went wrong, Please try again. \n");
                    await GenerateReportAgain();
                }

                return isServerSelected;
            }
        }

        /// <summary>
        /// Gets the selected database.
        /// </summary>
        /// <returns></returns>
        private static bool GetSelectedDB()
        {
            bool isDBSelected = false;
            Console.WriteLine("What Database would you like to use?\r");
            Console.WriteLine("\t1 Staging");
            Console.WriteLine("\t2 Preliminary");
            Console.WriteLine("\t3 Final");

            Console.WriteLine("Please enter select DB");
            isDBSelected = int.TryParse(Console.ReadLine(), out selectedDBVal);

            while (!isDBSelected || (selectedDBVal < 1) || (selectedDBVal > 3))
            {
                Console.WriteLine("Please enter valid number");
                isDBSelected = int.TryParse(Console.ReadLine(), out selectedDBVal);
            }
            return isDBSelected;
        }

        /// <summary>
        /// Gets the selected province.
        /// </summary>
        /// <returns></returns>
        private static bool GetSelectedProvince()
        {
            bool isProvinceSelected = false;
            Console.WriteLine("What Province?\r");

            for (int i = 0; i < objProvince.Count; i++)
            {
                Console.WriteLine("\t" + objProvince[i].ProvinceId + " " + objProvince[i].ProvinceName);
            }
            Console.WriteLine("Please enter select province");
            string provinceIdList = string.Join(",", objProvince.Select(c => c.ProvinceId.ToString()));
            isProvinceSelected = int.TryParse(Console.ReadLine(), out selectedProvinceVal);

            while (!isProvinceSelected || Array.IndexOf(provinceIdList.Split(','), selectedProvinceVal.ToString()) == -1)
            {
                Console.WriteLine("Please enter valid number");
                isProvinceSelected = int.TryParse(Console.ReadLine(), out selectedProvinceVal);
            }
            return isProvinceSelected;
        }

        /// <summary>
        /// Gets the selected district.
        /// </summary>
        /// <returns></returns>
        private static bool GetSelectedDistrict()
        {

            bool isDistrictSelected = false;

            var provinceWiseDistrictList = objDistrict.Where(c => c.ProvinceId == selectedProvinceVal).ToList();
            if (provinceWiseDistrictList != null && provinceWiseDistrictList.Count > 0)
            {
                Console.WriteLine("What District?\r");
                for (int i = 0; i < provinceWiseDistrictList.Count; i++)
                {
                    Console.WriteLine("\t" + provinceWiseDistrictList[i].DistrictId + " " + provinceWiseDistrictList[i].DistrictName);
                }

                DistrictAllNo = objDistrict.OrderByDescending(c => c.DistrictId).FirstOrDefault().DistrictId + 1;
                Console.WriteLine("\t" + DistrictAllNo + " All");

                Console.WriteLine("Please enter select district");
                string districtIdList = string.Join(",", provinceWiseDistrictList.Select(c => c.DistrictId.ToString())) + "," + DistrictAllNo;
                isDistrictSelected = int.TryParse(Console.ReadLine(), out selectedDistrictVal);

                while (!isDistrictSelected || Array.IndexOf(districtIdList.Split(','), selectedDistrictVal.ToString()) == -1)
                {
                    Console.WriteLine("Please enter valid number");
                    isDistrictSelected = int.TryParse(Console.ReadLine(), out selectedDistrictVal);
                }
            }
            return isDistrictSelected;
        }

        /// <summary>
        /// Gets the selected LLG.
        /// </summary>
        /// <returns></returns>
        private static bool GetSelectedLLG()
        {
            bool isLLGSelected = false;

            var districtWiseLLGList = objLLG.Where(c => c.DistrictId == selectedDistrictVal).ToList();
            if (districtWiseLLGList != null && districtWiseLLGList.Count > 0)
            {
                Console.WriteLine("What LLG?\r");
                for (int i = 0; i < districtWiseLLGList.Count; i++)
                {
                    Console.WriteLine("\t" + districtWiseLLGList[i].LLGId + " " + districtWiseLLGList[i].LLGName);
                }

                LLGAllNo = objLLG.OrderByDescending(c => c.LLGId).FirstOrDefault().LLGId + 1;
                Console.WriteLine("\t" + LLGAllNo + " All");

                Console.WriteLine("Please enter select llg");
                string llgIdList = string.Join(",", districtWiseLLGList.Select(c => c.LLGId.ToString())) + "," + LLGAllNo;
                isLLGSelected = int.TryParse(Console.ReadLine(), out selectedLLGVal);

                while (!isLLGSelected || Array.IndexOf(llgIdList.Split(','), selectedLLGVal.ToString()) == -1)
                {
                    Console.WriteLine("Please enter valid number");
                    isLLGSelected = int.TryParse(Console.ReadLine(), out selectedLLGVal);
                }
            }

            if (selectedReportVal == 5 && selectedLLGVal != LLGAllNo) //5 - location roll, when location then 
            {
                isLLGSelected = GetSelectedWard();
            }

            return isLLGSelected;
        }

        private static bool GetSelectedWard()
        {
            bool isWardSelected = false;

            var llgWiseWardList = objWard.Where(c => c.LLGId == selectedLLGVal).ToList().OrderBy(o => o.WardNumber).ToList();
            if (llgWiseWardList != null && llgWiseWardList.Count > 0)
            {
                Console.WriteLine("What Ward?\r");
                for (int i = 0; i < llgWiseWardList.Count; i++)
                {
                    Console.WriteLine("\t" + llgWiseWardList[i].WardId + " " + llgWiseWardList[i].WardNumber + "-" + llgWiseWardList[i].WardName);
                }

                WardAllNo = objWard.OrderByDescending(c => c.WardId).FirstOrDefault().WardId + 1;
                Console.WriteLine("\t" + WardAllNo + " All");

                Console.WriteLine("Please enter select ward");
                string llgIdList = string.Join(",", llgWiseWardList.Select(c => c.WardId.ToString())) + "," + WardAllNo;
                isWardSelected = int.TryParse(Console.ReadLine(), out selectedWardVal);

                while (!isWardSelected || Array.IndexOf(llgIdList.Split(','), selectedWardVal.ToString()) == -1)
                {
                    Console.WriteLine("Please enter valid number");
                    isWardSelected = int.TryParse(Console.ReadLine(), out selectedWardVal);
                }
            }
            return isWardSelected;
        }

        /// <summary>
        /// Gets the selected possible deletion report.
        /// </summary>
        /// <returns></returns>
        private static bool GetSelectedPossibleDeletionReport()
        {
            bool isPossibleDeletionSelected = false;
            Console.WriteLine("Include Possible Deletion Report?\r");
            Console.WriteLine("\t1 Yes");
            Console.WriteLine("\t2 No");

            Console.WriteLine("Please enter possible deletion report option");
            isPossibleDeletionSelected = int.TryParse(Console.ReadLine(), out selectedPossibleDeletionReportVal);

            while (!isPossibleDeletionSelected || (selectedPossibleDeletionReportVal < 1) || (selectedPossibleDeletionReportVal > 2))
            {
                Console.WriteLine("Please enter valid number");
                isPossibleDeletionSelected = int.TryParse(Console.ReadLine(), out selectedPossibleDeletionReportVal);
            }
            return isPossibleDeletionSelected;
        }

        /// <summary>
        /// Gets the selected envelop.
        /// </summary>
        /// <returns></returns>
        private static bool GetSelectedEnvelop()
        {
            bool isEnvelopSelected = false;
            Console.WriteLine("Include Envelop Label?\r");
            Console.WriteLine("\t1 Yes");
            Console.WriteLine("\t2 No");

            Console.WriteLine("Please enter envelop label");
            isEnvelopSelected = int.TryParse(Console.ReadLine(), out selectedEnvelopVal);

            while (!isEnvelopSelected || (selectedEnvelopVal < 1) || (selectedEnvelopVal > 2))
            {
                Console.WriteLine("Please enter valid number");
                isEnvelopSelected = int.TryParse(Console.ReadLine(), out selectedEnvelopVal);
            }
            return isEnvelopSelected;
        }

        /// <summary>
        /// Gets the selected gender.
        /// </summary>
        /// <returns></returns>
        private static bool GetSelectedGender()
        {
            bool isGenderSelected = false;
            Console.WriteLine("Include Gender?\r");
            Console.WriteLine("\t1 Male");
            Console.WriteLine("\t2 Female");
            Console.WriteLine("\t3 All");

            Console.WriteLine("Please enter select gender");
            isGenderSelected = int.TryParse(Console.ReadLine(), out selectedGenderVal);

            while (!isGenderSelected || (selectedGenderVal < 1) || (selectedGenderVal > 3))
            {
                Console.WriteLine("Please enter valid number");
                isGenderSelected = int.TryParse(Console.ReadLine(), out selectedGenderVal);
            }

            isGenderSelected = GetSelectedAlphaSplit();

            return isGenderSelected;
        }

        private static bool GetSelectedAlphaSplit()
        {
            bool isAlphaSplitSelected = false;
            Console.WriteLine("Include Alpha Split?\r");
            Console.WriteLine("\t1 Yes");
            Console.WriteLine("\t2 No");

            Console.WriteLine("Please enter alpha split option");
            isAlphaSplitSelected = int.TryParse(Console.ReadLine(), out selectedAlphaSplitOption);

            while (!isAlphaSplitSelected || (selectedAlphaSplitOption < 1) || (selectedAlphaSplitOption > 2))
            {
                Console.WriteLine("Please enter valid option");
                isAlphaSplitSelected = int.TryParse(Console.ReadLine(), out selectedAlphaSplitOption);
            }

            if (selectedAlphaSplitOption == 1)
            {
                Console.WriteLine("Please enter start alphabet");
                isAlphaSplitSelected = char.TryParse(Console.ReadLine(), out selectedStartAlphabet);
                isAlphaSplitSelected = char.IsLetter(selectedStartAlphabet);
                while (!isAlphaSplitSelected)
                {
                    Console.WriteLine("Please enter valid start alphabet");
                    isAlphaSplitSelected = char.TryParse(Console.ReadLine(), out selectedStartAlphabet);
                    isAlphaSplitSelected = char.IsLetter(selectedStartAlphabet);
                }

                Console.WriteLine("Please enter end alphabet");
                isAlphaSplitSelected = char.TryParse(Console.ReadLine(), out selectedEndAlphabet);
                isAlphaSplitSelected = char.IsLetter(selectedEndAlphabet);
                while (!isAlphaSplitSelected)
                {
                    Console.WriteLine("Please enter valid end alphabet");
                    isAlphaSplitSelected = char.TryParse(Console.ReadLine(), out selectedEndAlphabet);
                    isAlphaSplitSelected = char.IsLetter(selectedEndAlphabet);
                }
            }

            return isAlphaSplitSelected;
        }

        /// <summary>
        /// Gets the selected percentage.
        /// </summary>
        /// <returns></returns>
        private static bool GetSelectedPercentage()
        {
            int form11Option = 0;
            bool isCorrectOptionSelected = false;
            Console.WriteLine("do you want to generate form11 on percentage or number of forms?\r");
            Console.WriteLine("\t1 Percentage");
            Console.WriteLine("\t2 Number of forms");

            Console.WriteLine("Please enter option from above");
            isCorrectOptionSelected = int.TryParse(Console.ReadLine(), out form11Option);
            while (!isCorrectOptionSelected || (form11Option < 1) || (form11Option > 2))
            {
                Console.WriteLine("Please enter valid option");
                isCorrectOptionSelected = int.TryParse(Console.ReadLine(), out form11Option);
            }

            if (form11Option == 1)
            {
                bool isPercentageSelected = false;
                Console.WriteLine("What PERCENTAGE (%) of Voter based Form 11?\r");

                Console.WriteLine("Please enter percentage between 1 to 100");
                isPercentageSelected = int.TryParse(Console.ReadLine(), out selectedPercentageVal);

                while (!isPercentageSelected || (selectedPercentageVal < 1) || (selectedPercentageVal > 100))
                {
                    Console.WriteLine("Please enter valid number");
                    isPercentageSelected = int.TryParse(Console.ReadLine(), out selectedPercentageVal);
                }
                return isPercentageSelected;
            }
            else
            {
                bool isnumberSelected = false;
                Console.WriteLine("How many form11 want you generate?\r");

                Console.WriteLine("Please enter number between 1 to 300");
                isnumberSelected = int.TryParse(Console.ReadLine(), out selectedNumberOfFormsVal);

                while (!isnumberSelected || (selectedNumberOfFormsVal < 1) || (selectedNumberOfFormsVal > 300))
                {
                    Console.WriteLine("Please enter valid number");
                    isnumberSelected = int.TryParse(Console.ReadLine(), out selectedNumberOfFormsVal);
                }
                return isnumberSelected;
            }
        }

        /// <summary>
        /// Generates the report asynchronous.
        /// </summary>
        /// <param name="locationString">The location string.</param>
        /// <returns></returns>
        private static async Task<int> GenerateReportAsync(string locationString, string GeoName, int GeoId, string saveFolderPath)
        {
            string Gender = string.Empty;
            bool isEnvelopLabel = false;
            bool isPossibleDeletion = false;

            #region Database
            if (selectedDBVal == 1)
            {
                DatabaseName = "Staging";
            }
            else if (selectedDBVal == 2)
            {
                DatabaseName = "Preliminary";
            }
            else if (selectedDBVal == 3)
            {
                DatabaseName = "Final";
            }
            #endregion

            //#region Server
            //if (selectedServerVal == 1)
            //{
            //    ServerName = "ProductionApiURL";
            //}
            //else if (selectedServerVal == 2)
            //{
            //    ServerName = "TrainingApiURL";
            //}
            //else if (selectedServerVal == 3)
            //{
            //    ServerName = "DevelopmentApiURL";
            //}
            //#endregion

            if (selectedEnvelopVal == 1)
            {
                isEnvelopLabel = true;
            }
            else
            {
                isEnvelopLabel = false;
            }

            if (selectedPossibleDeletionReportVal == 1)
            {
                isPossibleDeletion = true;
            }
            else
            {
                isPossibleDeletion = false;
            }

            #region Gender
            if (selectedGenderVal == 1)
            {
                Gender = "M";
            }
            else if (selectedGenderVal == 2)
            {
                Gender = "F";
            }
            else if (selectedGenderVal == 3)
            {
                Gender = "All";
            }
            #endregion

            if (isEnvelopLabel)
            {
                if (selectedReportVal == 5)
                {
                    LocationModel locationData = objLocation.Where(c => c.LocationId == GeoId).FirstOrDefault();
                    envelopeExport.LocationNameList = new List<LocationModel> { locationData };
                }
                else
                {
                    envelopeExport.LocationNameList = await DataService.GetLocationListByWardIdAsync(GeoId, ServerNameKey);
                }
            }

            return await Report.ReportService.GenerateWardRollReport(selectedReportVal, DatabaseName, ServerNameKey, Gender, locationString.Replace("/", "_").Replace("\\", "_"), GeoName, GeoId, isEnvelopLabel, saveFolderPath, envelopeExport, isPossibleDeletion, selectedAlphaSplitOption, selectedStartAlphabet.ToString().ToUpper(), selectedEndAlphabet.ToString().ToUpper(), selectedReportWardRollType);
        }

        /// <summary>
        /// Generates the report again.
        /// </summary>
        private async static Task GenerateReportAgain()
        {
            bool isGenerateReportAgain = false;

            Console.WriteLine("Would you like to generate report again?\r");
            Console.WriteLine("\t1 Yes");
            Console.WriteLine("\t2 No");

            Console.WriteLine("Please enter your choice");
            isGenerateReportAgain = int.TryParse(Console.ReadLine(), out selectedReportGenerateAgain);

            while (!isGenerateReportAgain || (selectedReportGenerateAgain < 1) || (selectedReportGenerateAgain > 2))
            {
                Console.WriteLine("Please enter valid number");
                isGenerateReportAgain = int.TryParse(Console.ReadLine(), out selectedReportGenerateAgain);
            }

            if (selectedReportGenerateAgain == 1)
            {
                objProvince = null;
                objDistrict = null;
                objLLG = null;
                objWard = null;
                selectedReportVal = 0;
                selectedDBVal = 0;
                selectedServerVal = 0;
                selectedProvinceVal = 0;
                selectedDistrictVal = 0;
                DistrictAllNo = 0;
                selectedLLGVal = 0;
                LLGAllNo = 0;
                selectedWardVal = 0;
                WardAllNo = 0;
                selectedEnvelopVal = 0;
                selectedReportGenerateAgain = 0;
                selectedProvinceVal = 0;
                selectedNumberOfFormsVal = 0;
                selectedAlphaSplitOption = 0;
                await MainCall();
            }
            else
            {
                await Report.ReportService.AddActivityLogForReport(loginResponse.Data.ListItems.FirstOrDefault().UserId, "Login", "Login Out " + loginResponse.Data.ListItems.FirstOrDefault().EmailAddress);
                Environment.Exit(0);
            }
        }

        #region Form 11
        /// <summary>
        /// Deletes the QRCode Images Directory
        /// </summary>
        private static void DeleteQRCodeImagesDirectory()
        {
            if (Directory.Exists(string.Concat(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), "\\QRCodeImages")))
            {
                Directory.Delete(string.Concat(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), "\\QRCodeImages"), true);
            }
        }

        /// <summary>
        /// Enrolments the forms export report source.
        /// </summary>
        /// <param name="startRange">The start range.</param>
        /// <param name="endRange">The end range.</param>
        /// <param name="provinceId">The province identifier.</param>
        /// <param name="wardId">The ward identifier.</param>
        /// <param name="regionId">The region identifier.</param>
        /// <param name="districtId">The district identifier.</param>
        /// <param name="llgId">The LLG identifier.</param>
        /// <param name="batchName">Name of the batch.</param>
        /// <returns></returns>
        private static async Task<byte[]> EnrolmentFormsExportReportSource(int startRange, int endRange, int regionId, int provinceId, int districtId, int llgId, int wardId, string batchName)
        {
            byte[] bytes = null;

            await Task.Delay(10);

            var numberOfForms = endRange - startRange;
            string[] PDFfilepaths = new string[numberOfForms + 1];
            string FormNo, qrCodeHashNumber, QRCodeUrl, firstFormNo = "", lastFormNo = "";

            var RegionName = objRegion.Where(x => x.RegionId == regionId).Select(y => y.RegionName).FirstOrDefault();
            var ProvinceName = objProvince.Where(x => x.ProvinceId == provinceId).Select(y => y.ProvinceName).FirstOrDefault();
            var DistrictName = objDistrict.Where(x => x.DistrictId == districtId).Select(y => y.DistrictName).FirstOrDefault();
            var LLGName = objLLG.Where(x => x.LLGId == llgId).Select(y => y.LLGName).FirstOrDefault();
            var WardName = objWard.Where(x => x.WardId == wardId).Select(y => y.WardName).FirstOrDefault();
            var WardNumber = objWard.Where(x => x.WardId == wardId).Select(y => y.WardNumber).FirstOrDefault();
            var LocationNameList = "";
            //var LocationNameList = Globals.GeoList.LocationList.Where(x => x.RegionId == regionId
            //    && x.ProvinceId == provinceId
            //    && x.DistrictId == districtId
            //    && x.LLGId == llgId
            //    && x.WardId == wardId).Select(y => y.LocationName).ToList();

            string LocationStr = "";
            //string LocationStr = string.Join(",", LocationNameList);

            dtForm11 = new DataTable("tblForm11");
            dtForm11.Clear();
            dtForm11.Columns.Add("FormId", typeof(Int32));
            dtForm11.Columns.Add("FormNo");


            dtSubForm11 = new DataTable("tblSubForm11");
            dtSubForm11.Clear();
            dtSubForm11.Columns.Add("FormId", typeof(Int32));
            dtSubForm11.Columns.Add("FormNo");
            dtSubForm11.Columns.Add("QRCodeUrl");
            dtSubForm11.Columns.Add("Province");
            dtSubForm11.Columns.Add("District");
            dtSubForm11.Columns.Add("LLG");
            dtSubForm11.Columns.Add("WardNoWardName");

            for (int i = 0; i <= numberOfForms; i++)
            {
                DataRow drForm11 = dtForm11.NewRow();
                DataRow drSubForm11 = dtSubForm11.NewRow();

                drForm11["FormId"] = (i + 1);
                drSubForm11["FormId"] = (i + 1);

                FormNo = GenerateFormNumber(i, startRange, provinceId, wardId);
                FormNo.Replace("#", "");
                drForm11["FormNo"] = FormNo;
                drSubForm11["FormNo"] = FormNo;

                qrCodeHashNumber = GenerateQRCodeHashNumber(regionId, provinceId, districtId, llgId, wardId, batchName, i, startRange);

                QRCodeUrl = GenerateQRCode(qrCodeHashNumber);
                drSubForm11["QRCodeUrl"] = @"file:\" + QRCodeUrl;

                drSubForm11["Province"] = ProvinceName;
                drSubForm11["District"] = DistrictName;
                drSubForm11["LLG"] = LLGName;
                drSubForm11["WardNoWardName"] = WardNumber + " - " + WardName;

                dtForm11.Rows.Add(drForm11);
                dtSubForm11.Rows.Add(drSubForm11);

                if (i == 0)
                {
                    firstFormNo = FormNo;
                }
                else if (i == numberOfForms)
                {
                    lastFormNo = FormNo;
                }
            }

            if ((dtForm11 != null && dtSubForm11 != null) && (dtForm11.Rows.Count > 0 && dtSubForm11.Rows.Count > 0))
            {
                dsForm11 = new DataSet();
                dsForm11.Tables.AddRange(new DataTable[] { dtForm11, dtSubForm11 });

                if (dsForm11.Tables[0].Rows != null && dsForm11.Tables[0].Rows.Count > 0)
                {
                    Warning[] warnings;
                    string[] streamIds;
                    string mimeType = string.Empty;
                    string encoding = string.Empty;
                    string extension = string.Empty;
                    dsForm11.Tables[0].TableName = "tblForm11";

                    string formRange = string.Empty;

                    if (!string.IsNullOrWhiteSpace(firstFormNo) && !string.IsNullOrWhiteSpace(lastFormNo))
                    {
                        formRange = firstFormNo + "-" + lastFormNo;
                    }
                    else
                    {
                        formRange = firstFormNo;
                    }

                    ReportParameter[] param = new ReportParameter[8];
                    param[0] = new ReportParameter("Region", RegionName, true);
                    param[1] = new ReportParameter("Province", ProvinceName, true);
                    param[2] = new ReportParameter("District", DistrictName, true);
                    param[3] = new ReportParameter("LLG", LLGName, true);
                    param[4] = new ReportParameter("Ward", WardName, true);
                    param[5] = new ReportParameter("Location", LocationStr, true);
                    param[6] = new ReportParameter("FormCount", Convert.ToString(numberOfForms + 1), true);
                    param[7] = new ReportParameter("FormRange", (formRange), true);

                    ReportDataSource dataSource = new ReportDataSource("ds_Form11", dsForm11.Tables[0].Copy());
                    ReportViewer viewer = new ReportViewer();
                    viewer.LocalReport.Refresh();
                    viewer.LocalReport.ReportPath = "Reports/Form11.rdlc";
                    viewer.LocalReport.EnableExternalImages = true;
                    viewer.LocalReport.SetParameters(param);
                    viewer.LocalReport.DataSources.Add(dataSource);
                    viewer.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SetEnrolmentFormsSubDataSource);
                    bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
                }
            }

            return bytes;
        }

        /// <summary>
        /// Sets the enrolment forms sub data source.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="SubreportProcessingEventArgs"/> instance containing the event data.</param>
        private static void SetEnrolmentFormsSubDataSource(object sender, SubreportProcessingEventArgs e)
        {
            long FormId = Convert.ToInt32(e.Parameters[0].Values[0]);

            if (FormId > 0)
            {
                DataTable tblSubRCardFiltered = dsForm11.Tables[1].AsEnumerable().Where(row => row.Field<int>("FormId") == FormId).CopyToDataTable();
                tblSubRCardFiltered.TableName = "tblSubForm11";
                ReportDataSource datasource = new ReportDataSource("ds_SubForm11", tblSubRCardFiltered);
                e.DataSources.Add(datasource);
            }
        }

        /// <summary>
        /// Generates the form number.
        /// </summary>
        /// <param name="formNumberCount">The form number count.</param>
        /// <returns>return form number</returns>
        private static string GenerateFormNumber(int formNumberCount, int startRange, int provinceId, int wardId)
        {
            var ward = wardId.ToString().PadLeft(4, '0');
            var numberWithRange = (startRange + formNumberCount).ToString().PadLeft(7, '0');
            return string.Format("{0}{1}{2}", provinceId, ward, numberWithRange);
        }

        /// <summary>
        /// Generates the qr code hash number.
        /// </summary>
        /// <param name="regionId">The region identifier.</param>
        /// <param name="provinceId">The province identifier.</param>
        /// <param name="districtId">The district identifier.</param>
        /// <param name="llgId">The LLG identifier.</param>
        /// <param name="wardId">The ward identifier.</param>
        /// <param name="batchName">Name of the batch.</param>
        /// <param name="formNumberCount">The form number count.</param>
        /// <param name="startRange">The start range.</param>
        /// <returns></returns>
        private static string GenerateQRCodeHashNumber(int regionId, int provinceId, int districtId, int llgId, int wardId, string batchName, int formNumberCount, int startRange)
        {
            var numberWithRange = (startRange + formNumberCount).ToString().PadLeft(7, '0');
            return string.Format
                ("{0}#{1}#{2}#{3}#{4}#{5}#{6}", regionId, provinceId, districtId, llgId, wardId, batchName, numberWithRange);

        }

        /// <summary>
        /// Generates the QR Code.
        /// </summary>
        /// <param name="modeQCTextl">The QCText.</param>
        private static string GenerateQRCode(string QCText)
        {
            var QCwriter = new BarcodeWriter();
            QCwriter.Options = new ZXing.Common.EncodingOptions { Margin = 0, Height = 100, Width = 100 };
            QCwriter.Format = BarcodeFormat.QR_CODE;

            var result = QCwriter.Write(QCText);

            string appPath = string.Concat(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), "\\QRCodeImages");
            string filePath = string.Concat(appPath, "\\", QCText.Replace("#", string.Empty).Replace(" ", string.Empty), ".jpg");
            if (!Directory.Exists(appPath))
            {
                Directory.CreateDirectory(appPath);
            }
            var barcodeBitmap = new Bitmap(result);

            using (MemoryStream memory = new MemoryStream())
            {
                using (FileStream fs = new FileStream(filePath,
                   FileMode.Create, FileAccess.ReadWrite))
                {
                    //byte array to html
                    barcodeBitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Jpeg);
                    byte[] bytes = memory.ToArray();
                    fs.Write(bytes, 0, bytes.Length);
                }
            }
            return filePath;
        }
        #endregion
    }
}
