namespace Flashcards.CLI;

using static SharpViews.Components;

public partial class Screens
{
    internal static void DeckDetails(Deck deck)
    {
        ClearConsole();

        Console.WriteLine(
            UiFrame(
                CenteredText(
                    "Deck details" + "\n\n" +
                    $"Card count: {deck.Cards.Count}" + "\n" +
                    $"Created at: {deck.CreationTimestamp.ToLocalTime()}" + "\n\n" +
                    $"Internal deck ID: {deck.Id}" + "\n\n" +
                    "Press any key to go back..."
                ),
                deck.Name
            )
        );
    }
}