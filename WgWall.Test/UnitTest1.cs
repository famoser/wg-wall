using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WgWall.Controllers;
using WgWall.Data.Model;
using WgWall.Test.Mock;
using WgWall.Test.Mock.Data.Repositories;

namespace WgWall.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task GetProduct_ShouldNotFindProduct()
        {
            var controller = new FrontendUsersController(new MockFrontendUserRepository(GetTestUsers()));

            var result = await controller.Check("not contained");
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            //var res = await result.ExecuteResultAsync()
        }
        private List<FrontendUser> GetTestUsers()
        {
            var frontendUsers = new List<FrontendUser>
            {
                new FrontendUser {Name = "Florian"},
                new FrontendUser {Name = "Cédric"},
                new FrontendUser {Name = "Xenia"}
            };
            return frontendUsers;
        }
    }
}
