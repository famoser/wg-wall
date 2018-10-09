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
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync("/api/FrontendUsers");
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

                var json = await response.Content.ReadAsStringAsync();
                var users = JsonConvert.DeserializeObject(json) as JArray;

                Assert.IsNotNull(users);
                Assert.IsTrue(users.Count > 0);
                foreach (var user in users)
                {
                    var obj = user as JObject;

                    Assert.IsNotNull(obj);
                    Assert.IsTrue(obj.Properties().Count() == 1);
                }
            }
        }
    }
}
