using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WgWall.Api.Dto;
using WgWall.Api.Dto.Base;
using WgWall.Data.Model.Base;
using WgWall.Data.Repository.Base.Interfaces;

namespace WgWall.Controllers.Base
{
    public abstract class HideableCrudController<TEntity, TDto, TPayload> : PutController<TEntity, TDto, TPayload>
    where TEntity : BaseEntity, new()
    where TDto : BaseDto
    {
        protected IHideableCrudRepository<TEntity> _entityRepository;

        protected HideableCrudController(ITryFindRepository<TEntity> entityRepository) : base(entityRepository)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var entities = await _entityRepository.GetActiveAsync();

            var dtos = Mapper.Map<IList<TDto>>(entities);
            return Ok(dtos);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Hide([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var entity = await _entityRepository.TryFindAsync(id);
            if (entity == null) return NotFound();

            await _entityRepository.HideAsync(entity);

            return NoContent();
        }
    }
}
