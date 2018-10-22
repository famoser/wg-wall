using WgWall.Api.Dto.Base;

namespace WgWall.Api.Dto
{
    public class ProductDto : BaseIdEntityDto
    {
        public string Name { get; set; }
        public int Amount { get; set; }
        public int? BoughtById { get; set; }
        public bool Hide { get; set; }
    }
}
