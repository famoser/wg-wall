using Microsoft.EntityFrameworkCore;
using WgWall.Data;

namespace WgWall.Test.Mock.Data
{
    sealed class MockDbContext : MyDbContext
    {
        public MockDbContext() : base(new DbContextOptionsBuilder<MyDbContext>().UseLazyLoadingProxies().UseSqlite("DataSource =test.sqlite", x => x.MigrationsAssembly("WgWall.Migrations")).Options)
        {
            //migrations do not work; could not find solution
            var migrations = Database.GetPendingMigrations();
            migrations.ToString();
        }
    }
}
