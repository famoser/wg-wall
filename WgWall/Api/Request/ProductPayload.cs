using WgWall.Api.Request.Interface;
using WgWall.Data.Model;

namespace WgWall.Api.Request
{
    public class ProductPayload : IWriteIntoPayload<Product>
    {
        public string Name { get; set; }
        public int Amount { get; set; }
        public bool IsHidden { get; set; }

        public void WriteInto(Product entity)
        {
            entity.Name = Name;
            entity.Amount = Amount;
            entity.IsHidden = IsHidden;
        }
    }
}
