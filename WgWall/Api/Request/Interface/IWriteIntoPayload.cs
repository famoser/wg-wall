using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WgWall.Api.Request.Interface
{
    public interface IWriteIntoPayload<in T>
    {
        void WriteInto(T entity);
    }
}
