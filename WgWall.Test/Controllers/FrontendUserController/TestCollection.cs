using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;
using WgWall.Controllers;
using WgWall.Data.Model;
using WgWall.Data.Repository.Interfaces;
using WgWall.Dto;

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
            var controller = new WgWall.Controllers.FrontendUserController(_serviceProvider.GetService<IFrontendUserRepository>());

            //act
            var notExitingUser = new FrontendUserDto() { Name = "not existing" };
            var result = await controller.Check(notExitingUser);

            //assert
            AssertHelper.AssertBooleanResult(result, false);
        }

        [TestMethod]
        public async Task Check_ShouldReturnTrue()
        {
            //arrange
            var controller = new WgWall.Controllers.FrontendUserController(_serviceProvider.GetService<IFrontendUserRepository>());

            //act
            var exitingUser = new FrontendUserDto() { Name = "Florian" };
            var result = await controller.Check(exitingUser);

            //assert
            AssertHelper.AssertBooleanResult(result, true);
        }

        [TestMethod]
        public async Task CreateFrontendUser_ShouldPersistUser()
        {
            //arrange
            var controller = new WgWall.Controllers.FrontendUserController(_serviceProvider.GetService<IFrontendUserRepository>());

            //act
            var newUser = new FrontendUserDto() { Name = "NewName" };
            var result = await controller.PostFrontendUser(newUser);

            //assert
            AssertNewUser(result, newUser);
        }

        [TestMethod]
        public async Task GetFrontendUsers_ShouldReturnAllUsers()
        {
            //arrange
            var controller = new WgWall.Controllers.FrontendUserController(_serviceProvider.GetService<IFrontendUserRepository>());

            //act
            var newUser = new FrontendUserDto() {Name = "NewName"};
            var result = await controller.GetFrontendUsers();
            var creationResult = await controller.PostFrontendUser(newUser);
            var result2 = await controller.GetFrontendUsers();

            //assert
            var list = AssertUsers(result);
            AssertNewUser(creationResult, newUser);
            var list2 = AssertUsers(result2);
            Assert.IsTrue(list.Count + 1 == list2.Count);
        }

        protected FrontendUserDto AssertNewUser(IActionResult result, FrontendUserDto newUser)
        {
            var objectResult = result as OkObjectResult;
            Assert.IsNotNull(objectResult);

            var user = objectResult.Value as FrontendUserDto;
            Assert.IsNotNull(user);
            Assert.AreEqual(newUser.Name, user.Name);
            Assert.IsTrue(user.Id > 0);

            return user;
        }

        protected IList<FrontendUserDto> AssertUsers(IActionResult result)
        {
            var objectResult = result as OkObjectResult;
            Assert.IsNotNull(objectResult);

            var users = objectResult.Value as IList<FrontendUserDto>;
            Assert.IsNotNull(users);

            return users;
        }
    }
}
