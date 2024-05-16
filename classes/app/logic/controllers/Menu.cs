namespace Flashcards;

using SharpViews;

public static partial class Logic
{
    public enum HandleMenuResult {
        ContinueLoop, MoveForward, MoveBackward, OpenHelp, OpenDeck, Exit
    }
    public static HandleMenuResult HandleMenu(FlashcardsDatabase database, ChoiceList<Deck> deckChoiceList)
    {
        ConsoleKey consoleKey = ConsoleInput.GetConsoleKey();

        switch (consoleKey)
        {
            // Navigation
            case ConsoleKey.UpArrow:
                return HandleMenuResult.MoveBackward;
            case ConsoleKey.DownArrow:
                return HandleMenuResult.MoveForward;
            // New deck
            case ConsoleKey.N:
                Deck? newDeck = NewDeck(database);
                if (newDeck is not null) deckChoiceList.MoveToChoice(newDeck);
                break;
            // Help menu
            case ConsoleKey.H:
                return HandleMenuResult.OpenHelp;
            // Exit app
            case ConsoleKey.Escape:
                return HandleMenuResult.Exit;
        }
        // Only if the user has decks
        if (deckChoiceList.SelectedChoice is not null)
        {
            switch (consoleKey)
            {
                // Navigation
                case ConsoleKey.Spacebar:
                case ConsoleKey.Enter:
                    return HandleMenuResult.OpenDeck;
                // Deck editing
                case ConsoleKey.Delete:
                    RemoveDeck(database, deckChoiceList.SelectedChoice);
                    break;
                case ConsoleKey.R:
                case ConsoleKey.F2:
                    Deck renamedDeck = RenameDeck(database, deckChoiceList.SelectedChoice);
                    deckChoiceList.MoveToChoice(renamedDeck);
                    break;
            }
        }
        
        return HandleMenuResult.ContinueLoop;
    }
}