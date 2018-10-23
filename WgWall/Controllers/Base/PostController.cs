using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WgWall.Api.Dto;
using WgWall.Api.Dto.Base;
using WgWall.Api.Request.Interface;
using WgWall.Data.Model.Base;
using WgWall.Data.Repository.Base.Interfaces;

namespace WgWall.Controllers.Base
{
    public abstract class PostController<TEntity, TDto, TPayload> : ControllerBase
    where TEntity : BaseEntity, new()
    where TDto : BaseDto
    where TPayload : IWriteIntoPayload<TEntity>
    {
        private readonly ISaveRepository<TEntity> _entityRepository;
        protected readonly IMapper _mapper;

        protected PostController(IHideableCrudRepository<TEntity> entityRepository)
        {
            _entityRepository = entityRepository;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TEntity, TDto>());
            _mapper = new Mapper(config);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TPayload payload)
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
