using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WgWall.Data.Model;
using Task = System.Threading.Tasks.Task;

namespace WgWall.Data.Repository.Interfaces
{
    public interface ITaskTemplateRepository
    {
        Task Update(int taskTemplateId, string name, int? intervalInDays);
        Task<TaskTemplate> Create(string name, int? intervalInDays, FrontendUser frontendUser);
        Task<List<TaskTemplate>> GetAllAsync();
        Task Hide(int taskTemplateId);
    }
}
