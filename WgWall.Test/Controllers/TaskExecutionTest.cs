using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;
using WgWall.Test.Utils;
using WgWall.Test.Utils.Interface;

namespace WgWall.Test.Controllers
{
    [TestClass]
    public class TaskExecutionTest : AbstractIntegrationTest
    {
        protected override async Task PerformIntegrationTest(ITestClient testClient)
        {
            //arrange
            var expectedFields = new[] { "id" };
            var payloadFields = new Dictionary<string, object> { { "taskTemplateId", 1 }, { "frontendUserId", 1 } };
            var apiUrl = "/api/TaskExecution";
            
            //test post
            await TestPost(testClient, apiUrl, expectedFields, payloadFields);
        }
    }
}
