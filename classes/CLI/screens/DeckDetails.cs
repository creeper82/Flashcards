using Flashcards;
namespace CLI;

using static Components;

public partial class Screens
{
    internal static void DeckDetails(Deck deck)
    {
        ClearConsole();

        Console.WriteLine(
            UiFrame(
                // CenteredText($"The selected deck has ID {deck.Id}") + "\n" +
                // CenteredText($"Created at {deck.CreationTimestamp.ToLocalTime()}")
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