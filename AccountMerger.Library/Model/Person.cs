using AccountMerger.Library.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountMerger.Library.Model
{
    public class Person : IPerson
    {
        public Person()
        {
            Applications = new HashSet<string>();
            Emails = new HashSet<string>();
            Name = string.Empty;
        }

        public ISet<string> Applications { get; set; }
        public ISet<string> Emails { get; set; }
        public string Name { get; set; }
    }
}
