using WgWall.Api.Dto.Base;

namespace WgWall.Api.Dto
{
    public class ProductDto : BaseDto
    {
        public string Name { get; set; }
        public int Amount { get; set; }
    }
}
