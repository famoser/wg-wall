using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;
using WgWall.Api.Request;
using WgWall.Controllers;
using WgWall.Data.Model;
using WgWall.Data.Repository.Interfaces;
using WgWall.Dto;

namespace WgWall.Test.Controllers.ProductController
{
    public abstract class TestCollection
    {
        private ServiceProvider _serviceProvider;

        protected void SetServiceProvider(ServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        private WgWall.Controllers.ProductController GetController()
        {
            return new WgWall.Controllers.ProductController(_serviceProvider.GetService<IProductRepository>(), _serviceProvider.GetService<IFrontendUserRepository>());
        }

        private async Task<FrontendUserDto> GetActiveUser()
        {
            var controller = new WgWall.Controllers.FrontendUserController(_serviceProvider.GetService<IFrontendUserRepository>());
            var users = AssertHelper.AssertUsers(await controller.GetFrontendUsers());

            return users[0];
        }

        [TestMethod]
        public async Task Get_ShouldReturnProducts()
        {
            //arrange
            var controller = GetController();

            //act
            var result = await controller.GetProducts();

            //assert
            AssertProducts(result);
        }

        [TestMethod]
        public async Task PostProduct_ShouldAddToCollection()
        {
            //arrange
            var controller = GetController();
            var frontendUser = await GetActiveUser();

            //act
            var previousResult = await controller.GetProducts();
            var payload = new ProductPostPayload {Name = "fun", FrontendUserId = frontendUser.Id};
            var newProduct = await controller.PostProduct(payload);
            var result = await controller.GetProducts();

            //assert
            AssertNewProduct(newProduct, payload);
            var previousList = AssertProducts(previousResult);
            var list = AssertProducts(result);
            Assert.IsTrue(list.Count == previousList.Count + 1);
        }

        private ProductDto AssertNewProduct(IActionResult result, ProductPostPayload newProduct)
        {
            var objectResult = result as OkObjectResult;
            Assert.IsNotNull(objectResult);

            var product = objectResult.Value as ProductDto;
            Assert.IsNotNull(product);
            Assert.AreEqual(newProduct.Name, product.Name);
            Assert.IsTrue(product.Id > 0);
            Assert.IsTrue(product.Amount > 0);

            return product;
        }

        private IList<ProductDto> AssertProducts(IActionResult result)
        {
            var objectResult = result as OkObjectResult;
            Assert.IsNotNull(objectResult);

            var products = objectResult.Value as IList<ProductDto>;
            Assert.IsNotNull(products);

            return products;
        }
    }
}
