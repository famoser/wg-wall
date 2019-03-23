using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WgWall.Data.Model;
using WgWall.Data.Repository.Base;
using WgWall.Data.Repository.Interfaces;

namespace WgWall.Data.Repository
{
    public class PlateRepository : TryFindRepository<Plate>,  IPlateRepository
    {
        public PlateRepository(MyDbContext context) : base(context)
        {
        }

        public Task<List<Plate>> GetAsync()
        {
            var today = DateTime.Today;
            return Context.Plates.Where(p => p.ValidityDate == today).ToListAsync();
        }
    }
}
