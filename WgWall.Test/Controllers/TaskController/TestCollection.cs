using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using WgWall.Api.Dto;
using WgWall.Api.Request.Base;
using WgWall.Data.Repository.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace WgWall.Test.Controllers.TaskController
{
    public abstract class TestCollection
    {
        private ServiceProvider _serviceProvider;

        protected void SetServiceProvider(ServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        private WgWall.Controllers.TaskController GetController()
        {
            return new WgWall.Controllers.TaskController(_serviceProvider.GetService<IFrontendUserRepository>(), _serviceProvider.GetService<ITaskRepository>(), _serviceProvider.GetService<ITaskTemplateRepository>());
        }

        [TestMethod]
        public async Task Get_ShouldReturnTasks()
        {
            //arrange
            var controller = GetController();

            //act
            var result = await controller.GetActiveTasks();

            //assert
            AssertHelper.AssertList<TaskDto>(result);
        }

        [TestMethod]
        public async Task PostTask_ShouldAddToCollection()
        {
            //arrange
            var controller = GetController();
            var frontendUser = await ServiceProviderHelper.GetActiveUser(_serviceProvider);
            var taskTemplate = await ServiceProviderHelper.GetSomeTaskTemplate(_serviceProvider);

            //act
            var previousResult = await controller.GetActiveTasks();
            var newTask = await controller.Create(taskTemplate.Id, new AccountablePayload() { FrontendUserId = frontendUser.Id });
            var result = await controller.GetActiveTasks();

            //assert
            AssertNewTask(newTask, taskTemplate.Id);
            var previousList = AssertHelper.AssertList<TaskDto>(previousResult); ;
            var list = AssertHelper.AssertList<TaskDto>(result);
            Assert.IsTrue(list.Count == previousList.Count + 1);
        }

        [TestMethod]
        public async Task Done_ShouldRemoveTask()
        {
            //arrange
            var controller = GetController();
            var frontendUser = await ServiceProviderHelper.GetActiveUser(_serviceProvider);

            //act
            var previousResult = await controller.GetActiveTasks();
            var previousList = AssertHelper.AssertList<TaskDto>(previousResult);
            var prod = previousList[0];

            await controller.Done(prod.Id, new AccountablePayload { FrontendUserId = frontendUser.Id });
            var result = await controller.GetActiveTasks();
            var list = AssertHelper.AssertList<TaskDto>(result);
            var doneProd = list.FirstOrDefault(p => p.Id == prod.Id);

            //assert
            Assert.IsNull(doneProd);
            Assert.IsTrue(list.Count + 1 == previousList.Count);
        }

        [TestMethod]
        public async Task Delete_ShouldRemoveTask()
        {
            //arrange
            var controller = GetController();

            //act
            var previousResult = await controller.GetActiveTasks();
            var previousList = AssertHelper.AssertList<TaskDto>(previousResult);
            var prod = previousList[0];

            await controller.Delete(prod.Id);
            var result = await controller.GetActiveTasks();
            var list = AssertHelper.AssertList<TaskDto>(result);
            var doneProd = list.FirstOrDefault(p => p.Id == prod.Id);

            //assert
            Assert.IsNull(doneProd);
            Assert.IsTrue(list.Count + 1 == previousList.Count);
        }

        private TaskDto AssertNewTask(IActionResult result, int expectedTemplateId)
        {
            var objectResult = result as OkObjectResult;
            Assert.IsNotNull(objectResult);

            var task = objectResult.Value as TaskDto;
            Assert.IsNotNull(task);
            Assert.IsTrue(task.Id > 0);
            Assert.AreEqual(task.TaskTemplateId, expectedTemplateId);

            return task;
        }
    }
}
