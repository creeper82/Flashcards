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

        CLI.ChoiceList<Deck> deckChoiceList = new(database.GetDecks());
        while (true)
        {
            CLI.Menu(deckChoiceList.choices, deckChoiceList.selectedIndex);

            try
            {
                ConsoleKey consoleKey = Console.ReadKey().Key;
                if (consoleKey == ConsoleKey.UpArrow) deckChoiceList.moveBackward();
                else if (consoleKey == ConsoleKey.DownArrow) deckChoiceList.moveForward();
                else if (consoleKey == ConsoleKey.Enter) Deck(deckChoiceList.selectedItem);
            }
            catch (InvalidOperationException) { break; }
        }

    }

    public static void Deck(Deck deck)
    {
        CLI.Deck(deck);
        Console.ReadKey();
    }
}