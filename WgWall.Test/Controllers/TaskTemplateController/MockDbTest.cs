using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WgWall.Test.Controllers.TaskTemplateController
{
    [TestClass]
    public class MockDbTest : TestCollection
    {
        [TestInitialize]
        public void TestInitialize()
        {
            SetServiceProvider(ServiceProviderHelper.SetUpMockDb());
        }

        [TestCleanup]
        public void TestCleanup()
        {
            ServiceProviderHelper.CleanupMockDb();
        }
    }
}
