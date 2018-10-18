using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WgWall.Test.Controllers.TaskController
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
