﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WgWall.Data.Model;

namespace WgWall.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {

        }

        public DbSet<Products> Products { get; set; }
        public DbSet<FrontendUser> FrontendUsers { get; set; }
    }
}
