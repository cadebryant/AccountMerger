using System.Reflection;
using AccountMerger.Library.Interface;
using AccountMerger.Library.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AccountMerger.UnitTests
{
    [TestClass]
    public class AccountMergerTest
    {
        private string _accountsJson = string.Empty;
        private string _accountsFileName = Path.Combine( "Resources.accounts.json");
        private JsonSerializerSettings _jsonSettings = 
            new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };

        [TestMethod]
        public void TestCanParseJsonResource()
        {
            using (var fileStream = Assembly.GetExecutingAssembly().GetEmbeddedResourceFileStream(_accountsFileName))
            {
                using (var reader = new StreamReader(fileStream))
                {
                    _accountsJson = reader.ReadToEnd();
                }
            }
            Assert.IsNotNull(_accountsJson);
            var obj = JsonConvert.DeserializeObject<IEnumerable<Account>>(_accountsJson, _jsonSettings);
            Assert.IsNotNull(obj);
        }
    }
}