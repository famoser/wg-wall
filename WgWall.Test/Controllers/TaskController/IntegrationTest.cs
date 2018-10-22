using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using WgWall.Api.Request;
using WgWall.Api.Request.Base;

namespace WgWall.Test.Controllers.TaskController
{
    [TestClass]
    public class IntegrationTest
    {
        [TestMethod]
        public async Task Get_ShouldReturnExpectedFields()
        {
            var expectedTaskFields = new[] {"id", "activatedAt", "taskTemplateId"};
            using (var client = new TestClientProvider())
            {
                //list
                var response = await client.GetJsonAsync("/api/TaskExecution");
                Assert.IsInstanceOfType(response, typeof(JArray));
                AssertHelper.AssertFields(((JArray)response)[0] as JObject, expectedTaskFields);
            }
        }
    }
}
