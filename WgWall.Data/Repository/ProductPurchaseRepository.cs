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
    public class ProductPurchaseRepository : SaveRepository<ProductPurchase>, IProductPurchaseRepository
    {
        public ProductPurchaseRepository(MyDbContext context) : base(context)
        {

        }
    }
}
