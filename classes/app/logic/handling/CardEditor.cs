namespace FlashcardsApp;

using Flashcards;

public static partial class Logic
{
    public class HandleCardEditorResult
    {
        public class SaveChanges : HandleCardEditorResult { }

        public class CancelChanges : HandleCardEditorResult { }

        public class ContinueLoop : HandleCardEditorResult { }
    }

    public static HandleCardEditorResult HandleCardEditor(Card card)
    {
        ConsoleKey consoleKey = CLI.ConsoleInput.GetConsoleKey();

        switch (consoleKey)
        {
            case ConsoleKey.Enter:
            case ConsoleKey.Spacebar:
                return new HandleCardEditorResult.SaveChanges();
            case ConsoleKey.F:
            case ConsoleKey.UpArrow:
                string newFront = CLI.Dialogs.Input("Edit front", $"Currently: {card.Front}").Trim();
                if (newFront != "") card.Front = newFront;
                break;
            case ConsoleKey.B:
            case ConsoleKey.DownArrow:
                string newBack = CLI.Dialogs.Input("Edit back", $"Currently: {card.Back}").Trim();
                if (newBack != "") card.Back = newBack;
                break;
            case ConsoleKey.S:
                (card.Back, card.Front) = (card.Front, card.Back);
                break;
            case ConsoleKey.Escape:
                return new HandleCardEditorResult.CancelChanges();
        }

        return new HandleCardEditorResult.ContinueLoop();

    }
}