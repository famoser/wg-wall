using WgWall.Api.Dto.Base;

namespace WgWall.Api.Dto
{
    public class PlateDto : BaseDto
    {
        public int FrontendUserId { get; set; }
        public int DinnerState { get; set; }
    }
}
