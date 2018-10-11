using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WgWall.Data.Model;
using WgWall.Data.Repository.Interfaces;

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
namespace WgWall.Test.Mock.Data.Repositories
{
    class MockFrontendUserRepository : IFrontendUserRepository
    {
        private readonly List<FrontendUser> _testSet;

        public MockFrontendUserRepository(List<FrontendUser> testSet)
        {
            _testSet = testSet;
        }

        public async Task<bool> CheckExistenceAsync(string name)
        {
            return _testSet.FirstOrDefault(f => f.Name == name) != null;
        }

        public async Task<FrontendUser> TryGet(int frontendUserId)
        {
            return _testSet.FirstOrDefault(t => t.Id == frontendUserId);
        }

        public async Task<FrontendUser> CreateAsync(string name)
        {
            var user = FrontendUser.Create(name);
            user.Id = _testSet.Max(u => u.Id) + 1;
            _testSet.Add(user);
            return user;
        }

        public async Task<List<FrontendUser>> GetAllAsync()
        {
            return new List<FrontendUser>(_testSet);
        }
    }
}
