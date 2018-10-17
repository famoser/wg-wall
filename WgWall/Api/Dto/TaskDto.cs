using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WgWall.Api.Dto
{
    public class TaskDto
    {
        public DateTime ActivatedAt { get; set; }
        public int TaskTemplateId { get; set; }
    }
}
