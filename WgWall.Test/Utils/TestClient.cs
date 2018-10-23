using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using WgWall.Test.IntegrationTests.Utils.Interface;
using WgWall.Test.Utils;

namespace WgWall.Test.IntegrationTests.Utils
{
    public class TestClient<TStartup> : IDisposable, ITestClient
        where TStartup : WgWall.Startup
    {
        private readonly TestServer _server;

        private readonly HttpClient _client;

        public TestClient()
        {
            _server = new TestServer(new WebHostBuilder().UseStartup<TStartup>());
            _client = _server.CreateClient();
        }

        public async Task<object> GetJsonAsync(string link)
        {
            var response = await _client.GetAsync(link);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);

            return await DeserializeResponse(response);
        }

        public async Task<object> PostAsync<T1>(string link, T1 content) where T1 : class
        {
            var response = await _client.PostAsync(link, new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json"));
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);

            return await DeserializeResponse(response);
        }

        public async Task<object> PutAsync<T>(string link, T content)
        {
            var response = await _client.PutAsync(link, new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json"));
            Assert.IsTrue(response.StatusCode == HttpStatusCode.NoContent);

            return await DeserializeResponse(response);
        }

        public async Task<object> DeleteAsync(string link)
        {
            var response = await _client.DeleteAsync(link);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.NoContent);

            return await DeserializeResponse(response);
        }

        private async Task<object> DeserializeResponse(HttpResponseMessage response)
        {
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject(json);
        }

        public void Dispose()
        {
            File.Delete(SeededDatabaseStartup.DbName);
            _server?.Dispose();
            _client?.Dispose();
        }
    }
}
