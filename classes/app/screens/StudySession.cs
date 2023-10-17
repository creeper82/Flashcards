namespace FlashcardsApp;

using Flashcards;
using CLI;

public static partial class App {
    public static void StudySession(FlashcardsDatabase database, List<Card> cards) {
        ChoiceList<Card> cardChoiceList = new(cards);

        bool running = true;
        bool isCardRevealed = false;

        while (running) {
            Screens.StudySession(
                card: cardChoiceList.SelectedItem,
                currentCardNumber: cardChoiceList.selectedIndex + 1,
                maxCardNumber: cardChoiceList.MaxIndex + 1,
                revealCard: isCardRevealed,
                sessionFinished: (cardChoiceList.selectedIndex == cardChoiceList.MaxIndex) && isCardRevealed
            );
            
            var handleResult = Interactions.HandleStudySession(cardChoiceList);

            if (handleResult is Interactions.HandleStudySessionResult.RevealOrNext) {
                if (isCardRevealed && (cardChoiceList.selectedIndex != cardChoiceList.MaxIndex)) {
                    cardChoiceList.MoveForward();
                    isCardRevealed = false;
                }
                else isCardRevealed = true;
                
            }
            if (handleResult is Interactions.HandleStudySessionResult.MoveBackward) {
                cardChoiceList.MoveBackward();
                isCardRevealed = true;
            }
            if (handleResult is Interactions.HandleStudySessionResult.Exit) running = false;
        }

    }
}