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
    public class FrontendUserController : ControllerBase
    {
        private readonly IFrontendUserRepository _frontendUserRepository;
        private readonly IMapper _mapper;

        public FrontendUserController(IFrontendUserRepository frontendUserRepository)
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
        public async Task<IActionResult> Check([FromBody] FrontendUserDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            return Ok(await _frontendUserRepository.CheckExistenceAsync(user.Name));
        }
        
        [HttpPost]
        public async Task<IActionResult> PostFrontendUser([FromBody] FrontendUserDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newUser = await _frontendUserRepository.CreateFrontendUserAsync(user.Name);
            var newUserDto = _mapper.Map<FrontendUserDto>(newUser);
            return Ok(newUserDto);
        }
    }
}