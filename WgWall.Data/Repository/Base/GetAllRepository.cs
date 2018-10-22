using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WgWall.Data.Model.Base;
using WgWall.Data.Repository.Base.Interfaces;

namespace WgWall.Data.Repository.Base
{
    public abstract class GetAllRepository<T> : TryGetRepository<T>, IGetAllRepository<T>
    where T : BaseIdEntity
    {
        protected GetAllRepository(MyDbContext context) : base(context)
        {
        }

        public Task<List<T>> GetAllAsync()
        {
            return Context.Set<T>().ToListAsync();
        }
    }
}
