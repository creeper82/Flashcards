namespace FlashcardsApp;

using Flashcards;
using CLI;

public static partial class Interactions
{
    public static bool HandleMenu(FlashcardsDatabase database, ChoiceList<Deck> deckChoiceList)
    {
        ConsoleKey consoleKey = Console.ReadKey().Key;

        switch (consoleKey)
        {
            // Navigation
            case ConsoleKey.UpArrow:
                deckChoiceList.MoveBackward();
                break;
            case ConsoleKey.DownArrow:
                deckChoiceList.MoveForward();
                break;
            // New deck
            case ConsoleKey.N:
                Deck? newDeck = NewDeckAction(database);
                if (newDeck != null) deckChoiceList.MoveToChoice(newDeck);
                break;
            // Exit app
            case ConsoleKey.Escape:
                return false;
        }
        // Only if the user has decks
        if (deckChoiceList.SelectedItem != null)
        {
            switch (consoleKey)
            {
                // Navigation
                case ConsoleKey.Spacebar:
                case ConsoleKey.Enter:
                    App.Deck(database, deckChoiceList.SelectedItem);
                    break;
                // Deck editing
                case ConsoleKey.Delete:
                    RemoveDeckAction(database, deckChoiceList.SelectedItem);
                    break;
                case ConsoleKey.R:
                case ConsoleKey.F2:
                    Deck renamedDeck = RenameDeckAction(database, deckChoiceList.SelectedItem);
                    deckChoiceList.MoveToChoice(renamedDeck);
                    break;
            }

        }

        return true;
    }
}