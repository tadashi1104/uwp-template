using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace UwpTemplate.Helpers
{
    class ApiSend
    {
        static HttpClient client = new HttpClient();

        /// <summary>
        /// API呼び出し
        /// </summary>
        /// <param name="url">呼び出し対象URL</param>
        /// <param name="json">ボディにセットするJson</param>
        /// <returns></returns>
        public async Task<T> SendGet<T>(string url, string json = null)
        {
            string errMsg;

            try
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
                if (json != null)
                {
                    var body = new StringContent(json, Encoding.UTF8, "application/json");
                    request.Content = body;
                    request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    request.Content.Headers.ContentType.CharSet = "UTF-8";
                }

                HttpResponseMessage response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    using (HttpContent content = response.Content)
                    {
                        var result = await content.ReadAsStringAsync();
                        var res = JsonConvert.DeserializeObject<T>(result);
                        return res;
                    }
                }
                else
                {
                    string result;
                    using (HttpContent content = response.Content)
                    {
                        result = await content.ReadAsStringAsync();
                    }
                    errMsg = $@"{response.StatusCode.ToString()} : {result}";
                }


            }
            catch
            {
                throw;
            }

            throw new Exception(errMsg);

        }
        
        /// <summary>
        /// API呼び出し
        /// </summary>
        /// <param name="url">呼び出し対象URL</param>
        /// <param name="httpMethod">HTTPメソッド</param>
        /// <param name="dic">ボディにセットするdictionaly</param>
        /// <returns></returns>
        public async Task<bool> Send(string url, HttpMethod httpMethod, Dictionary<string, string> dic = null)
        {
            string errMsg;

            try
            {
                HttpRequestMessage request = new HttpRequestMessage(httpMethod, url);
                if (dic != null)
                {
                    var body = new FormUrlEncodedContent(dic);
                    request.Content = body;
                    request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
                    //request.Content.Headers.ContentType.CharSet = "UTF-8";
                }

                HttpResponseMessage response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    string result;
                    using (HttpContent content = response.Content)
                    {
                        result = await content.ReadAsStringAsync();
                    }
                    errMsg = $@"{response.StatusCode.ToString()} : {result}";
                }


            }
            catch
            {
                throw;
            }

            throw new Exception(errMsg);

        }
    }
}
