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
    public abstract class PutController<TEntity, TDto, TPayload> : PostController<TEntity, TDto, TPayload>
    where TEntity : BaseEntity, new()
    where TDto : BaseDto
    {
        private readonly ITryFindRepository<TEntity> _entityRepository;

        protected PutController(ITryFindRepository<TEntity> entityRepository): base(entityRepository)
        {
            _entityRepository = entityRepository;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] TPayload payload)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var entity = await _entityRepository.TryFindAsync(id);
            if (entity == null) return NotFound();

            if (!WriteInto(entity, payload) && !await WriteIntoAsync(entity, payload)) return BadRequest();

            await _entityRepository.Save(entity);

            return NoContent();
        }
    }
}
