using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WgWall.Api.Dto;
using WgWall.Data;
using WgWall.Data.Model;
using WgWall.Data.Repository.Interfaces;
using WgWall.Api.Request;
using WgWall.Controllers.Base;
using WgWall.Data.Repository.Base.Interfaces;

namespace WgWall.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FrontendUserController : GetController<FrontendUser, FrontendUserDto, FrontendUserPayload>
    {
        public FrontendUserController(IFrontendUserRepository repository) : base(repository)
        {
        }

        protected override bool WriteInto(FrontendUser target, FrontendUserPayload source)
        {
            target.Name = source.Name;
            return true;
        }
    }
}