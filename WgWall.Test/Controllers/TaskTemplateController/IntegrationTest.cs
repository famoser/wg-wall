using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WgWall.Test.Utils.IntegrationTest;
using WgWall.Test.Utils.IntegrationTest.Interface;

namespace WgWall.Test.Controllers.TaskTemplateController
{
    [TestClass]
    public class IntegrationTest : AbstractIntegrationTest
    {
        protected override async Task PerformIntegrationTest(ITestClient testClient)
        {
            //arrange
            var expectedFields = new[] { "name", "id", "intervalInDays", "lastExecutionAt" };
            var newTemplate = new { Name = "new task template name", IntervalInDays = 2 };
            var putFieldChange = new Dictionary<string, object> { { "name", "new task template" }, { "intervalInDays", 6 } };
            var apiUrl = "/api/TaskTemplate";

            //test get
            await TestGet(testClient, apiUrl, expectedFields);

            //test post
            await TestPost(testClient, apiUrl, newTemplate, expectedFields, true);

            //test put
            await TestPut(testClient, apiUrl, expectedFields, putFieldChange);

            //test put
            await TestRemove(testClient, apiUrl, expectedFields);
        }

        private async Task TestRemove(ITestClient client, string apiUrl, string[] expectedFields, string identifierName = "id")
        {
            var existing = await GetExistingObject(client, apiUrl, expectedFields);
            Assert.IsNotNull(existing);

            //put changes
            await client.DeleteAsync(apiUrl + "/" + existing[identifierName]);

            //check that it has been removed
            var existingCheck = await GetExistingObject(client, apiUrl, expectedFields, identifierName, (int)existing[identifierName]);
            Assert.IsNull(existingCheck);
        }

        private async Task<object> TestGet(ITestClient client, string apiUrl, string[] expectedFields)
        {
            var response = await client.GetJsonAsync(apiUrl);
            AssertHelper.AssertArray(response, expectedFields);

            return response;
        }

        private async Task TestPut(ITestClient client, string apiUrl, string[] expectedFields, Dictionary<string, object> updates, string identifierName = "id")
        {
            var existing = await GetExistingObject(client, apiUrl, expectedFields);
            Assert.IsNotNull(existing);
            
            //ensure new props
            foreach (var key in updates.Keys.ToList())
            {
                Assert.IsTrue(existing.ContainsKey(key), $"{key} not found in json");

                ObjectMatchesJsonType(existing[key].Type, updates[key]);

                //have to check for null because existing[key] can be null
                if (existing[key] == null && updates[key] == null || existing[key].ToString() == updates[key].ToString())
                {
                    Assert.Fail($"property {key} is already equal; hence this test does not fully test the intended behaviour. aborting");
                }
            }

            //put changes
            await client.PutAsync(apiUrl + "/" + existing[identifierName], updates);

            //check that it has been preserved
            var newExisting = await GetExistingObject(client, apiUrl, expectedFields, identifierName, (int)existing[identifierName]);
            foreach (var key in updates.Keys.ToList())
            {
                //write props
                if (newExisting[key] == null && updates[key] != newExisting[key] || newExisting[key].ToString() != updates[key].ToString())
                {
                    Assert.Fail($"{key} does not have the intended value");
                }
            }
        }

        private void ObjectMatchesJsonType(JTokenType tokenType, object value)
        {
            switch (tokenType)
            {
                case JTokenType.Null:
                    //can't enforce here anything
                    break;
                case JTokenType.Boolean:
                    Assert.IsInstanceOfType(value, typeof(bool));
                    break;
                case JTokenType.Integer:
                    Assert.IsInstanceOfType(value, typeof(int));
                    break;
                case JTokenType.String:
                    Assert.IsInstanceOfType(value, typeof(string));
                    break;
                case JTokenType.Date:
                    Assert.IsInstanceOfType(value, typeof(DateTime));
                    break;
                default:
                    Assert.Fail($"unknown type {tokenType}");
                    break;
            }
        }

        private async Task<object> TestPost<T>(ITestClient client, string apiUrl, T payload, string[] expectedFields, bool verifyWithGet = false)
        where T : class
        {
            //only perform get if explicitly requested
            var getResponse = verifyWithGet ? await TestGet(client, apiUrl, expectedFields) : null;

            var newTaskTemplateResponse = await client.PostAsync(apiUrl, payload);
            AssertHelper.AssertObject(newTaskTemplateResponse, expectedFields);

            //check with second get if really added to list
            if (getResponse != null)
            {
                var getResponse2 = await TestGet(client, apiUrl, expectedFields);
                AssertHelper.AssertArrayDifference(getResponse, getResponse2, 1);
            }

            return newTaskTemplateResponse;
        }

        private async Task<JObject> GetExistingObject(ITestClient client, string apiUrl, string[] expectedFields)
        {
            var getResponse = await TestGet(client, apiUrl, expectedFields);
            var getArray = AssertHelper.AssertArray(getResponse);

            //if no identifier provided simply return first element
            return getArray.Count > 0 ? AssertHelper.AssertObject(getArray[0], expectedFields) : null;
        }

        private async Task<JObject> GetExistingObject(ITestClient client, string apiUrl, string[] expectedFields, string identifierName, int identifier)
        {
            var getResponse = await TestGet(client, apiUrl, expectedFields);
            var getArray = AssertHelper.AssertArray(getResponse);

            return getArray.Children().FirstOrDefault(j => (int)j[identifierName] == identifier) as JObject;
        }
    }
}
