using Newtonsoft.Json;
using PNGEC.API.Domain.Listing;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PNGEC.API.Helper
{
    public class GenericHttpClient
    {
        /// <summary>
        /// Gets the response.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns></returns>
        public static async Task<Response> GetResponse(string uri)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Globals.serverURL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                using (HttpResponseMessage response = await client.GetAsync(uri))
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<Response>(responseBody);
                    }

                }
            }
            return JsonConvert.DeserializeObject<Response>(null);
        }

        /// <summary>
        /// Gets the request.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri">The URI.</param>
        /// <returns></returns>
        public static async Task<List<T>> GetRequest<T>(string uri)
        {
            using (var client = new HttpClient())
            {
                //client.BaseAddress = new Uri(ConfigurationManager.AppSettings["apiURL"]);
                client.BaseAddress = new Uri(Globals.serverURL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                using (HttpResponseMessage response = await client.GetAsync(uri))
                {
                    if(response.StatusCode == HttpStatusCode.OK)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<List<T>>(responseBody);
                    }
                }
            }
            return JsonConvert.DeserializeObject<List<T>>(null);
        }

        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns></returns>
        public static async Task<DataTable> GetList(string uri)
        {
            using (var client = new HttpClient())
            {
                //client.BaseAddress = new Uri(ConfigurationManager.AppSettings["apiURL"]);
                client.BaseAddress = new Uri(Globals.serverURL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                using (HttpResponseMessage response = await client.GetAsync(uri))
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<DataTable>(responseBody);
                    }

                }
            }
            return JsonConvert.DeserializeObject<DataTable>(null);
        }

        /// <summary>
        /// Posts the request.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri">The URI.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static async Task<Response<T>> PostRequest<T>(string uri, PageList model)
        {
            using (var client = new HttpClient())
            {
                //client.BaseAddress = new Uri(ConfigurationManager.AppSettings["apiURL"]);
                client.BaseAddress = new Uri(Globals.serverURL);
                using (HttpResponseMessage response = client.PostAsJsonAsync(uri, model).Result)
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<Response<T>>(responseBody);
                    }
                }
                return JsonConvert.DeserializeObject<Response<T>>(null);
            }
        }

        /// <summary>
        /// Posts the request.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri">The URI.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static async Task<Response<T>> PostRequest<T>(string uri, object model)
        {
            using (var client = new HttpClient())
            {
                //client.BaseAddress = new Uri(ConfigurationManager.AppSettings["apiURL"]);
                client.BaseAddress = new Uri(Globals.serverURL);
                using (HttpResponseMessage response = client.PostAsJsonAsync(uri, model).Result)
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<Response<T>>(responseBody);
                    }
                }
                return JsonConvert.DeserializeObject<Response<T>>(null);
            }
        }
        
        /// <summary>
        /// Posts the request.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static async Task<Response> PostRequest(string uri, object model)
        {
            using (var client = new HttpClient())
            {
                //client.BaseAddress = new Uri(ConfigurationManager.AppSettings["apiURL"]);
                client.BaseAddress = new Uri(Globals.serverURL);
                using (HttpResponseMessage response = client.PostAsJsonAsync(uri, model).Result)
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
        /// Posts the request.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri">The URI.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static async Task<CommonResponse<T>> PostRequestCommon<T>(string uri, object model)
        {
            using (var client = new HttpClient())
            {
                //client.BaseAddress = new Uri(ConfigurationManager.AppSettings["apiURL"]);
                client.BaseAddress = new Uri(Globals.serverURL);
                using (HttpResponseMessage response = client.PostAsJsonAsync(uri, model).Result)
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<CommonResponse<T>>(responseBody);
                    }
                }
                return JsonConvert.DeserializeObject<CommonResponse<T>>(null);
            }
        }

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns></returns>
        public static async Task<bool> DeleteRequest(string uri)
        {
            using (var client = new HttpClient())
            {
                //client.BaseAddress = new Uri(ConfigurationManager.AppSettings["apiURL"]);
                client.BaseAddress = new Uri(Globals.serverURL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                using (HttpResponseMessage response = await client.DeleteAsync(uri))
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        return true;
                    }
                }
            }
            return false;
        }


        public static async Task<Response<T>> PostRequest<T>(string uri)
        {
            using (var client = new HttpClient())
            {
                client.Timeout = new TimeSpan(0, 20, 0);
                client.BaseAddress = new Uri(Globals.serverURL);
                using (HttpResponseMessage response = client.PostAsJsonAsync(uri, string.Empty).Result)
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<Response<T>>(responseBody);
                    }
                }
                return JsonConvert.DeserializeObject<Response<T>>(null);
            }
        }
    }
}
