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
string? acctsPath;

Console.Write("Type D to merge the default accounts file or U to use your own accounts file: ");
var selection = Console.ReadKey();
if (selection.Key == ConsoleKey.U)
{
    Console.WriteLine();
    Console.Write("Enter the full file path of your JSON data file: ");
    acctsPath = Console.ReadLine();
}
else if (selection.Key == ConsoleKey.D)
{
    acctsPath = "Data/accounts.json";
}
else
{
    Console.WriteLine();
    Console.WriteLine("You have entered an invalid key.  Exiting...");
    return;
}

if (string.IsNullOrWhiteSpace(acctsPath))
{
    Console.WriteLine();
    Console.WriteLine("No filepath was entered.  Exiting...");
    return;
}
Console.WriteLine();
var accountsJson = await File.ReadAllTextAsync(acctsPath);
var accounts = JsonConvert.DeserializeObject<IEnumerable<Account>>(accountsJson, jsonSettings);
if (accounts == null || !accounts.Any())
{
    Console.WriteLine("Accounts could not be parsed from JSON.");
    return;
}

Console.WriteLine("Merging accounts...");
var results = merger.Merge(accounts);
if (results == null || !results.Any())
{
    Console.WriteLine("Accounts could not be merged.");
    return;
}
Console.WriteLine("Finished merging accounts.");
Console.WriteLine();
Console.WriteLine(JsonConvert.SerializeObject(results, jsonSettings));
