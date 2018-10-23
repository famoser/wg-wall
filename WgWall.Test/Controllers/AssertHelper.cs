using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace WgWall.Test.Controllers
{
    public static class AssertHelper
    {
        public static JObject AssertObject(object input, string[] expectedFields = null)
        {
            var jObject = input as JObject;
            Assert.IsNotNull(jObject);

            if (expectedFields != null)
            {
                Assert.IsTrue(jObject.Properties().Count() == expectedFields.Length);
                foreach (var property in jObject.Properties())
                {
                    Assert.IsTrue(expectedFields.Contains(property.Name),
                        $"expectedFields.Contains(property.Name) failed for ${property.Name}");
                }
            }

            return jObject;
        }

        public static JArray AssertArray(object input, string[] expectedFields = null)
        {
            var array = input as JArray;
            Assert.IsNotNull(array);

            if (expectedFields != null)
            {
                Assert.IsTrue(array.Count > 0, "array empty; expected fields can't be checked");
                AssertObject(array[0], expectedFields);
            }

            return array;
        }

        public static IList<T> AssertList<T>(IActionResult result)
        {
            var objectResult = result as OkObjectResult;
            Assert.IsNotNull(objectResult);

            var users = objectResult.Value as IList<T>;
            Assert.IsNotNull(users);

            return users;
        }

        public static void AssertArrayDifference(object getResponse, object getResponse2, int difference)
        {
            var array1 = getResponse as JArray;
            Assert.IsNotNull(array1);

            var array2 = getResponse2 as JArray;
            Assert.IsNotNull(array2);

            Assert.AreEqual(array1.Count + difference, array2.Count);
        }
    }
}
