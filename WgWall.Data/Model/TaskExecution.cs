using System;
using WgWall.Data.Model.Base;

namespace WgWall.Data.Model
{
    public class TaskExecution : BaseEntity
    {
        public DateTime ExecutedAt { get; set; }

        public int TaskTemplateId { get; set; }
        public virtual TaskTemplate TaskTemplate { get; set; }

        public int? DoneById { get; set; }
        public virtual FrontendUser DoneBy { get; set; }

        public static TaskExecution Create(TaskTemplate taskTemplate, FrontendUser createdBy)
        {
            var element = new TaskExecution  { TaskTemplate = taskTemplate, TaskTemplateId = taskTemplate.Id, ExecutedAt = DateTime.Now};
            RegisterCreation(element, createdBy);
            return element;
        }
    }
}
