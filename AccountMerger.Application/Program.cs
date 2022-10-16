using AccountMerger.Library;
using AccountMerger.Library.Interface;
using AccountMerger.Library.Merger;
using AccountMerger.Library.Merger.Impl;
using AccountMerger.Library.Merger.Interface;
using AccountMerger.Library.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

IObjectMerger<IAccount, Person> merger = new AccountPersonMerger();
var jsonSettings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
var acctsPath = "Data/accounts.json";
var accountsJson = await File.ReadAllTextAsync(acctsPath);
var accounts = JsonConvert.DeserializeObject<IEnumerable<Account>>(accountsJson, jsonSettings);
if (accounts == null || !accounts.Any())
{
    Console.WriteLine("Accounts could not be parsed from JSON.");
    return;
}
var results = merger.Merge(accounts);
if (results == null || !results.Any())
{
    Console.WriteLine("Accounts could not be merged.");
    return;
}
Console.WriteLine(JsonConvert.SerializeObject(results, jsonSettings));
