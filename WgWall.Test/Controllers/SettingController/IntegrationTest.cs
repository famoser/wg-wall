using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using WgWall.Api.Request;

namespace WgWall.Test.Controllers.SettingController
{
    [TestClass]
    public class IntegrationTest
    {
        [TestMethod]
        public async Task Get_ShouldReturnExpectedFields()
        {
            var expectedSettingFields = new[] { "key", "value" };
            var newUser = new SettingPayload() { Key = "test", Value = "val" };
            using (var client = new TestClientProvider())
            {
                //create
                await client.PostJsonAsync("/api/Setting", newUser);

                //list
                var response = await client.GetJsonAsync("/api/Setting");
                Assert.IsInstanceOfType(response, typeof(JArray));
                AssertHelper.AssertFields(((JArray)response)[0] as JObject, expectedSettingFields);
            }
        }
    }
}
