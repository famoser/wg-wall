using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;
using WgWall.Test.Utils;
using WgWall.Test.Utils.Interface;

namespace WgWall.Test.Controllers
{
    [TestClass]
    public class ProductPurchaseTest : AbstractIntegrationTest
    {
        protected override async Task PerformIntegrationTest(ITestClient testClient)
        {
            //arrange
            var expectedFields = new[] { "id" };
            var payloadFields = new Dictionary<string, object> { { "productId", 1 }, { "frontendUserId", 1 } };
            var apiUrl = "/api/ProductPurchase";
            
            //test post
            await TestPost(testClient, apiUrl, expectedFields, payloadFields);
        }
    }
}
