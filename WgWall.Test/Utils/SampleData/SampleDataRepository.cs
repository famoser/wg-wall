using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using WgWall.Data.Model;
using WgWall.Data.Model.Base;
using WgWall.Test.Utils.SampleData.Interface;

namespace WgWall.Test.Utils.SampleData
{
    public class SampleDataRepository : ISampleDataService
    {
        private static List<T> AddIds<T>(List<T> list)
        where T : BaseEntity
        {
            var id = 1;
            list.ForEach(fu => fu.Id = id++);
            return list;
        }

        private List<T> LoadSampleFor<T>()
            where T : BaseEntity
        {
            var typeName = typeof(T).Name;
            var snakeCase = Regex.Replace(typeName, "[a-z][A-Z]", "_$1");
            var filePath = "Seed" + Path.DirectorySeparatorChar + snakeCase + ".json";

            return File.Exists(filePath) ? JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(filePath)) : new List<T>();
        }

        private List<TaskExecution> LoadTasks(List<TaskTemplate> taskTemplates)
        {
            int skip = 3;
            var res = new List<TaskExecution>();
            for (int i = 0; i < taskTemplates.Count * 2; i++)
            {
                if (i % skip != 0)
                {
                    res.Add(new TaskExecution()
                    {
                        ExecutedAt = new DateTime(),
                        Entity = taskTemplates[i % taskTemplates.Count]
                    });
                }
            }
            return AddIds(res);
        }

        public List<BaseEntity> LoadEntities()
        {
            var list = new List<BaseEntity>();
            list.AddRange(LoadSampleFor<Event>());
            list.AddRange(LoadSampleFor<FrontendUser>());
            list.AddRange(LoadSampleFor<Product>());
            list.AddRange(LoadSampleFor<Setting>());
            list.AddRange(LoadSampleFor<TaskTemplate>());

            return list;
        }
    }
}
