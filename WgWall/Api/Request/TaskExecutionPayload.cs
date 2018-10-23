using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WgWall.Api.Request.Base;

namespace WgWall.Api.Request
{
    public class TaskExecutionPayload : AccountablePayload
    {
        public int TaskTemplateId { get; set; }
    }
}
