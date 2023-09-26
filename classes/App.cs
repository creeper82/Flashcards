namespace FlashcardsApp;

using Flashcards;
using CLI;

public static class App {
    public static void Start(FlashcardsDatabase database) {
        database.ResetAll();
        database.CreateDeck("Aaaa");
        database.CreateDeck("Sample dec");
        database.CreateDeck("Another deck");
        CLI.Menu(database.GetDecks());
    }
}