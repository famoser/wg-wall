using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WgWall.Data.Model.Base;
using WgWall.Data.Repository.Base.Interfaces;

namespace WgWall.Data.Repository.Base
{
    public abstract class HideableCrudRepository<T> : TryFindRepository<T>, IHideableCrudRepository<T>
    where T : HideableEntity
    {
        protected HideableCrudRepository(MyDbContext context) : base(context)
        {
        }

        public Task<List<T>> GetActiveAsync()
        {
            return Context.Query<T>().Where(t => !t.IsHidden).ToListAsync();
        }

        public Task HideAsync(T element)
        {
            element.IsHidden = true;
            return Context.SaveChangesAsync();
        }
    }
}
