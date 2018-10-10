using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using WgWall.Data;
using WgWall.Data.Model;

namespace WgWall.Test
{
    public static class SampleData
    {
        public static void EnsureSeeded(this MyDbContext context)
        {
            if (!context.FrontendUsers.Any())
            {
                context.FrontendUsers.AddRange(LoadFrontendUsers());
            }

            if (!context.Products.Any())
            {
                context.Products.AddRange(LoadProducts());
            }

            context.SaveChanges();
        }

        public static List<FrontendUser> LoadFrontendUsers()
        {
            return JsonConvert.DeserializeObject<List<FrontendUser>>(File.ReadAllText("Seed" + Path.DirectorySeparatorChar + "frontend_users.json"));
        }

        public static List<Product> LoadProducts()
        {
            return JsonConvert.DeserializeObject<List<Product>>(File.ReadAllText("Seed" + Path.DirectorySeparatorChar + "products.json"));
        }
    }
}
