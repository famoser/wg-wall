using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.IO;
using System.Net.Http;
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

        public void Dispose()
        {
            File.Delete(MockStartup.DbName);
            _server?.Dispose();
            Client?.Dispose();
        }
    }
}
