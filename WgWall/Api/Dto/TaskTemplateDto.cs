using System;
using System.Collections.Generic;

namespace WgWall.Api.Dto
{
    public class TaskTemplateDto
    {
        public string Name { get; set; }
        public int? IntervalInDays { get; set; }
        public DateTime? LastExecutionAt { get; set; }
        public bool Hidden { get; set; }
    }
}
