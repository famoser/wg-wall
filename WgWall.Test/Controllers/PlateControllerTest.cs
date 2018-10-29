using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;
using WgWall.Test.Utils;
using WgWall.Test.Utils.Interface;

namespace WgWall.Test.Controllers
{
    [TestClass]
    public class PlateControllerTest : AbstractIntegrationTest
    {
        protected override async Task PerformIntegrationTest(ITestClient testClient)
        {
            //arrange
            var expectedFields = new[] { "id", "frontendUserId", "dinnerState" };
            var payloadFields = new Dictionary<string, object> { { "dinnerState", 2 }, { "frontendUserId", 2 } };
            var apiUrl = "/api/Plate";

            //test get
            await TestGet(testClient, apiUrl, expectedFields);

            //test post
            await TestPost(testClient, apiUrl, expectedFields, payloadFields, true);

            //test put
            await TestPut(testClient, apiUrl, expectedFields, payloadFields);
        }
    }
}
