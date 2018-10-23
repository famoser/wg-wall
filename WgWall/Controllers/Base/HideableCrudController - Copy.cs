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
    public abstract class CrudController<TEntity, TDto, TPayload> : PostController<TEntity, TDto, TPayload>
    where TEntity : BaseEntity, new()
    where TDto : BaseDto
    where TPayload : IWriteIntoPayload<TEntity>
    {
        private readonly IHideableCrudRepository<TEntity> _entityRepository;

        protected CrudController(IHideableCrudRepository<TEntity> entityRepository) : base(entityRepository)
        {
            _entityRepository = entityRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await _entityRepository.GetActiveAsync();

            var productsDto = _mapper.Map<IList<ProductDto>>(products);
            return Ok(productsDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] TPayload payload)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var taskTemplate = await _entityRepository.TryFindAsync(id);
            if (taskTemplate == null) return NotFound();

            payload.WriteInto(taskTemplate);
            await _entityRepository.Save(taskTemplate);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var taskTemplate = await _entityRepository.TryFindAsync(id);
            if (taskTemplate == null) return NotFound();

            await _entityRepository.HideAsync(taskTemplate);

            return NoContent();
        }
    }
}
