using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WgWall.Data.Model;
using WgWall.Data.Repository.Base;
using WgWall.Data.Repository.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace WgWall.Data.Repository
{
    public class SettingRepository : GetAllRepository<Setting>, ISettingRepository
    {
        public SettingRepository(MyDbContext context) : base(context)
        {
        }
    }
}
