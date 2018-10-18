using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WgWall.Test.Controllers.TaskTemplateController
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
