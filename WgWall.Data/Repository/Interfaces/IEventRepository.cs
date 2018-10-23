using System;
using System.Collections.Generic;
using System.Text;
using WgWall.Data.Model;
using WgWall.Data.Repository.Base.Interfaces;

namespace WgWall.Data.Repository.Interfaces
{
    public interface IEventRepository : ICrudRepository<Event>
    {
    }
}
