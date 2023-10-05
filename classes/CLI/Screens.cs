using Flashcards;
namespace CLI;

public static partial class CLI
{
    public static void Menu(IEnumerable<Deck> decks, int selectedDeckIndex)
    {
        ClearConsole();
        // Display menu
        Console.WriteLine(
            UiFrame(
                    MultilineCenteredText("Welcome to Flashcards!\nHere are your decks:\n") +
                    DeckList(decks, selectedDeckIndex),

                    "Flashcards"
                    )
        );

        // Display options
        Console.WriteLine(
            KeyboardActionList(KeyboardActions.DeckScreen)
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