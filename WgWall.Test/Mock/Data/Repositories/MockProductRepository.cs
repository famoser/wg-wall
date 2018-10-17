using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WgWall.Data.Model;
using WgWall.Data.Repository.Interfaces;
using Task = System.Threading.Tasks.Task;

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
namespace WgWall.Test.Mock.Data.Repositories
{
    class MockProductRepository : IProductRepository
    {
        private readonly List<Product> _testSet;

        public MockProductRepository(List<Product> testSet)
        {
            _testSet = testSet;
        }

        public async Task Update(int productId, string name, int amount, FrontendUser boughtBy)
        {
            var prod = _testSet.FirstOrDefault(p => p.Id == productId);
            if (prod == null)
            {
                return;
            }

            prod.Name = name;
            prod.Amount = amount;
            prod.BoughtBy = boughtBy;
            prod.BoughtById = boughtBy?.Id;
        }

        public async Task<Product> Create(string name, FrontendUser frontendUser)
        {
            var product = Product.Create(name, frontendUser);
            product.Id = _testSet.Max(p => p.Id) + 1;
            product.Amount = 1;
            _testSet.Add(product);

            return product;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return new List<Product>(_testSet);
        }

        public async Task HideAll(string name)
        {
            foreach (var product in _testSet.Where(t => t.Name == name))
            {
                product.Hide = true;
            }
        }
    }
}
