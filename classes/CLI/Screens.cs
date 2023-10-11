using Flashcards;
namespace CLI;

using static Components;

public static class Screens
{
    public static void Menu(IEnumerable<Deck> decks, int selectedDeckIndex, int startIndex = 0)
    {
        ClearConsole();
        // Display menu
        Console.WriteLine(
            UiFrame(
                    inner: CenteredText("Welcome to Flashcards!\nHere are your decks:\n") +
                    (
                        decks.Any()
                        ? DeckList(decks, selectedDeckIndex, startIndex)
                        : CenteredText("You have no decks. Add a new deck with [N]")
                    ),
                    title: "Flashcards",
                    verticalScroll: true
                    )
        );

        // Display keyboard actions
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
                CenteredText(
                    deck.Cards.Any()
                    ? $"This deck has {deck.Cards.Count} cards"
                    : "This deck is empty\nOpen card editor with [C]"
                ),
                deck.Name
            )
        );

        // Display keyboard actions
        Console.WriteLine(KeyboardActionList(deck.Cards.Any() ? KeyboardActions.DeckScreen : KeyboardActions.DeckScreenEmpty));
    }

    internal static void DeckCardList(Card? card, int currentCardNumber, int maxCardNumber, string deckName)
    {
        ClearConsole();

        if (card == null)
        {
            Console.WriteLine(
                UiFrame(
                    CenteredText(
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
                    inner: CenteredText(
                        $"Card {currentCardNumber} of {maxCardNumber}  **  ASCENDING"
                    ) + "\n\n" +
                    DeckCard(card, true),
                    title: deckName,
                    horizontalScroll: true
                )
            );
        }
        // Display keyboard actions
        Console.WriteLine(KeyboardActionList(card is not null ? KeyboardActions.DeckCardListScreen : KeyboardActions.DeckCardListScreenEmpty));
    }

    internal static void CardEditor(Card card) {
        ClearConsole();

        Console.WriteLine(
                UiFrame(
                    DeckCard(card, true),
                    "Edit card"
                )
        );

        Console.WriteLine(KeyboardActionList(KeyboardActions.CardEditorScreen));
    }

    internal static void Help()
    {
        ClearConsole();

        Console.WriteLine(
            UiFrame(
                inner: (
                    CenteredText("GENERAL") + "\n" +
                    "Welcome to the Flashcards app. This app can serve as your study helper.\n" +
                    "To get started, create a deck, go to the card editor and add some cards. " +
                    "The app controls will always be on the bottom of the screen\n\n" +

                    CenteredText("CONTROLS") + "\n" +
                    "The app is controlled using your keyboard. " +
                    "Most of the keys are repetitive and keep their functions across different screens\n" +
                    "The < > and ∧ ∨ arrows signify either horizontal or vertical scrolling with arrow keys is possible, " +
                    "for example to move your selection in a list\n\n" +
                    "Here are some commonly used controls and their alternatives:\n" +
                    "Enter or SPACE  -  select item / open screen\n" +
                    "R or F2  -  rename currently selected item\n" +
                    "N  -  add a new item\n" +
                    "Esc  -  go back\n\n" +

                    CenteredText("AUTHOR") + "\n" +
                    "The whole project is made by me (creeper82), you can see my other projects there ^^\n" +
                    "Have fun using the app! Report issues on the GitHub page, if any.\n\n" +
                    "Press any key to go back..."
                ),
                title: "Flashcards"
            )
        );
    }
}