using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WgWall.Data.Model;
using WgWall.Data.Repository.Interfaces;

namespace WgWall.Data.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly MyDbContext _context;

        public ProductRepository(MyDbContext context)
        {
            _context = context;
        }

        public Task<Product> TryGet(int productId)
        {
            return _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
        }

        public async Task Update(Product product)
        {
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
    }
}
