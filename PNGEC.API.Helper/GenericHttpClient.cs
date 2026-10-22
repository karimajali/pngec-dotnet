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
        private static readonly HttpClient _httpClient = new HttpClient
        {
            Timeout = TimeSpan.FromMinutes(5)
        };

        private static Uri GetTargetUri(string uri)
        {
            string baseUrl = Globals.serverURL;
            if (string.IsNullOrWhiteSpace(baseUrl))
            {
                baseUrl = "http://localhost:57712/api/";
            }
            if (!baseUrl.EndsWith("/"))
            {
                baseUrl += "/";
            }
            return new Uri(new Uri(baseUrl), uri.TrimStart('/'));
        }

        private static HttpRequestMessage CreateRequest(HttpMethod method, string uri, HttpContent content = null)
        {
            var request = new HttpRequestMessage(method, GetTargetUri(uri));
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Add("X-PNGEC-Admin-Key", Globals.AdminSecurityKey);
            if (content != null)
            {
                request.Content = content;
            }
            return request;
        }

        /// <summary>
        /// Gets the response.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns></returns>
        public static async Task<Response> GetResponse(string uri)
        {
            using (var request = CreateRequest(HttpMethod.Get, uri))
            {
                using (HttpResponseMessage response = await _httpClient.SendAsync(request).ConfigureAwait(false))
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        return JsonConvert.DeserializeObject<Response>(responseBody);
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Gets the request.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri">The URI.</param>
        /// <returns></returns>
        public static async Task<List<T>> GetRequest<T>(string uri)
        {
            using (var request = CreateRequest(HttpMethod.Get, uri))
            {
                using (HttpResponseMessage response = await _httpClient.SendAsync(request).ConfigureAwait(false))
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        return JsonConvert.DeserializeObject<List<T>>(responseBody);
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns></returns>
        public static async Task<DataTable> GetList(string uri)
        {
            using (var request = CreateRequest(HttpMethod.Get, uri))
            {
                using (HttpResponseMessage response = await _httpClient.SendAsync(request).ConfigureAwait(false))
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        return JsonConvert.DeserializeObject<DataTable>(responseBody);
                    }
                }
            }
            return null;
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
            var content = new StringContent(JsonConvert.SerializeObject(model), System.Text.Encoding.UTF8, "application/json");
            using (var request = CreateRequest(HttpMethod.Post, uri, content))
            {
                using (HttpResponseMessage response = await _httpClient.SendAsync(request).ConfigureAwait(false))
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        return JsonConvert.DeserializeObject<Response<T>>(responseBody);
                    }
                }
            }
            return null;
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
            var content = new StringContent(JsonConvert.SerializeObject(model), System.Text.Encoding.UTF8, "application/json");
            using (var request = CreateRequest(HttpMethod.Post, uri, content))
            {
                using (HttpResponseMessage response = await _httpClient.SendAsync(request).ConfigureAwait(false))
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        return JsonConvert.DeserializeObject<Response<T>>(responseBody);
                    }
                }
            }
            return null;
        }
        
        /// <summary>
        /// Posts the request.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static async Task<Response> PostRequest(string uri, object model)
        {
            var content = new StringContent(JsonConvert.SerializeObject(model), System.Text.Encoding.UTF8, "application/json");
            using (var request = CreateRequest(HttpMethod.Post, uri, content))
            {
                using (HttpResponseMessage response = await _httpClient.SendAsync(request).ConfigureAwait(false))
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        return JsonConvert.DeserializeObject<Response>(responseBody);
                    }
                }
            }
            return null;
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
            var content = new StringContent(JsonConvert.SerializeObject(model), System.Text.Encoding.UTF8, "application/json");
            using (var request = CreateRequest(HttpMethod.Post, uri, content))
            {
                using (HttpResponseMessage response = await _httpClient.SendAsync(request).ConfigureAwait(false))
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        return JsonConvert.DeserializeObject<CommonResponse<T>>(responseBody);
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns></returns>
        public static async Task<bool> DeleteRequest(string uri)
        {
            using (var request = CreateRequest(HttpMethod.Delete, uri))
            {
                using (HttpResponseMessage response = await _httpClient.SendAsync(request).ConfigureAwait(false))
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
            var content = new StringContent(string.Empty, System.Text.Encoding.UTF8, "application/json");
            using (var request = CreateRequest(HttpMethod.Post, uri, content))
            {
                using (HttpResponseMessage response = await _httpClient.SendAsync(request).ConfigureAwait(false))
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        return JsonConvert.DeserializeObject<Response<T>>(responseBody);
                    }
                }
            }
            return null;
        }
    }
}
