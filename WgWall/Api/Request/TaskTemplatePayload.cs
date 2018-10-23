using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WgWall.Api.Request.Base;
using WgWall.Api.Request.Interface;
using WgWall.Data.Model;

namespace WgWall.Api.Request
{
    public class TaskTemplatePayload : IWriteIntoPayload<TaskTemplate>
    {
        public string Name { get; set; }
        public int? IntervalInDays { get; set; }
        public void WriteInto(TaskTemplate entity)
        {
            Name = entity.Name;
            IntervalInDays = entity.IntervalInDays;
        }
    }
}
