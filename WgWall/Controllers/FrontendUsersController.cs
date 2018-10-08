using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WgWall.Data;
using WgWall.Model;

namespace WgWall.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FrontendUsersController : ControllerBase
    {
        private readonly MyDbContext _context;

        public FrontendUsersController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/FrontendUsers
        [HttpGet]
        public IEnumerable<FrontendUser> GetFrontendUsers()
        {
            return _context.FrontendUsers;
        }

        // GET: api/FrontendUsers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFrontendUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var frontendUser = await _context.FrontendUsers.FindAsync(id);

            if (frontendUser == null)
            {
                return NotFound();
            }

            return Ok(frontendUser);
        }

        // PUT: api/FrontendUsers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFrontendUser([FromRoute] int id, [FromBody] FrontendUser frontendUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != frontendUser.Id)
            {
                return BadRequest();
            }

            _context.Entry(frontendUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FrontendUserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/FrontendUsers
        [HttpPost]
        public async Task<IActionResult> PostFrontendUser([FromBody] FrontendUser frontendUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.FrontendUsers.Add(frontendUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFrontendUser", new { id = frontendUser.Id }, frontendUser);
        }

        // DELETE: api/FrontendUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFrontendUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var frontendUser = await _context.FrontendUsers.FindAsync(id);
            if (frontendUser == null)
            {
                return NotFound();
            }

            _context.FrontendUsers.Remove(frontendUser);
            await _context.SaveChangesAsync();

            return Ok(frontendUser);
        }

        private bool FrontendUserExists(int id)
        {
            return _context.FrontendUsers.Any(e => e.Id == id);
        }
    }
}