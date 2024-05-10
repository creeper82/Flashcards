namespace Flashcards;
using SharpViews;

public static partial class App
{
    public static void Help(string dbPath = "Unknown path")
    {
        CLI.Screens.Help(dbPath);
        ConsoleInput.WaitForAnyKey();
    }

}