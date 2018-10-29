using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WgWall.Test.Utils;
using WgWall.Test.Utils.Interface;

namespace WgWall.Test.Controllers
{
    [TestClass]
    public class BackupControllerTest : AbstractIntegrationTest
    {
        protected override async Task PerformIntegrationTest(ITestClient testClient)
        {
            //arrange
            var payload = new byte[] { 50, 20, 30 };
            var apiUrl = "/Backup";

            //info page should contain infos to urls
            var str = await testClient.GetAsync(apiUrl);
            Assert.IsNotNull(str);
            Assert.IsTrue(str.Contains(apiUrl));

            //download original db
            var originalDb = await testClient.GetFileAsync(apiUrl + "/db.sqlite");
            Assert.IsNotNull(originalDb);
            Assert.IsTrue(originalDb.Length > 0);

            //replace db with own file
            var replaceResponse = await testClient.PostFileAsync(apiUrl + "/post", payload, "files");
            Assert.IsNotNull(replaceResponse);
            Assert.IsTrue(replaceResponse.ToLower().Contains("true"));

            //check that replace was successful
            var replaceDb = await testClient.GetFileAsync(apiUrl + "/db.sqlite");
            CheckArraysMatch(payload, replaceDb);

            //check that backup was successful
            var backupDb = await testClient.GetFileAsync(apiUrl + "/db.sqlite_backup");
            CheckArraysMatch(originalDb, backupDb);

            //reupload correct db
            await testClient.PostFileAsync(apiUrl + "/post", payload, "database");
        }

        private void CheckArraysMatch(byte[] expected, byte[] actual)
        {
            Assert.IsNotNull(expected);
            Assert.IsNotNull(actual);

            Assert.AreEqual(expected.Length, actual.Length);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }
    }
}
