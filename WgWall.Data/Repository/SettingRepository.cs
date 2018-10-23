using WgWall.Data.Model;
using WgWall.Data.Repository.Base;
using WgWall.Data.Repository.Interfaces;

namespace WgWall.Data.Repository
{
    public class SettingRepository : GetRepository<Setting>, ISettingRepository
    {
        public SettingRepository(MyDbContext context) : base(context)
        {
        }
    }
}
