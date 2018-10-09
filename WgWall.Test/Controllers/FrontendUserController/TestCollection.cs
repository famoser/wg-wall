using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;
using WgWall.Controllers;
using WgWall.Data.Model;
using WgWall.Data.Repository.Interfaces;

namespace WgWall.Test.Controllers.FrontendUserController
{
    public abstract class TestCollection
    {
        private ServiceProvider _serviceProvider;

        protected void SetServiceProvider(ServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        [TestMethod]
        public async Task Check_ShouldReturnFalse()
        {
            //arrange
            var controller = new FrontendUsersController(_serviceProvider.GetService<IFrontendUserRepository>());

            //act
            var result = await controller.Check("no name of an user");

            //assert
            AssertHelper.AssertBooleanResult(result, false);
        }

        [TestMethod]
        public async Task Check_ShouldReturnTrue()
        {
            //arrange
            var controller = new FrontendUsersController(_serviceProvider.GetService<IFrontendUserRepository>());

            //act
            var result = await controller.Check("Florian");

            //assert
            AssertHelper.AssertBooleanResult(result, true);
        }

        [TestMethod]
        public async Task CreateFrontendUser_ShouldPersistUser()
        {
            //arrange
            var controller = new FrontendUsersController(_serviceProvider.GetService<IFrontendUserRepository>());

            //act
            string newName = "NewName";
            var result = await controller.CreateFrontendUser(newName);

            //assert
            AssertNewUser(result, newName);
        }

        [TestMethod]
        public async Task GetFrontendUsers_ShouldReturnAllUsers()
        {
            //arrange
            var controller = new FrontendUsersController(_serviceProvider.GetService<IFrontendUserRepository>());

            //act
            string newName = "NewName";
            var result = await controller.GetFrontendUsers();
            var creationResult = await controller.CreateFrontendUser(newName);
            var result2 = await controller.GetFrontendUsers();

            //assert
            var list = AssertUsers(result);
            AssertNewUser(creationResult, newName);
            var list2 = AssertUsers(result2);
            Assert.IsTrue(list.Count + 1 == list2.Count);
        }

        protected FrontendUser AssertNewUser(IActionResult result, string expectedName)
        {
            var objectResult = result as OkObjectResult;
            Assert.IsNotNull(objectResult);

            var user = objectResult.Value as FrontendUser;
            Assert.IsNotNull(user);
            Assert.AreEqual(expectedName, user.Name);
            Assert.IsTrue(user.Id > 0);

            return user;
        }

        protected IList<FrontendUser> AssertUsers(IActionResult result)
        {
            var objectResult = result as OkObjectResult;
            Assert.IsNotNull(objectResult);

            var users = objectResult.Value as IList<FrontendUser>;
            Assert.IsNotNull(users);

            return users;
        }
    }
}
