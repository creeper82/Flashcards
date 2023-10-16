namespace FlashcardsApp;

using Flashcards;

public static partial class Interactions
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

        public class ExitList : HandleDeckCardListResult {}
        public class ContinueLoop : HandleDeckCardListResult {}
    }
    public static HandleDeckCardListResult HandleDeckCardList(FlashcardsDatabase database, CLI.ChoiceList<Card> cardChoiceList, Deck deck)
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
                    EditCardAction(database, card);
                    break;
                case ConsoleKey.Delete:
                    RemoveCardAction(database, card);
                    break;
                case ConsoleKey.S:
                    return new HandleDeckCardListResult.ChangeSort(Sorting.SortType.DATE_DESCENDING);
            }
        }

        // Options available no matter if there are any cards
        switch (consoleKey)
        {
            case ConsoleKey.N:
                var newCard = CreateCardAction(database, deck);
                if (newCard != null) cardChoiceList.MoveToChoice(newCard);
                break;
            case ConsoleKey.Escape:
                return new HandleDeckCardListResult.ExitList();
        }

        return new HandleDeckCardListResult.ContinueLoop();
    }
}