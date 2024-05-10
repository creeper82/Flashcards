namespace Flashcards;

using Flashcards;
using SharpViews;

public static partial class App
{
    public static void DeckDetails(Deck deck)
    {
        CLI.Screens.DeckDetails(deck);
        ConsoleInput.WaitForAnyKey();
    }
}