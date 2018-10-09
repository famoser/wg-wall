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
            var result = await controller.CreateFrontendUser("NewUser");

            //assert
            AssertHelper.AssertInstanceResult(result, typeof(FrontendUser));
        }
    }
}
