namespace Flashcards.CLI;

using static SharpViews.Components;
using static Flashcards.CLI.Components;

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
                    inner: CenteredText(
                        isFiltered
                        ? "No cards meet the filter criteria"
                        : "This deck has no cards\n" +
                          "Add a new card with [N]"
                    ),
                    title: deckName + " ~ card editor"
                )
            );
        }
        else
        {
            string topText = "" +
                (isFiltered ? "   FILTERING APPLIED" : new(' ', 20)) +
                (card.TaggedAsLearning ? "   STILL LEARNING" : new(' ', 17));

            Console.WriteLine(
                UiFrame(
                    inner:
                        RightAlignedText(topText) +
                        CenteredText(
                            $"Card {currentCardNumber} of {maxCardNumber}  **  {sortName}"
                        ) + "\n" +
                        DeckCard(card, true),
                    title: deckName + " ~ card editor",
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