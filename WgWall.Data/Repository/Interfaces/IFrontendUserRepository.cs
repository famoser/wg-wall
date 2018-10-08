using System.Collections.Generic;
using System.Threading.Tasks;
using WgWall.Data.Model;

namespace WgWall.Data.Repository.Interfaces
{
    public interface IFrontendUserRepository
    {
        Task<bool> CheckExistenceAsync(string name);
        Task<FrontendUser> CreateFrontendUserAsync(string name);
        Task<List<FrontendUser>> GetAllAsync();
    }
}
