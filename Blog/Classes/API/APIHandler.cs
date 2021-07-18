using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Classes.API
{
    public class APIHandler
    {
        private HttpClient http;
        public APIHandler(HttpClient http)
        {
            this.http = http;
        }
             
        public async Task<T> APICall<T>(object obj ,HttpMethod method, string endpoint)
        {
            var message = HttpRequestMessage(method, endpoint, obj);
            var response = await http.SendAsync(message);
            return await GetContent<T>(response);
        }

        private HttpRequestMessage HttpRequestMessage(HttpMethod method, string endpoint, object obj)
        {
            HttpRequestMessage message = new();
            message.Method = method;
            message.RequestUri = new Uri($"{APIAddressHandler.APIAddress}{endpoint}");
            message.Content = HttpContent(obj);
            return message;
        }

        private StringContent HttpContent(object obj)
        {
            if (obj != null)
            {
                var serilizeContent = JsonConvert.SerializeObject(obj, Formatting.Indented);
                var content = new StringContent(serilizeContent, Encoding.UTF8, "application/json");
                return content;
            }
            else return null;
        }

        private async Task<T> GetContent<T>(HttpResponseMessage response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var content = JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
                    return content;
            }
            return default;
        }
    }
}
