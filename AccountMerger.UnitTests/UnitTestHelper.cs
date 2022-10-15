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
    }
}
