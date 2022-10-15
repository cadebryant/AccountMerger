using System.Reflection;

namespace AccountMerger.UnitTests
{
    [TestClass]
    public class AccountMergerTest
    {
        private string _accountsJson = string.Empty;
        private string _accountsFileName = "accounts.join";

        [TestMethod]
        public void TestCanReadJsonFile()
        {
            using (var fileStream = Assembly.GetExecutingAssembly().GetEmbeddedResourceFileStream(_accountsFileName))
            {
                using (var reader = new StreamReader(fileStream))
                {
                    _accountsJson = reader.ReadToEnd();
                }
            }
            Assert.IsNotNull(_accountsJson);
        }
    }
}