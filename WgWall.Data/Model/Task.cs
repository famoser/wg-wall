using System;
using WgWall.Data.Model.Base;

namespace WgWall.Data.Model
{
    public class Task : BaseEntity
    {
        public DateTime ActivatedAt { get; set; }

        public int TaskTemplateId { get; set; }
        public virtual TaskTemplate TaskTemplate { get; set; }

        public int? DoneById { get; set; }
        public virtual FrontendUser DoneBy { get; set; }

        public static Task Create(TaskTemplate taskTemplate, FrontendUser createdBy)
        {
            var element = new Task  { TaskTemplate = taskTemplate, TaskTemplateId = taskTemplate.Id, ActivatedAt = DateTime.Now};
            RegisterCreation(element, createdBy);
            return element;
        }
    }
}
