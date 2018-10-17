using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WgWall.Api.Dto
{
    public class TaskTemplateDto
    {
        public string Name { get; set; }
        public int? IntervalInDays { get; set; }
        public DateTime? LastActivationAt { get; set; }
        public bool Hide { get; set; }
    }
}
