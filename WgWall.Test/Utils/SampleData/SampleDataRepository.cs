using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
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
            var snakeCase = Regex.Replace(typeName, "(.)([A-Z])", "$1_$2", RegexOptions.Compiled).ToLower();
            var filePath = "Seed" + Path.DirectorySeparatorChar + snakeCase + "s.json";

            return File.Exists(filePath) ? JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(filePath)) : new List<T>();
        }

        private List<TTrack> LoadTrackActionEntity<TEntity, TTrack>(List<TEntity> entities, List<FrontendUser> frontendUsers)
            where TTrack : TrackActionEntity<TEntity>, new()
        {
            var res = new List<TTrack>();
            for (int i = 0; i < entities.Count * 2; i++)
            {
                res.Add(new TTrack()
                {
                    ExecutedAt = new DateTime(),
                    Entity = entities[i % entities.Count],
                    Accountable = frontendUsers[i % frontendUsers.Count],
                    KarmaEarned = i % entities.Count
                });
            }
            return AddIds(res);
        }

        private List<Plate> LoadPlates(List<FrontendUser> frontendUsers)
        {
            int skip = 0;
            var plates = new List<Plate>();
            foreach (var frontendUser in frontendUsers)
            {
                if (skip++ % 2 == 0)
                {
                    plates.Add(new Plate()
                    {
                        Accountable = frontendUser,
                        DinnerState = skip % 3,
                        ValidityDate = DateTime.Today
                    });
                }
            }
            return AddIds(plates);
        }

        public List<BaseEntity> LoadEntities()
        {
            var list = new List<BaseEntity>();
            list.AddRange(LoadSampleFor<Event>());
            list.AddRange(LoadSampleFor<Setting>());

            var frontendUsers = LoadSampleFor<FrontendUser>();
            list.AddRange(frontendUsers);
            list.AddRange(LoadPlates(frontendUsers));
            
            var products = LoadSampleFor<Product>();
            list.AddRange(products);
            list.AddRange(LoadTrackActionEntity<Product, ProductPurchase>(products, frontendUsers));

            var taskTemplates = LoadSampleFor<TaskTemplate>();
            list.AddRange(taskTemplates);
            list.AddRange(LoadTrackActionEntity<TaskTemplate, TaskExecution>(taskTemplates, frontendUsers));

            return list;
        }
    }
}
