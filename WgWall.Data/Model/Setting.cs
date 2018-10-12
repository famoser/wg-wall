using WgWall.Data.Model.Base;

namespace WgWall.Data.Model
{
    public class Setting : BaseIdEntity
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
