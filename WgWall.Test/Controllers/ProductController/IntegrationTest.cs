﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using WgWall.Api.Request;
using WgWall.Dto;

namespace WgWall.Test.Controllers.ProductController
{
    [TestClass]
    public class IntegrationTest
    {
        [TestMethod]
        public async Task Get_ShouldReturnExpectedFields()
        {
            var expectedProductFields = new[] {"name", "id", "amount", "boughtById", "hide"};
            var newProduct = new ProductPostPayload() { Name = "new product name"};
            using (var client = new TestClientProvider())
            {
                //creation
                var newProductResponse = await client.PostJsonAsync("/api/Product", newProduct);
                AssertHelper.AssertFields(newProductResponse as JObject, expectedProductFields);
                
                //list
                var response = await client.GetJsonAsync("/api/Product");
                Assert.IsInstanceOfType(response, typeof(JArray));
                AssertHelper.AssertFields(((JArray)response)[0] as JObject, expectedProductFields);
            }
        }
    }
}
