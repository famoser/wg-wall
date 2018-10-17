using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Castle.Core.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WgWall.Api.Dto;
using WgWall.Api.Request;
using WgWall.Data;
using WgWall.Data.Model;
using WgWall.Data.Repository.Interfaces;
using WgWall.Dto;

namespace WgWall.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskTemplateController : ControllerBase
    {
        private readonly ITaskTemplateRepository _taskTemplateRepository;
        private readonly IFrontendUserRepository _frontendUserRepository;
        private readonly IMapper _mapper;

        public TaskTemplateController(IFrontendUserRepository frontendUserRepository, ITaskTemplateRepository taskTemplateRepository)
        {
            _frontendUserRepository = frontendUserRepository;
            _taskTemplateRepository = taskTemplateRepository;
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TaskTemplate, TaskTemplateDto>());
            _mapper = new Mapper(config);
        }

        // GET: api/TaskTemplates
        [HttpGet]
        public async Task<IActionResult> GetTasks()
        {
            var taskTemplates = await _taskTemplateRepository.GetAllAsync();

            var taskTemplatesDto = _mapper.Map<IList<TaskTemplateDto>>(taskTemplates);
            return Ok(taskTemplatesDto);
        }

        [HttpGet("hide/{id}")]
        public async Task<IActionResult> Hide([FromRoute] int templateId)
        {
            await _taskTemplateRepository.Hide(templateId);
            return NoContent();
        }

        // PUT: api/TaskTemplates/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaskTemplate([FromRoute] int id, [FromBody] TaskTemplatePayload payload)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            await _taskTemplateRepository.Update(id, payload.Name, payload.IntervalInDays);
            
            return NoContent();
        }

        // POST: api/TaskTemplates
        [HttpPost]
        public async Task<IActionResult> PostTaskTemplate([FromBody] TaskTemplatePayload payload)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var taskTemplate = await _taskTemplateRepository.Create(payload.Name, payload.IntervalInDays, await _frontendUserRepository.TryGet(payload.FrontendUserId));
            var taskTemplateDto = _mapper.Map<TaskTemplateDto>(taskTemplate);
            return Ok(taskTemplateDto);
        }
    }
}