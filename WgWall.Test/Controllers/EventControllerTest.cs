using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WgWall.Test.Utils;
using WgWall.Test.Utils.Interface;

namespace WgWall.Test.Controllers
{
    [TestClass]
    public class EventControllerTest : AbstractIntegrationTest
    {
        protected override async Task PerformIntegrationTest(ITestClient testClient)
        {
            //arrange
            var expectedFields = new[] { "id", "name", "startDate" };
            var payloadFields = new Dictionary<string, object> { { "name", "new event" }, { "startDate", DateTime.Now } };
            var apiUrl = "/api/Event";

            //test get
            await TestGet(testClient, apiUrl, expectedFields);

            //test post
            await TestPost(testClient, apiUrl, expectedFields, payloadFields, true);

            //test put
            await TestPut(testClient, apiUrl, expectedFields, payloadFields);

            //test remove
            await TestRemove(testClient, apiUrl, expectedFields);
        }
    }
}
