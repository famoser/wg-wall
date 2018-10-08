using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WgWall.Data;

namespace WgWall.Test.Mock
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
