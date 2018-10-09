using System.Linq;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WgWall.Test.Controllers.FrontendUserController
{
    [TestClass]
    public class IntegrationTest
    {
        [TestMethod]
        public async Task Get_ShouldReturnExpectedFields()
        {
            using (var client = new TestClientProvider())
            {
                var response = await client.GetJsonAsync("/api/FrontendUsers") as JArray;

                Assert.IsNotNull(response);
                if (response.Count > 0)
                {
                    AssertHelper.AssertFields(response[0] as JObject, new[] {"name", "id"});
                }
            }
        }
    }
}
