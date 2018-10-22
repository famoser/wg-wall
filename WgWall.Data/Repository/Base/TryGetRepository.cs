using System.Threading.Tasks;
using WgWall.Data.Model.Base;
using WgWall.Data.Repository.Base.Interfaces;

namespace WgWall.Data.Repository.Base
{
    public abstract class TryGetRepository<T> : SaveRepository<T>, ITryGetRepository<T>
    where T : BaseIdEntity
    {
        protected TryGetRepository(MyDbContext context) : base(context)
        {
        }

        public Task<T> TryGetAsync(int entityId)
        {
            return Context.FindAsync<T>(entityId);
        }
    }
}
