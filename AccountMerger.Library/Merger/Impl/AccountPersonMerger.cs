using AccountMerger.Library.Interface;
using AccountMerger.Library.Merger.Interface;
using AccountMerger.Library.Model;
using AccountMerger.Library.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountMerger.Library.Merger.Impl
{
    public class AccountPersonMerger : IObjectMerger<IAccount, Person>
    {
        private Dictionary<string, List<IAccount>> _emailGroupings = new Dictionary<string, List<IAccount>>();

        public IEnumerable<Person> Merge(IEnumerable<IAccount> source)
        {
            //This will never happen with the sample data,
            //but one should always validate that params/variables are not null/empty
            //to avoid null reference errors and exit early if there are not data items:
            if (source == null || !source.Any())
                return Enumerable.Empty<Person>();

            var personList = new List<Person>();
            
            if (source.Count() == 1) //Only one record, nothing to merge with:
            {
                var singleAcct = source.Single();
                return new[]
                {
                    new Person
                    {
                        Applications = new[] { singleAcct.Application }.ToHashSet(),
                        Emails = singleAcct.Emails,
                        Name = singleAcct.Name
                    }
                };
            }

            //This stores the first email in each account's set of emails
            //(could be any email in the set but first is safest since some accounts may have only one): 
            var parentEmails = new Dictionary<string, string>();
            //This stores all the other emails for each account:
            var allEmails = new Dictionary<string, string>();
            //This stores the emails grouped by their parent:
            var emailGroupings = new Dictionary<string, SortedSet<(string, string)>>();

            foreach (var sourceItem in source)
            {
                if (source == null) continue;
                var skipCount = 0; //Math.Min(sourceItem.Emails.Count - 1, 1);
                foreach (string email in sourceItem.Emails.Skip(skipCount))
                {
                    parentEmails[email] = email;
                    allEmails[email] = sourceItem.Emails.First();
                }
            }

            foreach (var sourceItem in source)
            {
                var skipCount = 0; //Math.Min(sourceItem.Emails.Count - 1, 1);
                string parent = parentEmails.FindRecursively(sourceItem.Emails.Skip(skipCount).First());
                skipCount = 0; //Math.Min(sourceItem.Emails.Count - 1, 2);
                foreach (var email in sourceItem.Emails.Skip(skipCount))
                {
                    parentEmails[parentEmails.FindRecursively(email)] = parent;
                }
            }

            foreach (var sourceItem in source)
            {
                var skipCount = 0; //Math.Min(sourceItem.Emails.Count - 1, 1);
                string parent = parentEmails.FindRecursively(sourceItem.Emails.Skip(skipCount).First());
                if (!emailGroupings.ContainsKey(parent))
                {
                    emailGroupings[parent] = new SortedSet<(string, string)>();
                }
                foreach (var email in sourceItem.Emails.Skip(skipCount))
                {
                    emailGroupings[parent].Add((email, sourceItem.Name));  
                }
            }

            foreach (var group in emailGroupings)
            {
                var person = new Person();
                person.Emails.Add(allEmails[group.Key]);
                person.Name = group.Value.First().Item2;
                foreach (var entry in group.Value)
                {
                    person.Emails.Add(entry.Item1);
                }
                personList.Add(person);
            }
            return personList;
        }
    }
}
