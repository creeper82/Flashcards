using Flashcards;
namespace CLI;

using static Components;

public partial class Screens
{
    internal static void Deck(Deck deck)
    {
        ClearConsole();

        Console.WriteLine(
            UiFrame(
                CenteredText(
                    deck.Cards.Any()
                    ? $"This deck has {deck.Cards.Count} cards"
                    : "This deck is empty\nOpen card editor with [C]"
                ),
                deck.Name
            )
        );

        // Display keyboard actions
        Console.WriteLine(
            KeyboardActionList(
                deck.Cards.Any()
                ? KeyboardActions.DeckScreen
                : KeyboardActions.DeckScreenEmpty
            )
        );
    }
}