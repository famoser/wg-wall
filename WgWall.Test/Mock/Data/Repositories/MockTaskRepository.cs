using System;
using System.Collections.Generic;
using System.Text;
using WgWall.Data.Model;
using WgWall.Data.Repository.Interfaces;

namespace WgWall.Test.Mock.Data.Repositories
{
    public class MockTaskRepository : ITaskRepository
    {
        private readonly List<Task> _testSet;
        private readonly ITaskTemplateRepository _taskTemplateRepository;

        public MockTaskRepository(List<Task> testSet, ITaskTemplateRepository taskTemplateRepository)
        {
            _testSet = testSet;
            _taskTemplateRepository = taskTemplateRepository;
        }

        public async System.Threading.Tasks.Task<Task> Create(int taskTemplateId, FrontendUser frontendUser)
        {
            _testSet.Add(Task.Create(_taskTemplateRepository.GetAllAsync()));
        }

        public System.Threading.Tasks.Task<List<Task>> GetActiveAsync()
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task Done(int taskId, FrontendUser frontendUser)
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task Remove(int taskId)
        {
            throw new NotImplementedException();
        }
    }
}