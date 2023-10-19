using Flashcards;
namespace CLI;

using static Components;

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