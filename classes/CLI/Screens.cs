using Flashcards;
namespace CLI;

public static partial class CLI {
    public static void Menu(IEnumerable<Deck> decks, int selectedDeckIndex)
    {
        ClearConsole();
        Console.WriteLine(
            UiFrame(
                (
                    MultilineCenteredText("Welcome to Flashcards!\nHere are your decks:\n") +
                    DeckList(decks, selectedDeckIndex)
                ),
                "Flashcards")
        );
    }
}