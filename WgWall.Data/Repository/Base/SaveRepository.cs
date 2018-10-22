using System.Threading.Tasks;
using WgWall.Data.Model.Base;
using WgWall.Data.Repository.Base.Interfaces;

namespace WgWall.Data.Repository.Base
{
    public abstract class SaveRepository<T> : ISaveRepository<T>
    where T : BaseEntity
    {
        protected readonly MyDbContext Context;

        public SaveRepository(MyDbContext context)
        {
            Context = context;
        }


        public async Task Save(T entity)
        {
            Context.Attach(entity);
            await Context.SaveChangesAsync();
        }
    }
}
