using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountMerger.Library.Interface
{
    public interface IPerson
    {
        public ISet<string> Applications { get; set; }
        public ISet<string> Emails { get; set; }
        public string Name { get; set; }
    }
}
