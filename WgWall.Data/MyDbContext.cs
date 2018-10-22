using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Newtonsoft.Json;
using WgWall.Data.Model;
using Task = WgWall.Data.Model.Task;

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
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Task> TaskTemplates { get; set; }
        public DbSet<TaskExecution> Tasks { get; set; }
    }
}
