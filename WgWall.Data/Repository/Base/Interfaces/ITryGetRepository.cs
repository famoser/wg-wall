using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WgWall.Data.Model.Base;

namespace WgWall.Data.Repository.Base.Interfaces
{
    public interface ITryGetRepository<T> : ISaveRepository<T>
        where T : BaseIdEntity
    {
        Task<T> TryGetAsync(int entityId);
    }
}
