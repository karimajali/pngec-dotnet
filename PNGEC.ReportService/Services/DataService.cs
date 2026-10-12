using Newtonsoft.Json;
using PNGEC.API.Domain.District;
using PNGEC.API.Domain.LLG;
using PNGEC.API.Domain.Location;
using PNGEC.API.Domain.Province;
using PNGEC.API.Domain.Region;
using PNGEC.API.Domain.Report;
using PNGEC.API.Domain.Ward;
using PNGEC.API.Helper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PNGEC.ReportService.Data
{
    public class DataService
    {
        /// <summary>
        /// Gets the region list asynchronous.
        /// </summary>
        /// <returns></returns>
        public static async Task<List<RegionModel>> GetRegionListAsync(string serverName)
        {
            using (var client = new HttpClient())
            {
                client.Timeout = new TimeSpan(0, 10, 0);
                var url = new Uri(GetServerBaseUrl(serverName) + "Region/GetRegionList");
                var webresponse = await client.GetAsync(url);
                string resp = await webresponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<RegionModel>>(resp);
            }
        }

        /// <summary>
        /// Gets the province list asynchronous.
        /// </summary>
        /// <returns></returns>
        public static async Task<List<ProvinceModel>> GetProvinceListAsync(string serverName)
        {
            using (var client = new HttpClient())
            {
                client.Timeout = new TimeSpan(0, 10, 0);
                var url = new Uri(GetServerBaseUrl(serverName) + "Province/GetProvinceList");
                var webresponse = await client.GetAsync(url);
                string resp = await webresponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<ProvinceModel>>(resp);
            }
        }

        /// <summary>
        /// Gets the district list asynchronous.
        /// </summary>
        /// <returns></returns>
        public static async Task<List<DistrictModel>> GetDistrictListAsync(string serverName)
        {
            using (var client = new HttpClient())
            {
                client.Timeout = new TimeSpan(0, 10, 0);
                var url = new Uri(GetServerBaseUrl(serverName) + "District/GetDistrictList");
                var webresponse = await client.GetAsync(url);
                string resp = await webresponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<DistrictModel>>(resp);
            }
        }

        /// <summary>
        /// Gets the LLG list asynchronous.
        /// </summary>
        /// <returns></returns>
        public static async Task<List<LLGModel>> GetLLGListAsync(string serverName)
        {
            using (var client = new HttpClient())
            {
                client.Timeout = new TimeSpan(0, 10, 0);
                var url = new Uri(GetServerBaseUrl(serverName) + "LLG/GetLLGList");
                var webresponse = await client.GetAsync(url);
                string resp = await webresponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<LLGModel>>(resp);
            }
        }

        /// <summary>
        /// Gets the ward list asynchronous.
        /// </summary>
        /// <returns></returns>
        public static async Task<List<WardModel>> GetWardListAsync(string serverName)
        {
            using (var client = new HttpClient())
            {
                client.Timeout = new TimeSpan(0, 10, 0);
                var url = new Uri(GetServerBaseUrl(serverName) + "Ward/GetWardList");
                var webresponse = await client.GetAsync(url);
                string resp = await webresponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<WardModel>>(resp);
            }
        }

        /// <summary>
        /// Gets the location list asynchronous.
        /// </summary>
        /// <returns></returns>
        public static async Task<List<LocationModel>> GetLocationListAsync(string serverName)
        {
            using (var client = new HttpClient())
            {
                client.Timeout = new TimeSpan(0, 15, 0);
                var url = new Uri(GetServerBaseUrl(serverName) + "Location/GetLocationList");
                var webresponse = await client.GetAsync(url);
                string resp = await webresponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<LocationModel>>(resp);
            }
        }

        /// <summary>
        /// Gets the location list by ward identifier asynchronous.
        /// </summary>
        /// <param name="wardId">The ward identifier.</param>
        /// <returns></returns>
        public static async Task<List<LocationModel>> GetLocationListByWardIdAsync(int wardId, string serverName)
        {
            using (var client = new HttpClient())
            {
                client.Timeout = new TimeSpan(0, 10, 0);
                var url = new Uri(GetServerBaseUrl(serverName) + "Location/ListLocationByWardId/" + wardId);
                var webresponse = await client.GetAsync(url);
                string resp = await webresponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<LocationModel>>(resp);
            }
        }

        /// <summary>
        /// Gets the wards number of form11.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static async Task<Response> GetWardsNumberOfForm11(object model, string serverName)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ReportService.Report.ReportService.GetServerBaseUrl(serverName));

                var dataContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                using (HttpResponseMessage response = client.PostAsync("Reports/GetWardsNumberOfForm11", dataContent).Result)
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<Response>(responseBody);
                    }
                }
                return JsonConvert.DeserializeObject<Response>(null);
            }
        }

        /// <summary>
        /// Gets the server base URL.
        /// </summary>
        /// <param name="serverName">Name of the server.</param>
        /// <returns></returns>
        public static string GetServerBaseUrl()
        {
            if (ConfigurationManager.AppSettings["IsGeoFromProduction"] == "true")
            {
                return ConfigurationManager.AppSettings["ProductionApiURL"];
            }
            else
            {
                return ConfigurationManager.AppSettings["TrainingApiURL"];
            }
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
    }
}
