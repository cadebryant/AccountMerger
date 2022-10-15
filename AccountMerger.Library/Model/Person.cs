using AccountMerger.Library.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountMerger.Library.Model
{
    internal class Person : IPerson
    {
        internal Person()
        {
            Applications = new HashSet<string>();
            Emails = new HashSet<string>();
            Name = string.Empty;
        }

        internal Person(ISet<string> applications, ISet<string> emails, string name)
        {
            Applications = applications;
            Emails = emails;
            Name = name;
        }

        public ISet<string> Applications { get; set; }
        public ISet<string> Emails { get; set; }
        public string Name { get; set; }
    }
}
