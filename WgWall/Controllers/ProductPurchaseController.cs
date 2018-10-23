using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WgWall.Api.Dto.Base;
using WgWall.Api.Request;
using WgWall.Controllers.Base;
using WgWall.Data.Model;
using WgWall.Data.Repository.Interfaces;

namespace WgWall.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductPurchaseController : PostController<ProductPurchase, BaseDto, ProductPurchasePayload>
    {
        private readonly IFrontendUserRepository _frontendUserRepository;
        private readonly IProductRepository _productRepository;

        public ProductPurchaseController(IProductPurchaseRepository entityRepository, IFrontendUserRepository frontendUserRepository, IProductRepository productRepository) : base(entityRepository)
        {
            _frontendUserRepository = frontendUserRepository;
            _productRepository = productRepository;
        }

        protected override async Task<bool> WriteIntoAsync(ProductPurchase target, ProductPurchasePayload source)
        {
            var frontendUser = await _frontendUserRepository.TryFindAsync(source.FrontendUserId);
            var product = await _productRepository.TryFindAsync(source.ProductId);

            if (frontendUser == null || product == null)
            {
                return false;
            }

            target.Accountable = frontendUser;
            target.Entity = product;

            return true;
        }
    }
}