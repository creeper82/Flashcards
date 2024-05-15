namespace Flashcards;
using SharpViews;

public static partial class Logic
{
    public class HandleStudySessionResult
    {
        public class RevealOrNext : HandleStudySessionResult { }
        public class Exit : HandleStudySessionResult { }
        public class ContinueLoop : HandleStudySessionResult { }
        public class MoveBackward : HandleStudySessionResult { }
        public class RestartSession : HandleStudySessionResult { }
        public class ContinueOnlyTagged : HandleStudySessionResult { }
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
                    return new HandleStudySessionResult.MoveBackward();
                case ConsoleKey.Enter:
                case ConsoleKey.Spacebar:
                case ConsoleKey.RightArrow:
                    return new HandleStudySessionResult.RevealOrNext();
                case ConsoleKey.T:
                    TagOrUntagCard(database, card);
                    break;
                case ConsoleKey.C:
                    return new HandleStudySessionResult.ContinueOnlyTagged();
                case ConsoleKey.R:
                    return new HandleStudySessionResult.RestartSession();
            }
        }

        // Options available no matter if there are any cards
        switch (consoleKey)
        {
            case ConsoleKey.Escape:
                return new HandleStudySessionResult.Exit();
        }

        return new HandleStudySessionResult.ContinueLoop();
    }
}