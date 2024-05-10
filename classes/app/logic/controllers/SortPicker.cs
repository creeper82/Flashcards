namespace Flashcards;
using SharpViews;

public static partial class Logic
{
    public static bool HandleSortPicker(ChoiceList<string> choiceList)
    {
        ConsoleKey consoleKey = ConsoleInput.GetConsoleKey();

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