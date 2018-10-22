using System;
using WgWall.Data.Model.Base;

namespace WgWall.Data.Model
{
    public class TaskTemplate : HideableEntity
    {
        public string Name { get; set; }
        public int? IntervalInDays { get; set; }
        public DateTime? LastExecutionAt { get; set; }
    }
}
