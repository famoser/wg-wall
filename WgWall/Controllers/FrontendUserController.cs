using Microsoft.AspNetCore.Mvc;
using WgWall.Api.Dto;
using WgWall.Data.Model;
using WgWall.Data.Repository.Interfaces;
using WgWall.Api.Request;
using WgWall.Controllers.Base;

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