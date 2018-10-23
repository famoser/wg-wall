using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WgWall.Data.Model.Base;
using WgWall.Data.Repository.Base.Interfaces;

namespace WgWall.Data.Repository.Base
{
    public abstract class GetRepository<T> : TryFindRepository<T>, IGetRepository<T>
    where T : BaseEntity
    {
        protected GetRepository(MyDbContext context) : base(context)
        {
        }

        public Task<List<T>> GetAsync()
        {
            return Context.Set<T>().ToListAsync();
        }
    }
}
