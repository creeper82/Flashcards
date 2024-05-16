namespace Flashcards;

using SharpViews;

public static partial class Logic
{
    public enum HandleCardEditorResult {
        ContinueLoop, SaveChanges, CancelChanges, EditFront, EditBack, Swap
    }

    public static HandleCardEditorResult HandleCardEditor()
    {
        ConsoleKey consoleKey = ConsoleInput.GetConsoleKey();

        return consoleKey switch
        {
            ConsoleKey.Enter or ConsoleKey.Spacebar => HandleCardEditorResult.SaveChanges,
            ConsoleKey.F or ConsoleKey.UpArrow => HandleCardEditorResult.EditFront,
            ConsoleKey.B or ConsoleKey.DownArrow => HandleCardEditorResult.EditBack,
            ConsoleKey.S => HandleCardEditorResult.Swap,
            ConsoleKey.Escape => HandleCardEditorResult.CancelChanges,
            _ => HandleCardEditorResult.ContinueLoop,
        };
    }
}