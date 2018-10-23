using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using WgWall.Api.Dto;
using WgWall.Api.Request;
using WgWall.Controllers.Base;
using WgWall.Data;
using WgWall.Data.Model;
using WgWall.Data.Repository.Base.Interfaces;
using WgWall.Data.Repository.Interfaces;
using WgWall.Test.Utils.Startup;

namespace WgWall.Test.Controllers.TaskTemplateController
{
    public class ControllerTest : HideableCrudController<TaskTemplate, TaskTemplateDto, TaskTemplatePayload>
    {
        public ControllerTest() : base(null)
        {
            //fill up read services & mock DB
            var startup = new MockDatabaseStartup(null);
            var services = new ServiceCollection();
            startup.ConfigureServices(services);

            //prepare db for run
            var serviceProvider = services.BuildServiceProvider();
            startup.PreConfigureHook(serviceProvider);

            _entityRepository = serviceProvider.GetService<ITaskTemplateRepository>();
        }
    }
}
