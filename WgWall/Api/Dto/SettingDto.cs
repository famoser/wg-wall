using WgWall.Api.Dto.Base;

namespace WgWall.Api.Dto
{
    public class SettingDto : BaseDto
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
