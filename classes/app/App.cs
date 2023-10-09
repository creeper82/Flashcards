namespace FlashcardsApp;

using Flashcards;
using CLI;

public static class App
{
    public static void Start(FlashcardsDatabase database)
    {
        database.ResetAll();
        database.CreateDeck("Aaaa");
        database.CreateDeck("Sample dec");
        database.CreateDeck("Another deck");
        database.CreateCard(database.GetDecks().First(), "a", "b");

        Console.OutputEncoding = System.Text.Encoding.UTF8;

        ChoiceList<Deck> deckChoiceList = new(database.GetDecks());

        bool running = true;

        while (running)
        {
            deckChoiceList.CheckOutOfBoundsPointer();
            Screens.Menu(deckChoiceList.choices, deckChoiceList.selectedIndex);

            try
            {
                ConsoleKey consoleKey = Console.ReadKey().Key;

                switch (consoleKey)
                {
                    // Navigation
                    case ConsoleKey.UpArrow:
                        deckChoiceList.MoveBackward();
                        break;
                    case ConsoleKey.DownArrow:
                        deckChoiceList.MoveForward();
                        break;
                    // New deck
                    case ConsoleKey.N:
                        Deck? newDeck = NewDeckAction(database);
                        if (newDeck != null) deckChoiceList.MoveToChoice(newDeck);
                        break;
                    // Exit app
                    case ConsoleKey.Escape:
                        running = false;
                        break;
                }
                // Only if the user has decks
                if (deckChoiceList.SelectedItem != null)
                {
                    switch (consoleKey)
                    {
                        // Navigation
                        case ConsoleKey.Spacebar:
                        case ConsoleKey.Enter:
                            Deck(database, deckChoiceList.SelectedItem);
                            break;
                        // Deck editing
                        case ConsoleKey.Delete:
                            RemoveDeckDialog(database, deckChoiceList.SelectedItem);
                            break;
                        case ConsoleKey.R:
                        case ConsoleKey.F2:
                            Deck renamedDeck = RenameDeckAction(database, deckChoiceList.SelectedItem);
                            deckChoiceList.MoveToChoice(renamedDeck);
                            break;
                    }
                }

            }
            catch (InvalidOperationException) { break; }
        }

    }

    private static void RemoveDeckDialog(FlashcardsDatabase database, Deck deck)
    {
        int cards = deck.Cards.Count;

        if (Dialogs.Confirm(
            title: "Remove deck",
            message: $"Do you want to remove the deck: {deck.Name}?\n\n" +
            (cards > 0 ? $"Number of cards in deck: {deck.Cards.Count}" : "This deck has no cards"),

            okButton: "remove",
            cancelButton: "cancel"
        ))
        {
            database.RemoveDeck(deck);
        }
    }

    private static Deck RenameDeckAction(FlashcardsDatabase database, Deck deck)
    {
        string newName = Dialogs.Input(
            title: "Rename deck",
            message: $"Enter a new name for deck: {deck.Name}"
        ).Trim();

        if (newName != "") database.RenameDeck(deck, newName);

        return deck;
    }

    private static Deck? NewDeckAction(FlashcardsDatabase database)
    {
        string newName = Dialogs.Input(
            title: "New deck",
            message: "Enter deck name"
        ).Trim();

        if (newName != "") return database.CreateDeck(newName);
        return null;
    }

    public static void Deck(FlashcardsDatabase database, Deck deck)
    {
        bool running = true;

        while (running)
        {
            Screens.Deck(deck);
            ConsoleKey consoleKey = Console.ReadKey().Key;

            switch (consoleKey)
            {
                case ConsoleKey.Delete:
                    RemoveDeckDialog(database, deck);
                    break;
                case ConsoleKey.R:
                case ConsoleKey.F2:
                    RenameDeckAction(database, deck);
                    break;
                case ConsoleKey.C:
                    DeckCardList(database, deck);
                    break;
                case ConsoleKey.I:
                    //TODO: implement deck details
                    break;
                case ConsoleKey.Escape:
                    running = false;
                    break;
            }
        }

    }

    public static void DeckCardList(FlashcardsDatabase database, Deck deck)
    {
        ChoiceList<Card> cardChoiceList = new(deck.Cards);

        bool running = true;

        while (running)
        {
            cardChoiceList.CheckOutOfBoundsPointer();
            Screens.CardEditor(
                card: cardChoiceList.SelectedItem,
                currentCardNumber: cardChoiceList.selectedIndex + 1,
                maxCardNumber: cardChoiceList.MaxIndex - 1,
                deckName: deck.Name
            );

            ConsoleKey consoleKey = Console.ReadKey().Key;
        }
    }
}