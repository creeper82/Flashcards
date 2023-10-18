namespace FlashcardsApp;

public static partial class Logic
{
    public static bool HandleSortPicker(CLI.ChoiceList<string> choiceList)
    {
        ConsoleKey consoleKey = Console.ReadKey().Key;

        switch (consoleKey)
        {
            case ConsoleKey.Enter:
                return false;
            case ConsoleKey.UpArrow:
                choiceList.MoveBackward();
                break;
            case ConsoleKey.DownArrow:
                choiceList.MoveForward();
                break;
        }

        return true;
    }
}