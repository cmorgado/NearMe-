using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NearMe.Domain.Code;
using NearMe.Domain.Interfaces;
using NearMe.Rest.Models;

namespace NearMe.Rest.Service
{
    public partial class ServiceBroker
    {
        IPlatform _global;

        private readonly HttpClient _client = new HttpClient();

        public ServiceBroker(IPlatform platform)
        {
            _client.DefaultRequestHeaders.Clear();
            _global = platform;
        }

        public ServiceBroker()
        {
        }

        public string BaseUrl { get; set; }

        /// <summary>
        /// </summary>
        /// <param name="format"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        private Dictionary<string, string> GetDefaultParameters(string format = "xml", bool language = false)
        {
            var oParameters = new Dictionary<string, string> {{"format", format}};


            if (language)
            {
                oParameters.Add("Language", CultureInfo.CurrentUICulture.TwoLetterISOLanguageName);
            }
            return oParameters;
        }


        private Uri ProcessRequest(string sUrl, Dictionary<string, string> oParameters)
        {
            const string sSeparator = "&";


            // Argument validation
            if (string.IsNullOrEmpty(sUrl))
            {
                throw new ArgumentException("Request URL is mandatory parameter.");
            }

            // Build the URL
            var sRequest = sUrl;
            foreach (var oParameter in oParameters.Keys)
            {
                var bIndInsertSeparator =
                    !((sRequest[sRequest.Length - 1] == '&') || (sRequest[sRequest.Length - 1] == '?'));
                sRequest = string.Concat(sRequest, (bIndInsertSeparator ? sSeparator : ""), oParameter, "=",
                    oParameters[oParameter]);
            }
            return new Uri(sRequest);
        }

        private async Task<RestResult<T>> DoHttpGet<T>(StringBuilder url)
        {
            var r = new RestResult<T>();

            var response = new HttpResponseMessage();
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get,
                    new Uri(Uri.EscapeUriString(url.ToString()), UriKind.RelativeOrAbsolute));

                response = await _client.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    //var content = await response.Content.ReadAsStringAsync();
                    //Debug.WriteLine(content);
                    // r.DATA = content.FromJson<T>();

                    var content = await response.Content.ReadAsStringAsync();
                    r.Error = new Error {HasError = false};
                    if (typeof (T) != typeof (string))
                        r.Data = content.FromJson<T>();
                    else
                        r.Raw = content;
                }
                else
                {
                    r.Error.HasError = true;
                    r.Error.Message = $"HTTP error: {response.StatusCode}";
                    r.Error.ErrorCode = response.StatusCode.ToString();
                    r.Raw = await response.Content.ReadAsStringAsync();
                }
            }
            catch (HttpRequestException hex)
            {
                r.Error.HasError = true;
                r.Error.Message = $"HTTP error: {hex.Message}";
                r.Error.ErrorCode = response.StatusCode.ToString();
            }
            catch (Exception ex)
            {
                r.Error.HasError = true;
                r.Error.Message = $"HTTP error: {ex.Message}";
            }
            return r;
        }


        private async Task<RestResult<T>> DoHttpDelete<T>(StringBuilder url)
        {
            var r = new RestResult<T>();
            var response = new HttpResponseMessage();
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Delete,
                    new Uri(url.ToString(), UriKind.RelativeOrAbsolute));
                response = await _client.SendAsync(request);
                var content = await response.Content.ReadAsStringAsync();
                Debug.WriteLine(content);
                r.Error = new Error {HasError = false};
                r.Data = content.FromJson<T>();
            }
            catch (HttpRequestException hex)
            {
                r.Error.HasError = true;
                r.Error.Message = $"HTTP error: {hex.Message}";
                r.Error.ErrorCode = response.StatusCode.ToString();
            }
            catch (Exception ex)
            {
                r.Error.HasError = true;
                r.Error.Message = $"HTTP error: {ex.Message}";
            }
            return r;
        }

        private async Task<RestResult<T>> DoHttpPut<T>(StringBuilder Url)
        {
            var r = new RestResult<T>();
            var response = new HttpResponseMessage();
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Put, new Uri(Url.ToString(), UriKind.RelativeOrAbsolute));
                response = await _client.SendAsync(request);
                var content = await response.Content.ReadAsStringAsync();
                Debug.WriteLine(content);
                r.Error = new Error {HasError = false};
                r.Data = content.FromJson<T>();
            }
            catch (HttpRequestException hex)
            {
                r.Error.HasError = true;
                r.Error.Message = $"HTTP error: {hex.Message}";
                r.Error.ErrorCode = response.StatusCode.ToString();
            }
            catch (Exception ex)
            {
                r.Error.HasError = true;
                r.Error.Message = $"HTTP error: {ex.Message}";
            }
            return r;
        }


        /// <summary>
        ///     Post JSON
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private async Task<RestResult<T>> DoHttpPostJson<T>(StringBuilder url, object data)
        {
            var r = new RestResult<T>();
            var response = new HttpResponseMessage();
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post,
                    new Uri(url.ToString(), UriKind.RelativeOrAbsolute))
                {
                    Content = new StringContent(data.ToJson(), Encoding.UTF8, "application/json")
                };
                response = await _client.SendAsync(request);
                var content = await response.Content.ReadAsStringAsync();
                r.Data = content.FromJson<T>();
                r.Raw = content;
                //response.EnsureSuccessStatusCode();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    r.Error = new Error {HasError = false};
                }
                else
                {
                    r.Error = new Error
                    {
                        HasError = true,
                        Message = $"HTTP error: {response.StatusCode}",
                        ErrorCode = response.StatusCode.ToString()
                    };
                }
            }
            catch (HttpRequestException hex)
            {
                r.Error.HasError = true;
                r.Error.Message = $"HTTP error: {hex.Message}";
                r.Error.ErrorCode = response.StatusCode.ToString();
            }
            catch (Exception ex)
            {
                r.Error.HasError = true;
                r.Error.Message = $"HTTP error: {ex.Message}";
            }

            return r;
        }

        /// <summary>
        ///     POST FORM
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private async Task<RestResult<T>> DoHttpPostForm<T>(StringBuilder Url, List<KeyValuePair<string, string>> data)
        {
            var r = new RestResult<T>();
            var response = new HttpResponseMessage();
            try
            {
                response = await _client.PostAsync(Url.ToString(), new FormUrlEncodedContent(data));
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    r.Error = new Error {HasError = false};
                    if (typeof (T) != typeof (string))
                        r.Data = content.FromJson<T>();
                    else
                        r.Raw = content;
                }
                else
                {
                    r.Error.HasError = true;
                    var body = await response.Content.ReadAsStringAsync();
                    r.Error.Message = $"HTTP error: {response.StatusCode}";
                    r.Error.ErrorCode = response.StatusCode.ToString();
                    r.Raw = body;
                }
            }
            catch (HttpRequestException hex)
            {
                r.Error.HasError = true;
                r.Error.Message = $"HTTP error: {hex.Message}";
                r.Error.ErrorCode = response.StatusCode.ToString();
            }
            catch (Exception ex)
            {
                r.Error.HasError = true;
                r.Error.Message = $"HTTP error: {ex.Message}";
            }
            return r;
        }
    }
}