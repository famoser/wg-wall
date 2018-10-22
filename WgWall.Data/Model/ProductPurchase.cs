using WgWall.Data.Model.Base;

namespace WgWall.Data.Model
{
    public class ProductPurchase : BaseEntity
    {
        public int? ProductId { get; set; }
        public virtual Product Product { get; set; }

        public int? BoughtById { get; set; }
        public virtual FrontendUser BoughtBy { get; set; }

        public static Product Create(string name, FrontendUser createdBy = null)
        {
            var element = new Product { Name = name };
            RegisterCreation(element, createdBy);
            return element;
        }
    }
}
