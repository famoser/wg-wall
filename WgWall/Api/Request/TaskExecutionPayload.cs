using WgWall.Api.Request.Base;

namespace WgWall.Api.Request
{
    public class TaskExecutionPayload : AccountablePayload
    {
        public int TaskTemplateId { get; set; }
    }
}
