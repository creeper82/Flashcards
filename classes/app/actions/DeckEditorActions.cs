namespace FlashcardsApp;

using Flashcards;

public static partial class Interactions
{
    private static void RemoveCardAction(FlashcardsDatabase database, Card card) {

        if(CLI.Dialogs.Confirm(
            title: "Remove card",
            message: "Do you want to remove this card?",
            okButton: "remove",
            cancelButton: "cancel"
        )) {
            database.RemoveCard(card);
        }
    }
}