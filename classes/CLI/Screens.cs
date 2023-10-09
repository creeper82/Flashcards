using Flashcards;
namespace CLI;

using static Components;

public static class Screens
{
    public static void Menu(IEnumerable<Deck> decks, int selectedDeckIndex)
    {
        ClearConsole();
        // Display menu
        Console.WriteLine(
            UiFrame(
                    MultilineCenteredText("Welcome to Flashcards!\nHere are your decks:\n") +
                    (
                        decks.Any()
                        ? DeckList(decks, selectedDeckIndex)
                        : CenteredText("You have no decks. Add a new deck with [N]")
                    ),
                    "Flashcards"
                    )
        );

        // Display options
        Console.WriteLine(
            KeyboardActionList(decks.Any() ? KeyboardActions.DeckListScreen : KeyboardActions.DeckListScreenEmpty)
        );
    }

    internal static void Deck(Deck deck)
    {
        ClearConsole();

        Console.WriteLine(
            UiFrame(
                // CenteredText($"The selected deck has ID {deck.Id}") + "\n" +
                // CenteredText($"Created at {deck.CreationTimestamp.ToLocalTime()}")
                MultilineCenteredText(
                    deck.Cards.Any()
                    ? $"This deck has {deck.Cards.Count} cards"
                    : "This deck is empty\nOpen card editor with [C]"
                ),
                deck.Name
            )
        );

        // Display options
        Console.WriteLine(KeyboardActionList(deck.Cards.Any() ? KeyboardActions.DeckScreen : KeyboardActions.DeckScreenEmpty));
    }

    internal static void CardEditor(Card? card, int currentCardNumber, int maxCardNumber, string deckName)
    {
        ClearConsole();

        if (card == null)
        {
            Console.WriteLine(
                UiFrame(
                    MultilineCenteredText(
                        "This deck has no cards\n" +
                        "Add a new card with [N]"
                    ),
                    deckName
                )
            );
        }
        else
        {
            Console.WriteLine(
                UiFrame(
                    CenteredText(
                        $"Card {currentCardNumber} of ${maxCardNumber}  **  ASCENDING"
                    ) + "\n\n" +
                    DeckCard(card, true),
                    deckName
                )
            );
        }
    }
}