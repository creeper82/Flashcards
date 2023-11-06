namespace FlashcardsApp;

using Flashcards;
using CLI;

public static partial class App
{
    public static void StudySession(FlashcardsDatabase database, IEnumerable<Card> cards)
    {
        IEnumerable<Card> originalCardSet = cards.ToList();
        ChoiceList<Card> cardChoiceList = new(cards);

        bool running = true;
        bool isCardRevealed = false;
        // when set to true, study session will re-shuffle cards and go back to first card
        bool resetStudySession = true;

        while (running)
        {
            if (resetStudySession)
            {
                cards = cards.Shuffle();
                cardChoiceList = new(cards);
                isCardRevealed = false;
                resetStudySession = false;
            }

            Screens.StudySession(
                card: cardChoiceList.SelectedItem,
                currentCardNumber: cardChoiceList.selectedIndex + 1,
                maxCardNumber: cardChoiceList.MaxIndex + 1,
                revealCard: isCardRevealed,
                sessionFinished: (cardChoiceList.selectedIndex == cardChoiceList.MaxIndex) && isCardRevealed
            );

            var handleResult = Logic.HandleStudySession(database, cardChoiceList);

            if (handleResult is Logic.HandleStudySessionResult.RevealOrNext)
            {
                if (isCardRevealed && (cardChoiceList.selectedIndex != cardChoiceList.MaxIndex))
                {
                    cardChoiceList.MoveForward();
                    isCardRevealed = false;
                }
                else isCardRevealed = true;

            }
            if (handleResult is Logic.HandleStudySessionResult.MoveBackward)
            {
                cardChoiceList.MoveBackward();
                isCardRevealed = true;
            }
            if (handleResult is Logic.HandleStudySessionResult.RestartSession)
            {
                cards = originalCardSet;
                resetStudySession = true;
            }
            if (handleResult is Logic.HandleStudySessionResult.ContinueOnlyTagged)
            {
                cards = cards.ApplyFilter(new(onlyTagged: true));
                resetStudySession = true;
            }
            if (handleResult is Logic.HandleStudySessionResult.Exit) running = false;
        }

    }
}