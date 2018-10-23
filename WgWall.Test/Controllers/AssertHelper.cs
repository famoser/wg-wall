using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using WgWall.Api.Dto;
using WgWall.Data.Repository.Interfaces;

namespace WgWall.Test.Controllers
{
    public static class AssertHelper
    {
        public static void AssertFields(JObject obj, string[] fields)
        {
            Assert.IsNotNull(obj);

            Assert.IsTrue(obj.Properties().Count() == fields.Length);
            foreach (var property in obj.Properties())
            {
                Assert.IsTrue(fields.Contains(property.Name));
            }
        }

        public static IList<T> AssertList<T>(IActionResult result)
        {
            var objectResult = result as OkObjectResult;
            Assert.IsNotNull(objectResult);

            var users = objectResult.Value as IList<T>;
            Assert.IsNotNull(users);

            return users;
        }
    }
}
