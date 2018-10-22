using WgWall.Data.Model.Base;

namespace WgWall.Data.Model
{
    public class Product : HideableEntity
    {
        public string Name { get; set; }
        public int Amount { get; set; }
        public virtual FrontendUser BoughtBy { get; set; }
    }
}
