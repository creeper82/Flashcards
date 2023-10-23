namespace FlashcardsApp;

using Flashcards;
using CLI;

public static partial class App
{
    public static void DeckDetails(Deck deck)
    {
        Screens.DeckDetails(deck);
        Console.ReadKey();
    }
}