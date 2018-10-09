using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WgWall.Controllers;
using WgWall.Data.Model;
using WgWall.Data.Repository.Interfaces;
using WgWall.Test.Mock.Data.Repositories;

namespace WgWall.Test.Controllers.FrontendUserController
{
    public class TestCollection
    {
        private readonly ServiceProvider _serviceProvider;

        public TestCollection(ServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task GetFrontendUser_ShouldNotFindFrontendUser()
        {
            //arrange
            var controller = new FrontendUsersController(_serviceProvider.GetService<IFrontendUserRepository>());

            //act
            var result = await controller.Check("no name of an user");
            var objectResult = result as OkObjectResult;

            //assert
            Assert.IsNotNull(objectResult);
            Assert.IsInstanceOfType(objectResult.Value, typeof(bool));
            Assert.AreEqual(false, (bool)objectResult.Value);
        }
    }
}
