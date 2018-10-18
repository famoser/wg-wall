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
    public class TaskRepository : ITaskRepository
    {
        private readonly MyDbContext _context;

        public TaskRepository(MyDbContext context)
        {
            _context = context;
        }

        public async Task<Model.Task> Create(TaskTemplate taskTemplate, FrontendUser frontendUser)
        {
            var task = Model.Task.Create(taskTemplate, frontendUser);

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public Task<List<Model.Task>> GetActiveAsync()
        {
            return _context.Tasks.Where(t => t.DoneBy == null).ToListAsync();
        }

        public async Task Done(int taskId, FrontendUser frontendUser)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == taskId);
            if (task == null) return;

            task.DoneBy = frontendUser;
            task.DoneById = frontendUser.Id;

            await _context.SaveChangesAsync();
        }

        public async Task Remove(int taskId)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == taskId);
            if (task == null) return;

            _context.Remove(task);

            await _context.SaveChangesAsync();
        }
    }
}
