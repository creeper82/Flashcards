namespace FlashcardsApp;

using Flashcards;
using CLI;

public static class App {
    public static void Start(FlashcardsDatabase database) {
        CLI.Menu();
    }
}