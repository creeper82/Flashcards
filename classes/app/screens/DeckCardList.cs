namespace FlashcardsApp;
using Flashcards;
using CLI;
using static Flashcards.Sorting;
using static Flashcards.Filtering;

public static partial class App
{
    public static void DeckCardList(FlashcardsDatabase database, Deck deck, SortType sortType = 0, CardFilter? cardFilter = null)
    {
        ChoiceList<Card> cardChoiceList = new(deck.Cards.SortBy(sortType));

        // if the card filter wasn't specified, create a new one
        cardFilter ??= new();

        bool running = true;

        while (running)
        {
            cardChoiceList.choices = deck.Cards
                .SortBy(sortType)
                .ApplyFilter(cardFilter);
            cardChoiceList.CheckOutOfBoundsPointer();

            Screens.DeckCardList(
                card: cardChoiceList.SelectedItem,
                currentCardNumber: cardChoiceList.selectedIndex + 1,
                maxCardNumber: cardChoiceList.MaxIndex + 1,
                deckName: deck.Name,
                sortName: sortType.SortFriendlyName(),
                isFiltered: cardFilter.HasAnyFilter
            );

            Logic.HandleDeckCardListResult handleResult = Logic.HandleDeckCardList(
                database: database,
                cardChoiceList: cardChoiceList,
                deck: deck,
                currentSortType: sortType,
                currentCardFilter: cardFilter
            );

            if (handleResult is Logic.HandleDeckCardListResult.ChangeSort changeSortResult)
            {
                sortType = changeSortResult.newSortType;
            }
            if (handleResult is Logic.HandleDeckCardListResult.ChangeFilter changeFilterResult) {
                cardFilter = changeFilterResult.newCardFilter;
            }
            else if (handleResult is Logic.HandleDeckCardListResult.ExitList) running = false;
        }
    }
}