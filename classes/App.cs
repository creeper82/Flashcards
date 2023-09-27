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
                deckChoiceList.waitForArrowKey();
            }
            catch (InvalidOperationException) { break; }
        }

    }
}