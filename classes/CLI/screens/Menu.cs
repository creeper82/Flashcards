namespace Flashcards.CLI;

using static SharpViews.Components;
using static Flashcards.CLI.Components;

public partial class Screens
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
                    verticalScroll: decks.Any()
                    )
        );

        // Display keyboard actions
        Console.WriteLine(
            KeyboardActionList(decks.Any() ? KeyboardActions.DeckListScreen : KeyboardActions.DeckListScreenEmpty)
        );
    }
}