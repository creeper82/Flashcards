namespace Flashcards.CLI;

using static SharpViews.Components;

public partial class Screens
{
    internal static void Deck(Deck deck)
    {
        ClearConsole();

        Console.WriteLine(
            UiFrame(
                CenteredText(
                    deck.Cards.Count != 0
                    ? $"This deck has {deck.Cards.Count} cards"
                    : "This deck is empty\nOpen card editor with [C]"
                ),
                deck.Name
            )
        );

        // Display keyboard actions
        Console.WriteLine(
            KeyboardActionList(
                deck.Cards.Count != 0
                ? KeyboardActions.DeckScreen
                : KeyboardActions.DeckScreenEmpty
            )
        );
    }
}