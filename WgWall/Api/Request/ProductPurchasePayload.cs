using WgWall.Api.Request.Base;

namespace WgWall.Api.Request
{
    public class ProductPurchasePayload : AccountablePayload
    {
        public int ProductId { get; set; }
    }
}
