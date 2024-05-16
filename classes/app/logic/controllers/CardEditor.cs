namespace Flashcards;

using SharpViews;

public static partial class Logic
{
    public enum HandleCardEditorResult {
        ContinueLoop, SaveChanges, CancelChanges
    }

    public static HandleCardEditorResult HandleCardEditor(Card card)
    {
        ConsoleKey consoleKey = ConsoleInput.GetConsoleKey();

        switch (consoleKey)
        {
            case ConsoleKey.Enter:
            case ConsoleKey.Spacebar:
                return HandleCardEditorResult.SaveChanges;
            case ConsoleKey.F:
            case ConsoleKey.UpArrow:
                string newFront = Dialogs.Input("Edit front", $"Currently: {card.Front}").Trim();
                if (newFront != "") card.Front = newFront;
                break;
            case ConsoleKey.B:
            case ConsoleKey.DownArrow:
                string newBack = Dialogs.Input("Edit back", $"Currently: {card.Back}").Trim();
                if (newBack != "") card.Back = newBack;
                break;
            case ConsoleKey.S:
                (card.Back, card.Front) = (card.Front, card.Back);
                break;
            case ConsoleKey.Escape:
                return HandleCardEditorResult.CancelChanges;
        }

        return HandleCardEditorResult.ContinueLoop;

    }
}