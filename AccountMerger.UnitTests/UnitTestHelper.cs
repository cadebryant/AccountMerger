using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AccountMerger.UnitTests
{
    internal static class UnitTestHelper
    {
        private static string _accountsFileName = Path.Combine("Resources.accounts.json");
        private static string _resultsFileName = Path.Combine("Resources.expectedResult.json");

        internal static Stream GetEmbeddedResourceFileStream(this Assembly assembly, string fileName)
        {
            if (assembly == null)
                throw new ArgumentNullException(nameof(assembly));
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentNullException(nameof(fileName));

            var resourcePath = String.Format("{0}.{1}",
                Regex.Replace(assembly.ManifestModule.Name, @"\.(exe|dll)$",
                      string.Empty, RegexOptions.IgnoreCase), fileName);

            var stream = assembly.GetManifestResourceStream(resourcePath);
            if (stream == null)
                throw new ArgumentException($"Embedded resource file '{fileName}' could not be found.");
            return stream;
        }

        internal static async Task<string> GetTestAccountRecords()
        {
            using (var fileStream = Assembly.GetExecutingAssembly().GetEmbeddedResourceFileStream(_accountsFileName))
            {
                using (var reader = new StreamReader(fileStream))
                {
                    return await reader.ReadToEndAsync();
                }
            }
        }

        internal static async Task<string> GetTestExpectedResult()
        {
            using (var fileStream = Assembly.GetExecutingAssembly().GetEmbeddedResourceFileStream(_resultsFileName))
            {
                using (var reader = new StreamReader(fileStream))
                {
                    return await reader.ReadToEndAsync();
                }
            }
        }
    }
}
