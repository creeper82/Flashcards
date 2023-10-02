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

        while (true)
        {
            deckChoiceList.CheckOutOfBoundsPointer();
            Screens.Menu(deckChoiceList.choices, deckChoiceList.selectedIndex);

            try
            {
                ConsoleKey consoleKey = Console.ReadKey().Key;
                switch (consoleKey)
                {
                    case ConsoleKey.UpArrow:
                        deckChoiceList.MoveBackward();
                        break;
                    case ConsoleKey.DownArrow:
                        deckChoiceList.MoveForward();
                        break;
                    case ConsoleKey.Enter:
                        Deck(deckChoiceList.SelectedItem);
                        break;
                    case ConsoleKey.Delete:
                        Deck deck = deckChoiceList.SelectedItem;
                        int cards = deck.Cards.Count;

                        if (Dialogs.Confirm(
                            title: "Remove deck",
                            message: $"Do you want to remove the deck: {deck.Name}?\n\n" +
                            (cards > 0 ? $"Number of cards in deck: {deck.Cards.Count}" : "This deck has no cards"),
                            
                            okButton: "remove",
                            cancelButton: "cancel"
                        ))
                        {
                            database.RemoveDeck(deckChoiceList.SelectedItem);
                        }

                        break;
                }

            }
            catch (InvalidOperationException) { break; }
        }

    }

    public static void Deck(Deck deck)
    {
        Screens.Deck(deck);
        Console.ReadKey();
    }
}