using Flashcards;
using FlashcardsApp;
using Microsoft.Data.Sqlite;

var db = new FlashcardsDatabase();

try
{
    App.Menu(db);
}
catch (SqliteException)
{
    Console.WriteLine("ERROR. The database is corrupt");
    throw;
}