using System;
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

            if (!context.TaskTemplates.Any())
            {
                var taskTemplates = LoadTaskTemplates();
                context.TaskTemplates.AddRange(taskTemplates);

                if (!context.Tasks.Any())
                {
                    context.Tasks.AddRange(LoadTasks(taskTemplates));
                }
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

        public static List<TaskTemplate> LoadTaskTemplates()
        {
            return AddIds(JsonConvert.DeserializeObject<List<TaskTemplate>>(File.ReadAllText("Seed" + Path.DirectorySeparatorChar + "task_templates.json")));
        }

        public static List<Task> LoadTasks(List<TaskTemplate> taskTemplates)
        {
            int skip = 3;
            var res = new List<Task>();
            for (int i = 0; i < taskTemplates.Count * 2; i++)
            {
                if (i % skip != 0)
                {
                    res.Add(new Task()
                    {
                        ActivatedAt = new DateTime(),
                        TaskTemplate = taskTemplates[i % taskTemplates.Count]
                    });
                }
            }
            return AddIds(res);
        }
    }
}
