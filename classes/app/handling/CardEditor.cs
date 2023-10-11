namespace FlashcardsApp;

using Flashcards;

public static partial class Interactions
{
    public class HandleCardEditorResult
    {
        public class SaveChanges : HandleCardEditorResult { }

        public class CancelChanges : HandleCardEditorResult { }

        public class ContinueLoop : HandleCardEditorResult { }
    }
    public static HandleCardEditorResult HandleCardEditor(FlashcardsDatabase database, Card card)
    {
        ConsoleKey consoleKey = Console.ReadKey().Key;

        switch (consoleKey)
        {
            case ConsoleKey.Enter:
            case ConsoleKey.Spacebar:
                return new HandleCardEditorResult.SaveChanges();
            case ConsoleKey.F:
                card.Front = CLI.Dialogs.Input("Edit front", $"Currently: {card.Front}");
                break;
            case ConsoleKey.B:
                card.Back = CLI.Dialogs.Input("Edit back", $"Currently: {card.Back}");
                break;
            case ConsoleKey.Escape:
                return new HandleCardEditorResult.CancelChanges();
        }

        return new HandleCardEditorResult.ContinueLoop();

    }
}