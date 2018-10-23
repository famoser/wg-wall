using WgWall.Data.Model;
using WgWall.Data.Repository.Base;
using WgWall.Data.Repository.Interfaces;

namespace WgWall.Data.Repository
{
    public class ProductRepository : HideableCrudRepository<Product>, IProductRepository
    {
        public ProductRepository(MyDbContext context) : base(context)
        {
        }
    }
}
