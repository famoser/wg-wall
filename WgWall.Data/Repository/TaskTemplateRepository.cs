using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WgWall.Data.Model;
using WgWall.Data.Repository.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace WgWall.Data.Repository
{
    public class TaskTemplateRepository : ITaskTemplateRepository
    {
        private readonly MyDbContext _context;

        public TaskTemplateRepository(MyDbContext context)
        {
            _context = context;
        }

        public async Task Update(int taskTemplateId, string name, int? intervalInDays)
        {
            var task = await _context.TaskTemplates.FirstOrDefaultAsync(p => p.Id == taskTemplateId);
            if (task == null)
            {
                return;
            }

            task.Name = name;
            task.IntervalInDays = intervalInDays == 0 || intervalInDays == null ? null : intervalInDays;
            await _context.SaveChangesAsync();
        }

        public Task Update(TaskTemplate taskTemplate)
        {
            return _context.SaveChangesAsync();
        }

        public async Task<TaskTemplate> Create(string name, int? intervalInDays, FrontendUser frontendUser)
        {
            var taskTemplate = TaskTemplate.Create(name, intervalInDays, frontendUser);

            _context.TaskTemplates.Add(taskTemplate);
            await _context.SaveChangesAsync();

            return taskTemplate;
        }

        public Task<List<TaskTemplate>> GetAllAsync()
        {
            return _context.TaskTemplates.ToListAsync();
        }

        public async Task Hide(int taskTemplateId)
        {
            var task = await _context.TaskTemplates.FirstOrDefaultAsync(p => p.Id == taskTemplateId);
            if (task == null)
            {
                return;
            }

            task.Hide = true;
            await _context.SaveChangesAsync();
        }

        public Task<TaskTemplate> TryGet(int templateId)
        {
            return _context.TaskTemplates.FirstOrDefaultAsync(t => t.Id == templateId);
        }
    }
}
