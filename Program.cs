using Flashcards;
using SharpViews;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

FlashcardsDatabase db = new();
bool running = true;

while (running)
{
    try
    {
        Console.Clear();
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

        ConsoleInput.WaitForAnyKey();

    }

    catch (DbUpdateException e)
    {
        if (e.InnerException is SqliteException sqliteException) running = HandleSqliteError(sqliteException.SqliteErrorCode);
        else
        {
            Console.WriteLine("\n ---- ERROR\n");
            Console.WriteLine("Unknown error encountered. Can't save changes in the database.");
            Console.WriteLine("There is a chance that an external app modified the database, or that the database is corrupt.");
            Console.WriteLine("Error message: " + e.Message);
            Console.WriteLine("Inner error message (if any): " + e.InnerException?.Message);

            Console.WriteLine("\nPress R to try reloading the app, or any other key to exit");
            running = ConsoleInput.GetConsoleKey() == ConsoleKey.R;

        }
    }

    catch (SqliteException e)
    {
        running = HandleSqliteError(e.SqliteErrorCode, e.Message, db.Path);
    }
}

static bool HandleSqliteError(int? code = null, string? message = null, string dbPath = "Unknown path")
{
    Console.WriteLine("\n ---- ERROR\n");
    switch (code)
    {
        case 1:
            Console.WriteLine("Fatal error: The database structure may have unexpectedly changed");
            break;
        case 5:
            Console.WriteLine("The database is locked\nTry closing other windows of this app, if any are open\n");
            break;
        default:
            Console.WriteLine("The database error was unrecognized");
            break;
    }
    Console.WriteLine("Underlying error message: " + message ?? "Message not available");
    Console.WriteLine("\nPress R to try reloading the app, or any other key to exit");
    Console.WriteLine("If the error persists, press H for more info");

    ConsoleKey consoleKey = ConsoleInput.GetConsoleKey();

    switch (consoleKey)
    {
        case ConsoleKey.H:
            ShowHelpScreen(dbPath);
            Console.WriteLine("\nPress R to try reloading the app, or any other key to exit");
            return ConsoleInput.GetConsoleKey() == ConsoleKey.R;
        case ConsoleKey.R:
            return true;
        default:
            return false;
    }
}

static void ShowHelpScreen(string dbPath = "Unknown path")
{
    Console.WriteLine("\nHELP");
    Console.WriteLine(
        "If the error continues to show up when loading the app:\n" +
        "\t1. Check if the app isn't open in multiple windows\n" +
        "\t2. Reboot the device\n\n" +
        "LAST RESORT: If the steps still don't help, and you're getting a fatal error (CODE 1), then you may need to delete the whole database\n" +
        "The database file is located at the following path:   " + dbPath + "\n\n" +
        "If you're experienced, you may inspect it and try to repair it manually. " +
        "Otherwise, just delete the database file and restart the app\n" +
        "WARNING: DELETING THE DATABASE WILL RESULT IN COMPLETE DATA LOSS. ALL DECKS AND CARDS WILL BE LOST FOREVER"
    );
    Console.WriteLine("\nPress any key to close the help screen");
    ConsoleInput.WaitForAnyKey();
}