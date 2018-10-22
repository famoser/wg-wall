using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WgWall.Api.Dto;
using WgWall.Api.Request;
using WgWall.Api.Request.Base;
using WgWall.Data.Model;
using WgWall.Data.Repository.Interfaces;

namespace WgWall.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskTemplateController : ControllerBase
    {
        private readonly ITaskTemplateRepository _taskTemplateRepository;
        private readonly ITaskExecutionRepository _taskExecutionRepository;
        private readonly IFrontendUserRepository _frontendUserRepository;
        private readonly IMapper _mapper;

        public TaskTemplateController(IFrontendUserRepository frontendUserRepository, ITaskTemplateRepository taskTemplateRepository, ITaskExecutionRepository taskExecutionRepository)
        {
            _frontendUserRepository = frontendUserRepository;
            _taskTemplateRepository = taskTemplateRepository;
            _taskExecutionRepository = taskExecutionRepository;
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TaskTemplate, TaskTemplateDto>());
            _mapper = new Mapper(config);
        }
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var taskTemplates = await _taskTemplateRepository.GetAllAsync();

            var taskTemplatesDto = _mapper.Map<IList<TaskTemplateDto>>(taskTemplates);
            return Ok(taskTemplatesDto);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaskTemplate([FromRoute] int id, [FromBody] TaskTemplatePayload payload)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var taskTemplate = await _taskTemplateRepository.TryGet(id);
            if (taskTemplate == null) return NotFound();

            taskTemplate.Name = payload.Name;
            taskTemplate.IntervalInDays = payload.IntervalInDays;
            taskTemplate.Hidden = payload.Hidden;

            await _taskTemplateRepository.Save(taskTemplate);
            return NoContent();
        }
        
        [HttpPost]
        public async Task<IActionResult> PostTaskTemplate([FromBody] TaskTemplatePayload payload)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currentUser = await _frontendUserRepository.TryGet(payload.FrontendUserId);
            var taskTemplate = TaskTemplate.Create(payload.Name, payload.IntervalInDays, currentUser);
            await _taskTemplateRepository.Save(taskTemplate);

            var taskTemplateDto = _mapper.Map<TaskTemplateDto>(taskTemplate);
            return Ok(taskTemplateDto);
        }
    }
}