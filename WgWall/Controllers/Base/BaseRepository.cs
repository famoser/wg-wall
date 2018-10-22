using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WgWall.Api.Dto;
using WgWall.Api.Request;
using WgWall.Api.Request.Interface;
using WgWall.Data.Model;
using WgWall.Data.Model.Base;
using WgWall.Data.Repository.Base.Interfaces;
using WgWall.Data.Repository.Interfaces;

namespace WgWall.Controllers.Base
{
    public abstract class CrudController<TEntity, TPayload, TDto> : ControllerBase
    where TEntity: BaseEntity, new()
    where TPayload: IWriteIntoPayload<TEntity>
    {
        private readonly IGetAllRepository<TEntity> _entityRepository;
        private readonly IMapper _mapper;

        protected CrudController(IGetAllRepository<TEntity> entityRepository)
        {
            _entityRepository = entityRepository;
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TEntity, TDto>());
            _mapper = new Mapper(config);
        }

        // GET: api/Products
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _entityRepository.GetAllAsync();

            var productsDto = _mapper.Map<IList<ProductDto>>(products);
            return Ok(productsDto);
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct([FromRoute] int id, [FromBody] TPayload payload)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var taskTemplate = await _entityRepository.TryGetAsync(id);
            if (taskTemplate == null) return NotFound();

            payload.WriteInto(taskTemplate);
            await _entityRepository.Save(taskTemplate);

            return NoContent();
        }

        // POST: api/Products
        [HttpPost]
        public async Task<IActionResult> PostProduct([FromBody] TPayload payload)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var product = new TEntity();
            payload.WriteInto(product);
            await _entityRepository.Save(product);

            var productDto = _mapper.Map<ProductDto>(product);
            return Ok(productDto);
        }
    }
}
