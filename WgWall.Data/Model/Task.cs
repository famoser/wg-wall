using System;
using WgWall.Data.Model.Base;

namespace WgWall.Data.Model
{
    public class Task : BaseEntity
    {
        public DateTime ActivatedAt { get; set; }

        public int TaskTemplateId { get; set; }
        public TaskTemplate TaskTemplate { get; set; }

        public int? DoneById { get; set; }
        public FrontendUser DoneBy { get; set; }

        public static Task Create(TaskTemplate taskTemplate, FrontendUser createdBy)
        {
            var element = new Task  { TaskTemplate = taskTemplate };
            RegisterCreation(element, createdBy);
            return element;
        }
    }
}
