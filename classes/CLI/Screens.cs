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
        throw new NotImplementedException();
    }
}