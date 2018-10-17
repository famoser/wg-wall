using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WgWall.Data.Model;
using WgWall.Data.Repository.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace WgWall.Data.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly MyDbContext _context;

        public ProductRepository(MyDbContext context)
        {
            _context = context;
        }
        
        public async Task Update(int productId, string name, int amount, FrontendUser boughtBy)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
            if (product == null)
            {
                return;
            }

            product.Name = name;
            product.Amount = amount;
            product.BoughtBy = boughtBy;
            product.BoughtById = boughtBy?.Id;
            await _context.SaveChangesAsync();
        }

        public async Task<Product> Create(string name, FrontendUser frontendUser)
        {
            var product = Product.Create(name, frontendUser);
            product.Amount = 1;
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return product;
        }

        public Task<List<Product>> GetAllAsync()
        {
            return _context.Products.ToListAsync();
        }

        public async Task HideAll(string name)
        {
            var products = await _context.Products.Where(p => p.Name == name).ToListAsync();
            foreach (var product in products)
            {
                product.Hide = true;
            }

            await _context.SaveChangesAsync();
        }
    }
}
