namespace FlashcardsApp;

using Flashcards;

public static partial class Logic
{
    private static void RemoveCard(FlashcardsDatabase database, Card card)
    {

        if (CLI.Dialogs.Confirm(
            title: "Remove card",
            message: "Do you want to remove this card?",
            okButton: "remove",
            cancelButton: "cancel"
        ))
        {
            database.RemoveCard(card);
        }
    }

    private static void EditCard(FlashcardsDatabase database, Card card)
    {
        var editedCard = App.CardEditor(database, card);
        database.UpdateCard(card, editedCard.Front, editedCard.Back);
    }

    private static Card? CreateCard(FlashcardsDatabase database, Deck deck)
    {
        var newCard = App.CardEditor(database, Card.EmptyCard(deck), "New card");
        if (newCard.Front != "" && newCard.Back != "")
        {
            database.AppendCard(newCard);
            return newCard;
        }
        return null;
    }

    private static void TagOrUntagCard(FlashcardsDatabase database, Card card) {
        database.UpdateCard(card: card, newTaggedState: !card.TaggedAsLearning);
    }
}