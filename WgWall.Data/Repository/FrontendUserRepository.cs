using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WgWall.Data.Model;
using WgWall.Data.Repository.Interfaces;

namespace WgWall.Data.Repository
{
    public class FrontendUserRepository : IFrontendUserRepository
    {
        private readonly MyDbContext _context;

        public FrontendUserRepository(MyDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CheckExistenceAsync(string name)
        {
            var user = await _context.FrontendUsers.FirstOrDefaultAsync(e => e.Name == name);
            return user == null;
        }

        public async Task<FrontendUser> CreateFrontendUserAsync(string name)
        {
            var user = await _context.FrontendUsers.FirstOrDefaultAsync(e => e.Name == name);
            if (user == null)
            {
                user = FrontendUser.Create(name);
                _context.FrontendUsers.Add(user);
                await _context.SaveChangesAsync();
            }

            Contract.Assert(user.Id > 0);
            return user;
        }

        public Task<List<FrontendUser>> GetAllAsync()
        {
            return _context.FrontendUsers.ToListAsync();
        }
    }
}
