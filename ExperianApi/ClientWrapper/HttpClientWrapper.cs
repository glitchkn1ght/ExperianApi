using ExperianApi.Interfaces;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ExperianApi.ClientHandler
{
    public class HttpClientWrapper : IHttpClientWrapper
    {
        private HttpClient Client = new HttpClient();

        public HttpClientWrapper(HttpClient client)
        {
            this.Client = client;
        }

        public void SetBaseAddress(Uri baseAddress)
        {
            this.Client.BaseAddress = baseAddress;
        }

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            return await Client.GetAsync(url);
        }
    }
}
