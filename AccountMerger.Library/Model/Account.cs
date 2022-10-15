using AccountMerger.Library.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountMerger.Library.Model
{
    public class Account : IAccount
    {
        public Account()
        {
            Application = string.Empty;
            Emails = new HashSet<string>();
            Name = string.Empty;
        }

        public Account(string application, ISet<string> emails, string name)
        {
            Application = application;
            Emails = emails;
            Name = name;
        }

        public string Application { get; set; }
        public ISet<string> Emails { get; set; }
        public string Name { get; set; }
    }
}
