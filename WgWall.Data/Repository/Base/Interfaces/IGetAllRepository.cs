using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WgWall.Data.Model.Base;

namespace WgWall.Data.Repository.Base.Interfaces
{
    public interface IGetAllRepository<T> : ITryGetRepository<T>
        where T : BaseEntity
    {
        Task<List<T>> GetAllAsync();
    }
}
