namespace FlashcardsApp;

using Flashcards;
using CLI;

public static partial class App
{
    // accepts a card and then returns the edited one
    public static Card CardEditor(FlashcardsDatabase database, Card card, string title = "Edit card")
    {
        bool running = true;

        Card cardCopy = card.Clone();

        while (running)
        {
            Screens.CardEditor(cardCopy, title);
            var interactionStatus = Logic.HandleCardEditor(cardCopy);

            if (interactionStatus is Logic.HandleCardEditorResult.SaveChanges)
            {
                return cardCopy;
            }
            if (interactionStatus is Logic.HandleCardEditorResult.CancelChanges)
            {
                running = false;
            }
        }


        return card;
    }
}