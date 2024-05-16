namespace Flashcards;
using SharpViews;

public static partial class Logic
{
    public enum HandleSortPickerResult {
        ContinueLoop, MoveForward, MoveBackward, Exit
    }
    public static HandleSortPickerResult HandleSortPicker()
    {
        ConsoleKey consoleKey = ConsoleInput.GetConsoleKey();

        return consoleKey switch
        {
            ConsoleKey.Enter => HandleSortPickerResult.Exit,
            ConsoleKey.UpArrow => HandleSortPickerResult.MoveBackward,
            ConsoleKey.DownArrow => HandleSortPickerResult.MoveForward,
            _ => HandleSortPickerResult.ContinueLoop,
        };
    }
}