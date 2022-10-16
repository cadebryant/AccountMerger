# AccountMerger
AccountMerger is a .NET 6 console application which does the following:
1. Reads a JSON input file, [accounts.json](AccountMerger.Application/Data/accounts.json), which is stored in the [Data folder](AccountMerger.Application/Data/) of the [AccountMerger.Application subproject](AccountMerger.Application/).
2. Deserializes the JSON into an `IEnumerable` of objects that implement the interface `IAccount`.
    - Each `IAccount` entry represents a customer account.
3. Merges each `IAccount` record into record of type `Person`.
    - If two accounts share email addresses in common, they are assumed to be the same person and are merged into a single record.
      - Email addresses and applications from both of these records are merged into the `Emails` and `Applications` collections of the resultant `Person` object.
      - Duplicate emails/applications are discarded for each person record - in other words, each email or application collection per person will be a `union` of all the emails or applications for that customer that exist in any account record.
      - The process continues recursively until no more merging is possible - i.e. until no final records share email addresses in common.
4. Serializes the resulting collection of `Person` records and displays the resulting JSON to the console.

## Object Model
The application includes two classes, [`Account`](AccountMerger.Library/Model/Account.cs) and [`Person`](AccountMerger.Library/Model/Person.cs).  The former is a representation of the input account objects, and the latter is a representation of the output person objects.

The merging work is performed in the [`AccountPersonMerger`](AccountMerger.Library/Merger/Impl/AccountPersonMerger.cs) service.  This class implements [`IObjectMerger`](AccountMerger.Library/Merger/Interface/IObjectMerger.cs) and can thus be substituted with any class implementing that interface.

## Unit Testing
This project provides a [unit testing subproject](AccountMerger.UnitTests/), which includes some basic functionality tests, in addition to sample test data (input and expected output) embedded as resources.
