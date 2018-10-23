using System.Collections.Generic;
using System.Threading.Tasks;
using WgWall.Data.Model.Base;

namespace WgWall.Data.Repository.Base.Interfaces
{
    public interface IGetRepository<T> : ITryFindRepository<T>
        where T : BaseEntity
    {
        Task<List<T>> GetAsync();
    }
}
