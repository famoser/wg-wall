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
using WgWall.Api.Request;

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
        
        [HttpPost]
        public async Task<IActionResult> PostFrontendUser([FromBody] FrontendUserPayload user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newUser = await _frontendUserRepository.CreateAsync(user.Name);
            var newUserDto = _mapper.Map<FrontendUserDto>(newUser);
            return Ok(newUserDto);
        }
    }
}