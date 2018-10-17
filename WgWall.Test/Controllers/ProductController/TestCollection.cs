using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WgWall.Api.Request;
using WgWall.Controllers;
using WgWall.Data.Model;
using WgWall.Data.Repository.Interfaces;
using WgWall.Dto;
using Task = System.Threading.Tasks.Task;

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
            var payload = new ProductPostPayload { Name = "fun", FrontendUserId = frontendUser.Id };
            var newProduct = await controller.PostProduct(payload);
            var result = await controller.GetProducts();

            //assert
            AssertNewProduct(newProduct, payload);
            var previousList = AssertProducts(previousResult);
            var list = AssertProducts(result);
            Assert.IsTrue(list.Count == previousList.Count + 1);
        }

        [TestMethod]
        public async Task PutProduct_ShouldSaveChanges()
        {
            //arrange
            var controller = GetController();
            var frontendUser = await GetActiveUser();

            //act
            var previousResult = await controller.GetProducts();
            var previousList = AssertProducts(previousResult);
            var prod = previousList[0];

            var payload = new ProductPutPayload() { Name = "newName", Amount = 100, BoughtBy = frontendUser.Id };
            await controller.PutProduct(prod.Id, payload);

            var result = await controller.GetProducts();
            var list = AssertProducts(result);
            var newProd = list.FirstOrDefault(p => p.Id == prod.Id);

            //assert
            Assert.IsNotNull(newProd);
            Assert.AreEqual(payload.Name, newProd.Name);
            Assert.AreEqual(payload.Amount, newProd.Amount);
            Assert.AreEqual(payload.BoughtBy, newProd.BoughtById);
            Assert.IsTrue(list.Count == previousList.Count);
        }

        [TestMethod]
        public async Task HideAll_ShouldSaveChanges()
        {
            //arrange
            var controller = GetController();
            var frontendUser = await GetActiveUser();

            //act
            var previousResult = await controller.GetProducts();
            var previousList = AssertProducts(previousResult);
            var prod = previousList[0];

            await controller.HideAll(prod.Name);

            var result = await controller.GetProducts();
            var list = AssertProducts(result);
            var hiddenProducts = list.Where(p => p.Name == prod.Name).ToList();

            //assert
            Assert.IsTrue(hiddenProducts.Count(p => p.Hide) == hiddenProducts.Count());
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
