using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;
using WgWall.Api.Dto;
using WgWall.Api.Request;
using WgWall.Controllers;
using WgWall.Data.Model;
using WgWall.Data.Repository.Interfaces;
using Task = System.Threading.Tasks.Task;

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
        public async Task CreateFrontendUser_ShouldPersistUser()
        {
            //arrange
            var controller = new WgWall.Controllers.FrontendUserController(_serviceProvider.GetService<IFrontendUserRepository>());

            //act
            var newUser = new FrontendUserPayload() { Name = "NewName" };
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
            var newUser = new FrontendUserPayload() {Name = "NewName"};
            var result = await controller.GetFrontendUsers();
            var creationResult = await controller.PostFrontendUser(newUser);
            var result2 = await controller.GetFrontendUsers();

            //assert
            var list = AssertHelper.AssertList<FrontendUserDto>(result);
            AssertNewUser(creationResult, newUser);
            var list2 = AssertHelper.AssertList<FrontendUserDto>(result2);
            Assert.IsTrue(list.Count + 1 == list2.Count);
        }

        private FrontendUserDto AssertNewUser(IActionResult result, FrontendUserPayload newUser)
        {
            var objectResult = result as OkObjectResult;
            Assert.IsNotNull(objectResult);

            var user = objectResult.Value as FrontendUserDto;
            Assert.IsNotNull(user);
            Assert.AreEqual(newUser.Name, user.Name);
            Assert.IsTrue(user.Id > 0);

            return user;
        }
    }
}
