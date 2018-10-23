using System.Collections.Generic;
using System.Threading.Tasks;
using WgWall.Data.Model.Base;

namespace WgWall.Data.Repository.Base.Interfaces
{
    public interface ICrudRepository<T> : ITryGetRepository<T>
        where T : BaseEntity
    {
        Task<List<T>> GetAsync();
        Task RemoveAsync(T element);
    }
}
