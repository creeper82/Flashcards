namespace Flashcards.CLI;

using static SharpViews.Components;
using static Flashcards.CLI.Components;

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
                    inner: CenteredText("Good job!\nYou have finished the study session"),
                    title: "Study session"
                )
            );
        }
        else
        {
            string topText =
                card.TaggedAsLearning ? "STILL LEARNING" : "";

            Console.WriteLine(
                UiFrame(
                    inner:
                        RightAlignedText(topText) +
                        CenteredText(
                        $"Card {currentCardNumber} of {maxCardNumber}"
                    ) + "\n" +
                    DeckCard(card, revealCard) +
                    (
                        sessionFinished
                        ? CenteredText(
                            "\nYou have finished the study session!" +
                            "\nContinue studying tagged cards with [C]" +
                            "\nRestart the whole study session with [R]"
                        )
                        : ""
                    ) +
                    (currentCardNumber == 1 && maxCardNumber > 1 ? "\nNote: Tag difficult cards with [T] to study them later" : ""),
                    title: "Study session"
                )
            );
        }
        // Display keyboard actions
        Console.WriteLine(
            card is null
            ? KeyboardActionList(KeyboardActions.StudySessionScreenEmpty)
            : KeyboardActionList(KeyboardActions.StudySessionScreen)
        );
    }
}