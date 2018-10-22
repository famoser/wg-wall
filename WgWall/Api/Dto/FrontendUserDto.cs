using WgWall.Api.Dto.Base;

namespace WgWall.Api.Dto
{
    public class FrontendUserDto : BaseIdEntityDto
    {
        public string Name { get; set; }
        public int Karma { get; set; }
        public string ProfileImageSrc { get; set; }
    }
}
