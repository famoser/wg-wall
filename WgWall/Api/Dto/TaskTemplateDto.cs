using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WgWall.Api.Dto.Base;

namespace WgWall.Api.Dto
{
    public class TaskTemplateDto : BaseIdEntityDto
    {
        public string Name { get; set; }
        public int? IntervalInDays { get; set; }
        public DateTime? LastActivationAt { get; set; }
        public bool Hide { get; set; }
    }
}
