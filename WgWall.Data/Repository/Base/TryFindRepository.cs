using System.Threading.Tasks;
using WgWall.Data.Model.Base;
using WgWall.Data.Repository.Base.Interfaces;

namespace WgWall.Data.Repository.Base
{
    public abstract class TryFindRepository<T> : SaveRepository<T>, ITryFindRepository<T>
    where T : BaseEntity
    {
        protected TryFindRepository(MyDbContext context) : base(context)
        {
        }

        public Task<T> TryFindAsync(int entityId)
        {
            return Context.FindAsync<T>(entityId);
        }
    }
}
