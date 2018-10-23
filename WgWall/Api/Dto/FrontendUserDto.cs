using WgWall.Api.Dto.Base;

namespace WgWall.Api.Dto
{
    public class FrontendUserDto : BaseDto
    {
        public string Name { get; set; }
        public int Karma { get; set; }
    }
}
