using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WgWall.Dto.Base;

namespace WgWall.Api.Dto
{
    public class TaskDto : BaseIdEntityDto
    {
        public DateTime ActivatedAt { get; set; }
        public int TaskTemplateId { get; set; }
    }
}
