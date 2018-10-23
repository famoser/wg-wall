using Microsoft.AspNetCore.Mvc;
using WgWall.Api.Dto;
using WgWall.Api.Request;
using WgWall.Controllers.Base;
using WgWall.Data.Model;
using WgWall.Data.Repository.Interfaces;

namespace WgWall.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : HideableCrudController<Product, ProductDto, ProductPayload>
    {
        public ProductController(IProductRepository entityRepository) : base(entityRepository)
        {
        }

        protected override bool WriteInto(Product target, ProductPayload source)
        {
            target.Name = source.Name;
            target.Amount = source.Amount;

            return true;
        }
    }
}