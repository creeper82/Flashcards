using Flashcards;
using FlashcardsApp;
using Microsoft.Data.Sqlite;

var db = new FlashcardsDatabase();

Console.WriteLine($"Database path is {db.Path}");

try
{
    App.Start(db);
}
catch (SqliteException)
{
    Console.WriteLine("ERROR. The database is corrupt");
    throw;
}