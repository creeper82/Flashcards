namespace Flashcards;

using SharpViews;
using static Flashcards.Sorting;
using static Flashcards.Filtering;

public static partial class App
{
    public static void DeckCardList(FlashcardsDatabase database, Deck deck, SortType sortType = 0, CardFilter? cardFilter = null)
    {
        ChoiceList<Card> cardChoiceList = new(deck.Cards.ApplySort(sortType));

        // if the card filter wasn't specified, create a new one
        cardFilter ??= new();

        bool running = true;

        while (running)
        {
            cardChoiceList.UpdateChoices(
                deck.Cards
                .ApplySort(sortType)
                .ApplyFilter(cardFilter));

            CLI.Screens.DeckCardList(
                card: cardChoiceList.SelectedChoice,
                currentCardNumber: cardChoiceList.SelectedIndex + 1,
                maxCardNumber: cardChoiceList.MaxIndex + 1,
                deckName: deck.Name,
                sortName: sortType.GetName(),
                isFiltered: cardFilter.HasAnyFilter
            );

            Logic.HandleDeckCardListResult handleResult = Logic.HandleDeckCardList(
                database: database,
                cardChoiceList: cardChoiceList,
                deck: deck,
                hasAnyFilter: cardFilter.HasAnyFilter
            );
            if (handleResult == Logic.HandleDeckCardListResult.ExitList) break;
            if (handleResult == Logic.HandleDeckCardListResult.ChangeSort) sortType = SortTypePicker(sortType);
            if (handleResult == Logic.HandleDeckCardListResult.ChangeFilter) cardFilter = CardFilterPicker(cardFilter);
            if (handleResult == Logic.HandleDeckCardListResult.MoveRight) cardChoiceList.MoveForward();
            if (handleResult == Logic.HandleDeckCardListResult.MoveLeft) cardChoiceList.MoveBackward();
        }
    }
}