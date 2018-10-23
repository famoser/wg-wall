using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WgWall.Data.Model.Base;
using WgWall.Data.Repository.Base.Interfaces;

namespace WgWall.Data.Repository.Base
{
    public abstract class CrudRepository<T> : GetRepository<T>, ICrudRepository<T>
    where T : BaseEntity
    {
        protected CrudRepository(MyDbContext context) : base(context)
        {
        }

        public Task RemoveAsync(T element)
        {
            Context.Remove(element);
            return Context.SaveChangesAsync();
        }
    }
}
