using Microsoft.EntityFrameworkCore;
using WgWall.Data.Model;

namespace WgWall.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {

        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Plate> Plates { get; set; }
        public DbSet<FrontendUser> FrontendUsers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductPurchase> ProductPurchases { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<TaskExecution> TaskExecutions { get; set; }
        public DbSet<TaskTemplate> TaskTemplates { get; set; }
    }
}
