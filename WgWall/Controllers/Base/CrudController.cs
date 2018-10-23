using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WgWall.Api.Dto.Base;
using WgWall.Data.Model.Base;
using WgWall.Data.Repository.Base.Interfaces;

namespace WgWall.Controllers.Base
{
    public abstract class CrudController<TEntity, TDto, TPayload> : GetController<TEntity, TDto, TPayload>
    where TEntity : BaseEntity, new()
    where TDto : BaseDto
    {
        private readonly ICrudRepository<TEntity> _entityRepository;

        protected CrudController(ICrudRepository<TEntity> entityRepository) : base(entityRepository)
        {
            _entityRepository = entityRepository;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var entity = await _entityRepository.TryFindAsync(id);
            if (entity == null) return NotFound();

            await _entityRepository.RemoveAsync(entity);

            return NoContent();
        }
    }
}
