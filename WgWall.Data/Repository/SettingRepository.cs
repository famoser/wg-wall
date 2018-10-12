using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WgWall.Data.Model;
using WgWall.Data.Repository.Interfaces;

namespace WgWall.Data.Repository
{
    public class SettingRepository : ISettingRepository
    {
        private readonly MyDbContext _context;

        public SettingRepository(MyDbContext context)
        {
            _context = context;
        }

        public async Task Persist(string key, string value)
        {
            var existing = await _context.Settings.FirstOrDefaultAsync(s => s.Key == key);
            if (existing == null)
            {
                existing = new Setting { Key = key };
                _context.Settings.Add(existing);
            }

            existing.Value = value;
            await _context.SaveChangesAsync();
        }

        public Task<List<Setting>> GetAllAsync()
        {
            return _context.Settings.ToListAsync();
        }
    }
}
