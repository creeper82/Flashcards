namespace FlashcardsApp;
using Flashcards;
using CLI;
using static Flashcards.Sorting;

public static partial class App
{
    public static void DeckCardList(FlashcardsDatabase database, Deck deck, SortType sortType = 0)
    {
        ChoiceList<Card> cardChoiceList = new(deck.Cards.SortBy(sortType));

        bool running = true;

        while (running)
        {
            cardChoiceList.CheckOutOfBoundsPointer();
            cardChoiceList.choices = deck.Cards.SortBy(sortType);
            Screens.DeckCardList(
                card: cardChoiceList.SelectedItem,
                currentCardNumber: cardChoiceList.selectedIndex + 1,
                maxCardNumber: cardChoiceList.MaxIndex + 1,
                deckName: deck.Name,
                sortName: sortType.SortFriendlyName()
            );

            Logic.HandleDeckCardListResult handleResult = Logic.HandleDeckCardList(database, cardChoiceList, deck, sortType);
            if (handleResult is Logic.HandleDeckCardListResult.ChangeSort result)
            {
                sortType = result.newSortType;
            }
            if (handleResult is Logic.HandleDeckCardListResult.ExitList) running = false;
        }
    }
}