namespace FlashcardsApp;

using Flashcards;

public static partial class Logic
{
    public class HandleStudySessionResult
    {
        public class RevealOrNext : HandleStudySessionResult { }
        public class Exit : HandleStudySessionResult { }
        public class ContinueLoop : HandleStudySessionResult { }
        public class MoveBackward : HandleStudySessionResult { }
    }
    
    public static HandleStudySessionResult HandleStudySession(
        CLI.ChoiceList<Card> cardChoiceList
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
                    return new HandleStudySessionResult.MoveBackward();
                case ConsoleKey.Enter:
                case ConsoleKey.Spacebar:
                    return new HandleStudySessionResult.RevealOrNext();
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