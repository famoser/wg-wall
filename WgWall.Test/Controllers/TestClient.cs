using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WgWall.Test.Mock;

namespace WgWall.Test.Controllers
{
    public class TestClientProvider : IDisposable
    {
        private readonly TestServer _server;

        private readonly HttpClient _client;

        public TestClientProvider()
        {
            _server = new TestServer(new WebHostBuilder().UseStartup<MockStartup>());

            _client = _server.CreateClient();
        }

        public async Task<object> GetJsonAsync(string link)
        {
            var response = await _client.GetAsync(link);
            return await DeserializeResponse(response);
        }

        public async Task<object> PostJsonAsync(string link, object content)
        {
            var response = await _client.PostAsync(link, new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json"));
            return await DeserializeResponse(response);
        }

        private async Task<object> DeserializeResponse(HttpResponseMessage response)
        {
            Assert.IsTrue(response.StatusCode == HttpStatusCode.NoContent || response.StatusCode == HttpStatusCode.OK);

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject(json);
        }

        public void Dispose()
        {
            File.Delete(MockStartup.DbName);
            _server?.Dispose();
            _client?.Dispose();
        }
    }
}
