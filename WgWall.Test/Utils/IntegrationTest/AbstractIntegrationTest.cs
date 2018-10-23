using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WgWall.Test.Utils.IntegrationTest.Interface;
using WgWall.Test.Utils.Startup;

namespace WgWall.Test.Utils.IntegrationTest
{
    public abstract class AbstractIntegrationTest
    {
        //[TestMethod]
        public async Task IntegrationTestWithMockRepositories()
        {
            using (var client = new TestClient<MockRepositoriesStartup>())
            {
                await PerformIntegrationTest(client);
            }
        }

        [TestMethod]
        public async Task IntegrationTestWithMockDatabase()
        {
            using (var client = new TestClient<MockDatabaseStartup>())
            {
                await PerformIntegrationTest(client);
            }
        }

        protected abstract Task PerformIntegrationTest(ITestClient testClient);
    }
}
