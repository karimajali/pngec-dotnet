using Newtonsoft.Json;
using PNGEC.API.Domain.District;
using PNGEC.API.Domain.GeoStructure;
using PNGEC.API.Domain.LLG;
using PNGEC.API.Domain.Location;
using PNGEC.API.Domain.MKAGeoStructure;
using PNGEC.API.Domain.Province;
using PNGEC.API.Domain.Region;
using PNGEC.API.Domain.Ward;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace PNGEC.API.Helper.Data
{
    /// <summary>
    /// data service class
    /// </summary>
    /// <seealso cref="PNGEC.API.Helper.Data.IDataService" />
    public class DataService : IDataService
    {
        /// <summary>
        /// Gets the geo list asynchronous.
        /// </summary>
        /// <returns>
        /// return geo list
        /// </returns>
        public async Task<CommonResponse<GeoListModel>> GetGeoListAsync(int userId)
        {
            using (var client = new HttpClient())
            {
                client.Timeout = new TimeSpan(0, 10, 0);
                //var url = new Uri($"{ConfigurationManager.AppSettings["apiURL"]}GeoStructure/GetGeoList/" + userId);
                var url = new Uri($"{Globals.serverURL}GeoStructure/GetGeoList/" + userId);
                var webresponse = await client.GetAsync(url);
                string resp = await webresponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<CommonResponse<GeoListModel>>(resp);
            }
        }
        /// <summary>
        /// Gets the geo list asynchronous.
        /// </summary>
        /// <returns>
        /// return geo list
        /// </returns>
        public async Task<CommonResponse<MKAGeoListModel>> GetMKAGeoListAsync(int userId)
        {
            using (var client = new HttpClient())
            {
                client.Timeout = new TimeSpan(0, 10, 0);
                //var url = new Uri($"{ConfigurationManager.AppSettings["apiURL"]}GeoStructure/GetGeoList/" + userId);
                var url = new Uri($"{Globals.serverURL}MKAGeoStructure/GetMKAGeoList/" + userId);
                var webresponse = await client.GetAsync(url);
                string resp = await webresponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<CommonResponse<MKAGeoListModel>>(resp);
            }
        }
        /// <summary>
        /// Gets the region list asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<List<RegionModel>> GetRegionListAsync()
        {
            using (var client = new HttpClient())
            {
                client.Timeout = new TimeSpan(0, 10, 0);
                //var url = new Uri($"{ConfigurationManager.AppSettings["apiURL"]}Region/GetRegionList");
                var url = new Uri($"{Globals.serverURL}Region/GetRegionList");
                var webresponse = await client.GetAsync(url);
                string resp = await webresponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<RegionModel>>(resp);
            }
        }

        /// <summary>
        /// Gets the province list asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<List<ProvinceModel>> GetProvinceListAsync()
        {
            using (var client = new HttpClient())
            {
                client.Timeout = new TimeSpan(0, 10, 0);
                //var url = new Uri($"{ConfigurationManager.AppSettings["apiURL"]}Province/GetProvinceList");
                var url = new Uri($"{Globals.serverURL}Province/GetProvinceList");
                var webresponse = await client.GetAsync(url);
                string resp = await webresponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<ProvinceModel>>(resp);
            }
        }

        /// <summary>
        /// Gets the district list asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<List<DistrictModel>> GetDistrictListAsync()
        {
            using (var client = new HttpClient())
            {
                client.Timeout = new TimeSpan(0, 10, 0);
                //var url = new Uri($"{ConfigurationManager.AppSettings["apiURL"]}District/GetDistrictList");
                var url = new Uri($"{Globals.serverURL}District/GetDistrictList");
                var webresponse = await client.GetAsync(url);
                string resp = await webresponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<DistrictModel>>(resp);
            }
        }

        /// <summary>
        /// Gets the LLG list asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<List<LLGModel>> GetLLGListAsync()
        {
            using (var client = new HttpClient())
            {
                client.Timeout = new TimeSpan(0, 10, 0);
                //var url = new Uri($"{ConfigurationManager.AppSettings["apiURL"]}LLG/GetLLGList");
                var url = new Uri($"{Globals.serverURL}LLG/GetLLGList");
                var webresponse = await client.GetAsync(url);
                string resp = await webresponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<LLGModel>>(resp);
            }
        }

        /// <summary>
        /// Gets the ward list asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<List<WardModel>> GetWardListAsync()
        {
            using (var client = new HttpClient())
            {
                client.Timeout = new TimeSpan(0, 10, 0);
                //var url = new Uri($"{ConfigurationManager.AppSettings["apiURL"]}Ward/GetWardList");
                var url = new Uri($"{Globals.serverURL}Ward/GetWardList");
                var webresponse = await client.GetAsync(url);
                string resp = await webresponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<WardModel>>(resp);
            }
        }

        /// <summary>
        /// Gets the location list asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<List<LocationModel>> GetLocationListAsync()
        {
            using (var client = new HttpClient())
            {
                client.Timeout = new TimeSpan(0, 10, 0);
                //var url = new Uri($"{ConfigurationManager.AppSettings["apiURL"]}Location/GetLocationList");
                var url = new Uri($"{Globals.serverURL}Location/GetLocationList");
                var webresponse = await client.GetAsync(url);
                string resp = await webresponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<LocationModel>>(resp);
            }
        }
    }
}
