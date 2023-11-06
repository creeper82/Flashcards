namespace FlashcardsApp;

using Flashcards;
using CLI;

public static partial class App
{
    public static void StudySession(List<Card> cards)
    {
        ChoiceList<Card> cardChoiceList = new(cards);

        bool running = true;
        bool isCardRevealed = false;

        while (running)
        {
            Screens.StudySession(
                card: cardChoiceList.SelectedItem,
                currentCardNumber: cardChoiceList.selectedIndex + 1,
                maxCardNumber: cardChoiceList.MaxIndex + 1,
                revealCard: isCardRevealed,
                sessionFinished: (cardChoiceList.selectedIndex == cardChoiceList.MaxIndex) && isCardRevealed
            );

            var handleResult = Logic.HandleStudySession(cardChoiceList);

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
            if (handleResult is Logic.HandleStudySessionResult.Exit) running = false;
        }

    }
}