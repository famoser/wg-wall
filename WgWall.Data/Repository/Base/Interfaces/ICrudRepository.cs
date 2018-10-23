using System.Collections.Generic;
using System.Threading.Tasks;
using WgWall.Data.Model.Base;

namespace WgWall.Data.Repository.Base.Interfaces
{
    public interface ICrudRepository<T> : IGetRepository<T>
        where T : BaseEntity
    {
        Task RemoveAsync(T element);
    }
}
