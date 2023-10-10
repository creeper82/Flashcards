namespace FlashcardsApp;

using Flashcards;
using CLI;

public static partial class App
{
    public static void Deck(FlashcardsDatabase database, Deck deck)
    {
        bool running = true;

        while (running)
        {
            Screens.Deck(deck);
            running = Interactions.HandleDeck(database, deck);
        }

    }
}