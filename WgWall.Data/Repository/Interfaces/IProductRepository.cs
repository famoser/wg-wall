using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WgWall.Data.Model;

namespace WgWall.Data.Repository.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> TryGet(int productId);
        Task Update(Product product);
        Task<Product> Create(string name, FrontendUser frontendUser);
        Task<List<Product>> GetAllAsync();
    }
}
