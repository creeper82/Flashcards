namespace FlashcardsApp;
using CLI;

public static partial class App
{
    public static void Help(string dbPath = "Unknown path")
    {
        Screens.Help(dbPath);
        ConsoleInput.WaitForAnyKey();
    }

}