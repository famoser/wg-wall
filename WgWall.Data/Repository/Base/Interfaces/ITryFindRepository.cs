using System.Threading.Tasks;
using WgWall.Data.Model.Base;

namespace WgWall.Data.Repository.Base.Interfaces
{
    public interface ITryFindRepository<T> : ISaveRepository<T>
        where T : BaseEntity
    {
        Task<T> TryFindAsync(int entityId);
    }
}
