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
                    case ConsoleKey.Spacebar:
                    case ConsoleKey.Enter:
                        Deck(deckChoiceList.SelectedItem);
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
                    // New deck
                    case ConsoleKey.N:
                        Deck? newDeck = NewDeckAction(database);
                        if(newDeck != null) deckChoiceList.MoveToChoice(newDeck);
                        break;
                    // Exit app
                    case ConsoleKey.Escape:
                        running = false;
                        break;
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

    private static Deck RenameDeckAction(FlashcardsDatabase database, Deck deck) {
        string newName = Dialogs.Input(
            title: "Rename deck",
            message: $"Enter a new name for deck: {deck.Name}"
        ).Trim();

        if (newName != "") database.RenameDeck(deck, newName);

        return deck;
    }

    private static Deck? NewDeckAction(FlashcardsDatabase database) {
        string newName = Dialogs.Input(
            title: "New deck",
            message: "Enter deck name"
        ).Trim();

        if (newName != "") return database.CreateDeck(newName);
        return null;
    }

    public static void Deck(Deck deck)
    {
        Screens.Deck(deck);
        Console.ReadKey();
    }
}