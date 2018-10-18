using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WgWall.Api.Dto;
using WgWall.Api.Request.Base;
using WgWall.Data.Repository.Interfaces;

namespace WgWall.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ITaskTemplateRepository _taskTemplateRepository;
        private readonly IFrontendUserRepository _frontendUserRepository;
        private readonly IMapper _mapper;

        public TaskController(IFrontendUserRepository frontendUserRepository, ITaskRepository taskRepository, ITaskTemplateRepository taskTemplateRepository)
        {
            _frontendUserRepository = frontendUserRepository;
            _taskRepository = taskRepository;
            _taskTemplateRepository = taskTemplateRepository;
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Data.Model.Task, TaskDto>());
            _mapper = new Mapper(config);
        }

        // GET: api/Tasks
        [HttpGet]
        public async Task<IActionResult> GetActiveTasks()
        {
            var tasks = await _taskRepository.GetActiveAsync();

            var tasksDto = _mapper.Map<IList<TaskDto>>(tasks);
            return Ok(tasksDto);
        }

        [HttpPost("create/{templateId}")]
        public async Task<IActionResult> Create([FromRoute] int templateId, [FromBody] AccountablePayload payload)
        {
            var taskTemplate = await _taskTemplateRepository.TryGet(templateId);
            if (taskTemplate == null)
            {
                return NotFound();
            }

            //remember last activation
            taskTemplate.LastActivationAt = new DateTime();
            await _taskTemplateRepository.Update(taskTemplate);

            //save new task
            

            var task = await _taskRepository.Create(taskTemplate, await _frontendUserRepository.TryGet(payload.FrontendUserId));
            var taskDto = _mapper.Map<TaskDto>(task);
            return Ok(taskDto);
        }

        [HttpPost("done/{id}")]
        public async Task<IActionResult> Done([FromRoute] int id, [FromBody] AccountablePayload payload)
        {
            await _taskRepository.Done(id, await _frontendUserRepository.TryGet(payload.FrontendUserId));
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _taskRepository.Remove(id);
            return NoContent();
        }
    }
}