using System;
using WgWall.Api.Dto.Base;

namespace WgWall.Api.Dto
{
    public class EventDto : BaseDto
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
    }
}
