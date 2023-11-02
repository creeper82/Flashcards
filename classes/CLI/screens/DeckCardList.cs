using Flashcards;
namespace CLI;

using static Components;

public partial class Screens
{
    internal static void DeckCardList(
            Card? card, int currentCardNumber,
            int maxCardNumber,
            string deckName,
            string sortName = "not sorted",
            bool isFiltered = false
        )
    {
        ClearConsole();

        if (card == null)
        {
            Console.WriteLine(
                UiFrame(
                    CenteredText(
                        isFiltered
                        ? "No cards meet the filter criteria"
                        :   "This deck has no cards\n" +
                            "Add a new card with [N]"
                    ),
                    deckName
                )
            );
        }
        else
        {
            string topText = "" +
                (isFiltered ? "   FILTERING APPLIED" : new(' ', 20)) +
                (card.Tagged ? "   TAGGED" : new(' ', 9));

            Console.WriteLine(
                UiFrame(
                    inner: 
                        RightAlignedText(topText) + "\n" +
                        CenteredText(
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
                ? KeyboardActions.DeckCardListScreen
                : (
                    isFiltered
                    ? KeyboardActions.DeckCardListScreenEmptyFiltered
                    : KeyboardActions.DeckCardListScreenEmpty
                )
            )
        );
    }
}