using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WgWall.Controllers;
using WgWall.Data;
using WgWall.Data.Repository;
using WgWall.Data.Repository.Interfaces;
using WgWall.Test.Mock.Data.Repositories;

namespace WgWall.Test.Controllers.SettingController
{
    [TestClass]
    public class MockRepositoryTest : TestCollection
    {
        [TestInitialize]
        public void TestInitialize()
        {
            SetServiceProvider(ServiceProviderHelper.SetUpMockRepositories());
        }
    }
}
