using System.Collections.Generic;
using System.Threading.Tasks;
using WgWall.Data.Model.Base;

namespace WgWall.Data.Repository.Base.Interfaces
{
    public interface IHideableCrudRepository<T> : ITryFindRepository<T>
        where T : BaseEntity
    {
        Task<List<T>> GetActiveAsync();
        Task HideAsync(T element);
    }
}
