﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WgWall.Data.Model;
using WgWall.Data.Repositories.Interfaces;

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
namespace WgWall.Test.Mock.Data.Repositories
{
    class MockFrontendUserRepository : IFrontendUserRepository
    {
        private List<FrontendUser> _testSet;

        public MockFrontendUserRepository(List<FrontendUser> testSet)
        {
            _testSet = testSet;
        }

        public async Task<bool> CheckExistenceAsync(string name)
        {
            return _testSet.FirstOrDefault(f => f.Name == name) != null;
        }

        public async Task<FrontendUser> CreateFrontendUserAsync(string name)
        {
            var user = FrontendUser.Create(name);
            _testSet.Add(user);
            return user;
        }

        public async Task<List<FrontendUser>> GetAllAsync()
        {
            return _testSet;
        }
    }
}
