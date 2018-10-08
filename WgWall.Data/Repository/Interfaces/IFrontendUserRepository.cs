using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WgWall.Data.Model;

namespace WgWall.Data.Repositories.Interfaces
{
    public interface IFrontendUserRepository
    {
        Task<bool> CheckExistenceAsync(string name);
        Task<FrontendUser> CreateFrontendUserAsync(string name);
        Task<List<FrontendUser>> GetAllAsync();
    }
}
