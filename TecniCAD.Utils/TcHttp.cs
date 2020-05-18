using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TecniCAD.Utils
{
    public class TcHttp
    {
        private HttpClient http;
        private string HttpUrl;

        public TcHttp(string url)
        {
            HttpUrl = url;
            http = new HttpClient();
        }

        /// <summary>
        /// Returns a List<typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<List<T>> GetJsonList<T>() where T : class
        {
            var response = await http.GetStringAsync(HttpUrl);
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<List<T>>(response);
            return result;
        }

        public async Task<T> GetJsonClass<T>() where T : class
        {
            var response = await http.GetStringAsync(HttpUrl);
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(response);
            return result;
        }

        public HttpClient GetHttpClient()
        {
            return http;
        }

        public string ConvertToJson(dynamic obj)
        {
            var result = Newtonsoft.Json.JsonConvert.SerializeObject(obj);

            return result;
        }

        public StringContent GetJsonHttpContent(dynamic obj)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(obj).ToString();

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            return content;
        }

    }
}
