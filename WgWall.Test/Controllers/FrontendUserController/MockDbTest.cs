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

namespace WgWall.Test.Controllers.FrontendUserController
{
    [TestClass]
    public class MockDbTest : Base.MockDbTest
    {
        [TestMethod]
        public async Task GetFrontendUser_ShouldNotFindFrontendUser()
        {
            await new TestCollection(ServiceProvider).GetFrontendUser_ShouldNotFindFrontendUser();
        }
    }
}
