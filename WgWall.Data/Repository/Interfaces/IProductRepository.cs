using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WgWall.Data.Model;
using Task = System.Threading.Tasks.Task;

namespace WgWall.Data.Repository.Interfaces
{
    public interface IProductRepository
    {
        Task Update(int productId, string name, int amount, FrontendUser boughtBy);
        Task<Product> Create(string name, FrontendUser frontendUser);
        Task<List<Product>> GetAllAsync();
        Task HideAll(string name);
    }
}
