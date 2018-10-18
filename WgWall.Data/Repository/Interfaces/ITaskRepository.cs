using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WgWall.Data.Model;
using Task = System.Threading.Tasks.Task;

namespace WgWall.Data.Repository.Interfaces
{
    public interface ITaskRepository
    {
        Task<Model.Task> Create(TaskTemplate taskTemplate, FrontendUser frontendUser);
        Task<List<Model.Task>> GetActiveAsync();
        Task Done(int taskId, FrontendUser frontendUser);
        Task Remove(int taskId);
    }
}
