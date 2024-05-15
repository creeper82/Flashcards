namespace Flashcards;
using SharpViews;

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

            CLI.Screens.StudySession(
                card: cardChoiceList.SelectedChoice,
                currentCardNumber: cardChoiceList.SelectedIndex + 1,
                maxCardNumber: cardChoiceList.MaxIndex + 1,
                revealCard: isCardRevealed,
                sessionFinished: (cardChoiceList.SelectedIndex == cardChoiceList.MaxIndex) && isCardRevealed
            );

            var handleResult = Logic.HandleStudySession(database, cardChoiceList);

            if (handleResult is Logic.HandleStudySessionResult.RevealOrNext)
            {
                if (isCardRevealed && (cardChoiceList.SelectedIndex != cardChoiceList.MaxIndex))
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