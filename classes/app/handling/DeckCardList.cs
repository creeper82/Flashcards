namespace FlashcardsApp;

using Flashcards;

public static partial class Interactions
{
    public static bool HandleDeckCardList(FlashcardsDatabase database, CLI.ChoiceList<Card> cardChoiceList)
    {
        ConsoleKey consoleKey = Console.ReadKey().Key;

        // Options only available when there are any cards
        if (cardChoiceList.SelectedItem != null) {
            switch (consoleKey) {
                case ConsoleKey.LeftArrow:
                    cardChoiceList.MoveBackward();
                    break;
                case ConsoleKey.RightArrow:
                    cardChoiceList.MoveForward();
                    break;
                case ConsoleKey.Enter:
                    // Edit current card
                    break;
                case ConsoleKey.Delete:
                    // Delete current card
                    break;
                
            }
        }
        // Options available no matter if there are any cards
        switch (consoleKey)
        {
            case ConsoleKey.N:
                // Add new card
                break;
            case ConsoleKey.Escape:
                return false;
        }

        return true;
    }
}