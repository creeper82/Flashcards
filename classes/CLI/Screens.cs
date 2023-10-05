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
                    (decks.Any() ? DeckList(decks, selectedDeckIndex) : CenteredText("You have no decks. Add a new deck with [N]")),

                    "Flashcards"
                    )
        );

        // Display options
        Console.WriteLine(
            KeyboardActionList(decks.Any() ? KeyboardActions.DeckScreen : KeyboardActions.DeckScreenEmpty)
        );
    }

    internal static void Deck(Deck deck)
    {
        ClearConsole();

        Console.WriteLine(
            UiFrame(
                CenteredText($"The selected deck has ID {deck.Id}") + "\n" +
                CenteredText($"Created at {deck.CreationTimestamp.ToLocalTime()}"),
                deck.Name
            )
        );

        // Display options
        Console.WriteLine(KeyboardActionList(new() { new("enter", "go back") }));
    }
}