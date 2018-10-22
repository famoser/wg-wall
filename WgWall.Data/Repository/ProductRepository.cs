using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WgWall.Data.Model;
using WgWall.Data.Repository.Base;
using WgWall.Data.Repository.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace WgWall.Data.Repository
{
    public class ProductRepository :  GetAllRepository<Product>, IProductRepository
    {
        private readonly MyDbContext _context;

        public ProductRepository(MyDbContext context)
        {
            _context = context;
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
