namespace Flashcards;
using SharpViews;

public static partial class Logic
{
    public enum HandleStudySessionResult
    {
        ContinueLoop, RevealOrNext, MoveBackward, RestartSession, ContinueOnlyTagged, Exit
    }

    public static HandleStudySessionResult HandleStudySession(
        FlashcardsDatabase database,
        ChoiceList<Card> cardChoiceList
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
                    return HandleStudySessionResult.MoveBackward;
                case ConsoleKey.Enter:
                case ConsoleKey.Spacebar:
                case ConsoleKey.RightArrow:
                    return HandleStudySessionResult.RevealOrNext;
                case ConsoleKey.T:
                    TagOrUntagCard(database, card);
                    break;
                case ConsoleKey.C:
                    return HandleStudySessionResult.ContinueOnlyTagged;
                case ConsoleKey.R:
                    return HandleStudySessionResult.RestartSession;
            }
        }

        // Options available no matter if there are any cards
        switch (consoleKey)
        {
            case ConsoleKey.Escape:
                return HandleStudySessionResult.Exit;
        }

        return HandleStudySessionResult.ContinueLoop;
    }
}