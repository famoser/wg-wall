using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WgWall.Data;
using WgWall.Data.Model;
using WgWall.Data.Repository.Interfaces;
using WgWall.Dto;

namespace WgWall.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FrontendUsersController : ControllerBase
    {
        private readonly IFrontendUserRepository _frontendUserRepository;

        public FrontendUsersController(IFrontendUserRepository frontendUserRepository)
        {
            _frontendUserRepository = frontendUserRepository;
            Mapper.Initialize(cfg => cfg.CreateMap<FrontendUser, FrontendUserDto>());
        }

        [HttpGet]
        public async Task<IActionResult> GetFrontendUsers()
        {
            var users = await _frontendUserRepository.GetAllAsync();
            var usersDto = Mapper.Map<IList<FrontendUserDto>>(users);
            return Ok(usersDto);
        }

        [HttpPost("check")]
        public async Task<IActionResult> Check([FromBody] string name)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _frontendUserRepository.CheckExistenceAsync(name));
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateFrontendUser([FromBody] string name)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            return Ok(await _frontendUserRepository.CreateFrontendUserAsync(name));
        }
    }
}