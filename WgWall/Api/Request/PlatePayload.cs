using WgWall.Api.Request.Base;

namespace WgWall.Api.Request
{
    public class PlatePayload : AccountablePayload
    {
        public int DinnerState { get; set; }
    }
}
