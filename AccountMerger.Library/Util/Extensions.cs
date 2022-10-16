using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AccountMerger.Library.Util
{
    public static class Extensions
    {
        public static string FindRecursively(this Dictionary<string, string> dict, string searchVal)
        {
            if (dict.TryGetValue(searchVal, out var value))
            {
                if (value != searchVal)
                {
                    dict[searchVal] = dict.FindRecursively(value);
                }
            }
            return dict[searchVal];
        }
    }
}
