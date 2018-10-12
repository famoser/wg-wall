using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using WgWall.Data;
using WgWall.Data.Model;
using WgWall.Data.Model.Base;

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

            if (!context.Settings.Any())
            {
                context.Settings.AddRange(LoadSettings());
            }

            context.SaveChanges();
        }

        private static List<T> AddIds<T>(List<T> list)
        where T : BaseIdEntity
        {
            var id = 1;
            list.ForEach(fu => fu.Id = id++);
            return list;
        }

        public static List<FrontendUser> LoadFrontendUsers()
        {
            return AddIds(JsonConvert.DeserializeObject<List<FrontendUser>>(File.ReadAllText("Seed" + Path.DirectorySeparatorChar + "frontend_users.json")));
        }

        public static List<Product> LoadProducts()
        {
            return AddIds(JsonConvert.DeserializeObject<List<Product>>(File.ReadAllText("Seed" + Path.DirectorySeparatorChar + "products.json")));
        }

        public static List<Setting> LoadSettings()
        {
            return AddIds(JsonConvert.DeserializeObject<List<Setting>>(File.ReadAllText("Seed" + Path.DirectorySeparatorChar + "settings.json")));
        }
    }
}
