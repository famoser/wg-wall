using WgWall.Data.Model.Base;

namespace WgWall.Data.Model
{
    public class Setting : BaseEntity
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
