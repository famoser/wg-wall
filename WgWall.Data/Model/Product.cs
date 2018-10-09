using WgWall.Data.Model.Base;

namespace WgWall.Data.Model
{
    public class Product : BaseEntity
    {
        public string Text { get; set; }
        public int Amount { get; set; }
        public int BoughtById { get; set; }
        public virtual FrontendUser BoughtBy { get; set; }
    }
}
