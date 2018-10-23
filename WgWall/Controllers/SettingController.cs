using Microsoft.AspNetCore.Mvc;
using WgWall.Api.Dto;
using WgWall.Api.Request;
using WgWall.Controllers.Base;
using WgWall.Data.Model;
using WgWall.Data.Repository.Interfaces;

namespace WgWall.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingController : GetController<Setting, SettingDto, SettingPayload>
    {
        public SettingController(ISettingRepository settingRepository) : base(settingRepository)
        {
        }

        protected override bool WriteInto(Setting target, SettingPayload source)
        {
            target.Key = source.Key;
            target.Value = source.Value;

            return true;
        }
    }
}