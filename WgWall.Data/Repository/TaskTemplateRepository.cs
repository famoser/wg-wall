using WgWall.Data.Model;
using WgWall.Data.Repository.Base;
using WgWall.Data.Repository.Interfaces;

namespace WgWall.Data.Repository
{
    public class TaskTemplateRepository : HideableCrudRepository<TaskTemplate>, ITaskTemplateRepository
    {
        public TaskTemplateRepository(MyDbContext context) : base(context)
        {
        }
    }
}
