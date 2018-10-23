using WgWall.Data.Model;
using WgWall.Data.Repository.Base;
using WgWall.Data.Repository.Interfaces;

namespace WgWall.Data.Repository
{
    public class EventRepository : CrudRepository<Event>, IEventRepository
    {
        public EventRepository(MyDbContext context) : base(context)
        {
        }
    }
}
