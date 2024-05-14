namespace Flashcards;

using SharpViews;

public static partial class Logic
{
    public static bool HandleDeck(FlashcardsDatabase database, Deck deck)
    {
        ConsoleKey consoleKey = ConsoleInput.GetConsoleKey();

        if (deck.Cards.Count != 0)
        {
            switch (consoleKey)
            {
                case ConsoleKey.Enter:
                case ConsoleKey.Spacebar:
                    App.StudySession(database, deck.Cards);
                    break;
            }
        }

        switch (consoleKey)
        {
            case ConsoleKey.Delete:
                if (RemoveDeck(database, deck)) return false;
                break;
            case ConsoleKey.R:
            case ConsoleKey.F2:
                RenameDeck(database, deck);
                break;
            case ConsoleKey.C:
                App.DeckCardList(database, deck);
                break;
            case ConsoleKey.I:
                App.DeckDetails(deck);
                break;
            case ConsoleKey.Escape:
            case ConsoleKey.Q:
                return false;
        }

        return true;
    }
}