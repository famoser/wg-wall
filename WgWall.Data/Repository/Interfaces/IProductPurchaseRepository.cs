using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WgWall.Data.Model;
using WgWall.Data.Repository.Base.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace WgWall.Data.Repository.Interfaces
{
    public interface IProductPurchaseRepository : ISaveRepository<TaskExecution>
    {
    }
}
