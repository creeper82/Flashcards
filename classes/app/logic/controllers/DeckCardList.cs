namespace Flashcards;

using SharpViews;

public static partial class Logic
{
    public enum HandleDeckCardListResult {
        ContinueLoop, ChangeSort, ChangeFilter, ExitList
    }

    public static HandleDeckCardListResult HandleDeckCardList(
        FlashcardsDatabase database,
        ChoiceList<Card> cardChoiceList,
        Deck deck,
        bool hasAnyFilter
    )
    {
        ConsoleKey consoleKey = ConsoleInput.GetConsoleKey();
        Card? card = cardChoiceList.SelectedChoice;

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
                    return HandleDeckCardListResult.ChangeSort;
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
                return HandleDeckCardListResult.ExitList;
        }

        // When there are none or some cards, but filtering is already applied, allow to change the filter
        if (card is not null || hasAnyFilter)
        {
            if (consoleKey == ConsoleKey.F)
            {
                return HandleDeckCardListResult.ChangeFilter;
            }
        }

        return HandleDeckCardListResult.ContinueLoop;
    }
}