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

namespace WgWall.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingController : ControllerBase
    {
        private readonly ISettingRepository _settingRepository;
        private readonly IMapper _mapper;

        public SettingController(ISettingRepository settingRepository)
        {
            _settingRepository = settingRepository;
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Setting, SettingDto>());
            _mapper = new Mapper(config);
        }
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var settings = await _settingRepository.GetActiveAsync();

            var settingsDto = _mapper.Map<IList<SettingDto>>(settings);
            return Ok(settingsDto);
        }

        // POST: api/Products
        [HttpPost]
        public async Task<IActionResult> PostSetting([FromBody] SettingPayload payload)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _settingRepository.Persist(payload.Key, payload.Value);
           
            return NoContent();
        }
    }
}