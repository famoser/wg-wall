﻿using AutoMapper;
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
    public class TaskExecutionController : ControllerBase
    {
        private readonly ITaskTemplateRepository _taskTemplateRepository;
        private readonly ITaskExecutionRepository _taskExecutionRepository;
        private readonly IFrontendUserRepository _frontendUserRepository;

        public TaskExecutionController(IFrontendUserRepository frontendUserRepository, ITaskTemplateRepository taskTemplateRepository, ITaskExecutionRepository taskExecutionRepository)
        {
            _frontendUserRepository = frontendUserRepository;
            _taskTemplateRepository = taskTemplateRepository;
            _taskExecutionRepository = taskExecutionRepository;
        }

        [HttpPost("{templateId}")]
        public async Task<IActionResult> Execute([FromRoute] int id, [FromBody] AccountablePayload payload)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var taskTemplate = await _taskTemplateRepository.TryGet(id);
            if (taskTemplate == null) return NotFound();

            var currentUser = await _frontendUserRepository.TryGet(payload.FrontendUserId);
            var task = TaskExecution.Create(taskTemplate, currentUser);
            await _taskExecutionRepository.Save(task);

            return NoContent();
        }
    }
}