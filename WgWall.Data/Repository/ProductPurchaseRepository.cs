using WgWall.Data.Model;
using WgWall.Data.Repository.Base;
using WgWall.Data.Repository.Interfaces;

namespace WgWall.Data.Repository
{
    public class ProductPurchaseRepository : SaveRepository<ProductPurchase>, IProductPurchaseRepository
    {
        public ProductPurchaseRepository(MyDbContext context) : base(context)
        {

        }
    }
}
