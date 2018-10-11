﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IFrontendUserRepository _frontendUserRepository;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository productRepository, IFrontendUserRepository frontendUserRepository)
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

            await _productRepository.Update(product);
            
            return NoContent();
        }

        // POST: api/Products
        [HttpPost]
        public async Task<IActionResult> PostProduct([FromBody] ProductPostPayload payload)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var product = await _productRepository.Create(payload.Name, await _frontendUserRepository.TryGet(payload.FrontendUserId));
            var productDto = _mapper.Map<ProductDto>(product);
            return Ok(productDto);
        }
    }
}