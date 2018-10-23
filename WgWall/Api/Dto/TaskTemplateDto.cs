using System;
using WgWall.Api.Dto.Base;

namespace WgWall.Api.Dto
{
    public class TaskTemplateDto : BaseDto
    {
        public string Name { get; set; }
        public int? IntervalInDays { get; set; }
        public DateTime? LastExecutionAt { get; set; }
        public int Reward { get; set; }
    }
}
