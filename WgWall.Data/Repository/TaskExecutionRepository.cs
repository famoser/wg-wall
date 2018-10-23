using WgWall.Data.Model;
using WgWall.Data.Repository.Base;
using WgWall.Data.Repository.Interfaces;

namespace WgWall.Data.Repository
{
    public class TaskExecutionRepository : SaveRepository<TaskExecution>, ITaskExecutionRepository
    {
        public TaskExecutionRepository(MyDbContext context) : base(context)
        {
        }
    }
}
