using System.Reflection;
using AccountMerger.Library.Interface;
using AccountMerger.Library.Merger.Impl;
using AccountMerger.Library.Merger.Interface;
using AccountMerger.Library.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AccountMerger.UnitTests
{
    [TestClass]
    public class AccountMergerTest
    {
        private string _accountsJson = string.Empty;
        private string _expResultsJson = string.Empty;
        private JsonSerializerSettings _jsonSettings = 
            new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
        private AccountPersonMerger _merger = new AccountPersonMerger();

        [TestMethod]
        public async Task TestCanParseJsonResource()
        {
            _accountsJson = await UnitTestHelper.GetTestAccountRecords();
            Assert.IsNotNull(_accountsJson);
            var accounts = JsonConvert.DeserializeObject<IEnumerable<Account>>(_accountsJson, _jsonSettings);
            Assert.IsNotNull(accounts);
        }

        [TestMethod]
        public async Task TestCanMergeObjects_HappyPath()
        {
            _accountsJson = await UnitTestHelper.GetTestAccountRecords();
            Assert.IsNotNull(_accountsJson);
            var accounts = JsonConvert.DeserializeObject<IEnumerable<Account>>(_accountsJson, _jsonSettings);
            Assert.IsNotNull(accounts);
            var mergedPersons = _merger.Merge(accounts);
            Assert.IsNotNull(mergedPersons);
            var serialized = JsonConvert.SerializeObject(mergedPersons, _jsonSettings);
            Assert.IsNotNull(serialized);
            _expResultsJson =  await UnitTestHelper.GetTestExpectedResult();
            var deser = JsonConvert.DeserializeObject<IEnumerable<Person>>(_expResultsJson, _jsonSettings);
            Assert.IsNotNull(deser);
            Assert.AreEqual(deser.Count(), mergedPersons.Count());
            foreach (var personTup in mergedPersons.Zip(deser))
            {
                Assert.AreEqual(personTup.First.Name, personTup.Second.Name);
                Assert.AreEqual(
                    String.Join("", personTup.First.Emails.OrderBy(e => e)),
                    String.Join("", personTup.Second.Emails.OrderBy(e => e)));
                Assert.AreEqual(
                    String.Join("", personTup.First.Applications.OrderBy(a => a)),
                    String.Join("", personTup.Second.Applications.OrderBy(a => a)));
            }
        }

        [TestMethod]
        public async Task TestCanMergeObjects_MassiveDataset()
        {
            _accountsJson = await UnitTestHelper.GetTestAccountRecords(true);
            Assert.IsNotNull(_accountsJson);
            var accounts = JsonConvert.DeserializeObject<IEnumerable<Account>>(_accountsJson, _jsonSettings);
            Assert.IsNotNull(accounts);
            var mergedPersons = _merger.Merge(accounts);
            Assert.IsNotNull(mergedPersons);
            var serialized = JsonConvert.SerializeObject(mergedPersons, _jsonSettings);
            Assert.IsNotNull(serialized);
            _expResultsJson = await UnitTestHelper.GetTestExpectedResult();
            var deser = JsonConvert.DeserializeObject<IEnumerable<Person>>(_expResultsJson, _jsonSettings);
            Assert.IsNotNull(deser);
            Assert.AreEqual(deser.Count(), mergedPersons.Count());
            foreach (var personTup in mergedPersons.Zip(deser))
            {
                Assert.AreEqual(personTup.First.Name, personTup.Second.Name);
                Assert.AreEqual(
                    String.Join("", personTup.First.Emails.OrderBy(e => e)),
                    String.Join("", personTup.Second.Emails.OrderBy(e => e)));
                Assert.AreEqual(
                    String.Join("", personTup.First.Applications.OrderBy(a => a)),
                    String.Join("", personTup.Second.Applications.OrderBy(a => a)));
            }
        }
    }
}