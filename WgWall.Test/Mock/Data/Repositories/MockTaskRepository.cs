using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WgWall.Data.Model;
using WgWall.Data.Repository.Interfaces;
using Task = System.Threading.Tasks.Task;

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
namespace WgWall.Test.Mock.Data.Repositories
{
    public class MockTaskRepository : ITaskExecutionRepository
    {
        private readonly List<WgWall.Data.Model.TaskExecution> _testSet;

        public MockTaskRepository(List<WgWall.Data.Model.TaskExecution> testSet)
        {
            _testSet = testSet;
        }

        public async Task<WgWall.Data.Model.TaskExecution> Create(TaskTemplate taskTemplate, FrontendUser frontendUser)
        {
            var task = WgWall.Data.Model.TaskExecution.Create(taskTemplate, frontendUser);
            task.Id = _testSet.Max(t => t.Id) + 1;
            _testSet.Add(task);

            return task;
        }

        public async Task<List<WgWall.Data.Model.TaskExecution>> GetActiveAsync()
        {
            return _testSet.Where(t => t.DoneBy == null).ToList();
        }

        public async Task Done(int taskId, FrontendUser frontendUser)
        {
            foreach (var task in _testSet.Where(t => t.Id == taskId))
            {
                task.DoneBy = frontendUser;
                task.DoneById = frontendUser.Id;
            }
        }

        public async Task Remove(int taskId)
        {
            _testSet.Remove(_testSet.FirstOrDefault(t => t.Id == taskId));
        }
    }
}