using Flashcards;
namespace CLI;

using static Components;

public partial class Screens
{
    internal static void DeckCardList(
            Card? card, int currentCardNumber,
            int maxCardNumber,
            string deckName,
            string sortName = "not sorted"
        )
    {
        ClearConsole();

        if (card == null)
        {
            Console.WriteLine(
                UiFrame(
                    CenteredText(
                        "This deck has no cards\n" +
                        "Add a new card with [N]"
                    ),
                    deckName
                )
            );
        }
        else
        {
            Console.WriteLine(
                UiFrame(
                    inner: CenteredText(
                        $"Card {currentCardNumber} of {maxCardNumber}  **  {sortName}"
                    ) + "\n\n" +
                    DeckCard(card, true),
                    title: deckName,
                    horizontalScroll: true
                )
            );
        }
        // Display keyboard actions
        Console.WriteLine(
            KeyboardActionList(
                card is not null
                ? KeyboardActions.DeckCardListScreen :
                KeyboardActions.DeckCardListScreenEmpty
            )
        );
    }
}