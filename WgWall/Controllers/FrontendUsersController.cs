using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WgWall.Data;
using WgWall.Data.Model;
using WgWall.Data.Repository.Interfaces;

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
        }

        [HttpGet]
        public async Task<IActionResult> GetFrontendUsers()
        {
            return Ok(await _frontendUserRepository.GetAllAsync());
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

        // PUT: api/FrontendUsers/5
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