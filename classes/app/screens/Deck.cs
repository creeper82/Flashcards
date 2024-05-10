namespace Flashcards;

public static partial class App
{
    public static void Deck(FlashcardsDatabase database, Deck deck)
    {
        bool running = true;

        while (running)
        {
            CLI.Screens.Deck(deck);
            running = Logic.HandleDeck(database, deck);
        }

    }
}