namespace FlashcardsApp;

using Flashcards;
using CLI;

public static partial class App
{
    // accepts a card and then returns the edited one
    public static Card CardEditor(FlashcardsDatabase database, Card card) {
        bool running = true;
        
        Card cardCopy = card.Clone();

        while (running) {
            Screens.CardEditor(cardCopy);
            var interactionStatus = Interactions.HandleCardEditor(database, cardCopy);

            if (interactionStatus is Interactions.HandleCardEditorResult.SaveChanges) {
                return cardCopy;
            }
            if (interactionStatus is Interactions.HandleCardEditorResult.CancelChanges) {
                running = false;
            }
        }


        return card;
    }
}