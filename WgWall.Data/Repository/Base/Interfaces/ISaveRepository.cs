using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WgWall.Data.Model.Base;

namespace WgWall.Data.Repository.Base.Interfaces
{
    public interface ISaveRepository<in T>
        where T : BaseIdEntity
    {
        Task Save(T entity);
    }
}
