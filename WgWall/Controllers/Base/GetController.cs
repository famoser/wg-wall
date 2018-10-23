using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WgWall.Api.Dto.Base;
using WgWall.Data.Model.Base;
using WgWall.Data.Repository.Base.Interfaces;

namespace WgWall.Controllers.Base
{
    public abstract class GetController<TEntity, TDto, TPayload> : PutController<TEntity, TDto, TPayload>
    where TEntity : BaseEntity, new()
    where TDto : BaseDto
    {
        private readonly IGetRepository<TEntity> _entityRepository;

        protected GetController(IGetRepository<TEntity> entityRepository) : base(entityRepository)
        {
            _entityRepository = entityRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var entities = await _entityRepository.GetAsync();

            var dtos = Mapper.Map<IList<TDto>>(entities);
            return Ok(dtos);
        }
    }
}
