using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;
using WgWall.Test.Utils;
using WgWall.Test.Utils.Interface;

namespace WgWall.Test.Controllers
{
    [TestClass]
    public class SettingControllerTest : AbstractIntegrationTest
    {
        protected override async Task PerformIntegrationTest(ITestClient testClient)
        {
            //arrange
            var expectedFields = new[] { "id", "key", "value" };
            var payloadFields = new Dictionary<string, object> { { "key", "password" }, { "value", "fcb1893" } };
            var apiUrl = "/api/Setting";

            //test get
            await TestGet(testClient, apiUrl, expectedFields);

            //test post
            await TestPost(testClient, apiUrl, expectedFields, payloadFields, true);

            //test put
            await TestPut(testClient, apiUrl, expectedFields, payloadFields);
        }
    }
}
