using System.Collections.Generic;
using System.Threading.Tasks;
using WgWall.Data.Model;

namespace WgWall.Data.Repository.Interfaces
{
    public interface ISettingRepository
    {
        Task Persist(string key, string value);
        Task<List<Setting>> GetAllAsync();
    }
}
