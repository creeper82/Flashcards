using Flashcards;
namespace CLI;

public static partial class CLI {
    public static void Menu(IEnumerable<Deck> decks, int selectedDeckIndex)
    {
        ClearConsole();
        // Display menu
        Console.WriteLine(
            UiFrame(
                (
                    MultilineCenteredText("Welcome to Flashcards!\nHere are your decks:\n") +
                    DeckList(decks, selectedDeckIndex)
                ),
                "Flashcards")
        );
        // Display options
        Console.WriteLine(
            OptionList(Options.MenuOptions)
        );
    }

    internal static void Deck(Deck deck)
    {
        ClearConsole();

        Console.WriteLine(
            UiFrame(
                CenteredText("Deck screen not implemented yet"),
                deck.Name
            )
        );

        Console.WriteLine(OptionList(new() {new("enter", "go back")}));
    }
}