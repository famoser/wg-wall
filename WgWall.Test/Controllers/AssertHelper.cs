using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace WgWall.Test.Controllers
{
    public static class AssertHelper
    {
        public static void AssertBooleanResult(IActionResult result, bool expected)
        {
            var objectResult = result as OkObjectResult;

            Assert.IsNotNull(objectResult);
            Assert.IsInstanceOfType(objectResult.Value, typeof(bool));
            Assert.AreEqual(expected, (bool)objectResult.Value);
        }

        public static void AssertFields(JObject obj, string[] fields)
        {
            Assert.IsNotNull(obj);

            Assert.IsTrue(obj.Properties().Count() == fields.Length);
            foreach (var property in obj.Properties())
            {
                Assert.IsTrue(fields.Contains(property.Name));
            }
        }
    }
}
