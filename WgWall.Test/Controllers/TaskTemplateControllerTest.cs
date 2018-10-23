using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WgWall.Test.IntegrationTests.Utils;
using WgWall.Test.IntegrationTests.Utils.Interface;

namespace WgWall.Test.IntegrationTests
{
    [TestClass]
    public class TaskTemplateControllerTest : AbstractIntegrationTest
    {
        protected override async Task PerformIntegrationTest(ITestClient testClient)
        {
            //arrange
            var expectedFields = new[] { "id", "name", "intervalInDays", "lastExecutionAt" };
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
