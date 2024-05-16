namespace Flashcards;
using SharpViews;

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
            var handleCardEditorResult = Logic.HandleCardEditor();

            if (handleCardEditorResult == Logic.HandleCardEditorResult.EditFront) {
                string newFront = Dialogs.Input("Edit front", $"Currently: {cardCopy.Front}").Trim();
                if (newFront != "") cardCopy.Front = newFront;
            }
            if (handleCardEditorResult == Logic.HandleCardEditorResult.EditBack) {
                string newBack = Dialogs.Input("Edit back", $"Currently: {cardCopy.Back}").Trim();
                if (newBack != "") cardCopy.Back = newBack;
            }
            if (handleCardEditorResult == Logic.HandleCardEditorResult.Swap) (cardCopy.Back, cardCopy.Front) = (cardCopy.Front, cardCopy.Back);
            if (handleCardEditorResult == Logic.HandleCardEditorResult.SaveChanges) return cardCopy;
            if (handleCardEditorResult == Logic.HandleCardEditorResult.CancelChanges)
            {
                running = false;
            }
        }


        return cardCopy;
    }
}