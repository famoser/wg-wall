using System;
using WgWall.Data.Model.Base;

namespace WgWall.Data.Model
{
    public class TaskTemplate : BaseEntity
    {
        public string Name { get; set; }
        public int? IntervalInDays { get; set; }
        public DateTime? LastActivationAt { get; set; }
        public bool Hide { get; set; }

        public static TaskTemplate Create(string name, int? intervalInDays, FrontendUser createdBy)
        {
            var element = new TaskTemplate { Name = name, IntervalInDays = intervalInDays};
            RegisterCreation(element, createdBy);
            return element;
        }
    }
}
