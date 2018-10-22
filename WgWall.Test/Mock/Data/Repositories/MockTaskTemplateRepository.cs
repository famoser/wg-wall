using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WgWall.Data.Model;
using WgWall.Data.Repository.Interfaces;
using Task = System.Threading.Tasks.Task;

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
namespace WgWall.Test.Mock.Data.Repositories
{
    public class MockTaskTemplateRepository : ITaskTemplateRepository
    {
        private readonly List<WgWall.Data.Model.Task> _testSet;

        public MockTaskTemplateRepository(List<WgWall.Data.Model.Task> testSet)
        {
            _testSet = testSet;
        }

        public async Task Update(WgWall.Data.Model.Task task)
        {
            //dont need to do anything
        }

        public async Task<WgWall.Data.Model.Task> Create(string name, int? intervalInDays, FrontendUser frontendUser)
        {
            var taskTemplate = WgWall.Data.Model.Task.Create(name, intervalInDays, frontendUser);
            taskTemplate.Id = _testSet.Max(t => t.Id) + 1;
            _testSet.Add(taskTemplate);

            return taskTemplate;
        }

        public async Task Update(int taskTemplateId, string name, int? intervalInDays)
        {
            var taskTemplate = await TryGet(taskTemplateId);
            if (taskTemplate == null) return;

            taskTemplate.Name = name;
            taskTemplate.IntervalInDays = intervalInDays;
        }

        public async Task<List<WgWall.Data.Model.Task>> GetAllAsync()
        {
            return new List<WgWall.Data.Model.Task>(_testSet);
        }

        public async Task Hide(int taskTemplateId)
        {
            var taskTemplate = await TryGet(taskTemplateId);
            if (taskTemplate == null) return;

            taskTemplate.Hidden = true;
        }

        public async Task<WgWall.Data.Model.Task> TryGet(int templateId)
        {
            return _testSet.FirstOrDefault(t => t.Id == templateId);
        }
    }
}
