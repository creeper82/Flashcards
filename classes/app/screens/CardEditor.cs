namespace Flashcards;

public static partial class App
{
    // accepts a card and then returns the edited one
    public static Card CardEditor(Card card, string title = "Edit card")
    {
        bool running = true;

        Card cardCopy = card.Clone();

        while (running)
        {
            CLI.Screens.CardEditor(cardCopy, title);
            var interactionStatus = Logic.HandleCardEditor(cardCopy);

            if (interactionStatus == Logic.HandleCardEditorResult.SaveChanges)
            {
                return cardCopy;
            }
            if (interactionStatus == Logic.HandleCardEditorResult.CancelChanges)
            {
                running = false;
            }
        }


        return card;
    }
}