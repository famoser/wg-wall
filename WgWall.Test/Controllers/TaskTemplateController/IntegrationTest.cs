using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WgWall.Test.Utils.IntegrationTest;
using WgWall.Test.Utils.IntegrationTest.Interface;

namespace WgWall.Test.Controllers.TaskTemplateController
{
    [TestClass]
    public class IntegrationTest : AbstractIntegrationTest
    {
        protected override async Task PerformIntegrationTest(ITestClient testClient)
        {
            //arrange
            var expectedFields = new[] { "name", "id", "intervalInDays", "lastExecutionAt" };
            var payloadFields = new Dictionary<string, object> { { "name", "new task template" }, { "intervalInDays", 6 } };
            var apiUrl = "/api/TaskTemplate";

            //test get
            await TestGet(testClient, apiUrl, expectedFields);

            //test post
            await TestPost(testClient, apiUrl, expectedFields, payloadFields, true);

            //test put
            await TestPut(testClient, apiUrl, expectedFields, payloadFields);

            //test put
            await TestRemove(testClient, apiUrl, expectedFields);
        }
    }
}
