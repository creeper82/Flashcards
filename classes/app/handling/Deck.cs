namespace FlashcardsApp;

using Flashcards;

public static partial class Interactions
{
    public static bool HandleDeck(FlashcardsDatabase database, Deck deck)
    {
        ConsoleKey consoleKey = Console.ReadKey().Key;

        switch (consoleKey)
        {
            case ConsoleKey.Enter:
            case ConsoleKey.Spacebar:
                App.StudySession(database, deck.Cards);
                break;
            case ConsoleKey.Delete:
                RemoveDeckAction(database, deck);
                return false;
            case ConsoleKey.R:
            case ConsoleKey.F2:
                RenameDeckAction(database, deck);
                break;
            case ConsoleKey.C:
                App.DeckCardList(database, deck);
                break;
            case ConsoleKey.I:
                //TODO: implement deck details
                break;
            case ConsoleKey.Escape:
            case ConsoleKey.Q:
                return false;
        }

        return true;
    }
}