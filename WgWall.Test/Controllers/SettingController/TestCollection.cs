using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WgWall.Api.Dto;
using WgWall.Api.Request;
using WgWall.Controllers;
using WgWall.Data.Model;
using WgWall.Data.Repository.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace WgWall.Test.Controllers.SettingController
{
    public abstract class TestCollection
    {
        private ServiceProvider _serviceProvider;

        protected void SetServiceProvider(ServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        [TestMethod]
        public async Task Persist_ShouldPersistSetting()
        {
            //arrange
            var controller = new WgWall.Controllers.SettingController(_serviceProvider.GetService<ISettingRepository>());

            //act
            var newSetting = new SettingPayload() { Key = "new", Value = "yes"};
            await controller.PostSetting(newSetting);
            var result = await controller.Get();

            //assert
            var list = AssertSettings(result);
            var res = list.FirstOrDefault(l => l.Key == newSetting.Key);
            Assert.IsNotNull(res);
            Assert.AreEqual(newSetting.Value, res.Value);
        }

        [TestMethod]
        public async Task Get_ShouldReturnSettings()
        {
            //arrange
            var controller = new WgWall.Controllers.SettingController(_serviceProvider.GetService<ISettingRepository>());

            //act
            var result = await controller.Get();

            //assert
            AssertSettings(result);
        }

        public static IList<SettingDto> AssertSettings(IActionResult result)
        {
            var objectResult = result as OkObjectResult;
            Assert.IsNotNull(objectResult);

            var settings = objectResult.Value as IList<SettingDto>;
            Assert.IsNotNull(settings);

            return settings;
        }
    }
}
