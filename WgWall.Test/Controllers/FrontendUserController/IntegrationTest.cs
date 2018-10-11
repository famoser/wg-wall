using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using WgWall.Dto;

namespace WgWall.Test.Controllers.FrontendUserController
{
    [TestClass]
    public class IntegrationTest
    {
        [TestMethod]
        public async Task Get_ShouldReturnExpectedFields()
        {
            var expectedUserFields = new[] {"name", "id", "karma", "profileImageSrc"};
            var newUser = new FrontendUserDto() { Name = "new user name"};
            using (var client = new TestClientProvider())
            {
                //creation
                var newUserResponse = await client.PostJsonAsync("/api/FrontendUser", newUser);
                AssertHelper.AssertFields(newUserResponse as JObject, expectedUserFields);
                
                //list
                var response = await client.GetJsonAsync("/api/FrontendUser");
                Assert.IsInstanceOfType(response, typeof(JArray));
                AssertHelper.AssertFields(((JArray)response)[0] as JObject, expectedUserFields);
            }
        }
    }
}
