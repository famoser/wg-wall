using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WgWall.Api.Dto;
using WgWall.Api.Request;
using WgWall.Api.Request.Base;
using WgWall.Controllers.Base;
using WgWall.Data.Model;
using WgWall.Data.Repository.Interfaces;

namespace WgWall.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskTemplateController : HideableCrudController<TaskTemplate, TaskTemplateDto, TaskTemplatePayload>
    {
        public TaskTemplateController(ITaskTemplateRepository taskTemplateRepository) : base(taskTemplateRepository)
        {
        }
        
        protected override bool WriteInto(TaskTemplate target, TaskTemplatePayload source)
        {
            target.Name = source.Name;
            target.IntervalInDays = source.IntervalInDays;
            return true;
        }
    }
}