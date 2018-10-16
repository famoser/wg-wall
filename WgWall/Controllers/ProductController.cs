using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Castle.Core.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WgWall.Api.Request;
using WgWall.Data;
using WgWall.Data.Model;
using WgWall.Data.Repository.Interfaces;
using WgWall.Dto;

namespace WgWall.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IFrontendUserRepository _frontendUserRepository;
        private readonly IMapper _mapper;

        public ProductController(IProductRepository productRepository, IFrontendUserRepository frontendUserRepository)
        {
            _productRepository = productRepository;
            _frontendUserRepository = frontendUserRepository;
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductDto>());
            _mapper = new Mapper(config);
        }

        // GET: api/Products
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productRepository.GetAllAsync();

            var productsDto = _mapper.Map<IList<ProductDto>>(products);
            return Ok(productsDto);
        }

        [HttpGet("hide/{name}")]
        public async Task<IActionResult> HideAll([FromRoute] string name)
        {
            await _productRepository.HideAll(name);
            return NoContent();
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct([FromRoute] int id, [FromBody] ProductPutPayload payload)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await _productRepository.TryGet(id);
            product.Name = payload.Name;
            product.Amount = payload.Amount;
            product.BoughtBy = await _frontendUserRepository.TryGet(payload.BoughtBy);
            product.BoughtById = product.BoughtBy?.Id;

            await _productRepository.Update(product);
            
            return NoContent();
        }

        // POST: api/Products
        [HttpPost]
        public async Task<IActionResult> PostProduct([FromBody] ProductPostPayload payload)
        {
            if (!ModelState.IsValid || payload.Name.IsNullOrEmpty())
            {
                return BadRequest(ModelState);
            }
            

            var product = await _productRepository.Create(payload.Name, await _frontendUserRepository.TryGet(payload.FrontendUserId));
            var productDto = _mapper.Map<ProductDto>(product);
            return Ok(productDto);
        }
    }
}