namespace Flashcards;

using SharpViews;

public static partial class Logic
{
    public enum HandleDeckResult {
        ContinueLoop, OpenStudySession, OpenCardEditor, OpenDeckDetails, Exit
    }
    public static HandleDeckResult HandleDeck(FlashcardsDatabase database, Deck deck)
    {
        ConsoleKey consoleKey = ConsoleInput.GetConsoleKey();

        if (deck.Cards.Count != 0)
        {
            switch (consoleKey)
            {
                case ConsoleKey.Enter:
                case ConsoleKey.Spacebar:
                    return HandleDeckResult.OpenStudySession;
            }
        }

        switch (consoleKey)
        {
            case ConsoleKey.Delete:
                if (RemoveDeck(database, deck)) return HandleDeckResult.Exit;
                break;
            case ConsoleKey.R:
            case ConsoleKey.F2:
                RenameDeck(database, deck);
                break;
            case ConsoleKey.C:
                return HandleDeckResult.OpenCardEditor;
            case ConsoleKey.I:
                return HandleDeckResult.OpenDeckDetails;
            case ConsoleKey.Escape:
            case ConsoleKey.Q:
                return HandleDeckResult.Exit;
        }
        return HandleDeckResult.ContinueLoop;
    }
}