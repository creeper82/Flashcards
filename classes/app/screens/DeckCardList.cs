namespace FlashcardsApp;

using Flashcards;
using CLI;

public static partial class App {
    public static void DeckCardList(FlashcardsDatabase database, Deck deck)
{
    ChoiceList<Card> cardChoiceList = new(deck.Cards);

    bool running = true;

    while (running)
    {
        cardChoiceList.CheckOutOfBoundsPointer();
        Screens.DeckCardList(
            card: cardChoiceList.SelectedItem,
            currentCardNumber: cardChoiceList.selectedIndex + 1,
            maxCardNumber: cardChoiceList.MaxIndex + 1,
            deckName: deck.Name
        );

        running = Interactions.HandleDeckCardList(database, cardChoiceList);
    }
}
}