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
        private readonly IMapper _mapper;

        public FrontendUsersController(IFrontendUserRepository frontendUserRepository)
        {
            _frontendUserRepository = frontendUserRepository;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<FrontendUser, FrontendUserDto>());
            _mapper = new Mapper(config);
        }

        [HttpGet]
        public async Task<IActionResult> GetFrontendUsers()
        {
            var users = await _frontendUserRepository.GetAllAsync();

            var usersDto = _mapper.Map<IList<FrontendUserDto>>(users);
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

            var user = await _frontendUserRepository.CreateFrontendUserAsync(name);
            var userDto = _mapper.Map<FrontendUserDto>(user);
            return Ok(userDto);
        }
    }
}