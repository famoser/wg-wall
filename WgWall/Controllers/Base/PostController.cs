using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WgWall.Api.Dto.Base;
using WgWall.Data.Model.Base;
using WgWall.Data.Repository.Base.Interfaces;

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
namespace WgWall.Controllers.Base
{
    public abstract class PostController<TEntity, TDto, TPayload> : ControllerBase
    where TEntity : BaseEntity, new()
    where TDto : BaseDto
    {
        private readonly ISaveRepository<TEntity> _entityRepository;
        protected readonly IMapper Mapper;

        protected PostController(ISaveRepository<TEntity> entityRepository)
        {
            _entityRepository = entityRepository;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TEntity, TDto>());
            Mapper = new Mapper(config);
        }

        /// <summary>
        /// overwrite either this method or the async variation
        /// </summary>
        /// <param name="target"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        protected virtual bool WriteInto(TEntity target, TPayload source)
        {
            return false;
        }

        /// <summary>
        /// overwrite either this method or the sync variation
        /// </summary>
        /// <param name="target"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        protected virtual async Task<bool> WriteIntoAsync(TEntity target, TPayload source)
        {
            return false;
        }
        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TPayload payload)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var entity = new TEntity();
            if (!WriteInto(entity, payload) && !await WriteIntoAsync(entity, payload)) return BadRequest();

            await _entityRepository.Save(entity);

            var productDto = Mapper.Map<TDto>(entity);
            return Ok(productDto);
        }
    }
}
