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
            var expectedUserFields = new[] {"name", "id"};
            var newUser = new FrontendUserDto() { Name = "new user name", Id = 2};
            using (var client = new TestClientProvider())
            {
                //check
                var checkUserResponse = await client.PostJsonAsync("/api/FrontendUsers/check", newUser);
                Assert.IsFalse((bool)checkUserResponse);

                //creation
                var newUserResponse = await client.PostJsonAsync("/api/FrontendUsers", newUser);
                AssertHelper.AssertFields(newUserResponse as JObject, expectedUserFields);

                //check
                checkUserResponse = await client.PostJsonAsync("/api/FrontendUsers/check", newUser);
                Assert.IsTrue((bool)checkUserResponse);

                //list
                var response = await client.GetJsonAsync("/api/FrontendUsers");
                Assert.IsInstanceOfType(response, typeof(JArray));
                AssertHelper.AssertFields(((JArray)response)[0] as JObject, expectedUserFields);
            }
        }
    }
}
