using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ExperianApi.Interfaces
{
    public interface IHttpClientWrapper
    {
        public Task<HttpResponseMessage> GetAsync(string url);

        public void SetBaseAddress(Uri baseAddress);
    }
}
