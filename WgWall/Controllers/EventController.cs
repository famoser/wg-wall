using Microsoft.AspNetCore.Mvc;
using WgWall.Api.Dto;
using WgWall.Api.Request;
using WgWall.Controllers.Base;
using WgWall.Data.Model;
using WgWall.Data.Repository.Interfaces;

namespace WgWall.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : CrudController<Event, CalenderEntryDto, CalenderEntryPayload>
    {
        public EventController(IEventRepository entityRepository) : base(entityRepository)
        {
        }

        protected override bool WriteInto(Event target, CalenderEntryPayload source)
        {
            target.Name = source.Name;
            target.StartDate = source.StartDate;

            return true;
        }
    }
}