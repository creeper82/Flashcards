using Flashcards;
namespace CLI;

using static Components;

public partial class Screens
{
    internal static void StudySession(
        Card? card, int currentCardNumber,
        int maxCardNumber,
        bool revealCard,
        bool sessionFinished = false
    )
    {
        ClearConsole();

        if (card is null)
        {
            Console.WriteLine(
                UiFrame(
                    inner: CenteredText("Empty study session\nA study session must have at least one card"),
                    title: "Study session"
                )
            );
        }
        else
        {
            Console.WriteLine(
                UiFrame(
                    inner: CenteredText(
                        $"Card {currentCardNumber} of {maxCardNumber}"
                    ) + "\n\n" +
                    DeckCard(card, revealCard) +
                    (
                        sessionFinished
                        ? CenteredText("\nYou have finished the study session!")
                        : ""
                    ),
                    title: "Study session"
                )
            );
        }
        // Display keyboard actions
        Console.WriteLine(KeyboardActionList(KeyboardActions.StudySessionScreen));
    }
}