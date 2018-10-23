using WgWall.Data.Model;
using WgWall.Data.Repository.Base;
using WgWall.Data.Repository.Interfaces;

namespace WgWall.Data.Repository
{
    public class FrontendUserRepository : CrudRepository<FrontendUser>, IFrontendUserRepository
    {
        public FrontendUserRepository(MyDbContext context) : base(context)
        {
        }
    }
}
