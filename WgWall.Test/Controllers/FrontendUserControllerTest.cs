using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WgWall.Test.Utils;
using WgWall.Test.Utils.Interface;

namespace WgWall.Test.Controllers
{
    [TestClass]
    public class FrontendUserControllerTest : AbstractIntegrationTest
    {
        protected override async Task PerformIntegrationTest(ITestClient testClient)
        {
            //arrange
            var expectedFields = new[] { "id", "name", "karma" };
            var payloadFields = new Dictionary<string, object> { { "name", "ich bin der Kameramann" } };
            var apiUrl = "/api/FrontendUser";

            //test get
            await TestGet(testClient, apiUrl, expectedFields);

            //test post
            await TestPost(testClient, apiUrl, expectedFields, payloadFields, true);

            //test put
            await TestPut(testClient, apiUrl, expectedFields, payloadFields);
        }
    }
}
