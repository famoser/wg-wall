using System.Collections.Generic;
using System.Threading.Tasks;
using WgWall.Data.Model;

namespace WgWall.Data.Repository.Interfaces
{
    public interface IFrontendUserRepository
    {
        Task<FrontendUser> TryGet(int frontendUserId);
        Task<FrontendUser> CreateAsync(string name);
        Task<List<FrontendUser>> GetAllAsync();
    }
}
