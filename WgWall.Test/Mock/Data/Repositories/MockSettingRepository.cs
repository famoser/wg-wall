using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WgWall.Data.Model;
using WgWall.Data.Repository.Interfaces;

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
namespace WgWall.Test.Mock.Data.Repositories
{
    class MockSettingRepository : ISettingRepository
    {
        private readonly List<Setting> _testSet;

        public MockSettingRepository(List<Setting> testSet)
        {
            _testSet = testSet;
        }

        public async Task Persist(string key, string value)
        {
            var existing = _testSet.FirstOrDefault(s => s.Key == key);
            if (existing == null)
            {
                existing = new Setting {Key = key};
                _testSet.Add(existing);
            }

            existing.Value = value;
        }

        public async Task<List<Setting>> GetAllAsync()
        {
            return _testSet;
        }
    }
}
