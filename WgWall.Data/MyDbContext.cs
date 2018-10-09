using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Newtonsoft.Json;
using WgWall.Data.Model;

namespace WgWall.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<FrontendUser> FrontendUsers { get; set; }

        public void EnsureSeeded()
        {
            if (!FrontendUsers.Any())
            {
                var frontendUsers = JsonConvert.DeserializeObject<List<FrontendUser>>(File.ReadAllText("Seed" + Path.DirectorySeparatorChar + "frontend_users.json"));
                FrontendUsers.AddRange(frontendUsers);
            }

            if (!Products.Any())
            {
                var products = JsonConvert.DeserializeObject<List<Product>>(File.ReadAllText("Seed" + Path.DirectorySeparatorChar + "products.json"));
                Products.AddRange(products);
            }

            SaveChanges();
        }
    }
}
