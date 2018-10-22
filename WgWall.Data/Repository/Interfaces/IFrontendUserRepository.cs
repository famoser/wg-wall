using System.Collections.Generic;
using System.Threading.Tasks;
using WgWall.Data.Model;
using WgWall.Data.Repository.Base.Interfaces;

namespace WgWall.Data.Repository.Interfaces
{
    public interface IFrontendUserRepository : ITryGetRepository<FrontendUser>
    {
    }
}
