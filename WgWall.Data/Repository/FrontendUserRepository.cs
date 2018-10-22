using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WgWall.Data.Model;
using WgWall.Data.Repository.Base;
using WgWall.Data.Repository.Interfaces;

namespace WgWall.Data.Repository
{
    public class FrontendUserRepository : TryGetRepository<FrontendUser>, IFrontendUserRepository
    {
        public FrontendUserRepository(MyDbContext context) : base(context)
        {
        }
    }
}
