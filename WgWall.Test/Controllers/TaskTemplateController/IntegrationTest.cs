using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using WgWall.Api.Request;
using WgWall.Test.Utils;
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
            var expectedTaskTemplateFields = new[] { "name", "id", "intervalInDays", "lastExecutionAt" };
            var newTaskTemplate = new TaskTemplatePayload() { Name = "new task template name", IntervalInDays = 2 };

            //test create
            var newTaskTemplateResponse = await testClient.PostJsonAsync("/api/TaskTemplate", newTaskTemplate);
            AssertHelper.AssertFields(newTaskTemplateResponse as JObject, expectedTaskTemplateFields);

            //test get
            var response = await testClient.GetJsonAsync("/api/TaskTemplate");
            Assert.IsInstanceOfType(response, typeof(JArray));
            AssertHelper.AssertFields(((JArray)response)[0] as JObject, expectedTaskTemplateFields);
        }
    }
}
