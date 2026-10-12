using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using PNGEC.Desktop.Model;

namespace PNGEC.Desktop.Helper
{
    public static class LocationDetails
    {
        /// <summary>
        /// Gets the location details.
        /// </summary>
        public static void GetLocationDetails()
        {
            string externalIpString = new WebClient().DownloadString("http://icanhazip.com").Replace("\\r\\n", "").Replace("\\n", "").Trim();
            string ipAddress = Convert.ToString(IPAddress.Parse(externalIpString));

            // IP API URL
            var Ip_Api_Url = "http://ip-api.com/json/" + ipAddress;

            // Use HttpClient to get the details from the Json response
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                // Pass API address to get the Geolocation details 
                httpClient.BaseAddress = new Uri(Ip_Api_Url);
                HttpResponseMessage httpResponse = httpClient.GetAsync(Ip_Api_Url).GetAwaiter().GetResult();
                // If API is success and receive the response, then get the location details
                if (httpResponse.IsSuccessStatusCode)
                {
                    var geolocationInfo = httpResponse.Content.ReadAsAsync<LocationDetailsModel>().GetAwaiter().GetResult();
                    if (geolocationInfo != null)
                    {
                        GlobalsModel.LocationDetails.City = geolocationInfo.City;
                        GlobalsModel.LocationDetails.Lat = geolocationInfo.Lat;
                        GlobalsModel.LocationDetails.Lon = geolocationInfo.Lon;
                    }
                }
            }
        }
    }
}
