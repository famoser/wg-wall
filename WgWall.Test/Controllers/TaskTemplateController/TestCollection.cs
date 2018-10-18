using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using WgWall.Api.Dto;
using WgWall.Api.Request;
using WgWall.Data.Repository.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace WgWall.Test.Controllers.TaskTemplateController
{
    public abstract class TestCollection
    {
        private ServiceProvider _serviceProvider;

        protected void SetServiceProvider(ServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        private WgWall.Controllers.TaskTemplateController GetController()
        {
            return new WgWall.Controllers.TaskTemplateController(_serviceProvider.GetService<IFrontendUserRepository>(), _serviceProvider.GetService<ITaskTemplateRepository>());
        }

        [TestMethod]
        public async Task Get_ShouldReturnTasks()
        {
            //arrange
            var controller = GetController();

            //act
            var result = await controller.Get();

            //assert
            AssertHelper.AssertList<TaskTemplateDto>(result);
        }

        [TestMethod]
        public async Task PostTaskTemplate_ShouldAddToCollection()
        {
            //arrange
            var controller = GetController();
            var frontendUser = await ServiceProviderHelper.GetActiveUser(_serviceProvider);

            //act
            var previousResult = await controller.Get();
            var payload = new TaskTemplatePayload() { Name = "newName", FrontendUserId = frontendUser.Id };
            var newTaskTemplate = await controller.PostTaskTemplate(payload);
            var result = await controller.Get();

            //assert
            AssertNewTaskTemplate(newTaskTemplate, payload);
            var previousList = AssertHelper.AssertList<TaskTemplateDto>(previousResult);
            var list = AssertHelper.AssertList<TaskTemplateDto>(result);
            Assert.IsTrue(list.Count == previousList.Count + 1);
        }

        [TestMethod]
        public async Task PutTaskTemplate_ShouldSaveChanges()
        {
            //arrange
            var controller = GetController();
            var frontendUser = await ServiceProviderHelper.GetActiveUser(_serviceProvider);

            //act
            var previousResult = await controller.Get();
            var previousList = AssertHelper.AssertList<TaskTemplateDto>(previousResult);
            var prod = previousList[0];

            var payload = new TaskTemplatePayload() { Name = "newName", IntervalInDays = 100, FrontendUserId = frontendUser.Id };
            await controller.PutTaskTemplate(prod.Id, payload);

            var result = await controller.Get();
            var list = AssertHelper.AssertList<TaskTemplateDto>(result);
            var newProd = list.FirstOrDefault(p => p.Id == prod.Id);

            //assert
            Assert.IsNotNull(newProd);
            Assert.AreEqual(payload.Name, newProd.Name);
            Assert.AreEqual(payload.IntervalInDays, newProd.IntervalInDays);
            Assert.IsTrue(list.Count == previousList.Count);
        }

        [TestMethod]
        public async Task Hide_ShouldSetHideProperty()
        {
            //arrange
            var controller = GetController();

            //act
            var previousResult = await controller.Get();
            var previousList = AssertHelper.AssertList<TaskTemplateDto>(previousResult);
            var prod = previousList[0];

            await controller.Hide(prod.Id);
            var result = await controller.Get();
            var list = AssertHelper.AssertList<TaskTemplateDto>(result);
            var doneProd = list.FirstOrDefault(p => p.Id == prod.Id);

            //assert
            Assert.IsNotNull(doneProd);
            Assert.IsTrue(doneProd.Hide);
            Assert.IsTrue(list.Count == previousList.Count);
        }

        private TaskTemplateDto AssertNewTaskTemplate(IActionResult result, TaskTemplatePayload expectedTaskTemplate)
        {
            var objectResult = result as OkObjectResult;
            Assert.IsNotNull(objectResult);

            var task = objectResult.Value as TaskTemplateDto;
            Assert.IsNotNull(task);
            Assert.IsTrue(task.Id > 0);
            Assert.AreEqual(task.IntervalInDays, expectedTaskTemplate.IntervalInDays == 0 ? null : expectedTaskTemplate.IntervalInDays);
            Assert.AreEqual(task.Name, expectedTaskTemplate.Name);

            return task;
        }
    }
}
