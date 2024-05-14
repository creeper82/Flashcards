namespace Flashcards.CLI;

using static SharpViews.Components;
using static Flashcards.CLI.Components;

public partial class Screens
{
    internal static void CardEditor(Card card, string title = "Edit card")
    {
        ClearConsole();

        Console.WriteLine(
            UiFrame(
                DeckCard(card, true),
                title
            )
        );

        Console.WriteLine(KeyboardActionList(KeyboardActions.CardEditorScreen));
    }
}