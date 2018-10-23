using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WgWall.Api.Dto.Base;

namespace WgWall.Api.Dto
{
    public class CalenderEntryDto : BaseDto
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
    }
}
