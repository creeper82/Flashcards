using Flashcards;
using FlashcardsApp;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

FlashcardsDatabase db;
bool running = true;

while (running)
{
    try
    {
        Console.WriteLine("\nLoading...");
        db = new FlashcardsDatabase();
        App.Menu(db);
        running = false;
    }

    catch (DbUpdateConcurrencyException e)
    {
        Console.WriteLine("\n ---- ERROR\n");
        Console.WriteLine(
            "A database conflict appeared.\n" +
            "That means the database was changed by some external program or a second window of this app."
        );
        Console.WriteLine("Number of unexpected changes is at least " + e.Entries.Count + " (or possibly more)");
        Console.WriteLine("Press any key to restart the app and reflect the unexpected changes");
        Console.WriteLine("At the moment there is no way to ignore these changes");

        Console.ReadKey();

    }

    catch (DbUpdateException e)
    {
        Console.WriteLine("\n ---- ERROR\n");
        if (e.InnerException is SqliteException sqliteException)
        {
            switch (sqliteException.SqliteErrorCode)
            {
                case 5:
                    Console.WriteLine("The database is locked\nTry closing other windows of this app, if any are open\n");
                    break;
                default:
                    Console.WriteLine("The database error was unrecognized");
                    break;
            }
        }
        else
        {
            Console.WriteLine("Can't save changes in the database.");
            Console.WriteLine("There is a chance that an external app modified the database");
            Console.WriteLine("Error message: " + e.Message);
        }

        Console.WriteLine("\nPress R to try reloading the app, or any other key to exit");
        running = Console.ReadKey().Key == ConsoleKey.R;
    }


}
