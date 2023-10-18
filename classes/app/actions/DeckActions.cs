namespace FlashcardsApp;

using Flashcards;

public static partial class Logic
{
    // tries to remove a deck and returns true if it was removed (if users accepts the dialog)
    private static bool RemoveDeck(FlashcardsDatabase database, Deck deck)
    {
        int cards = deck.Cards.Count;

        if (CLI.Dialogs.Confirm(
            title: "Remove deck",
            message: $"Do you want to remove the deck: {deck.Name}?\n\n" +
            (cards > 0 ? $"Number of cards in deck: {deck.Cards.Count}" : "This deck has no cards"),

            okButton: "remove",
            cancelButton: "cancel"
        ))
        {
            database.RemoveDeck(deck);
            return true;
        }

        return false;
    }

    private static Deck RenameDeck(FlashcardsDatabase database, Deck deck)
    {
        string newName = CLI.Dialogs.Input(
            title: "Rename deck",
            message: $"Enter a new name for deck: {deck.Name}"
        ).Trim();

        if (newName != "") database.RenameDeck(deck, newName);

        return deck;
    }

    private static Deck? NewDeck(FlashcardsDatabase database)
    {
        string newName = CLI.Dialogs.Input(
            title: "New deck",
            message: "Enter deck name"
        ).Trim();

        if (newName != "") return database.CreateDeck(newName);
        return null;
    }
}