using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WgWall.Test.Mock;

namespace WgWall.Test.Controllers
{
    public class TestClientProvider : IDisposable
    {
        private readonly TestServer _server;

        public HttpClient Client { get; }

        public TestClientProvider()
        {
            _server = new TestServer(new WebHostBuilder().UseStartup<MockStartup>());
            
            Client = _server.CreateClient();
        }

        public async Task<object> GetJsonAsync(string link)
        {
            var response = await Client.GetAsync(link);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject(json);
        }

        public void Dispose()
        {
            File.Delete(MockStartup.DbName);
            _server?.Dispose();
            Client?.Dispose();
        }
    }
}
