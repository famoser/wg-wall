using System.Threading.Tasks;
using WgWall.Data.Model.Base;

namespace WgWall.Data.Repository.Base.Interfaces
{
    public interface ISaveRepository<in T>
        where T : BaseEntity
    {
        Task Save(T entity);
    }
}
