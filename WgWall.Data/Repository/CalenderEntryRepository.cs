using WgWall.Data.Model;
using WgWall.Data.Repository.Base;
using WgWall.Data.Repository.Interfaces;

namespace WgWall.Data.Repository
{
    public class CalenderEntryRepository : CrudRepository<CalenderEntry>, ICalenderEntryRepository
    {
        public CalenderEntryRepository(MyDbContext context) : base(context)
        {
        }
    }
}
