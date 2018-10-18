using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using WgWall.Api.Request;

namespace WgWall.Test.Controllers.TaskTemplateController
{
    [TestClass]
    public class IntegrationTest
    {
        [TestMethod]
        public async Task Get_ShouldReturnExpectedFields()
        {
            var expectedTaskTemplateFields = new[] {"name", "id", "intervalInDays", "lastActivationAt", "hide"};
            var newTaskTemplate = new TaskTemplatePayload() { Name = "new task name", IntervalInDays = 2};
            using (var client = new TestClientProvider())
            {
                //creation
                var newTaskTemplateResponse = await client.PostJsonAsync("/api/TaskTemplate", newTaskTemplate);
                AssertHelper.AssertFields(newTaskTemplateResponse as JObject, expectedTaskTemplateFields);
                
                //list
                var response = await client.GetJsonAsync("/api/TaskTemplate");
                Assert.IsInstanceOfType(response, typeof(JArray));
                AssertHelper.AssertFields(((JArray)response)[0] as JObject, expectedTaskTemplateFields);
            }
        }
    }
}
