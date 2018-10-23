using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using WgWall.Test.Controllers;
using WgWall.Test.Utils.Interface;

namespace WgWall.Test.Utils
{
    public abstract class AbstractIntegrationTest
    {
        [TestMethod]
        public async Task IntegrationTestWithMockDatabase()
        {
            using (var client = new TestClient<SeededDatabaseStartup>())
            {
                await PerformIntegrationTest(client);
            }
        }

        protected abstract Task PerformIntegrationTest(ITestClient testClient);
        
        protected async Task TestRemove(ITestClient client, string apiUrl, string[] expectedFields, string identifierName = "id")
        {
            var existing = await GetExistingObject(client, apiUrl, expectedFields);
            Assert.IsNotNull(existing);

            //put changes
            await client.DeleteAsync(apiUrl + "/" + existing[identifierName]);

            //check that it has been removed
            var existingCheck = await GetExistingObject(client, apiUrl, expectedFields, identifierName, (int)existing[identifierName]);
            Assert.IsNull(existingCheck);
        }

        protected async Task<object> TestGet(ITestClient client, string apiUrl, string[] expectedFields)
        {
            var response = await client.GetJsonAsync(apiUrl);
            AssertHelper.AssertArray(response, expectedFields);

            return response;
        }

        protected async Task TestPut(ITestClient client, string apiUrl, string[] expectedFields, Dictionary<string, object> updates, string identifierName = "id")
        {
            var existing = await GetExistingObject(client, apiUrl, expectedFields);
            Assert.IsNotNull(existing);

            //ensure new props
            foreach (var key in updates.Keys.ToList())
            {
                Assert.IsTrue(existing.ContainsKey(key), $"{key} not found in json");
                EnsureJsonTypeMatchesObjectType(existing[key].Type, updates[key]);

                //have to check for null because existing[key] can be null
                if (existing[key] == null && updates[key] == null || existing[key].ToString() == updates[key].ToString())
                {
                    Assert.Fail($"property {key} is already equal; hence this test does not fully test the intended behaviour. aborting");
                }
            }

            //put changes
            await client.PutAsync(apiUrl + "/" + existing[identifierName], updates);

            //check that it has been preserved
            await EnsureObjectExists(client, apiUrl, expectedFields, identifierName, (int)existing[identifierName], updates);
        }
        
        protected async Task<object> TestPost(ITestClient client, string apiUrl, string[] expectedFields, Dictionary<string, object> payload, bool verifyWithGet = false, string identifierName = "id")
        {
            //only perform get if explicitly requested
            var getResponse = verifyWithGet ? await TestGet(client, apiUrl, expectedFields) : null;

            var newTaskTemplateResponse = await client.PostAsync(apiUrl, payload);
            var newObject = AssertHelper.AssertObject(newTaskTemplateResponse, expectedFields);

            //check with second get if really added to list
            if (getResponse != null)
            {
                //check more elements returned
                var getResponse2 = await TestGet(client, apiUrl, expectedFields);
                AssertHelper.AssertArrayDifference(getResponse, getResponse2, 1);

                //check added element returned
                await EnsureObjectExists(client, apiUrl, expectedFields, identifierName, (int) newObject[identifierName], payload);
            }

            return newTaskTemplateResponse;
        }

        private void EnsureJsonTypeMatchesObjectType(JTokenType tokenType, object value)
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

            var existingCandidates = getArray.Children().Where(j => (int)j[identifierName] == identifier).ToList();
            Assert.IsTrue(existingCandidates.Count <= 1);

            return existingCandidates.FirstOrDefault() as JObject;
        }

        private async Task<JObject> EnsureObjectExists(ITestClient client, string apiUrl, string[] expectedFields, string identifierName, int identifier, Dictionary<string, object> updates)
        {
            var existing = await GetExistingObject(client, apiUrl, expectedFields, identifierName, identifier);
            Assert.IsNotNull(existing);
            foreach (var key in updates.Keys.ToList())
            {
                Assert.IsTrue(existing.ContainsKey(key), $"{key} not found in json");
                EnsureJsonTypeMatchesObjectType(existing[key].Type, updates[key]);

                //check all props same
                if (existing[key] == null && updates[key] != existing[key] || existing[key].ToString() != updates[key].ToString())
                {
                    Assert.Fail($"{key} does not have the intended value");
                }
            }

            return existing;
        }
    }
}
