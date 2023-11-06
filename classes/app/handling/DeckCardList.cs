namespace FlashcardsApp;

using Flashcards;

public static partial class Logic
{
    public class HandleDeckCardListResult
    {
        public class ChangeSort : HandleDeckCardListResult
        {
            public Sorting.SortType newSortType;
            public ChangeSort(Sorting.SortType newSortType)
            {
                this.newSortType = newSortType;
            }
        }

        public class ChangeFilter : HandleDeckCardListResult
        {
            public Filtering.CardFilter newCardFilter;
            public ChangeFilter(Filtering.CardFilter newCardFilter)
            {
                this.newCardFilter = newCardFilter;
            }
        }

        public class ExitList : HandleDeckCardListResult { }
        public class ContinueLoop : HandleDeckCardListResult { }
    }

    public static HandleDeckCardListResult HandleDeckCardList(
        FlashcardsDatabase database,
        CLI.ChoiceList<Card> cardChoiceList,
        Deck deck,
        Sorting.SortType currentSortType,
        Filtering.CardFilter currentCardFilter
    )
    {
        ConsoleKey consoleKey = Console.ReadKey().Key;
        Card? card = cardChoiceList.SelectedItem;

        // Options only available when there are any cards
        if (card is not null)
        {
            switch (consoleKey)
            {
                case ConsoleKey.LeftArrow:
                    cardChoiceList.MoveBackward();
                    break;
                case ConsoleKey.RightArrow:
                    cardChoiceList.MoveForward();
                    break;
                case ConsoleKey.Enter:
                case ConsoleKey.F2:
                    EditCard(database, card);
                    break;
                case ConsoleKey.Delete:
                    RemoveCard(database, card);
                    break;
                case ConsoleKey.T:
                    TagOrUntagCard(database, card);
                    break;
                case ConsoleKey.S:
                    return new HandleDeckCardListResult.ChangeSort(
                        App.SortTypePicker(currentSortType)
                    );
            }
        }

        // Options available no matter if there are any cards
        switch (consoleKey)
        {
            case ConsoleKey.N:
                var newCard = CreateCard(database, deck);
                if (newCard != null) cardChoiceList.MoveToChoice(newCard);
                break;
            case ConsoleKey.Escape:
                return new HandleDeckCardListResult.ExitList();
        }

        // When there are none or some cards, but filtering is already applied, allow to change the filter
        if (card is not null || currentCardFilter.HasAnyFilter)
        {
            if (consoleKey == ConsoleKey.F)
            {
                return new HandleDeckCardListResult.ChangeFilter(
                        App.CardFilterPicker(currentCardFilter)
                );
            }
        }

        return new HandleDeckCardListResult.ContinueLoop();
    }
}