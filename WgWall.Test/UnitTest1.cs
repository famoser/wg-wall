using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WgWall.Controllers;
using WgWall.Model;
using WgWall.Test.Mock;

namespace WgWall.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

        }


        [TestMethod]
        public async Task GetProduct_ShouldNotFindProduct()
        {
            var controller = new FrontendUsersController(new MockDbContext());

            var result = await controller.GetFrontendUser(999);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
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
