using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WgWall.Api.Dto;
using WgWall.Api.Dto.Base;
using WgWall.Api.Request;
using WgWall.Api.Request.Base;
using WgWall.Controllers.Base;
using WgWall.Data.Model;
using WgWall.Data.Repository.Interfaces;

namespace WgWall.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskExecutionController : PostController<TaskExecution, BaseDto, TaskExecutionPayload>
    {
        private readonly ITaskTemplateRepository _taskTemplateRepository;
        private readonly IFrontendUserRepository _frontendUserRepository;

        public TaskExecutionController(IFrontendUserRepository frontendUserRepository, ITaskTemplateRepository taskTemplateRepository, ITaskExecutionRepository taskExecutionRepository) : base(taskExecutionRepository)
        {
            _frontendUserRepository = frontendUserRepository;
            _taskTemplateRepository = taskTemplateRepository;
        }

        protected override async Task<bool> WriteIntoAsync(TaskExecution target, TaskExecutionPayload source)
        {
            var frontendUser = await _frontendUserRepository.TryFindAsync(source.FrontendUserId);
            var taskTemplate = await _taskTemplateRepository.TryFindAsync(source.TaskTemplateId);

            if (frontendUser == null || taskTemplate == null)
            {
                return false;
            }

            target.Accountable = frontendUser;
            target.Entity = taskTemplate;

            return true;
        }
    }
}